using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Display(Name = "Zapamiętaj")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Podaj email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [StringLength(30, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być jednakowe")]
        public string ConfirmPassword { get; set; }
        public DaneKlienta DaneKlienta { get; set; }
    }
}