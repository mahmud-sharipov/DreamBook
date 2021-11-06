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
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Book_Name);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguageGuid.Contains(code))
                .WithMessage(type => Messages.LanguageDoesNotSupport.Format(type.LanguageGuid))
                .WithName(ModelsLabel.PostCategoryTranslation_Language);
        }
    }
}
