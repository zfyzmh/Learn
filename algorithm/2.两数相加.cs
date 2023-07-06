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
            ListNode list1 = new ListNode(0, new ListNode(2, new ListNode(3)));
            ListNode list2 = new ListNode(0, new ListNode(5, new ListNode(9)));

            var a = AddTwoNumbers2(list2, list1);
            System.Console.WriteLine(a.val + "->" + a.next.val + "->" + a.next.next.val);
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            //标准解法
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

        public static ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
        {
            //定义返回值
            var result = new ListNode(-1);
            //定义循环用的对象，将形参制定到temp
            var temp = result;
            //前一轮数字和的十位数
            int Other = 0;
            do
            {
                //取出numbe1和number2的值
                var number1 = l1 == null ? 0 : l1.val;
                var number2 = l2 == null ? 0 : l2.val;
                //计算两数之和 并加上前一轮需要进位的值
                var sum = number1 + number2 + Other;
                //计算个位
                var value = sum % 10;
                //计算十位并赋值
                Other = sum / 10;
                //将数据添加到循环链表中
                temp.next = new ListNode(value);
                //循环用的temp对象赋值为循环链表中的下一个对象
                temp = temp.next;
                //l1 l2 指向自己在链表中对应的下一个值
                l1 = l1?.next;
                l2 = l2?.next;
            } while (l1 != null || l2 != null || Other != 0);
            return result.next;
        }

        /// <summary>
        /// 返回链表中逆序存储的两数之和
        /// </summary>
        /// <param name="l1"> 非空链表，逆序表示数值 1 </param>
        /// <param name="l2"> 非空链表，逆序表示数值 2 </param>
        /// <returns> 非空链表，逆序表示数值之和 </returns>
        public ListNode AddTwoNumbers3(ListNode l1, ListNode l2)
        {
            var listNode = new ListNode(-1);
            var lnTemp = listNode;
            int temp = 0;

            while (l1 != null || l2 != null || temp / 10 != 0)
            {
                temp = (l1?.val ?? 0) + (l2?.val ?? 0) + temp / 10;
                lnTemp.next ??= new ListNode(-1);
                lnTemp.next.val = temp % 10;
                lnTemp = lnTemp.next;
                l1 = l1?.next; l2 = l2?.next;
            }
            return listNode.next;
        }
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