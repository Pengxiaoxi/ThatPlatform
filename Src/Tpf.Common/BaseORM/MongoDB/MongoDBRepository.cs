using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tpf.Common.BaseDomain.Entity;
using Tpf.Infrastructure.CommonAttributes;
using Tpf.Infrastructure.CommonAttributes.Database;

namespace Tpf.Common.BaseORM.MongoDB
{
    [DependsOn(typeof(IMongoDBRepository<>))]
    public class MongoDBRepository<T> : IMongoDBRepository<T> where T : BaseEntity<string>
    {
        #region Fields
        /// <summary>
        /// Gets the collection
        /// </summary>
        protected IMongoCollection<T> _collection;
        public IMongoCollection<T> Collection
        {
            get
            {
                return _collection;
            }
        }

        /// <summary>
        /// Mongo Database
        /// </summary>
        protected IMongoDatabase _database;
        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }
        #endregion

        #region Ctor
        public MongoDBRepository()
        {
            //var connectionString = "mongodb://42.192.5.10:27017";
            var connectionString = "mongodb://admin:admin996@42.192.5.10:27017";

            var client = new MongoClient(connectionString);

            var mongoDbCollectionAttribute = typeof(T).GetCustomAttributes(typeof(MongoDbCollectionAttribute), true).
                FirstOrDefault()
                as MongoDbCollectionAttribute;

            var databaseName = "tpf_default";
            var collectionName = "defaultTable";
            if (mongoDbCollectionAttribute is not null)
            {
                databaseName = mongoDbCollectionAttribute.DatabaseName;
                collectionName = mongoDbCollectionAttribute.CollectionName;
            }

            _database = client.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(collectionName);

        }
        #endregion


        #region Async
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return await this._collection.Find(expression).FirstOrDefaultAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, ProjectionDefinition<T, T> projecter = null, SortDefinition<T> sorter = null)
        {
            return (List<T>)await this._collection.FindAsync(expression, new FindOptions<T, T>() { Projection = projecter, Sort = sorter });
        }

        #endregion


        #region Sync
        public IQueryable<T> All()
        {
            throw new NotImplementedException();
        }

        public BulkWriteResult BulkWrite(List<UpdateOneModel<T>> updates)
        {
            throw new NotImplementedException();
        }

        public long Count(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<T, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public DeleteResult Delete(FilterDefinition<T> filter, bool isOne = false)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<T, bool>> expression, bool isOne = false)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> expression, bool isOne = false)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteAsync(FilterDefinition<T> filter, bool isOne = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public T DeleteByIdAndFind(string id, FindOneAndDeleteOptions<T, T> options = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public bool Exists(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null, SortDefinition<T> sorter = null)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter, string sorter)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(Expression<Func<T, bool>> filter, ProjectionDefinition<T, T> projecter = null, SortDefinition<T> sorter = null)
        {
            return this._collection.Find(filter).Project(projecter).Sort(sorter).ToList();
        }

        

        public List<T> Find(Expression<Func<T, bool>> filter, ProjectionDefinition<T, T> projecter, string sorter)
        {
            throw new NotImplementedException();
        }

        //public List<T> Find(Expression<Func<T, bool>> filter = null, ProjectionDefinition<T, T> projecter = null, PageInfo? pageInfo = null, params (Expression<Func<T, object>>, bool)[] orderBys)
        //{
        //    throw new NotImplementedException();
        //}

        public List<TResult> Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> selector, params (Expression<Func<T, object>>, bool)[] orderBys)
        {
            throw new NotImplementedException();
        }

        public T FindOne(FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null, params (Expression<Func<T, object>>, bool)[] orderBys)
        {
            throw new NotImplementedException();
        }

        public T FindOne(Expression<Func<T, bool>> filter = null, ProjectionDefinition<T, T> projecter = null, params (Expression<Func<T, object>>, bool)[] orderBys)
        {
            throw new NotImplementedException();
        }

        public T FindOneForGridFS(Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public T GetById(string id, ProjectionDefinition<T, T> projecter = null)
        {
            throw new NotImplementedException();
        }

        public T GetById(string id, List<Expression<Func<T, object>>> includeFields, List<Expression<Func<T, object>>> excludeFields = null)
        {
            throw new NotImplementedException();
        }

        public List<T> GetByIds(IEnumerable<string> ids, ProjectionDefinition<T, T> projecter = null)
        {
            throw new NotImplementedException();
        }

        public List<T> GetByIds(IEnumerable<string> ids, List<Expression<Func<T, object>>> includeFields, List<Expression<Func<T, object>>> excludeFields = null)
        {
            throw new NotImplementedException();
        }

        //public Tuple<long, List<T>> GetPageList(PageInfo pageInfo, FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public (Task<long>, Task<List<T>>) GetPageListAsync(PageInfo pageInfo, FilterDefinition<T> filter, ProjectionDefinition<T, T> projecter = null)
        //{
        //    throw new NotImplementedException();
        //}

        public T Insert(T entity, bool isAsync = false)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }



        public void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition, IEnumerable<ArrayFilterDefinition> arrayFilters = null, FindOneAndUpdateOptions<T, T> option = null)
        {
            throw new NotImplementedException();
        }

        public T Update(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, IEnumerable<ArrayFilterDefinition> arrayFilters = null, FindOneAndUpdateOptions<T, T> option = null)
        {
            throw new NotImplementedException();
        }

        public UpdateResult Update(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, UpdateOptions options)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return result.ModifiedCount > 0;
        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T UpdateById(string id, UpdateDefinition<T> updateDefinition, IEnumerable<ArrayFilterDefinition> arrayFilters = null)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, UpdateOptions options = null)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition, UpdateOptions options = null)
        {
            throw new NotImplementedException();
        }

        public T UpdateOrInsert(T entity, Expression<Func<T, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        
        #endregion

        #region Private

        #endregion
    }
}
