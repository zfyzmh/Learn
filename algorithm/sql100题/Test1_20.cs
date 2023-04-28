using NUnit.Framework;
using SixLabors.ImageSharp.PixelFormats;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebFirst.Entities;

namespace algorithm.sql100题
{
    public class Test1_20
    {
        private static SqlSugarScope _db = Database.Instance;

        /// <summary>
        /// 查询" 01 “课程比” 02 “课程成绩高的学生的信息及课程分数
        /// </summary>
        [Test]
        public void Test1()
        {
            var sql = @"SELECT s.SId, s.Sname, s.Ssex, sc1.score as '01_score', sc2.score as '02_score'
                        FROM Student s
                        INNER JOIN SC sc1 ON s.SId = sc1.SId AND sc1.CId = '01'
                        INNER JOIN SC sc2 ON s.SId = sc2.SId AND sc2.CId = '02'
                        WHERE sc1.score > sc2.score;";
            var list = _db.Ado.SqlQuery<dynamic>(sql);

            var result = _db.Queryable<Student>()
                .LeftJoin<Sc>((s, sc1) => s.SId == sc1.SId && sc1.CId == "01")
                .LeftJoin<Sc>((s, sc1, sc2) => s.SId == sc2.SId && sc2.CId == "02")
                .Where((s, sc1, sc2) => sc1.Score > sc2.Score)
                .Select((s, sc1, sc2) => new { sid = s.SId, sname = s.Sname, sc1 = sc1.Score, sc2 = sc2.Score })
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
        }

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
        }

        /// <summary>
        /// 查询不存在" 01 “课程但存在” 02 "课程的情况
        /// </summary>
        [Test]
        public void Test1_3()
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
                .Select((s, c) => new
                {
                    s.SId,
                    s.Sname
                })
                .ToList();
        }
    }
}