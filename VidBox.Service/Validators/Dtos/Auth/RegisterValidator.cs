using FluentValidation;
using VidBox.Service.Dtos.Auth;

namespace VidBox.Service.Validators.Dtos.Auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name is required!")
            .MaximumLength(30).WithMessage("Name must be less than 30 characters")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is not strong password!");
    }
}
