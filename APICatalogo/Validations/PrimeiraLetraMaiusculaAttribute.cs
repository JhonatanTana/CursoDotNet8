using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations; 
public class PrimeiraLetraMaiusculaAttribute: ValidationAttribute {

    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

        if (value == null || string.IsNullOrEmpty(value.ToString())) {

            return ValidationResult.Success;
        }

        var primeriaLetra = value.ToString()[0].ToString();
        if (primeriaLetra != primeriaLetra.ToUpper()) { // Se a primeira letra nao for Maiuscula

            return new ValidationResult("A primeira letra do nome deve ser maiuscula");
        }

        return ValidationResult.Success;
    }
}
