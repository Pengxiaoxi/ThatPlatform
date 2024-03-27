using System.Linq.Expressions;

namespace Tpf.Domain.Base.Application.Svc
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? whereExpression = null);

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(T entity);

        /// <summary>
        /// InsertManyAsync
        /// </summary>
        /// <param name="entities">Entities</param>
        Task<bool> InsertManyAsync(IEnumerable<T> entities);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression);

        /// <summary>
        /// UpdateManyAsync
        /// </summary>
        /// <param name="entities">entities</param>
        Task<bool> UpdateManyAsync(IEnumerable<T> entities);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        Task<bool> DeleteByIdAsync(string id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 根据条件统计总数
        /// </summary>
        /// <param name="filter">条件Expression,可以空，代表获取表的总数</param>
        /// <returns>总数</returns>
        Task<int> CountAsync(Expression<Func<T, bool>>? whereExpression = null);

        /// <summary>
        /// 根据条件是否存在数据
        /// </summary>
        /// <param name="filter">条件Expression</param>
        /// <returns>是否存在，true:是 false：否</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression);

    }
}
