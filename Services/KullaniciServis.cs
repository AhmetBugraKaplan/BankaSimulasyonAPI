using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Responses;
using BankaSimulasyon.Repositories;

namespace BankaSimulasyon.Services
{
    public class KullaniciServis: IKullaniciService
    {
        //En son kullanici servisinin interfacesını hazırladık reposu zxaten hazır burdan kullanici servisinin kendisini yazıp yeni bir 
        // kontroller ekleyip o kontrollere bağlayacağız usterı ıslemlerı oluşturulan yenı kontrollerde olacak
        private readonly IKullaniciRepository _kullaniciRepository;
        public KullaniciServis(IKullaniciRepository kullaniciRepository)
        {   
            _kullaniciRepository = kullaniciRepository;
        }

        public async Task<KullaniciResponse> yeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet)
        {
            KullaniciResponse kullaniciResponse = new();
            
            int sonuc = await _kullaniciRepository.yeniKullaniciEkleAsync(isim,soyisim,telefonNumarasi,adres,cinsiyet);

            if(sonuc > 0)
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

            if(sonuc > 0)
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

        
    
    }
}