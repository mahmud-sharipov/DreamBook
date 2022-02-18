namespace DreamBook.Domain.Interfaces;

public interface IUser : IEntity
{
    string UserName { get; set; }
    string Email { get; set; }
    string FullName { get; set; }
    string AvatarImage { get; set; }
}
