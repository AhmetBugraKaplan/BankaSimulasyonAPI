using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IAtmKasetRepository
    {
        List<AtmKaset> AtmdekiKasetleriGetir (int atmId);
        void AtmKasetGuncelle (AtmKaset atmKaset);
    }
}