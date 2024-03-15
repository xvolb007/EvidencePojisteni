using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EvidencePojistencu1.Models
{
    public class InsuredPerson
    {
        public int InsuredPersonId { get; set; }

        [Required(ErrorMessage = "Jméno je povinné.")]
        [Display(Name = "Jmeno", Prompt ="Napis sem jmeno")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Adresa je povinná.")]
        [Display(Name = "Adresa", Prompt = "Napis sem adresu")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Telefonní číslo je povinné.")]
        [RegularExpression(@"^\+\d{1,3}\d{3}\d{3}\d{3}$", ErrorMessage = "Telefonní číslo musí být ve formátu: +XXXXXXXXXXXX.")]
        [Display(Name = "Telefonni cislo", Prompt = "Napis sem telefonni cislo")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Adresa je povinná.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email adresa neni platna")]
        [Display(Name = "Email", Prompt = "Asi uz vis")]
        public string? Email { get; set; }

        // Navigační vlastnost pro seznam pojištění spojených s touto osobou
        public virtual Collection<Insurance>? Insurances { get; set; }
    }
}
