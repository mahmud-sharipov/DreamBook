﻿namespace DreamBook.Application.Users;

public class UserRequestModel : IRequestModel
{
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string AvatarImage { get; set; }
}
