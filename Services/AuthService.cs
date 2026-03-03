using BankaSimulasyon.Data;
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

        public async Task<string?> GirisYap(string email, string sifre)
        {
            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == email);

            if (kullanici == null) return "kullanici bulunamadi";

            bool sifreDogruMu = BCrypt.Net.BCrypt.Verify(sifre, kullanici.PasswordHash);

            if (!sifreDogruMu) return "sifre yanlis";

            return _jwtService.TokenUret(kullanici);
        }
    }
}