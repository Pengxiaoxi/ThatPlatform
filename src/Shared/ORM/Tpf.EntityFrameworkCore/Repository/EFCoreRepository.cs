using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;
using Tpf.Autofac;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.EntityFrameworkCore.Repository
{
    public class EFCoreRepository<TEntity> :
        BaseRepository<TEntity>,
        IEFCoreRepository<TEntity>
        where TEntity : BaseEntity<string>
    {
        private TpfDbContextBase _context;
        private DbSet<TEntity> _entities;

        public EFCoreRepository(TpfDbContextBase dbContext
            )
        {
            _context = dbContext;
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => DbSet;

        ///// <summary>
        ///// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        ///// </summary>
        //public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            if (whereExpression is null) { throw new ArgumentNullException(nameof(whereExpression)); }
            
            return await DbSet.AnyAsync(whereExpression);
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return await DbSet.CountAsync(whereExpression);
        }

        public override async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            _context.Remove(entity);

            return await SaveChangesAsync();
        }

        public override async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            if (whereExpression is null) { throw new ArgumentNullException(nameof(whereExpression)); }

            _context.RemoveRange(DbSet.Where(whereExpression));

            return await SaveChangesAsync();
        }

        public override async Task<bool> DeleteByIdAsync(string id)
        {
            if (id is null) { throw new ArgumentNullException(nameof(id)); }

            _context.RemoveRange(DbSet.Where(x => x.Id == id));

            return await SaveChangesAsync();
        }

        public override async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return whereExpression is null
                ? await DbSet.FirstOrDefaultAsync()
                : await DbSet.FirstOrDefaultAsync(whereExpression);
        }

        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return whereExpression is null
                ? await DbSet.AsNoTracking().ToListAsync()
                : await DbSet.AsNoTracking().Where(whereExpression).ToListAsync();
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            await DbSet.AddAsync(entity);

            return await SaveChangesAsync();
        }

        public override async Task<bool> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null) { throw new ArgumentNullException(nameof(entities)); }

            await DbSet.AddRangeAsync(entities);

            return await SaveChangesAsync();
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            DbSet.Update(entity);

            return await SaveChangesAsync();
        }

        public override async Task<bool> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null) { throw new ArgumentNullException(nameof(entities)); }

            DbSet.UpdateRange(entities);

            return await SaveChangesAsync();
        }

        public override async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            //DbSet.Where(whereExpression)

            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            //// 未开启事务则直接保存 
            if ((DbContext)_context.Database.CurrentTransaction is null)
            {
                //return await _context.SaveChangesAsync() >= 0;
            }

            //_unitOfWork.SaveChangesAsync

            return await Task.FromResult(true);
        }


        #region Private Method
        protected virtual DbSet<TEntity> DbSet => _entities ?? (_entities = GetEntityDbContext(typeof(TEntity)).Set<TEntity>());

        /// <summary>
        /// 获取实体 DbContextAttribute 特性中设置的 DbContext
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected DbContext GetEntityDbContext(Type entityType)
        {
            //return _context;

            if (entityType.IsDefined(typeof(DbContextAttribute)))
            {
                var entityDbContextType = entityType.GetCustomAttribute<DbContextAttribute>().ContextType;
                if (entityDbContextType.Equals(_context.GetType()))
                {
                    return _context;
                }

                _context = (TpfDbContextBase)AutofacFactory.GetContainer().Resolve(entityDbContextType);
            }

            return _context;
        }

        #endregion

    }
}
