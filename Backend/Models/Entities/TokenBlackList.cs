using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class TokenBlackList
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireTime { get; set; }
    }
}