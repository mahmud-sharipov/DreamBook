using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;
using System;
using System.Linq;

namespace DreamBook.Application.Books
{
    public class BookTranslaionRequestModelValidator : AbstractValidator<BookTranslationRequestModel>
    {
        public BookTranslaionRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(FluentMessages.NotEmpty.Format(ModelsLabel.Name)); ;

            RuleFor(p => p.LanguageGuid)
                .NotEmpty().WithMessage(ModelsLabel.Language)
                .Must(languageGuid => appLanguageManager.SupportLanguagesGuid.Contains(languageGuid))
                .WithMessage(type => ExceptionMessages.LanguageNotFound.Format(type.LanguageGuid));
        }
    }
}
