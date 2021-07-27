using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGraph.Models;
using DemoGraph.Services.IServices;
using DemoGraph.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoGraph.Controllers
{
    public class DemoGraphTypeController : Controller
    {
        private readonly DemoGraphTypeIService _typeIService;
        private readonly DemoGraphTypeDTLIService _typeDTLIService;
        private readonly ILogger<DemoGraphTypeController> _log;
        public DemoGraphTypeController(DemoGraphTypeIService typeIService,DemoGraphTypeDTLIService typeDTLIService, ILogger<DemoGraphTypeController> log)
        {
            _typeDTLIService = typeDTLIService;
            _typeIService = typeIService;
            _log = log;
        }
        // GET: DemoGraphType
        [HttpPost]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_typeIService.GetAll());
            }
            catch (Exception ex)
            {
                _log.LogError($"DemoGraphTypeController/GetAll--> {ex.Message}");
            }
            return View();
        }


        [HttpPost]
        public ActionResult GetByID(int?id)
        {
            try
            {
                return Ok(_typeIService.GetById(id));
            }
            catch (Exception ex)
            {
                _log.LogError($"DemoGraphTypeController/GetByID--> {ex.Message}");
            }
            return View();
        }

        // POST: DemoGraphType/AddORUpdate
        [HttpPost]
        public async Task<bool> AddORUpdate(DemographicTypeVM type)
        {
            if (ModelState.IsValid)
            {
                DemographicTypeVM typeVM = new DemographicTypeVM();
                try
                {
                    typeVM = await _typeIService.AddORUpdate(type);
                    return true;
                }
                catch(Exception ex)
                {
                    _log.LogError($"DemoGraphTypeController/AddORUpdate--> {ex.Message}");
                }
            }
            
            return false;
        }
       
        // POST: DemoGraphType/Delete/5
        [HttpPost]
        public bool Delete(int? id)
        {
            try
            {
                return _typeIService.Remove(id);
            }
            catch(Exception ex)
            {
                _log.LogError($"DemoGraphTypeController/Delete--> {ex.Message}");
            }
            return false;
        }
    }
}