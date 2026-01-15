using FluentValidation;
using Shared.Enums;

namespace Application.Common.Validations.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out var guid) && guid != Guid.Empty)
                    .WithMessage("Id must be a valid GUID.");
        }
        
        public static IRuleBuilderOptions<T, string> MustBeValidName<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder                
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Person name is required.")
                .Length(3, 100)
                    .WithMessage("Person name must be between 3 and 100 characters.");
        }

        public static IRuleBuilderOptions<T, DateTimeOffset> MustBeValidBirthDate<T>(this IRuleBuilderInitial<T, DateTimeOffset> ruleBuilder)
        {
            var today = DateTimeOffset.UtcNow.Date;

            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEqual(DateTimeOffset.MinValue)
                    .WithMessage("Birth date must be informed.")
                .LessThan(today)
                    .WithMessage("Birth date must be in the past.")
                .GreaterThan(DateTimeOffset.UtcNow.Date.AddYears(-70))
                    .WithMessage("The age cannot exceed 70 years.");
        }
        
        public static IRuleBuilderOptions<T, string> MustBeValidCpfNumber<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("CPF number is required.")
                .Matches(@"^\d{11}$")
                    .WithMessage("CPF number must be 11 digits.");
        }

        public static IRuleBuilderOptions<T, DateTimeOffset> MustBeValidCpfRegistrationDate<T>(this IRuleBuilderInitial<T, DateTimeOffset> ruleBuilder, Func<T, DateTimeOffset> birthDateSelector)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEqual(DateTimeOffset.MinValue)
                    .WithMessage("CPF registration date must be informed.")
                .Must((instance, registrationDate) =>
                        registrationDate > birthDateSelector(instance))
                    .WithMessage("CPF registration date must be after birth date.");
        }

        public static IRuleBuilderOptions<T, string> MustBeValidRgNumber<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("RG number is required.")
                .Matches(@"^\d{9}$")
                    .WithMessage("RG number must be 9 digits.");
        }

        public static IRuleBuilderOptions<T, IssuingAuthority> MustBeValidRgIssuingAuthority<T>(this IRuleBuilder<T, IssuingAuthority> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum()
                    .WithMessage("RG issuing authority is invalid.");
        }
    }
}
