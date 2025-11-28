using EntityLayer.WebApplication.ViewModels.Category;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.CategoryValidation
{
    public class CategoryUpdateValidation : AbstractValidator<CategoryUpdateMV>
    {
        public CategoryUpdateValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
               .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
               .MaximumLength(50).WithMessage(ValidationMessages.MaximumCharachterAllowence("Name", 50));
        }
    }
}
