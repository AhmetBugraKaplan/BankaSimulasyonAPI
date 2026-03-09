using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Repositories
{
    public class HesapRepository : IHesapRepository
    {
        private readonly AppDbContext _context;

        public HesapRepository(AppDbContext context)
        {
            _context = context;
        }

        public KullaniciHesap? kullanicininHessabiniBul(int hesapNumarasi)
        {
            var hesapNumarasiParam = new SqlParameter("@HesapNumarasi", hesapNumarasi);

            var hesap = _context.KullaniciHesaplari
            .FromSqlRaw("EXEC SP_HesapGetirHesapNoGore @HesapNumarasi", hesapNumarasiParam)
            .AsEnumerable()
            .FirstOrDefault();

            return hesap;
        }


        public void hesapGuncelleAsync(KullaniciHesap kullaniciHesap)
        {
            var idParam = new SqlParameter("@Id", kullaniciHesap.id);
            var hesapNumarasiParam = new SqlParameter("@HesapNumarasi", kullaniciHesap.HesapNumarasi);
            var bakiyeParam = new SqlParameter("@Bakiye", kullaniciHesap.Bakiye);

            var sonuc = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonuc.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
           "EXEC SP_HesapGuncelle @Id, @HesapNumarasi, @Bakiye,@Sonuc OUTPUT",
           idParam, hesapNumarasiParam, bakiyeParam, sonuc
           );
        }

        public int kullaniciHesapLimitGuncelle(int kullaniciId, decimal kullaniciHesapLimit)
        {
            var kullaniciIdParam = new SqlParameter("@KullaniciId", kullaniciId);
            var kullaniciHesapLimitParam = new SqlParameter("@HesapLimit", kullaniciHesapLimit);

            var sonuc = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonuc.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw("EXEC SP_KullaniciHesapLimitGuncelle @KullaniciId, @HesapLimit, @Sonuc OUTPUT"
            , kullaniciIdParam, kullaniciHesapLimitParam, sonuc
            );

            return (int)sonuc.Value;
        }

        public decimal kullaniciHesapLimitGetir(int kullaniciId)
        {
            var kullaniciIdParam = new SqlParameter("@KullaniciId", kullaniciId);

            var limit = _context.Database
            .SqlQuery<Decimal>($"EXEC SP_KullaniciHesapLimitGetir {kullaniciId}")
            .AsEnumerable()
            .FirstOrDefault();

            return limit;
        }



    }
}