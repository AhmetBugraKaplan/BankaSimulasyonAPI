using BankaSimulasyon.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Middlewares
{
    public class IpControlMiddleware
    {
        private readonly RequestDelegate _next;

        public IpControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //Xforaward içindeki ip yimainülüle edebilrmiyiz

        public async Task InvokeAsync(HttpContext context)
        {
            // Sadece sisteme giriş yapmış (Token'ı doğrulanmış) istekler için IP kontrolü yapıyoruz.
            // AllowAnonymous olan (örn: Login, Swagger) endpointlerde burası 'false' döner ve if içine girmez.
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // ① Blacklist kontrolü — YENİ
                var rawToken = context.Request.Headers["Authorization"]
                    .FirstOrDefault()?.Replace("Bearer ", "");

                var kartRepository = context.RequestServices
                    .GetRequiredService<IKartRepository>();

                if (!string.IsNullOrEmpty(rawToken) && kartRepository.TokenBlacklistteMi(rawToken))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Oturumunuz sonlandırılmış. Lütfen tekrar giriş yapın."
                    });
                    return;
                }

                // 1. Token oluşturulurken (Login aşamasında) içine gömdüğümüz IP adresini al
                var tokenIp = context.User.FindFirst("ipAdresi")?.Value;

                // 2. İsteği anlık olarak yapanın gerçek IP adresini al (BFF'ten gelen X-Forwarded-For)
                var istekIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                // Eğer istek BFF'ten değil de doğrudan backend'e gelmişse (Test ortamı vb.), fallback olarak kendi IP'sini al
                if (string.IsNullOrEmpty(istekIp))
                {
                    istekIp = context.Connection.RemoteIpAddress?.ToString();
                }

                // 3. Güvenlik Kontrolü: Token'daki IP ile İsteği yapan IP aynı mı?
                if (!string.IsNullOrEmpty(tokenIp) && tokenIp != istekIp)
                {
                    // Eşleşmiyorsa işlemi derhal kes ve hata dön!
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Yetkisiz erişim. Oturumunuz farklı bir cihazdan/ağdan kullanılamaz. Lütfen tekrar giriş yapın."
                    });

                    return; // return diyerek _next(context)'i ÇAĞIRMIYORUZ. İstek burada ölür, Controller'a ulaşamaz.
                }
            }

            // IP'ler eşleşiyorsa VEYA kullanıcı henüz giriş yapmamışsa (Token gerektirmeyen bir işlemse) yola devam et.
            await _next(context);
        }
    }
}