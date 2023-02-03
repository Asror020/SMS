using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Common;
using SMS.BLL.Services.EntityServices.Interfaces;

namespace SMS.BLL.Services.EntityServices
{
    public class EntityBaseService<TEntity, TRepository> : IEntityBaseService<TEntity> where TEntity : class, IEntity where TRepository : IRepositoryBase<TEntity>
    {
        protected readonly IRepositoryBase<TEntity> EntityRepository;

        public EntityBaseService(IRepositoryBase<TEntity> entityRepository)
        {
            EntityRepository = entityRepository;
        }

        public virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception();
            }

            return Task.Run(async () =>
            {
                EntityRepository.Create(entity);

                return await EntityRepository.SaveChangesAsync() ? entity : throw new InvalidOperationException();
            });
        }

        public virtual Task<bool> DeleteAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }

            return Task.Run(async () =>
            {
                var data = EntityRepository.Get(x => x.Id == id).FirstOrDefault() ?? throw new EntryPointNotFoundException(nameof(TEntity));

                EntityRepository.Delete(data);

                return await EntityRepository.SaveChangesAsync();
            });
        }

        public virtual Task<TEntity> GetByIdAsync(long id)
        {
            if (id <= 0) throw new EntryPointNotFoundException();

            return Task.Run(() =>
            {
                var data = EntityRepository.Get(x => x.Id == id).FirstOrDefault();

                return data != null ? data : throw new EntryPointNotFoundException();
            });
        }

        public virtual Task<bool> UpdateAsync(long id, TEntity entity)
        {
            if (id <= 0 || entity == null) throw new EntryPointNotFoundException();

            return Task.Run(async () =>
            {
                var data = EntityRepository.Get(x => x.Id == id).FirstOrDefault() ?? throw new EntryPointNotFoundException();

                EntityRepository.Update(entity);

                return await EntityRepository.SaveChangesAsync();
            });
        }
    }
}
