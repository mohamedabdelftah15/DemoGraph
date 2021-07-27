using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoGraph.Models
{
    [Table("DemographicTypeDTL")]
    public partial class DemographicTypeDtl
    {
        [Key]
        [Column("DemTypeDTL_ID")]
        public int DemTypeDtlId { get; set; }
        [Column("DemoTypeID")]
        public int DemoTypeId { get; set; }
        [Required]
        [Column("ChoiceAR")]
        [StringLength(150)]
        public string ChoiceAr { get; set; }
        [Column("ChoiceEN")]
        [StringLength(150)]
        public string ChoiceEn { get; set; }
        public int WeightValue { get; set; }
        [Required]
        [StringLength(150)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [StringLength(150)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("DemoTypeId")]
        [InverseProperty("DemographicTypeDtl")]
        public DemographicType DemoType { get; set; }
    }
}
