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
        public KullaniciResponse ParaCek(int hesapNumarasi,int atmId, int cekilecekTutar);
        public KullaniciResponse HesapLimitGuncelle(int kullaniciId, decimal kullaniciHesapLimit);

    }
}