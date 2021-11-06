namespace DreamBook.Domain.Interfaces
{
    public interface IUser : IEntity
    {
        string UserName { get; }
        string Email { get; }
    }
}
