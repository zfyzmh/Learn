using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    public class 协变和逆变
    {
        /// <summary>
        /// 协变-in-入参,逆变-out-出参.遵循里氏替换原则,即可用父类替换子类.在使用协变或者逆变的类型的时候,是只能使用父类的功能的
        /// public delegate TResult Func<in T, out TResult>(T arg);
        /// </summary>
        [Test]
        public void 协变逆变()
        {
            Func<string, object> list = new Func<object, string>((m) =>
            {
                Console.WriteLine(m);
                Console.WriteLine(m.GetType());

                return "I got it";
            });

            object a = list.Invoke("I got it");
        }
    }
}