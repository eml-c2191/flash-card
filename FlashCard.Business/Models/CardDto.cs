using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Business.Models
{
    public record CardDto
    {
        public string Content { get; set; } = string.Empty;
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public string CardType { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
