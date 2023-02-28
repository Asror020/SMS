using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.EntityServices
{
    public class SelectionItemService : EntityBaseService<SelectionItem, IRepositoryBase<SelectionItem>>, ISelectionItemService
    {
        public SelectionItemService(IRepositoryBase<SelectionItem> entityRepository) : base(entityRepository)
        {
        }

        public IList<SelectionItem> GetByType(string type)
        {
            return EntityRepository.Get(x => x.Type == type).ToList();
        }
    }
}
