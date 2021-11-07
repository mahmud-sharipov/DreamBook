using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Abstraction
{
    public abstract  class TranslatableRequestModelValidator<TTranslatable, TTranslation> : AbstractValidator<TTranslatable>
        where TTranslation : class, ITranslationRequestModel
        where TTranslatable : class, ITranslatableRequestModel<TTranslation>
    {
        public TranslatableRequestModelValidator(IValidator<TTranslation> translationValidator)
        {
            RuleFor(p => p.Translations)
                  .NotEmpty().WithMessage(FluentMessages.NotEmpty.Format(ModelsLabel.Translations));

            RuleForEach(p => p.Translations)
                .NotNull().WithMessage(FluentMessages.NotNull.Format(ModelsLabel.Translations))
                .SetValidator(translationValidator);
        }
    }
}
