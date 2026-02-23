using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public class KartRepository : IKartRepository
    {

        private readonly AppDbContext _context;

        public KartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> KartEkle(int kullaniciHesapId, string KartNumara, string KartSKT, string CVV,string KartTipi,bool AktifMi)
        {

            Kart kart = new Kart
            {
                KullaniciHesapId = kullaniciHesapId,
                KartNumara = KartNumara,
                KartSKT = KartSKT,
                CVV = CVV,
                KartTipi = KartTipi,
                AktifMi = AktifMi
                
            };

            _context.Kartlar.Add(kart);
            int sonuc  = await _context.SaveChangesAsync();

            return sonuc;
        }
    }
}