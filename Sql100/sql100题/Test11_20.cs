using Student = WebFirst.Entities.Student;
using Sc = WebFirst.Entities.Sc;
using Course = WebFirst.Entities.Course;
using Teacher = WebFirst.Entities.Teacher;
using SqlSugar;
using Microsoft.EntityFrameworkCore;

namespace Sql100
{
    public class Test11_20
    {
        private static SqlSugarScope _db = Database.Instance;
        private LearningContext _context = new LearningContext();

        /// <summary>
        /// 查询两门及其以上不及格课程的同学的学号，姓名及其平均成绩
        /// </summary>
        [Test]
        public void Test11()
        {
            var sql = @"SELECT
	                    s.SId AS sid,
	                    s.Sname AS sname,
	                    AVG( sc.Score ) AS avg_score
                    FROM
	                    Student s
	                    INNER JOIN SC sc ON s.SId = sc.SId
                    WHERE
	                    sc.Score < 60 GROUP BY s.SId, s.Sname HAVING COUNT(*) >= 2;";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Student>()
                .InnerJoin<Sc>((s, sc) => s.SId == sc.SId)
                .Where((s, sc) => sc.Score < 60)
                .GroupBy(s => new { s.SId, s.Sname })
                .Having(s => SqlFunc.AggregateCount("*") >= 2)
                .Select((s, sc) => new { s.SId, s.Sname, avg = SqlFunc.AggregateAvg(sc.Score) })
                .ToList();

            var result2 = _context.Students
                            .Where(m => m.Scs.Count(m => m.Score < 60) >= 2)
                            .Select(g => new
                            {
                                sid = g.Id,
                                sname = g.Name,
                                avg_score = g.Scs.Average(sc => sc.Score)
                            })
                            .ToList();
        }

        /// <summary>
        /// 检索" 01 "课程分数小于 60，按分数降序排列的学生信息
        /// </summary>
        [Test]
        public void Test12()
        {
            var sql = @"SELECT
	                        s.SId AS sid,
	                        s.Sname AS sname,
	                        sc.score AS score
                        FROM
	                        Student s
	                        INNER JOIN SC sc ON s.SId = sc.SId
                        WHERE
	                        sc.CId = 1
	                        AND sc.score < 60
                        ORDER BY
	                        sc.score DESC;";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Student>()
                .InnerJoin<Sc>((s, sc) => s.SId == sc.SId && sc.CId == "01")
                .Where((s, sc) => sc.Score < 60)
                .OrderByDescending((s, sc) => new { sc.Score })
                .Select((s, sc) => new { s.SId, s.Sname, sc.Score })
                .ToList();

            var result2 = _context.Students
                            .Where(m => m.Scs.Any(m => m.CourseId == 1 && m.Score < 60))
                            .OrderByDescending(m => m.Scs.Single(m => m.CourseId == 1).Score)
                            .Select(g => new
                            {
                                sid = g.Id,
                                sname = g.Name,
                                score1 = g.Scs.Single(m => m.CourseId == 1).Score
                            })
                            .ToList();
        }

        /// <summary>
        /// 按平均成绩从高到低显示所有学生的所有课程的成绩以及平均成绩
        /// </summary>
        [Test]
        public void Test13()
        {
            var sql = @"SELECT
	                    s.Sname,
	                    c.Cname,
	                    sc.SId,
	                    AVG( sc2.score ) AS AverageScore,
	                    sc.score
                    FROM
	                    SC sc
	                    LEFT JOIN Course c ON c.CId = sc.CId
	                    LEFT JOIN Student s ON s.SId = sc.SId
	                    INNER JOIN SC sc2 ON sc2.SId = s.SId
                    GROUP BY
	                    sc.SId,
	                    c.Cname,
	                    s.Sname,
	                    sc.score
                    ORDER BY
	                    AverageScore DESC";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Sc>()
                .LeftJoin<Student>((sc, st) => sc.SId == st.SId)
                .LeftJoin<Course>((sc, st, co) => sc.CId == co.CId)
                .InnerJoin<Sc>((sc, st, co, sc1) => sc1.SId == st.SId)
                .GroupBy((sc, st, co, sc1) => new { sc.SId, co.Cname, st.Sname, sc.Score })
                .OrderByDescending((sc, st, co, sc1) => SqlFunc.AggregateAvg(sc1.Score))
                .Select((sc, st, co, sc1) => new
                {
                    sc.SId,
                    co.Cname,
                    st.Sname,
                    sc.Score,
                    avg = SqlFunc.AggregateAvg(sc1.Score)
                })
                .ToList();

            var result2 = _context.Scs
                            .Select(g => new
                            {
                                g.StudentId,
                                g.Course.Cname,
                                g.Student.Name,
                                AverageScore = _context.Scs.Where(m => m.StudentId == g.StudentId).Average(m => m.Score),
                                g.Score
                            })
                            .OrderByDescending(x => x.AverageScore)
                            .ToList();
        }

        /// <summary>
        /// 查询各科成绩最高分、最低分和平均分及格率，中等率，优良率，优秀率及格为>=60，中等为：70-80，优良为：80-90，优秀为：>=90
        /// </summary>
        [Test]
        public void Test14()
        {
            var sql = @"SELECT
	                sc.CId,
	                co.Cname,
	                AVG( sc.score ) AS avg_score,
	                MIN( sc.score ) AS min_score,
	                MAX( sc.score ) AS max_score,
	                SUM( CASE WHEN sc.score >= 60 THEN 1 ELSE 0 END )/ COUNT(*) AS PassedRate,
	                SUM( CASE WHEN sc.score >= 70 AND sc.score < 80 THEN 1 ELSE 0 END ) / COUNT(*) AS MediumRate,
	                SUM( CASE WHEN sc.score >= 80 AND sc.score < 90 THEN 1 ELSE 0 END )/ COUNT(*) AS GoodRate,
	                SUM( CASE WHEN sc.score >= 90 THEN 1 ELSE 0 END ) / COUNT(*) AS ExcellentRate
                FROM
	                SC sc
	                LEFT JOIN Course co ON sc.CId = co.CId
                GROUP BY
	                sc.CId,
	                co.Cname";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Sc>()
                    .LeftJoin<Course>((sc, co) => sc.CId == co.CId)
                    .GroupBy((sc, co) => new { sc.CId, co.Cname })
                    .Select((sc, co) => new
                    {
                        sc.CId,
                        co.Cname,
                        MaxScore = SqlFunc.AggregateMax(sc.Score),
                        MinScore = SqlFunc.AggregateMin(sc.Score),
                        AvgScore = SqlFunc.AggregateAvg(sc.Score),
                        PassedRate = (double)SqlFunc.AggregateSum(SqlFunc.IIF(sc.Score >= 60, 1, 0)) / SqlFunc.AggregateCount("*"),
                        MediumRate = (double)SqlFunc.AggregateSum(SqlFunc.IIF(sc.Score >= 70 && sc.Score < 80, 1, 0)) / SqlFunc.AggregateCount("*"),
                        GoodRate = (double)SqlFunc.AggregateSum(SqlFunc.IIF(sc.Score >= 80 && sc.Score < 90, 1, 0)) / SqlFunc.AggregateCount("*"),
                        ExcellentRate = (double)SqlFunc.AggregateSum(SqlFunc.IIF(sc.Score >= 90, 1, 0)) / SqlFunc.AggregateCount("*"),
                    })
                    .ToList();

            var result2 = _context.Scs
                            .GroupBy(m => m.CourseId)
                            .Select(m => new
                            {
                                cid = m.Key,
                                cname = m.First().Course.Cname,
                                MaxScore = m.Max(sc => sc.Score),
                                MinScore = m.Min(sc => sc.Score),
                                AvgScore = m.Average(sc => sc.Score),
                                PassedRate = (double)m.Count(m => m.Score >= 60) / m.Count(),
                                MediumRate = (double)m.Count(m => m.Score >= 70 && m.Score < 80) / m.Count(),
                                GoodRate = (double)m.Count(m => m.Score >= 80 && m.Score < 90) / m.Count(),
                                ExcellentRate = (double)m.Count(m => m.Score >= 90) / m.Count(),
                                count = m.Count()
                            })
                            .ToList();
        }

        /// <summary>
        /// 查询各课程选修人数，查询结果按人数降序排列，若人数相同，按课程号升序排列
        /// </summary>
        [Test]
        public void Test15()
        {
            var sql = @"SELECT
	                    c.CId AS CourseId,
	                    c.Cname AS CourseName,
	                    COUNT( sc.SId ) AS StudentCount
                    FROM
	                    Course c
	                    LEFT JOIN SC sc ON c.CId = sc.CId
                    GROUP BY
	                    c.CId,
	                    c.Cname
                    ORDER BY
	                    COUNT( sc.SId ) DESC,
	                    c.CId ASC;";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Sc>()
                    .LeftJoin<Course>((sc, co) => sc.CId == co.CId)
                    .GroupBy((sc, co) => new { sc.CId, co.Cname })
                    .OrderBy((sc, co) => SqlFunc.AggregateCount("*"), OrderByType.Desc)
                    .OrderBy((sc, co) => co.CId, OrderByType.Asc)
                    .Select((sc, co) => new
                    {
                        sc.CId,
                        co.Cname,
                    })
                    .ToList();

            var result2 = _context.Scs
                            .GroupBy(m => m.CourseId)
                            .Select(m => new
                            {
                                cid = m.Key,
                                cname = m.First().Course.Cname,
                                count = m.Count()
                            })
                            .OrderByDescending(m => m.count)
                            .ThenBy(m => m.cid)
                            .ToList();
        }

        /// <summary>
        /// 按各科成绩进行排序，并显示排名,如需不保留排名只需要将DISTINCT语句去除即可
        /// </summary>
        [Test]
        public void Test16()
        {
            var sql = @"SELECT
	                    cou.Cname,
	                    a.SId,
	                    a.CId,
	                    a.score,
	                    (
	                    SELECT
		                    COUNT( DISTINCT b.score ) + 1
	                    FROM
		                    SC b
	                    WHERE
		                    b.CId = a.CId
		                    AND b.score > a.score
	                    ) AS r
                    FROM
	                    SC a
	                    LEFT JOIN Course cou ON cou.CId = a.CId
                    ORDER BY
	                    a.CId ASC,
	                    a.score DESC;
                    ";
            var result = _db.Ado.SqlQuery<dynamic>(sql);

            var result1 = _db.Queryable<Sc>()
                    .LeftJoin<Course>((sc, cou) => sc.CId == cou.CId)
                    .Select((sc, cou) => new
                    {
                        cou.Cname,
                        sc.SId,
                        sc.CId,
                        sc.Score,
                        Rank = SqlFunc.Subqueryable<Sc>().Where(b => b.CId == sc.CId && b.Score > sc.Score).Select(b => SqlFunc.AggregateCount(b.Score)) + 1
                    })
                    .OrderBy(sc => sc.CId, OrderByType.Asc)
                    .OrderBy(sc => sc.Score, OrderByType.Desc)
                    .ToList();

            var result2 = _context.Scs
                .Select(sc => new
                {
                    sc.Course.Cname,
                    sc.CourseId,
                    sc.Student.Name,
                    sc.StudentId,
                    sc.Score,
                    Rank = _context.Scs.Where(b => b.CourseId == sc.CourseId && b.Score > sc.Score)
                     .Select(b => b.Score)
                     .Distinct()
                     .Count() + 1
                })
                .OrderBy(m => m.CourseId)
                .ThenByDescending(m => m.Score)
                .ToList();
        }
    }
}