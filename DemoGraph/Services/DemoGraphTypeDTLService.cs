using DemoGraph.Models;
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
    public class DemoGraphTypeDTLService : DemoGraphTypeDTLIService
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        public DemoGraphTypeDTLService(IUnitOfWork uof,ILogger<DemoGraphTypeDTLService> logger)
        {
            _uof = uof;
            _logger = logger;
        }
        public List<DemographicTypeDtlVM> GetAllByTypeID(int?id)
        {
            List<DemographicTypeDtlVM> list = new List<DemographicTypeDtlVM>();
            try
            {
                list = _uof.DemoGraphTypeDTLIRepository.GetAll().Where(e=>e.DemoTypeId==id.Value&&e.IsDeleted==false).Select(e => new DemographicTypeDtlVM
                {
                    DemTypeDtlId = e.DemTypeDtlId,
                    DemoTypeId = e.DemoTypeId,
                    ChoiceAr = e.ChoiceAr,
                    ChoiceEn = e.ChoiceEn,
                    WeightValue = e.WeightValue,
                    DemoTypeVM = new DemographicTypeVM
                    {
                        DemoTypeId = e.DemoType.DemoTypeId,
                        TypeDescAr = e.DemoType.TypeDescAr,
                        TypeDescEn = e.DemoType.TypeDescEn
                    }
                }).ToList();
                return list;
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeDTLService/GetAll--> {ex.Message}");
            }
            return list;
        }

        public DemographicTypeDtlVM GetById(int? id)
        {
            DemographicType typeVM = new DemographicType();
            DemographicTypeDtl dtl = new DemographicTypeDtl();
            DemographicTypeDtlVM dtlVM = new DemographicTypeDtlVM();
            try
            {
                dtl = _uof.DemoGraphTypeDTLIRepository.GetById(id);
                if (dtl!=null&&dtl.IsDeleted==false)
                {
                    typeVM = _uof.DemoGraphTypeIRepository.GetById(dtl.DemoTypeId);
                    if (typeVM!=null)
                    {
                        dtlVM = new DemographicTypeDtlVM
                        {
                            DemTypeDtlId = dtl.DemTypeDtlId,
                            DemoTypeId = dtl.DemoTypeId,
                            ChoiceAr = dtl.ChoiceAr,
                            ChoiceEn = dtl.ChoiceEn,
                            WeightValue = dtl.WeightValue,
                            DemoTypeVM = new DemographicTypeVM
                            {
                                DemoTypeId = typeVM.DemoTypeId,
                                TypeDescAr = typeVM.TypeDescAr,
                                TypeDescEn = typeVM.TypeDescEn
                            }
                        };
                        return dtlVM;
                    }
                    
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeDTLService/GetById--> {ex.Message}");
            }
            return dtlVM;
        }
        public async Task<DemographicTypeDtlVM> AddORUpdate(DemographicTypeDtlVM entity)
        {
            DemographicTypeDtl dtl = new DemographicTypeDtl();
            try
            {
                if (entity!=null)
                {
                    if (entity.DemTypeDtlId == 0)
                    {
                        dtl = new DemographicTypeDtl
                        {
                            DemoTypeId = entity.DemoTypeId,
                            ChoiceAr = entity.ChoiceAr,
                            ChoiceEn = entity.ChoiceEn,
                            WeightValue = entity.WeightValue,
                            CreatedBy = "Demo",
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                       await _uof.DemoGraphTypeDTLIRepository.AddAsync(dtl);
                    }
                    else
                    {
                        dtl = _uof.DemoGraphTypeDTLIRepository.GetById(entity.DemTypeDtlId);
                        if (dtl != null)
                        {
                            dtl.DemoTypeId = entity.DemoTypeId;
                            dtl.ChoiceAr = entity.ChoiceAr;
                            dtl.ChoiceEn = entity.ChoiceEn;
                            dtl.WeightValue = entity.WeightValue;
                            dtl.UpdatedBy = "Demo";
                            dtl.UpdatedDate = DateTime.Now;
                        }    
                    }
                     _uof.DemoGraphTypeDTLIRepository.Update(dtl);
                   var res= await _uof.Complete();
                    entity.DemTypeDtlId = dtl.DemTypeDtlId;
                    return entity;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeDTLService/AddORUpdate--> {ex.Message}");
            }
            return null;
        }

        public async Task<bool> AddORUpdateRangeAsync(List<DemographicTypeDtlVM> entities)
        {
           List<DemographicTypeDtl> updatedList = new List<DemographicTypeDtl>();
           List<DemographicTypeDtl> addedList = new List<DemographicTypeDtl>();
           List<DemographicTypeDtl> deletedList = new List<DemographicTypeDtl>();
           List<int> listDTLIds = new List<int>();
            try
            {
                if (entities != null)
                {
                    int? typeID = entities.FirstOrDefault(e=>e.DemoTypeId>0).DemoTypeId;
                    if (typeID!=null&&typeID!=0)
                    {
                        deletedList = GetAllByTypeID(typeID).Where(e => entities.All(x => x.DemTypeDtlId != e.DemTypeDtlId)).Select(e => new DemographicTypeDtl { DemTypeDtlId = e.DemTypeDtlId, DemoTypeId = e.DemoTypeId, ChoiceEn = e.ChoiceEn, ChoiceAr = e.ChoiceAr, CreatedBy = "Demo", CreatedDate = DateTime.Now, WeightValue = e.WeightValue, IsDeleted = true }).ToList();
                    }
                    updatedList = entities.Where(x=>x.DemTypeDtlId>0).Select(e => new DemographicTypeDtl { DemTypeDtlId = e.DemTypeDtlId, DemoTypeId = e.DemoTypeId, ChoiceEn = e.ChoiceEn, ChoiceAr = e.ChoiceAr, CreatedBy = "Demo", CreatedDate = DateTime.Now, WeightValue = e.WeightValue, IsDeleted = false }).ToList();
                    addedList = entities.Where(x=>x.DemTypeDtlId==0).Select(e => new DemographicTypeDtl { DemTypeDtlId = e.DemTypeDtlId, DemoTypeId = e.DemoTypeId, ChoiceEn = e.ChoiceEn, ChoiceAr = e.ChoiceAr, CreatedBy = "Demo", CreatedDate = DateTime.Now, WeightValue = e.WeightValue, IsDeleted = false }).ToList();

                    if (updatedList.Count>0)
                    {
                        _uof.DemoGraphTypeDTLIRepository.UpdateRange(updatedList);
                    }
                    if (addedList.Count > 0)
                    {
                      await _uof.DemoGraphTypeDTLIRepository.AddRangeAsync(addedList);
                    }
                    if (deletedList.Count > 0)
                    {
                        _uof.DemoGraphTypeDTLIRepository.UpdateRange(deletedList);
                    }
                    var res=await _uof.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeDTLService/AddORUpdateRange--> {ex.Message}");
            }
            return false;
        }

        public bool Remove(int?id)
        {
            DemographicTypeDtl dtl = new DemographicTypeDtl();
            try
            {
                dtl = _uof.DemoGraphTypeDTLIRepository.GetById(id);
                if (dtl!=null)
                {
                    dtl.IsDeleted = true;
                    _uof.DemoGraphTypeDTLIRepository.Update(dtl);
                    _uof.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"DemoGraphTypeDTLService/Remove--> {ex.Message}");
            }
            return false;
        }

        public bool RemoveRange(IEnumerable<DemographicTypeDtlVM> entities)
        {
            throw new NotImplementedException();
        }

        
    }
}
