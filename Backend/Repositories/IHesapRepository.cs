using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IHesapRepository
    {
        public int HesapOlustur(string hesapNumara,decimal bakiye);
        public decimal HesapBakiyeGetir(string hesapNumara);
        public List<Hesap> MusterininTumHesaplariniGetir(string kartNumara);
        public int HesapVarMi(string hesapNumara);
        public int HesapLimitYeterliMi(string hesapNumara, decimal gonderilecekPara);
        public int HesapBakiyeGuncelle(string hesapNumara,decimal degisimTutari);
        public int HesapVarMiTelNoIle(string telefonNumara);
        public int HesabaKartsizParaGonder(string hesapNumara,decimal gonderilecekTutar);
    }
}