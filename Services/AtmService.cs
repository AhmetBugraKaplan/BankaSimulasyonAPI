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
        private readonly IKartRepository _kartRepository;


        public AtmService(IAtmKasetRepository atmKasetRepository, IAtmRepository atmRepository, IKartRepository kartRepository)
        {
            _atmKasetRepository = atmKasetRepository;
            _atmRepository = atmRepository;
            _kartRepository = kartRepository;
        }



        public AtmdenParaCekmeResponse AtmdenParaCek(int atmId, int cekilecekTutar, string kartNumara)
        {
            AtmdenParaCekmeResponse atmdenParaCekmeResponse = new();
            List<AtmKaset> kasetDizisi = _atmKasetRepository.AtmdekiKasetleriGetir(atmId);
            int atmdeBulunanToplamPara = AtmdekiToplamParayiHesapla(kasetDizisi);
            int orijinalCekilecekTutar = cekilecekTutar;

            // var kalanKullanilabilirHesapLimiti = _kartService.KalanKullanilabilirHesapLimit;

            //var kalanKullanilabilirHesapLimiti = 100;
            //decimal KullanilanKartinLimiti = _kartRepository.KartKalanLimitGetir(kartNumara);


                if (cekilecekTutar <= 0)
                {
                    atmdenParaCekmeResponse.IslemBasariliMi = false;
                    atmdenParaCekmeResponse.Mesaj = "Cekilmek istenen tutar 0'dan buyuk olmalidir";
                    atmdenParaCekmeResponse.Kasetler = null!;
                    return atmdenParaCekmeResponse;
                }

                else if (cekilecekTutar % 10 != 0)
                {
                    atmdenParaCekmeResponse.IslemBasariliMi = false;
                    atmdenParaCekmeResponse.Mesaj = "Cekilmek istenen para 10TL'nin katlari olmalidir";
                    atmdenParaCekmeResponse.Kasetler = null!;
                    return atmdenParaCekmeResponse;
                }

                else if (cekilecekTutar > atmdeBulunanToplamPara)
                {
                    atmdenParaCekmeResponse.IslemBasariliMi = false;
                    atmdenParaCekmeResponse.Mesaj = "ATM'de yeterli para bulunmuyor";
                    atmdenParaCekmeResponse.Kasetler = null!;
                    return atmdenParaCekmeResponse;
                }

                Dictionary<int, int> orijinalAdetler = kasetDizisi.ToDictionary(k => k.Id, k => k.Adet);

                int toplamVerilenBanknot = 0;
                AtmKaset sonKullanilanKaset = null!;

                // ATM deki küpürler arasından en yüksek 2 küpür değeri belirliyoruz
                // 200, 200, 100, 50 için = { 200, 100 }
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
                    .ThenByDescending(k => k.Kupur)
                    .ThenByDescending(k => k.Adet)
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
                        _atmKasetRepository.AtmKasetGuncelle(kaset);

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
          
        




        public int AtmdekiToplamParayiHesapla(List<AtmKaset> kasetDizisi)
        {
            return kasetDizisi.Sum(k => k.Kupur * k.Adet);
        }



        public ApiResponse<int> AtmdekiToplamParayiIdIleGetir(int atmId)
        {
            ApiResponse<int> toplamParayiGetirApiResponse = new ApiResponse<int>();
            int AtmKasetlerToplamPara = 0;

            var kasetDizisi = _atmKasetRepository.AtmdekiKasetleriGetir(atmId);

            if (kasetDizisi.Any())
            {
                foreach (AtmKaset kaset in kasetDizisi)
                {
                    toplamParayiGetirApiResponse.IslemBasariliMi = true;
                    toplamParayiGetirApiResponse.Mesaj = "Toplam para getirildi";
                    toplamParayiGetirApiResponse.Data = AtmKasetlerToplamPara += (kaset.Kupur * kaset.Adet);
                }
            }
            else
            {
                toplamParayiGetirApiResponse.IslemBasariliMi = true;
                toplamParayiGetirApiResponse.Mesaj = "ATM de para yok";
                toplamParayiGetirApiResponse.Data = 0;

            }

            return toplamParayiGetirApiResponse;
        }


        public ApiResponse<object> AtmKasetlerdekiKupurleriGuncelle(int atmId, int slotNumarasi, int adet, int kupur)
        {
            ApiResponse<object> kasetGuncellemeApiResponse = new ApiResponse<object>();
            var kasetDizisi = _atmKasetRepository.AtmdekiKasetleriGetir(atmId);


            if (kasetDizisi.Any())
            {
                var hedefKaset = kasetDizisi.FirstOrDefault(k => k.SlotNumarasi == slotNumarasi);

                if (hedefKaset != null)
                {
                    hedefKaset.Adet = adet;
                    hedefKaset.Kupur = kupur;
                    kasetGuncellemeApiResponse.IslemBasariliMi = true;
                    _atmKasetRepository.AtmKasetGuncelle(hedefKaset);

                }
                else
                {
                    kasetGuncellemeApiResponse.IslemBasariliMi = false;
                    kasetGuncellemeApiResponse.Mesaj = "Girilen slot numarasına ait slot bulunamadı";

                }
            }
            else
            {
                kasetGuncellemeApiResponse.IslemBasariliMi = false;
                kasetGuncellemeApiResponse.Mesaj = "Girilen atmId'e ait atm bulunamadı";

            }



            return kasetGuncellemeApiResponse;
        }


        public ApiResponse<object> AtmEkle(string konum, bool aktifMi)
        {
            ApiResponse<object> atmEklemeApiResponse = new();

            int sonuc = _atmRepository.AtmEkle(konum, aktifMi);

            if (sonuc > 0)
            {
                atmEklemeApiResponse.IslemBasariliMi = true;
                atmEklemeApiResponse.Mesaj = "ATM başarıyla eklendi.";

            }
            else
            {
                atmEklemeApiResponse.IslemBasariliMi = false;
                atmEklemeApiResponse.Mesaj = "ATM eklenemedi";
            }

            return atmEklemeApiResponse;
        }


        public ApiResponse<List<ATM>> TumAtmleriGetir()
        {
            ApiResponse<List<ATM>> tumAtmleriGetirApiResponse = new ApiResponse<List<ATM>>();
            
            var gelenAtmListesi = tumAtmleriGetirApiResponse.Data = _atmRepository.TumAtmleriGetir();
            if(gelenAtmListesi != null)
            {
                tumAtmleriGetirApiResponse.IslemBasariliMi = true;
                tumAtmleriGetirApiResponse.Mesaj = "Tüm ATM ler getirildi.";
            }
            else
            {
                tumAtmleriGetirApiResponse.IslemBasariliMi = false;
                tumAtmleriGetirApiResponse.Mesaj = "ATM listesi boş";
            }
            return tumAtmleriGetirApiResponse;
        }

    }
}