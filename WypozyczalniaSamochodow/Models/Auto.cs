using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WypozyczalniaSamochodow.Models
{
    public class Auto
    {
        [Key]
        public int AutoId { get; set; }
        public int KategoriaId { get; set; }
        [Required(ErrorMessage = "Podaj marke")]
        public string Marka { get; set; }
        [Required(ErrorMessage = "Podaj model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Podaj pojemnosc silnika")]
        public string Silnik { get; set; }
        [Required(ErrorMessage = "Podaj rok produkcji")]
        public string RokProdukcji { get; set; }
        public bool Wypozyczony { get; set; }
        public DateTime DataDodania { get; set; }
        [Required(ErrorMessage = "Podaj cene")]
        public decimal Cena { get; set; }
        [Required(ErrorMessage = "Podaj moc silnika")]
        public int Moc { get; set; }
        [Required(ErrorMessage = "Podaj typ silnika")]
        public string TypSilnika { get; set; }
        public string Opis { get; set; }
        public virtual Kategoria Kategoria { get; set; }
    }
}