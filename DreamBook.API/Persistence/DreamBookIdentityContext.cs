using DreamBook.API.Auth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DreamBook.API.Persistence
{
    public class DreamBookIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DreamBookIdentityContext(DbContextOptions<DreamBookIdentityContext> options) : base(options)
        {
        }
    }
}