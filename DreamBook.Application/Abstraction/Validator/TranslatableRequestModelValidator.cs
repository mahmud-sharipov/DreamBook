using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using FluentValidation;
using System.Linq;

namespace DreamBook.Application.Abstraction
{
    public abstract class TranslatableRequestModelValidator<TTranslatable, TTranslation> : AbstractValidator<TTranslatable>
        where TTranslation : class, ITranslationRequestModel
        where TTranslatable : class, ITranslatableRequestModel<TTranslation>
    {
        public TranslatableRequestModelValidator(IValidator<TTranslation> translationValidator)
        {
            RuleFor(p => p.Translations)
                .NotEmpty()
                .Must(t => t.GroupBy(t => t.LanguageGuid).All(l => l.Count() == 1)).WithMessage(FluentMessages.DuplicateTranslations.Format(ModelsLabel.Translations))
                .WithName(ModelsLabel.Translations);

            RuleForEach(p => p.Translations)
                .NotNull()
                .SetValidator(translationValidator)
                .WithName(ModelsLabel.Translations);
        }
    }
}
