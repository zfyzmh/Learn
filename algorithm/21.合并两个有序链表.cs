using NUnit.Framework;

namespace algorithm
{
    internal class _21
    {
        /*
         将两个升序链表合并为一个新的 升序 链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 

        示例 1：

        输入：l1 = [1,2,4], l2 = [1,3,4]
        输出：[1,1,2,3,4,4]
        示例 2：

        输入：l1 = [], l2 = []
        输出：[]
        示例 3：

        输入：l1 = [], l2 = [0]
        输出：[0]
 

        提示：

        两个链表的节点数目范围是 [0, 50]
        -100 <= Node.val <= 100
        l1 和 l2 均按 非递减顺序 排列

        来源：力扣（LeetCode）
        链接：https://leetcode.cn/problems/merge-two-sorted-lists
        著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
         */

        [Test]
        public void Test1()
        {
            var l1 = new ListNode(1, new ListNode(2, new ListNode(4)));
            var l2 = new ListNode(1, new ListNode(3, new ListNode(4)));
            var newlist = MergeTwoLists2(l1, l2);
            forlistnode(newlist);
        }

        /// <summary>
        /// 遍历解法
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode MergeTwoLists2(ListNode l1, ListNode l2)
        {
            //创建一个新的结果链表
            var head = new ListNode(0);
            //遍历过程中新链表的最后一个节点
            var currentNode = head;
            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    currentNode.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    currentNode.next = l2;
                    l2 = l2.next;
                }
                //更新结果链表的最后一个节点
                currentNode = currentNode.next;
            }

            //遍历完成后，肯定有一个不为空的链表，将其直接连接到结果链表的最后
            if (l1 == null)
            {
                currentNode.next = l2;
            }
            else
            {
                currentNode.next = l1;
            }

            return head.next;
        }

        /// <summary>
        /// 递归解法
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null)
            {
                return l2;
            }
            else if (l2 == null)
            {
                return l1;
            }
            else if (l1.val < l2.val)
            {
                l1.next = MergeTwoLists(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = MergeTwoLists(l1, l2.next);
                return l2;
            }
        }

        //This code prints out the values of a ListNode.
        private void forlistnode(ListNode list1)
        {
            //Print out the value of the first node
            System.Console.WriteLine(list1.val);

            //Loop through the list until the next node is null
            while (list1.next != null)
            {
                //Set the current node to the next node
                list1 = list1.next;

                //Print out the value of the current node
                System.Console.WriteLine(list1.val);
            }
        }
    }
}