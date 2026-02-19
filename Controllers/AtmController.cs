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
    public class AtmController : ControllerBase
    {
        private readonly IAtmService _atmService;

        public AtmController(IAtmService atmService)
        {
            _atmService = atmService;
        }


        [HttpGet("atmdeNeKadarVar")]
        public async Task<IActionResult> AtmdeNeKadarParaVar(int atmId)
        {
            var sonuc = await _atmService.AtmdekiToplamParayiIdIleGetirAsync(atmId);

            if (sonuc != 0)
            {
                return Ok(sonuc);
            }
            else
            {
                return Ok("Girilen atmId'e ait atm bulunamadı");
            }


        }


        [HttpPost("atmKasetKupurleriGuncelle")]
        public async Task<IActionResult> AtmKasetdekiKupurleriGuncelle(int atmId, int slotNumarasi, int adet, int kupur)
        {
            var sonuc = await _atmService.AtmKasetlerdekiKupurleriGuncelleAsync(atmId, slotNumarasi, adet, kupur);


            return Ok(sonuc);
        }

        [HttpPost("atmdenParaCek")]
        public async Task<IActionResult> AtmdenParaCek(int atmId, int cekilecekTutar)
        {
            var sonuc = await _atmService.AtmdenParaCekAsync(atmId,cekilecekTutar);

            return Ok(sonuc); 
        }

        [HttpPost("atmEkle")]

        public async Task<IActionResult> AtmEkle(string konum,bool aktifMi)
        {
            var sonuc = await _atmService.AtmEkleAsync(konum,aktifMi);

            return Ok(sonuc);
        }

        [HttpPost("atmleriListeseAktifligeGore")]
        public async Task<IActionResult> aktifligeGoreAtmListele(bool aktifMi)
        {   
            var sonuc = _atmService.AtmleriGetirAktifligeGoreAsync(aktifMi);

            return Ok(sonuc);
        }

        [HttpGet("tumAtmleriGetir")]
        public async Task<IActionResult> tumAtmleriGetir()
        {   
            var sonuc = _atmService.TumAtmleriGetirAsync();

            return Ok(sonuc);
        }
    }
}