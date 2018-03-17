using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WypozyczalniaSamochodow.Models
{
    public class DaneKlienta
    {
        [Required(ErrorMessage = "Podaj imie")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Podaj nazwisko")]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Podaj adres")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Podaj miasto")]
        public string Miasto { get; set; }
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Zły format numeru")]
        public string Telefon { get; set; }
    }
}