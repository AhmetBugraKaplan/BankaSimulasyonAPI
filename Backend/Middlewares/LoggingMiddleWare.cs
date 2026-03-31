namespace BankaSimulasyon.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Console.WriteLine($"---> İstek Geldi");
            Console.WriteLine($"     Metod : {context.Request.Method}");
            Console.WriteLine($"     Adres : {context.Request.Path}");
            Console.WriteLine($"     Zaman : {DateTime.Now}");


            await _next(context);       

            watch.Stop();               

            Console.WriteLine($"<--- Yanıt Gönderildi");                          
            Console.WriteLine($"     Status  : {context.Response.StatusCode}");  
            Console.WriteLine($"     Süre    : {watch.ElapsedMilliseconds}ms");
            Console.WriteLine($"-----------------------------");

        }





    }

}