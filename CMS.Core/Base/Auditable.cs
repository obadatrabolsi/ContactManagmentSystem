using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YIT.Core.Base
{
    public class Auditable : IAuditable
    {
        [MaxLength(50)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [MaxLength(50)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}