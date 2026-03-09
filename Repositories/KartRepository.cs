using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Microsoft.EntityFrameworkCore;


namespace BankaSimulasyon.Repositories
{
    public class KartRepository : IKartRepository
    {

        private readonly AppDbContext _context;

        public KartRepository(AppDbContext context)
        {
            _context = context;
        }

        public int KartEkle(int KullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi)
        {

            //Önce input atamalarını yapıyoaruz her zaman
            var kullaniciIdParam = new SqlParameter("@KullaniciId", KullaniciId);
            var kartNumaraParam = new SqlParameter("@KartNumara", KartNumara);
            var kartSktParam = new SqlParameter("@KartSKT", KartSKT);
            var cvvParam = new SqlParameter("@CVV", CVV);
            var kartTipiParam = new SqlParameter("@KartTipi", KartTipi);
            var aktifMiParam = new SqlParameter("@AktifMi", AktifMi);

            //Sonrasında output atamasını yapıp direction ile bu değerin output olduğunu belirtiyoruz
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
            "EXEC SP_YeniKartEkle @KullaniciId, @KartNumara, @KartSKT, @CVV, @KartTipi, @AktifMi, @Sonuc OUTPUT",
            kullaniciIdParam, kartNumaraParam, kartSktParam, cvvParam, kartTipiParam, aktifMiParam, sonucParam
            );


            int sonuc = (int)sonucParam.Value;


            return sonuc;
        }



        public int KullaniciKartLimitGuncelle(int kullaniciId, decimal kullaniciKartLimit)
        {
            var kullaniciIdParam = new SqlParameter("@KullaniciId",kullaniciId);
            var kullaniciKartLimitParam = new SqlParameter("@KartLimit",kullaniciKartLimit);

            var sonucParam = new SqlParameter("@Sonuc",SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXECUTE SP_KullaniciKartLimitGuncelle @KullaniciId,@KartLimit,@Sonuc OUTPUT",kullaniciIdParam,kullaniciKartLimitParam,sonucParam);

            return (int)sonucParam.Value;            
        }

    }
}