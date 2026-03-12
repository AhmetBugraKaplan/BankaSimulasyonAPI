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

        public int KartEkle(int KullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi,string KartSifre)
        {

            //Önce input atamalarını yapıyoaruz her zaman
            var kullaniciIdParam = new SqlParameter("@KullaniciId", KullaniciId);
            var kartNumaraParam = new SqlParameter("@KartNumara", KartNumara);
            var kartSktParam = new SqlParameter("@KartSKT", KartSKT);
            var cvvParam = new SqlParameter("@CVV", CVV);
            var kartTipiParam = new SqlParameter("@KartTipi", KartTipi);
            var aktifMiParam = new SqlParameter("@AktifMi", AktifMi);
            var kartSifreParam = new SqlParameter("@SifreHash",KartSifre);

            //Sonrasında output atamasını yapıp direction ile bu değerin output olduğunu belirtiyoruz
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
            "EXEC SP_YeniKartEkle @KullaniciId, @KartNumara, @KartSKT, @CVV, @KartTipi, @AktifMi,@SifreHash ,@Sonuc OUTPUT",
            kullaniciIdParam, kartNumaraParam, kartSktParam, cvvParam, kartTipiParam, aktifMiParam,kartSifreParam ,sonucParam
            );


            int sonuc = (int)sonucParam.Value;


            return sonuc;
        }



        public int KartLimitGuncelle(string kartNumara, decimal yeniKartLimit,int kullaniciId)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);
            var kullaniciKartLimitParam = new SqlParameter("@KartLimit", yeniKartLimit);
            var kullaniciIdParam = new SqlParameter("@KullaniciId",kullaniciId); 

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXECUTE SP_KullaniciKartLimitGuncelle @KartNumara,@KartLimit,@KullaniciId,@Sonuc OUTPUT", kartNumaraParam, kullaniciKartLimitParam, kullaniciIdParam,sonucParam);

            return (int)sonucParam.Value;
        }


        public decimal KartLimitGetir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            decimal kartLimit = _context.Database.SqlQuery<Decimal>
            ($"EXEC SP_KullaniciKartLimitGetir {kartNumaraParam}")
            .AsEnumerable()
            .FirstOrDefault();

            return kartLimit;
        }


        public List<Kart> TumKartlariGetir(int kullaniciId)
        {
            var kullaniciIdParam = new SqlParameter("@KullaniciId", kullaniciId);

            var kartListesi = _context.Kartlar.FromSqlRaw("EXEC SP_TumKartlariGetir @KullaniciId", kullaniciIdParam).ToList();

            return kartListesi;
        }



        //SP_KartSifreGuncelle
        public int KartSifreGuncelle(int yeniKartSifre,int kartId)
        {
            var yeniKartSifreParam = new SqlParameter("@KartSifre",yeniKartSifre);
            var kartIdParam = new SqlParameter("@KartId",kartId);

            var sonucParam = new SqlParameter("@Sonuc",SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_KartSifreGuncelle @KartSifre,@KartId,@Sonuc OUTPUT",yeniKartSifreParam,kartIdParam,sonucParam);
            
            return (int)sonucParam.Value;
        }





    }
}