using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public KullaniciResponse KullaniciKartLimitGuncelle(int kullaniciId, decimal kullaniciKartLimit)
        {
            KullaniciResponse kullaniciResponse = new();
            decimal MusteriHesapLimit = _hesapRepository.kullaniciHesapLimitGetir(kullaniciId);  

            int sonuc = _kartRepository.KullaniciKartLimitGuncelle(kullaniciId, kullaniciKartLimit);

            if (sonuc > 0 && kullaniciKartLimit <= MusteriHesapLimit)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Kart limitiniz başarıyla güncellendi";
            }
            else if(sonuc > 0 && kullaniciKartLimit > MusteriHesapLimit)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kartınızın limiti müşteri hesap limitinizden fazla olamaz";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Girilen kullanıcı id 'e ait kullanıcı bulunamadı";
            }

            return kullaniciResponse;

        }


    }
}