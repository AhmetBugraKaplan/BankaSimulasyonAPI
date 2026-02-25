using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
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

        public async Task<KullaniciHesap?> kullanicininHessabiniBulAsync(int hesapNumarasi)
        {
            return await _context.KullaniciHesaplari.FirstOrDefaultAsync(k => k.HesapNumarasi == hesapNumarasi);
        }

        public async Task hesapGuncelleAsync(KullaniciHesap kullaniciHesap)
        {
            
            _context.KullaniciHesaplari.Update(kullaniciHesap);
            await _context.SaveChangesAsync();
        }
    }
}