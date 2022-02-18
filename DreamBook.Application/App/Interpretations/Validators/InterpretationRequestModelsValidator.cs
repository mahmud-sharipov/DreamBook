using FluentValidation;

namespace DreamBook.Application.Interpretations
{
    public class CreateInterpretationRequestModelValidator : TranslatableRequestModelValidator<CreateInterpretationRequestModel, InterpretationTranslationRequestModel>
    {
        public CreateInterpretationRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(new InterpretationTranslationRequestModelValidator(appLanguageManager))
        {
            RuleFor(p => p.WordGuid).NotEmpty().WithName(ModelsLabel.Word);
            RuleFor(p => p.BookGuid).NotEmpty().WithName(ModelsLabel.Book);
        }
    }

    public class UpdtaeInterpretationRequestModelValidator : TranslatableRequestModelValidator<UpdateInterpretationRequestModel, InterpretationTranslationRequestModel>
    {
        public UpdtaeInterpretationRequestModelValidator(AppLanguageManager appLanguageManager) :
            base(new InterpretationTranslationRequestModelValidator(appLanguageManager))
        {
            RuleFor(p => p.WordGuid).NotEmpty().WithName(ModelsLabel.Word);
            RuleFor(p => p.BookGuid).NotEmpty().WithName(ModelsLabel.Book);
        }
    }

    public class InterpretationTranslationRequestModelValidator : TranslationRequestModelValidator<InterpretationTranslationRequestModel>
    {
        public InterpretationTranslationRequestModelValidator(AppLanguageManager appLanguageManager) 
            : base(appLanguageManager)
        {
            RuleFor(p => p.Description).NotEmpty().WithName(ModelsLabel.Description);
        }
    }

}
