using System;
using System.Collections.Generic;
using System.Linq;
using BankaSimulasyon.Models.Dtos.Requests.Musteri;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;

        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        [Authorize]
        [HttpPost("MusteriEkle")]
        public IActionResult YeniMusteriEkle([FromBody] MusteriEkleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    basarili = false,
                    hatalar = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });

            var sonuc = _musteriService.YeniMusteriEkle(request.Isim, request.Soyisim);
            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("MusteriGetirIdGore")]
        public IActionResult MusteriGetirIdGore([FromBody] MusteriGetirIdGoreRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    basarili = false,
                    hatalar = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });

            var sonuc = _musteriService.MusteriGetirIdGore(request.Id);
            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("MusteriSilIdGore")]
        public IActionResult MusteriSilIdGore([FromBody] MusteriSilIdGoreRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    basarili = false,
                    hatalar = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });

            var sonuc = _musteriService.MusteriSilIdGore(request.Id);
            return Ok(sonuc);
        }

        [AllowAnonymous]
        [HttpGet("MusteriTestHata")]
        public IActionResult MusteriTestHata()
        {
            var sonuc = _musteriService.MusteriGetirIdGore(0);
            return Ok(sonuc);
        }



    }
}