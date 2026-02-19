using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Models.Responses
{
    public class AtmdenParaCekmeResponse
{
    public bool IslemBasariliMi { get; set; }
    public string Mesaj { get; set; } = "MesajYok";
    public List<AtmKaset> Kasetler { get; set; } = new();
}
}