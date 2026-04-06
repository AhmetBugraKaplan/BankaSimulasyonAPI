using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Repositories
{
    public interface IHesapRepository
    {
        public int HesapOlustur(string hesapNumara,decimal bakiye);
        public decimal HesapBakiyeGetir(string hesapNumara);
    }
}