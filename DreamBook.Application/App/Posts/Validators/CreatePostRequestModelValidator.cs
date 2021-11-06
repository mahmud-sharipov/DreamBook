using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Posts
{
    public class CreatePostRequestModelValidator : AbstractValidator<CreatePostRequestModel>
    {
        public CreatePostRequestModelValidator() : base()
        {
            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Post_Title);
            RuleFor(p => p.Content).NotEmpty().WithName(ModelsLabel.Post_Content);
            RuleFor(p => p.CategoryGuid).NotEmpty().WithName(ModelsLabel.Post_Category);
        }
    }
}
