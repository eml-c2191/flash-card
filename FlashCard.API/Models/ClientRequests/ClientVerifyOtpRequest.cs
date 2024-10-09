using FlashCard.Abstract.Request.Abstract;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.API.Models.ClientRequests
{
    public record ClientVerifyOtpRequest : HasMobileNo
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("([0-9]+)")]
        public string Otp { get; set; } = String.Empty;
    }
}
