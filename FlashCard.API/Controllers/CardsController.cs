using FlashCard.Abstract.Exceptions;
using FlashCard.Abstract.Request;
using FlashCard.Abstract.Responses;
using FlashCard.API.Models.Validations;
using FlashCard.Auth.Attributes;
using FlashCard.Business.Cards;
using FlashCard.Business.Models;
using FlashCard.Core.Entities;
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

                //_cardServices.Validate(input);

                int? registrationId = RegistrationId;

                if (!registrationId.HasValue)
                {
                    return CustomBadRequestResult.Fail(ApiConstants.InvalidRegistrationId);
                }

                int cardId = await _cardServices.CreateAsync(input, registrationId.Value);

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
        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateCardAsync(int id,[FromBody] CardRequest request)
        {
            try
            {
                CardDto input = new ()
                {
                    Title = request.Title,
                    Date = request.Date?.Date,
                    CardType = request.CardType,
                    Time = request.Time,
                    Content = request.Content,
                    Address = request.Address
                };


                int? registrationId = RegistrationId;

                if (!registrationId.HasValue)
                {
                    return CustomBadRequestResult.Fail(ApiConstants.InvalidRegistrationId);
                }

                await _cardServices.UpdateCardAsync(id, input, registrationId.Value);

                return Ok();
            }
            catch (BusinessValidationException e)
            {
                return CustomBadRequestResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// Delete user appointment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteCardAsync(int id)
        {
            try
            {
                int? registrationId = RegistrationId;

                if (!registrationId.HasValue)
                {
                    return CustomBadRequestResult.Fail(ApiConstants.InvalidRegistrationId);
                }

                await _cardServices.DeleteCardAsync(id, registrationId.Value);

                return Ok();
            }
            catch (BusinessValidationException e)
            {
                return CustomBadRequestResult.Fail(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<PagingResultDto<CardResponse>>> GetCardsAsync(int pageNo = ApiConstants.DefaultPageNo, int pageSize = ApiConstants.DefaultPageSize)
        {
            try
            {
                int? registrationId = RegistrationId;

                if (!registrationId.HasValue)
                {
                    return CustomBadRequestResult.Fail(ApiConstants.InvalidRegistrationId);
                }

                PagingResultDto<CardResponse> result = await _cardServices.GetCardsAsync(registrationId.Value, pageNo, pageSize);

                return Ok(result);
            }
            catch (BusinessValidationException e)
            {
                return CustomBadRequestResult.Fail(e.Message);
            }
        }
    }
}
