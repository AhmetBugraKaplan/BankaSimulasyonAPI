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

        public int HesapOlustur(string hesapNumara, decimal bakiye)
        {

            return 1;
        }

        public decimal HesapBakiyeGetir(string hesapNumara)
        {

            return 1;
        }

        public List<Hesap> MusterininTumHesaplariniGetir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            List<Hesap> musteriHesapListesi = _context.Hesaplar
            .FromSqlRaw("EXEC SP_MusteriHesaplariniGetir @KartNumara", kartNumaraParam).ToList();

            return musteriHesapListesi;
        }

        public int HesapVarMi(string hesapNumara)
        {

            var hesapNumaraParam = new SqlParameter("@HesapNumara", hesapNumara);
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_HesapVarMi @HesapNumara, @Sonuc OUTPUT", hesapNumaraParam, sonucParam);

            return (int)sonucParam.Value;
        }

        public int HesapLimitYeterliMi(string hesapNumara, decimal gonderilecekPara)
        {
            var hesapNumaraParam = new SqlParameter("@HesapNumara", hesapNumara);
            var gonderilecekParaParam = new SqlParameter("@GonderilecekPara",gonderilecekPara);
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_HespaLimitYeterliMi @HesapNumara, @GonderilecekPara, @Sonuc OUTPUT",
                hesapNumaraParam,gonderilecekParaParam,sonucParam
            );


            return (int)sonucParam.Value;
        }

        public int BaskasininHesabinaHavaleYap(string gonderenHesapNumara,string aliciHesapNumara,decimal gonderilenTutar, string kartNumara)
        {
            var gonderenHesapNumaraParam = new SqlParameter("@GonderenHesapNumara",gonderenHesapNumara);
            var aliciHesapNumaraParam = new SqlParameter("@AliciHesapNumara",aliciHesapNumara);
            var gonderilenTutarParam = new SqlParameter("@GonderilenTutar",gonderilenTutar);
            var kartNumaraParam = new SqlParameter("@KartNumara",kartNumara);
            var sonucParam = new SqlParameter("@Sonuc",SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_BaskasininHesabinaHavaleYap @GonderenHesapNumara, @AliciHesapNumara, @GonderilenTutar, @KartNumara, @Sonuc OUTPUT",
                gonderenHesapNumaraParam,aliciHesapNumaraParam,gonderilenTutarParam,kartNumaraParam,sonucParam
            );

            return (int)sonucParam.Value;
        }







    }
}