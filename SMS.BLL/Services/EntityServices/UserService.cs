using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Entities;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMSCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace SMS.BLL.Services.EntityServices
{
    public class UserService : EntityBaseService<User, IRepositoryBase<User>>, IUserService
    {
        public UserService(IRepositoryBase<User> entityRepository, IEmailService emailService) : base(entityRepository)
        {
        }

        public async Task<User> CreateAsync(User entity, long userId)
        {
            return await Task.Run(() =>
            {
                if (entity.Role == null) entity.Role = UserRoles.User.ToString();

                if (EmailAddressExists(entity.EmailAddress)) return Task.Run(() => new User());

                var Creator = EntityRepository.Get(x => x.Id == userId).FirstOrDefault() ?? throw new Exception();

                entity.University = Creator.University;

                return base.CreateAsync(entity);
            });
        }

        public async Task<IEnumerable<User>> GetUsersAsync(long userId)
        {
            if (userId <= 0) throw new Exception();

            return await Task.Run(async () =>
            {
                var receiver = await GetByIdAsync(userId);

                if (receiver.Role == UserRoles.Admin.ToString())
                {
                    return EntityRepository.Get(x => x.Role == UserRoles.Owner.ToString()).Include(x => x.University);
                }

                return EntityRepository.Get(x => x.University == receiver.University).Include(x => x.University);
            });
        }

        public async Task<IEnumerable<User>> GetNewUsersAsync()
        {
            return await Task.Run(() => EntityRepository.Get(x => x.IsUserConfirmed == false).Include(x => x.University));
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) throw new EntryPointNotFoundException();

            return await Task.Run(() =>
            {
                var data = EntityRepository.Get(x => x.EmailAddress == email).FirstOrDefault();

                return data;
            });
        }

        public override Task<User> CreateAsync(User entity)
        {
            return Task.Run(() =>
            {
                if (entity.Role == null) entity.Role = UserRoles.User.ToString();

                if(EmailAddressExists(entity.EmailAddress)) return Task.Run(() => new User());

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

        public override async Task<bool> DeleteAsync(long id)
        {
            var user = await GetByIdAsync(id);

            if(user.Role == UserRoles.Owner.ToString())
            {
                await EntityRepository.Get(x => x.University == user.University && x.Id != user.Id)
                    .ForEachAsync(async x => await base.DeleteAsync(id));
            }

            return await base.DeleteAsync(id);
        }

        public async Task<bool> ApproveAsync(long id)
        {
            return await Task.Run(async () =>
            {
                var user = await GetByIdAsync(id);

                user.IsUserConfirmed = true;

                user.Role = UserRoles.Owner.ToString();

                return await UpdateAsync(id, user);
            });
        }

        public bool EmailAddressExists(string emailAddress)
        {
            if(EntityRepository.Get(x => x.EmailAddress == emailAddress).FirstOrDefault() == null) return false;

            return true;
        }
    }
}
