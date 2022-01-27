using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using DreamBook.Persistence.TableConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DreamBook.Persistence.Database
{
    public class DreamBookContext : DbContext//, IContext
    {
        IConfiguration _configuration;

        public DreamBookContext() : base() { }

        public DreamBookContext(IConfiguration configuration) : base()
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EntityBase>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }


        #region Add

        public void AddRange(IEnumerable<IEntity> entities) => base.AddRange(entities);

        public void Add(IEntity entity) => base.Add(entity);



        #endregion


        #region Save
        public override int SaveChanges() => base.SaveChanges();

        #endregion

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
        {
            if (predicate == null)
                return Set<T>().Count();

            return Set<T>().Where(predicate).Count();
        }

        #region Dispose
        public override void Dispose()
        {
            _configuration = null;
            base.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public override ValueTask DisposeAsync()
        {
            _configuration = null;
            return base.DisposeAsync();
        }
        #endregion
    }
}
