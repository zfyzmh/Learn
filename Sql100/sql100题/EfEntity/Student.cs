using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sql100.sql100题.EfEntity;

/// <summary>
/// 学生表
/// </summary>
public partial class Student
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? Sage { get; set; }

    public string? Ssex { get; set; }

    public List<Sc> Scs { get; set; }
}