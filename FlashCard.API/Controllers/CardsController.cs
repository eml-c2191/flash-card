using FlashCard.Abstract.Exceptions;
using FlashCard.Abstract.Request;
using FlashCard.API.Models.Validations;
using FlashCard.Business.Cards;
using FlashCard.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashCard.API.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailedResult), StatusCodes.Status400BadRequest)]
    public class CardsController : FLCBaseController
    {
        private readonly ICardServices _cardServices;
        public CardsController(ICardServices cardServices)
        {
            _cardServices = cardServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCardAsync(CardRequest request)
        {
            try
            {
                //await ValidateDuplicateCardAsync(registrationId, card);
                CardDto input = new ()
                {
                    Title = request.Title,
                    Date = request.Date?.Date,
                    CardType = request.CardType,
                    Time = request.Time,
                    Content = request.Content,
                    Address = request.Address
                };

                //_iWAppointmentServices.Validate(input);

                //int? registrationId = RegistrationId;

                //if (!registrationId.HasValue)
                //{
                //    return CustomBadRequestResult.Fail(ApiConstants.InvalidRegistrationId);
                //}

                int cardId = await _cardServices.CreateAsync(input, 1);

                return Ok(new IdJson
                {
                    Id = cardId
                });

            }
            catch (BusinessValidationException e)
            {
                return CustomBadRequestResult.Fail(e.Message);
            }
        }
    }
}
