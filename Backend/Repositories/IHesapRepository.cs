using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Repositories
{
    public interface IHesapRepository
    {
        public int HesapOlustur(string hesapNumara, decimal bakiye);
        public decimal HesapBakiyeGetir(string hesapNumara);
        public List<Hesap> MusterininTumHesaplariniGetir(string kartNumara);
        public int HesapVarMi(string hesapNumara);
        public int HesapLimitYeterliMi(string hesapNumara, decimal gonderilecekPara);
        public int HesapBakiyeGuncelle(string hesapNumara, decimal degisimTutari);
        public int HesapVarMiTelNoIle(string telefonNumara);
        public int HesabaKartsizParaGonder(string hesapNumara, decimal gonderilecekTutar);
        public CebeSpResponse CebeParaGonder(string gonderenHesapNo, string aliciTckNO, 
                                                string aliciTelNo, decimal tutar);
        public string KartNoIleMusteriTelNoGetir(string kartNumara);
        public CebeSpResponse CebeParaCek(string aliciTckNO,string aliciTelNo,string gonderenTelNo,decimal tutar);
        public void IslemGecmisiEkleTekTarafli(string hesapNumara, string islemTuru, string islemYonu, decimal tutar,
        decimal islemSonrasiBakiye, int atmID, string islemAciklama);
        public void IslemGecmisiEkleCiftTarafli(string gonderenHesapNumara, string aliciHesapNumara, string islemTuru, decimal tutar,
        decimal gonderenIslemSonrasiBakiye, decimal aliciIslemSonrasiBakiye, int atmID, string gonderenAciklama, string aliciAciklama);
 

    }
}