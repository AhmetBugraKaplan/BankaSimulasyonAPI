using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankaSimulasyon.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly IHesapServis _hesapServis;


        public KullaniciController(IKullaniciService kullaniciService, IHesapServis hesapServis)
        {
            _kullaniciService = kullaniciService;
            _hesapServis = hesapServis;
        }


        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("KullaniciEkle")]
        public  IActionResult YeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet, string email, string sifre, string kullaniciRol)
        {

            var sonuc = _kullaniciService.yeniKullaniciEkle(isim, soyisim, telefonNumarasi, adres, cinsiyet, email, sifre, kullaniciRol);

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("KullaniciGetirIdGore")]
        public IActionResult KullaniciGetirIdGore(int id)
        {
            var sonuc =  _kullaniciService.kullaniciGetirIdGore(id);

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("KullaniciSilIdGore")]
        public  IActionResult KullaniciSilIdGore(int id)
        {
            var sonuc =  _kullaniciService.kullaniciSilIdGore(id);

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("KullaniciHesaptanParaCek")]
        public  IActionResult KullaniciHesaptanParaCek(int hesapNumarasi, int atmId, int cekilecekTutar)
        {
            var sonuc = _hesapServis.ParaCek(hesapNumarasi, atmId, cekilecekTutar);

            return Ok(sonuc);
        }


        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("KullaniciTestHata")]
        public IActionResult KullaniciTestHata()
        {
            throw new Exception("Bu bir test hatasıdır!");
        }


        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("HesapEkle")]
        public  IActionResult HesapEkle()
        {

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var sonuc =  _kullaniciService.kullaniciHesapEkle(kullaniciId);

            return Ok(sonuc);
        }


        [HttpPost("KullaniciHesapLimitGuncelle")]
        public IActionResult HesapLimitGuncelle(int kullaniciHesapId,decimal kullaniciHesapLimit)
        {
            var sonuc =  _hesapServis.HesapLimitGuncelle(kullaniciHesapId,kullaniciHesapLimit);

            return Ok(sonuc);
        }






    }
}