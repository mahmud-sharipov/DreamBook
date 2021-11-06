using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.PostCategories
{
    public class UpdatePostCategoryRequestModelValidator : AbstractValidator<UpdatePostCategoryRequestModel>
    {
        public UpdatePostCategoryRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleForEach(p => p.Translations)
                .SetValidator(new PostCategoryTranslaionRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Translations);
        }
    }
}
