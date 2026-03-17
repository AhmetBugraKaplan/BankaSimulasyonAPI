using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IKartRepository
    {
        public int KartEkle(int musteriId, string kartNumara, decimal kartGunlukLimit, string kartSifre);
        public int KartKalanLimitGuncelle(string kartNumara, decimal yeniKartLimit);
        public decimal KartKalanLimitGetir(string kartNumara);
        public List<Kart> TumKartlariGetir(int kullaniciId);
        public int KartSifreGuncelle(string yeniKartSifre, int kartId);
        public int KartSifreGetir(int kullaniciId, string kartNumara);

    }
}