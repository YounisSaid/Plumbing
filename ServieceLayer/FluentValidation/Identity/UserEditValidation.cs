using EntityLayer.Identity.ViewModels;
using FluentValidation;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.Identity
{
    public class UserEditValidation : AbstractValidator<UserEditMV>
    {
        public UserEditValidation()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                               .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                               .EmailAddress().WithMessage(IdentityValidationMessages.CheckEmail());

            RuleFor(x => x.UserName).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("UserName"))
                                    .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("UserName"));

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                                    .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Password"));

            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.Password).WithMessage(IdentityValidationMessages.ComaprePassword());


        }
    }
}
