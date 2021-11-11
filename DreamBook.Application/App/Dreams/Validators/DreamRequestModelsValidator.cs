using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Dreams
{
    public class DreamRequestModelValidator<TRequest> : AbstractValidator<TRequest> where TRequest : DreamRequestModel
    {
        public DreamRequestModelValidator() : base()
        {
            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Title);
            RuleFor(p => p.DateTime).NotEmpty().WithName(ModelsLabel.Post_Content);
            RuleFor(p => p.TypeGuid).NotEmpty().WithName(ModelsLabel.Category);
        }

    }

    public class CreateDreamRequestModelValidator : DreamRequestModelValidator<CreateDreamRequestModel>
    {
        public CreateDreamRequestModelValidator() : base()
        {
            When(d => d.Words.Count > 0, () => RuleForEach(d => d.Words).NotEmpty().WithName(ModelsLabel.EntityGuid));
        }
    }

    public class UpdateDreamRequestModelValidator : DreamRequestModelValidator<UpdateDreamRequestModel>
    {
        public UpdateDreamRequestModelValidator() : base() { }
    }
}
