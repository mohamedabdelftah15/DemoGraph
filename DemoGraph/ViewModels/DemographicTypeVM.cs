using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoGraph.ViewModels
{
    public class DemographicTypeVM
    {
        public int DemoTypeId { get; set; }
        [Required(ErrorMessage = "")]
        public string TypeDescAr { get; set; }
        [Required(ErrorMessage = "")]
        public string TypeDescEn { get; set; }
       
        public IList<DemographicTypeDtlVM> DemographicTypeDtlVM { get; set; }
    }
}
