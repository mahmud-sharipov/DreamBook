using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.PostCategories
{
    public class CreatePostCategoryRequestModelValidator : TranslatableRequestModelValidator<UpdatePostCategoryRequestModel, PostCategoryTranslationRequestModel>
    {
        public CreatePostCategoryRequestModelValidator(AppLanguageManager appLanguageManager)
            : base(new PostCategoryTranslaionRequestModelValidator(appLanguageManager))
        {
        }
    }

    public class UpdatePostCategoryRequestModelValidator : TranslatableRequestModelValidator<UpdatePostCategoryRequestModel, PostCategoryTranslationRequestModel>
    {
        public UpdatePostCategoryRequestModelValidator(AppLanguageManager appLanguageManager)
             : base(new PostCategoryTranslaionRequestModelValidator(appLanguageManager))
        {
        }
    }

    public class PostCategoryTranslaionRequestModelValidator : TranslationRequestModelValidator<PostCategoryTranslationRequestModel>
    {
        public PostCategoryTranslaionRequestModelValidator(AppLanguageManager appLanguageManager) : base(appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Name);
        }
    }
}
