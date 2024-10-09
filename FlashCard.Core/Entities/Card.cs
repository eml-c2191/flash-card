using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Entities
{
    public class Card : BaseEntity<int>, IHasDateTime
    {
        public int RegistrationId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public string CardType { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
    public interface IHasDateTime
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        public TimeSpan? Time { get; set; }
    }
}
