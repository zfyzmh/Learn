using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace algorithm
{
    internal class _20
    {
        /*
         * 给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串 s ，判断字符串是否有效。

            有效字符串需满足：

            左括号必须用相同类型的右括号闭合。
            左括号必须以正确的顺序闭合。
            每个右括号都有一个对应的相同类型的左括号。
 

            示例 1：

            输入：s = "()"
            输出：true
            示例 2：

            输入：s = "()[]{}"
            输出：true
            示例 3：

            输入：s = "(]"
            输出：false
 

            提示：

            1 <= s.length <= 104
            s 仅由括号 '()[]{}' 组成

            来源：力扣（LeetCode）
            链接：https://leetcode.cn/problems/valid-parentheses
            著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
         */

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string s = "({[{}]})";
            System.Console.WriteLine(IsValid(s));
        }

        /// <summary>
        /// 我的解法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsValid(string s)
        {
            if (s.Count(x => x == '(') != s.Count(x => x == ')')) return false;
            if (s.Count(x => x == '{') != s.Count(x => x == '}')) return false;
            if (s.Count(x => x == '[') != s.Count(x => x == ']')) return false;

            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                switch (c)
                {
                    case '(':
                        stack.Push(c);

                        break;

                    case '{':
                        stack.Push(c);
                        break;

                    case '[':
                        stack.Push(c);
                        break;

                    case ')':
                        bool aa = stack.TryPeek(out char a);

                        if (aa)
                        {
                            if (a == '(') stack.Pop(); else return false;
                        }
                        else
                        {
                            return false;
                        }

                        break;

                    case ']':
                        bool bb = stack.TryPeek(out char b);

                        if (bb)
                        {
                            if (b == '[') stack.Pop(); else return false;
                        }
                        else
                        {
                            return false;
                        }
                        break;

                    case '}':
                        bool ccc = stack.TryPeek(out char cc);

                        if (ccc)
                        {
                            if (cc == '{') stack.Pop(); else return false;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                }
            }

            return true;
        }
    }
}