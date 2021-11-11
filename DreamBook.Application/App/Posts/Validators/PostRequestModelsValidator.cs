using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Posts
{
    public class PostRequestModelValidator<TRequest> : AbstractValidator<TRequest> where TRequest : CreatePostRequestModel
    {
        public PostRequestModelValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithName(ModelsLabel.Title);
            RuleFor(p => p.Content).NotEmpty().WithName(ModelsLabel.Post_Content);
            RuleFor(p => p.CategoryGuid).NotEmpty().WithName(ModelsLabel.Category);
        }
    }

    public class CreatePostRequestModelValidator : PostRequestModelValidator<CreatePostRequestModel>
    {
        public CreatePostRequestModelValidator() : base() { }
    }

    public class UpdatePostRequestModelValidator : PostRequestModelValidator<UpdatePostRequestModel>
    {
        public UpdatePostRequestModelValidator() : base() { }
    }
}
