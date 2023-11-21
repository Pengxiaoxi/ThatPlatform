using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.SqlSugar.TestModel
{
    /// <summary>
    /// 化验室填报公式页面配置
    /// </summary>
    [SugarTable("tb_sewage_labmpointpageconfig")]
    public class TB_Sewage_LabMpointPageConfigEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "f_id")]
        public string? F_ID { get; set; }
        /// <summary>
        /// 指标编码
        /// </summary>
        [SugarColumn(ColumnName = "f_mpointid")]
        public string? F_MpointId { get; set; }


        /// <summary>
        /// 采样点类别
        /// </summary>
        [SugarColumn(ColumnName = "f_checkpointtype")]
        public string? F_CheckPointType { get; set; }

        /// <summary>
        /// 页面类别(0公式，1计算)
        /// </summary>
        [SugarColumn(ColumnName = "f_pagetype")]
        public string? F_PageType { get; set; }


        /// <summary>
        /// 页面编码
        /// </summary>
        [SugarColumn(ColumnName = "f_pagecode")]
        public string? F_PageCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "f_sortcode")]
        public int F_SortCode { get; set; }


        public void Create()
        {
            //this.F_ID = CommonHelper.GetGuid;
        }
    }
}
