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
    public class KartController : ControllerBase
    {

        private readonly IKartService _kartService;

        public KartController(IKartService kartService)
        {
            _kartService = kartService;
        }


        [HttpPost("KartEkle")]
        public async Task<IActionResult> KartEkle(int kullaniciHesapId, string KartNumara, string KartSKT, string CVV,string KartTipi,bool AktifMi)
        {
            var sonuc = _kartService.KartEkle(kullaniciHesapId,KartNumara,KartSKT,CVV,KartTipi,AktifMi);

            return Ok(sonuc);
        }




    }
}
