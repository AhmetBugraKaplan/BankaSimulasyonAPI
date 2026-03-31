using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Repositories
{
    public class AtmKasetRepository : IAtmKasetRepository
    {
        private readonly AppDbContext _context;
        public AtmKasetRepository(AppDbContext context)
        {
            _context = context;
        }



        public List<AtmKaset> AtmdekiKasetleriGetir(int atmId)
        {

            var atmIdParam = new SqlParameter("@atmId", atmId);

            var kasetListesi = _context.AtmKasetler
            .FromSqlRaw("EXEC SP_AtmdekiKasetleriGetir @atmId", atmIdParam)
            .ToList();

            return kasetListesi;
        }



        public void AtmKasetGuncelle(AtmKaset atmKaset)
        {
            var atmIdParam = new SqlParameter("@AtmId", atmKaset.AtmId);
            var SlotNumarasiParam = new SqlParameter("@SlotNumarasi", atmKaset.SlotNumarasi);
            var adetParam = new SqlParameter("@Adet", atmKaset.Adet);
            var kupurParam = new SqlParameter("@Kupur", atmKaset.Kupur);
            var kritikDegerParam = new SqlParameter("@KritikDeger", atmKaset.KritikDeger);

            _context.Database.ExecuteSqlRaw(
                "EXEC SP_ATMKasetleriGuncelle @AtmId, @SlotNumarasi, @Adet, @Kupur, @KritikDeger"
                , atmIdParam, SlotNumarasiParam, adetParam, kupurParam, kritikDegerParam);

        }

    }
}

//Async thread kullanımı tam olarka ne işe yarıyor