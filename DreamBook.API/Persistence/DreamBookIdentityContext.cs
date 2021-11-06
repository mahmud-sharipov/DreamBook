using DreamBook.API.Auth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DreamBook.API.Persistence
{
    public class DreamBookIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public DreamBookIdentityContext(DbContextOptions<DreamBookIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            string connectionString = configuration["ConnectionStrings:DreamBookConnection"];
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}