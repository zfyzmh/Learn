using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    public class 协变和逆变
    {
        [Test]
        public void 协变()
        {
            ICollection<int> list = new List<int>();
            IFoo<string> foo = new Foo();

            Console.WriteLine(foo.GetName());

            子类 zi = new 子类();

            父类 fu = new 父类();
        }

        [Test]
        public void 逆变()
        {
        }
    }

    public class Foo : IFoo<string>
    {
        public string GetName()
        {
            return GetType().Name;
        }
    }

    internal interface IFoo<T>
    {
        T GetName();
    }

    public class 父类
    {
    }

    public class 子类 : 父类
    {
    }
}