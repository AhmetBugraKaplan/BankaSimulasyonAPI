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
    public class AtmController : ControllerBase
    {
        private readonly IAtmService _atmService;

        public AtmController(IAtmService atmService)
        {
            _atmService = atmService;
        }



        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("atmdeNeKadarVar")]
        public IActionResult AtmdeNeKadarParaVar(int atmId)
        {
            var sonuc = _atmService.AtmdekiToplamParayiIdIleGetir(atmId);

            if (sonuc > 0)
            {
                return Ok(sonuc);
            }
            else if (sonuc == 0)
            {
                return Ok("ATM KASETLERİ TAMAMEN BOŞ");
            }
            else
            {
                return Ok("Girilen atmId'e ait atm bulunamadı");
            }


        }




        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("atmKasetKupurleriGuncelle")]
        public IActionResult AtmKasetdekiKupurleriGuncelle(int atmId, int slotNumarasi, int adet, int kupur)
        {
            var sonuc = _atmService.AtmKasetlerdekiKupurleriGuncelle(atmId, slotNumarasi, adet, kupur);


            return Ok(sonuc);
        }




        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici,musteri")]
        [HttpPost("atmdenParaCek")]
        public IActionResult AtmdenParaCek(int atmId, int cekilecekTutar)
        {
            var sonuc = _atmService.AtmdenParaCek(atmId, cekilecekTutar);

            return Ok(sonuc);
        }




        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpPost("atmEkle")]
        public IActionResult AtmEkle(string konum, bool aktifMi)
        {
            var sonuc = _atmService.AtmEkle(konum, aktifMi);

            return Ok(sonuc);
        }




        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpPost("atmleriListeseAktifligeGore")]
        public IActionResult aktifligeGoreAtmListele(bool aktifMi)
        {
            var sonuc = _atmService.AtmleriGetirAktifligeGore(aktifMi);

            return Ok(sonuc);
        }




        [AllowAnonymous]    
        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("tumAtmleriGetir")]
        public IActionResult tumAtmleriGetir()
        {
            var sonuc = _atmService.TumAtmleriGetir();

            return Ok(sonuc);
        }
    }
}