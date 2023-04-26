using NUnit.Framework;
using System;
using System.Linq;

namespace algorithm.GoF23DesignMode.结构型模式.装饰器模式
{
    public class Attribute版
    {
        [Test]
        public void Main()
        {
            ShapeFactory<Rectangle> shapeFactory = new ShapeFactory<Rectangle>();
            // Creating a rectangle
            IShape rectangle = shapeFactory.CreateShape();

            // Decorating the rectangle with a red border
            MyClass myClass = new MyClass("adaasd");

            Console.WriteLine(rectangle.Draw()); ;
        }

        private class MyClass
        {
            private readonly string nAME;

            public MyClass(string name)
            {
                nAME = name;
            }
        }

        // Component
        public interface IShape
        {
            string Draw();
        }

        [Shape("红色")]
        public class Rectangle : IShape
        {
            public string Draw()
            {
                return "Drawing a rectangle.";
            }
        }

        /// <summary>
        /// 泛型工厂
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ShapeFactory<T> where T : IShape, new()
        {
            /// <summary>
            /// 初始化Doman
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public IShape CreateShape()
            {
                var model = new T();
                // 给Doman层添加其他层的依赖
                return SetModel(model);
            }

            private IShape SetModel(T model)
            {
                Type modelType = model.GetType();
                // 获取DomanModel类上所有的特性
                ShapeAttribute modell = (ShapeAttribute)modelType.GetCustomAttributes(true).FirstOrDefault();
                //.Where(m => m.GetType().BaseType.Name == "ShapeAttribute").ToArray();
                modell.GetModel(model);
                var lastModel = (IShape)modell;
                return lastModel;
            }
        }

        /// <summary>
        /// 该类将作为各个装饰类的基类
        /// </summary>
        public class ShapeAttribute : Attribute, IShape
        {
            protected IShape _model;
            private readonly string color;

            public ShapeAttribute()
            {
            }

            public ShapeAttribute(string color)
            {
                this.color = color;
            }

            public void GetModel(IShape model)
            {
                this._model = model;
            }

            public string Draw()
            {
                return _model.Draw() + $"且加了{color}色边框";
            }
        }
    }
}