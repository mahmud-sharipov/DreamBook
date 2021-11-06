using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Interpretations
{
    public class UpdtaeInterpretationRequestModelValidator : AbstractValidator<UpdateInterpretationRequestModel>
    {
        public UpdtaeInterpretationRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Guid).NotEmpty().WithName(ModelsLabel.EntityGuid);

            RuleFor(p => p.WordGuid).NotEmpty();
            RuleFor(p => p.BookGuid).NotEmpty();

            RuleForEach(p => p.Translations)
                .SetValidator(new InterpretationTranslationRequestModelValidator(appLanguageManager))
                .WithName(ModelsLabel.Translations);
        }
    }
}
