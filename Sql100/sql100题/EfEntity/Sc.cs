using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sql100.sql100题.EfEntity;

public partial class Sc
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }

    public decimal Score { get; set; }
}