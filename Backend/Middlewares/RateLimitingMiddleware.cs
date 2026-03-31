using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Dictionary<string,ClientRequestInfo> _clients
             = new Dictionary<string, ClientRequestInfo>();

        private const int LIMIT = 50;
        
        private const int SURE_DAKIKA = 1;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAdresi = context.Connection.RemoteIpAddress?.ToString() ?? "bilinmiyor";

            if (!_clients.ContainsKey(ipAdresi))
            {
                _clients[ipAdresi] = new ClientRequestInfo
                {
                    IlkIstek = DateTime.Now,
                    IstemSayisi = 1
                };
            }
            else
            {
                var clientBilgi = _clients[ipAdresi];
                var gecenSure = DateTime.Now - clientBilgi.IlkIstek;


                if(gecenSure.TotalMinutes >= SURE_DAKIKA)
                {
                    clientBilgi.IlkIstek = DateTime.Now;
                    clientBilgi.IstemSayisi = 1;
                }
                else if (clientBilgi.IstemSayisi >= LIMIT)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsJsonAsync(new
                    {
                       message = "Çok fazla istek attınız.",
                       detail = $"Lütfen {SURE_DAKIKA} dakika kadar bekleyiniz" 
                    });
                    return;
                }
                else
                {
                    clientBilgi.IstemSayisi++;
                }
            }
            await _next(context);
        
        }
    }


    public class ClientRequestInfo
    {
        public DateTime IlkIstek {get;set;}
        public int IstemSayisi { get; set; }
    }

}