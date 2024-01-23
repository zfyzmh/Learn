using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sql100
{
    public class Database
    {
        /// <summary>
        /// SqlSugar 数据库实例
        /// </summary>
        public static readonly SqlSugarScope Instance = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "server=zifeiyu.fun;port=19960;database=test;uid=readonly;pwd=123456",//连接符字串
            DbType = DbType.MySql,//数据库类型
            IsAutoCloseConnection = true //不设成true要手动close
        },
             db =>
             {
                 //(A)全局生效配置点，一般AOP和程序启动的配置扔这里面 ，所有上下文生效
                 //调试SQL事件，可以删掉
                 db.Aop.OnLogExecuting = (sql, pars) =>
                 {
                     //Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响

                     //5.0.8.2 获取无参数化 SQL  对性能有影响，特别大的SQL参数多的，调试使用
                     Console.WriteLine("🚀   " + UtilMethods.GetSqlString(DbType.MySql, sql, pars, DisableNvarchar: true));
                 };
             });
    }
}