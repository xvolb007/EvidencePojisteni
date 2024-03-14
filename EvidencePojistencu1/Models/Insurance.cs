using System.ComponentModel.DataAnnotations;

namespace EvidencePojistencu1.Models
{
    public class Insurance
    {
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "Typ pojištění je povinný.")]
        public string InsuranceType { get; set; }

        [Required(ErrorMessage = "Výše pojistného je povinná.")]
        [Range(0, int.MaxValue, ErrorMessage = "Výše pojistného musí být kladné číslo.")]
        public int PremiumAmount { get; set; }

        [Required(ErrorMessage = "Datum začátku je povinné.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Datum konce je povinné.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "Datum konce musí být později než datum začátku.")]
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
