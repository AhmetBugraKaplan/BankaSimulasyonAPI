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
        AtmdenParaCekmeResponse AtmdenParaCek(int atmId, int cekilecekTutar,string kartNumara,int kullaniciId);
        int AtmdekiToplamParayiIdIleGetir(int atmId);
        KasetGuncellemeResponse AtmKasetlerdekiKupurleriGuncelle(int atmId, int slotNumarasi, int adet, int kupur);
        int AtmdekiToplamParayiHesapla(List<AtmKaset> kasetDizisi);
        List<ATM> TumAtmleriGetir();
        AtmEklemeResponse AtmEkle(string konum, bool aktifMi);
        List<ATM> AtmleriGetirAktifligeGore(bool aktifMi);



    }
}