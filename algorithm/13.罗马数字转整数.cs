using NUnit.Framework;
using System;

namespace algorithm
{
    internal class _13
    {
        /* 罗马数字包含以下七种字符: I， V， X， L，C，D 和 M。

        字符 数值
        I             1
        V             5
        X             10
        L             50
        C             100
        D             500
        M             1000
        例如， 罗马数字 2 写做 II ，即为两个并列的 1 。12 写做 XII ，即为 X + II 。 27 写做 XXVII, 即为 XX + V + II 。

        通常情况下，罗马数字中小的数字在大的数字的右边。但也存在特例，例如 4 不写做 IIII，而是 IV。数字 1 在数字 5 的左边，所表示的数等于大数 5 减小数 1 得到的数值 4 。同样地，数字 9 表示为 IX。这个特殊的规则只适用于以下六种情况：

        I 可以放在 V(5) 和 X(10) 的左边，来表示 4 和 9。
        X 可以放在 L(50) 和 C(100) 的左边，来表示 40 和 90。 
        C 可以放在 D(500) 和 M(1000) 的左边，来表示 400 和 900。
        给定一个罗马数字，将其转换成整数。
        */

        [Test]
        public void Test1()
        {
            string a = "MCMXCIV";
            Console.WriteLine(RomanToInt(a));
        }

        /// <summary>
        /// 简单粗暴替换字符串法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int RomanToInt(string s)
        {
            s = s.Replace("IV", "Y");
            s = s.Replace("IX", "T");
            s = s.Replace("XL", "U");
            s = s.Replace("XC", "R");
            s = s.Replace("CD", "O");
            s = s.Replace("CM", "W");
            int sum = 0;
            int i = 0;
            for (i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case 'M':
                        sum += 1000;
                        break;

                    case 'W':
                        sum += 900;
                        break;

                    case 'D':
                        sum += 500;
                        break;

                    case 'O':
                        sum += 400;
                        break;

                    case 'C':
                        sum += 100;
                        break;

                    case 'R':
                        sum += 90;
                        break;

                    case 'L':
                        sum += 50;
                        break;

                    case 'U':
                        sum += 40;
                        break;

                    case 'X':
                        sum += 10;
                        break;

                    case 'T':
                        sum += 9;
                        break;

                    case 'V':
                        sum += 5;
                        break;

                    case 'Y':
                        sum += 4;
                        break;

                    case 'I':
                        sum += 1;
                        break;
                }
            }
            return sum;
        }

        public int RomanToInt2(string s)
        {
            int result = 0;
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case 'I':
                        result += 1;
                        if (i != s.Length - 1)
                            if (s[i + 1] == 'V' || s[i + 1] == 'X') result -= 2;
                        break;

                    case 'V':
                        result += 5;
                        break;

                    case 'X':
                        result += 10;
                        if (i != s.Length - 1)
                            if (s[i + 1] == 'L' || s[i + 1] == 'C') result -= 20;
                        break;

                    case 'L':
                        result += 50;
                        break;

                    case 'C':
                        result += 100;
                        if (i != s.Length - 1)
                            if (s[i + 1] == 'D' || s[i + 1] == 'M') result -= 200;
                        break;

                    case 'D':
                        result += 500;
                        break;

                    case 'M':
                        result += 1000;
                        break;
                }
            }
            return result;
        }

        private int getint(string num)
        {
            switch (num)
            {
                case "I": return 1;
                case "V": return 5;
                case "X": return 10;
                case "L": return 50;
                case "C": return 100;
                case "D": return 500;
                case "M": return 1000;
            }
            return 0;
        }
    }
}