using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyonAraKatman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HesapController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HesapController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("MusteriTumHesaplariGetir")]
        public async Task<IActionResult> MusteriTumHesaplariGetir([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var token = Request.Headers["Authorization"].ToString();

            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("api/Hesap/MusteriTumHesaplariGetir", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }

        [HttpPost("BaskasininHesabinaHavaleYap")]
        public async Task<IActionResult> BaskasininHesabinaHavaleYap([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var token = Request.Headers["Authorization"].ToString();

             client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("api/Hesap/BaskasininHesabinaHavaleYap", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }



    }
}