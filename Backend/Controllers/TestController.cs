using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public TestController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost("SmsTesti")]
        public IActionResult SmsTesti()
        {
            var kod = new Random().Next(1000, 9999).ToString();
            _smsService.SmsGonder("5522161298", kod);
            return Ok($"SMS gönderildi. Kod: {kod}");
        }
    }
}