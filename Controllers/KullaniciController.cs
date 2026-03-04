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
    [Authorize]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly IHesapServis _hesapServis;


        public KullaniciController(IKullaniciService kullaniciService, IHesapServis hesapServis)
        {
            _kullaniciService = kullaniciService;
            _hesapServis = hesapServis;
        }


        //[AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("KullaniciEkle")]
        public async Task<IActionResult> YeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet, string email, string sifre,string kullaniciRol)
        {

            var sonuc = await _kullaniciService.yeniKullaniciEkle(isim, soyisim, telefonNumarasi, adres, cinsiyet,email,sifre,kullaniciRol);

            return Ok(sonuc);
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("KullaniciGetirIdGore")]
        public async Task<IActionResult> KullaniciGetirIdGore(int id)
        {
            var sonuc = await _kullaniciService.kullaniciGetirIdGore(id);

            return Ok(sonuc);
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("KullaniciSilIdGore")]
        public async Task<IActionResult> KullaniciSilIdGore(int id)
        {
            var sonuc = await _kullaniciService.kullaniciSilIdGore(id);

            return Ok(sonuc);
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("KullaniciHesaptanParaCek")]
        public async Task<IActionResult> KullaniciHesaptanParaCek(int hesapNumarasi, string girilenSifre, int atmId, int cekilecekTutar)
        {
            var sonuc = await _hesapServis.ParaCek(hesapNumarasi, girilenSifre, atmId, cekilecekTutar);

            return Ok(sonuc);
        }


        //[AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("KullaniciTestHata")]
        public IActionResult KullaniciTestHata()
        {
            throw new Exception("Bu bir test hatasıdır!");
        }


        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("HesapEkle")]
        public async Task<IActionResult> HesapEkle(string sifre)
        {

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var sonuc = await _kullaniciService.kullaniciHesapEkle(kullaniciId,sifre);

            return Ok(sonuc);
        }








    }
}