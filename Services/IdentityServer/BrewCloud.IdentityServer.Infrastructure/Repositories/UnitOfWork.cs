using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BrewCloud.IdentityServer.Infrastructure.Persistence;
using System.Linq;

namespace BrewCloud.IdentityServer.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;

        private IDbContextTransaction _trans;
        private string _errorMessage = string.Empty;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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

        public void CreateTransaction()
        {
            _trans = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _trans.Commit();
        }

        public void Rollback()
        {
            _trans.Rollback();
            _trans.Dispose();
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

                //foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                //throw new Exception(_errorMessage, dbEx);

                throw;
            }
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(string query, object parameters)
        {
            return _dbContext.SQLQuery<T>(query, parameters).ToList();
        }

        public List<T> Query<T>(string query)
        {
            return _dbContext.SQLQuery<T>(query).ToList();
        }

        public int Execute(string query, object parameters)
        {
            return _dbContext.Execute(query, parameters);
        }
    }
}
