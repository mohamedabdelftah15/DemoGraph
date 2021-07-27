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
    public class DemoGraphTypeRepository:Repository<DemographicType>,DemoGraphTypeIRepository
    {
        public DemoGraphTypeRepository(DemoGraphContext context,ILogger<DemoGraphTypeRepository> log):base(context,log)
        {

        }
    }
}
