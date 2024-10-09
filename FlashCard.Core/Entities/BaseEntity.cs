using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Entities
{
    public abstract class BaseEntity<T> : IHasUpdatedDate
    {
        public T? Id { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
    public interface IHasUpdatedDate
    {
        public string? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
