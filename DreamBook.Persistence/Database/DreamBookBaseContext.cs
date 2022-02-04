using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using DreamBook.Persistence.TableConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DreamBook.Persistence.Database
{
    public abstract class DreamBookBaseContext : DbContext, IContext
    {
        DreamBookBaseContext() { }

        public DreamBookBaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EntityBase>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public int? CommandTimeout
        {
            get => Database.GetCommandTimeout();
            set => Database.SetCommandTimeout(value);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity
        {
            return await GetQuery(predicate, orderBy).ToListAsync();
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity
        {
            return GetQuery(predicate, orderBy);
        }

        public async Task<T> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : class, IEntity =>

            await Set<T>().FirstOrDefaultAsync(e => e.Guid == id, cancellationToken);

        public T GetById<T>(Guid guid) where T : class, IEntity => Find<T>(guid);

        public T GetFirstOrDefault<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity
        {
            return GetQuery(predicate, orderBy, false).FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity
        {
            return await GetQuery(predicate, orderBy).FirstOrDefaultAsync();
        }

        public int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class, IEntity
        {
            if (predicate == null)
                return Set<T>().Count();

            return Set<T>().Where(predicate).Count();
        }

        public async Task AddRangeAsync(IEnumerable<IEntity> entities, CancellationToken cancellationToken = default) => await base.AddRangeAsync(entities, cancellationToken);

        public void AddRange(IEnumerable<IEntity> entities) => base.AddRange(entities);

        public void Add(IEntity entity) => base.Add(entity);

        public async Task AddAsync(IEntity entity) => await base.AddAsync(entity).AsTask();

        public void UpdateRange(IEnumerable<IEntity> entities) => base.UpdateRange(entities);

        public void Update<T>(Guid id, T entity) where T : class, IEntity => base.Update(entity);

        public void DeleteRange(IEnumerable<IEntity> entities) => base.RemoveRange(entities);

        public void Delete<T>(T entity) where T : class, IEntity => Remove(entity);

        public override int SaveChanges() => base.SaveChanges(true);

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) =>
          await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await base.SaveChangesAsync(cancellationToken);

        public override int SaveChanges(bool acceptAllChangesOnSuccess) =>
            base.SaveChanges(acceptAllChangesOnSuccess);

        public bool HasChange() => ChangeTracker.HasChanges();

        public void UndoChanges()
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> predicate = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  bool disableTracking = false) where T : class, IEntity
        {
            IQueryable<T> query = Set<T>();
            if (disableTracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }
    }
}
