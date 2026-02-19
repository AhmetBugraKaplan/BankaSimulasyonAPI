using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
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



        public async Task<List<AtmKaset>> AtmdekiKasetleriGetirAsync(int atmId)
        {
            var kasetDizisi = await _context.AtmKasetler.Where(k => k.AtmId == atmId).ToListAsync();

            return kasetDizisi;
        }



        public async Task AtmKasetGuncelleAsync(AtmKaset atmKaset)
        {
            _context.AtmKasetler.Update(atmKaset);
            await _context.SaveChangesAsync();
        }

        



    }
}