using DemoGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGraph.Services.IServices
{
   public interface DemoGraphTypeIService
    {
        List<DemographicTypeVM> GetAll();
        DemographicTypeVM GetById(int? id);
        Task<DemographicTypeVM> AddORUpdate(DemographicTypeVM entity);
        Task<bool> AddORUpdateRange(IEnumerable<DemographicTypeVM> entities);
        bool Remove(int?id);
        bool RemoveRange(IEnumerable<DemographicTypeVM> entities);
    }
}
