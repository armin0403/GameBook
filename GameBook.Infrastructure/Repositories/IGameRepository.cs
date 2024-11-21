using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBook.Core.Models;

namespace GameBook.Infrastructure.Repositories
{
    public interface IGameRepository : IBaseRepository<Game>
    {
    }
}
