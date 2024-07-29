using System.ComponentModel.DataAnnotations;

namespace Domain.Validations
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValidationAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Enum.IsDefined(_enumType, value))
            {
                var allowedValues = Enum.GetValues(_enumType).Cast<int>();
                return new ValidationResult($"Invalid value for {validationContext.DisplayName}. Allowed values are: {string.Join(", ", allowedValues)}");
            }

            return ValidationResult.Success;
        }
    }
}
