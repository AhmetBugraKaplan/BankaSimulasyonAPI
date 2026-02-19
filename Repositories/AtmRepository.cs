using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using Microsoft.EntityFrameworkCore;


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
            return await _context.AtmLer.ToListAsync();
        }

        public async Task<List<ATM>> AtmleriGetirKonumaGoreAsync(string konum)
        {

            return await _context.AtmLer.Where(a => a.Konum == konum).ToListAsync();
        }

        public async Task<List<ATM>> AtmleriGetirAktifligeGoreAsync(bool aktifMi)
        {
            return await _context.AtmLer.Where(a => a.AktifMi == aktifMi).ToListAsync();
        }

        public async Task<int> AtmEkleAsync(string konum, bool aktifMi)
        {



            ATM atm = new ATM
            {
                Konum = konum,
                AktifMi = aktifMi,
                Kasetler = new List<AtmKaset>
              {
                new AtmKaset { SlotNumarasi = 1, Kupur = 0, Adet = 0, KritikDeger = 5 },
                new AtmKaset { SlotNumarasi = 2, Kupur = 0, Adet = 0, KritikDeger = 5 },
                new AtmKaset { SlotNumarasi = 3, Kupur = 0, Adet = 0, KritikDeger = 5 },
                new AtmKaset { SlotNumarasi = 4, Kupur = 0, Adet = 0, KritikDeger = 5 }
              }
            };

            _context.AtmLer.Add(atm);
            return await _context.SaveChangesAsync();
        }

    }
}