using FlashCard.Abstract.Responses;
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
        Task UpdateCardAsync(int cardId, CardDto card, int registrationId);
        Task<PagingResultDto<CardResponse>> GetCardsAsync(int registrationId,
            int pageNo = BusinessConstants.DefaultPageNo,
            int pageSize = BusinessConstants.DefaultPageSize);
        Task DeleteCardAsync(int cardId, int registrationId);
    }
}
