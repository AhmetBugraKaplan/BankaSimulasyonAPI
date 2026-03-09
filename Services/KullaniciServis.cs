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

        public  KullaniciResponse yeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet, string email, string sifre, string kullaniciRol)
        {
            KullaniciResponse kullaniciResponse = new();

            string hashlenmisSfire = BCrypt.Net.BCrypt.HashPassword(sifre);

            int sonuc = _kullaniciRepository.yeniKullaniciEkle(isim, soyisim, telefonNumarasi, adres, cinsiyet, email, hashlenmisSfire, kullaniciRol);

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




        public  Kullanici? kullaniciGetirIdGore(int id)
        {
            return _kullaniciRepository.kullaniciGetirIdGore(id);
        }




        public KullaniciResponse kullaniciSilIdGore(int id)
        {
            KullaniciResponse kullaniciResponse = new();

            int sonuc =  _kullaniciRepository.kullaniciSilIdGore(id);

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




        public  KullaniciResponse kullaniciHesapEkle(int kullaniciId)
        {
            KullaniciResponse kullaniciResponse = new();

            var kullanici = _kullaniciRepository.kullaniciGetirIdGore(kullaniciId);

            if (kullanici == null)
            {
                kullaniciResponse.IslemBasariliMi = false;
                kullaniciResponse.Mesaj = "Kullanıcı bulunamadı";
                return kullaniciResponse;
            }

            Random random = new Random();
            int hesapNumarasi = random.Next(100000, 999999);


            int sonuc = _kullaniciRepository.kullaniciHesapEkle(kullaniciId,hesapNumarasi,0);

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