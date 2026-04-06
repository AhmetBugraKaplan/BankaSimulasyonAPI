using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;

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



    }
}