using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    /*    编写一个函数来查找字符串数组中的最长公共前缀。

    如果不存在公共前缀，返回空字符串 ""。

 

    示例 1：

    输入：strs = ["flower","flow","flight"]
    输出："fl"
    示例 2：

    输入：strs = ["dog","racecar","car"]
    输出：""
    解释：输入不存在公共前缀。
    提示：

    1 <= strs.length <= 200
    0 <= strs[i].length <= 200
    strs[i] 仅由小写英文字母组成
    */

    internal class _14
    {
        [Test]
        public void Test1()
        {
            string[] strs = new string[] { "flower", "flow" };
            Console.WriteLine(LongestCommonPrefix(strs));
        }

        public string LongestCommonPrefix(string[] strs)
        {
            string m = "";//要返回的总前缀
            for (int i = 0; i < strs.Length; i++)
            {
                for (int j = 0; j < strs[i].Length; j++)
                {
                    Console.WriteLine(i + "------" + j);
                    Console.WriteLine(strs[i][j]);
                }
            }
            return m;
        }
    }
}