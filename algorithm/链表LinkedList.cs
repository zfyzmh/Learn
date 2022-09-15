using NUnit.Framework;
using System;
using System.Text;

namespace algorithm
{
    internal class 链表LinkedList
    {
        [Test]
        public void Test1()
        {
            //！！测试各个方法
            LinkedList1<int> l = new LinkedList1<int>();//在输出打印的时候必须要有输出打印的方法
            for (int i = 0; i < 5; i++)
            {
                l.AddFirst(i);
                Console.WriteLine(l);
            }
            l.AddLast(666);
            Console.WriteLine(l);

            l.Add(2, 999);
            Console.WriteLine(l);

            l.Set(2, 1000);
            Console.WriteLine(l);

            //4->3->1000->2->1->0->666->Null
            //！！测试删除方法
            l.Remove(2);//删除元素为2的结点
            Console.WriteLine(l);
            l.RemoveAt(2);//删除index=2的结点
            Console.WriteLine(l);

        }
    }

    internal class LinkedList1<E>
    {
        private class Node//只有链表能够使用
        {
            public E e;//实际存储元素的类型
            public Node next;//记录当前节点的下一个节点是谁

            public Node(E e, Node next)//使用构造函数对类进行初始化
            {
                this.e = e;
                this.next = next;
            }

            public Node(E e)//若不知道下一个节点是什么
            {
                this.e = e;
                this.next = null;
            }

            public override string ToString()//打印输出
            {
                return e.ToString();
            }
        }

        private Node head;//链表的头部
        private int N;//链表中存储元素的个数

        public LinkedList1()
        {
            head = null;
            N = 0;
        }

        public int Count
        {
            get { return N; }
        }

        public bool IsEmpty
        {
            get { return N == 0; }
        }

        //！！往链表中添加结点
        public void Add(int index, E e)
        {
            if (index < 0 || index > N)
            {
                throw new ArgumentException("非法索引");
            }
            if (index == 0)//在链表头部插入结点
            {
                Node node = new Node(e);
                node.next = head;
                head = node;

                //head = new Node(e, head);//等同于前面三句代码
            }
            else
            {
                Node pre = head;//在链表中间或者尾部插入结点
                for (int i = 0; i < index - 1; i++)
                {
                    pre = pre.next;//将指针移到要加入结点的前一个索引
                }
                Node node = new Node(e);
                node.next = pre.next;
                pre.next = node;

                //pre.next = new Node(e, pre.next);//等同于前面三条语句
            }
            N++;
        }

        public void AddFirst(E e)
        {
            Add(0, e);
        }

        public void AddLast(E e)
        {
            Add(N, e);
        }

        //！！查找链表中的结点
        public E Get(int index)
        {
            if (index < 0 || index > N)
            {
                throw new ArgumentException("非法索引");
            }
            Node cur = head;
            for (int i = 0; i < index; i++)
            {
                cur = cur.next;
            }
            return cur.e;
        }

        public E GetFirst()
        {
            return Get(0);
        }

        public E GetLast()
        {
            return Get(N - 1);
        }

        //！！修改方法
        public void Set(int index, E newE)
        {
            if (index < 0 || index > N)
            {
                throw new ArgumentException("非法索引");
            }
            Node cur = head;
            for (int i = 0; i < index; i++)
            {
                cur = cur.next;
            }
            cur.e = newE;
        }

        //！！查看是否包含某个元素
        public bool Contains(E e)
        {
            Node cur = head;
            while (cur != null)
            {
                if (cur.e.Equals(e))
                {
                    return true;
                }
                cur = cur.next;
            }
            return false;
        }

        //！！删除链表中的结点方法
        public E RemoveAt(int index)
        {
            if (index < 0 || index > N)
            {
                throw new ArgumentException("非法索引");
            }
            if (index == 0)//删除链表的头部结点
            {
                Node delNode = head;
                head = head.next;//将头部的指针指向头部的下一个结点
                N--;
                return delNode.e;
            }
            else//删除链表中间或尾部的函数
            {
                Node pre = head;
                for (int i = 0; i < index - 1; i++)
                {
                    pre = pre.next;
                }
                Node delNode = pre.next;
                pre.next = delNode.next;
                N--;
                return delNode.e;
            }
        }

        public E RemoveFirst()
        {
            return RemoveAt(0);
        }

        public E RemoveLast()
        {
            return RemoveAt(N - 1);
        }

        //！！通过指定的元素删除一个结点
        public void Remove(E e)
        {
            if (head == null)
                return;
            if (head.e.Equals(e))
            {
                head = head.next;
                N--;
            }
            else
            {
                Node cur = head;
                Node pre = null;
                while (cur != null)
                {
                    pre = cur;
                    cur = cur.next;
                    if (cur.e.Equals(e))
                        break;
                }
                if (cur != null)
                {
                    pre.next = cur.next;
                    N--;
                }
            }
        }

        //！！为链表实现一个打印输出的方法
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            Node cur = head;
            while (cur != null)
            {
                res.Append(cur + "->");
                cur = cur.next;
            }
            res.Append("Null");
            return res.ToString();
        }
    }
}