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
        List<ATM> TumAtmleriGetir();
        List<ATM> AtmleriGetirKonumaGore(string konum);
        List<ATM> AtmleriGetirAktifligeGore(bool aktifMi);
        int AtmEkle(string konum, bool aktifMi);


    }
}