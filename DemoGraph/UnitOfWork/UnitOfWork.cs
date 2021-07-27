using DemoGraph.Data;
using DemoGraph.Models;
using DemoGraph.Repositories;
using DemoGraph.Repositories.IRepositories;
using DemoGraph.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGraph.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoGraphContext _context;
        public UnitOfWork(DemoGraphContext context, ILogger<DemoGraphTypeRepository> logger, ILogger<DemoGraphTypeDTLRepository> logger2)
        {
            _context = context;
            DemoGraphTypeIRepository = new DemoGraphTypeRepository(_context,logger);
            DemoGraphTypeDTLIRepository = new DemoGraphTypeDTLRepository(_context,logger2);
        }
        
        public DemoGraphTypeIRepository DemoGraphTypeIRepository { get; private set; }

        public DemoGraphTypeDTLIRepository DemoGraphTypeDTLIRepository { get; private set; }

        public async Task<bool> Complete()
        {
            try
            {
                int result= await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            
        }
        public void Dispose()
        {
           _context.Dispose();
        }
    }
}
