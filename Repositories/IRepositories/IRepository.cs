using System.Linq.Expressions;

namespace Clinic_Management_System.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        // ---------- Create ----------
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        // ---------- Update ----------
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        // ---------- Delete ----------
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        // ---------- Read ----------

        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,    
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool tracked = true,
            CancellationToken cancellationToken = default);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool tracked = true,
            CancellationToken cancellationToken = default);

        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool tracked = false,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default);

        // ---------- Save ----------
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
