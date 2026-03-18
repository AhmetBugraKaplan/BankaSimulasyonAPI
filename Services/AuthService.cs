
using BankaSimulasyon.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;

        public AuthService(AppDbContext context, JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public string TokenUret(string kartNumara,int atmId)
        {
            return _jwtService.TokenUret(kartNumara,atmId);
        }
    }
}
