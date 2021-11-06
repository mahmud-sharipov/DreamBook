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
            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.AdTranslation_Title);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguageGuid.Contains(code))
                .WithMessage(type => Messages.LanguageDoesNotSupport.Format(type.LanguageGuid))
                .WithName(ModelsLabel.AdTranslation_Language);
        }
    }
}
