
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

        public ApiResponse<int> HavaleYap(string gonderenHesapNumara, string aliciHesapNumara, decimal gonderilenTutar, string kartNumara,int atmId, bool kendiHesaplarimArasiMi = false);

        public ApiResponse<bool> HesapVarMi(string hesapNumara);

        public ApiResponse<bool> HesapVarMiTelNoIle(string hesapNumara);

        public ApiResponse<int> HesabaKartsizParaGonder(string hesapNumara, decimal gonderilecekTutar);
        public ApiResponse<object> CebeParaGonder(string gonderenKartNo, string aliciTckNO, string aliciTelNo, decimal gonderilenTutar);
        ApiResponse<object> CebeParaCek(string aliciTckNO, string aliciTelNo, string gonderenTelNo, decimal tutar);



    }
}

