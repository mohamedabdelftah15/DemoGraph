using DemoGraph.Models;
using DemoGraph.Repositories;
using DemoGraph.Services.IServices;
using DemoGraph.UnitOfWork;
using DemoGraph.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGraph.Services
{
    public class DemoGraphTypeService : DemoGraphTypeIService
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        public DemoGraphTypeService(IUnitOfWork uof, ILogger<DemoGraphTypeDTLService> logger)
        {
            _uof = uof;
            _logger = logger;
        }
        public List<DemographicTypeVM> GetAll()
        {
            List<DemographicTypeVM> list = new List<DemographicTypeVM>();
            try
            {
                list = _uof.DemoGraphTypeIRepository.GetAll().Where(e=>e.IsDeleted==false).Select(e => new DemographicTypeVM
                {
                    DemoTypeId = e.DemoTypeId,
                    TypeDescAr = e.TypeDescAr,
                    TypeDescEn = e.TypeDescEn,
                }).ToList();
                return list;
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeService/GetAll--> {ex.Message}");
            }
            return list;
        }
        public DemographicTypeVM GetById(int? id)
        {
            DemographicType dtl = new DemographicType();
            DemographicTypeVM dtlVM = new DemographicTypeVM();
            try
            {
                dtl = _uof.DemoGraphTypeIRepository.GetById(id);
                if (dtl != null && dtl.IsDeleted==false)
                {
                    dtlVM = new DemographicTypeVM
                    {
                        DemoTypeId = dtl.DemoTypeId,
                        TypeDescAr = dtl.TypeDescAr,
                        TypeDescEn = dtl.TypeDescEn,
                    };
                    return dtlVM;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeService/GetById--> {ex.Message}");
            }
            return dtlVM;
        }
        public async Task<DemographicTypeVM> AddORUpdate(DemographicTypeVM entity)
        {
            DemographicType dtl = new DemographicType();
            try
            {
                if (entity != null)
                {
                    if (entity.DemoTypeId == 0)
                    {
                        dtl = new DemographicType
                        {
                            DemoTypeId = entity.DemoTypeId,
                            TypeDescAr = entity.TypeDescAr,
                            TypeDescEn = entity.TypeDescEn,
                            CreatedBy = "Demo",
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                       var demoGraphType= await _uof.DemoGraphTypeIRepository.AddAsync(dtl);
                    }
                    else
                    {
                        dtl = _uof.DemoGraphTypeIRepository.GetById(entity.DemoTypeId);
                        if (dtl != null)
                        {
                            dtl.DemoTypeId = entity.DemoTypeId;
                            dtl.TypeDescAr = entity.TypeDescAr;
                            dtl.TypeDescEn = entity.TypeDescEn;
                            dtl.UpdatedBy = "Demo";
                            dtl.UpdatedDate = DateTime.Now;
                        }
                    }
                    var res=await _uof.Complete();
                    entity.DemoTypeId = dtl.DemoTypeId;
                    return entity;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeService/AddORUpdate--> {ex.Message}");
            }
            return null;
        }
        public Task<bool> AddORUpdateRange(IEnumerable<DemographicTypeVM> entities)
        {
            throw new NotImplementedException();
        }
        public bool Remove(int? id)
        {
            DemographicType dtl = new DemographicType();
            try
            {
                dtl = _uof.DemoGraphTypeIRepository.GetById(id);
                dtl.IsDeleted = true;
                _uof.DemoGraphTypeIRepository.Update(dtl);
                var res=_uof.Complete();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeService/Remove--> {ex.Message}");
            }
            return false;
        }
        public bool RemoveRange(IEnumerable<DemographicTypeVM> entities)
        {
            throw new NotImplementedException();
        }
    }

}

