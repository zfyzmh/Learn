using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;

namespace WebFirst.Entities
{
    /// <summary>
    /// 学生表
    ///</summary>
    [SugarTable("Student")]
    public class Student
    {
        /// <summary>
        ///  学生编号
        ///</summary>
        [SugarColumn(ColumnName = "SId")]
        public string SId { get; set; }

        /// <summary>
        ///  学生名称
        ///</summary>
        [SugarColumn(ColumnName = "Sname")]
        public string Sname { get; set; }

        /// <summary>
        ///  学生生日
        ///</summary>
        [SugarColumn(ColumnName = "Sage")]
        public DateTime? Sage { get; set; }

        /// <summary>
        ///  学生性别
        ///</summary>
        [SugarColumn(ColumnName = "Ssex")]
        public string Ssex { get; set; }
    }
}