using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Models.Dtos.Requests.ATM;
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
        [HttpGet("{atmId}/atmdeNeKadarVar")]
        public IActionResult AtmdeNeKadarParaVar(int atmId)
        {
            var sonuc = _atmService.AtmdekiToplamParayiIdIleGetir(atmId);

            if (sonuc.Data > 0)
            {
                return Ok(sonuc);
            }
            else if (sonuc.Data == 0)
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
        public IActionResult AtmKasetdekiKupurleriGuncelle([FromBody] AtmKasetGuncelleRequest request)
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpPost("atmEkle")]
        public IActionResult AtmEkle([FromBody] AtmEkleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { hatalar = ModelState.Values.SelectMany(v => v.Errors) });

            var sonuc = _atmService.AtmEkle(
                request.Konum,
                request.AktifMi
            );

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin,gelistirici")]
        [HttpGet("tumAtmleriGetir")]
        public IActionResult TumAtmleriGetir()
        {
            var sonuc = _atmService.TumAtmleriGetir();

            return Ok(sonuc);
        }

        [AllowAnonymous]
        [HttpPost("ParaCekAlgoritmaTest")]
        public IActionResult ParaCekAlgoritmaTest([FromBody] int atmId)
        {
            var satirlar = new List<string>();
            int basarili = 0;
            int basarisiz = 0;

            for (int tutar = 10; tutar <= 1000; tutar += 10)
            {
                var sonuc = _atmService.AtmdenParaCek(atmId, tutar, "");

                if (sonuc.IslemBasariliMi && sonuc.Kasetler != null)
                {
                    basarili++;
                    var dagitim = string.Join(" + ", sonuc.Kasetler
                        .Select(k => $"{k.Kupur}TL x{k.Adet}"));
                    int toplamBanknot = sonuc.Kasetler.Sum(k => k.Adet);
                    satirlar.Add($"{tutar,5} TL ✓  {dagitim}  ({toplamBanknot} banknot)");
                }
                else
                {
                    basarisiz++;
                    satirlar.Add($"{tutar,5} TL ✗  {sonuc.Mesaj}");
                }
            }

            satirlar.Add("");
            satirlar.Add($"ÖZET: {basarili} başarılı / {basarisiz} başarısız");

            // Dosyaya da kaydet
            string dosyaYolu = "AtmParaCekTest_Sonuc.txt";
            System.IO.File.WriteAllLines(dosyaYolu, satirlar, System.Text.Encoding.UTF8);

            return Ok();
        }
    }
}