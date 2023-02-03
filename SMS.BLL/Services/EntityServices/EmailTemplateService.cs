using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Constants;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SMS.BLL.Models.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace SMS.BLL.Services.EntityServices
{
    public class EmailTemplateService : EntityBaseService<EmailTemplate, IRepositoryBase<EmailTemplate>>, IEmailTemplateService
    {
        public EmailTemplateService(IRepositoryBase<EmailTemplate> entityRepository) : base(entityRepository)
        {
        }

        public async Task<EmailTemplate> GetByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(name);

            return await Task.Run(() =>
            {
                var data = EntityRepository.Get(x => x.Name == name).FirstOrDefault() ?? throw new ArgumentException();

                return data;
            });
        }
    }
}
