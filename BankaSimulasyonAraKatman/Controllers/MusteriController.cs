using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyonAraKatman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusteriController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MusteriController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("MusteriGetirIdGore")]
        public async Task<IActionResult> MusteriGetirIdGore([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var token = Request.Headers["Authorization"].ToString();

            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("api/Musteri/MusteriGetirIdGore", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }

    }
}