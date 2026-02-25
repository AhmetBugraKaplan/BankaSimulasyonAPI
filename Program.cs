using BankaSimulasyon.Data;
using BankaSimulasyon.Repositories;
using BankaSimulasyon.Services;
using BankaSimulasyon.Middlewares;
using Microsoft.EntityFrameworkCore;
using BankaSimulasyon.Models.Entities;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{   var config = builder.Configuration;
    var connectionString =config.GetConnectionString("database");
    options.UseSqlite(connectionString);
});

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAtmKasetRepository,AtmKasetRepository>();
builder.Services.AddScoped<IAtmService,AtmService>();
builder.Services.AddScoped<IAtmRepository, AtmRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciServis>();
builder.Services.AddScoped<IHesapRepository, HesapRepository>();
builder.Services.AddScoped<IHesapServis,HesapService>();
builder.Services.AddScoped<IKartRepository,KartRepository>();
builder.Services.AddScoped<IKartService,KartService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

//Middleware ekle
app.UseMiddleware<ExceptionMiddleware>(); //Hata yakalama
app.UseMiddleware<RateLimitingMiddleware>(); //İstek sınırla
app.UseMiddleware<LoggingMiddleware>(); //Yapılan her isteği (get/post fark etmez) logluyoruz


//app.UseHttpsRedirection();
app.MapControllers();

app.Run();


//veri tabanı erişimleri store prosedürler ile yapılacak

//Bir uygulama yapıyoruz her tutarı tekr tekr kendisi istiyor ve sonuçları listeliyor. 10 liradan 1000tl ye kadar teker teker çeksin

//Token-jvt