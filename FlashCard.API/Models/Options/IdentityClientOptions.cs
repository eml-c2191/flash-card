using FlashCard.Abstract;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.API.Models.Options
{
    public record IdentityClientOptions
    {
        public Uri Endpoint { get; set; } = AbstractConstants.DefaultUri;
        [Required]
        [Range(1, int.MaxValue)]
        public int OTPRequestLimitCount { get; set; } = 100;
    }
}
