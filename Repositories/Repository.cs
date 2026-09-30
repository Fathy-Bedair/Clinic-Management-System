using System.Linq.Expressions;

namespace Clinic_Management_System.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // ---------- Create ----------

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var result = await _dbSet.AddAsync(entity, cancellationToken);
            return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        // ---------- Update ----------

        public void Update(T entity) => _dbSet.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

        // ---------- Delete ----------

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        // ---------- Read ----------

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool tracked = true,
            CancellationToken cancellationToken = default)
        {
            var query = BuildQuery(expression, include, orderBy, tracked);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetOneAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool tracked = true,
            CancellationToken cancellationToken = default)
        {
            var query = BuildQuery(expression, include, orderBy, tracked);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool tracked = false,
            CancellationToken cancellationToken = default)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalCount = await CountAsync(expression, cancellationToken);

            var items = await BuildQuery(expression, include, orderBy, tracked)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default)
        {
            return expression is null
                ? await _dbSet.AnyAsync(cancellationToken)
                : await _dbSet.AnyAsync(expression, cancellationToken);
        }

        public async Task<int> CountAsync(
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default)
        {
            return expression is null
                ? await _dbSet.CountAsync(cancellationToken)
                : await _dbSet.CountAsync(expression, cancellationToken);
        }

        // ---------- Save ----------

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        // ---------- Helpers ----------

        private IQueryable<T> BuildQuery(
            Expression<Func<T, bool>>? expression,
            Func<IQueryable<T>, IQueryable<T>>? include,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            bool tracked)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (expression is not null)
                query = query.Where(expression);

            if (include is not null)
                query = include(query);

            if (orderBy is not null)
                query = orderBy(query);

            return query;
        }
    }
}
