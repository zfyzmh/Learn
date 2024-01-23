using NUnit.Framework;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm
{
    /*
    给你一个下标从 0 开始的整数数组 nums 。如果 nums 中长度为 m 的子数组 s 满足以下条件，我们称它是一个 交替子数组 ：
    m 大于 1 。
    s1 = s0 + 1 。
    下标从 0 开始的子数组 s 与数组 [s0, s1, s0, s1,...,s(m-1) % 2] 一样。
    也就是说，s1 - s0 = 1 ，s2 - s1 = -1 ，s3 - s2 = 1 ，s4 - s3 = -1 ，以此类推，直到 s[m - 1] - s[m - 2] = (-1)m 。
    请你返回 nums 中所有 交替 子数组中，最长的长度，如果不存在交替子数组，请你返回 -1 。

    子数组是一个数组中一段连续 非空 的元素序列。

    示例 1：

    输入：nums = [2,3,4,3,4]
    输出：4
    解释：交替子数组有 [3,4] ，[3,4,3] 和 [3,4,3,4] 。最长的子数组为 [3,4,3,4] ，长度为4 。
    示例 2：

    输入：nums = [4,5,6]
    输出：2
    解释：[4,5] 和 [5,6] 是仅有的两个交替子数组。它们长度都为 2 。

    提示：

    2 <= nums.length <= 100
    1 <= nums[i] <= 104

 */

    internal class _2765
    {
        [Test]
        public void Test1()
        {
            Console.WriteLine(AlternatingSubarray([4, 5, 6]));
            Console.WriteLine(AlternatingSubarrayTwo([2, 3, 4, 3, 4]));
        }

        public int AlternatingSubarray(int[] nums)
        {
            int max = -1;

            for (int i = 0; i < nums.Length; i++)
            {
                int Count = 0;

                int AlternatingVariable = -1;

                for (int j = i; j < nums.Length - 1; j++)
                {
                    if (nums[j] - nums[j + 1] == AlternatingVariable)
                    {
                        AlternatingVariable = AlternatingVariable == 1 ? -1 : 1;
                        Count++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (Count > 0 && Count > max)
                {
                    max = Count;
                }
            }

            return max > 0 ? max + 1 : max;
        }

        public int AlternatingSubarrayTwo(int[] nums)
        {
            int maxLength = 1;
            int prevDiff = -2;
            int currLength = 1;
            int length = nums.Length;

            for (int i = 1; i < length; i++)
            {
                int currDiff = nums[i] - nums[i - 1];
                if (prevDiff * currDiff == -1)
                {
                    currLength++;
                }
                else if (currDiff == 1)
                {
                    currLength = 2;
                }
                else
                {
                    currLength = 1;
                }
                maxLength = Math.Max(maxLength, currLength);
                prevDiff = currDiff;
            }
            return maxLength > 1 ? maxLength : -1;
        }
    }
}