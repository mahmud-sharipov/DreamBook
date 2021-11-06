using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using System.Threading.Tasks;

namespace DreamBook.Application.Users
{
    public class UserService : EntityService<User, UserResponseModel>, IUserService
    {
        public UserService(IContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<UserResponseModel> Create(CreateUserRequestModel requestModel)
        {
            return await Create<CreateUserRequestModel>(requestModel);
        }

        public async Task Update(UpdateUserRequestModel requestModel)
        {
            await Update(requestModel, requestModel.Guid);
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(User entity)
        {
            return (false, Messages.EntityCanNotBeDeleted);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(User.FullName);
        protected override string GetDefaultPropertyNameToOrderBy() => nameof(User.FullName);
    }
}
