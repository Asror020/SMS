using SMSCore.Models.Entities;

namespace SMS.BLL.Services.EntityServices.Interfaces
{
    public interface IUserService : IEntityBaseService<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User> CreateAsync(User entity, long userId);

        Task<IEnumerable<User>> GetNewUsersAsync();

        Task<IEnumerable<User>> GetUsersAsync(long userId);

        Task<bool> ApproveAsync(long id);
    }
}