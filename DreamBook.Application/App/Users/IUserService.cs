using System.Collections.Generic;

namespace DreamBook.Application.Users;

public interface IUserService<TUser> where TUser : class, IUser
{
    Task<IEnumerable<UserResponseModel>> GetAll();
    Task<UserResponseModel> GetById(Guid id);
    Task<UserResponseModel> GetByUserName(string userName);
    Task<UserResponseModel> GetByEmail(string email);
    Task<IPagedList<UserResponseModel>> GetPagedList(IPagedListRequestModel<TUser> requestModel);
    Task<UserResponseModel> Create(CreateUserRequestModel requestModel);
    Task Update(UpdateUserRequestModel requestModel);
    Task Update(Guid userId, UserRequestModel requestModel);
    Task UpdateUsername(UpdateUserUsernameRequestModel requestModel);
    Task Delete(Guid id);
}
