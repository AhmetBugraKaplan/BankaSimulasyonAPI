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

        public async Task<int> yeniKullaniciEkleAsync(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet)
        {
            var isimParam = new SqlParameter("@Isim", isim);
            var soyisimParam = new SqlParameter("@Soyisim", soyisim);
            var telefonParam = new SqlParameter("@TelefonNumarasi", telefonNumarasi);
            var adresParam = new SqlParameter("@Adres", adres);
            var cinsiyetParam = new SqlParameter("@Cinsiyet", cinsiyet);
            

            var etkilenenSatirParam = new SqlParameter("@EtkilenenSatir", SqlDbType.Int);
            etkilenenSatirParam.Direction = ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_YeniMusteriEkle @Isim,  @Soyisim, @TelefonNumarasi, @Adres, @Cinsiyet, @EtkilenenSatir OUTPUT",
                isimParam, soyisimParam, telefonParam, adresParam, cinsiyetParam, etkilenenSatirParam
            );

            int sonuc = (int)etkilenenSatirParam.Value;

            return sonuc;
        }




        public async Task<Kullanici?> kullaniciGetirIdGore(int id)
        {
            return await _context.Kullanicilar.FirstOrDefaultAsync(k => k.id == id);
        }




        public async Task<int> kullaniciSilIdGore(int id)
        {

            var idParam = new SqlParameter("@Id",id);

            var _return = new SqlParameter("@Return",SqlDbType.Int);
            _return.Direction = ParameterDirection.Output; 


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_KullaniciSilIdGore @Id,@Return OUTPUT",idParam,_return
            );


            int sonuc = (int) _return.Value;

            return sonuc;

        }


        public async Task kullaniciHesapGuncelle(KullaniciHesap kullaniciHesap)
        {
            _context.KullaniciHesaplari.Update(kullaniciHesap);
            await _context.SaveChangesAsync();
        }





    }
}