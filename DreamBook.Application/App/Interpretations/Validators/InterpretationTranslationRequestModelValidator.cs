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
            RuleFor(p => p.Description).NotEmpty().WithName(ModelsLabel.InterpretationTranslation_Description);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguageGuid.Contains(code))
                .WithMessage(type => Messages.LanguageDoesNotSupport.Format(type.LanguageGuid))
                .WithName(ModelsLabel.InterpretationTranslation_Language);
        }
    }
}