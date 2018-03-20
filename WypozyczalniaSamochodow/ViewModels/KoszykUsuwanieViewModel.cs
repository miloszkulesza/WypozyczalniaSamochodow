using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WypozyczalniaSamochodow.ViewModels
{
    public class KoszykUsuwanieViewModel
    {
        public decimal KoszykCenaCalkowita { get; set; }
        public int IdPozycjiUsuwanej { get; set; }
        public int KoszykIloscPozycji { get; set; }
    }
}