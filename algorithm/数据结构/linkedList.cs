using System;

namespace algorithm.数据结构
{
    public class SingleLinkedListNode<T>
    {
        public T Value { get; set; }
        public SingleLinkedListNode<T> Next { get; set; }

        public SingleLinkedListNode()
        {
            Value = default; Next = null;
        }
    }

    internal class SingleLinkedList<T> : ISingleLinkedListFunction<T>
    {
        private SingleLinkedListNode<T> first = null;

        /// <summary>
        /// 头结点
        /// </summary>
        public SingleLinkedListNode<T> First
        {
            get
            {
                return first;
            }
        }

        private SingleLinkedListNode<T> last = null;

        /// <summary>
        /// 尾节点
        /// </summary>
        public SingleLinkedListNode<T> Last
        {
            get
            {
                return last;
            }
        }

        private int count;

        /// <summary>
        /// 链表节点数量
        /// </summary>
        public int Count
        {
            get
            {
                count = GetCount();
                return count;
            }
        }

        private bool isEmpty;

        /// <summary>
        /// 链表是否为空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                isEmpty = GetIsEmpty();
                return isEmpty;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                {
                    return default;
                }
                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index; i++)
                {
                    tempNode = tempNode.Next;
                }
                return tempNode.Value;
            }
        }

        /// <summary>
        /// 添加数据到链表首节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>Node节点</returns>
        public SingleLinkedListNode<T> AddFirst(T value)
        {
            SingleLinkedListNode<T> tempNode = new SingleLinkedListNode<T>();
            tempNode.Value = value;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                tempNode.Next = first;
                first = tempNode;
            }
            return tempNode;
        }

        /// <summary>
        /// 添加节点到首节点(此时储存的并不是传递过来的数据地址,而是新建节点,赋值传过来节点的数据)
        /// </summary>
        /// <param name="node">数据节点</param>
        /// <returns>真正添加的节点</returns>
        public SingleLinkedListNode<T> AddFirst(SingleLinkedListNode<T> node)
        {
            SingleLinkedListNode<T> tempNode = CopyToFrom(node);
            tempNode.Next = null;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                tempNode.Next = first;
                first = tempNode;
            }
            return tempNode;
        }

        /// <summary>
        /// 添加数据到尾节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>Node节点</returns>
        public SingleLinkedListNode<T> AddLast(T value)
        {
            SingleLinkedListNode<T> tempNode = new SingleLinkedListNode<T>();
            tempNode.Value = value;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                last.Next = tempNode;
                last = tempNode;
            }
            return tempNode;
        }

        /// <summary>
        /// 添加尾节点(此时储存的并不是传递过来的数据地址,而是新建节点,赋值传过来节点的数据)
        /// </summary>
        /// <param name="node">数据节点</param>
        /// <returns>返回真正添加的节点</returns>
        public SingleLinkedListNode<T> AddLast(SingleLinkedListNode<T> node)
        {
            SingleLinkedListNode<T> tempNode = CopyToFrom(node);
            tempNode.Next = null;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                last.Next = tempNode;
                last = tempNode;
            }
            return tempNode;
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear()
        {
            first = null;
            last = null;
        }

        /// <summary>
        /// 判断链表中是否有该数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            if (first == null)
            {
                return false;
            }
            SingleLinkedListNode<T> tempNode = first;
            if (tempNode.Value.Equals(value))
            {
                return true;
            }
            while (true)
            {
                if (tempNode.Next != null)
                {
                    if (tempNode.Value.Equals(value))
                    {
                        return true;
                    }
                    tempNode = tempNode.Next;
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        /// <summary>
        /// 删除第一个数据T的节点
        /// </summary>
        /// <param name="value"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(T value)
        {
            if (Contains(value))
            {
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Value.Equals(value))
                {
                    first = first.Next;
                    return true;
                }
                SingleLinkedListNode<T> tempPrevious = null;
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        tempPrevious = tempNode;
                        tempNode = tempNode.Next;
                        if (tempNode.Value.Equals(value))
                        {
                            tempPrevious.Next = tempNode.Next;
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除下标节点
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteAt(int index)
        {
            if (index >= Count)
            {
                return false;
            }
            else
            {
                if (index == 0)
                {
                    first = first.Next;
                    return true;
                }
                else
                {
                    SingleLinkedListNode<T> tempNode = first;
                    //拿到需要删除节点的上一个节点
                    for (int i = 0; i < index - 1; i++)
                    {
                        tempNode = tempNode.Next;
                    }
                    tempNode.Next = tempNode.Next.Next;
                    return true;
                }
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node">删除的节点</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(SingleLinkedListNode<T> node)
        {
            if (first == null)
            {
                return false;
            }
            else
            {
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Equals(node))
                {
                    first = first.Next;
                    return true;
                }
                SingleLinkedListNode<T> tempPrevious = null;
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        tempPrevious = tempNode;
                        tempNode = tempNode.Next;
                        if (tempNode.Equals(node))
                        {
                            tempPrevious.Next = tempNode.Next;
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 删除头节点
        /// </summary>
        /// <returns></returns>
        public bool DeleteFirst()
        {
            if (first == null)
            {
                return false;
            }
            else
            {
                first = first.Next;
                return true;
            }
        }

        /// <summary>
        /// 删除尾节点
        /// </summary>
        /// <returns></returns>
        public bool DeleteLast()
        {
            if (first == null)
            {
                return false;
            }
            else
            {
                if (first == last)
                {
                    last = null;
                    first = null;
                }
                else
                {
                    //获取last的上一个节点
                    SingleLinkedListNode<T> tempNode = first;
                    SingleLinkedListNode<T> tempPrevious = null;
                    while (true)
                    {
                        if (tempNode.Next != null)
                        {
                            tempPrevious = tempNode;
                            tempNode = tempNode.Next;
                        }
                        else
                        {
                            tempPrevious.Next = null;
                            last = tempPrevious;
                            break;
                        }
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 查找数据所属节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public SingleLinkedListNode<T> Find(T value)
        {
            if (Contains(value))
            {
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Value.Equals(value))
                {
                    return first;
                }
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        if (tempNode.Value.Equals(value))
                        {
                            return tempNode;
                        }
                        tempNode = tempNode.Next;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 查找数据上一个节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public SingleLinkedListNode<T> FindPrevious(T value)
        {
            if (first == null)
            {
                return null;
            }

            SingleLinkedListNode<T> tempNode = first;
            if (tempNode.Value.Equals(value))
            {
                return null;
            }

            SingleLinkedListNode<T> tempPrevious = null;
            while (true)
            {
                if (tempNode.Next != null)
                {
                    tempPrevious = tempNode;
                    tempNode = tempNode.Next;
                    if (tempNode.Value.Equals(value))
                    {
                        return tempPrevious;
                    }
                }
                else
                {
                    break;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取下标的数据域
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>数据</returns>
        public T GetElement(int index)
        {
            if (index >= Count)
            {
                return default;
            }
            else
            {
                if (index == 0)
                {
                    return first.Value;
                }
                else
                {
                    SingleLinkedListNode<T> tempNode = first;
                    //拿到需要删除节点的上一个节点
                    for (int i = 0; i < index; i++)
                    {
                        tempNode = tempNode.Next;
                    }
                    return tempNode.Value;
                }
            }
        }

        /// <summary>
        /// 查找数据下标
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>返回该数据第一次出现的下标</returns>
        public int IndexOf(T value)
        {
            if (Contains(value))
            {
                int tempIndex = 0;
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Value.Equals(value))
                {
                    return tempIndex;
                }
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        if (tempNode.Value.Equals(value))
                        {
                            return tempIndex;
                        }
                        tempNode = tempNode.Next;
                        tempIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 插入数据到Index下标处
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="index">下标</param>
        /// <returns>Node节点</returns>
        public SingleLinkedListNode<T> Insert(T value, int index)
        {
            if (index > Count || index < 0)
            {
                return null;
            }
            if (index == 0)
            {
                return AddFirst(value);
            }
            else if (index == Count)
            {
                return AddLast(value);
            }
            else
            {
                if (first == null) return null;

                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index - 1; i++)
                {
                    tempNode = tempNode.Next;
                }
                SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>();
                newNode.Value = value;
                newNode.Next = tempNode.Next;
                tempNode.Next = newNode;
                return newNode;
            }
        }

        /// <summary>
        /// 插入Node节点到Index下标节点
        /// </summary>
        /// <param name="node">数据节点</param>
        /// <param name="index">下标</param>
        /// <returns>真正添加的Node节点</returns>
        public SingleLinkedListNode<T> Insert(SingleLinkedListNode<T> node, int index)
        {
            if (index > Count || index < 0)
            {
                return null;
            }
            if (index == 0)
            {
                return AddFirst(node);
            }
            else if (index == Count)
            {
                return AddLast(node);
            }
            else
            {
                if (first == null) return null;

                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index - 1; i++)
                {
                    tempNode = tempNode.Next;
                }
                SingleLinkedListNode<T> newNode = CopyToFrom(node);
                newNode.Next = tempNode.Next;
                tempNode.Next = newNode;
                return newNode;
            }
        }

        /// <summary>
        /// 打印链表
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "链表信息";
            if (first == null)
            {
                Console.WriteLine("空链表");
                return str;
            }
            SingleLinkedListNode<T> tempNode = first;
            str += tempNode.Value.ToString();
            while (true)
            {
                if (tempNode.Next != null)
                {
                    tempNode = tempNode.Next;
                    str += tempNode.Value.ToString();
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine(str);
            return str;
        }

        /// <summary>
        /// 获取链表数量
        /// </summary>
        /// <returns></returns>
        private int GetCount()
        {
            if (first == null) return 0;

            int tempCount = 1;
            SingleLinkedListNode<T> tempNode = first;
            while (true)
            {
                if (tempNode.Next != null)
                {
                    tempNode = tempNode.Next;
                    tempCount++;
                }
                else
                {
                    break;
                }
            }
            return tempCount;
        }

        /// <summary>
        /// 判断链表是否为空
        /// </summary>
        /// <returns></returns>
        private bool GetIsEmpty()
        {
            return first == null;
        }

        /// <summary>
        /// 为避免参数重复赋值(比如已经加了头结点,又加入了尾节点)
        /// </summary>
        /// <returns></returns>
        private SingleLinkedListNode<T> CopyToFrom(SingleLinkedListNode<T> node)
        {
            SingleLinkedListNode<T> tempNode = new SingleLinkedListNode<T>();
            tempNode.Value = node.Value;
            tempNode.Next = tempNode.Next;
            return tempNode;
        }
    }

    public interface ISingleLinkedListFunction<T>
    { /// <summary>
      /// 第一个节点
      /// </summary>
        SingleLinkedListNode<T> First { get; }

        /// <summary>
        /// 最后一个节点
        /// </summary>
        SingleLinkedListNode<T> Last { get; }

        /// <summary>
        /// 链表数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 链表是否为空
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 清空链表
        /// </summary>
        void Clear();

        /// <summary>
        /// 链表中是否包含该数据
        /// </summary>
        /// <param name="value">验证数据项</param>
        /// <returns></returns>
        bool Contains(T value);

        /// <summary>
        /// 添加一个节点到节点起点
        /// </summary>
        /// <param name="value">节点数据</param>
        /// <returns>返回Node节点</returns>
        SingleLinkedListNode<T> AddFirst(T value);

        /// <summary>
        /// 添加一个节点到节点起点
        /// </summary>
        /// <param name="node">需要添加的节点</param>
        /// <returns>添加的并不是参数节点,而是参数数据,返回添加的节点</returns>
        SingleLinkedListNode<T> AddFirst(SingleLinkedListNode<T> node);

        /// <summary>
        /// 添加一个节点到链表最后
        /// </summary>
        /// <param name="value">节点数据</param>
        /// <returns>节点</returns>
        SingleLinkedListNode<T> AddLast(T value);

        ///<summary>
        /// 添加一个节点到链表最后
        /// </summary>
        /// <param name="node">需要添加的节点</param>
        /// <returns>添加的并不是参数节点,而是参数数据,返回添加的节点</returns>
        SingleLinkedListNode<T> AddLast(SingleLinkedListNode<T> node);

        /// <summary>
        /// 插入一个节点到指定的下标出
        /// </summary>
        /// <param name="value">节点数据</param>
        /// <param name="index">插入下标</param>
        /// <returns>新增节点</returns>
        SingleLinkedListNode<T> Insert(T value, int index);

        /// <summary>
        /// 插入一个节点到指定的下标出
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="index">插入下标</param>
        /// <returns>添加的并不是参数节点,而是参数数据,返回添加的节点</returns>
        SingleLinkedListNode<T> Insert(SingleLinkedListNode<T> node, int index);

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>是否删除成功</returns>
        bool Delete(T value);

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="index">删除的节点下标</param>
        /// <returns>是否删除成功</returns>
        bool DeleteAt(int index);

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="index">删除的节点</param>
        /// <returns>是否删除成功</returns>
        bool Delete(SingleLinkedListNode<T> node);

        /// <summary>
        /// 删除第一个节点
        /// </summary>
        /// <returns>是否删除成功</returns>
        bool DeleteFirst();

        /// <summary>
        /// 删除最后一个节点
        /// </summary>
        /// <returns>是否删除成功</returns>
        bool DeleteLast();

        /// <summary>
        /// 根据数据查找节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>节点</returns>
        SingleLinkedListNode<T> Find(T value);

        /// <summary>
        /// 根据数据查找上一个节点
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>节点</returns>
        SingleLinkedListNode<T> FindPrevious(T value);

        /// <summary>
        /// 索引器取数据
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns></returns>
        T this[int index] { get; }

        /// <summary>
        /// 根据下标取数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T GetElement(int index);

        /// <summary>
        /// 根据数据获取下标
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>数据节点下标</returns>
        int IndexOf(T value);
    }
}