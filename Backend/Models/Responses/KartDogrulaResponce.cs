using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Responses
{
    public class KartDogrulaResponce
    {
        public string Token { get; set; } = string.Empty;
        public string KartNumara { get; set; } = string.Empty;
        public int AtmId { get; set; }
    }
}