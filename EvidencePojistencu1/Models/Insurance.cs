using System.ComponentModel.DataAnnotations;

namespace EvidencePojistencu1.Models
{
    public class Insurance
    {
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "Typ pojištění je povinný.")]
        [Display(Name = "Typ pojistneho", Prompt = "Napis sem typ pojistneho")]
        public string InsuranceType { get; set; }

        [Required(ErrorMessage = "Výše pojistného je povinná.")]
        [Range(0, int.MaxValue, ErrorMessage = "Výše pojistného musí být kladné číslo.")]
        [Display(Name = "Vyse Pojisteni", Prompt = "Napis sem vysi pojistneho")]
        public int PremiumAmount { get; set; }

        [Required(ErrorMessage = "Datum začátku je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Zacatek")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Datum konce je povinné.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "Datum konce musí být později než datum začátku.")]
        [Display(Name = "Konec")]
        public DateTime EndDate { get; set; }

        // Cizí klíč pro identifikaci pojištěné osoby
        public int InsuredPersonId { get; set; }

        // Navigační vlastnost pro InsuredPerson
        public virtual InsuredPerson? InsuredPerson { get; set; }
        public class DateGreaterThanAttribute : ValidationAttribute
        {
            private readonly string _comparisonProperty;

            public DateGreaterThanAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);
                var comparisonValue = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance);

                if ((DateTime)value <= comparisonValue)
                {
                    return new ValidationResult(ErrorMessage ?? $"Datum musí být větší než {_comparisonProperty}.");
                }

                return ValidationResult.Success;
            }
        }
    }
}
