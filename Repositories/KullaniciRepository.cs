using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Migrations;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Repositories
{
    public class KullaniciRepository : IKullaniciRepository
    {

        private readonly AppDbContext _context;

        public KullaniciRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> yeniKullaniciEkleAsync(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet)
        {
            Kullanici kullanici = new Kullanici
            {
                Isim = isim,
                Soyisim = soyisim,
                TelefonNumarasi = telefonNumarasi,
                Adres = adres,
                Cinsiyet = cinsiyet
            };

            _context.Kullanicilar.Add(kullanici);
            int sonuc = await _context.SaveChangesAsync();

            return sonuc;
        }




        public async Task<Kullanici?> kullaniciGetirIdGore(int id)
        {
            return await _context.Kullanicilar.FirstOrDefaultAsync(k => k.id == id);
        }




        public async Task<int> kullaniciSilIdGore(int id)
        {

            int sonuc;
            var kullanici = await kullaniciGetirIdGore(id);

            if (kullanici == null)
            {
                sonuc = 0;
                return sonuc ;
            }
            else
            {
                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();
                sonuc = 1;
                return sonuc;
                
            }

        }


        public async Task kullaniciHesapGuncelle(KullaniciHesap kullaniciHesap)
        {
            _context.KullaniciHesaplari.Update(kullaniciHesap);
            await _context.SaveChangesAsync();
        }





    }
}