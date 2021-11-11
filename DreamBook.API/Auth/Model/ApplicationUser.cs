using DreamBook.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.ObjectModel;

namespace DreamBook.API.Auth.Model
{
    public class ApplicationUser : IdentityUser<Guid>, IUser
    {
        public ApplicationUser()
        {
            RefreshTokens = new Collection<RefreshToken>();
        }

        public Guid Guid => Id;

        public virtual Collection<RefreshToken> RefreshTokens { get; set; }
    }
}
