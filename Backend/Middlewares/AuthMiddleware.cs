using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankaSimulasyon.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        private readonly List<string> _tokenSizEndpointler = new List<string>
        {
            "/api/Kart/KartDogrula",
            "/api/Kart/KartSifreGuncelle",
            "/swagger",
            "/hangfire"
        };

        public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";

            //Tokensiz geçilebien ise direkt nextle
            if (_tokenSizEndpointler.Any(e => path.StartsWith(e)))
            {
                await _next(context);
                return;
            }



            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Token bulunamadı." });
                return;
            }

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                var handler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
                    ),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true  // expire kontrolü de burada yapılıyor
                };

                // Token'ı hem okur hem doğrular — imzayı kontrol eder!
                var principal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                context.User = principal;


                // IP kontrolü
                var tokenIp = principal.FindFirst("ipAdresi")?.Value;
                var istekIp = context.Connection.RemoteIpAddress?.ToString();

                if (tokenIp != istekIp)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { message = "Yetkisiz erişim. IP adresi eşleşmiyor." });
                    return;
                }

                await _next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Token süresi dolmuştur." });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Geçersiz token.", detail = ex.Message });
            }
        }
    }
}