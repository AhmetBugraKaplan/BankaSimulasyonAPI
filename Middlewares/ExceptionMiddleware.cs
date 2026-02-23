using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA YAKALANDI: {ex.Message}");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";


                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Sunucu hatası oluştu.",
                    detail = ex.Message
                });
            }

        

        







        }




    }
}