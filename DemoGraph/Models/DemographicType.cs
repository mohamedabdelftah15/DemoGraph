using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoGraph.Models
{
    public partial class DemographicType
    {
        public DemographicType()
        {
            DemographicTypeDtl = new HashSet<DemographicTypeDtl>();
        }

        [Key]
        [Column("DemoTypeID")]
        public int DemoTypeId { get; set; }
        [Required]
        [Column("TypeDescAR")]
        [StringLength(150)]
        public string TypeDescAr { get; set; }
        [Column("TypeDescEN")]
        [StringLength(150)]
        public string TypeDescEn { get; set; }
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

        [InverseProperty("DemoType")]
        public ICollection<DemographicTypeDtl> DemographicTypeDtl { get; set; }
    }
}
