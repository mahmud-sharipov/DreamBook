using DreamBook.Application.Abstraction;
using DreamBook.Application.LanguageResources;
using FluentValidation;
using System;
using System.Linq;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryTranslaionRequestModelValidator : AbstractValidator<PostCategoryTranslationRequestModel>
    {
        public PostCategoryTranslaionRequestModelValidator(AppLanguageManager appLanguageManager)
        {
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.Word_Name);

            RuleFor(p => p.LanguageGuid)
                .NotEmpty()
                .Must(code => appLanguageManager.SupportLanguageGuid.Contains(code))
                .WithMessage(type => Messages.LanguageDoesNotSupport.Format(type.LanguageGuid))
                .WithName(ModelsLabel.PostCategoryTranslation_Language);
        }
    }
}
