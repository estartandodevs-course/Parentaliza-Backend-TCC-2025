using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Attributes;

public class DataHoraFuturaAttribute : ValidationAttribute
{
    private readonly string _horaPropertyName;

    public DataHoraFuturaAttribute(string horaPropertyName)
    {
        _horaPropertyName = horaPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime data)
        {
            var horaProperty = validationContext.ObjectType.GetProperty(_horaPropertyName);
            if (horaProperty == null)
            {
                return new ValidationResult($"Propriedade {_horaPropertyName} não encontrada.");
            }

            var horaValue = horaProperty.GetValue(validationContext.ObjectInstance);
            if (horaValue is TimeSpan hora)
            {
                var dataHoraCompleta = data.Date.Add(hora);
                if (dataHoraCompleta < DateTime.UtcNow)
                {
                    return new ValidationResult(ErrorMessage ?? "A data e hora do evento não podem ser no passado.");
                }
            }
        }

        return ValidationResult.Success;
    }
}

