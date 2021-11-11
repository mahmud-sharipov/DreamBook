using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Users
{
    public class UserRequestModelValidator<TRequest> : AbstractValidator<TRequest> where TRequest : UserRequestModel
    {
        public UserRequestModelValidator()
        {
            RuleFor(p => p.FullName).NotEmpty().WithName(ModelsLabel.User_Name);
            RuleFor(p => p.Gender).NotEmpty().WithName(ModelsLabel.Gender);
            RuleFor(p => p.Birthday).NotEmpty().WithName(ModelsLabel.Gender);
        }
    }

    public class CreateUserRequestModelValidator : UserRequestModelValidator<CreateUserRequestModel>
    {
        public CreateUserRequestModelValidator() : base()
        {
            RuleFor(p => p.UserName).NotEmpty().WithName(ModelsLabel.Username);
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithName(ModelsLabel.Email);
        }
    }

    public class UpdateUserRequestModelValidator : UserRequestModelValidator<UpdateUserRequestModel>
    {
        public UpdateUserRequestModelValidator() : base() { }
    }

    public class UpdateUserUsernameRequestModelValidator : AbstractValidator<UpdateUserUsernameRequestModel>
    {
        public UpdateUserUsernameRequestModelValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithName(ModelsLabel.Username);
        }
    }
}
