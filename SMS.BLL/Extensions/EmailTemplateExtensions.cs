using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Extensions
{
    public static class EmailTemplateExtensions
    {
        public static Email ToEmail(this EmailTemplate template)
        {
            return new Email
            {
                Subject = template.Subject,
                Body = template.Body,
            };
        }
    }
}
