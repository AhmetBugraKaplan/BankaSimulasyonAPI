using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Repositories
{
    public class OnayRepository: IOnayRepository
    {
        private readonly AppDbContext _context;

        public OnayRepository(AppDbContext context)
        {
            _context = context;
        }

        public int OnayKoduDogruMu(string kod, string telefonNumara)
        {
            var telefonNumaraParam = new SqlParameter("@TelefonNumara", telefonNumara);
            var kodParam = new SqlParameter("@Kod", kod);

            var sonuc = _context.Database.SqlQueryRaw<int>(
                "EXEC SP_SMSOnayKoduDogruMu @TelefonNumara, @Kod", telefonNumaraParam, kodParam
            ).AsEnumerable()
            .FirstOrDefault();

            return sonuc;
        }

        public void OnayKodunuDbKaydet(string kod, string telefonNumara)
        {
            var telefonParam = new SqlParameter("@TelefonNumara", telefonNumara);
            var kodParam = new SqlParameter("@Kod", kod);

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_OnayKodunuKaydet @TelefonNumara, @Kod",
                telefonParam, kodParam
            );
        }

    }
}