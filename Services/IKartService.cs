using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IKartService
    {
        public Task<KullaniciResponse> KartEkle(int kullaniciHesapId, string KartNumara, string KartSKT, string CVV,string KartTipi,bool AktifMi);

    }
}