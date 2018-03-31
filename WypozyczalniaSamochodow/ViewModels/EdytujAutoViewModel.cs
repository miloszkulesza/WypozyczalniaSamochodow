using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.ViewModels
{
    public class EdytujAutoViewModel
    {
        public Auto Auto { get; set; }
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public bool? Potwierdzenie { get; set; }
    }
}