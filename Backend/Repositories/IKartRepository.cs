using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Repositories
{
    public interface IKartRepository
    {
        public int KartEkle(int musteriId, string kartNumara, decimal kartGunlukLimit, string kartSifre);
        public int KartKalanLimitGuncelle(string kartNumara, decimal yeniKartLimit);
        public int KartGunlukLimitGuncelle(string kartNumara, decimal yeniKartLimit);
        public decimal KartKalanLimitGetir(string kartNumara);
        public decimal KartGunlukLimitGetir(string kartNumara);
        public List<Kart> TumKartlariGetir(int kullaniciId);
        public int KartSifreGuncelle(string yeniKartSifre, string kartNumara);
        public string? KartSifreGetir(string kartNumara);
        public void YanlisGirisSayisiniArttir(string kartNumara);
        public int YanlisGirisSayisiGetir(string kartNumara);
        public void YanlisGirisSayisiSifirla(string kartNumara);
        public void TumKartlarinLimitleriniSifirla();
        public bool AyniNumaradaKartVarMi(string kartNumara);
        public DateOnly SonIslemTarihiGetir(string kartNumara);
        public void SonIslemTarihiniBugunYap(string kartNumara);
        public int KartKalanLimitSifirla(string kartNumara);
        int KartNumaraIleMusteriIdGetir(string kartNumara);




    }
}