using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IAtmService
    {
        AtmdenParaCekmeResponse AtmdenParaCek(int atmId, int cekilecekTutar,string kartNumara);
        ApiResponse<int> AtmdekiToplamParayiIdIleGetir(int atmId);
        ApiResponse<object> AtmKasetlerdekiKupurleriGuncelle(int atmId, int slotNumarasi, int adet, int kupur);
        ApiResponse<List<ATM>> TumAtmleriGetir();
        ApiResponse<object> AtmEkle(string konum, bool aktifMi);
        int AtmdekiToplamParayiHesapla(List<AtmKaset> kasetDizisi);


    }
}