using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IHesapServis
    {
        public bool hesaptaYeterinceParaVarmi(KullaniciHesap hesap, int tutar);
        public bool hesapSifresiDogruMu(KullaniciHesap hesap, string sifre);
        public Task<KullaniciResponse> ParaCek(int hesapNumarasi, string girilenSifre, int atmId, int cekilecekTutar);

    }
}