using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.DreamTypes
{
    public class UpdateDreamTypeRequestModelValidator : AbstractValidator<UpdateDreamTypeRequestModel>
    {
        public UpdateDreamTypeRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Guid).NotEmpty().WithName(ModelsLabel.Entity_Guid);

            RuleFor(p => p.Color).NotEmpty().WithName(ModelsLabel.DreamType_Color);

            RuleForEach(p => p.Translations)
               .SetValidator(new DreamTypeTranslationRequestModelValidator(appLanguageManager))
               .WithName(ModelsLabel.DreamType_Translations);
        }
    }
}
