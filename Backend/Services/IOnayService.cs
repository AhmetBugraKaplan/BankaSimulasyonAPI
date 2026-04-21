using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IOnayService
    {
        ApiResponse<object> OnayKoduDogruMu(string kod, string telefonNumara);
    }
}