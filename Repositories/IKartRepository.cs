using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IKartRepository
    {
        public int KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi,string KartSifre);
        public int KartLimitGuncelle( string kartNumara, decimal yeniKartLimit,int kullaniciId);
        public decimal KartLimitGetir(string kartNumara);
        public List<Kart> TumKartlariGetir(int kullaniciId);
        public int KartSifreGuncelle(int YeniKartSifre,int kartId); 

    }
}