using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IHesapRepository
    {
        public KullaniciHesap? kullanicininHessabiniBul(int hesapNumarasi);

        public  void hesapGuncelleAsync(KullaniciHesap kullaniciHesap);

    }
}