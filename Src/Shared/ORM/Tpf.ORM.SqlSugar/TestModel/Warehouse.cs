using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.ORM.SqlSugar.TestModel
{
    /// <summary>
    /// 仓库
    ///</summary>
    [SugarTable("tb_w_warehouse")]
    public class Warehouse
    {
        #region 实体字段
        /// <summary>
        /// 主键 
        ///</summary>
        [SugarColumn(ColumnName = "f_warehouseid", IsPrimaryKey = true)]
        public string? FWarehouseId { get; set; }
        /// <summary>
        /// 所属公司主键 
        ///</summary>
        [SugarColumn(ColumnName = "f_companyid")]
        public string? FCompanyId { get; set; }
        /// <summary>
        /// 所属片区 
        ///</summary>
        [SugarColumn(ColumnName = "f_areaid")]
        public string? FAreaId { get; set; }
        /// <summary>
        /// 污水厂编号 
        ///</summary>
        [SugarColumn(ColumnName = "f_code")]
        public string? FCode { get; set; }
        /// <summary>
        /// 仓库名称 
        ///</summary>
        [SugarColumn(ColumnName = "f_name")]
        public string? FName { get; set; }
        /// <summary>
        /// 负责人 
        ///</summary>
        [SugarColumn(ColumnName = "f_user")]
        public string? FUser { get; set; }
        /// <summary>
        /// 联系电话 
        ///</summary>
        [SugarColumn(ColumnName = "f_mobile")]
        public string? FMobile { get; set; }
        /// <summary>
        /// X坐标 
        ///</summary>
        [SugarColumn(ColumnName = "x_coor")]
        public string? XCoor { get; set; }
        /// <summary>
        /// Y坐标 
        ///</summary>
        [SugarColumn(ColumnName = "y_coor")]
        public string? YCoor { get; set; }
        /// <summary>
        /// 地址 
        ///</summary>
        [SugarColumn(ColumnName = "addr")]
        public string? Addr { get; set; }
        /// <summary>
        /// 删除标记 
        ///</summary>
        [SugarColumn(ColumnName = "f_deletemark")]
        public int? FDeletemark { get; set; }
        /// <summary>
        /// 排序码 
        ///</summary>
        [SugarColumn(ColumnName = "f_sortcode")]
        public int? FSortcode { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
        [SugarColumn(ColumnName = "f_description")]
        public string? FDescription { get; set; }
        /// <summary>
        /// 创建用户主键 
        ///</summary>
        [SugarColumn(ColumnName = "f_createuserid")]
        public string? FCreateuserid { get; set; }
        /// <summary>
        /// 创建日期 
        ///</summary>
        [SugarColumn(ColumnName = "f_createdate")]
        public DateTime? FCreatedate { get; set; }
        /// <summary>
        /// 修改用户主键 
        ///</summary>
        [SugarColumn(ColumnName = "f_modifyuserid")]
        public string? FModifyuserid { get; set; }
        /// <summary>
        /// 修改日期 
        ///</summary>
        [SugarColumn(ColumnName = "f_modifydate")]
        public DateTime? FModifydate { get; set; }
        /// <summary>
        /// 区域名称 
        ///</summary>
        [SugarColumn(ColumnName = "fareaname")]
        public string? FAreaName { get; set; }

        #endregion


        #region Extension Properties
        /// <summary>
        /// 状态  0报警 1正常
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string? Status { get; set; }
        #endregion


        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.FCreatedate = DateTime.Now;
            this.FDeletemark = 0;
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.FWarehouseId = keyValue;
            this.FModifydate = DateTime.Now;
            this.FDeletemark = 0;
        }
        #endregion


    }

}
