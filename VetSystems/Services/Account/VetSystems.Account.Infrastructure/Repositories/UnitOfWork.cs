using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Infrastructure.Persistence;
using VetSystems.Shared.Accounts;

namespace VetSystems.Account.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected VetSystemsDbContext _context;
        private readonly IMediator _mediator;

        public UnitOfWork(VetSystemsDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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

        public async Task MigrateDatabase(Tenant _tenant)
        {
            var builder = new DbContextOptionsBuilder<VetSystemsDbContext>();
            builder.UseSqlServer(_tenant.ConnectionString);
            using (var db = new VetSystemsDbContext(builder.Options, _mediator))
            {
                await db.Database.MigrateAsync();
            }
        }

    }
}
