using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBook.Infrastructure.Repositories;

namespace GameBook.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
