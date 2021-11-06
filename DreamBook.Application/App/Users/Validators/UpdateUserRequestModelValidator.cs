using DreamBook.Application.LanguageResources;
using FluentValidation;

namespace DreamBook.Application.Users
{
    public class UpdateUserRequestModelValidator : AbstractValidator<UpdateUserRequestModel>
    {
        public UpdateUserRequestModelValidator() : base()
        {
            RuleFor(p => p.Guid).NotEmpty().WithName(ModelsLabel.EntityGuid);
            RuleFor(p => p.Username).NotEmpty().WithName(ModelsLabel.Username);
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithName(ModelsLabel.Email);
            RuleFor(p => p.Name).NotEmpty().WithName(ModelsLabel.User_Name);
            RuleFor(p => p.Gender).NotEmpty().WithName(ModelsLabel.Gender);
        }
    }
}
