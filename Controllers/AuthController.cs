using BankaSimulasyon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankaSimulasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("GirisYap")]
        public async Task<IActionResult> GirisYap(string email, string sifre)
        {

            var token = await _authService.GirisYap(email, sifre);

            if (token == null || !token.StartsWith("eyJ"))
                return Unauthorized(new { Mesaj = token ?? "Giriş başarısız" });

            return Ok(new { Token = token });
        }
    }
}