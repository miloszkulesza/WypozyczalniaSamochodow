using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WypozyczalniaSamochodow.Models
{
    public class Kategoria
    {
        [Key]
        public int KategoriaId { get; set; }
        [Required(ErrorMessage = "Podaj nazwe kategorii")]
        public string NazwaKategorii { get; set; }
        public virtual ICollection<Auto> Auta { get; set; }
    }
}