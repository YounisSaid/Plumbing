using EntityLayer.Identity.ViewModels;
using FluentValidation;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.Identity
{
    public class LoginValidation : AbstractValidator<LoginMV>
    {
        public LoginValidation()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                              .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                              .EmailAddress().WithMessage(IdentityValidationMessages.CheckEmail());

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                                    .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Password"));
        }
    }
}
