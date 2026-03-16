using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult KartEkle(int kullaniciId, string KartNumara, string KartSKT, string CVV, string KartTipi, bool AktifMi,string KartSifre)
        {
            var sonuc = _kartService.KartEkle(kullaniciId, KartNumara, KartSKT, CVV, KartTipi, AktifMi,KartSifre);

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
        public IActionResult KartSifreGuncelle(string YeniKartSifre, int kartId)
        {
            var sonuc = _kartService.KartSifreGuncelle(YeniKartSifre, kartId);

            return Ok(sonuc);
        }

        




    }
}
