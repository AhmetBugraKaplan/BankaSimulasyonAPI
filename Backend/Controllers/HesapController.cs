using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Requests.HesapRequest;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HesapController : ControllerBase
    {
        private readonly IHesapServis _hesapService;
        public HesapController(IHesapServis hesapServis)
        {
            _hesapService = hesapServis;
        }

        [HttpPost("MusteriTumHesaplariGetir")]
        public IActionResult MusteriTumHesaplariGetir([FromBody] MusteriTumHesaplariGetirRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.MusterininTumHesaplariniGetir(
                request.KartNumara
            );

            return Ok(sonuc);
        }
    }
}