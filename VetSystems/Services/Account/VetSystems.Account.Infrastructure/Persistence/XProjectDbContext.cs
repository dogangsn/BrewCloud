using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Entities;

namespace VetSystems.Account.Infrastructure.Persistence
{
    public class VetSystemsDbContext : DbContext
    {
        private readonly IMediator _mediator;
        public VetSystemsDbContext(DbContextOptions options, IMediator mediator) : base(options)
        { 
            _mediator = mediator;   
        }

        public DbSet<Products> Products { get; set; }

    }
}
