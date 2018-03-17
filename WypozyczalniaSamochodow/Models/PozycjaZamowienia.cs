using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WypozyczalniaSamochodow.Models
{
    public class PozycjaZamowienia
    {
        [Key]
        public int PozycjaZamowieniaId { get; set; }
        public int AutoId { get; set; }
        public int WypozyczenieId { get; set; }
        public decimal WartoscZamowienia { get; set; }
        public virtual Auto auto { get; set; }
        public virtual Wypozyczenie wypozyczenie { get; set; }
    }
}