using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BankaSimulasyon.Services
{
    public class KartService : IKartService
    {

        private readonly IKartRepository _kartRepository;
        private readonly IMusteriRepository _musteriRepository;
        private readonly IAtmService _atmService;
        private readonly IAuthService _authService;

        public KartService(IKartRepository kartRepository, IAtmService atmService, IAuthService authService, IMusteriRepository musteriRepository)
        {
            _kartRepository = kartRepository;
            _atmService = atmService;
            _authService = authService;
            _musteriRepository = musteriRepository;
        }

        //Bu fonksiyonda 2 aşamalı bir kontrolden geçiyoruz öncelikle aynı numarada kart var mı
        //sonrasında istenen limit kalanMüşteri limitinden az mı 
        public ApiResponse<object> KartEkle(int musteriId, string kartNumara, decimal kartGunlukLimit, string kartSifre)
        {
            ApiResponse<object> kullaniciResponse = new();

            var kontrolResponse = AyniNumaradaKartVarMi(kartNumara);

            if (kontrolResponse.IslemBasariliMi == false)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = kontrolResponse.Mesaj; // "Aynı numarada kart sistemde mevcut..."
                return kullaniciResponse;
            }

            decimal musteriLimit = _musteriRepository.MusteriLimitGetirIdGore(musteriId);
            decimal musteriKullanilanToplamLimit = _musteriRepository.MusteriKullanilanLimitGetirIdGore(musteriId);
            decimal musteriKalanKullanilabilirLimit = musteriLimit - musteriKullanilanToplamLimit;

            if (kartGunlukLimit > musteriKalanKullanilabilirLimit)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = $"Yetersiz müşteri limiti! Kalan kullanılabilir limit: {musteriKalanKullanilabilirLimit:C}";
                return kullaniciResponse;
            }

            string hashlenmisSifre = BCrypt.Net.BCrypt.HashPassword(kartSifre);

            int sonuc = _kartRepository.KartEkle(musteriId, kartNumara, kartGunlukLimit, hashlenmisSifre);


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

            return kullaniciResponse;
        }


        /*
        public ApiResponse<object> KartKalanLimitGuncelle(int kullaniciId, string kartNumara, decimal yeniKartLimit)
        {
            ApiResponse<object> kullaniciResponse = new();

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
        }
         */

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

        //Gunluk limitini güncellerken müşteri limitini geçmemesi gerekiyor.
        public ApiResponse<object> KartGunlukLimitGuncelle(string kartNumara, decimal yeniKartLimit, int musteriId)
        {
            ApiResponse<object> kartGunlukLimitApiResponse = new();

            decimal musteriLimit = _musteriRepository.MusteriLimitGetirIdGore(musteriId);
            decimal musteriKullanilanToplamLimit = _musteriRepository.MusteriKullanilanLimitGetirIdGore(musteriId);
            decimal kartMevcutLimit = _kartRepository.KartGunlukLimitGetir(kartNumara);

            decimal musteriKalanKullanilabilirLimit = musteriLimit - musteriKullanilanToplamLimit + kartMevcutLimit;

            // Limiti yükseltiyoruz → müşteri limiti kontrolü gerekli
            if (kartMevcutLimit < yeniKartLimit)
            {
                if (yeniKartLimit <= musteriKalanKullanilabilirLimit)
                {
                    _kartRepository.KartGunlukLimitGuncelle(kartNumara, yeniKartLimit);
                    _kartRepository.KartKalanLimitGuncelle(kartNumara, yeniKartLimit);
                    kartGunlukLimitApiResponse.IslemBasariliMi = true;
                    kartGunlukLimitApiResponse.Mesaj = "Limit güncellendi";
                }
                else
                {
                    kartGunlukLimitApiResponse.IslemBasariliMi = false;
                    kartGunlukLimitApiResponse.Mesaj = "Yetersiz Müşteri Limiti!";
                }
            }
            // Limiti düşürüyoruz → müşteri limiti kontrolüne gerek yok
            else if (kartMevcutLimit > yeniKartLimit)
            {
                _kartRepository.KartGunlukLimitGuncelle(kartNumara, yeniKartLimit);
                _kartRepository.KartKalanLimitGuncelle(kartNumara, yeniKartLimit);
                kartGunlukLimitApiResponse.IslemBasariliMi = true;
                kartGunlukLimitApiResponse.Mesaj = "Limit güncellendi";
            }
            // Aynı limit
            else
            {
                kartGunlukLimitApiResponse.IslemBasariliMi = false;
                kartGunlukLimitApiResponse.Mesaj = "Yeni limit mevcut limitten farklı olmalıdır!";
            }

            return kartGunlukLimitApiResponse;
        }

        public void TumKartLimitleriniSifirla()
        {
            _kartRepository.TumKartlarinLimitleriniSifirla();
        }


        public ApiResponse<object> KartSifreGuncelle(string yeniKartSifre, string kartNumara)
        {
            ApiResponse<object> kullaniciResponse = new();

            string hashlenmisSifre = BCrypt.Net.BCrypt.HashPassword(yeniKartSifre.ToString());

            int sonuc = _kartRepository.KartSifreGuncelle(hashlenmisSifre, kartNumara);

            if (sonuc > 0)
            {
                _kartRepository.YanlisGirisSayisiSifirla(kartNumara);
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


        //Kalan limit güncellemicez, kullanılan limit güncellicez
        public ApiResponse<List<AtmKaset>> ParaCek(string kartNumara, int atmId, int cekilecekTutar)
        {
            ApiResponse<List<AtmKaset>> ParaCekApiResponse = new();

            if (cekilecekTutar <= 0)
            {
                ParaCekApiResponse.IslemBasariliMi = false;
                ParaCekApiResponse.Mesaj = "Çekilecek tutar 0'dan büyük olmalıdır.";
                return ParaCekApiResponse;
            }
            if (cekilecekTutar % 10 != 0)
            {
                ParaCekApiResponse.IslemBasariliMi = false;
                ParaCekApiResponse.Mesaj = "Çekilecek tutar 10'un katı olmalıdır.";
                return ParaCekApiResponse;
            }

            decimal kartKalanLimit = _kartRepository.KartKalanLimitGetir(kartNumara);

            if (cekilecekTutar <= kartKalanLimit)
            {
                AtmdenParaCekmeResponse AtmParaCekmeDonenDeger = _atmService.AtmdenParaCek(atmId, cekilecekTutar, kartNumara);

                if (AtmParaCekmeDonenDeger.IslemBasariliMi)
                {
                    decimal yeniKalanLimit = kartKalanLimit - cekilecekTutar;
                    _kartRepository.KartKalanLimitGuncelle(kartNumara, yeniKalanLimit);

                    ParaCekApiResponse.Data = AtmParaCekmeDonenDeger.Kasetler;
                    ParaCekApiResponse.IslemBasariliMi = true;
                    ParaCekApiResponse.Mesaj = "Para çekme işlemi başarıyla gerçekleştirildi.";
                }
                else
                {
                    ParaCekApiResponse.IslemBasariliMi = false;
                    ParaCekApiResponse.Mesaj = AtmParaCekmeDonenDeger.Mesaj;
                }
            }
            else
            {
                ParaCekApiResponse.IslemBasariliMi = false;
                ParaCekApiResponse.Mesaj = "Kartınızın limiti yetersiz.";
            }

            return ParaCekApiResponse;
        }


        public ApiResponse<object> KartDogrula(string kartNumara, string kartSifre, int atmId, string ipAdresi)
        {
            ApiResponse<object> kartDogrulaApiResponse = new();

            var sifreHash = _kartRepository.KartSifreGetir(kartNumara);

            if (sifreHash == null)
            {
                kartDogrulaApiResponse.Mesaj = "Girilen numaraya ait kart bulunamadı";
                kartDogrulaApiResponse.IslemBasariliMi = false;
                return kartDogrulaApiResponse;
            }

            //Aslında yukarıda aldığımız sifreHash şifreli şifre :D ama aşşağıdaki 
            // BCrypt fonksiyonu onu açıyor ve bizim girdiğimiz şifreye eşit olup olmadığına bakıyor.

            bool sifreDogruMu = BCrypt.Net.BCrypt.Verify(kartSifre, sifreHash);

            if (sifreDogruMu)
            {
                //Doğru şekilde giriş yaparsa gün limit kontrolü yapacağız.

                DateOnly sonIslemTarihi = _kartRepository.SonIslemTarihiGetir(kartNumara);
                DateOnly bugun = DateOnly.FromDateTime(DateTime.Today);

                if (sonIslemTarihi != bugun)
                {
                    _kartRepository.SonIslemTarihiniBugunYap(kartNumara);
                    _kartRepository.KartKalanLimitSifirla(kartNumara);
                }

                _kartRepository.YanlisGirisSayisiSifirla(kartNumara);
                string? token = _authService.TokenUret(kartNumara, atmId, ipAdresi);
                kartDogrulaApiResponse.Mesaj = "Giriş başarıyla yapıldı";
                kartDogrulaApiResponse.IslemBasariliMi = true;
                kartDogrulaApiResponse.Data = token;
            }
            else
            {
                _kartRepository.YanlisGirisSayisiniArttir(kartNumara);
                int yanlisGirisSayisi = _kartRepository.YanlisGirisSayisiGetir(kartNumara);
                kartDogrulaApiResponse.Mesaj = "Yanlış Şifre";
                kartDogrulaApiResponse.IslemBasariliMi = false;
                kartDogrulaApiResponse.Data = yanlisGirisSayisi;
            }

            return kartDogrulaApiResponse;
        }


        public ApiResponse<int> YanlisGirisSayisiGetir(string kartNumara)
        {
            ApiResponse<int> yanlisGirisSayisiApiResponse = new();

            int kartYanlisGirisSayisi = _kartRepository.YanlisGirisSayisiGetir(kartNumara);

            yanlisGirisSayisiApiResponse.Data = kartYanlisGirisSayisi;

            return yanlisGirisSayisiApiResponse;
        }

        public ApiResponse<decimal> KartKalanLimitGetir(string kartNumara)
        {
            ApiResponse<decimal> kartKalanLimitGetirApiResponse = new();

            decimal kalanLimit = _kartRepository.KartKalanLimitGetir(kartNumara);

            kartKalanLimitGetirApiResponse.IslemBasariliMi = true;
            kartKalanLimitGetirApiResponse.Mesaj = "Limit Getirildi";
            kartKalanLimitGetirApiResponse.Data = kalanLimit;

            return kartKalanLimitGetirApiResponse;
        }

        public ApiResponse<bool> AyniNumaradaKartVarMi(string kartNumara)
        {
            ApiResponse<bool> ayniNumaradaKartVarMiApiResponse = new();

            bool sonuc = _kartRepository.AyniNumaradaKartVarMi(kartNumara);

            if (sonuc == false)
            {
                ayniNumaradaKartVarMiApiResponse.IslemBasariliMi = true;
                ayniNumaradaKartVarMiApiResponse.Mesaj = "Ayni Numarada Kart Yok Sonraki İşleme Geçiliyor";
                ayniNumaradaKartVarMiApiResponse.Data = sonuc;
            }
            else
            {
                ayniNumaradaKartVarMiApiResponse.IslemBasariliMi = false;
                ayniNumaradaKartVarMiApiResponse.Mesaj = "Ayni numarada kart var böyle bir kart oluşturulamaz";
                ayniNumaradaKartVarMiApiResponse.Data = sonuc;
            }

            return ayniNumaradaKartVarMiApiResponse;
        }

    }
}