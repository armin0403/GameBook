using FluentValidation;
using GameBook.Resources;
using GameBook.Web.ViewModels;
using Microsoft.Extensions.Localization;

namespace GameBook.Validator.User
{
    public class LoginViewModelValidator : AbstractValidator<LogInViewModel>
    {
        public LoginViewModelValidator(IStringLocalizer<Resource> localizer) 
        {
            RuleFor(u => u.UsernameOrEmail).NotEmpty().WithMessage(localizer["FieldRequired"]);
            RuleFor(u => u.Password).NotEmpty().WithMessage(localizer["FieldRequired"]);
        }
    }
}
