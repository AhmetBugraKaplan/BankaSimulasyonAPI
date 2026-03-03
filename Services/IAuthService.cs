using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Services
{
    public interface IAuthService
    {
        Task<string?> GirisYap(string email, string sifre);
    }
}