using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Entities;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMSCore.Enums;

namespace SMS.BLL.Services.EntityServices
{
    public class UserService : EntityBaseService<User, IRepositoryBase<User>>, IUserService
    {
        public UserService(IRepositoryBase<User> entityRepository, IEmailService emailService) : base(entityRepository)
        {
        }

        public override Task<User> CreateAsync(User entity)
        {
            return Task.Run(() =>
            {
                if (entity.Role == null) entity.Role = "User";

                if(EmailAddessExists(entity.EmailAddress)) return Task.Run(() => new User());

                return base.CreateAsync(entity);
            });
        }

        public override async Task<bool> UpdateAsync(long id, User user)
        {
            if (user == null || id <= 0) throw new EntryPointNotFoundException();

            return await Task.Run(async () =>
            {
                var data = EntityRepository.Get(x => x.Id == id).FirstOrDefault() ?? throw new Exception();

                data.FirstName = user.FirstName;
                data.LastName = user.LastName;

                return await base.UpdateAsync(id, user);
            });
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) throw new EntryPointNotFoundException();

            return await Task.Run(() =>
            {
                var data = EntityRepository.Get(x => x.EmailAddress == email).FirstOrDefault() ?? throw new EntryPointNotFoundException();

                return data;
            });
        }

        public bool EmailAddessExists(string emailAddress)
        {
            if(EntityRepository.Get(x => x.EmailAddress == emailAddress).FirstOrDefault() == null) return false;

            return true;
        }
    }
}
