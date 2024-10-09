using FlashCard.Core.Entities;
using FlashCard.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Queries
{
    public class CardQuery : BaseQuery<Card, int>, ICardQuery
    {
        public CardQuery(IQueryable<Card> query) : base(query)
        {
        }
    }
}
