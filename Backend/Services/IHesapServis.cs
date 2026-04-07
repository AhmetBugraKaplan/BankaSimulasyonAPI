
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IHesapServis
    {
       public ApiResponse<List<Hesap>> MusterininTumHesaplariniGetir(string kartNumara);

    }
}

