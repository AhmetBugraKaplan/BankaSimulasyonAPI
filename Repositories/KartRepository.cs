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

        public int KartEkle(int KullaniciId, string KartNumara, decimal kartGunlukLimit, string KartSifre)
        {

            //Önce input atamalarını yapıyoaruz her zaman
            var kullaniciIdParam = new SqlParameter("@KullaniciId", KullaniciId);
            var kartNumaraParam = new SqlParameter("@KartNumara", KartNumara);
            var kartSifreParam = new SqlParameter("@SifreHash", KartSifre);

            //Sonrasında output atamasını yapıp direction ile bu değerin output olduğunu belirtiyoruz
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
            "EXEC SP_YeniKartEkle @KullaniciId, @KartNumara,@SifreHash ,@Sonuc OUTPUT",
            kullaniciIdParam, kartNumaraParam, kartSifreParam, sonucParam
            );


            return (int)sonucParam.Value;
        }



        public int KartKalanLimitGuncelle(string kartNumara, decimal yeniKartLimit)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);
            var yeniKartLimitParam = new SqlParameter("@YeniKartLimit", yeniKartLimit);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXECUTE SP_KartKalanLimitGuncelle @KartNumara,@YeniKartLimit,@Sonuc OUTPUT", kartNumaraParam, yeniKartLimitParam, sonucParam);

            return (int)sonucParam.Value;
        }


        public decimal KartKalanLimitGetir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            decimal kartKalanLimit = _context.Database.SqlQuery<Decimal>
            ($"EXEC SP_KalanKartLimitGetir {kartNumaraParam}")
            .AsEnumerable()
            .FirstOrDefault();

            return kartKalanLimit;
        }


        public List<Kart> TumKartlariGetir(int kullaniciId)
        {
            var kullaniciIdParam = new SqlParameter("@KullaniciId", kullaniciId);

            var kartListesi = _context.Kartlar.FromSqlRaw("EXEC SP_TumKartlariGetir @KullaniciId", kullaniciIdParam).ToList();

            return kartListesi;
        }



        //SP_KartSifreGuncelle
        public int KartSifreGuncelle(string yeniKartSifre, int kartId)
        {
            var yeniKartSifreParam = new SqlParameter("@KartSifre", yeniKartSifre);
            var kartIdParam = new SqlParameter("@KartId", kartId);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_KartSifreGuncelle @KartSifre,@KartId,@Sonuc OUTPUT", yeniKartSifreParam, kartIdParam, sonucParam);

            return (int)sonucParam.Value;
        }




        //Burası düzenlenecek
        public int KartSifreGetir(int kullaniciId, string kartNumara)
        {
            var kullaniciIdParam = new SqlParameter("@KullaniciId", kullaniciId);
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;






            return 1;
        }




    }
}