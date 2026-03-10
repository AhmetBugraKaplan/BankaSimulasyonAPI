using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IKartRepository
    {
        public int KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi);
        public int KartLimitGuncelle( string kartNumara, decimal yeniKartLimit);
        public decimal KartLimitGetir(string kartNumara);
        public List<Kart> TumKartlariGetir(int kullaniciId);

    }
}