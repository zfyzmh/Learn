using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;

namespace WebFirst.Entities
{
    /// <summary>
    /// 成绩表
    ///</summary>
    [SugarTable("SC")]
    public class Sc
    {
        /// <summary>
        ///  学生编号
        ///</summary>
        [SugarColumn(ColumnName = "SId")]
        public string SId { get; set; }

        /// <summary>
        ///  课程编号
        ///</summary>
        [SugarColumn(ColumnName = "CId")]
        public string CId { get; set; }

        /// <summary>
        ///  分数
        ///</summary>
        [SugarColumn(ColumnName = "score")]
        public decimal? Score { get; set; }
    }
}