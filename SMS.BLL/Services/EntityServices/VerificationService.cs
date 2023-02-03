
using Microsoft.Extensions.Options;
using SMS.BLL.Extensions;
using SMS.BLL.Models.Configurations;
using SMS.BLL.Services.EntityServices;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Constants;
using SMSCore.Enums;
using SMSCore.Models.Entities;

namespace Common.BLL.Services.EntityServices;

/// <summary>
/// Provides business logic for verification process token
/// </summary>
public class VerificationService : EntityBaseService<Verification, IRepositoryBase<Verification>>, IVerificationService
{
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IUserService _userService;
    private readonly EmailConfigurations _emailConfiguration;

    public VerificationService(
        IOptions<EmailConfigurations> emailConfiguration,
        IRepositoryBase<Verification> entityRepository,
        IEmailService emailService,
        IEmailTemplateService emailTemplateService,
        IUserService userService
    ) : base(entityRepository)
    {
        _emailConfiguration = emailConfiguration.Value;
        _emailService = emailService;
        _emailTemplateService = emailTemplateService;
        _userService = userService;
    }

    public async Task<Verification?> GetByPinCodeAsync(long pinCode)
    {
        if (pinCode <= 0) throw new ArgumentException();

        return await Task.Run(() => { return EntityRepository.Get(x => x.PinCode == pinCode).FirstOrDefault(); });
    }

    public long CreateCode(int length)
    {
        var checker = true;
        var generatedCode = 0;

        while (checker)
        {
            generatedCode = new Random().Next(100000, (int)Math.Pow(10, length));
            if (EntityRepository.Get(x => x.PinCode == generatedCode).FirstOrDefault() == null)
                checker = false;
        }

        return generatedCode;
    }

    public async Task<Verification> CreateVerificationAsync(long userId)
    {   
        if (userId <= 0) throw new EntryPointNotFoundException();

        return await Task.Run(async () =>
        {
            // Marking all previous verifications as used
            var result = await UpdateAsUsedAsync(userId);

            // Generating verification entity
            var verification = new Verification();

            verification.Status = VerificationStatuses.Active.ToString();
            verification.PinCode = CreateCode(6);
            verification.UserId = userId;
            verification.ExpiryDate = DateTime.Now.AddHours(1);

            return await CreateAsync(verification);
        });
    }

    public async Task<bool> SendEmailAsync(long userId)
    {
        if (userId <= 0)
            throw new ArgumentException();

        return await Task.Run(async () =>
        {
            // Getting new verification entity and user
            var verification = await CreateVerificationAsync(userId);
            var user = await _userService.GetByIdAsync(userId) ?? throw new Exception();

            // Getting email template
            var emailTemplate = await _emailTemplateService.GetByNameAsync("Email Verification") ?? throw new Exception();

            var verifyUrl = new Uri(new Uri(UrlConstants.LocalHost), string.Format(UrlConstants.VerifyUrl, verification.PinCode));

            // Preparing mail message
            var message = emailTemplate.ToEmail();
            message.From = _emailConfiguration.EmailAddress;
            message.To = user.EmailAddress;
            message.Body = message.Body.Replace(MessageConstants.VerifyUrlToken, verifyUrl.ToString()).Replace(MessageConstants.UsernameToken, user.FirstName + " " + user.LastName);

            return await _emailService.SendAsync(message);
        });
    }

    public async Task<bool> VerifyAsync(long verificationToken)
    {
        if (verificationToken <= 0) throw new ArgumentException();

        return await Task.Run(async () =>
        {
            // Getting verification and user entities
            var verification = await GetByPinCodeAsync(verificationToken) ?? throw new EntryPointNotFoundException();
            var user = await _userService.GetByIdAsync(verification.UserId) ?? throw new EntryPointNotFoundException();

            // Checking verification validity
            if (verification.Status != VerificationStatuses.Active.ToString() || verification.ExpiryDate < DateTime.UtcNow)
                throw new Exception("This code is expired or used");

            // Changing all previous verifications to used
            var allVerifications = EntityRepository.Get(x => x.UserId == verification.UserId);
            foreach (var item in allVerifications)
            {
                item.Status = VerificationStatuses.Used.ToString();
            }

            user.IsEmailVerified = true;
            verification.Status = VerificationStatuses.Used.ToString();

            return await _userService.UpdateAsync(user.Id, user) && await UpdateAsync(verification.Id, verification);

        });
    }

    public async Task<bool> UpdateAsUsedAsync(long userId)
    {
        if (userId <= 0)
            throw new ArgumentException();

        return await Task.Run(() =>
        {
            var previousVerifications = EntityRepository
                .Get(x => x.UserId == userId && x.Status == VerificationStatuses.Active.ToString())
                .ToList();

            var updateTasks = previousVerifications.ToList()
                .Select(x =>
                {
                    x.Status = VerificationStatuses.Used.ToString();
                    return Task.Run(() => EntityRepository.Update(x));
                });

            Task.WaitAll(updateTasks.ToArray());
            return EntityRepository.SaveChanges();
        });
    }
}