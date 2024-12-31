using FluentValidation;
using GameBook.Web.ViewModels;
using Microsoft.Extensions.Localization;

namespace GameBook.Validator.Game
{
    public class GameViewModelValidator : AbstractValidator<GameViewModel>
    {
        public GameViewModelValidator(IStringLocalizer<Resources.Resource> localizer) 
        {
            RuleFor(g => g.Name).NotEmpty().WithMessage(localizer[""]);
            RuleFor(g => g.Description).NotEmpty().WithMessage(localizer[""]);
            RuleFor(g => g.Difficulty).NotEmpty().WithMessage(localizer[""]);
            RuleFor(g => g.Rating).NotEmpty().WithMessage(localizer[""]);
            RuleFor(g => g.PhotoPath).NotEmpty().WithMessage(localizer[""]);
            RuleFor(g => g.MaxTrophies).NotEmpty().WithMessage(localizer[""]);
            RuleFor(g => g.Trophies).NotEmpty().Must((GameViewModel, Trophies) => Trophies <= GameViewModel.MaxTrophies).WithMessage(localizer[""]);
        }
    }
}
