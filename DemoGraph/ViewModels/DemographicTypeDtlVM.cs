using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoGraph.ViewModels
{
    public  class DemographicTypeDtlVM
    {
        public int DemTypeDtlId { get; set; }
        public int DemoTypeId { get; set; }
        [Required(ErrorMessage ="")]
        public string ChoiceAr { get; set; }
        [Required(ErrorMessage = "")]
        public string ChoiceEn { get; set; }
        public int WeightValue { get; set; }
        public DemographicTypeVM DemoTypeVM { get; set; }
    }
}
