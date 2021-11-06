using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Books
{
    public class CreateBookRequestModelValidator : AbstractValidator<CreateBookRequestModel>
    {
        public CreateBookRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleFor(p => p.Translations)
                .NotEmpty().WithMessage(FluentMessages.NotEmpty.Format(ModelsLabel.Translations));

            RuleForEach(p => p.Translations)
                .NotNull().WithMessage(FluentMessages.NotNull.Format(ModelsLabel.Translations))
                .SetValidator(new BookTranslaionRequestModelValidator(appLanguageManager));
        }
    }
}
