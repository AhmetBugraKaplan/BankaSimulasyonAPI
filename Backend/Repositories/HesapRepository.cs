using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BankaSimulasyon.Models.Responses;


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
            var gonderilecekParaParam = new SqlParameter("@GonderilecekPara", gonderilecekPara);
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_HespaLimitYeterliMi @HesapNumara, @GonderilecekPara, @Sonuc OUTPUT",
                hesapNumaraParam, gonderilecekParaParam, sonucParam
            );


            return (int)sonucParam.Value;
        }


        public int HesapBakiyeGuncelle(string hesapNumara, decimal degisimTutari)
        {
            var hesapNumaraParam = new SqlParameter("@HesapNumara", hesapNumara);
            var degisimTutariParam = new SqlParameter("@DegisimTutari", degisimTutari);
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_HesapBakiyeGuncelle @HesapNumara, @DegisimTutari, @Sonuc OUTPUT"
                , hesapNumaraParam, degisimTutariParam, sonucParam
            );

            return (int)sonucParam.Value;
        }

        public int HesapVarMiTelNoIle(string telefonNumara)
        {
            var telefonNumaraParam = new SqlParameter("@TelefonNumara", telefonNumara);
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_MusteriVarMiTelNoIleKontrol @TelefonNumara, @Sonuc OUTPUT", telefonNumaraParam, sonucParam
            );

            return (int)sonucParam.Value;
        }


        public int HesabaKartsizParaGonder(string hesapNumara, decimal gonderilecekTutar)
        {
            var hesapNumaraParam = new SqlParameter("@HesapNumara", hesapNumara);
            var gonderilecekTutarParam = new SqlParameter("@GonderilenTutar", gonderilecekTutar);
            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_KartsizParaGonderHesaba @HesapNumara, @GonderilenTutar, @Sonuc OUTPUT",
                hesapNumaraParam, gonderilecekTutarParam, sonucParam
            );

            return (int)sonucParam.Value;
        }

        public CebeSpResponse CebeParaGonder(string gonderenHesapNo, string aliciTckNO, string aliciTelNo, decimal tutar)
        {
            var gonderenHesapNoParam = new SqlParameter("@GonderenHesapNo", gonderenHesapNo);
            var aliciTckNOParam = new SqlParameter("@AliciTckNO", aliciTckNO);
            var aliciTelNoParam = new SqlParameter("@AliciTelNo", aliciTelNo);
            var tutarParam = new SqlParameter("@Tutar", tutar);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            var mesajParam = new SqlParameter("@Mesaj", SqlDbType.NVarChar, 255);
            mesajParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_CebeParaGonder @GonderenHesapNo, @AliciTckNO, @AliciTelNo, @Tutar, @Sonuc OUTPUT, @Mesaj OUTPUT",
                gonderenHesapNoParam, aliciTckNOParam, aliciTelNoParam, tutarParam, sonucParam, mesajParam
            );

            return new CebeSpResponse
            {
                Sonuc = (int)sonucParam.Value,
                Mesaj = mesajParam.Value?.ToString() ?? string.Empty
            };
        }


        public string? KartNoIleMusteriTelNoGetir(string kartNumara)
        {
            var kartNumaraParam = new SqlParameter("@KartNumara", kartNumara);

            var telefonNumaraParam = new SqlParameter("@TelefonNumara", SqlDbType.NVarChar, 16);
            telefonNumaraParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_KartNoIleMusteriTelNoGetir @KartNumara, @TelefonNumara OUTPUT",
                kartNumaraParam, telefonNumaraParam
            );

            return telefonNumaraParam.Value?.ToString();
        }


        public CebeSpResponse CebeParaCek(string aliciTckNO, string aliciTelNo, string gonderenTelNo, decimal tutar)
        {
            var aliciTckNOParam = new SqlParameter("@AliciTckNO", aliciTckNO);
            var aliciTelNoParam = new SqlParameter("@AliciTelNo", aliciTelNo);
            var gonderenTelNoParam = new SqlParameter("@GonderenTelNo", gonderenTelNo);
            var tutarParam = new SqlParameter("@Tutar", tutar);

            var sonucParam = new SqlParameter("@Sonuc", SqlDbType.Int);
            sonucParam.Direction = ParameterDirection.Output;

            var mesajParam = new SqlParameter("@Mesaj", SqlDbType.NVarChar, 255);
            mesajParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_CebeParaCek @AliciTckNO, @AliciTelNo, @GonderenTelNo, @Tutar, @Sonuc OUTPUT, @Mesaj OUTPUT",
                aliciTckNOParam, aliciTelNoParam, gonderenTelNoParam, tutarParam, sonucParam, mesajParam
            );

            return new CebeSpResponse
            {
                Sonuc = (int)sonucParam.Value,
                Mesaj = mesajParam.Value?.ToString() ?? string.Empty
            };
        }

        public void IslemGecmisiEkleTekTarafli(string hesapNumara, string islemTuru, string islemYonu, decimal tutar,
        decimal islemSonrasiBakiye, int atmID, string islemAciklama)
        {
            var hesapNumaraParam = new SqlParameter("@HesapNumara", hesapNumara);
            var islemTuruParam = new SqlParameter("@IslemTuru", islemTuru);
            var islemYonuParam = new SqlParameter("@IslemYonu", islemYonu);
            var tutarParam = new SqlParameter("@IslemTutar", tutar);
            var islemSonrasiBakiyeParam = new SqlParameter("@IslemSonrasiBakiye", islemSonrasiBakiye);
            var atmIDParam = new SqlParameter("@AtmID", atmID);
            var islemAciklamaParam = new SqlParameter("@IslemAciklama", islemAciklama);

            _context.Database.ExecuteSqlRaw(
            "EXEC SP_IslemGecmisiEkleTekTarafli @HesapNumara, @IslemTuru, @IslemYonu, @IslemTutar, @IslemSonrasiBakiye, @AtmID, @IslemAciklama",
            hesapNumaraParam, islemTuruParam, islemYonuParam, tutarParam,
            islemSonrasiBakiyeParam, atmIDParam, islemAciklamaParam);
        }

        public void IslemGecmisiEkleCiftTarafli(string gonderenHesapNumara, string aliciHesapNumara, string islemTuru, decimal tutar,
        decimal gonderenIslemSonrasiBakiye, decimal aliciIslemSonrasiBakiye, int atmID, string gonderenAciklama, string aliciAciklama)
        {
            var gonderenHesapNumaraParam = new SqlParameter("@GonderenHesapNumara", gonderenHesapNumara);
            var aliciHesapNumaraParam = new SqlParameter("@AliciHesapNumara", aliciHesapNumara);
            var islemTuruParam = new SqlParameter("@IslemTuru", islemTuru);
            var tutarParam = new SqlParameter("@Tutar", tutar);
            var gonderenIslemSonrasiBakiyeParam = new SqlParameter("@GonderenIslemSonrasiBakiye", gonderenIslemSonrasiBakiye);
            var aliciIslemSonrasiBakiyeParam = new SqlParameter("@AliciIslemSonrasiBakiye", aliciIslemSonrasiBakiye);
            var atmIDParam = new SqlParameter("@AtmID", atmID);
            var gonderenAciklamaParam = new SqlParameter("@GonderenAciklama", gonderenAciklama);
            var aliciAciklamaParam = new SqlParameter("@AliciAciklama", aliciAciklama);

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_IslemGecmisiEkleCiftTarafli @GonderenHesapNumara, @AliciHesapNumara, @IslemTuru, @Tutar, @GonderenIslemSonrasiBakiye, @AliciIslemSonrasiBakiye, @AtmID, @GonderenAciklama, @AliciAciklama",
                gonderenHesapNumaraParam, aliciHesapNumaraParam, islemTuruParam, tutarParam,
                gonderenIslemSonrasiBakiyeParam, aliciIslemSonrasiBakiyeParam, atmIDParam,
                gonderenAciklamaParam, aliciAciklamaParam
            );
        }











    }
}