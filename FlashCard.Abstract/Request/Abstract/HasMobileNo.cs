using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.Request.Abstract
{
    public record HasMobileNo
    {
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "An issue has occurred with your registration details. Please ensure you have entered a valid Australian phone number and your Date of Birth is in the format DD-MM-YYYY. Contact your case manager if the issue persists.")]
        [RegularExpression("^\\+84\\d{9,11}$",
                   ErrorMessage = "An issue has occurred with your registration details. Please ensure you have entered a valid Australian phone number and your Date of Birth is in the format DD-MM-YYYY. Contact your case manager if the issue persists.")]
        [MaxLength(11)]
        [MinLength(10)]
        public string MobileNo { get; set; } = string.Empty;
    }
}
