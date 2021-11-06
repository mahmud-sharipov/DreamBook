using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Users
{
    public class CreateUserRequestModelValidator : AbstractValidator<CreateUserRequestModel>
    {
        public CreateUserRequestModelValidator() : base()
        {
            RuleFor(p => p.Username).NotEmpty().WithName(ModelsLabel.Username);
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithName(ModelsLabel.Email);
        }
    }
}
