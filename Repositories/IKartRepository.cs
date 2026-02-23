using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Repositories
{
    public interface IKartRepository
    {
        public Task<int> KartEkle(int kullaniciHesapId, string KartNumara, string KartSKT, string CVV,string KartTipi,bool AktifMi);
    }
}