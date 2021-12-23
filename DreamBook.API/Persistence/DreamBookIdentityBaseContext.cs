using DreamBook.API.Auth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DreamBook.API.Persistence
{
    public abstract class DreamBookIdentityBaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DreamBookIdentityBaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}