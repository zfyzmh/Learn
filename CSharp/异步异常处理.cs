using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace algorithm
{
    public class 异步异常处理
    {


        [Test]
        public async Task Test1()
        {
            Task tasks = null;
            try
            {
                var task1 = Task.Run(() => throw new IndexOutOfRangeException("IndexOutOfRangeException is thrown."));
                var task2 = Task.Run(() => throw new ArithmeticException("ArithmeticException is thrown."));
                tasks = Task.WhenAll(task1, task2);
                await tasks;
            }

            catch (Exception ex)
            {
                AggregateException exception = tasks.Exception;
                foreach (Exception ex2 in exception.InnerExceptions)
                {
                    Console.WriteLine(ex2.Message);
                }
                Console.WriteLine(exception.Message);
            }
        }
    }
}