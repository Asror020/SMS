using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.EntityServices.Interfaces
{
    public interface IStudentService : IEntityBaseService<Student>
    {
        Task<IEnumerable<Student>> GetAll(long userId);

        Task<Student> CreateAsync(long userId, Student model);
    }
}
