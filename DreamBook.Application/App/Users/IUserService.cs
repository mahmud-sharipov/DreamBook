using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using System.Threading.Tasks;

namespace DreamBook.Application.Users
{
    public interface IUserService : IEntityService<User,UserResponseModel>
    {
        Task<UserResponseModel> Create(CreateUserRequestModel requestModel);
        Task Update(UpdateUserRequestModel requestModel);
    }
}
