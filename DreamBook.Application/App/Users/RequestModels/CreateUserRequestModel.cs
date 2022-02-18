namespace DreamBook.Application.Users;

public class CreateUserRequestModel : UserRequestModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
