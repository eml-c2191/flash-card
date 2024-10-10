using FlashCard.Abstract.Exceptions;
using FlashCard.Abstract.Responses;
using FlashCard.Business.Models;
using FlashCard.Core.Entities;
using FlashCard.Core.Repositories.Abstract;
using FlashCard.Core.UnitOfWork;

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
                Content = cardDto.Content,
                Address = cardDto.Address
            };

            await _repository.AddAsync(newEntity);
            await _unitOfWorkService.SaveChangeAsync();
            return newEntity.Id;

        }

        public async Task DeleteCardAsync(int cardId, int registrationId)
        {
            Card? existedCard = await _repository.FindAsync(cardId);

            if (existedCard is null || existedCard.RegistrationId != registrationId)
                throw new BusinessValidationException("Card Not Found");
            _repository.SoftRemove(existedCard);
            await _unitOfWorkService.SaveChangeAsync();
        }

        public async Task<PagingResultDto<CardResponse>> GetCardsAsync(int registrationId,
            int pageNo = BusinessConstants.DefaultPageNo,
            int pageSize = BusinessConstants.DefaultPageSize)
        {
            pageNo = pageNo < 1 ? 1 : pageNo;
            pageSize = pageSize < 0 ? 1 : pageSize;


            return await _repository.BuildQuery()
                .FilterByRegistrationId(registrationId)
                .AsPagination(c => new CardResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Date = c.Date,
                    Address = c.Address,
                    Time = c.Time,
                    CardType = c.CardType,
                    Content = c.Content,
                }, pageNo, pageSize);
        }

        public async  Task UpdateCardAsync(int cardId, CardDto card, int registrationId)
        {
            Card? existedCard = await _repository.FindAsync(cardId);

            if (existedCard is null || existedCard.RegistrationId != registrationId)
                throw new BusinessValidationException("Card Not Found");
            //TODO: implement validation duplicated time
            existedCard.Title = card.Title;
            existedCard.Date = card.Date;
            existedCard.Address = card.Address;
            existedCard.Time = card.Time;
            existedCard.CardType = card.CardType;
            existedCard.Content = card.Content;

            await _unitOfWorkService.SaveChangeAsync();
        }
    }
}
