//using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tpf.Domain.Base.Domain.Context;
using Tpf.Utils.Guids;

namespace Tpf.Domain.Base.Domain.Entity
{
    /// <summary>
    /// BaseEntity<T>
    /// TODO: 考虑区分关系型数据库和非关系型数据库 BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T> where T : class //, IDelete
    {
        #region Ctor
        public BaseEntity()
        {

        }
        #endregion


        #region Field
        /// <summary>
        /// Key
        /// </summary>
        [Key]
        [Column("id")] // EFCore、Dapper
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true)] // SqlSugar
        [JsonProperty("id")]
        public string Id { get; set; }

        [Column("created_user_id")]
        [SugarColumn(ColumnName = "created_user_id")]
        [JsonProperty("created_user_id")]
        public string? CreatedUserId { get; set; }

        [Column("created_date")]
        [SugarColumn(ColumnName = "created_date")]
        [JsonProperty("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Column("update_user_id")]
        [SugarColumn(ColumnName = "update_user_id")]
        [JsonProperty("update_user_id")]
        public string? UpdateUserId { get; set; }

        [Column("update_date")]
        [SugarColumn(ColumnName = "update_date")]
        [JsonProperty("update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column("is_deleted")]
        [SugarColumn(ColumnName = "is_deleted")]
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        //[JsonProperty("tenant_id")]
        //public string? TenantId { get; set; }
        #endregion


        #region Extensions Method
        public void Create()
        {
            this.Id = GuidGenerator.Create();
            this.CreatedUserId = UserContext.CurrentUserAccount;
            this.CreatedDate = DateTime.Now;
            this.IsDeleted = false;
        }

        public void Modify(string id)
        {
            this.Id = id;
            this.UpdateUserId = UserContext.CurrentUserAccount;
            this.UpdateDate = DateTime.Now;
        }
        #endregion
    }

}
