using Microsoft.EntityFrameworkCore;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Enums;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.EntityServices
{
    public class StudentService : EntityBaseService<Student, IRepositoryBase<Student>>, IStudentService
    {
        private readonly IUserService _useService;
        public StudentService(IRepositoryBase<Student> entityRepository, IUserService useService) : base(entityRepository)
        {
            _useService = useService;
        }

        public async override Task<Student?> GetByIdAsync(long id)
        {
            return await Task.Run(() => EntityRepository.Get(x => x.Id == id).Include(x => x.User).FirstOrDefault());
        }

        public async Task<IEnumerable<Student>> GetAll(long userId)
        {
            return await Task.Run(async () =>
            {
                var user = await _useService.GetByIdAsync(userId);

                if (user.Role == UserRoles.Student.ToString() || 
                    user.Role == UserRoles.User.ToString())
                    return Enumerable.Empty<Student>();

                return EntityRepository.Get(x => x.User.University == user.University).Include(x => x.User);
            });
        }

        public async Task<Student> CreateAsync(long userId, Student model)
        {
            return await Task.Run(async () =>
            {
                if (await EmailAddressExists(model.User.EmailAddress))
                {
                    model.User.EmailAddress = null;

                    return model;
                }

                var user = await _useService.GetByIdAsync(userId);

                model.User.Role = UserRoles.Student.ToString();
                model.User.University = user.University;

                return await base.CreateAsync(model);
            });
        }

        public override async Task<bool> UpdateAsync(long id, Student model)
        {
            return await Task.Run(async () =>
            {
                var data = await GetByIdAsync(id);
                if (data == null) return false;

                data.DateOfBirth = model.DateOfBirth;
                data.CourseLevel = model.CourseLevel;
                data.GroupName = model.GroupName;
                data.User.FirstName = model.User.FirstName;
                data.User.LastName = model.User.LastName;
                data.StudentId = model.StudentId;

                return await base.UpdateAsync(id, data);
            });
        }

        public async Task<bool> EmailAddressExists(string emailAddress)
        {
            if (await _useService.GetByEmailAsync(emailAddress) == null) return false;

            return true;
        }
    }
}
