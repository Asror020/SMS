using SMSCore.Models.Common;

namespace SMSCore.Models.Entities
{
    public class University : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int NumberOfStudents { get; set; }

        public int NumberOfStaff { get; set; }

        public string Location { get; set; } = null!;

        public long UniversityAdminUserId { get; set; }
    }
}
