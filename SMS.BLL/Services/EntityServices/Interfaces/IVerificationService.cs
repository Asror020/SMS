

using SMSCore.Models.Entities;

namespace SMS.BLL.Services.EntityServices.Interfaces;

public interface IVerificationService : IEntityBaseService<Verification>
{
    Task<Verification?> GetByPinCodeAsync(long pinCode);

    Task<bool> SendEmailAsync(long userId);

    Task<Verification> CreateVerificationAsync(long userId);

    Task<bool> VerifyAsync(long varificationToken);

    Task<bool> UpdateAsUsedAsync(long userId);
}