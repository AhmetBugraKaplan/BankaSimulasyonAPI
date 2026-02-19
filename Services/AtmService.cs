using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;


namespace BankaSimulasyon.Services
{
    public class AtmService : IAtmService
    {
        private readonly IAtmKasetRepository _atmKasetRepository;
        private readonly IAtmRepository _atmRepository;

        public AtmService(IAtmKasetRepository atmKasetRepository, IAtmRepository atmRepository)
        {
            _atmKasetRepository = atmKasetRepository;
            _atmRepository = atmRepository;
        }


        public async Task<AtmdenParaCekmeResponse> AtmdenParaCekAsync(int atmId, int cekilecekTutar)
        {
            AtmdenParaCekmeResponse atmdenParaCekmeResponse = new();
            List<AtmKaset> kasetDizisi = await _atmKasetRepository.AtmdekiKasetleriGetirAsync(atmId);
            int atmdeBulunanToplamPara = await AtmdekiToplamParayiIdIleGetirAsync(atmId);


            if (cekilecekTutar <= 0)
            {
                atmdenParaCekmeResponse.IslemBasariliMi = false;
                atmdenParaCekmeResponse.Mesaj = "Cekilmek istenen tutar 0'dan buyuk olmalidir";
                atmdenParaCekmeResponse.Kasetler = null!;
                return atmdenParaCekmeResponse;
            }
            if (cekilecekTutar % 10 != 0)
            {
                atmdenParaCekmeResponse.IslemBasariliMi = false;
                atmdenParaCekmeResponse.Mesaj = "Cekilmek istenen para 10TL'nin katlari olmalidir";
                atmdenParaCekmeResponse.Kasetler = null!;
                return atmdenParaCekmeResponse;
            }

            if (cekilecekTutar > atmdeBulunanToplamPara)
            {
                atmdenParaCekmeResponse.IslemBasariliMi = false;
                atmdenParaCekmeResponse.Mesaj = "ATM'de yeterli para bulunmuyor";
                atmdenParaCekmeResponse.Kasetler = null!;
                return atmdenParaCekmeResponse;
            }

            List<AtmKaset> siraliKasetDizisi = kasetDizisi.OrderByDescending(k => k.Kupur).ToList();
            Dictionary<int, int> orijinalAdetler = siraliKasetDizisi.ToDictionary(k => k.Id, k => k.Adet);
            int toplamVerilenBanknot = 0;
            AtmKaset sonKullanilanKaset = null!;

            foreach (AtmKaset kaset in siraliKasetDizisi)
            {
                if (kaset.Adet > kaset.KritikDeger && kaset.Kupur <= cekilecekTutar)
                {
                    int kacKere = Math.Min(cekilecekTutar / kaset.Kupur, kaset.Adet);
                    cekilecekTutar -= kaset.Kupur * kacKere;
                    kaset.Adet -= kacKere;
                    toplamVerilenBanknot += kacKere;
                    sonKullanilanKaset = kaset;
                }
            }

            if (cekilecekTutar > 0)
            {
                foreach (AtmKaset kaset in siraliKasetDizisi.OrderBy(k => k.Kupur))
                {
                    if (kaset.Kupur <= cekilecekTutar && kaset.Adet > 0)
                    {
                        int kacKere = Math.Min(cekilecekTutar / kaset.Kupur, kaset.Adet);
                        cekilecekTutar -= kaset.Kupur * kacKere;
                        kaset.Adet -= kacKere;
                        toplamVerilenBanknot += kacKere;
                        sonKullanilanKaset = kaset;
                    }
                    if (cekilecekTutar == 0) break;
                }
            }

            int bozmaEsigi = 3;
            if (cekilecekTutar == 0 && sonKullanilanKaset != null && toplamVerilenBanknot >= bozmaEsigi)
            {
                int sonKupur = sonKullanilanKaset.Kupur;
                sonKullanilanKaset.Adet += 1;
                int bozulacak = sonKupur;

                Dictionary<int, int> bozmaOncesiAdetler = siraliKasetDizisi.ToDictionary(k => k.Id, k => k.Adet);

                foreach (AtmKaset kaset in siraliKasetDizisi.OrderByDescending(k => k.Kupur))
                {
                    if (kaset.Kupur < sonKupur && kaset.Kupur <= bozulacak && kaset.Adet > 0)
                    {
                        int kacKere = Math.Min(bozulacak / kaset.Kupur, kaset.Adet);
                        bozulacak -= kaset.Kupur * kacKere;
                        kaset.Adet -= kacKere;
                    }
                    if (bozulacak == 0) break;
                }

                if (bozulacak != 0)
                {
                    foreach (var kaset in siraliKasetDizisi)
                        kaset.Adet = bozmaOncesiAdetler[kaset.Id];
                    sonKullanilanKaset.Adet -= 1;
                }
            }

            if (cekilecekTutar == 0)
            {
                foreach (var kaset in siraliKasetDizisi)
                    await _atmKasetRepository.AtmKasetGuncelleAsync(kaset);

                var kullanilanKasetler = siraliKasetDizisi
                    .Where(k => k.Adet != orijinalAdetler[k.Id])
                    .ToList();

                foreach (var kaset in kullanilanKasetler)
                {
                    kaset.Adet = orijinalAdetler[kaset.Id] - kaset.Adet;
                }

                atmdenParaCekmeResponse.IslemBasariliMi = true;
                atmdenParaCekmeResponse.Mesaj = "Para basariyla cekildi";
                atmdenParaCekmeResponse.Kasetler = kullanilanKasetler;
                return atmdenParaCekmeResponse;
            }

            foreach (var kaset in siraliKasetDizisi)
                kaset.Adet = orijinalAdetler[kaset.Id];

            atmdenParaCekmeResponse.IslemBasariliMi = false;
            atmdenParaCekmeResponse.Mesaj = "Kupurler uyusmuyor, islem gerceklestirilemedi";
            atmdenParaCekmeResponse.Kasetler = null!;
            return atmdenParaCekmeResponse;
        }




        public async Task<AtmdenParaCekmeResponse> AtmdenParaCekAsync2(int atmId, int cekilecekTutar)
        {
            AtmdenParaCekmeResponse atmdenParaCekmeResponse = new();
            List<AtmKaset> kasetDizisi = await _atmKasetRepository.AtmdekiKasetleriGetirAsync(atmId);
            int atmdeBulunanToplamPara = await AtmdekiToplamParayiIdIleGetirAsync(atmId);

            if (cekilecekTutar % 10 == 0)
            {
                if (cekilecekTutar <= atmdeBulunanToplamPara)
                {
                    List<AtmKaset> siraliKasetDizisi = kasetDizisi.OrderByDescending(k => k.Kupur).ToList();

                    foreach (AtmKaset kaset in siraliKasetDizisi)
                    {
                        if (kaset.Adet > kaset.KritikDeger)
                        {
                            if (kaset.Kupur <= cekilecekTutar)
                            {
                                int kacKere = Math.Min(cekilecekTutar / kaset.Kupur, kaset.Adet);
                                int kalan = cekilecekTutar - (kaset.Kupur * kacKere);
                                kaset.Adet = kaset.Adet - kacKere;
                                cekilecekTutar = kalan;
                            }
                        }
                    }

                    if (cekilecekTutar > 0)
                    {
                        foreach (AtmKaset kaset in siraliKasetDizisi.OrderBy(k => k.Kupur))
                        {
                            if (kaset.Kupur <= cekilecekTutar)
                            {
                                int kacKere = Math.Min(cekilecekTutar / kaset.Kupur, kaset.Adet);
                                int kalan = cekilecekTutar - (kaset.Kupur * kacKere);
                                kaset.Adet = kaset.Adet - kacKere;
                                cekilecekTutar = kalan;
                            }
                            if (cekilecekTutar == 0) break;

                        }
                    }

                    if (cekilecekTutar == 0)
                    {
                        foreach (var kaset in siraliKasetDizisi)
                        {
                            await _atmKasetRepository.AtmKasetGuncelleAsync(kaset);
                        }
                        atmdenParaCekmeResponse.IslemBasariliMi = true;
                        atmdenParaCekmeResponse.Mesaj = "Para basariyla cekildi";
                        atmdenParaCekmeResponse.Kasetler = siraliKasetDizisi;
                        return atmdenParaCekmeResponse;
                    }

                    atmdenParaCekmeResponse.IslemBasariliMi = false;
                    atmdenParaCekmeResponse.Mesaj = "Kupurler uyusmuyor, islem gerceklestirilemedi";
                    atmdenParaCekmeResponse.Kasetler = null!;
                    return atmdenParaCekmeResponse;
                }
            }
            else
            {
                atmdenParaCekmeResponse.IslemBasariliMi = false;
                atmdenParaCekmeResponse.Mesaj = "Cekilmek istenen para 10Tl'nin katları olmalıdır";
                atmdenParaCekmeResponse.Kasetler = null!;
                return atmdenParaCekmeResponse;
            }

            atmdenParaCekmeResponse.IslemBasariliMi = false;
            atmdenParaCekmeResponse.Mesaj = "ATM'de yeterli para bulunmuyor";
            atmdenParaCekmeResponse.Kasetler = null!;
            return atmdenParaCekmeResponse;
        }




        public async Task<int> AtmdekiToplamParayiIdIleGetirAsync(int atmId)
        {
            KasetGuncellemeResponse kasetGuncellemeResponse = new KasetGuncellemeResponse();
            int AtmKasetlerToplamPara = 0;

            var kasetDizisi = await _atmKasetRepository.AtmdekiKasetleriGetirAsync(atmId);

            if (kasetDizisi.Any())
            {
                foreach (AtmKaset kaset in kasetDizisi)
                {
                    AtmKasetlerToplamPara += (kaset.Kupur * kaset.Adet);
                }
            }
            else
            {
                AtmKasetlerToplamPara = 0;

            }



            return AtmKasetlerToplamPara;
        }




        public async Task<KasetGuncellemeResponse> AtmKasetlerdekiKupurleriGuncelleAsync(int atmId, int slotNumarasi, int adet, int kupur)
        {
            KasetGuncellemeResponse kasetGuncellemeResponse = new KasetGuncellemeResponse();
            var kasetDizisi = await _atmKasetRepository.AtmdekiKasetleriGetirAsync(atmId);


            if (kasetDizisi.Any())
            {
                var hedefKaset = kasetDizisi.FirstOrDefault(k => k.SlotNumarasi == slotNumarasi);

                if (hedefKaset != null)
                {
                    hedefKaset.Adet = adet;
                    hedefKaset.Kupur = kupur;
                    kasetGuncellemeResponse.IslemBasariliMi = true;
                    await _atmKasetRepository.AtmKasetGuncelleAsync(hedefKaset);

                }
                else
                {
                    kasetGuncellemeResponse.IslemBasariliMi = false;
                    kasetGuncellemeResponse.HataKodu = "Girilen slot numarasına ait slot bulunamadı";

                }
            }
            else
            {
                kasetGuncellemeResponse.IslemBasariliMi = false;
                kasetGuncellemeResponse.HataKodu = "Girilen atmId'e ait atm bulunamadı";

            }


            
            return kasetGuncellemeResponse;
        }




        public async Task<AtmEklemeResponse> AtmEkleAsync(string konum, bool aktifMi)
        {
            AtmEklemeResponse atmEklemeResponse = new();

            int sonuc = await _atmRepository.AtmEkleAsync(konum, aktifMi);

            if (sonuc > 0)
            {
                atmEklemeResponse.IslemBasariliMi = true;
                atmEklemeResponse.Mesaj = "ATM başarıyla eklendi.";

            }
            else
            {
                atmEklemeResponse.IslemBasariliMi = false;
                atmEklemeResponse.Mesaj = "ATM eklenemedi";
            }

            return atmEklemeResponse;
        }




        public async Task<List<ATM>> AtmleriGetirAktifligeGoreAsync(bool aktifMi)
        {

            return await _atmRepository.AtmleriGetirAktifligeGoreAsync(aktifMi); 
        }



        public async Task<List<ATM>> TumAtmleriGetirAsync()
        {
            return await _atmRepository.TumAtmleriGetirAsync();
        }

    }
}