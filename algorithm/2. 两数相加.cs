using NUnit.Framework;

namespace algorithm
{
    internal class _2
    {
        //给你两个 非空 的链表，表示两个非负的整数。
        //它们每位数字都是按照 逆序 的方式存储的，并且每个节点只能存储 一位 数字。
        //请你将两个数相加，并以相同形式返回一个表示和的链表。
        //你可以假设除了数字 0 之外，这两个数都不会以 0 开头。
        [Test]
        public void Test1()
        {
            //两个链表
            ListNode list1 = new ListNode(1, new ListNode(2, new ListNode(3)));
            ListNode list2 = new ListNode(4, new ListNode(5, new ListNode(6)));

            var a = AddTwoNumbers(list2,list1);
            System.Console.WriteLine(a.val + "->" + a.next.val + "->" + a.next.next.val);
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int x = 0, y = 0, z = 0;
            int pull = 0; 
            ListNode nodeAll = new ListNode();//这是火车
            ListNode nextNode = nodeAll;//这是锚点
            while (l1 != null || l2 != null)
            {
                x = l1 != null ? l1.val : 0;
                y = l2 != null ? l2.val : 0;
                z = x + y + pull;//将本次总和计入 并计入上次总和
                pull = 0;//上次总和置为0
                if (z >= 10)
                {//这一块处理当前节点的总和
                    pull += 1; z -= 10;
                }
                l1 = l1 != null ? l1.next : null;//进入下一节点
                l2 = l2 != null ? l2.next : null;//进入下一节点
                ListNode newnode = new ListNode(z);
                if (pull > 0) newnode.next = new ListNode(pull);
                nextNode.next = newnode;//我组装一个车厢之后
                nextNode = nextNode.next;//切换锚点到这个车厢
            }
            return nodeAll.next;
        }
    }

    /// <summary>
    /// 单向链表 Definition for singly-linked list.
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}