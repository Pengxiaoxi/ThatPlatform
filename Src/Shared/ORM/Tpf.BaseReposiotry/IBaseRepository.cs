using System.Linq.Expressions;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.BaseRepository
{
    /// <summary>
    /// IBaseRepository<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>  where T : BaseEntity<string>
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
        Task<long> CountAsync(Expression<Func<T, bool>> whereExpression = null);

        /// <summary>
        /// 根据条件是否存在数据
        /// </summary>
        /// <param name="filter">条件Expression</param>
        /// <returns>是否存在，true:是 false：否</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression);


        #region Extensions
        ///// <summary>
        ///// 获取列表 ，返回IQueryable
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //IQueryable<T> All();

        ///// <summary>
        ///// 获取查询列表 ，返回IQueryable
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="expression"></param>
        ///// <returns></returns>
        //IQueryable<T> Where(Expression<Func<T, bool>> expression);

        ///// <summary>
        /////  根据id更新对象，若不存id则会添加
        /////  和Update操作区别开
        ///// </summary>
        ///// <param name="entity">操作对象</param>
        ///// <param name="expression">Lambda查询表达式</param>
        ///// <returns></returns>
        //T UpdateOrInsert(T entity, Expression<Func<T, bool>> expression = null);

        ///// <summary>
        ///// Delete entity
        ///// </summary>
        ///// <param name="entity">Entity</param>
        //void Delete(T entity);

        ///// <summary>
        ///// Delete entities
        ///// </summary>
        ///// <param name="entities">Entities</param>
        //void Delete(IEnumerable<T> entities);

        ///// <summary>
        ///// 删除对象根据Expression表达式
        ///// </summary>
        ///// <param name="expression"></param>
        ///// <param name="isOne">是否删除一条数据，默认:false 代表 多条 ， true: 单条</param>
        ///// <returns>DeleteResult</returns>
        //int Delete(Expression<Func<T, bool>> expression, bool isOne = false); 
        #endregion

    }
}
