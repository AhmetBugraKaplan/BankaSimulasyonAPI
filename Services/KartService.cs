using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;

namespace BankaSimulasyon.Services
{
    public class KartService :  IKartService
    {

        private readonly IKartRepository _kartRepository;

        public KartService(IKartRepository kartRepository)
        {
            _kartRepository = kartRepository;
        }


        public async Task<KullaniciResponse> KartEkle(int kullaniciHesapId, string KartNumara, string KartSKT, string CVV,string KartTipi,bool AktifMi)
        {
            KullaniciResponse kullaniciResponse = new();
          
          int sonuc = await _kartRepository.KartEkle(kullaniciHesapId,KartNumara,KartSKT,CVV,KartTipi,AktifMi);

            if(sonuc > 0)
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
    }
}