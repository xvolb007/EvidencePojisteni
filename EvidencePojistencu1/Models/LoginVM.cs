﻿using System.ComponentModel.DataAnnotations;

namespace EvidencePojistencu1.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Vyplňte emailovou adresu")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
