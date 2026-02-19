using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;

        public KullaniciController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        [HttpPost("KullaniciEkle")]
        public async Task<IActionResult> YeniKullaniciEkle(string isim, string soyisim, string telefonNumarasi, string adres, string cinsiyet)
        {

            var sonuc = _kullaniciService.yeniKullaniciEkle(isim, soyisim, telefonNumarasi, adres, cinsiyet);

            return Ok(sonuc);
        }

        [HttpPost("KullaniciGetirIdGore")]
        public async Task<IActionResult> KullaniciGetirIdGore(int id)
        {
            var sonuc = _kullaniciService.kullaniciGetirIdGore(id); 

            return Ok(sonuc);
        }

        [HttpPost("KullaniciSilIdGore")]
        public async Task<IActionResult> KullaniciSilIdGore(int id)
        {
            var sonuc = _kullaniciService.kullaniciSilIdGore(id);

            return Ok(sonuc);
        }






    }
}