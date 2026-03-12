using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Repositories
{
    public interface IKullaniciRepository
    {
        public int yeniKullaniciEkle(string isim,string soyisim,string telefonNumarasi,string adres,string cinsiyet);
        public Kullanici? kullaniciGetirIdGore(int id);
        public int kullaniciSilIdGore(int id);
        public void kullaniciHesapGuncelle(KullaniciHesap kullaniciHesap);

        public int kullaniciHesapEkle (int kullaniciId,int hesapNumarasi,decimal bakiye);

        public int HesapLimitGuncelle (int hesapNumarasi);

    }
}