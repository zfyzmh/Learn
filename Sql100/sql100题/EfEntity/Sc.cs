using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sql50.Sql50题.EfEntity;

/// <summary>
/// 成绩表
/// </summary>
public partial class Sc
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 课程编号
    /// </summary>
    public int CourseId { get; set; }

    public Course Course { get; set; }

    /// <summary>
    /// 学生编号
    /// </summary>
    public int StudentId { get; set; }

    public Student Student { get; set; }

    /// <summary>
    /// 成绩
    /// </summary>
    public decimal? Score { get; set; }
}