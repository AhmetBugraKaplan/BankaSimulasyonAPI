

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
        private readonly ISmsService _smsService;

        public HesapService(IHesapRepository hesapRepository,
                            IAtmService atmService, IMusteriRepository kullaniciRepository, AppDbContext context, ISmsService smsService)
        {
            _hesapRepository = hesapRepository;
            _atmService = atmService;
            _kullaniciRepository = kullaniciRepository;
            _context = context;
            _smsService = smsService;
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
            string gonderenHesapNumara, string aliciHesapNumara, decimal gonderilenTutar, string kartNumara,int atmID)
        {
            ApiResponse<int> HavaleYapApiResponse = new();

            //Başkasının hesabına para göndermeden önce birkaç kontrol sağlamamız gerekiyor 
            //HesapVarMi girilen hesap numarasına ait hespa olup olmadığını kontrol eden bir yapı.
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

                    // Islem Geçmişi için gerekli parametreleri oluşturuyoruz.
                    decimal gonderenIslemSonrasiBakiye = _hesapRepository.HesapBakiyeGetir(gonderenHesapNumara);
                    decimal aliciIslemSonrasiBakiye = _hesapRepository.HesapBakiyeGetir(aliciHesapNumara);
                    //İşlem geçmişini kaydediyoruz.
                    _hesapRepository.IslemGecmisiEkleCiftTarafli(gonderenHesapNumara, aliciHesapNumara, "Havale", gonderilenTutar,
                    gonderenIslemSonrasiBakiye, aliciIslemSonrasiBakiye,atmID,"","");

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

        public ApiResponse<bool> HesapVarMiTelNoIle(string telefonNumara)
        {
            ApiResponse<bool> response = new();
            int varMi = _hesapRepository.HesapVarMiTelNoIle(telefonNumara);
            response.IslemBasariliMi = varMi > 0;
            response.Mesaj = varMi > 0 ? "Hesap bulundu." : "Hesap bulunamadı.";
            response.Data = true;

            return response;
        }

        public ApiResponse<int> HesabaKartsizParaGonder(string hesapNumara, decimal gonderilecekTutar)
        {
            ApiResponse<int> response = new();

            try
            {
                int sonuc = _hesapRepository.HesabaKartsizParaGonder(hesapNumara, gonderilecekTutar);

                if (sonuc == 1)
                {   
                    decimal guncelBakiye = _hesapRepository.HesapBakiyeGetir(hesapNumara);
                    _hesapRepository.IslemGecmisiEkleCiftTarafli("",hesapNumara,"Kartsiz Para Gönderme",gonderilecekTutar,0,guncelBakiye,7,
                    "Kartısz Para Gönderdiniz","Hesabınıza Kartsız Para Gönderildi.");
                    response.IslemBasariliMi = true;
                    response.Data = sonuc;
                    response.Mesaj = "Para transferi başarıyla gerçekleşti.";
                }
                else if (sonuc == 0)
                {
                    response.IslemBasariliMi = false;
                    response.Mesaj = "Alıcı hesap bulunamadı.";
                }
                else
                {
                    response.IslemBasariliMi = false;
                    response.Mesaj = "Transfer sırasında bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                response.IslemBasariliMi = false;
                response.Mesaj = "Beklenmedik bir hata oluştu: " + ex.Message;
            }

            return response;
        }

        public ApiResponse<object> CebeParaGonder(string gonderenKartNo, string aliciTckNO, string aliciTelNo, decimal gonderilenTutar)
        {
            ApiResponse<object> CebeParaGonderApiResponse = new();

            // 1. Validation - Tutar
            if (gonderilenTutar <= 0)
            {
                CebeParaGonderApiResponse.IslemBasariliMi = false;
                CebeParaGonderApiResponse.Mesaj = "Gönderilecek tutar sıfırdan büyük olmalıdır.";
                return CebeParaGonderApiResponse;
            }

            if (gonderilenTutar > 5000)
            {
                CebeParaGonderApiResponse.IslemBasariliMi = false;
                CebeParaGonderApiResponse.Mesaj = "Tek seferde en fazla 5.000 ₺ gönderebilirsiniz.";
                return CebeParaGonderApiResponse;
            }

            // 2. Gönderen kartına ait hesabı bul
            List<Hesap> gonderenHesaplari = _hesapRepository.MusterininTumHesaplariniGetir(gonderenKartNo);

            if (gonderenHesaplari == null || gonderenHesaplari.Count == 0)
            {
                CebeParaGonderApiResponse.IslemBasariliMi = false;
                CebeParaGonderApiResponse.Mesaj = "Gönderen hesap bulunamadı.";
                return CebeParaGonderApiResponse;
            }

            string gonderenHesapNo = gonderenHesaplari.First().HesapNumara;



            // 3. SP çağır (bakiye düş + bekleyen kayıt)
            CebeSpResponse spSonuc = _hesapRepository.CebeParaGonder(
                gonderenHesapNo, aliciTckNO, aliciTelNo, gonderilenTutar);

            if (spSonuc.Sonuc == 0)
            {
                CebeParaGonderApiResponse.IslemBasariliMi = false;
                CebeParaGonderApiResponse.Mesaj = spSonuc.Mesaj;
                return CebeParaGonderApiResponse;
            }

            decimal gonderenHesapGuncelBakiye = _hesapRepository.HesapBakiyeGetir(gonderenHesapNo);
            
            _hesapRepository.IslemGecmisiEkleCiftTarafli(gonderenHesapNo,"","Cebe Para Al/Gönder",gonderilenTutar,gonderenHesapGuncelBakiye,gonderilenTutar,7,"Cebe para gönderildi.","Cebe para geldi.");

            // 4. Başarılı response
            CebeParaGonderApiResponse.IslemBasariliMi = true;
            CebeParaGonderApiResponse.Mesaj = "Para gönderme işlemi başarılı. Alıcı 3 gün içinde parayı çekebilir.";
            return CebeParaGonderApiResponse;
        }


        public ApiResponse<object> CebeParaCek(string aliciTckNO, string aliciTelNo, string gonderenTelNo, decimal tutar)
        {
            ApiResponse<object> CebeParaCekApiResponse = new();


            if (tutar <= 0)
            {
                CebeParaCekApiResponse.IslemBasariliMi = false;
                CebeParaCekApiResponse.Mesaj = "Tutar sıfırdan büyük olmalıdır.";
                return CebeParaCekApiResponse;
            }

            // 2. SP çağır (eşleşme + süre + Durum güncelle)
            CebeSpResponse spSonuc = _hesapRepository.CebeParaCek(
                aliciTckNO, aliciTelNo, gonderenTelNo, tutar);

            if (spSonuc.Sonuc == 0)
            {
                CebeParaCekApiResponse.IslemBasariliMi = false;
                CebeParaCekApiResponse.Mesaj = spSonuc.Mesaj;
                return CebeParaCekApiResponse;
            }

            // 3. Başarılı response
            CebeParaCekApiResponse.IslemBasariliMi = true;
            CebeParaCekApiResponse.Mesaj = spSonuc.Mesaj;
            return CebeParaCekApiResponse;
        }









    }
}

