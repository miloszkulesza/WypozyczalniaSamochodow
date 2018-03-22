using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WypozyczalniaSamochodow.Models
{
    public class Wypozyczenie
    {
        [Key]
        public int WypozyczenieId { get; set; }
        [Required(ErrorMessage = "Podaj imie")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Podaj nazwisko")]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Podaj adres")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Podaj miasto")]
        public string Miasto { get; set; }
        [Required(ErrorMessage = "Podaj telefon")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Podaj email")]
        public string Email { get; set; }
        public DateTime DataZlozenia { get; set; }
        [Required(ErrorMessage = "Podaj ilość dni wypożyczenia")]
        public int iloscDni { get; set; }
        public DateTime DataZwrotu { get; set; }
        [Required(ErrorMessage = "Podaj wartosc")]
        public decimal Wartosc { get; set; }
        public List<PozycjaWypozyczenia> PozycjeWypozyczenia { get; set; }
    }
}