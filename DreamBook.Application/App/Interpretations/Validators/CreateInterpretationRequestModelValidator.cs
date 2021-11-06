using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Interpretations
{
    public class CreateInterpretationRequestModelValidator : AbstractValidator<CreateInterpretationRequestModel>
    {
        public CreateInterpretationRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.WordGuid).NotEmpty();
            RuleFor(p => p.BookGuid).NotEmpty();

            RuleForEach(p => p.Translations)
                .SetValidator(new InterpretationTranslationRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Interpretation_Translations);
        }
    }
}
