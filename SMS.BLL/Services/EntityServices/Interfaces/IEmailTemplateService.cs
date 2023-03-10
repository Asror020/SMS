using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.EntityServices.Interfaces
{
    public interface IEmailTemplateService : IEntityBaseService<EmailTemplate>
    {
        Task<EmailTemplate> GetByNameAsync(string name);
    }
}
