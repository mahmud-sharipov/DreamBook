using DreamBook.API.Auth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DreamBook.API.Persistence
{
    public class DreamBookIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private IConfiguration _configuration;

        public DreamBookIdentityContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration == null)
                throw new ArgumentNullException("IConfiguration is null");

            optionsBuilder.SetupProviderOptions(_configuration);
            base.OnConfiguring(optionsBuilder);
        }

        #region Dispose
        public override void Dispose()
        {
            _configuration = null;
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            _configuration = null;
            return base.DisposeAsync();
        }
        #endregion
    }
}