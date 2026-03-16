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
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;


        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }


        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("MusteriEkle")]
        public IActionResult YeniMusteriEkle(string isim, string soyisim)
        {
            var sonuc = _musteriService.YeniMusteriEkle(isim, soyisim);

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("MusteriGetirIdGore")]
        public IActionResult MusteriGetirIdGore(int id)
        {
            var sonuc = _musteriService.MusteriGetirIdGore(id);

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("MusteriSilIdGore")]
        public IActionResult MusteriSilIdGore(int id)
        {
            var sonuc = _musteriService.MusteriSilIdGore(id);

            return Ok(sonuc);
        }


        /*
        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("MusteriHesaptanParaCek")]
        public  IActionResult MusteriHesaptanParaCek(int hesapNumarasi, int atmId, int cekilecekTutar,string kartNumara,int MusteriId)
        {
            var sonuc = _hesapServis.ParaCek(hesapNumarasi, atmId, cekilecekTutar,kartNumara,MusteriId);

            return Ok(sonuc);
        }
        */


        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("MusteriTestHata")]
        public IActionResult MusteriTestHata()
        {
            throw new Exception("Bu bir test hatasıdır!");
        }


        /*
        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("HesapEkle")]
        public IActionResult HesapEkle()
        {

            var MusteriId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var sonuc = _musteriService.MusteriHesapEkle(MusteriId);

            return Ok(sonuc);
        }
        */

    }
}