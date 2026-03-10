using BankaSimulasyon.Data;
using BankaSimulasyon.Repositories;
using BankaSimulasyon.Services;
using BankaSimulasyon.Middlewares;
using Microsoft.EntityFrameworkCore;
using BankaSimulasyon.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{   var config = builder.Configuration;
    var connectionString =config.GetConnectionString("database");
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    /*
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http, // ApiKey değil Http yap
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    Description = "Sadece token giriniz, Bearer otomatik eklenecek"
});

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    }); */
});




builder.Services.AddScoped<IAtmKasetRepository,AtmKasetRepository>();
builder.Services.AddScoped<IAtmService,AtmService>();
builder.Services.AddScoped<IAtmRepository, AtmRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciServis>();
builder.Services.AddScoped<IHesapRepository, HesapRepository>();
builder.Services.AddScoped<IHesapServis,HesapService>();
builder.Services.AddScoped<IKartRepository,KartRepository>();
builder.Services.AddScoped<IKartService,KartService>();


//builder.Services.AddScoped<JwtService>();
// builder.Services.AddScoped<IAuthService, AuthService>();

/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"] ?? string.Empty
        );

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        options.Events = new JwtBearerEvents
        {
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"message\": \"Bu işlem için yetkiniz bulunmamaktadır.\"}");
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"message\": \"Bu işlem için giriş yapmanız gerekmektedir.\"}");
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = false;
});
*/

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


//Middleware ekliyoruz
app.UseMiddleware<ExceptionMiddleware>(); //Hata yakalama !!!HER ZAMAN EN USTTE OLMALI!!!
app.UseMiddleware<LoggingMiddleware>(); //Yapılan her isteği (get/post fark etmez) logluyoruz
app.UseCors("AllowAngular");
app.UseMiddleware<RateLimitingMiddleware>(); //İstek sınırla

/* Authenticationu kapatıyoruz.
app.UseAuthentication(); 
app.UseAuthorization();
*/

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();


//veri tabanı erişimleri store prosedürler ile yapılacak

//Bir uygulama yapıyoruz her tutarı tekr tekr kendisi istiyor ve sonuçları listeliyor. 10 liradan 1000tl ye kadar teker teker çeksin

//Token-jvt


//müşteri limiti ayarlıcaz kartların lşmiti olucak müşterinin toplam limiti kartların limtinden fazla olamaz +++
//Top nokta müşteri limitine göre ayarlanacak kartın max limiti müşterinin limiti kadar olabilecek +++
//kartının limiti özel olarak belirlenebilecek

//limit set eidlen ekranıız var para çekme işlemi limitkleri update edilecek ayrıca atm kasasıda güncellenecek