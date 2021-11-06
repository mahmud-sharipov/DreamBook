using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Posts
{
    public class UpdatePostRequestModelValidator : AbstractValidator<UpdatePostRequestModel>
    {
        public UpdatePostRequestModelValidator() : base()
        {
            RuleFor(p => p.Guid).NotEmpty().WithName(ModelsLabel.Entity_Guid);

            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Post_Title);
            RuleFor(p => p.Content).NotEmpty().WithName(ModelsLabel.Post_Content);
            RuleFor(p => p.CategoryGuid).NotEmpty().WithName(ModelsLabel.Post_Category);
        }
    }
}
