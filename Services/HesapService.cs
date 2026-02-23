using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;


namespace BankaSimulasyon.Services
{
    public class HesapService : IHesapServis
    {
        private readonly IHesapRepository _hesapRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IAtmService _atmService;

        public HesapService(IHesapRepository hesapRepository, IAtmService atmService, IKullaniciRepository kullaniciRepository)
        {
            _hesapRepository = hesapRepository;
            _atmService = atmService;
            _kullaniciRepository = kullaniciRepository;
        }

        public bool hesapSifresiDogruMu(KullaniciHesap hesap, string sifre)
        {
            return hesap.Sifre == sifre;
        }

        public bool hesaptaYeterinceParaVarmi(KullaniciHesap hesap, int tutar)
        {
            return hesap.Bakiye >= tutar;
        }



        public async Task<KullaniciResponse> ParaCek(int hesapNumarasi, string girilenSifre, int atmId, int cekilecekTutar)
        {

            KullaniciResponse kullaniciResponse = new();

            var hesap = await _hesapRepository.kullanicininHessabiniBulAsync(hesapNumarasi);
            if (hesap == null)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Hesap bulunamadi";
                return kullaniciResponse;
            }

            if (!hesapSifresiDogruMu(hesap, girilenSifre))
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Sifre yanlis";
                return kullaniciResponse;
            }


            if (!hesaptaYeterinceParaVarmi(hesap, cekilecekTutar))
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Bakiye yetersiz";
                return kullaniciResponse;
            }

            var atmSonuc = await _atmService.AtmdenParaCekAsync(atmId, cekilecekTutar);
            if (!atmSonuc.IslemBasariliMi)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = atmSonuc.Mesaj;
                return kullaniciResponse;
            }

            hesap.Bakiye -= cekilecekTutar;
            await _hesapRepository.hesapGuncelleAsync(hesap);

            kullaniciResponse.IslemBasariliMi = true;
            kullaniciResponse.Mesaj = "Para basariyla cekildi";
            return kullaniciResponse;
        }
    }
}