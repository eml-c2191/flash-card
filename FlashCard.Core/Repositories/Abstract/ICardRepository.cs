using FlashCard.Core.Entities;
using FlashCard.Core.Queries.Abstract;

namespace FlashCard.Core.Repositories.Abstract
{
    public interface ICardRepository : IRepository<Card, int>
    {
        ICardQuery BuildQuery();
    }
}
