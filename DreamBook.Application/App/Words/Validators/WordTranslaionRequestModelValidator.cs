using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;
using System;
using System.Linq;

namespace DreamBook.Application.Words
{
    public class WordTranslaionRequestModelValidator : AbstractValidator<WordTranslationRequestModel>
    {
        public WordTranslaionRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Name);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguagesGuid.Contains(code))
                .WithMessage(type => ExceptionMessages.LanguageNotFound.Format(type.LanguageGuid.ToString()))
                .WithName(ModelsLabel.Language);
        }
    }
}
