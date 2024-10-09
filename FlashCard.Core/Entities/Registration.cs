using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Entities
{
    public class Registration : BaseEntity<int>
    {
        public string MobileNo { get; set; } = String.Empty;
        public DateTime LastSessionTimeStamp { get; set; }
        public bool IsActive { get; set; } = true;

        public bool? LatestTermAccept { get; set; }
    }
}
