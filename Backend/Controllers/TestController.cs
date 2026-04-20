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
            _smsService.SmsGonder("5522161298", "Proje için test sms'i gönderiyorum -Buğra");
            return Ok("SMS gönderildi.");
        }
    }
}