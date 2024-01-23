using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sql50.Sql50题.EfEntity;

/// <summary>
/// 教室表
/// </summary>
public partial class Teacher
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}