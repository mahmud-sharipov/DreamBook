﻿using AutoMapper;
using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;

namespace DreamBook.Persistence.Services;

public class UserService : EntityService<User, UserResponseModel>, IUserService
{
    public UserService(IContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<UserResponseModel> GetByUserName(string userName)
    {
        var user = await Context.GetFirstOrDefaultAsync<User>(x => x.UserName == userName);
        if (user == null)
            throw new EntityNotFoundException(ModelsLabel.User, userName);

        return Mapper.Map<UserResponseModel>(user);
    }

    public async Task<UserResponseModel> Create(CreateUserRequestModel requestModel)
    {
        var usersWithSameEmailAndUserName = await Context.GetAllAsync<User>(u => u.UserName.ToLower() == requestModel.UserName.ToLower() || u.Email.ToLower() == requestModel.Email.ToLower());
        if (usersWithSameEmailAndUserName.Any())
        {
            if (usersWithSameEmailAndUserName.Any(u => u.UserName.ToLower() == requestModel.UserName.ToLower()))
                throw new BusinessLogicException(ExceptionMessages.UserWithTheSameUserNameAlreadyExist.Format(requestModel.UserName));

            throw new BusinessLogicException(ExceptionMessages.UserWithTheSameEmailAlreadyExist.Format(requestModel.Email));
        }

        return await Create<CreateUserRequestModel>(requestModel);
    }

    public async Task Update(UpdateUserRequestModel requestModel)
    {
        await Update(requestModel, requestModel.Guid);
    }

    public async Task Update(Guid userId, UserRequestModel requestModel)
    {
        await Update(requestModel, userId);
    }

    public async Task UpdateUsername(UpdateUserUsernameRequestModel requestModel)
    {
        var user = await Context.GetByIdAsync<User>(requestModel.Guid);
        if (user == null)
            throw new EntityNotFoundException(ModelsLabel.User, requestModel.Guid);

        var usersWithSameUserName = await Context.GetFirstOrDefaultAsync<User>(u => u.UserName.ToLower() == requestModel.UserName.ToLower() && u.Id != requestModel.Guid);
        if (usersWithSameUserName != null)
            throw new BusinessLogicException(ExceptionMessages.UserWithTheSameUserNameAlreadyExist.Format(requestModel.UserName));

        user.UserName = requestModel.UserName;
        await Context.SaveChangesAsync();
    }

    public async Task<IPagedList<UserResponseModel>> GetPagedList(IPagedListRequestModel<IUser> requestModel) => await base.GetPagedList(requestModel as IPagedListRequestModel<User>);

    protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(User entity)
    {
        return (false, ExceptionMessages.UserCannotBeDeleted);
    }
    protected override string GetDefaultSearchPropertyName() => nameof(User.FullName);
    protected override string GetDefaultPropertyNameToOrderBy() => nameof(User.FullName);
    protected override string GetEntityLabel() => ModelsLabel.User;
}