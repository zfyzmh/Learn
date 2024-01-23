using System.ComponentModel.DataAnnotations;

namespace Sql50.Sql50题.EfEntity;

/// <summary>
/// 课程表
/// </summary>
public partial class Course
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 课程名称
    /// </summary>

    public string? Cname { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
}