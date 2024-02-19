using NUnit.Framework;
using SharpCompress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithm.N叉树
{
    /// <summary>
    /// 给定一个 n 叉树的根节点  root ，返回 其节点值的 后序遍历 。
    /// </summary>
    internal class _590
    {
        [Test]
        public void Test1()
        {
            Node node = new(1);
            node.children = new List<Node>() { new(3, [new(5), new(6)]), new(2), new(4) };

            Console.WriteLine(string.Join(" ", Postorder(node)));
        }

        public IList<int> Postorder(Node root)
        {
            if (root == null) return new List<int>();

            var result = new List<int>();

            if (root.children != null && root.children.Any())
            {
                foreach (var child in root.children)
                {
                    result.AddRange(Postorder(child));
                }
            }
            result.Add(root.val);

            return result;
        }
    }
}