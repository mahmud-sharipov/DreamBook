using FluentValidation;

namespace DreamBook.Application.Words
{
    public class CreateWordRequestModelValidator : TranslatableRequestModelValidator<CreateWordRequestModel, WordTranslationRequestModel>
    {
        public CreateWordRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(new WordTransltaionRequestModelValidator(appLanguageManager))
        {
        }
    }

    public class UpdateWordRequestModelValidator : TranslatableRequestModelValidator<UpdateWordRequestModel, WordTranslationRequestModel>
    {
        public UpdateWordRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(new WordTransltaionRequestModelValidator(appLanguageManager))
        {
        }
    }

    public class WordTransltaionRequestModelValidator : TranslationRequestModelValidator<WordTranslationRequestModel>
    {
        public WordTransltaionRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Name);
        }
    }
}
