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

            Dictionary<int, int> orijinalAdetler = kasetDizisi.ToDictionary(k => k.Id, k => k.Adet);

            int toplamVerilenBanknot = 0;
            AtmKaset sonKullanilanKaset = null!;

            // ATM'deki tüm küpürler arasından en yüksek 2 küpür değeri belirlenir
            // Örnek: 200, 200, 100, 50 varsa → enYuksek2Kupur = { 200, 100 }
            var enYuksek2Kupur = kasetDizisi
                .Select(k => k.Kupur)
                .Distinct()
                .OrderByDescending(k => k)
                .Take(2)
                .ToHashSet();

            // Kasetler sıralanırken önce top 2 küpüre sahip kasetler gelir, kendi aralarında adete göre sıralanır
            // Top 2 dışındaki küpürler en sona eklenir, bu sayede örneğin 50 TL'de 200 adet olsa bile sıranın sonuna gider
            var siraliKasetler = kasetDizisi
                .OrderByDescending(k => enYuksek2Kupur.Contains(k.Kupur) ? 1 : 0)
                .ThenByDescending(k => k.Adet)
                .ThenByDescending(k => k.Kupur)
                .ToList();

            foreach (AtmKaset kaset in siraliKasetler)
            {
                if (kaset.Kupur > cekilecekTutar) continue;

                int maxAlinabilir = Math.Max(0, kaset.Adet - kaset.KritikDeger);
                int gereken = cekilecekTutar / kaset.Kupur;
                int alinacak = Math.Min(gereken, maxAlinabilir);

                if (alinacak == 0) continue;

                kaset.Adet -= alinacak;
                cekilecekTutar -= alinacak * kaset.Kupur;
                toplamVerilenBanknot += alinacak;
                sonKullanilanKaset = kaset;

                if (cekilecekTutar == 0) break;
            }

            //Yukarıda kritik değerden dolayı verilmeyen küpür mecbur kalınınca aşşağıdaki if döngüsü içinde verilecek.
            if (cekilecekTutar > 0)
            {
                foreach (AtmKaset kaset in siraliKasetler)
                {
                    if (kaset.Kupur > cekilecekTutar) continue;

                    int gereken = cekilecekTutar / kaset.Kupur;
                    int alinacak = Math.Min(gereken, kaset.Adet);

                    if (alinacak == 0) continue;

                    kaset.Adet -= alinacak;
                    cekilecekTutar -= alinacak * kaset.Kupur;
                    toplamVerilenBanknot += alinacak;
                    sonKullanilanKaset = kaset;

                    if (cekilecekTutar == 0) break;
                }
            }

            if (cekilecekTutar == 0 && sonKullanilanKaset != null)
            {
                AtmKaset? bozulacakKaset = sonKullanilanKaset;

                while (bozulacakKaset != null)
                {
                    int sonKupur = bozulacakKaset.Kupur;
                    bozulacakKaset.Adet += 1;
                    int bozulacak = sonKupur;

                    Dictionary<int, int> bozmaOncesiAdetler = kasetDizisi.ToDictionary(k => k.Id, k => k.Adet);
                    AtmKaset? buTurdakiSonKaset = null;

                    foreach (AtmKaset kaset in kasetDizisi
                        .OrderByDescending(k => k.Kupur)
                        .ThenByDescending(k => k.Adet))
                    {
                        if (kaset.Kupur < sonKupur && kaset.Kupur <= bozulacak && kaset.Adet > 0)
                        {
                            int kacKere = Math.Min(bozulacak / kaset.Kupur, kaset.Adet);
                            bozulacak -= kaset.Kupur * kacKere;
                            kaset.Adet -= kacKere;
                            buTurdakiSonKaset = kaset;
                        }
                        if (bozulacak == 0) break;
                    }

                    if (bozulacak != 0)
                    {
                        foreach (var kaset in kasetDizisi)
                            kaset.Adet = bozmaOncesiAdetler[kaset.Id];
                        bozulacakKaset.Adet -= 1;
                        break;
                    }

                    bozulacakKaset = buTurdakiSonKaset;
                }
            }

            if (cekilecekTutar == 0)
            {
                foreach (var kaset in kasetDizisi)
                    await _atmKasetRepository.AtmKasetGuncelleAsync(kaset);

                var kullanilanKasetler = kasetDizisi
                    .Where(k => k.Adet != orijinalAdetler[k.Id])
                    .Select(k => new AtmKaset
                    {
                        Id = k.Id,
                        AtmId = k.AtmId,
                        SlotNumarasi = k.SlotNumarasi,
                        Kupur = k.Kupur,
                        Adet = orijinalAdetler[k.Id] - k.Adet
                    })
                    .OrderByDescending(k => k.Kupur)
                    .ToList();

                atmdenParaCekmeResponse.IslemBasariliMi = true;
                atmdenParaCekmeResponse.Mesaj = "Para basariyla cekildi";
                atmdenParaCekmeResponse.Kasetler = kullanilanKasetler;
                return atmdenParaCekmeResponse;
            }

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

            //Yukarıda kritik değerden dolayı verilmeyen küpür mecbur kalınınca aşşağıdaki if döngüsü içinde verilecekAdet.
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
            if (cekilecekTutar == 0 && sonKullanilanKaset != null && toplamVerilenBanknot > bozmaEsigi)
            {
                int sonKupur = sonKullanilanKaset.Kupur;
                sonKullanilanKaset.Adet += 1;
                int bozulacak = sonKupur;

                Dictionary<int, int> bozmaOncesiAdetler = siraliKasetDizisi.ToDictionary(k => k.Id, k => k.Adet);

                foreach (AtmKaset kaset in siraliKasetDizisi.OrderBy(k => k.Kupur))
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
                .Select(k => new AtmKaset
                {
                    Id = k.Id,
                    AtmId = k.AtmId,
                    SlotNumarasi = k.SlotNumarasi,
                    Kupur = k.Kupur,
                    Adet = orijinalAdetler[k.Id] - k.Adet
                })
                .ToList();

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