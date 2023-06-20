using NUnit.Framework;
using System;

namespace algorithm.GoF23DesignMode._2.桥接模式
{
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        [Test]
        public void Main()
        {
            Circle circle = new Circle(new RedColor());
            circle.FillColor();

            Rectangle rectangle = new Rectangle(new GreenColor());
            rectangle.FillColor();
        }

        public interface IColor
        {
            void FillColor();
        }

        public class RedColor : IColor
        {
            public void FillColor()
            {
                Console.WriteLine("填充红色");
            }
        }

        public class GreenColor : IColor
        {
            public void FillColor()
            {
                Console.WriteLine("填充绿色");
            }
        }

        public abstract class Shape
        {
            protected IColor color;

            public Shape(IColor color)
            {
                this.color = color;
            }

            public abstract void FillColor();
        }

        public class Circle : Shape
        {
            public Circle(IColor color) : base(color)
            {
            }

            public override void FillColor()
            {
                Console.Write("圆形 ");
                color.FillColor();
            }
        }

        public class Rectangle : Shape
        {
            public Rectangle(IColor color) : base(color)
            {
            }

            public override void FillColor()
            {
                Console.Write("矩形 ");
                color.FillColor();
            }
        }
    }
}