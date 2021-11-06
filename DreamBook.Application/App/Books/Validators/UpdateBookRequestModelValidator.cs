using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Books
{
    public class UpdateBookRequestModelValidator : AbstractValidator<UpdateBookRequestModel>
    {
        public UpdateBookRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleForEach(p => p.Translations)
                .SetValidator(new BookTranslaionRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Book_Translations);
        }
    }
}
