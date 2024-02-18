using NUnit.Framework;
using SharpCompress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithm
{
    /// <summary>
    /// 给定一个 n 叉树的根节点  root ，返回 其节点值的 前序遍历 。
    /// </summary>
    internal class _589
    {
        [Test]
        public void Test1()
        {
            Node node = new(1);
            node.children = new List<Node>() { new(3, [new(5), new(6)]), new(2), new(4) };

            Console.WriteLine(string.Join(" ", Preorder(node)));
        }

        public IList<int> Preorder(Node root)
        {
            if (root == null) return new List<int>();

            var result = new List<int>();

            result.Add(root.val);

            if (root.children != null && root.children.Any())
            {
                foreach (var child in root.children)
                {
                    result.AddRange(Preorder(child));
                }
            }

            return result;
        }
    }

    public class Node
    {
        public int val;
        public IList<Node> children;

        public Node()
        { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }
}