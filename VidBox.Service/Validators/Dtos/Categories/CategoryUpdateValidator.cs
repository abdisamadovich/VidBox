using FluentValidation;
using VidBox.Service.Dtos.Categories;

namespace VidBox.Service.Validators.Dtos.Categories;

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(20).WithMessage("Name must be less than 30 characters");
    }
}
