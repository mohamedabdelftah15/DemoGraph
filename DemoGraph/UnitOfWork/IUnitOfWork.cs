using DemoGraph.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGraph.UnitOfWork
{
   public interface IUnitOfWork:IDisposable
    {
        DemoGraphTypeIRepository DemoGraphTypeIRepository { get; }
        DemoGraphTypeDTLIRepository DemoGraphTypeDTLIRepository { get; }
        Task<bool> Complete();
    }
}
