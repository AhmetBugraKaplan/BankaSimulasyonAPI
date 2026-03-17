using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;

namespace BankaSimulasyon.Services
{
    public class MusteriServis : IMusteriService
    {

        private readonly IMusteriRepository _kullaniciRepository;
        public MusteriServis(IMusteriRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        public ApiResponse<object> YeniMusteriEkle(string isim, string soyisim)
        {
            ApiResponse<object> apiResponse = new();

            //string hashlenmisSfire = BCrypt.Net.BCrypt.HashPassword(sifre);

            int sonuc = _kullaniciRepository.YeniMusteriEkle(isim, soyisim);

            if (sonuc > 0)
            {
                apiResponse.IslemBasariliMi = true;
                apiResponse.Mesaj = "Kullanici başarıyla eklendi";
            }
            else
            {
                apiResponse.IslemBasariliMi = false;
                apiResponse.Mesaj = "Kullanıcı ekleme hatası";
            }

            return apiResponse;
        }


        public ApiResponse<Musteri> MusteriGetirIdGore(int id)
        {
            ApiResponse<Musteri> apiResponse = new();

            var musteri = _kullaniciRepository.MusteriGetirIdGore(id);

            if (musteri != null)
            {
                apiResponse.IslemBasariliMi = true;
                apiResponse.Mesaj = "Muşteri bulundu";
                apiResponse.Data = musteri;
            }
            else
            {
                apiResponse.IslemBasariliMi = false;
                apiResponse.Mesaj = "Müşteri bulunamadı";

            }

            return apiResponse;
        }



        public ApiResponse<object> MusteriSilIdGore(int id)
        {
            ApiResponse<object>  kullaniciResponse = new();

            int sonuc = _kullaniciRepository.MusteriSilIdGore(id);

            if (sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Kullanıcı silindi";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kullanıcı silinirken bir hata meydana geldi";
            }
            return kullaniciResponse;
        }



        /*
        public ApiResponse MusteriHesapEkle(int kullaniciId)
        {
            ApiResponse kullaniciResponse = new();

            var kullanici = _kullaniciRepository.MusteriGetirIdGore(kullaniciId);

            if (kullanici == null)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kullanıcı bulunamadı";
                return kullaniciResponse;
            }

            Random random = new Random();
            int hesapNumarasi = random.Next(100000, 999999);


            int sonuc = _kullaniciRepository.MusteriHesapEkle(kullaniciId, hesapNumarasi, 0);

            if (sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = $"Hesap oluşturuldu. Hesap numaranız: {hesapNumarasi}";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Hesap oluşturulurken hata oluştu";
            }

            return kullaniciResponse;
        }
        */


    }
}