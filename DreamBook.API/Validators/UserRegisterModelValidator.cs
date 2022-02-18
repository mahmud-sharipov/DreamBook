using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.API.Validators
{
    public class UserRegisterModelValidator : UserRequestModelValidator<UserRegisterModel>
    {
        public UserRegisterModelValidator() : base()
        {
            RuleFor(p => p.UserName).NotEmpty().WithName(ModelsLabel.Username);
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithName(ModelsLabel.Email);
            RuleFor(p => p.Password).NotEmpty().MinimumLength(6).WithName(ModelsLabel.Password);
        }
    }
}
