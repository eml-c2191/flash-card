using FlashCard.Abstract.UserContext;
using FlashCard.Core.Entities;
using FlashCard.Core.Queries;
using FlashCard.Core.Queries.Abstract;
using FlashCard.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories
{
    public class CardRepository : BaseRepository<Card, int>, ICardRepository
    {
        public CardRepository(FlashCardDbContext context, IUserContext userContext) : base(context, userContext)
        {
        }

        public ICardQuery BuildQuery()
        {
            return new CardQuery(_context.Cards);
        }
    }
}
