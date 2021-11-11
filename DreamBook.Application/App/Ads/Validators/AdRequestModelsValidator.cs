using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Ads
{
    public class CreateAdRequestModelValidator : TranslatableRequestModelValidator<CreateAdRequestModel, AdTranslationRequestModel>
    {
        public CreateAdRequestModelValidator(AppLanguageManager appLanguageManager)
            : base(new AdTranslationRequestModelValidator(appLanguageManager))
        {
            RuleFor(p => p.Source).NotEmpty().WithName(ModelsLabel.Source);
        }
    }

    public class UpdateAdRequestModelValidator : TranslatableRequestModelValidator<UpdateAdRequestModel, AdTranslationRequestModel>
    {
        public UpdateAdRequestModelValidator(AppLanguageManager appLanguageManager)
            : base(new AdTranslationRequestModelValidator(appLanguageManager))
        {
            RuleFor(p => p.Source).NotEmpty().WithName(ModelsLabel.Source);
        }
    }

    public class AdTranslationRequestModelValidator : TranslationRequestModelValidator<AdTranslationRequestModel>
    {
        public AdTranslationRequestModelValidator(AppLanguageManager appLanguageManager) : base(appLanguageManager)
        {
            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Title);
        }
    }
}
