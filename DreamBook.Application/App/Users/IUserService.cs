using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DreamBook.Application.Users
{
    public interface IUserService : IEntityService<User, UserResponseModel>
    {
        Task<UserResponseModel> GetByUserName(string userName);
        Task<UserResponseModel> Create(CreateUserRequestModel requestModel);
        Task Update(UpdateUserRequestModel requestModel);
        Task Update(Guid userId, UserRequestModel requestModel);
        Task UpdateUsername(UpdateUserUsernameRequestModel requestModel);
        Task DeleteFull(Guid id);
    }
}
