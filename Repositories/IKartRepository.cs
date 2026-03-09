using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Repositories
{
    public interface IKartRepository
    {
        public int KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi);
        public int KullaniciKartLimitGuncelle(int kullaniciId, decimal kullaniciKartLimit);
    }
}