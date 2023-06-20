namespace APMS.Managers.Common
{
    public interface IManager<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        TEntity FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        Task DeleteAsync(TEntity item);
        Task InsertAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
