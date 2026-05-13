using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Requests.OnayRequest;
using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnayController : ControllerBase
    {
        private readonly IOnayService _onayService;
        private readonly ISmsService _smsService;
        public OnayController(IOnayService onayService, ISmsService smsService)
        {
            _onayService = onayService;
            _smsService = smsService;
        }

        [AllowAnonymous]
        [HttpPost("OnayKoduDogruMu")]
        public IActionResult OnayKoduDogruMu([FromBody] OnayKoduDogruMuRequest request)
        {
            var sonuc = _onayService.OnayKoduDogruMu(request.Kod, request.TelefonNumara);
            return Ok(sonuc);
        }

        [AllowAnonymous]
        [HttpPost("OnayKoduUret")]
        public IActionResult OnayKoduUret([FromBody] OnayKoduUretRequest request)
        {
            var kod = new Random().Next(1000, 9999).ToString();
            _smsService.SmsGonder(request.TelefonNumara, kod);
            return Ok(new { Basarili = true });
        }


    }
}