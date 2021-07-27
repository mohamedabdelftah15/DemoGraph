using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGraph.Services.IServices;
using DemoGraph.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoGraph.Controllers
{
    public class DemoGraphTypeDTLController : Controller
    {
        private readonly DemoGraphTypeIService _typeIService;
        private readonly DemoGraphTypeDTLIService _typeDTLIService;
        private readonly ILogger<DemoGraphTypeController> _log;
        public DemoGraphTypeDTLController(DemoGraphTypeIService typeIService, DemoGraphTypeDTLIService typeDTLIService, ILogger<DemoGraphTypeController> log)
        {
            _typeDTLIService = typeDTLIService;
            _typeIService = typeIService;
            _log = log;
        }
        // GET: DemoGraphTypeDTL
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAllByTypeID(int? id)
        {
            try
            {
                return Ok(_typeDTLIService.GetAllByTypeID(id));
            }
            catch (Exception ex)
            {
                _log.LogError($"DemoGraphTypeDTLController/GetAllByTypeID--> {ex.Message}");
            }
            return Ok(null);
        }

        [HttpPost]
        public IActionResult GetByID(int? id)
        {
            try
            {
                return Ok(_typeDTLIService.GetById(id));
            }
            catch (Exception ex)
            {
                _log.LogError($"DemoGraphTypeDTLController/GetByID--> {ex.Message}");
            }
            return Ok(null);
        }

        // POST: DemoGraphTypeDTL/AddORUpdate
        [HttpPost]
        public async Task<bool> AddORUpdate(DemoGraphVM demoGraphVM)
        {
                try
                {
                    if (demoGraphVM!=null)
                    {
                    
                   var demoGraphTypeVm= await _typeIService.AddORUpdate(demoGraphVM.DemographicType);
                    foreach (var item in demoGraphVM.DemographicTypeDtls)
                    {
                        item.DemoTypeId = demoGraphTypeVm.DemoTypeId;
                    }
                   var res= await _typeDTLIService.AddORUpdateRangeAsync(demoGraphVM.DemographicTypeDtls);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError($"DemoGraphTypeDTLController/AddORUpdate--> {ex.Message}");
                }
            
            return false;
        }

        // POST: DemoGraphTypeDTL/Delete/5
        [HttpPost]
        public bool Delete(int? id)
        {
            try
            {
                return _typeDTLIService.Remove(id);
            }
            catch (Exception ex)
            {
                _log.LogError($"DemoGraphTypeDTLController/Delete--> {ex.Message}");
            }
            return false;
        }
    }
}