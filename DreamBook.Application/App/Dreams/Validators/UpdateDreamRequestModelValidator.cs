using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Dreams
{
    public class UpdateDreamRequestModelValidator : AbstractValidator<UpdateDreamRequestModel>
    {
        public UpdateDreamRequestModelValidator() : base()
        {
            RuleFor(p => p.Guid).NotEmpty().WithName(ModelsLabel.EntityGuid);

            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Title);
            RuleFor(p => p.DateTime).NotEmpty().WithName(ModelsLabel.Post_Content);
            RuleFor(p => p.TypeGuid).NotEmpty().WithName(ModelsLabel.Category);
        }
    }
}
