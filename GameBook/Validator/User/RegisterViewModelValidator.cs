﻿using FluentValidation;
using GameBook.Resources;
using GameBook.Web.ViewModels;
using Microsoft.Extensions.Localization;

namespace GameBook.Validator.User
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(localizer["FirstNameValidation"]);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(localizer["LastNameValidation"]);
            RuleFor(x => x.Username).Length(5, 15).Matches(@"\d").NotEmpty().WithMessage(localizer["UsernameValidation"]);
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage(localizer["EmailValidation"]);
            RuleFor(x => x.DateOfBirth).Must(BeAtLeast18).NotEmpty().WithMessage(localizer["DateOfBirthValidation"]);
            RuleFor(x => x.Password).Length(8, 18).NotEmpty().WithMessage(localizer["PasswordValidation"]);
            RuleFor(x => x.RetypePassword).Equal(x => x.Password).NotEmpty().WithMessage(localizer["RetypePasswordValidation"]);
            RuleFor(x => x.CountryId).NotEmpty().WithMessage(localizer["CountryValidation"]);
            RuleFor(x => x.PhotoUpload).NotEmpty().WithMessage(localizer["PhotoValidation"]);
        }

        private bool BeAtLeast18(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - dateOfBirth.Year;

            if(dateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            return age > 18;
        }
    }
}
