using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.DreamTypes
{
    public class CreateDreamTypeRequestModelValidator : TranslatableRequestModelValidator<CreateDreamTypeRequestModel, DreamTypeTranslationRequestModel>
    {
        public CreateDreamTypeRequestModelValidator(AppLanguageManager appLanguageManager) 
            : base(new DreamTypeTranslationRequestModelValidator(appLanguageManager))
        {
            RuleFor(p => p.Color).NotEmpty().WithName(ModelsLabel.Color);
        }
    }

    public class UpdateDreamTypeRequestModelValidator : TranslatableRequestModelValidator<UpdateDreamTypeRequestModel, DreamTypeTranslationRequestModel>
    {
        public UpdateDreamTypeRequestModelValidator(AppLanguageManager appLanguageManager) 
            : base(new DreamTypeTranslationRequestModelValidator(appLanguageManager))
        {
            RuleFor(p => p.Color).NotEmpty().WithName(ModelsLabel.Color);
        }
    }

    public class DreamTypeTranslationRequestModelValidator : TranslationRequestModelValidator<DreamTypeTranslationRequestModel>
    {
        public DreamTypeTranslationRequestModelValidator(AppLanguageManager appLanguageManager) : base(appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Name);
        }
    }
}
