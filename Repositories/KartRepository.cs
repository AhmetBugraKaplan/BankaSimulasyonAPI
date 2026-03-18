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
            var yeniKartKalanLimitParam = new SqlParameter("@YeniKartLimit", yeniKartLimit);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXECUTE SP_KartKalanLimitGuncelle @KartNumara,@YeniKartLimit,@Sonuc OUTPUT", kartNumaraParam, yeniKartKalanLimitParam, sonucParam);

            return (int)sonucParam.Value;
        }

        public int KartGunlukLimitGuncelle(string kartNumara, decimal yeniKartLimit)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);
            var yeniKartGunlukLimitParam = new SqlParameter("@KartGunlukLimit", yeniKartLimit);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_KartGunlukKalanLimitGuncelle @KartNumara, @KartGunlukLimit, @Sonuc OUTPUT",kartNumaraParam,yeniKartGunlukLimitParam,sonucParam);
            
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
        public int KartSifreGuncelle(string yeniKartSifre, string kartNumara)
        {
            var yeniKartSifreParam = new SqlParameter("@KartSifre", yeniKartSifre);
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_KartSifreGuncelle @KartSifre,@KartNumara,@Sonuc OUTPUT", yeniKartSifreParam, kartNumaraParam, sonucParam);

            return (int)sonucParam.Value;
        }




        //Burası düzenlenecek
        public string? KartSifreGetir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            return _context.Database.SqlQuery<string>(
                $"EXEC SP_KartSifreGetir {kartNumaraParam}")
                .AsEnumerable()
                .FirstOrDefault();
        }

        public void YanlisGirisSayisiniArttir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            _context.Database.ExecuteSqlRaw("EXEC SP_KartYanlisGirisSayisiArttir @KartNumara", kartNumaraParam);
        }

        public int YanlisGirisSayisiGetir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);
            return _context.Database.SqlQuery<int>($"EXEC SP_KartYanlisGirisSayisiGetir {kartNumaraParam}")
            .AsEnumerable()
            .FirstOrDefault();
        }


        public void YanlisGirisSayisiSifirla(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);
            _context.Database.ExecuteSqlRaw("EXEC SP_KartYanlisGirisSayisiSifirla @KartNumara", kartNumaraParam);
        }


    }
}