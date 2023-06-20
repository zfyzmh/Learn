using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode.创建型模式.建造者模式
{
    public class Program
    {
        [Test]
        public void Main()
        {
            // 创建具体建造者对象
            Product product = new ConcreteBuilder()
                .BuildPart1("Part 1")
                .BuildPart1("Part 1")
                .BuildPart3("Part 3")
                .GetResult();
            Console.WriteLine(product);
        }
    }

    internal abstract class Builder
    {
        public abstract Builder BuildPart1(string value);

        public abstract Builder BuildPart2(string value);

        public abstract Builder BuildPart3(string value);

        public abstract Product GetResult();
    }

    internal class ConcreteBuilder : Builder
    {
        private Product product = new Product();

        public override Builder BuildPart1(string value)
        {
            product._part1 += value;
            return this;
        }

        public override Builder BuildPart2(string value)
        {
            product._part2 += value;
            return this;
        }

        public override Builder BuildPart3(string value)
        {
            product._part3 += value;
            return this;
        }

        public override Product GetResult()
        {
            return product;
        }
    }

    internal class Product
    {
        public string _part1 { get; set; }
        public string _part2 { get; set; }
        public string _part3 { get; set; }
    }
}