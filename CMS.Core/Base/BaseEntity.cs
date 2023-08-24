using System;
using System.ComponentModel.DataAnnotations;

namespace YIT.Core.Base
{
    public partial class BaseEntity<T> : Auditable
    {
        protected BaseEntity()
        {
        }

        [Key]
        public T Id { get; set; }

        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
    }

    public partial class BaseEntity : BaseEntity<Guid>
    {
    }
}