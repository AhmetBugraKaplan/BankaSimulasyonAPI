using BankaSimulasyon.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public  string? GirisYap(string email, string sifre)
        {
            var emailParam = new SqlParameter("@Email",email);

            var kullaniciListesi = 
            _context.Kullanicilar.FromSqlRaw
            ("EXEC SP_KullaniciGetirEmailGore @Email", emailParam).ToList();

            if (kullaniciListesi == null || kullaniciListesi.Count == 0) return "kullanici bulunamadi";

            var kullanici = kullaniciListesi.FirstOrDefault();

            if (kullanici == null) return "kullanici bulunamadi";

            bool sifreDogruMu = BCrypt.Net.BCrypt.Verify(sifre, kullanici.PasswordHash);

            if (!sifreDogruMu) return "sifre yanlis";

            return _jwtService.TokenUret(kullanici);
        }
    }
}