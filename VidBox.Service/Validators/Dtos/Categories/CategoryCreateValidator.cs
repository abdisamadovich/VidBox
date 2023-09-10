using FluentValidation;
using VidBox.Service.Dtos.Categories;

namespace VidBox.Service.Validators.Dtos.Categories;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(30).WithMessage("Name must be less than 30 characters");
    }
}
