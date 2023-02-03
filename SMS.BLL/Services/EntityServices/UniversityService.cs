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
    public class UniversityService : EntityBaseService<University, IRepositoryBase<University>>, IUniversityService
    {
        private readonly IUserService _userService;
        public UniversityService(IRepositoryBase<University> entityRepository, IUserService userService) : base(entityRepository)
        {
            _userService = userService;
        }

        public override async Task<University> CreateAsync(University entity)
        {
            if(UniversityExists(entity.Name)) return new University();

            var createdUniversity = await base.CreateAsync(entity);

            var user = await _userService.GetByIdAsync(createdUniversity.UniversityAdminUserId);

            user.University = createdUniversity;

            await _userService.UpdateAsync(user.Id, user);

            return createdUniversity;
        }

        private bool UniversityExists(string name)
        {
            if(EntityRepository.Get(x => x.Name == name).FirstOrDefault() == null) return false;

            return true;
        }
    }
}
