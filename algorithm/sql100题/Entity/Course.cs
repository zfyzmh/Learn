using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;

namespace WebFirst.Entities
{
    /// <summary>
    ///
    ///</summary>
    [SugarTable("Course")]
    public class Course
    {
        /// <summary>
        /// 课程编号
        ///</summary>
        [SugarColumn(ColumnName = "CId")]
        public string CId { get; set; }

        /// <summary>
        /// 课程名称
        ///</summary>
        [SugarColumn(ColumnName = "Cname")]
        public string Cname { get; set; }

        /// <summary>
        /// 教师编号
        ///</summary>
        [SugarColumn(ColumnName = "TId")]
        public string TId { get; set; }
    }
}