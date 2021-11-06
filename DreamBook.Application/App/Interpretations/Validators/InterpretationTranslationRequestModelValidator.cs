using DreamBook.Application.LanguageResources;
using DreamBook.Application.Abstraction;
using FluentValidation;
using System;
using System.Linq;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationTranslationRequestModelValidator : AbstractValidator<InterpretationTranslationRequestModel>
    {
        public InterpretationTranslationRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Description).NotEmpty().WithName(ModelsLabel.Description);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguagesGuid.Contains(code))
                .WithMessage(type => ExceptionMessages.LanguageNotFound.Format(type.LanguageGuid))
                .WithName(ModelsLabel.Language);
        }
    }
}