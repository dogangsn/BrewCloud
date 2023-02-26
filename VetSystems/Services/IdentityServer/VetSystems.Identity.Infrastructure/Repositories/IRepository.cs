using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Identity.Infrastructure.Repositories
{
    public interface IRepository <T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
