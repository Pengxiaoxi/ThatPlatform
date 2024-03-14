using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tpf.BaseRepository;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.EntityFrameworkCore.Repository
{
    public class EFCoreRepository<TEntity> :
        BaseRepository<TEntity>,
        IEFCoreRepository<TEntity>
        where TEntity : BaseEntity<string>
    {
        private readonly TpfDbContextBase _context;
        private DbSet<TEntity> _entities;

        public EFCoreRepository(TpfDbContextBase dbContext)
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
            if (whereExpression == null) { throw new ArgumentNullException(nameof(whereExpression)); }
            
            return await DbSet.AnyAsync(whereExpression);
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return await DbSet.CountAsync(whereExpression);
        }

        public override async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);

            return await SaveChangesAsync();
        }

        public override async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            _context.RemoveRange(DbSet.Where(whereExpression));

            return await SaveChangesAsync();
        }

        public override async Task<bool> DeleteByIdAsync(string id)
        {
            _context.RemoveRange(DbSet.Where(x => x.Id == id));

            return await SaveChangesAsync();
        }

        public override async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DbSet.FirstOrDefaultAsync(whereExpression);
        }

        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return await DbSet
                .AsNoTracking()
                .Where(whereExpression)
                .ToListAsync();
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);

            return await SaveChangesAsync();
        }

        public override async Task<bool> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);

            return await SaveChangesAsync();
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);

            return await SaveChangesAsync();
        }

        public override async Task<bool> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
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
            //return await _context.SaveChangesAsync() >= 0;

            // 此处不直接 SaveChangesAsync；通过 Uow 统计提交以便于实现事务
            return await Task.FromResult(true);
        }


        #region Private Method
        protected virtual DbSet<TEntity> DbSet => _entities ?? (_entities = _context.Set<TEntity>());

        
        #endregion

    }
}
