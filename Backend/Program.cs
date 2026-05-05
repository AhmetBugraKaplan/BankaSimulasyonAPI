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
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Buraya sadece ürettiğiniz Token'ı yapıştırın (Başına Bearer yazmanıza gerek yok, otomatik eklenecektir)."
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
            Array.Empty<string>()
        }
    });
});




builder.Services.AddScoped<IAtmKasetRepository, AtmKasetRepository>();
builder.Services.AddScoped<IAtmService, AtmService>();
builder.Services.AddScoped<IAtmRepository, AtmRepository>();
builder.Services.AddScoped<IMusteriRepository, MusteriRepository>();
builder.Services.AddScoped<IMusteriService, MusteriServis>();
builder.Services.AddScoped<IKartRepository, KartRepository>();
builder.Services.AddScoped<IKartService, KartService>();
builder.Services.AddScoped<IHesapRepository, HesapRepository>();
builder.Services.AddScoped<IHesapServis, HesapService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IOnayRepository, OnayRepository>();
builder.Services.AddScoped<IOnayService, OnayService>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddMemoryCache();

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



//Güncellemeyi kart kart yapmamız gerekyior. Son işlem yapılan tarih bir yerde tutulacak değiştiğinde güncellencek (+)
//kalan limiti kullanılan limit olarka değiştir ve son işlem tarihi ekle her limit güncellemede son işlem tarihi değişiyor
//gün değişince otomatik algılıyoruz zaten orda işlemden önce limiti güncelleyecğiz. 

//Ara server katmanı oluşturacağım bu sadece server olacak istek atıcaz oraya 


//Middleware ekliyoruz
app.UseMiddleware<ExceptionMiddleware>(); //Hata yakalama !!!HER ZAMAN EN USTTE OLMALI!!!
app.UseMiddleware<LoggingMiddleware>(); //Yapılan her isteği (get/post fark etmez) logluyoruz
app.UseCors("AllowAngular");
app.UseMiddleware<RateLimitingMiddleware>(); //İstek sınırla
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<IpControlMiddleware>();


//app.UseHttpsRedirection();
app.MapControllers();

app.Run();


/*  1.Toplantı
veri tabanı erişimleri store prosedürler ile yapılacak

Bir uygulama yapıyoruz her tutarı tekr tekr kendisi istiyor ve sonuçları listeliyor. 10 liradan 1000tl ye kadar teker teker çeksin

Token-jvt
*/

/* 2.Toplantı
müşteri limiti ayarlıcaz kartların lşmiti olucak müşterinin toplam limiti kartların limtinden fazla olamaz +++
Top nokta müşteri limitine göre ayarlanacak kartın max limiti müşterinin limiti kadar olabilecek +++
kartının limiti özel olarak belirlenebilecek

limit set eidlen ekranıız var para çekme işlemi limitkleri update edilecek ayrıca atm kasasıda güncellenecek ++

Kullanıcı eklenince otomatik olarak hesapda oluşsun (Hesap tablosunu sildik :D)

Kart numaraları aynı ise para çekme işlemine girmiyor o kısımda hem KartNo ile kullanıcıId kontrolü yapmamız gerekiyor.
Kart şifrelerini hashlememiz lazım ++
Para çekerken kartın şifresini de girmemiz lazım eğer şifre doğru ise para çekicek.

*/

/* 3.Toplantı
Kullanicilar tablosundan Adres cinsiyet  telefon numarası bilgilerini çıkart. (+)
Kullanıcılar tablosunun ismini müşteriler olarak değiştiricez. (+)
KullanıcıHesapları aıdndaki tabloyu sileceğiz. (+)

para çekerken restful servis olarak yazıcaz. Bir server bir de client olucak. İstek beklyecek sürekli servis çağırılıcak sürekli 
Kasayı ve limitleri burada güncelle (+)

Atm numarası , kart numarası ve kart şifresi girilecek login olucaz. 3 denem hakkı => Burada giriş yaptığımız zaman jwt ile tokenlerımız kaydolsun 
tüm işlemlerde bizim hesabımıza göre işelm yaplaım (+)

Sonraki ekranda tutar girilecek ve para çekme işlemi yapacağız. (Arkaplanda restful servis oluşturacak) (+) 

Limit günlük ya da aylık olabilir, sen kalan limit adında bir değişken daha oluşturacaksın tabloya o değişken kalan limitini tutarken
diğeri gün sonu ya da ay sonu sıfırlanacak.(Kalan limit oluşturuldu fakat gün sonu sıfırlanma işlemini daha yapmadık.) (-)



---4.Toplantı---
kalan Limitimiz gun sonunda tekrardan sıfırlanmalı bir kartın birden fazla limiti olabilir o mevzuyu çöz. (+)

//restful servis protol öğren
//wcf seris ile web servis restful servis farkı nelerdir
//neden web servis kullanırız neden restful servis kullanırız.


//bodyla veriyi gönder (+) --> Bu işlem için DTO katmanına requestler ekledik FromBody ile request nesneleri body içinde dönüyor.

/web servisleri json-datayı body nin içine nasıl göndereiblirim (+)

sonrasında json datayı ihtiyacımız olan alanlarda parse edeceğiz.

JWT bir middle ware aracılığyla auth m.ware ile nasıl kullanılır bunu araştır.
Ürettiğimiz token angular tarafında header 'a eklememiz gerekiyor restful servisin
oauth2.0 bearer token

claimiçine ip port ekle  tokenı localstorage de çalıştır


AUTH MV
mw localde var mı

tokenı claımlerıne ayır

claimleri kontrol ediyoruz requestlerin ipsi ile cliam içi ip aynı mı

expire date geçti mi
------------



lifetimelera çaloş


loogout localden bu tokenı silicez



kötü niyetli bi bankanın komisyon ödemeedne nasıl para çekersin
token manipüle

//Options metodları nedir bak internetten

*/

