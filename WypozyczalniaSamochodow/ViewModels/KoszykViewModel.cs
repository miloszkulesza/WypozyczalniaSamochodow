using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.ViewModels
{
    public class KoszykViewModel
    {
        public List<PozycjaKoszyka> PozycjeKoszyka { get; set; }
        public decimal WartoscCalkowita { get; set; }
    }
}