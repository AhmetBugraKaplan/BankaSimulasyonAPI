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
        Task<KullaniciResponse> yeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet);

        Task<Kullanici?> kullaniciGetirIdGore(int id);

        Task<KullaniciResponse> kullaniciSilIdGore(int id);
        Task<KullaniciResponse> kullaniciHesaptanParaCek();
    }
}