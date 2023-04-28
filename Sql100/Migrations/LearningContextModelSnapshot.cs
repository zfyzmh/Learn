﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sql100;

#nullable disable

namespace Sql100.Migrations
{
    [DbContext(typeof(LearningContext))]
    partial class LearningContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cname")
                        .HasColumnType("longtext");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Sc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Scs");
                });

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Sage")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Ssex")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Course", b =>
                {
                    b.HasOne("Sql100.sql100题.EfEntity.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Sc", b =>
                {
                    b.HasOne("Sql100.sql100题.EfEntity.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sql100.sql100题.EfEntity.Student", "Student")
                        .WithMany("Scs")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Sql100.sql100题.EfEntity.Student", b =>
                {
                    b.Navigation("Scs");
                });
#pragma warning restore 612, 618
        }
    }
}
