using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.EntityServices.Interfaces
{
    public interface IGroupService : IEntityBaseService<Group>
    {
        Task<bool> AddStudent(string groupName, long studentId);
        Task<bool> RemoveStudentAsync(string groupName, long studentId);
        Task<Group?> GetByNameAsync(string name);

        Task<IEnumerable<string>> GetGroupNames(long id);
    }
}
