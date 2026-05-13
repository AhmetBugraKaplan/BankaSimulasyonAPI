using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Requests.HesapRequest;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize]
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

        [Authorize]
        [HttpPost("HavaleYap")]
        public IActionResult HavaleYap([FromBody] HavaleYapRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.HavaleYap(
                request.GonderenHesapNumara,
                request.AliciHesapNumara,
                request.GonderilenTutar,
                request.KartNumara,
                request.AtmId,
                request.KendiHesaplarimArasiMi
            );

            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("HesapVarMi")]
        public IActionResult HesapVarMi([FromBody] HesapVarMiRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.HesapVarMi(request.HesapNumara);
            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("HesapVarMiTelNoIle")]
        public IActionResult HesapVarMiTelNoIle([FromBody] HesapVarMiTelefonNoIleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.HesapVarMiTelNoIle(request.TelefonNumara);
            return Ok(sonuc);
        }

        [AllowAnonymous]
        [HttpPost("HesabaKartsizParaGonder")]
        public IActionResult HesabaKartsizParaGonder([FromBody] HesabaKartsizParaGonderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.HesabaKartsizParaGonder(
                request.HesapNumara,
                request.GonderilecekTutar
            );

            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("CebeParaGonder")]
        public IActionResult CebeParaGonder([FromBody] CebeParaGonderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.CebeParaGonder(
                request.GonderenKartNo,
                request.AliciTckNO,
                request.AliciTelNo,
                request.GonderilenTutar
            );

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [HttpPost("CebeParaCek")]
        public IActionResult CebeParaCek([FromBody] CebeParaCekRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _hesapService.CebeParaCek(
                request.AliciTckNO,
                request.AliciTelNo,
                request.GonderenTelNo,
                request.Tutar
            );

            return Ok(sonuc);
        }








    }
}