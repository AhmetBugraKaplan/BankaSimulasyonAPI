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

        public async Task<KullaniciHesap?> kullanicininHessabiniBulAsync(int hesapNumarasi)
        {
            var hesapNumarasiParam = new SqlParameter("@HesapNumarasi",hesapNumarasi);

            var hesap = _context.KullaniciHesaplari
            .FromSqlRaw("EXEC SP_HesapGetirHesapNoGore @HesapNumarasi",hesapNumarasiParam)
            .AsEnumerable()
            .FirstOrDefault();

            return hesap;
        }

         
        public async Task hesapGuncelleAsync(KullaniciHesap kullaniciHesap)
        {

            var idParam = new SqlParameter("@Id", kullaniciHesap.id);
            var hesapNumarasiParam = new SqlParameter("@HesapNumarasi", kullaniciHesap.HesapNumarasi);
            var bakiyeParam = new SqlParameter("@Bakiye", kullaniciHesap.Bakiye);
            var sifreParam = new SqlParameter("@Sifre", kullaniciHesap.Sifre);

            var sonuc = new SqlParameter("Sonuc", SqlDbType.Int);
            sonuc.Direction = ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync(
            "EXEC SP_HesapGuncelle @Id, @HesapNumarasi, @Bakiye, @Sifre, @Sonuc OUTPUT",
            idParam, hesapNumarasiParam, bakiyeParam, sifreParam, sonuc
            );
        }
    }
}