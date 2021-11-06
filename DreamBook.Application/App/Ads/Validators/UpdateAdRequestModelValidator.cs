using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Ads
{
    public class UpdateAdRequestModelValidator : AbstractValidator<UpdateAdRequestModel>
    {
        public UpdateAdRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Guid).NotEmpty().WithName(ModelsLabel.Entity_Guid);
            RuleFor(p => p.Source).NotEmpty().WithName(ModelsLabel.Ad_Source);
            RuleForEach(p => p.Translations)
                .SetValidator(new AdTranslationRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Ad_Translations);
        }
    }
}
