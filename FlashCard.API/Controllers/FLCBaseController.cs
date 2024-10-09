using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using System.Globalization;
using System.Security.Claims;

namespace FlashCard.API.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class FLCBaseController : ControllerBase
    {
        public FLCBaseController()
        {
        }

        public string MobileNo => User.FindFirstValue(ClaimTypes.MobilePhone) ?? String.Empty;

        public DateTime? DateOrBirth
        {
            get
            {
                bool valid = DateTime.TryParseExact
                    (
                        User.FindFirstValue(ClaimTypes.DateOfBirth),
                        ApiConstants.DateFormat,
                        System.Globalization.CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime dateOrBirth
                    );
                return valid ? dateOrBirth : null;
            }
        }

        public int? RegistrationId
        {
            get
            {
                bool valid = int.TryParse
                    (
                        User.FindFirstValue(ApiConstants.RegistrationIdKey),
                        out int registrationId
                    );
                return valid ? registrationId : null;
            }
        }
    }
}
