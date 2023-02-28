using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.EntityServices
{
    public class GroupService : EntityBaseService<Group, IRepositoryBase<Group>>, IGroupService
    {
        private readonly IUserService _userService;
        public GroupService(IRepositoryBase<Group> entityRepository, IUserService userService) : base(entityRepository)
        {
            _userService = userService;
        }

        public async Task<Group?> GetByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name)) return null;

            return await Task.Run(() =>
            {
                return EntityRepository.Get(x => x.Name == name).FirstOrDefault();
            });
        }

        public override async Task<bool> UpdateAsync(long id, Group group)
        {
            if(id < 0) return false;

            return await Task.Run(async () =>
            {
                var data = await GetByIdAsync(id);
                if (group == null) return false;

                data.Name = group.Name;
                data.StudentIds = group.StudentIds;

                return await base.UpdateAsync(id, data);
            });
        }
        public async Task<bool> AddStudent(string groupName, long studentId)
        {
            if (studentId < 0) return false;

            return await Task.Run(async () =>
            {
                var group = await GetByNameAsync(groupName);

                if (group == null) return false;

                if (!string.IsNullOrEmpty(group.StudentIds)) group.StudentIds += ",";

                group.StudentIds += studentId.ToString() ;

                return await UpdateAsync(group.Id, group);
            });
        }

        public async Task<bool> RemoveStudentAsync(string groupName, long studentId)
        {
            if(studentId < 0) return false;

            return await Task.Run(async () =>
            {
                var group = await GetByNameAsync(groupName);

                if (group == null) return false;

                var studentIds = group.StudentIds.Split(",").ToList();

                if (!studentIds.Any(x => x == studentId.ToString())) return false;

                studentIds.Remove(studentId.ToString());

                studentIds.ForEach(x =>
                {
                    group.StudentIds += x + ",";
                });

                group.StudentIds = group.StudentIds.Substring(0, group.StudentIds.Length - 1);

                return await UpdateAsync(group.Id, group);
            });
        }

        public async Task<IEnumerable<string>> GetGroupNames(long id)
        {
            return await Task.Run(async () =>
            {

                var user = await _userService.GetByIdAsync(id);

                var names = new List<string>();

                var groups = EntityRepository.Get(x => x.University == user.University).ToList();

                groups.ForEach(x => names.Add(x.Name));

                return names;
            });
        }
    }
}
