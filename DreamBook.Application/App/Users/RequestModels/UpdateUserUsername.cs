namespace DreamBook.Application.Users;

public class UpdateUserUsernameRequestModel : IRequestModel
{
    public Guid Guid { get; set; }
    public string UserName { get; set; }
}
