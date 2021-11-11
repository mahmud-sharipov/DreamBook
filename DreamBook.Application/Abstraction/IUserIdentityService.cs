using DreamBook.Domain.Interfaces;

namespace DreamBook.Application.Abstraction
{
    public interface IUserIdentityService
    {
        IUser GetCurrentUser();
    }
}
