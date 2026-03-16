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
    public class MusteriRepository : IMusteriRepository
    {

        private readonly AppDbContext _context;

        public MusteriRepository(AppDbContext context)
        {
            _context = context;
        }

        public int YeniMusteriEkle(string isim, string soyisim)
        {
            var isimParam = new SqlParameter("@Isim", isim);
            var soyisimParam = new SqlParameter("@Soyisim", soyisim);
            var sonuc = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonuc.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
               "EXEC SP_YeniMusteriEkle @Isim, @Soyisim,@Sonuc OUTPUT",
               isimParam, soyisimParam, sonuc
           );

            return (int)sonuc.Value; ;
        }




        public Musteri? MusteriGetirIdGore(int id)
        {
            var idParam = new SqlParameter("@Id", id);

            var kullanici = _context.Musteriler
            .FromSqlRaw("EXEC SP_MusteriGetirIdGore @Id", idParam)
            .AsEnumerable()
            .FirstOrDefault();

            return kullanici;
        }




        public int MusteriSilIdGore(int id)
        {

            var idParam = new SqlParameter("@Id", id);

            var _return = new SqlParameter("@Sonuc", SqlDbType.Int);
            _return.Direction = ParameterDirection.Output;


            _context.Database.ExecuteSqlRaw(
               "EXEC SP_MusteriSilIdGore @Id,@Sonuc OUTPUT", idParam, _return
           );


            int sonuc = (int)_return.Value;

            return sonuc;

        }






    }
}