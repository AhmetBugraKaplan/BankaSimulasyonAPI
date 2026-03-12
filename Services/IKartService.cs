using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IKartService
    {
        public KullaniciResponse KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi,string KartSifre);

        public KullaniciResponse KartLimitGuncelle(int kullaniciId, string kartNumara, decimal yeniKartLimit);

        public decimal KalanKullanilabilirHesapLimit(int kullaniciId, string kartNumara);

        public KullaniciResponse KartSifreGuncelle(int YeniKartSifre,int kartId);


    }
}