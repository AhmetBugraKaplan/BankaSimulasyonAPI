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
        private readonly IHesapRepository _hesapRepository;

        public KartService(IKartRepository kartRepository, IHesapRepository hesapRepository)
        {
            _kartRepository = kartRepository;
            _hesapRepository = hesapRepository;
        }


        public KullaniciResponse KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi)
        {
            KullaniciResponse kullaniciResponse = new();

            int sonuc = _kartRepository.KartEkle(kullaniciId, KartNumara, KartSKT, CVV, KartTipi, AktifMi);

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


        public KullaniciResponse KartLimitGuncelle(int kullaniciId, string kartNumara, decimal yeniKartLimit)
        {
            KullaniciResponse kullaniciResponse = new();
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


            if (yeniKartLimit <= kalanKullanilabilirLimit)
            {
                if (yeniKartLimit != guncellemeOncesiKartLimit)
                {
                    _kartRepository.KartLimitGuncelle(kullaniciId, kartNumara,yeniKartLimit);
                    kullaniciResponse.IslemBasariliMi = true;
                    kullaniciResponse.Mesaj = "Kart limitiniz başarıyla güncellendi";
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

            return kullaniciResponse;
        }







    }
}