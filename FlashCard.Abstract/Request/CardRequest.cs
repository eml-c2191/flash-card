using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.Request
{
    public record CardRequest
    {
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid datetime format: yyyy-MM-dd")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Time is required.")]
        public TimeSpan? Time { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Maximum length of type is 50.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required.")]
        public string CardType { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required.")]
        [StringLength(maximumLength: 200, ErrorMessage = "Maximum length of Content is 200.")]
        public string Content { get; set; } = string.Empty;


        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Maximum length of Title is 50.")]
        public string Title { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required.")]
        [StringLength(maximumLength: 200, ErrorMessage = "Maximum length of Address is 200.")]
        public string Address { get; set; } = string.Empty;
    }
}
