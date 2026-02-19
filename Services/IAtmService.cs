using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;

namespace BankaSimulasyon.Services
{
    public interface IAtmService
    {
        Task<AtmdenParaCekmeResponse> AtmdenParaCekAsync(int atmId, int cekilecekTutar);
        Task<AtmdenParaCekmeResponse> AtmdenParaCekAsync2(int atmId, int cekilecekTutar);

        Task<int> AtmdekiToplamParayiIdIleGetirAsync(int atmId);

        Task<KasetGuncellemeResponse> AtmKasetlerdekiKupurleriGuncelleAsync(int atmId, int slotNumarasi, int adet, int kupur);

        Task<List<ATM>> TumAtmleriGetirAsync();
        Task<AtmEklemeResponse> AtmEkleAsync(string konum, bool aktifMi);
        Task<List<ATM>> AtmleriGetirAktifligeGoreAsync(bool aktifMi);



    }
}