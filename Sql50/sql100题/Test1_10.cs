using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using Sql50;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebFirst.Entities;
using System.Linq.Expressions;
using Sql50.Sql50题.EfEntity;
using Student = WebFirst.Entities.Student;
using Sc = WebFirst.Entities.Sc;
using Course = WebFirst.Entities.Course;
using Teacher = WebFirst.Entities.Teacher;

namespace Sql50
{
    public class Test1_20
    {
        private static SqlSugarScope _db = Database.Instance;
        private LearningContext _context = new LearningContext();

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// 查询" 01 “课程比” 02 “课程成绩高的学生的信息及课程分数
        /// </summary>
        [Test]
        public void Test1()
        {
            var sql = @"SELECT s.SId, s.Sname, s.Ssex, sc1.score as '01_score', sc2.score as '02_score'
                        FROM Student s
                        LEFT JOIN SC sc1 ON s.SId = sc1.SId AND sc1.CId = '01'
                        LEFT JOIN SC sc2 ON s.SId = sc2.SId AND sc2.CId = '02'
                        WHERE sc1.score > sc2.score;";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Student>()
                .LeftJoin<Sc>((s, sc1) => s.SId == sc1.SId && sc1.CId == "01")
                .LeftJoin<Sc>((s, sc1, sc2) => s.SId == sc2.SId && sc2.CId == "02")
                .Where((s, sc1, sc2) => sc1.Score > sc2.Score)
                .Select((s, sc1, sc2) => new { sid = s.SId, sname = s.Sname, sc1 = sc1.Score, sc2 = sc2.Score })
                .ToList();

            var result2 = _context.Students
            .Where(s => s.Scs.Any(sc => sc.CourseId == 1 && sc.Score > s.Scs.FirstOrDefault(sc2 => sc2.CourseId == 2).Score))
            .Select(s => new Result(s.Id, s.Name, s.Scs.FirstOrDefault(sc => sc.CourseId == 1).Score, s.Scs.FirstOrDefault(sc => sc.CourseId == 2).Score))
            .ToList();
        }

        /// <summary>
        /// 查询存在" 01 “课程但可能不存在” 02 "课程的情况(不存在时显示为 null )
        /// </summary>
        [Test]
        public void Test1_1()
        {
            var sql = @"SELECT s.SId, s.Sname, s.Ssex, sc1.score as 'score1', sc2.score as 'score2'
                        FROM Student s
                        INNER JOIN SC sc1 ON s.SId = sc1.SId AND sc1.CId = '01'
                        LEFT JOIN SC sc2 ON s.SId = sc2.SId AND sc2.CId = '02';";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Ssex, s.Sname, s.score1, s.score2 });

            var result = _db.Queryable<Student>()
                .InnerJoin<Sc>((s, sc1) => s.SId == sc1.SId && sc1.CId == "01")
                .LeftJoin<Sc>((s, sc1, sc2) => s.SId == sc2.SId && sc2.CId == "02")
                .Select((s, sc1, sc2) => new { sid = s.SId, sname = s.Sname, sc1 = sc1.Score, sc2 = sc2.Score })
                .ToList();

            var result2 = _context.Students
                        .Where(m => m.Scs.FirstOrDefault(a => a.CourseId == 1) != null)
                        .Select(s => new Result(s.Id, s.Name, s.Scs.First(sc => sc.CourseId == 1).Score, s.Scs.FirstOrDefault(sc => sc.CourseId == 2) == null ? null : s.Scs.First(sc => sc.CourseId == 2).Score)
                        )
                        .ToList();
        }

        public record Result(int sid, string sname, decimal? c1, decimal? c2);

        /// <summary>
        /// 查询不存在" 01 “课程但存在” 02 "课程的情况
        /// </summary>
        [Test]
        public void Test1_2()
        {
            var sql = @"SELECT s.SId, s.Sname, s.Ssex,sc1.score as 'score1', sc2.score as 'score2'
                        FROM Student s
                        INNER JOIN SC sc2 ON s.SId = sc2.SId AND sc2.CId = '02'
                        LEFT JOIN SC sc1 ON s.SId = sc1.SId AND sc1.CId = '01'
                        WHERE sc1.score IS NULL;";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Ssex, s.Sname, s.score1, s.score2 });

            var result = _db.Queryable<Student>()
                .LeftJoin<Sc>((s, sc1) => s.SId == sc1.SId && sc1.CId == "01")
                .InnerJoin<Sc>((s, sc1, sc2) => s.SId == sc2.SId && sc2.CId == "02")
                .Where((s, sc1, sc2) => sc1.Score == null)
                .Select((s, sc1, sc2) => new { sid = s.SId, sname = s.Sname, sc1 = sc1.Score, sc2 = sc2.Score })
                .ToList();

            var result2 = _context.Students
                        .Where(m => m.Scs.FirstOrDefault(a => a.CourseId == 1) == null && m.Scs.FirstOrDefault(a => a.CourseId == 2) != null)
                        .Select(s => new Result(s.Id, s.Name, null, s.Scs.First(sc => sc.CourseId == 2).Score)
                        )
                        .ToList();
        }

        /// <summary>
        /// 查询同时存在” 01 “课程和” 02 "课程的情况
        /// </summary>
        [Test]
        public void Test1_3()
        {
            var sql = @"SELECT s.SId, s.Sname, s.Ssex,sc1.score as 'score1', sc2.score as 'score2'
                        FROM Student s
                        INNER JOIN SC sc2 ON s.SId = sc2.SId AND sc2.CId = '02'
                        INNER JOIN SC sc1 ON s.SId = sc1.SId AND sc1.CId = '01'
                        ";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Ssex, s.Sname, s.score1, s.score2 });

            var result = _db.Queryable<Student>()
                .InnerJoin<Sc>((s, sc1) => s.SId == sc1.SId && sc1.CId == "01")
                .InnerJoin<Sc>((s, sc1, sc2) => s.SId == sc2.SId && sc2.CId == "02")
                .Select((s, sc1, sc2) => new { sid = s.SId, sname = s.Sname, sc1 = sc1.Score, sc2 = sc2.Score })
                .ToList();

            var result2 = _context.Students
                        .Where(m => m.Scs.FirstOrDefault(a => a.CourseId == 1) != null && m.Scs.FirstOrDefault(a => a.CourseId == 2) != null)
                        .Select(s => new Result(s.Id, s.Name, null, s.Scs.First(sc => sc.CourseId == 2).Score)
                        )
                        .ToList();
        }

        /// <summary>
        /// 查询平均成绩大于等于 60 分的同学的学生编号和学生姓名和平均成绩
        /// </summary>
        [Test]
        public void Test2()
        {
            var sql = @"SELECT SC.SId, Student.Sname, AVG(SC.score) AS 'average_score'
                        FROM SC
                        INNER JOIN Student ON SC.SId = Student.SId
                        GROUP BY SC.SId, Student.Sname
                        HAVING AVG(SC.score) >= 60;";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Sname, s.average_score });

            var result = _db.Queryable<Student>()
                .InnerJoin<Sc>((s, c) => s.SId == c.SId)
                .GroupBy((s, c) => new { s.SId, s.Sname })
                .Having((s, c) => SqlFunc.AggregateAvg(c.Score) > 60)
                .Select((s, c) => new
                {
                    average_score = SqlFunc.AggregateAvg(c.Score),
                    s.SId,
                    s.Sname
                })
                .ToList();

            var result2 = _context.Students
                .Where(s => s.Scs.Average(sc => sc.Score) >= 60)
                .Select(s => new
                {
                    sid = s.Id,
                    sname = s.Name,
                    avgScore = s.Scs.Average(sc => sc.Score)
                })
                .ToList();
        }

        /// <summary>
        /// 查询在SC表存在成绩的学生信息
        /// </summary>
        [Test]
        public void Test3()
        {
            var sql = @"SELECT DISTINCT s.SId, s.Sname, s.Ssex
                        FROM Student s
                        INNER JOIN SC sc ON s.SId = sc.SId;";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Sname, s.Ssex });

            var result = _db.Queryable<Student>()
                .Distinct()
                .InnerJoin<Sc>((s, c) => s.SId == c.SId)
                .ToList();

            var result2 = _context.Students
                .Where(s => s.Scs.Count > 0)
                .ToList();
        }

        /// <summary>
        /// 查询所有同学的学生编号、学生姓名、选课总数、所有课程的总成绩(没成绩的显示为 null )
        /// </summary>

        [Test]
        public void Test4()
        {
            var sql = @"SELECT s.SId AS sid, s.Sname AS sname, COUNT(sc.CId) AS courseCount, SUM(sc.Score) AS totalScore
                        FROM Student s
                        LEFT JOIN SC sc ON s.SId = sc.SId
                        GROUP BY s.SId, s.Sname;";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Sname, s.courseCount, s.totalScore });

            var result = _db.Queryable<Student>()
                .LeftJoin<Sc>((s, c) => s.SId == c.SId)
                .GroupBy((s, c) => new { s.SId, s.Sname, s.Sage })
                .Select((s, c) => new { s.SId, s.Sname, s.Sage, totalScore = SqlFunc.AggregateSum(c.Score), courseCount = SqlFunc.AggregateCount(c.Score) })
                .ToList();

            var result2 = _context.Students
                .Select(m => new { m.Id, m.Name, count = m.Scs.Count, Sum = m.Scs.Sum(m => m.Score) })
                .ToList();
        }

        /// <summary>
        /// 查有成绩的学生信息
        /// </summary>

        [Test]
        public void Test4_1()
        {
            var sql = @"SELECT s.SId AS sid, s.Sname AS sname, COUNT(sc.CId) AS courseCount, SUM(sc.Score) AS totalScore
                        FROM Student s
                        LEFT JOIN SC sc ON s.SId = sc.SId
                        WHERE sc.Score IS NOT NULL
                        GROUP BY s.SId, s.Sname
                        ;
                        ";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql).Select(s => new { s.SId, s.Sname, s.courseCount, s.totalScore });

            var result = _db.Queryable<Student>()
                .LeftJoin<Sc>((s, c) => s.SId == c.SId)
                .Where((s, c) => c.Score != null)
                .GroupBy((s, c) => new { s.SId, s.Sname, s.Sage })
                .Select((s, c) => new { s.SId, s.Sname, s.Sage, totalScore = SqlFunc.AggregateSum(c.Score), courseCount = SqlFunc.AggregateCount(c.Score) })
                .ToList();

            var result2 = _context.Students
                .Where(m => m.Scs.Any())
                .Select(m => new { m.Id, m.Name, count = m.Scs.Count, Sum = m.Scs.Sum(m => m.Score) })
                .ToList();
        }

        /// <summary>
        /// 查询「李」姓老师的数量
        /// </summary>

        [Test]
        public void Test5()
        {
            var sql = @"SELECT COUNT(1) FROM `Teacher`  WHERE  (`Tname` like concat('李','%'));
                        ";
            var sqllist = _db.Ado.SqlQuery<int>(sql);

            var result = _db.Queryable<Teacher>()
                .Where(m => m.Tname.StartsWith("李"))
                .Count();

            var result2 = _context.Teachers
                .Where(m => m.Name.StartsWith("李"))
                .Count();
        }

        /// <summary>
        /// 查询学过「张三」老师授课的同学的信息
        /// </summary>
        [Test]
        public void Test6()
        {
            var sql = @"SELECT
	                    `st`.`SId`,
	                    `st`.`Sname`,
	                    `st`.`Sage`,
	                    `st`.`Ssex`
                    FROM
	                    `Student` `st`
	                    LEFT JOIN `SC` `sc` ON ( `sc`.`SId` = `st`.`SId` )
	                    LEFT JOIN `Course` `co` ON ( `sc`.`CId` = `co`.`CId` )
	                    LEFT JOIN `Teacher` `t` ON ( `co`.`TId` = `t`.`TId` )
                    WHERE
	                    ( `t`.`Tname` = '张三' )
                                            ";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql);

            var result = _db.Queryable<Student>()
                .LeftJoin<Sc>((st, sc) => sc.SId == st.SId)
                .LeftJoin<Course>((st, sc, co) => sc.CId == co.CId)
                .LeftJoin<Teacher>((st, sc, co, t) => co.TId == t.TId)
                .Where((st, sc, co, t) => t.Tname == "张三")
                .ToList();

            var result2 = _context.Students
                .Where(m => m.Scs.Any(sc => sc.Course.Teacher.Name == "张三"))
                .ToList();
        }

        /// <summary>
        /// 查询没有学全所有课程的同学的信息
        /// </summary>
        [Test]
        public void Test7()
        {
            var sql = @"SELECT
	                        *
                        FROM
	                        Student
                        WHERE
	                        ( SELECT COUNT( DISTINCT CId ) FROM SC WHERE SId = Student.SId ) < (
	                        SELECT
		                        COUNT(*)
                        FROM
	                        Course)";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql);

            var result = _db.Queryable<Student>()
                .Where(s => SqlFunc.Subqueryable<Sc>().Where(sc => sc.SId == s.SId).DistinctCount(m => m.CId) < _db.Queryable<Course>().Count())
                .ToList();

            var result2 = _context.Students
                .Where(m => m.Scs.GroupBy(m => m.CourseId).Count() < _context.Courses.Count())
                .ToList();
        }

        /// <summary>
        /// 查询至少有一门课与学号为" 01 "的同学所学相同的同学的信息
        /// </summary>
        [Test]
        public void Test8()
        {
            var sql = @"SELECT DISTINCT
	                    s2.*
                    FROM
	                    Student s1
	                    INNER JOIN SC sc1 ON s1.SId = sc1.SId
	                    INNER JOIN SC sc2 ON sc1.CId = sc2.CId
	                    AND sc1.SId != sc2.SId
	                    INNER JOIN Student s2 ON s2.SId = sc2.SId
                    WHERE
	                    s1.SId = '01'";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql);

            var result = _db.Queryable<Student, Sc>((st, sc) => st.SId == sc.SId)
                .Where((st, sc) => sc.CId != "01" && _db.Queryable<Sc>().Where(x => x.SId == "01").Select(x => x.CId).ToList().Contains(sc.CId))
                .Select((st, sc) => st)
                .Distinct()
                .ToList();

            var result2 = _context.Students
                .Where(s => s.Scs.Any(sc => _context.Students.Single(m => m.Id == 1).Scs.Any(sc2 => sc2.CourseId == sc.CourseId)))
                .ToList();
        }

        /// <summary>
        /// 查询和" 01 "号的同学学习的课程 完全相同的其他同学的信息
        /// </summary>
        [Test]
        public void Test9()
        {
            var sql = @"SELECT DISTINCT s2.*
                        FROM Student s1
                        INNER JOIN SC sc1 ON s1.SId = sc1.SId AND sc1.CId != '01'
                        INNER JOIN SC sc2 ON sc1.CId = sc2.CId AND sc2.SId != '01'
                        INNER JOIN Student s2 ON sc2.SId = s2.SId
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM SC sc3
                            WHERE sc3.SId = '01'
                            AND NOT EXISTS (
                                SELECT 1
                                FROM SC sc4
                                WHERE sc4.SId = s2.SId AND sc3.CId = sc4.CId
                            )
                        )";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql);
            var sameCourseList = _db.Queryable<Sc>()
                                .Where(sc => sc.SId == "01")
                                .Select(sc => sc.CId)
                                .ToList();
            var result = _db.Queryable<Sc>()
                            .LeftJoin<Student>((sc, st) => sc.SId == st.SId)
                            .Where(sc => sameCourseList.Contains(sc.CId))
                            .GroupBy((sc, st) => new { sc.SId, st.Sname })
                            .Where(sc => sc.SId != "01")
                            .Having(sc => SqlFunc.AggregateCount(sc.CId) == sameCourseList.Count)
                            .Select((sc, st) => new
                            {
                                sc.SId,
                                st.Sname
                            })
                            .ToList();

            var Scs1 = _context.Scs.Where(m => m.StudentId == 1);

            var result2 = _context.Students
                .Where(s => s.Id != 1 && s.Scs.Select(sc => sc.Id).Distinct().Count() == Scs1.Select(sc => sc.CourseId).Distinct().Count()
                      && s.Scs.All(sc => Scs1.Any(sc1 => sc1.CourseId == sc.CourseId)))
                .ToList();
        }

        /// <summary>
        /// 查询没学过"张三"老师讲授的任一门课程的学生姓名
        /// </summary>
        [Test]
        public void Test10()
        {
            var sql = @"SELECT  s.Sname
                                FROM Student s
                                WHERE s.SId NOT IN (
                                    SELECT sc.SId
                                    FROM SC sc
                                    INNER JOIN Course c ON sc.CId = c.CId
                                    INNER JOIN Teacher t ON c.TId = t.TId
                                    WHERE t.Tname = '张三'
                                )";
            var sqllist = _db.Ado.SqlQuery<dynamic>(sql);
            var sameCourseList = _db.Queryable<Sc>()
                                .Where(sc => sc.SId == "01")
                                .Select(sc => sc.CId)
                                .ToList();
            var result = _db.Queryable<Sc>()
                            .LeftJoin<Student>((sc, st) => sc.SId == st.SId)
                            .Where(sc => sameCourseList.Contains(sc.CId))
                            .GroupBy((sc, st) => new { sc.SId, st.Sname })
                            .Where(sc => sc.SId != "01")
                            .Having(sc => SqlFunc.AggregateCount(sc.CId) == sameCourseList.Count)
                            .Select((sc, st) => new
                            {
                                sc.SId,
                                st.Sname
                            })
                            .ToList();

            var result2 = _context.Students
                .Where(s => !s.Scs.Any(sc => sc.Course.Teacher.Name == "张三"))
                .Select(s => s.Name)
                .ToList();
        }
    }
}