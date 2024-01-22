using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    internal class 冒泡排序
    {
        [Test]
        public void Test1()
        {
            int[] vs = new Int32[] { 24, 65, 2, 35, 87, 1, 6 };
            foreach (var item in BubblingSort(vs))
            {
                Console.WriteLine(item);
            }

            foreach (var item in BubblingSortTwo(vs))
            {
                Console.WriteLine(item);
            }
        }

        public int[] BubblingSort(int[] vs)
        {
            for (int i = 0; i < vs.Length - 1; i++)
            {
                for (int j = 0; j < vs.Length - 1 - i; j++)
                {
                    if (vs[j] > vs[j + 1])
                    {
                        var a = vs[j + 1];
                        vs[j + 1] = vs[j];
                        vs[j] = a;
                    }
                }
            }

            return vs;
        }

        public int[] BubblingSortTwo(int[] vs)
        {
            for (int i = 0; i < vs.Length - 1; i++)
            {
                for (int j = 0; j < vs.Length - 1 - i; j++)
                {
                    if (vs[j] < vs[j + 1])
                    {
                        var a = vs[j + 1];
                        vs[j + 1] = vs[j];
                        vs[j] = a;
                    }
                }
            }

            return vs;
        }
    }
}