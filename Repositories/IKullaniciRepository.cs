using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IKullaniciRepository
    {
        public Task<int> yeniKullaniciEkleAsync(string isim,string soyisim,string telefonNumarasi,string adres,string cinsiyet,string email,string sifre,string kullaniciRol);
        public Task<Kullanici?> kullaniciGetirIdGore(int id);
        public Task<int> kullaniciSilIdGore(int id);
        public Task kullaniciHesapGuncelle(KullaniciHesap kullaniciHesap);

    }
}