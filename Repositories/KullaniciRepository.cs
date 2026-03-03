using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankaSimulasyon.Repositories
{
    public class KullaniciRepository : IKullaniciRepository
    {

        private readonly AppDbContext _context;

        public KullaniciRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> yeniKullaniciEkleAsync(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet, string email, string sifre,string kullaniciRol)
        {
            var isimParam = new SqlParameter("@Isim", isim);
            var soyisimParam = new SqlParameter("@Soyisim", soyisim);
            var telefonParam = new SqlParameter("@TelefonNumarasi", telefonNumarasi);
            var adresParam = new SqlParameter("@Adres", adres);
            var cinsiyetParam = new SqlParameter("@Cinsiyet", cinsiyet);
            var emailParam = new SqlParameter("@Email", email);
            var sifreParam = new SqlParameter("@Sifre", sifre);
            var kullaniciRolParam = new SqlParameter("@KullaniciRol",kullaniciRol);


            var etkilenenSatirParam = new SqlParameter("@EtkilenenSatir", SqlDbType.Int);
            etkilenenSatirParam.Direction = ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_YeniMusteriEkle @Isim,  @Soyisim, @TelefonNumarasi, @Adres, @Cinsiyet, @Email, @Sifre,@KullaniciRol,@EtkilenenSatir OUTPUT",
                isimParam, soyisimParam, telefonParam, adresParam, cinsiyetParam, emailParam, sifreParam, kullaniciRolParam,etkilenenSatirParam
            );

            int sonuc = (int)etkilenenSatirParam.Value;

            return sonuc;
        }




        public async Task<Kullanici?> kullaniciGetirIdGore(int id)
        {
            var idParam = new SqlParameter("@Id", id);

            var kullanici = _context.Kullanicilar
            .FromSqlRaw("EXEC SP_KullaniciGetirIdGore @Id", idParam)
            .AsEnumerable()
            .FirstOrDefault();

            return kullanici;
        }




        public async Task<int> kullaniciSilIdGore(int id)
        {

            var idParam = new SqlParameter("@Id", id);

            var _return = new SqlParameter("@Return", SqlDbType.Int);
            _return.Direction = ParameterDirection.Output;


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_KullaniciSilIdGore @Id,@Return OUTPUT", idParam, _return
            );


            int sonuc = (int)_return.Value;

            return sonuc;

        }

        //Bu işlemi zaten HesapRepositroy=>HesapGuncelle Fonksiyonu yapıyor şu anda aktif olarak referansıda yok ileride bi kontrol edelim tekrardan.
        public async Task kullaniciHesapGuncelle(KullaniciHesap kullaniciHesap)
        {
            var idParam = new SqlParameter("@Id", kullaniciHesap.id);
            var hesapNumarasiParam = new SqlParameter("@HesapNumarasi", kullaniciHesap.HesapNumarasi);
            var bakiyeParam = new SqlParameter("@Bakiye", kullaniciHesap.Bakiye);
            var sifreParam = new SqlParameter("@Sifre", kullaniciHesap.Sifre);

            var sonuc = new SqlParameter("Sonuc", SqlDbType.Int);
            sonuc.Direction = ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync(
            "EXEC SP_KullaniciHesapGuncelle @Id, @HesapNumarasi, @Bakiye, @Sifre, @Sonuc OUTPUT",
            idParam, hesapNumarasiParam, bakiyeParam, sifreParam, sonuc
            );
        }





    }
}