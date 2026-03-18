using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IKartService
    {
        public ApiResponse<object> KartEkle(int kullaniciId, string KartNumara, decimal kartGunlukLimit, string kartSifre);
        public ApiResponse<object> KartGunlukLimitGuncelle(string kartNumara, decimal yeniKartLimit);
        
        //public ApiResponse<object> KartKalanLimitGuncelle(int kullaniciId, string kartNumara, decimal yeniKartLimit);

        //public decimal KalanKullanilabilirHesapLimit(int kullaniciId, string kartNumara);
        public ApiResponse<object> KartSifreGuncelle(string yeniKartSifre, string kartNumara);
        public ApiResponse<List<AtmKaset>> ParaCek(string kartNumara, int atmId, int cekilecekTutar);

        public ApiResponse<object> KartDogrula(string kartNumara, string kartSifre, int atmId);

        public ApiResponse<int> YanlisGirisSayisiGetir(string kartNumara);



    }
}