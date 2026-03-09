using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IKullaniciService
    {
        KullaniciResponse yeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet, string email, string sifre, string kullaniciRol);

        Kullanici? kullaniciGetirIdGore(int id);

        KullaniciResponse kullaniciSilIdGore(int id);

        KullaniciResponse kullaniciHesapEkle(int kullaniciId);
    }
}