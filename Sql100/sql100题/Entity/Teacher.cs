using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;

namespace WebFirst.Entities
{
    /// <summary>
    ///
    ///</summary>
    [SugarTable("Teacher")]
    public class Teacher
    {
        /// <summary>
        ///  教师编号
        ///</summary>
        [SugarColumn(ColumnName = "TId")]
        public string TId { get; set; }

        /// <summary>
        ///  教师名称
        ///</summary>
        [SugarColumn(ColumnName = "Tname")]
        public string Tname { get; set; }
    }
}