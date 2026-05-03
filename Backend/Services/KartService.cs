using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Responses;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace BankaSimulasyon.Services
{
    public class KartService : IKartService
    {

        private readonly IKartRepository _kartRepository;
        private readonly IMusteriRepository _musteriRepository;
        private readonly IAtmService _atmService;
        private readonly IAuthService _authService;
        private readonly IHesapRepository _hesapRepository;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public KartService(IKartRepository kartRepository, IAtmService atmService, IAuthService authService,
         IMusteriRepository musteriRepository, IHesapRepository hesapRepository, AppDbContext context, IConfiguration configuration)
        {
            _kartRepository = kartRepository;
            _atmService = atmService;
            _authService = authService;
            _musteriRepository = musteriRepository;
            _hesapRepository = hesapRepository;
            _context = context;
            _configuration = configuration;
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


        //Kalan limit güncellemicez
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

            //Biz  kalanLimiti silip kullanılan limit değerini kullanmaya başlarsak bu seferde çekilecek tutardan önce
            //KartGulukLimit - kullanilan Limit  = Kalan limit hesaplamasını yapmamız geerekiypr bunun sebebi şu
            //Para çekmek istediğim zaman limit kontrolü yapmam gerekiyor e zaten kalan limiti silmemizin sebebi limitguncelleme işlemindkei
            //hesaplama maliyetini düşürmekti bu seferde burda bir hesaplama işlemi yapılıyor manası kalmıyor 


            //Transaction bloğumuz burada başlayacak.
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                decimal kartKalanLimit = _kartRepository.KartKalanLimitGetir(kartNumara);

                if (cekilecekTutar <= kartKalanLimit)
                {
                    AtmdenParaCekmeResponse AtmParaCekmeDonenDeger = _atmService.AtmdenParaCek(atmId, cekilecekTutar, kartNumara);

                    if (AtmParaCekmeDonenDeger.IslemBasariliMi)
                    {
                        decimal yeniKalanLimit = kartKalanLimit - cekilecekTutar;
                        _kartRepository.KartKalanLimitGuncelle(kartNumara, yeniKalanLimit);

                        //Geçmiş İşlemler Tablosuna Kaydediyoruz.
                        _hesapRepository.IslemGecmisiEkleTekTarafli("", "Para Çekme", "Cikis", cekilecekTutar, yeniKalanLimit, atmId, "ATM'den PARA ÇEKME İŞLEMİ");

                        transaction.Commit();

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
            }
            catch (Exception)
            {
                transaction.Rollback();
                ParaCekApiResponse.IslemBasariliMi = false;
                ParaCekApiResponse.Mesaj = "Beklenmeyen bir hata oluştu.";
            }

            return ParaCekApiResponse;
        }


        public ApiResponse<object> KartDogrula(string kartNumara, string kartSifre, int atmId, string ipAdresi)
        {
            ApiResponse<object> kartDogrulaApiResponse = new();


            //Şifre kontrolü yapıyoruz girilen numaraya ait şifre yoksa demekki kartta yok :D
            var sifreHash = _kartRepository.KartSifreGetir(kartNumara);

            if (sifreHash == null)
            {
                kartDogrulaApiResponse.Mesaj = "Girilen numaraya ait kart bulunamadı";
                kartDogrulaApiResponse.IslemBasariliMi = false;
                return kartDogrulaApiResponse;
            }

            //Bloke kontrolü yapıyoruz.
            int mevcutYanlisGirisSayisi = _kartRepository.YanlisGirisSayisiGetir(kartNumara);
            if (mevcutYanlisGirisSayisi >= 3)
            {
                kartDogrulaApiResponse.Mesaj = "Kart bloke edilmiştir";
                kartDogrulaApiResponse.IslemBasariliMi = false;
                kartDogrulaApiResponse.Data = mevcutYanlisGirisSayisi;
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
                kartDogrulaApiResponse.Data = new KartDogrulaResponce
                {
                    Token = token!,
                    KartNumara = kartNumara,
                    AtmId = atmId
                };
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

        public int KartNumaraIleMusteriIdGetir(string kartNumara)
        {
            return _kartRepository.KartNumaraIleMusteriIdGetir(kartNumara);
        }

        public ApiResponse<bool> CikisYap(string token)
        {
            ApiResponse<bool> response = new();

            double dakika = Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]);
            DateTime expireDate = DateTime.Now.AddMinutes(dakika);

            _kartRepository.CikisYap(token, expireDate);

            response.IslemBasariliMi = true;
            response.Mesaj = "Çıkış başarıyla yapıldı";
            return response;
        }


    }
}


