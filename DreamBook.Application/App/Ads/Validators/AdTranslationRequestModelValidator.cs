using DreamBook.Application.LanguageResources;
using DreamBook.Application.Abstraction;
using FluentValidation;
using System;
using System.Linq;

namespace DreamBook.Application.Ads
{
    public class AdTranslationRequestModelValidator : AbstractValidator<AdTranslationRequestModel>
    {
        public AdTranslationRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Title);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguagesGuid.Contains(code))
                .WithMessage(type => ExceptionMessages.LanguageNotFound.Format(type.LanguageGuid))
                .WithName(ModelsLabel.Language);
        }
    }
}
