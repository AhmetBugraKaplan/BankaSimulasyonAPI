using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace BankaSimulasyon.Repositories
{
    public class AtmRepository : IAtmRepository
    {
        private readonly AppDbContext _context;

        public AtmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ATM>> TumAtmleriGetirAsync()
        {
            var atmListesi = _context.AtmLer
            .FromSqlRaw("EXEC SP_TumATMGetir").ToList();

            return  atmListesi;
        }

        public async Task<List<ATM>> AtmleriGetirKonumaGoreAsync(string konum)
        {
            var konumParam = new SqlParameter("@Konum",konum);

            var atmListesi = _context.AtmLer
            .FromSqlRaw("EXEC SP_ATMGetirKonumaGore @Konum",konumParam)
            .ToList();

            return atmListesi;
        }

        public async Task<List<ATM>> AtmleriGetirAktifligeGoreAsync(bool aktifMi)
        {
            var aktifMiParam = new SqlParameter("@AktifMi",aktifMi);

            var atmListesi = _context.AtmLer
            .FromSqlRaw("EXEC SP_ATMGetirAktifligeGore @AktifMi",aktifMiParam)
            .ToList();

            return atmListesi;
        }

        public async Task<int> AtmEkleAsync(string konum, bool aktifMi)
        {

            var konumParam = new SqlParameter("@Konum",konum);
            var aktifMiParam = new SqlParameter("@AktifMi",aktifMi);

            var etkilenenSatirParam = new SqlParameter("@EtkilenenSatir",SqlDbType.Int);
            etkilenenSatirParam.Direction = ParameterDirection.Output;


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_AtmEkle @Konum, @AktifMi, @EtkilenenSatir OUTPUT",
                konumParam,aktifMiParam,etkilenenSatirParam
            );

           int sonuc = (int)etkilenenSatirParam.Value;

           return sonuc; 
        }

    }
}