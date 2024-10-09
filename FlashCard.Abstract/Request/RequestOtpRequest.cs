using FlashCard.Abstract.Request.Abstract;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.Abstract.Request
{
    public record RequestOtpRequest : HasMobileNo
    {   
        [Required]
        [DataType(DataType.Date, ErrorMessage = "An issue has occurred with your registration details. Please ensure you have entered a valid Australian phone number and your Date of Birth is in the format DD-MM-YYYY. Contact your case manager if the issue persists.")]
        public DateTime DateOfBirth { get; set; }
    }
}
