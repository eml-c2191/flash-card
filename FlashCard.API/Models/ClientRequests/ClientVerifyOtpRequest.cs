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
    public record RegisterRequest : HasMobileNo
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("([0-9]+)")]
        public string Otp { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Date, ErrorMessage = "An issue has occurred with your registration details. Please ensure you have entered a valid VietNam phone number and your Date of Birth is in the format DD/MM/YYYY.")]
        public DateTime DateOfBirth { get; set; }
    }
}
