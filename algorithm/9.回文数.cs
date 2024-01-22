using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    internal class _9
    {
        //给你一个整数 x ，如果 x 是一个回文整数，返回 true ；否则，返回 false 。
        //   回文数是指正序（从左向右）和倒序（从右向左）读都是一样的整数。复数永远不是回文数
        [Test]
        public void Test1()
        {
            Console.WriteLine(huiWenShu2(121));
            Console.WriteLine(huiWenShu2(123));
        }

        public bool huiWenShu(int x)
        {
            if (x < 0 || (x % 10 == 0 && x != 0)) return false;
            var aa = x.ToString().ToCharArray();
            Array.Reverse(aa);
            if (new string(aa) == x.ToString()) return true;
            return false;
        }

        public bool huiWenShu2(int x)
        {
            var str = x.ToString();
            for (int i = 0, j = str.Length - 1; i < j; i++, j--)
            {
                if (str[i] != str[j])
                {
                    return false;
                }
            }
            return true;
        }
    }
}