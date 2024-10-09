using FlashCard.Business.Models;
using FlashCard.Core.Entities;
using FlashCard.Core.Repositories.Abstract;
using FlashCard.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Business.Cards
{
    public class CardServices : ICardServices
    {
        private readonly ICardRepository _repository;
        private readonly IUnitOfWorkService _unitOfWorkService;
        public CardServices(ICardRepository repository, IUnitOfWorkService unitOfWorkService)
        {
            _repository = repository;
            _unitOfWorkService = unitOfWorkService;
        }
        public async Task<int> CreateAsync(CardDto cardDto, int registrationId)
        {

            Card newEntity = new()
            {
                Title = cardDto.Title,
                Date = cardDto.Date,
                CardType = cardDto.CardType,
                RegistrationId = registrationId,
                Time = cardDto.Time,
                Content = cardDto.Content
            };

            await _repository.AddAsync(newEntity);
            await _unitOfWorkService.SaveChangeAsync();
            return newEntity.Id;

        }

        public Task DeleteCardAsync(Guid cardId)
        {
            throw new NotImplementedException();
        }

        public Task<CardDto> GetCardAsync(Guid cardId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CardDto>> GetCardsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCardAsync(CardDto card)
        {
            throw new NotImplementedException();
        }
    }
}
