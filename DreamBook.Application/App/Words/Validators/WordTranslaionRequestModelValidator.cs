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
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Word_Name);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguageGuid.Contains(code))
                .WithMessage(type => Messages.LanguageDoesNotSupport.Format(type.LanguageGuid.ToString()))
                .WithName(ModelsLabel.PostCategoryTranslation_Language);
        }
    }
}
