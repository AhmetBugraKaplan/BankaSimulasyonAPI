using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IHesapRepository
    {
        public Task<KullaniciHesap?> kullanicininHessabiniBulAsync(int hesapNumarasi);

        public Task hesapGuncelleAsync(KullaniciHesap kullaniciHesap);

    }
}