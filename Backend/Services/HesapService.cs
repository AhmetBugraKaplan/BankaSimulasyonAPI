

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Data;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;
using Microsoft.IdentityModel.Tokens;


namespace BankaSimulasyon.Services
{
    public class HesapService : IHesapServis
    {
        private readonly IHesapRepository _hesapRepository;
        private readonly IMusteriRepository _kullaniciRepository;
        private readonly IAtmService _atmService;
        private readonly AppDbContext _context;

        public HesapService(IHesapRepository hesapRepository, IAtmService atmService, IMusteriRepository kullaniciRepository, AppDbContext context)
        {
            _hesapRepository = hesapRepository;
            _atmService = atmService;
            _kullaniciRepository = kullaniciRepository;
            _context = context;
        }

        public ApiResponse<List<Hesap>> MusterininTumHesaplariniGetir(string kartNumara)
        {
            ApiResponse<List<Hesap>> MusterininTumHesaplariniGetirApiResponse = new();
            List<Hesap> musteriHesapListesi = _hesapRepository.MusterininTumHesaplariniGetir(kartNumara);

            if (musteriHesapListesi == null || musteriHesapListesi.Count == 0)
            {
                MusterininTumHesaplariniGetirApiResponse.Data = musteriHesapListesi;
                MusterininTumHesaplariniGetirApiResponse.IslemBasariliMi = false;
                MusterininTumHesaplariniGetirApiResponse.Mesaj = "Müşteriye ait hesap bulunamadı.";
                return MusterininTumHesaplariniGetirApiResponse;
            }
            else
            {
                MusterininTumHesaplariniGetirApiResponse.Data = musteriHesapListesi;
                MusterininTumHesaplariniGetirApiResponse.IslemBasariliMi = true;
                MusterininTumHesaplariniGetirApiResponse.Mesaj = "Müşteriye ait hesaplar başarıyla listelendi.";
                return MusterininTumHesaplariniGetirApiResponse;
            }
        }

        public ApiResponse<int> HavaleYap(
            string gonderenHesapNumara, string aliciHesapNumara, decimal gonderilenTutar, string kartNumara)
        {
            ApiResponse<int> HavaleYapApiResponse = new();

            //Başkasının hesabına para göndermeden önce birkaç kontrol sağlamamız gerekiyor 

            int aliciVarMi = _hesapRepository.HesapVarMi(aliciHesapNumara);

            if (aliciVarMi == 0)
            {
                HavaleYapApiResponse.IslemBasariliMi = false;
                HavaleYapApiResponse.Mesaj = "Girdiğiniz hesap numarasına ait hesap bulunamadı";

                return HavaleYapApiResponse;
            }

            int limitYeterliMi = _hesapRepository.HesapLimitYeterliMi(gonderenHesapNumara, gonderilenTutar);

            if (limitYeterliMi == 0)
            {
                HavaleYapApiResponse.IslemBasariliMi = false;
                HavaleYapApiResponse.Mesaj = "Hesabınızda yeterli bakiye bulunmamakta";

                return HavaleYapApiResponse;
            }

            // Kontrollerimizi sağladık şimdi transaction başlatıyoruz.
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {     //İlk işlem olarak parayı gönderen kişinin bakiyesinden tutarı düşücez. Tutarı düşmek için değeri - olarak gönderiyoruz.
                    _hesapRepository.HesapBakiyeGuncelle(gonderenHesapNumara, -gonderilenTutar);

                    //İkinci işlem olarak parayı alan hesabın hesap bakiyeisni arttırıyoruz
                    _hesapRepository.HesapBakiyeGuncelle(aliciHesapNumara, gonderilenTutar);

                    transaction.Commit();

                    HavaleYapApiResponse.IslemBasariliMi = true;
                    HavaleYapApiResponse.Mesaj = "Başkasının hesabına para yatırma işlemi başarıyla gerçekleşti";
                    return HavaleYapApiResponse;
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    HavaleYapApiResponse.IslemBasariliMi = false;
                    HavaleYapApiResponse.Mesaj = "Başkasının hesabına para yatırma işlemi sırasında bir hata ile karşılaşıldı.";
                    return HavaleYapApiResponse;
                }
            }
        }

        public ApiResponse<bool> HesapVarMi(string hesapNumara)
        {
            ApiResponse<bool> response = new();
            int varMi = _hesapRepository.HesapVarMi(hesapNumara);
            response.IslemBasariliMi = varMi > 0;
            response.Mesaj = varMi > 0 ? "Hesap bulundu." : "Hesap bulunamadı.";
            return response;
        }








    }
}

