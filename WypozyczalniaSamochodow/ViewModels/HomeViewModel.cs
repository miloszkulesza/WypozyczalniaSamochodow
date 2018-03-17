using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Auto> Nowosci { get; set; }
    }
}