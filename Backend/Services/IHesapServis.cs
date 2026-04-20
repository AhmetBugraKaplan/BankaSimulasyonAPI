
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IHesapServis
    {
        public ApiResponse<List<Hesap>> MusterininTumHesaplariniGetir(string kartNumara);

        public ApiResponse<int> HavaleYap(string gonderenHesapNumara, string aliciHesapNumara, decimal gonderilenTutar, string kartNumara);

        public ApiResponse<bool> HesapVarMi(string hesapNumara);

        public ApiResponse<bool> HesapVarMiTelNoIle(string hesapNumara);




    }
}

