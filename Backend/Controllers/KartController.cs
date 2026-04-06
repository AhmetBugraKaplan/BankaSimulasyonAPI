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
using BankaSimulasyon.Models.Requests;
using BankaSimulasyon.Models.Dtos.Requests;
using Backend.Models.Requests.KartRequest;


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
        public IActionResult KartEkle([FromBody] KartEkleRequest request)
        {
            //Aşşağıdaki if bloğu requestteki requried içerisindekiş şartlar sağlanmayınca patlıyor ve errormesage'ı döndürüyor.
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _kartService.KartEkle(
                request.KullaniciId,
                 request.KartNumara,
                 request.KartGunlukLimit,
                 request.KartSifre
                 );

            return Ok(sonuc);
        }

        [HttpPost("KartSifreGuncelle")]
        public IActionResult KartSifreGuncelle([FromBody] KartSifreGuncelleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _kartService.KartSifreGuncelle(
                request.YeniKartSifre,
                request.KartNumara
                );

            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("ParaCek")]
        public IActionResult ParaCek([FromBody] ParaCekRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _kartService.ParaCek(
                request.KartNumara,
                request.AtmId,
                request.CekilecekTutar
            );

            return Ok(sonuc);
        }


        [AllowAnonymous]
        [HttpPost("KartDogrula")]
        public IActionResult KartDogrula([FromBody] KartDogrulaRequest request)
        {
            //Aşşağıdaki if bloğu requestteki requried içerisindekiş şartlar sağlanmayınca patlıyor ve errormesage'ı döndürüyor.
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var ipAdresi = Request.Headers["X-Forwarded-For"].FirstOrDefault()
               ?? HttpContext.Connection.RemoteIpAddress?.ToString()
               ?? "bilinmiyor";

            var sonuc = _kartService.KartDogrula(
                request.KartNumara,
                request.KartSifre,
                request.AtmId,
                ipAdresi);

            return Ok(sonuc);
        }




        [HttpPost("KartGunlukLimitGuncelle")]
        public IActionResult KartGunlukLimitGuncelle([FromBody] KartGunlukLimitGuncelleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _kartService.KartGunlukLimitGuncelle(
                request.KartNumara,
                request.YeniKartLimit,
                request.MusteriId
            );

            return Ok(sonuc);
        }

        [Authorize]
        [HttpPost("KartKalanLimitGetir")]
        public IActionResult KartKalanLimitGetir([FromBody] KartKalanLimitGetir request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _kartService.KartKalanLimitGetir(request.KartNumara);

            return Ok(sonuc);
        }








    }
}
