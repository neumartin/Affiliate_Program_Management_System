using APMS.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace APMS.Managers.Common
{
    public class GenericManager<TEntity> : IManager<TEntity> where TEntity : class
    {
        public GenericManager()
        {
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            using (var context = new ApmsDbContext())
            {
                var dbSet = context.Set<TEntity>();
                return await dbSet.ToListAsync();
            }
        }

        public TEntity FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = new ApmsDbContext())
            {
                var dbSet = context.Set<TEntity>();
                return dbSet.FirstOrDefault(predicate);
            }
        }

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = new ApmsDbContext())
            {
                var dbSet = context.Set<TEntity>();
                return await dbSet.FirstOrDefaultAsync(predicate);
            }
        }

        public virtual void Delete(TEntity item)
        {
            DeleteAsync(item).Wait();
        }

        public virtual async Task DeleteAsync(TEntity item)
        {
            using (var context = new ApmsDbContext())
            {
                await DeleteAsync(item, context);
            }
        }

        public virtual async Task DeleteAsync(TEntity item, ApmsDbContext context)
        {
            var dbSet = context.Set<TEntity>();

            context.Attach(item);
            context.Entry(item).State = EntityState.Modified;
            dbSet.Remove(item);

            await context.SaveChangesAsync();
        }
        
        public virtual void Insert(TEntity item)
        {
            using (var context = new ApmsDbContext())
            {
                var dbSet = context.Set<TEntity>();
                dbSet.Add(item);
                context.SaveChanges();
            }
        }

        public virtual async Task InsertAsync(TEntity item)
        {
            using (var context = new ApmsDbContext())
            {
                var dbSet = context.Set<TEntity>();
                dbSet.Add(item);
                await context.SaveChangesAsync();
            }
        }

        public virtual void Update(TEntity item)
        {
            using (var context = new ApmsDbContext())
            {
                context.Attach(item);
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            using (var context = new ApmsDbContext())
            {
                context.Attach(item);
                context.Entry(item).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public object[] GetKeyValues(TEntity item)
        {
            return (from property in item.GetType().GetProperties()
                    where property.GetCustomAttribute(typeof(KeyAttribute)) != null
                    select property.GetValue(item)).ToArray();
        }
    }
}
