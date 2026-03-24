using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Dtos.Requests.ATM;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtmController : ControllerBase
    {
        private readonly IAtmService _atmService;

        public AtmController(IAtmService atmService)
        {
            _atmService = atmService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("atmEkle")]
        public IActionResult AtmEkle([FromBody] AtmEkleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _atmService.AtmEkle(request.Konum, request.AktifMi);
            return Ok(sonuc);
        }

        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("atmKasetKupurleriGuncelle")]
        public IActionResult AtmKasetGuncelle([FromBody] AtmKasetGuncelleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _atmService.AtmKasetlerdekiKupurleriGuncelle(
                request.AtmId,
                request.SlotNumarasi,
                request.Adet,
                request.Kupur
            );
            return Ok(sonuc);
        }

        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("atmdenParaCek")]
        public IActionResult AtmdenParaCek([FromBody] AtmdenParaCekRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _atmService.AtmdenParaCek(
                request.AtmId,
                request.CekilecekTutar,
                request.KartNumara
            );
            return Ok(sonuc);
        }

        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("tumAtmleriGetir")]
        public IActionResult TumAtmleriGetir()
        {
            var sonuc = _atmService.TumAtmleriGetir();
            return Ok(sonuc);
        }

        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("{atmId}/atmdeNeKadarVar")]
        public IActionResult AtmdeNeKadarParaVar(int atmId)
        {
            var sonuc = _atmService.AtmdekiToplamParayiIdIleGetir(atmId);
            return Ok(sonuc);
        }
    }
}