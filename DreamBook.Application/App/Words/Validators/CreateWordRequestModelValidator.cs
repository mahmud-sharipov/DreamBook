using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Words
{
    public class CreateWordRequestModelValidator : AbstractValidator<CreateWordRequestModel>
    {
        public CreateWordRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleForEach(p => p.Translations)
                .SetValidator(new WordTranslaionRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Translations);
        }
    }
}
