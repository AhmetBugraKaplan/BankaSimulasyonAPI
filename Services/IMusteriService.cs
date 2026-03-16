using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IMusteriService
    {
        ApiResponse YeniMusteriEkle(string isim, string soyisim);

        ApiResponse MusteriGetirIdGore(int id);

        ApiResponse MusteriSilIdGore(int id);

    }
}