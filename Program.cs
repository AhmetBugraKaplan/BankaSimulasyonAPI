using BankaSimulasyon.Data;
using BankaSimulasyon.Repositories;
using BankaSimulasyon.Services;
using BankaSimulasyon.Middlewares;
using Microsoft.EntityFrameworkCore;



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





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware ekle
app.UseMiddleware<ExceptionMiddleware>(); //Hata yakalama
app.UseMiddleware<RateLimitingMiddleware>(); //İstek sınırla
app.UseMiddleware<LoggingMiddleware>(); //Yapılan her isteği (get/post fark etmez) logluyoruz


app.UseHttpsRedirection();
app.MapControllers();




app.Run();


