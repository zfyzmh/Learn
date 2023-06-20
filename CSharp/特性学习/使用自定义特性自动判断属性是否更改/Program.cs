using NUnit.Framework;
using System;

namespace algorithm.使用自定义特性自动判断属性是否更改
{
    internal class Program
    {
        [Test]
        public void Test1()
        {
            Entity entity = new Entity() { Id = Guid.NewGuid(), OId = DateTime.Now.ToString(), A = "a1", B = 0.01, C = true };

            Entity newentity = new Entity() { A = "a2", B = 1.01 };

            var logs = entity.GetPropertyLogs(newentity);
            foreach (var log in logs)
            {
                Console.WriteLine(log.ToString());
            }
        }
    }
}