using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;

namespace DreamBook.API.Services;

public class UserService : IUserService<User>
{
    private UserManager<User> _userManager;
    private IContext _context;
    private IMapper _mapper;

    public UserService(UserManager<User> userManager, IContext context, IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserResponseModel> GetById(Guid id)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());
        if (entity == null)
            throw new EntityNotFoundException(ModelsLabel.User, id);

        return _mapper.Map<UserResponseModel>(entity);
    }

    public async Task<UserResponseModel> GetByUserName(string userName)
    {
        var entity = await _userManager.FindByNameAsync(userName);
        if (entity == null)
            throw new EntityNotFoundException(ModelsLabel.User, userName);

        return _mapper.Map<UserResponseModel>(entity);
    }

    public async Task<UserResponseModel> GetByEmail(string email)
    {
        var entity = await _userManager.FindByEmailAsync(email);
        if (entity == null)
            throw new EntityNotFoundException(ModelsLabel.User, email);

        return _mapper.Map<UserResponseModel>(entity);
    }

    public async Task<IEnumerable<UserResponseModel>> GetAll()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserResponseModel>>(users);
    }

    public async Task<IPagedList<UserResponseModel>> GetPagedList(IPagedListRequestModel<User> requestModel)
    {
        var entities = requestModel.Filter(_userManager.Users, nameof(User.UserName), nameof(User.UserName));
        var result = new PagedList<User, UserResponseModel>(entities, l => _mapper.Map<IEnumerable<UserResponseModel>>(l), requestModel.PageNumber, requestModel.PageSize);
        return await Task.FromResult(result);
    }

    public async Task<UserResponseModel> Create(CreateUserRequestModel requestModel)
    {
        var entity = _mapper.Map<User>(requestModel);
        var sameUser = await _userManager.FindByNameAsync(requestModel.UserName);
        if (sameUser != null)
            throw new BusinessLogicException(ExceptionMessages.UserWithTheSameUserNameAlreadyExist.Format(requestModel.UserName));
        sameUser = await _userManager.FindByEmailAsync(requestModel.Email);
        if (sameUser != null)
            throw new BusinessLogicException(ExceptionMessages.UserWithTheSameEmailAlreadyExist.Format(requestModel.Email));

        var result = await _userManager.CreateAsync(entity, requestModel.Password);

        if (!result.Succeeded)
            throw new BusinessLogicException("User creation failed! Please check info details and try again.");

        await _userManager.AddToRoleAsync(entity, UserRoles.Basic);
        return _mapper.Map<UserResponseModel>(entity);
    }

    public async Task Update(UpdateUserRequestModel requestModel)
    {
        await Update(requestModel.Guid, requestModel);
    }

    public async Task Update(Guid userId, UserRequestModel requestModel)
    {
        var entity = await _userManager.FindByIdAsync(userId.ToString());
        if (entity == null)
            throw new EntityNotFoundException(ModelsLabel.User, userId);

        _mapper.Map(requestModel, entity);
        await _userManager.UpdateAsync(entity);
    }

    public async Task UpdateUsername(UpdateUserUsernameRequestModel requestModel)
    {
        var user = await _userManager.FindByIdAsync(requestModel.Guid.ToString());
        if (user == null)
            throw new EntityNotFoundException(ModelsLabel.User, requestModel.Guid);

        var userWithSameUserName = await _userManager.FindByNameAsync(requestModel.UserName);
        if (userWithSameUserName == null)
        {
            user.UserName = requestModel.UserName;
            await _context.SaveChangesAsync();
        }
        if (userWithSameUserName.Guid != user.Guid)
            throw new BusinessLogicException(ExceptionMessages.UserWithTheSameUserNameAlreadyExist.Format(requestModel.UserName));
    }

    public async Task Delete(Guid id)
    {
        var user = await _context.GetByIdAsync<User>(id);
        if (user == null)
            throw new EntityNotFoundException(ModelsLabel.User, id);

        _context.DeleteRange(user.Dreams);
        _context.Delete(user);
        await _context.SaveChangesAsync();
    }
}
