using SMSCore.Enums;
using SMSCore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCore.Models.Entities
{
    public class Student : IEntity
    {
        public long Id { get; set; }

        public string StudentId { get; set; } = null!;

        public string CourseLevel { get; set; } = null!;

        public string Degree { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string? GroupName { get; set; }

        public User User { get; set; } = null!;
    }
}
