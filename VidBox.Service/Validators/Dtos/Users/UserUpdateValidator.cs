using FluentValidation;
using VidBox.Service.Dtos.Users;

namespace VidBox.Service.Validators.Dtos.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MinimumLength(2).WithMessage("LastName must be more than 2 characters")
            .MaximumLength(20).WithMessage("Lastname must be less than 20 characters");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is not strong password!");
    }
}
