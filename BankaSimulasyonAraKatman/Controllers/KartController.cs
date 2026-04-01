using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyon.AraKatman.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KartController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public KartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("KartDogrula")]
        public async Task<IActionResult> KartDogrula([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            //Ara katmanımıza istek angular tarafından geliyor. 
            //Aşağıdaki kodda anguları kullanan kişinin ipsini alıyoruz
            var gercekIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "bilinmiyor";

            client.DefaultRequestHeaders.Add("X-Forwarded-For", gercekIp);

            //CoreAPI'nin ilgili endpointine angulardan gelen request'i 
            //iletiyoruz ve sonuç olarak bir response elde ediyoruz.
            var response = await client.PostAsJsonAsync("api/Kart/KartDogrula", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }



        [HttpPost("ParaCek")]
        public async Task<IActionResult> ParaCek([FromBody] object request)
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            //Ara katmanımıza istek angular tarafından body içinde json formatında geliyor. 

            var token = Request.Headers["Authorization"].ToString();

            // Authorization headerına "Bearer eyJhbGci..." değerini ekle
            client.DefaultRequestHeaders.Add("Authorization", token);

            //CoreAPI'nin ilgili endpointine angulardan gelen request'i 
            //iletiyoruz ve sonuç olarak bir response elde ediyoruz.
            var response = await client.PostAsJsonAsync("api/Kart/ParaCek", request);
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }

        


        [HttpPost("KartKalanLimitGetir")]
        public async Task<IActionResult> KartKalanLimitGetir()
        {
            var client = _httpClientFactory.CreateClient("CoreAPI");

            var token = Request.Headers["Authorization"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsJsonAsync("api/Kart/KartKalanLimitGetir", new {});
            var sonuc = await response.Content.ReadAsStringAsync();
            return Ok(sonuc);
        }
    }
}