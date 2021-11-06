using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Books
{
    public class CreateBookRequestModelValidator : AbstractValidator<CreateBookRequestModel>
    {
        public CreateBookRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleForEach(p => p.Translations)
                .SetValidator(new BookTranslaionRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Book_Translations);
        }
    }
}
