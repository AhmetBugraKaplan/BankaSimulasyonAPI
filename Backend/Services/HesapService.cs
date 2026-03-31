/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;


namespace BankaSimulasyon.Services
{
    public class HesapService : IHesapServis
    {
        private readonly IHesapRepository _hesapRepository;
        private readonly IMusteriRepository _kullaniciRepository;
        private readonly IAtmService _atmService;

        public HesapService(IHesapRepository hesapRepository, IAtmService atmService, IMusteriRepository kullaniciRepository)
        {
            _hesapRepository = hesapRepository;
            _atmService = atmService;
            _kullaniciRepository = kullaniciRepository;
        }



        public bool hesaptaYeterinceParaVarmi(KullaniciHesap hesap, int tutar)
        {
            return hesap.Bakiye >= tutar;
        }



        public  ApiResponse ParaCek(int hesapNumarasi,int atmId, int cekilecekTutar,string kartNumara,int kullaniciId)
        {

            ApiResponse kullaniciResponse = new();

            var hesap = _hesapRepository.kullanicininHessabiniBul(hesapNumarasi);
            if (hesap == null)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Hesap bulunamadi";
                return kullaniciResponse;
            }




            if (!hesaptaYeterinceParaVarmi(hesap, cekilecekTutar))
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Bakiye yetersiz";
                return kullaniciResponse;
            }

            var atmSonuc =  _atmService.AtmdenParaCek(atmId, cekilecekTutar,kartNumara,kullaniciId);
            if (!atmSonuc.IslemBasariliMi)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = atmSonuc.Mesaj;
                return kullaniciResponse;
            }

            hesap.Bakiye -= cekilecekTutar;
             _hesapRepository.hesapGuncelleAsync(hesap);

            kullaniciResponse.IslemBasariliMi = true;
            kullaniciResponse.Mesaj = "Para basariyla cekildi";
            return kullaniciResponse;
        }
    
    
        public ApiResponse HesapLimitGuncelle(int kullaniciId, decimal kullaniciHesapLimit)
        {
            ApiResponse kullaniciResponse = new ApiResponse();

            int sonuc = _hesapRepository.HesapLimitGuncelle(kullaniciId,kullaniciHesapLimit);

            if(sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Kullanıcı hesap limit başarıyla güncellendi";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Girilen kullanıcı id ye ait kullanıcı bulunamadı";
            }

            return kullaniciResponse;
        }

        
    

    }
}

*/