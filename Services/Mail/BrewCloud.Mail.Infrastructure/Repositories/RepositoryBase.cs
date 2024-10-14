using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Domain.Common;
using BrewCloud.Mail.Domain.Contracts;
using BrewCloud.Mail.Infrastructure.Persistance;

namespace BrewCloud.Mail.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected MailDbContext _dbContext;

        public RepositoryBase(MailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool disableTracking = false)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {


            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool disableTracking = false)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, bool disableTracking = false)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString).AsSplitQuery();


            }

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdIntAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity).Property(x => x.Id).IsModified = false;
        }
        public void UpdateState(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.Update(entity).Property(x => x.Id).IsModified = false;
        }
        public async Task<IEnumerable<T>> GetListAsync(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> objects)
        {
            await _dbContext.Set<T>().AddRangeAsync(objects);
            return objects;
        }

        public void ChangeDbContext(string connection)
        {
            var builder = new DbContextOptionsBuilder<MailDbContext>();
            builder.UseSqlServer(connection);

            _dbContext = new MailDbContext(builder.Options, null);

        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
                // sadece değişen alanları güncellemeyide sağlayabiliriz.
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw;
            }
        }

        #region IDisposable Members   
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
