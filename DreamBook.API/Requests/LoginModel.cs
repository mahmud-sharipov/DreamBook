﻿using DreamBook.Application.Abstraction.Request;

namespace DreamBook.API.Requests
{
    public class LoginModel : IRequestModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}