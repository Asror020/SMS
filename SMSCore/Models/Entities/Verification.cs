using SMSCore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCore.Models.Entities
{
    public class Verification : IEntity
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long PinCode { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Status { get; set; } = null!;
    }
}
