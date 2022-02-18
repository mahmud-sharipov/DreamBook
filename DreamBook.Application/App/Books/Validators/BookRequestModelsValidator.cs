using FluentValidation;

namespace DreamBook.Application.Books
{
    public class CreateBookRequestModelValidator : TranslatableRequestModelValidator<CreateBookRequestModel, BookTranslationRequestModel>
    {
        public CreateBookRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(new BookTransltaionRequestModelValidator(appLanguageManager))
        {
        }
    }

    public class UpdateBookRequestModelValidator : TranslatableRequestModelValidator<UpdateBookRequestModel, BookTranslationRequestModel>
    {
        public UpdateBookRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(new BookTransltaionRequestModelValidator(appLanguageManager))
        {
        }
    }

    public class BookTransltaionRequestModelValidator : TranslationRequestModelValidator<BookTranslationRequestModel>
    {
        public BookTransltaionRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Name);
        }
    }

}
