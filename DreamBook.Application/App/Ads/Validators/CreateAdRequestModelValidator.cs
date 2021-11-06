using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Ads
{
    public class CreateAdRequestModelValidator : AbstractValidator<CreateAdRequestModel>
    {
        public CreateAdRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Source).NotEmpty().WithName(ModelsLabel.Source);

            RuleForEach(p => p.Translations)
                .SetValidator(new AdTranslationRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Translations);
        }
    }
}
