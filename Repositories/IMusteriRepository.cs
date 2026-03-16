using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Repositories
{
    public interface IMusteriRepository
    {
        public int YeniMusteriEkle(string isim,string soyisim);
        public Musteri? MusteriGetirIdGore(int id);
        public int MusteriSilIdGore(int id);

    }
}