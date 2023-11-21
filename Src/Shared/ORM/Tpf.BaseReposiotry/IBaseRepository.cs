using System.Linq.Expressions;

namespace Tpf.BaseRepository
{
    /// <summary>
    /// IBaseRepository<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> // where T : class
    {
        /// <summary>
        /// 获取列表 ，返回IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> All();

        /// <summary>
        /// 获取查询列表 ，返回IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        /// <summary>
        /// FindOneAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> expression);

        ///// <summary>
        ///// 根据条件统计总数
        ///// </summary>
        ///// <param name="filter">条件Expression,可以空，代表获取表的总数</param>
        ///// <returns>总数</returns>
        //long Count(Expression<Func<T, bool>> expression = null);

        ///// <summary>
        ///// 根据条件是否存在数据
        ///// </summary>
        ///// <param name="filter">条件Expression</param>
        ///// <returns>是否存在，true:是 false：否</returns>
        //bool Exists(Expression<Func<T, bool>> expression);

        ///// <summary>
        ///// Insert entity
        ///// </summary>
        ///// <param name="entity">Entity</param>
        ///// <param name="isAsync">是否异步处理，默认：false : 否  ,true: 是</param>
        //T Insert(T entity, bool isAsync = false);

        ///// <summary>
        ///// Insert entities
        ///// </summary>
        ///// <param name="entities">Entities</param>
        //void Insert(IEnumerable<T> entities);

        Task InsertAsync(T entity);

        ///// <summary>
        ///// 批量插入数据，和Insert的区别是会判断插入大小不要超过GridFS大小
        ///// 正常情况使用Insert
        ///// </summary>
        ///// <param name="entities"></param>
        //void InsertMany(IEnumerable<T> entities);


        ///// <summary>
        ///// 单个对象Update entity
        ///// </summary>
        ///// <param name="entity">Entity</param>
        //T Update(T entity);

        Task<bool> UpdateAsync(T entity);

        ///// <summary>
        ///// Update entities
        ///// </summary>
        ///// <param name="entities">Entities</param>
        //void Update(IEnumerable<T> entities);

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

        ///// <summary>
        ///// 根据id删除对象
        ///// </summary>
        ///// <param name="id"></param>
        //void DeleteById(string id);


        ///// <summary>
        ///// 根据id列表批量删除 对象
        ///// </summary>
        ///// <param name="ids">id列表</param>
        //void DeleteByIds(IEnumerable<string> ids);

        Task DeleteAsync(T entity);

        Task DeleteAsync(Expression<Func<T, bool>> expression);

    }
}
