using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WypozyczalniaSamochodow.Models
{
    public class PozycjaKoszyka
    {
        public Auto Auto { get; set; }
        public decimal Wartosc { get; set; }
    }
}