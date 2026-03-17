using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;

namespace BankaSimulasyon.Services
{
    public class KartService : IKartService
    {

        private readonly IKartRepository _kartRepository;
        private readonly IAtmService _atmService;

        public KartService(IKartRepository kartRepository, IAtmService atmService)
        {
            _kartRepository = kartRepository;
            _atmService = atmService;
        }


        public ApiResponse<object> KartEkle(int kullaniciId, string kartNumara, decimal kartGunlukLimit, string kartSifre)
        {
            ApiResponse<object> kullaniciResponse = new();

            string hashlenmisSifre = BCrypt.Net.BCrypt.HashPassword(kartSifre);

            int sonuc = _kartRepository.KartEkle(kullaniciId, kartNumara, kartGunlukLimit, hashlenmisSifre);

            if (sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Kart Eklendi.";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kart eklenirken bir hata gerçekleşti.";
            }

            return kullaniciResponse!;
        }


        /*
        public ApiResponse KartLimitGuncelle(int kullaniciId, string kartNumara, decimal yeniKartLimit)
        {
            ApiResponse kullaniciResponse = new();

            try
            {
                decimal HesapLimit = _hesapRepository.HesapLimitGetir(kullaniciId);
                decimal guncellemeOncesiKartLimit = _kartRepository.KartLimitGetir(kartNumara);
                decimal kalanKullanilabilirLimit;
                

                kalanKullanilabilirLimit = KalanKullanilabilirHesapLimit(kullaniciId, kartNumara);

                if (yeniKartLimit <= kalanKullanilabilirLimit)
                {
                    if (yeniKartLimit != guncellemeOncesiKartLimit)
                    {
                        int sonuc = _kartRepository.KartLimitGuncelle(kartNumara, yeniKartLimit,kullaniciId);
                        if (sonuc > 0)
                        {
                            kullaniciResponse.IslemBasariliMi = true;
                            kullaniciResponse.Mesaj = "Kart limitiniz başarıyla güncellendi";
                        }
                        else
                        {
                            kullaniciResponse.IslemBasariliMi = false;
                            kullaniciResponse.Mesaj = "Girdiğiniz kart numarasına ait kart bulunamadı";
                        }
                        
                    }
                    else
                    {
                        kullaniciResponse.IslemBasariliMi = false;
                        kullaniciResponse.Mesaj = "Güncellenecek limit aktif limitten farklı olmalıdır";
                    }

                }
                else if (yeniKartLimit > HesapLimit)
                {
                    kullaniciResponse.IslemBasariliMi = false;
                    kullaniciResponse.Mesaj = "Kartınızın limiti müşteri hesap limitinizden fazla olamaz";
                }
                else
                {
                    kullaniciResponse.IslemBasariliMi = false;
                    kullaniciResponse.Mesaj = "Diğer kartlarınız hesap limitinizi kullandığından bu kart için yeterli limit bulunmamaktadır";
                }
            }
            catch (Exception ex)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = ex.Message; 
                return kullaniciResponse;
            }



            return kullaniciResponse;
        } */


        /*
        public decimal KalanKullanilabilirHesapLimit(int kullaniciId, string kartNumara)
        {
            decimal HesapLimit = _hesapRepository.HesapLimitGetir(kullaniciId);
            decimal guncellemeOncesiKartLimit = _kartRepository.KartLimitGetir(kartNumara);
            decimal ToplamKullanilanKartLimiti = 0;
            decimal kalanKullanilabilirLimit;


            List<Kart> kartlar = _kartRepository.TumKartlariGetir(kullaniciId);

            foreach (var kart in kartlar)
            {
                ToplamKullanilanKartLimiti += kart.KartLimit;
            }

            //Hesabın total limitinden kartlarda aktif olarak kullanılan limiti çıkarıyoruz aşşağıda 
            //limit karşılaştırmayı kalanKullanilabilirLimit üzerinden yapıcaz
            kalanKullanilabilirLimit = HesapLimit - ToplamKullanilanKartLimiti + guncellemeOncesiKartLimit;
            Console.WriteLine($"LOG:{kalanKullanilabilirLimit}");
            return kalanKullanilabilirLimit;
        } 
        */




        public ApiResponse<object> KartSifreGuncelle(string YeniKartSifre, string kartNumara)
        {
            ApiResponse<object> kullaniciResponse = new();

            string hashlenmisSifre = BCrypt.Net.BCrypt.HashPassword(YeniKartSifre.ToString());

            int sonuc = _kartRepository.KartSifreGuncelle(hashlenmisSifre, kartNumara);

            if (sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Şifre başarıyla güncellendi";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Şifre güncellenirken hata oluştu";
            }

            return kullaniciResponse;
        }


        public ApiResponse<List<AtmKaset>> ParaCek(string kartNumara, int atmId, int cekilecekTutar, int kullaniciId)
        {
            ApiResponse<List<AtmKaset>> ParaCekApiResponse = new();

            decimal kartKalanLimit = _kartRepository.KartKalanLimitGetir(kartNumara);

            if (cekilecekTutar <= kartKalanLimit)
            {
                decimal yeniKalanLimit = (kartKalanLimit - cekilecekTutar);

                _kartRepository.KartKalanLimitGuncelle(kartNumara, yeniKalanLimit);
                AtmdenParaCekmeResponse AtmParaCekmeDonenDeger = _atmService.AtmdenParaCek(atmId, cekilecekTutar, kartNumara, kullaniciId);

                List<AtmKaset> DonenListe = AtmParaCekmeDonenDeger.Kasetler;

                ParaCekApiResponse.Data = DonenListe;
                ParaCekApiResponse.IslemBasariliMi = true;
                ParaCekApiResponse.Mesaj = "Para çekme işlemi başarıyla gerçekleştirildi.";
            }
            else
            {
                ParaCekApiResponse.IslemBasariliMi = false;
                ParaCekApiResponse.Mesaj = "Kartınızın limiti yetersiz.";
            }

            return ParaCekApiResponse;
        }




        public ApiResponse<object> KartDogrula(string kartNumara, string kartSifre)
        {
            ApiResponse<object> kartDogrulaApiResponse = new();

            var sifreHash = _kartRepository.KartSifreGetir(kartNumara);

            if (sifreHash == null)
            {
                kartDogrulaApiResponse.Mesaj = "Girilen numaraya ait kart bulunamadı";
                kartDogrulaApiResponse.IslemBasariliMi = false;
                return kartDogrulaApiResponse;
            }

            bool sifreDogruMu = BCrypt.Net.BCrypt.Verify(kartSifre, sifreHash);

            if (sifreDogruMu)
            {
                kartDogrulaApiResponse.Mesaj = "Giriş başarıyla yapıldı";
                kartDogrulaApiResponse.IslemBasariliMi = true;
            }
            else
            {
                kartDogrulaApiResponse.Mesaj = "Yanlış Şifre";
                kartDogrulaApiResponse.IslemBasariliMi = false;
            }

            return kartDogrulaApiResponse;
        }


    }
}