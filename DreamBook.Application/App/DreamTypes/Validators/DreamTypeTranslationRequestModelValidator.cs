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
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.DreamTypeTranslation_Name);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguageGuid.Contains(code))
                .WithMessage(type => Messages.LanguageDoesNotSupport.Format(type.LanguageGuid))
                .WithName(ModelsLabel.DreamTypeTranslation_Language);
        }
    }
}
