using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging;
using Sql100.sql100题.EfEntity;

namespace Sql100;

public partial class LearningContext : DbContext
{
    [Obsolete]
    public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

    public LearningContext()
    {
    }

    public LearningContext(DbContextOptions<LearningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Sc> Scs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=127.0.0.1;port=3307;database=learning2;uid=root;pwd=210610zho.", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
        optionsBuilder.UseLoggerFactory(LoggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Method intentionally left empty.
    }
}