using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Base
{
    public partial class BaseEntity<T>
    {
        protected BaseEntity()
        {
        }

        [Key]
        public T Id { get; set; }
    }

    public partial class BaseEntity : BaseEntity<Guid>
    {
    }
}