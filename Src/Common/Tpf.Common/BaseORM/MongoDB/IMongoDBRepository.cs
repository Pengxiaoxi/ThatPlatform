using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tpf.Common.BaseDomain.Entity;

namespace Tpf.Common.BaseORM.MongoDB
{
    public interface IMongoDBRepository<T> : IBaseRepository<T> where T : BaseEntity<string>
    {
        IMongoDatabase Database { get; }


        IMongoCollection<T> Collection { get; }


        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="projecter">查询字段控制类似select，默认是全部字段</param>
        /// <returns>Entity</returns>
        T GetById(string id, ProjectionDefinition<T, T> projecter = null);

        /// <summary>
        /// 获取对象集合根据id集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeFields">包含的lanbda表达式字段</param>
        /// <param name="excludeFields">不包含的lanbda表达式字段</param>
        /// <returns></returns>
        T GetById(string id, List<Expression<Func<T, object>>> includeFields, List<Expression<Func<T, object>>> excludeFields = null);

        /// <summary>
        /// Get entity list by identifier
        /// </summary>
        /// <param name="ids">id 集合列表</param>
        /// <param name="projecter">查询字段控制类似select，默认是全部字段</param>
        /// <returns>Entity list</returns>
        List<T> GetByIds(IEnumerable<string> ids, ProjectionDefinition<T, T> projecter = null);

        /// <summary>
        /// 获取对象集合根据id集合
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="includeFields">包含的lanbda表达式字段</param>
        /// <param name="excludeFields">不包含的lanbda表达式字段</param>
        /// <returns></returns>
        List<T> GetByIds(IEnumerable<string> ids, List<Expression<Func<T, object>>> includeFields, List<Expression<Func<T, object>>> excludeFields = null);

        /// <summary>
        /// 根据条件获取结果列表
        /// </summary>
        /// <param name="filter">条件FilterDefinition</param>
        /// <param name="sorter">排序规则定义</param>
        /// <param name="projecter">查询字段定义</param>
        /// <returns>结果列表</returns>
        List<T> Find(FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null, SortDefinition<T> sorter = null);

        /// <summary>
        /// 根据条件获取结果列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="projecter"></param>
        /// <param name="sorter"> 根据排序字符串（和sql一样），规则如下：Model属性名 [asc|desc]  ，默认asc可以不写，eg: id,name desc </param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter, string sorter);

        /// <summary>
        /// 根据条件获取结果列表
        /// </summary>
        /// <param name="filter">条件Expression</param>
        /// <returns>结果列表</returns>
        List<T> Find(Expression<Func<T, bool>> filter, ProjectionDefinition<T, T> projecter = null, SortDefinition<T> sorter = null);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T, T> projecter = null, SortDefinition<T> sorter = null);

        /// <summary>
        /// 根据条件获取结果列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="projecter"></param>
        /// <param name="sorter"> 根据排序字符串（和sql一样），规则如下：Model属性名 [asc|desc]  ，默认asc可以不写，eg: id,name desc </param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> filter, ProjectionDefinition<T, T> projecter, string sorter);

        /// <summary>
        /// 根据条件获取结果列表
        /// </summary>
        /// <param name="filter">条件Lambada表达式</param>
        /// <param name="projecter">查询字段控制类似select</param>
        /// <param name="pageInfo">分页查询对象，且pageInfo.PageIndex>0才会执行分页</param>
        /// <param name="orderBys">
        /// 排序，支持多个组合，类型Tuple可变数组，
        /// Tuple2个值，第一个是关联字段T的lanbda表达式，第二个是否升序（true:升序 false:降序）</param>
        /// <returns>查询结果列表</returns>
        //List<T> Find(Expression<Func<T, bool>> filter = null, ProjectionDefinition<T, T> projecter = null, PageInfo? pageInfo = null, params ValueTuple<Expression<Func<T, object>>, bool>[] orderBys);

        /// <summary>
        /// 根据条件获取结果列表
        /// </summary>
        /// <param name="filter">条件Lambada表达式</param>
        /// <param name="selector">查询条件Lambada表达式</param>
        /// <param name="orderBys">
        /// 排序，支持多个组合，类型Tuple可变数组，
        /// Tuple2个值，第一个是关联字段T的lanbda表达式，第二个是否升序（true:升序 false:降序）</param>
        /// <returns>查询结果列表</returns>
        List<TResult> Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> selector, params ValueTuple<Expression<Func<T, object>>, bool>[] orderBys);

        /// <summary>
        /// 根据条件获取结果
        /// </summary>
        /// <param name="filter">条件FilterDefinition</param>
        /// <param name="projecter">查询字段控制类似select</param>
        /// <param name="orderBys">
        /// 排序，支持多个组合，类型Tuple可变数组，
        /// Tuple2个值，第一个是关联字段T的lanbda表达式，第二个是否升序（true:升序 false:降序）</param>
        /// <returns>结果</returns>
        T FindOne(FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null, params ValueTuple<Expression<Func<T, object>>, bool>[] orderBys);

        /// <summary>
        /// 根据条件获取第一个结果
        /// </summary>
        /// <param name="filter">条件Lambada表达式</param>
        /// <param name="projecter">查询字段控制类似select</param>
        /// <param name="orderBys">
        /// 排序，支持多个组合，类型Tuple可变数组，
        /// Tuple2个值，第一个是关联字段T的lanbda表达式，第二个是否升序（true:升序 false:降序）</param>
        /// <returns>查询结果列表</returns>
        T FindOne(Expression<Func<T, bool>> filter = null, ProjectionDefinition<T, T> projecter = null, params ValueTuple<Expression<Func<T, object>>, bool>[] orderBys);

        T FindOneForGridFS(Expression<Func<T, bool>> filter = null);

        /// <summary>
        ///  IRepository的分页查询的扩展方法.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="pageInfo">分页对象，子属性pageIndex:从1开始</param>
        /// <param name="filter">查询条件FilterDefinition</param>
        /// <param name="projector">查询字段控制类似select</param>
        /// <returns>Tuple返回值，itme1:总页数 itme2:当前分页的数据集合List</returns>
        //Tuple<long, List<T>> GetPageList(PageInfo pageInfo, FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null);

        /// <summary>
        ///  IRepository的分页查询的扩展方法.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageInfo">分页对象，子属性pageIndex:从1开始</param>
        /// <param name="filter">查询条件FilterDefinition</param>
        /// <param name="projecter">查询字段控制类似select</param>
        /// <returns>Tuple返回值，itme1:总页数 itme2:当前分页的数据集合List</returns>
        //ValueTuple<Task<long>, Task<List<T>>> GetPageListAsync(PageInfo pageInfo, FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null);

        /// <summary>
        ///  IRepository的分页查询的扩展方法.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="pageInfo">分页对象，子属性pageIndex:从1开始</param>
        /// <param name="filter">查询条件BsonDocument</param>
        /// <returns>Tuple返回值，itme1:总页数 itme2:当前分页的数据集合List</returns>
        //Tuple<long, List<T>> GetPageList(PageInfo pageInfo, BsonDocument filter);

        /// <summary>
        /// 根据条件统计总数
        /// </summary>
        /// <param name="filter">条件FilterDefinition</param>
        /// <returns>总数</returns>
        long Count(FilterDefinition<T> filter);

        

        /// <summary>
        /// 根据条件是否存在数据
        /// </summary>
        /// <param name="filter">条件FilterDefinition</param>
        /// <returns>是否存在，true:是 false：否</returns>
        bool Exists(FilterDefinition<T> filter);

        

        

        

        /// <summary>
        /// 单个对象更新操作,底层调用时 FindOneAndUpdate
        /// </summary>
        /// <param name="expression">条件Lambada表达式</param>
        /// <param name="updateDefinition">查询条件</param>
        /// <param name="arrayFilters">递归属性集合对象条件</param>
        /// <param name="option">FindOneAndUpdateOption对象</param>
        /// <returns></returns>
        T Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition, IEnumerable<ArrayFilterDefinition> arrayFilters = null, FindOneAndUpdateOptions<T, T> option = null);

        /// <summary>
        ///  根据id 进行部分更新,且返回更新后对象
        /// </summary>
        /// <param name="id">对象主键id</param>
        /// <param name="updateDefinition">部分更新对象</param>
        /// <param name="arrayFilters">递归属性集合对象条件</param>
        T UpdateById(string id, UpdateDefinition<T> updateDefinition, IEnumerable<ArrayFilterDefinition> arrayFilters = null);

        /// <summary>
        /// 单个对象修改操作，Update record by specified filer. 
        /// </summary>
        /// <param name="filter">The filter expression which used to search the record to update.</param>
        /// <param name="updateDefinition">The update value expression.</param>
        /// <param name="arrayFilters">递归属性集合对象条件</param>
        /// <returns>The updated entity.</returns>
        T Update(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, IEnumerable<ArrayFilterDefinition> arrayFilters = null, FindOneAndUpdateOptions<T, T> option = null);

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <param name="filter">查询条件定义</param>
        /// <param name="updateDefinition">更新定义</param>
        /// <param name="options">更新相关设置</param>
        /// <returns></returns>
        UpdateResult Update(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, UpdateOptions options);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="filter">条件表达式.</param>
        /// <param name="updateDefinition">更新表达式</param>
        /// <returns>批量更新结果.</returns>
        UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, UpdateOptions options = null);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="expression">条件表达式.</param>
        /// <param name="updateDefinition">更新表达式</param>
        /// <returns>批量更新结果.</returns>
        UpdateResult UpdateMany(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition, UpdateOptions options = null);

        

        /// <summary>
        /// 批量删除根据FilterDefinition 条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isOne">是否删除一条数据，默认:false 代表 多条 ， true: 单条</param>
        /// <returns>DeleteResult</returns>
        DeleteResult Delete(FilterDefinition<T> filter, bool isOne = false);

        Task DeleteAsync(T entity);

        /// <summary>
        /// 异步删除对象根据Expression表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isOne">是否删除一条数据，默认:false 代表 多条 ， true: 单条</param>
        /// <returns>DeleteResult</returns>
        Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> expression, bool isOne = false);

        /// <summary>
        /// 异步批量删除根据FilterDefinition 条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isOne">是否删除一条数据，默认:false 代表 多条 ， true: 单条</param>
        /// <returns>DeleteResult</returns>
        Task<DeleteResult> DeleteAsync(FilterDefinition<T> filter, bool isOne = false);

        /// <summary>
        /// 根据id删除对象，返回删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T DeleteByIdAndFind(string id, FindOneAndDeleteOptions<T, T> options = null);

        
        /// <summary>
        ///  批处理操作,操作的数据条数需要大于0
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        BulkWriteResult BulkWrite(List<UpdateOneModel<T>> updates);


        #region Index
        /// <summary>
        /// 创建索引，支持多个字段组合索引
        /// </summary>
        /// <param name="indexName">索引名</param>
        /// <param name="tuples">
        /// 索引建立字段，支持多个组合，类型数组Tuple集合，
        /// Tuple2个值，第一个是关联字段 字符串表达式，第二个是否升序（true:升序 false:降序）
        /// </param>
        //public void CreateIndex(string indexName, params ValueTuple<string, bool>[] tuples);

        /// <summary>
        /// 创建索引，支持多个字段组合索引
        /// </summary>
        /// <param name="indexName">索引名,若null,则系统默认生成索引名字</param>
        /// <param name="tuples">
        /// 索引建立字段，支持多个组合，类型数组Tuple集合，
        /// Tuple2个值，第一个是关联字段T的lanbda表达式，第二个是否升序（true:升序 false:降序）
        /// </param>
        //void CreateIndex(string indexName, params ValueTuple<Expression<Func<T, object>>, bool>[] tuples);

        /// <summary>
        /// 创建单字段索引
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="bAscending">是否升序</param>
        /// <param name="indexOption">索引条件</param>
        //void CreateIndex(Expression<Func<T, object>> expression, bool bAscending, CreateIndexOptions indexOption = null);

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="indexNames">索引名称集合</param>
        //void DropIndex(params string[] indexNames); 
        #endregion

    }
}
