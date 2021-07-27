using DemoGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGraph.Services.IServices
{
   public interface DemoGraphTypeDTLIService
    {
        List<DemographicTypeDtlVM> GetAllByTypeID(int?id);
        DemographicTypeDtlVM GetById(int? id);
        Task<DemographicTypeDtlVM> AddORUpdate(DemographicTypeDtlVM entity);
        Task<bool> AddORUpdateRangeAsync(List<DemographicTypeDtlVM> entities);
        bool Remove(int?id);
        bool RemoveRange(IEnumerable<DemographicTypeDtlVM> entities);
    }
}
