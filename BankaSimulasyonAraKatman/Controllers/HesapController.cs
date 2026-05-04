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

            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }

        [HttpPost("HavaleYap")]
        public async Task<IActionResult> HavaleYap([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var token = Request.Headers["Authorization"].ToString();

            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("api/Hesap/HavaleYap", request);
            var sonuc = await response.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }

        [HttpPost("HesapVarMi")]
        public async Task<IActionResult> HesapVarMi([FromBody] object request)
        {

            var client = _httpClientFactory.CreateClient("CoreAPI");

            var response = await client.PostAsJsonAsync("api/Hesap/HesapVarMi", request);
            var sonuc = await response.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }


        [HttpPost("HesapVarMiTelNoIle")]
        public async Task<IActionResult> HesapVarMiTelNoIle([FromBody] object request)
        {

            var client = _httpClientFactory.CreateClient("CoreAPI");

            var response = await client.PostAsJsonAsync("api/Hesap/HesapVarMiTelNoIle", request);
            var sonuc = await response.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }

        [HttpPost("HesabaKartsizParaGonder")]
        public async Task<IActionResult> HesabaKartsizParaGonder([FromBody] object request)
        {

            var client = _httpClientFactory.CreateClient("CoreAPI");

            var response = await client.PostAsJsonAsync("api/Hesap/HesabaKartsizParaGonder", request);
            var sonuc = await response.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }

        [HttpPost("CebeParaGonder")]
        public async Task<IActionResult> CebeParaGonder([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var token = Request.Headers["Authorization"].ToString();

            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("api/Hesap/CebeParaGonder", request);
            var sonuc = await response.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }


        [HttpPost("CebeParaCek")]
        public async Task<IActionResult> CebeParaCek([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var response = await client.PostAsJsonAsync("api/Hesap/CebeParaCek", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            
            return new ContentResult
            {
                Content = sonuc,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode // CoreAPI'nin döndüğü asıl statü kodunu Angular'a iletiyoruz
            };
        }


    }
}

//dinamik sp bak