using EntityLayer.WebApplication.ViewModels.Portfolio;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.PortfolioValidation
{
    public class PortfolioUpdateValidation : AbstractValidator<PortfolioUpdateMV>
    {
        public PortfolioUpdateValidation()
        {

            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
               .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
               .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharachterAllowence("Title", 200));

        }
    }
}
