using EntityLayer.WebApplication.ViewModels.Team;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.TeamValidation
{
    public class TeamUpdateValidation : AbstractValidator<TeamUpdateMV>
    {
        public TeamUpdateValidation()
        {
            RuleFor(x => x.FullName)
              .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("FullName"))
              .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("FullName"))
              .MaximumLength(100).WithMessage(ValidationMessages.MaximumCharachterAllowence("FullName", 100));
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
               .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
               .MaximumLength(100).WithMessage(ValidationMessages.MaximumCharachterAllowence("Title", 100));

        }
    }
}
