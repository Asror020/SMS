using SMSCore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCore.Models.Entities
{
    public class Email : IEntity
    {
        public long Id { get; set; }

        public long UniversityId { get; set; }

        public string From { get; set; } = null!;

        public string To { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Body { get; set; } = null!;

        public DateTime SentDate { get; set; }

        public string Type { get; set; } = null!;
    }
}
