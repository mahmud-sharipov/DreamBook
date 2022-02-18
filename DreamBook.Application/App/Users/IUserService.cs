namespace DreamBook.Application.Users;

public interface IUserService : IEntityService<IUser, UserResponseModel>
{
    Task<UserResponseModel> GetByUserName(string userName);
    Task<UserResponseModel> Create(CreateUserRequestModel requestModel);
    Task Update(UpdateUserRequestModel requestModel);
    Task Update(Guid userId, UserRequestModel requestModel);
    Task UpdateUsername(UpdateUserUsernameRequestModel requestModel);
}
