using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankaSimulasyon.Models.Entities
{

    //Atmkasetler ATM classının içinde.
    public class AtmKaset
    {
        public ATM Atm { get; set; } = null!;
        [Key]
        public int Id { get; set; }
        public int AtmId { get; set; }
        public int SlotNumarasi { get; set; }
        public int Kupur { get; set; }
        public int Adet { get; set; }
        public int KritikDeger { get; set; }
        



    }
}