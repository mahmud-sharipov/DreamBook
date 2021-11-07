using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using FluentValidation;
using System;
using System.Linq;

namespace DreamBook.Application.Abstraction
{
    public abstract class TranslationRequestModelValidator<TTranslation> : AbstractValidator<TTranslation>
        where TTranslation : class, ITranslationRequestModel
    {
        public TranslationRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleFor(p => p.LanguageGuid)
                .NotEmpty().WithMessage(FluentMessages.NotEmpty.Format(ModelsLabel.Language))
                .Must(languageGuid => appLanguageManager.SupportLanguagesGuid.Contains(languageGuid))
                .WithMessage(type => ExceptionMessages.LanguageNotFound.Format(type.LanguageGuid));
        }
    }
}
