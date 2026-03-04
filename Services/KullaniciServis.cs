using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;

namespace BankaSimulasyon.Services
{
    public class KullaniciServis : IKullaniciService
    {

        private readonly IKullaniciRepository _kullaniciRepository;
        public KullaniciServis(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        public async Task<KullaniciResponse> yeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet, string email, string sifre, string kullaniciRol)
        {
            KullaniciResponse kullaniciResponse = new();

            string hashlenmisSfire = BCrypt.Net.BCrypt.HashPassword(sifre);

            int sonuc = await _kullaniciRepository.yeniKullaniciEkleAsync(isim, soyisim, telefonNumarasi, adres, cinsiyet, email, hashlenmisSfire, kullaniciRol);

            if (sonuc > 0)
            {
                kullaniciResponse.IslemBasariliMi = true;
                kullaniciResponse.Mesaj = "Kullanici başarıyla eklendi";
            }
            else
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kullanıcı ekleme hatası";
            }

            return kullaniciResponse;
        }




        public async Task<Kullanici?> kullaniciGetirIdGore(int id)
        {
            return await _kullaniciRepository.kullaniciGetirIdGore(id);
        }




        public async Task<KullaniciResponse> kullaniciSilIdGore(int id)
        {
            KullaniciResponse kullaniciResponse = new();

            int sonuc = await _kullaniciRepository.kullaniciSilIdGore(id);

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




        public async Task<KullaniciResponse> kullaniciHesapEkle(int kullaniciId, string hesapsifresi)
        {
            KullaniciResponse kullaniciResponse = new();

            var kullanici = await _kullaniciRepository.kullaniciGetirIdGore(kullaniciId);

            if (kullanici == null)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kullanıcı bulunamadı";
                return kullaniciResponse;
            }

            Random random = new Random();
            int hesapNumarasi = random.Next(100000, 999999);


            int sonuc = await _kullaniciRepository.kullaniciHesapEkle(kullaniciId,hesapNumarasi,0,hesapsifresi);

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



    }
}