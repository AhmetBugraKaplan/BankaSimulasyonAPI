using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class OnayKod
    {
        [Key]
        public int Id { get; set; }
        public string TelefonNumara { get; set; } = string.Empty;
        public int Kod { get; set; }
        public DateTime OlusturulmaZamani { get; set; }
        public bool GecerliMi { get; set; }
        
    }
}