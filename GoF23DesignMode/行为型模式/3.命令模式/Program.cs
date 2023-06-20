using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode._1.命令模式
{
    public class Program
    {
        [Test]
        public void Main()
        {
            var numbers = new List<int>();
            var commandManager = new CommandManager();

            // 添加元素
            commandManager.ExecuteCommand(new AddCommand(numbers, 1));
            commandManager.ExecuteCommand(new AddCommand(numbers, 1));
            commandManager.ExecuteCommand(new AddCommand(numbers, 3));

            Console.WriteLine("添加元素后的数组：");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            // 撤销
            commandManager.Undo();
            commandManager.Undo();
            Console.WriteLine("撤销后的数组：");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            // 重做
            commandManager.Redo();
            commandManager.Redo();
            Console.WriteLine("重做后的数组：");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}