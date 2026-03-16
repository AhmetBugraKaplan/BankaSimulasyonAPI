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

        public KartService(IKartRepository kartRepository)
        {
            _kartRepository = kartRepository;
        }


        public ApiResponse KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi, string KartSifre)
        {
            ApiResponse kullaniciResponse = new();

            int sonuc = _kartRepository.KartEkle(kullaniciId, KartNumara, KartSKT, CVV, KartTipi, AktifMi, KartSifre);

            if (sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Kart Eklendi.";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = true;
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




        public ApiResponse KartSifreGuncelle(string YeniKartSifre, int kartId)
        {
            ApiResponse kullaniciResponse = new();

            string hashlenmisSifre = BCrypt.Net.BCrypt.HashPassword(YeniKartSifre.ToString());

            int sonuc = _kartRepository.KartSifreGuncelle(hashlenmisSifre, kartId);

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




    }
}