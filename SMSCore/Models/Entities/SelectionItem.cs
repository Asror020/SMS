using SMSCore.Models.Common;

namespace SMSCore.Models.Entities
{
    public class SelectionItem : IEntity
    {
        public long Id { get; set; }

        public string Type { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
