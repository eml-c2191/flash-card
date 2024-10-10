using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.Responses
{
    public record PagingResultDto<TOutput>
    {
        public int Total { get; set; }

        public IEnumerable<TOutput> Items { get; set; } = Enumerable.Empty<TOutput>();

        public static PagingResultDto<TOutput> Empty => new PagingResultDto<TOutput>
        {
            Total = 0,
            Items = Enumerable.Empty<TOutput>()
        };
    }
}
