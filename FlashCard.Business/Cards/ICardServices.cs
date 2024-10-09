using FlashCard.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Business.Cards
{
    public interface ICardServices
    {
        Task<int> CreateAsync(CardDto card, int registrationId);
        Task UpdateCardAsync(CardDto card);
        Task<CardDto> GetCardAsync(Guid cardId);
        Task<IEnumerable<CardDto>> GetCardsAsync();
        Task DeleteCardAsync(Guid cardId);
    }
}
