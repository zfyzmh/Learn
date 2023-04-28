using NUnit.Framework;
using System;

namespace algorithm.GoF23DesignMode._2.适配器模式
{
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        [Test]
        public void Main()
        {
            IShape rectangle = new Rectangle { Width = 10, Height = 20 };
            Circle circle = new Circle { Radius = 5 };
            IShape circleAdapter = new CircleAdapter(circle);
            Console.WriteLine("The area of rectangle is: " + rectangle.GetArea());
            Console.WriteLine("The area of circle is: " + circleAdapter.GetArea());
            Console.ReadLine();
        }
    }

    public interface IShape
    {
        int GetArea();
    }

    public class Rectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int GetArea()
        {
            return Width * Height;
        }
    }

    public class Circle
    {
        public int Radius { get; set; }

        public int GetCircleArea()
        {
            return (int)(Radius * Radius * Math.PI);
        }
    }

    public class CircleAdapter : IShape
    {
        private Circle Circle;

        public CircleAdapter(Circle circle)
        {
            Circle = circle;
        }

        public int GetArea()
        {
            return Circle.GetCircleArea();
        }
    }
}