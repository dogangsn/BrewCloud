using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Chat.Infrastructure.Common;

namespace VetSystems.Chat.Infrastructure.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetListAsync(IQueryable<T> query);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, bool disableTracking = true);
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> objects);
        void Update(T entity);
        void Delete(T entity);

        void ChangeDbContext(string connection);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));


    }
}
