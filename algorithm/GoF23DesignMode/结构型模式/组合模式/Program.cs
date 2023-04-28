using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace algorithm.GoF23DesignMode._2.组合模式
{
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        [Test]
        public void Main()
        {
            // 创建一个文件夹
            Folder root = new Folder("根目录");

            // 添加文件和文件夹
            root.Add(new File("文件1.txt"));
            root.Add(new Folder("文件夹1"));
            root.Add(new Folder("文件夹2"));

            // 添加子文件和子文件夹
            Folder folder1 = (Folder)root.children[1];
            folder1.Add(new File("文件2.txt"));

            Folder folder2 = (Folder)root.children[2];
            folder2.Add(new File("文件3.txt"));
            folder2.Add(new Folder("文件夹3"));

            // 显示根目录及其所有子文件和子文件夹
            root.Display(0);
        }

        // 抽象组件类
        private abstract class FileSystemItem
        {
            protected string name;

            public FileSystemItem(string name)
            {
                this.name = name;
            }

            public abstract void Display(int depth);
        }

        // 文件夹组件类
        private class Folder : FileSystemItem
        {
            public List<FileSystemItem> children = new List<FileSystemItem>();

            public Folder(string name) : base(name)
            {
            }

            public void Add(FileSystemItem item)
            {
                children.Add(item);
            }

            public void Remove(FileSystemItem item)
            {
                children.Remove(item);
            }

            public override void Display(int depth)
            {
                Console.WriteLine(new string('-', depth) + name);

                foreach (var child in children)
                {
                    child.Display(depth + 2);
                }
            }
        }

        // 文件组件类
        private class File : FileSystemItem
        {
            public File(string name) : base(name)
            {
            }

            public override void Display(int depth)
            {
                Console.WriteLine(new string('-', depth) + name);
            }
        }
    }
}