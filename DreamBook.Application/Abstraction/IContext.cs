using DreamBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DreamBook.Application.Abstraction
{
    public interface IContext : IDisposable
    {
        int? CommandTimeout { get; set; }

        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity;
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity;

        T GetById<T>(Guid id) where T : class, IEntity;
        Task<T> GetByIdAsync<T>(Guid guid, CancellationToken cancellationToken = default) where T : class, IEntity;

        T GetFirstOrDefault<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity;
        Task<T> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class, IEntity;

        int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class, IEntity;

        Task AddRangeAsync(IEnumerable<IEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
        void AddRange(IEnumerable<IEntity> entities);
        void Add(IEntity entity);
        Task AddAsync(IEntity entity);

        void Update<T>(Guid id, T entity) where T : class, IEntity;
        void UpdateRange(IEnumerable<IEntity> entities);

        void Delete<T>(T entity) where T : class, IEntity;
        void DeleteRange(IEnumerable<IEntity> entities);

        bool HasChange();

        void UndoChanges();

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges(bool acceptAllChangesOnSuccess);

    }
}
