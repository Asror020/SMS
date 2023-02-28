namespace SMS.BLL.Services.EntityServices.Interfaces
{
    public interface IEntityBaseService<TEntity>
    {
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">entity id being queried</param>
        /// <returns>Entity</returns>
        Task<TEntity?> GetByIdAsync(long id);

        /// <summary>
        /// Created entity
        /// </summary>
        /// <param name="entity">Entity being created</param>
        /// <returns>Entity</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="entity">entity</param>
        /// <returns>true if updated or false</returns>
        Task<bool> UpdateAsync(long id, TEntity entity);

        /// <summary>
        /// Deletes entity by id
        /// </summary>
        /// <param name="id">id of the entity being queried</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
    }
}
