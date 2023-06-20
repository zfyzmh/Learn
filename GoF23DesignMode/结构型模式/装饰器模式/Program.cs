using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode.结构型模式.装饰器模式
{
    public class Program
    {
        [Test]
        public void Main()
        {
            void Main(string[] args)
            {
                // Creating a rectangle
                IShape rectangle = new Rectangle();

                // Decorating the rectangle with a red border
                IShape redBorderRectangle = new RedBorderDecorator(rectangle);

                // Drawing the rectangle with a red border
                redBorderRectangle.Draw();
            }
        }

        // Component
        public interface IShape
        {
            void Draw();
        }

        // Concrete Component
        public class Rectangle : IShape
        {
            public void Draw()
            {
                Console.WriteLine("Drawing a rectangle.");
            }
        }

        // Decorator
        public abstract class ShapeDecorator : IShape
        {
            protected IShape shape;

            public ShapeDecorator(IShape shape)
            {
                this.shape = shape;
            }

            public virtual void Draw()
            {
                shape.Draw();
            }
        }

        // Concrete Decorator
        public class RedBorderDecorator : ShapeDecorator
        {
            public RedBorderDecorator(IShape shape) : base(shape)
            {
            }

            public override void Draw()
            {
                shape.Draw();
                Console.WriteLine("Adding red border.");
            }
        }
    }
}