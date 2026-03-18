
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Services
{
    public interface IAuthService
    {
        string? TokenUret(string kartNumara, int atmId);
    }
}
