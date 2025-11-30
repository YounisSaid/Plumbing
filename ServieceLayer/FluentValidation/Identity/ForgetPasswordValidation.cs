using EntityLayer.Identity.ViewModels;
using FluentValidation;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.Identity
{
    public class ForgetPasswordValidation : AbstractValidator<ForgetPasswordMV>
    {
        public ForgetPasswordValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                              .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                              .EmailAddress().WithMessage(IdentityValidationMessages.CheckEmail());
        }
    }
}
