using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    /*
     给定一个非负整数，你至多可以交换一次数字中的任意两位。返回你能得到的最大值。

    示例 1 :

    输入: 2736
    输出: 7236
    解释: 交换数字2和数字7。
    示例 2 :

    输入: 9973
    输出: 9973
    解释: 不需要交换。
    注意:

    给定数字的范围是 [0, 108]

     */

    internal class _670
    {
        [Test]
        public void Test1()
        {
            Console.WriteLine(MaximumSwap(1993));
        }

        /// <summary>
        /// 暴力双重循环
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int MaximumSwap(int num)
        {
            char[] nums = num.ToString().ToCharArray();

            int max = num;

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 1 + i; j < nums.Length; j++)
                {
                    char[] clones = new char[nums.Length];
                    Array.Copy(nums, clones, nums.Length);

                    var a = clones[i];
                    clones[i] = clones[j];
                    clones[j] = a;

                    if (int.Parse(clones) > max)
                    {
                        max = int.Parse(clones);
                    }
                }
            }

            return max;
        }
    }
}