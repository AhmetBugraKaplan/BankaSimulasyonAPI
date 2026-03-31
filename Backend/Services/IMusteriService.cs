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
        ApiResponse<object> YeniMusteriEkle(string isim, string soyisim);

        ApiResponse<Musteri> MusteriGetirIdGore(int id);

        ApiResponse<object> MusteriSilIdGore(int id);

    }
}