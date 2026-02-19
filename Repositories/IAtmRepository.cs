using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IAtmRepository
    {
        Task<List<ATM>> TumAtmleriGetirAsync();
        Task<List<ATM>> AtmleriGetirKonumaGoreAsync(string konum);
        Task<List<ATM>> AtmleriGetirAktifligeGoreAsync(bool aktifMi);
        Task<int> AtmEkleAsync(string konum,bool aktifMi);


    }
}