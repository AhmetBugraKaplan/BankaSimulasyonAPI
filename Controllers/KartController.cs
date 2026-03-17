using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KartController : ControllerBase
    {

        private readonly IKartService _kartService;

        public KartController(IKartService kartService)
        {
            _kartService = kartService;
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("KartEkle")]
        public IActionResult KartEkle(int kullaniciId, string kartNumara, decimal kartGunlukLimit, string kartSifre)
        {
            var sonuc = _kartService.KartEkle(kullaniciId, kartNumara, kartGunlukLimit, kartSifre);

            return Ok(sonuc);
        }

        /*
        [HttpPost("KartLimitGuncelle")]
        public IActionResult KartLimitGuncelle(int kullaniciId,string kartNumara,decimal kartLimit)
        {
            var sonuc = _kartService.KartLimitGuncelle(kullaniciId,kartNumara,kartLimit);

            return Ok(sonuc);
        }
        */


        [HttpPost("KartSifreGuncelle")]
        public IActionResult KartSifreGuncelle(string YeniKartSifre, string kartNumara)
        {
            var sonuc = _kartService.KartSifreGuncelle(YeniKartSifre, kartNumara);

            return Ok(sonuc);
        }

        [HttpPost("ParaÇek")]
        public IActionResult KarttanParaCek(string kartNumara, int atmId, int cekilecekTutar, int kullaniciId)
        {
            var sonuc = _kartService.ParaCek(kartNumara,atmId,cekilecekTutar,kullaniciId);

            return Ok(sonuc);
        }

        [HttpPost("KartDogrula")]
        public IActionResult KartDogrula(string kartNumara,string kartSifre)
        {
            var sonuc = _kartService.KartDogrula(kartNumara,kartSifre);
            return Ok(sonuc);
        }






    }
}
