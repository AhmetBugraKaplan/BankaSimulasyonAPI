using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;

namespace BankaSimulasyon.Services
{
    public class OnayService : IOnayService
    {

        private readonly IOnayRepository _onayRepository;
        public OnayService(IOnayRepository onayRepository)
        {
            _onayRepository = onayRepository;
        }

        public ApiResponse<object> OnayKoduDogruMu(string kod, string telefonNumara)
        {
            var sonuc = _onayRepository.OnayKoduDogruMu(kod, telefonNumara);

            if (sonuc == 1)
            {
                return new ApiResponse<object> { IslemBasariliMi = true, Mesaj = "Kod doğrulandı.", Data= "Onay Kodu VAR" };

            }
            return new ApiResponse<object> { IslemBasariliMi = false, Mesaj = "Kod hatalı veya süresi dolmuş." ,Data = "Onay Kodu YOK"};
        }


    }
}