using System.ComponentModel.DataAnnotations;

namespace Sql100.sql100题.EfEntity;

public partial class Course
{
    [Key]
    public int Id { get; set; }

    public string? Cname { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
}