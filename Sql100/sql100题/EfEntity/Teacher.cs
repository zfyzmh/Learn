using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sql100.sql100题.EfEntity;

public partial class Teacher
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}