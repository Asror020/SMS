using SMSCore.Models.Entities;

namespace SMS.BLL.Services.EntityServices.Interfaces
{
    public interface IUserService : IEntityBaseService<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}