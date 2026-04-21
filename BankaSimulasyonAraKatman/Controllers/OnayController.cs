using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankaSimulasyonAraKatman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OnayController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("OnayKoduDogruMu")]
        public async Task<IActionResult> OnayKoduDogruMu([FromBody] object request)
        {

            var client = _httpClientFactory.CreateClient("CoreAPI");

            var response = await client.PostAsJsonAsync("api/Onay/OnayKoduDogruMu", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }

        [HttpPost("OnayKoduUret")]
        public async Task<IActionResult> OnayKoduUret([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");
            var response = await client.PostAsJsonAsync("api/Onay/OnayKoduUret", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }

    }
}