using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Auth.Options
{
    public record FLCAuthenticationOptions
    {
        [Required(AllowEmptyStrings = false)]
        public string? AccessTokenIssuer { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string? AccessTokenAudience { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Scope { get; set; } = String.Empty;
    }
}
