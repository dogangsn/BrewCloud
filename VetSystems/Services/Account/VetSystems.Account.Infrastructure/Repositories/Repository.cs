﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Infrastructure.Persistence;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        protected VetSystemsDbContext _dbContext;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMediator _mediator;
        public Repository(VetSystemsDbContext dbContext, IIdentityRepository identityRepository, IMediator mediator)
        {
            _dbContext = dbContext;
            _identityRepository = identityRepository;
            _mediator = mediator;
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


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

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

        public async Task<T> GetByIdAsync(Guid id)
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
        }

        public async Task<IEnumerable<T>> GetListAsync(IQueryable<T> query, List<Expression<Func<T, object>>> includes = null)
        {
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> objects)
        {
            await _dbContext.Set<T>().AddRangeAsync(objects);
            return objects;
        }

        public void ChangeDbContext(Tenant tenant)
        {
            var builder = new DbContextOptionsBuilder<VetSystemsDbContext>();
            builder.UseSqlServer(tenant.DatabaseConnectionString);
            _dbContext = new VetSystemsDbContext(builder.Options, null, _identityRepository, _mediator);
        }

        #region IDisposable Members
        // Burada IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
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
