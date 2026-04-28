using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class CebeParaCekRequest
    {
        public string AliciTckNO { get; set; } = string.Empty;
        public string AliciTelNo { get; set; } = string.Empty;
        public string GonderenTelNo { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
    }
}