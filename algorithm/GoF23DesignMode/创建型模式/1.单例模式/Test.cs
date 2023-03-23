using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode._1.单例模式
{
    public class Test
    {
        [Test]
        public void Main()
        {
            var AA = Singleton<MyClass>.Instance;
            AA.MyProperty = 2;
            Console.WriteLine(Singleton<MyClass>.Instance.MyProperty);
        }
    }

    public class MyClass
    {
        public int MyProperty { get; set; }
    }
}