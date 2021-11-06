using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.PostCategories
{
    public class CreatePostCategoryRequestModelValidator : AbstractValidator<CreatePostCategoryRequestModel>
    {
        public CreatePostCategoryRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleForEach(p => p.Translations)
                .SetValidator(new PostCategoryTranslaionRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Word_Translations);
        }
    }
}
