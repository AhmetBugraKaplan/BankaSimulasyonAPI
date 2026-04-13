

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

        public HesapService(IHesapRepository hesapRepository, IAtmService atmService, IMusteriRepository kullaniciRepository,AppDbContext context)
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

            if (musteriHesapListesi.IsNullOrEmpty())
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

        public ApiResponse<int> BaskasininHesabinaHavaleYap(
            string gonderenHesapNumara, string aliciHesapNumara, decimal gonderilenTutar, string kartNumara)
        {
            ApiResponse<int> BaskasininHesabinaHavaleYapApiResponse = new();

            //Başkasının hesabına para göndermeden önce birkaç kontrol sağlamamız gerekiyor 

            int aliciVarMi = _hesapRepository.HesapVarMi(aliciHesapNumara);

            if (aliciVarMi == 0)
            {
                BaskasininHesabinaHavaleYapApiResponse.IslemBasariliMi = false;
                BaskasininHesabinaHavaleYapApiResponse.Mesaj = "Girdiğiniz hesap numarasına ait hesap bulunamadı";

                return BaskasininHesabinaHavaleYapApiResponse;
            }

            int limitYeterliMi = _hesapRepository.HesapLimitYeterliMi(gonderenHesapNumara, gonderilenTutar);

            if (limitYeterliMi == 0)
            {
                BaskasininHesabinaHavaleYapApiResponse.IslemBasariliMi = false;
                BaskasininHesabinaHavaleYapApiResponse.Mesaj = "Hesabınızda yeterli bakiye bulunmamakta";

                return BaskasininHesabinaHavaleYapApiResponse;
            }

            // Kontrollerimizi sağladık şimdi transaction başlatıyoruz.
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {     //İlk işlem olarak parayı gönderen kişinin bakiyesinden tutarı düşücez. Tutarı düşmek için değeri - olarak gönderiyoruz.
                    _hesapRepository.HesapBakiyeGuncelle(gonderenHesapNumara,-gonderilenTutar);

                    //İkinci işlem olarak parayı alan hesabın hesap bakiyeisni arttırıyoruz
                    _hesapRepository.HesapBakiyeGuncelle(aliciHesapNumara,gonderilenTutar);

                    transaction.Commit();

                    BaskasininHesabinaHavaleYapApiResponse.IslemBasariliMi = true;
                    BaskasininHesabinaHavaleYapApiResponse.Mesaj = "Başkasının hesabına para yatırma işlemi başarıyla gerçekleşti";
                    return BaskasininHesabinaHavaleYapApiResponse;
                }
                catch(Exception)
                {
                    transaction.Rollback();

                    BaskasininHesabinaHavaleYapApiResponse.IslemBasariliMi = false;
                    BaskasininHesabinaHavaleYapApiResponse.Mesaj = "Başkasının hesabına para yatırma işlemi sırasında bir hata ile karşılaşıldı.";
                    return BaskasininHesabinaHavaleYapApiResponse;
                }
            }
        }

       






    }
}

