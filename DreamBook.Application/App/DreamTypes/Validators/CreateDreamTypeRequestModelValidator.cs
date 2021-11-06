using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.DreamTypes
{
    public class CreateDreamTypeRequestModelValidator : AbstractValidator<CreateDreamTypeRequestModel>
    {
        public CreateDreamTypeRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Color).NotEmpty().WithName(ModelsLabel.Color);

            RuleForEach(p => p.Translations)
               .SetValidator(new DreamTypeTranslationRequestModelValidator(appLanguageManager))
               .WithName(ModelsLabel.Translations);
        }
    }
}
