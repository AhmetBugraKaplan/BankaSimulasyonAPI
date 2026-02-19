using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IAtmKasetRepository
    {
        Task<List<AtmKaset>> AtmdekiKasetleriGetirAsync (int atmId);
        Task AtmKasetGuncelleAsync (AtmKaset atmKaset);
    }
}