using FlashCard.Abstract.Exceptions;
using FlashCard.Abstract.Responses;
using FlashCard.API.Models.ClientRequests;
using FlashCard.API.Models.Validations;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace FlashCard.API.Controllers
{
    [ApiController]
    public class RegistrationController : FLCBaseController
    {
        // [HttpPost("Otp")]
        // [AnonymousAuthorize]
        // public async Task<IActionResult> RequestOTPAsync
        //(
        //    [FromBody][Required] RequestOtpRequest request
        //)
        // {
        //     // Pre-process request
        //     request.DateOfBirth = request.DateOfBirth.Date;

        //     try
        //     {
        //        // TODO: Implement captcha validation


        //         TODO: Implement mobile number and date of birth validation
        //             
        //             
        //         

        //         if (!response.IsValid || !response.ClaimSystemId.HasValue)
        //         {
        //             return CustomBadRequestResult.Fail(ApiConstants.InvalidUserInfo);
        //         }

        //         await _identityClient.RequestOtpAsync(request.MobileNo);

        //         return Ok();
        //     }
        //     catch (InvalidRequestException e)
        //     {
        //         return CustomBadRequestResult.Fail(e.Message);
        //     }
        //     catch (BusinessValidationException e)
        //     {
        //         return CustomBadRequestResult.Fail(e.Message);
        //     }
        // }
        // [HttpPut("RegistrationService")]
        // [ProducesResponseType(typeof(VerifyOtpResponse), StatusCodes.Status200OK)]
        // [AnonymousAuthorize]
        // public async Task<ActionResult<VerifyOtpResponse>> RegisterAsync
        // (
        // [FromBody] RegisterRequest request)
        // {
        //     request.DateOfBirth = request.DateOfBirth.Date;

        //     try
        //     {
        //          TODO: Implement mobile number and date of birth validation
        //             
        //             
        //        

        //         if (!response.IsValid || !response.ClaimSystemId.HasValue)
        //         {
        //             return CustomBadRequestResult.Fail(ApiConstants.InvalidUserInfo);
        //         }

        //         ClientVerifyOtpRequest verifyOtpRequest = new()
        //         {
        //             MobileNo = request.MobileNo,
        //             Otp = request.Otp
        //         };

        //         bool isValidOtp = await _identityClient.VerifyOtpAsync(verifyOtpRequest);
        //         if (!isValidOtp)
        //         {
        //             return CustomBadRequestResult.Fail(ApiConstants.InvalidOTP);
        //         }

        //         // isRegistered = false when mobileNo + dateOfBirth not exist in EML
        //         (int registrationId, string registrationHash) = await _registerService.RegisterAsync
        //         (
        //             request.MobileNo
        //         );

        //         AuthoriseResponse authoriseResponse = await _identityClient.GetTokenAsync(
        //             claims: new List<KeyValuePair<string, string>>  {
        //             new KeyValuePair<string, string>(ClaimTypes.NameIdentifier, request.MobileNo),
        //             new KeyValuePair<string, string>(ClaimTypes.MobilePhone, request.MobileNo),
        //             new KeyValuePair<string, string>(ClaimTypes.DateOfBirth, request.DateOfBirth.ToString(ApiConstants.DateFormat)),
        //             new KeyValuePair<string, string>("scp", _scope),
        //             new KeyValuePair<string, string>(ApiConstants.RegistrationIdKey, registrationId.ToString()),
        //             new KeyValuePair<string, string>(ApiConstants.RegistrationHash, registrationHash)
        //             });

        //         return Ok(authoriseResponse);
        //     }
        //     catch (InvalidRequestException e)
        //     {
        //         return CustomBadRequestResult.Fail(e.Message);
        //     }
        //     catch (BusinessValidationException e)
        //     {
        //         return CustomBadRequestResult.Fail(e.Message);
        //     }
        // }
    }
}
