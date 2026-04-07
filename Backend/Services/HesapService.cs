

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;
using Microsoft.IdentityModel.Tokens;


namespace BankaSimulasyon.Services
{
    public class HesapService : IHesapServis
    {
        private readonly IHesapRepository _hesapRepository;
        private readonly IMusteriRepository _kullaniciRepository;
        private readonly IAtmService _atmService;

        public HesapService(IHesapRepository hesapRepository, IAtmService atmService, IMusteriRepository kullaniciRepository)
        {
            _hesapRepository = hesapRepository;
            _atmService = atmService;
            _kullaniciRepository = kullaniciRepository;
        }

        public ApiResponse<List<Hesap>> MusterininTumHesaplariniGetir(string kartNumara)
        {
            ApiResponse<List<Hesap>> MusterininTumHesaplariniGetirApiResponse = new();
            List<Hesap> musteriHesapListesi = _hesapRepository.MusterininTumHesaplariniGetir(kartNumara);

            if (musteriHesapListesi.IsNullOrEmpty())
            {
                MusterininTumHesaplariniGetirApiResponse.Data = musteriHesapListesi;
                MusterininTumHesaplariniGetirApiResponse.IslemBasariliMi = false;
                MusterininTumHesaplariniGetirApiResponse.Mesaj = "Müşteriye ait hesap bulunamadı.";
                return MusterininTumHesaplariniGetirApiResponse;
            }
            else
            {
                MusterininTumHesaplariniGetirApiResponse.Data = musteriHesapListesi;
                MusterininTumHesaplariniGetirApiResponse.IslemBasariliMi = true;
                MusterininTumHesaplariniGetirApiResponse.Mesaj = "Müşteriye ait hesaplar başarıyla listelendi.";
                return MusterininTumHesaplariniGetirApiResponse;
            }
        }

        



    }
}

