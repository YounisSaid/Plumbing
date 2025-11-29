using EntityLayer.Identity.ViewModels;
using FluentValidation;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.Identity
{
    public class SignUpValidation : AbstractValidator<SignUpVM>
    {
        public SignUpValidation()
        {
            RuleFor(x=>x.Email).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                               .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                               .EmailAddress().WithMessage(IdentityValidationMessages.CheckEmail());

            RuleFor(x => x.UserName).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("UserName"))
                                    .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("UserName"));

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                                    .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Password"));

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Confirm Password"))
                                           .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Confirm Password"))
                                           .Equal(x=>x.Password).WithMessage(IdentityValidationMessages.ComaprePassword());

            




        }
    }
}
