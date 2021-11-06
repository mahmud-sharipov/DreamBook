using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;
using System.Linq;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeTranslationRequestModelValidator : AbstractValidator<DreamTypeTranslationRequestModel>
    {
        public DreamTypeTranslationRequestModelValidator(AppLanguageManager appLanguageManager) : base()
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Name);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguagesGuid.Contains(code))
                .WithMessage(type => ExceptionMessages.LanguageNotFound.Format(type.LanguageGuid))
                .WithName(ModelsLabel.Language);
        }
    }
}
