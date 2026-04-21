using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Repositories
{
    public interface IOnayRepository
    {
        public int OnayKoduDogruMu (string kod,string telefonNumara);
        void OnayKoduUret(string kod,string telefonNumara);
    }
}