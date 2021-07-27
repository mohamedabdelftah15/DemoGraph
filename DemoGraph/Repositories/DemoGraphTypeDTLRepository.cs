using DemoGraph.Data;
using DemoGraph.Models;
using DemoGraph.Repositories.Generics;
using DemoGraph.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGraph.Repositories
{
    public class DemoGraphTypeDTLRepository : Repository<DemographicTypeDtl>, DemoGraphTypeDTLIRepository
    {
        public DemoGraphTypeDTLRepository(DemoGraphContext context, ILogger<DemoGraphTypeDTLRepository> log) : base(context, log)
        {

        }
    }
}
