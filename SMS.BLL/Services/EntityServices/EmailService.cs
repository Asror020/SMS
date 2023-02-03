using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using SMS.BLL.Models.Configurations;
using Microsoft.Extensions.Options;

namespace SMS.BLL.Services.EntityServices
{
    public class EmailService : EntityBaseService<Email, IRepositoryBase<Email>>, IEmailService
    {
        private readonly EmailConfigurations _emailConfig;
        public EmailService(IRepositoryBase<Email> entityRepository, IOptions<EmailConfigurations> emailConfig) : base(entityRepository)
        {
            _emailConfig = emailConfig.Value;
        }

        public Task<bool> SendAsync(Email email)
        {
            if (email == null) throw new ArgumentNullException();

            return Task.Run(() =>
            {
                var result = false;
                try
                {
                    // Sending email
                    using var smtp = CreateClient();
                    using var message = CreateMail(email);
                    smtp.Send(message);

                    // Creating email entity
                    email.SentDate = DateTime.UtcNow;

                    EntityRepository.Create(email);

                    result = true;
                }
                catch
                {
                    //TODO: Log about failed email
                }

                return result;
            });
        }

        public MailMessage CreateMail(Email email)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };
            message.To.Add(new MailAddress(email.To));

            return message;
        }

        public SmtpClient CreateClient()
        {
            return new SmtpClient()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailConfig.EmailAddress, _emailConfig.Password),
                Port = _emailConfig.Port,
                Host = _emailConfig.Host,
                EnableSsl = true
            };
        }
    }
}
