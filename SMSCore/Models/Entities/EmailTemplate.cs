using SMSCore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCore.Models.Entities
{
    public class EmailTemplate : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Body { get; set; } = null!;
    }
}
