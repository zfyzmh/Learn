using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algorithm.特性学习
{
    public class Demo
    {
        [Test]
        public void Main()
        {
            var model = ModelFactory.CreateModel("Doman层");
            Console.WriteLine(model.Assembling());
        }
    }

    public class ModelFactory
    {
        /// <summary>
        /// 初始化Doman
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IBaseModel CreateModel(string name)
        {
            var model = new DomanModel(name);
            // 给Doman层添加其他层的依赖
            return SetModel(model);
        }

        /// <summary>
        /// 给Doman层添加依赖
        /// </summary>
        /// <param name="model"></param>
        private static IBaseModel SetModel(DomanModel model)
        {
            Type modelType = model.GetType();
            // 获取DomanModel类上所有的特性
            var models = modelType.GetCustomAttributes(false)
                .Where(m => m.GetType().BaseType.Name == "ModleOnAttribute").ToArray();
            for (int i = 0; i < models.Length; i++)
            {
                var module = (ModleOnAttribute)models[i];
                if (i == 0)
                {
                    module.GetModel(model);
                }
                else
                {
                    module.GetModel((IBaseModel)models[i - 1]);
                }
            }
            var lastModel = (IBaseModel)models[models.Length - 1];
            return lastModel;
        }
    }

    [DataModel]
    [EvntbusModel]
    public class DomanModel : IBaseModel
    {
        private readonly string _name;

        public DomanModel(string name)
        {
            this._name = name;
        }

        public string Assembling()
        {
            return this._name;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ModleOnAttribute : Attribute, IBaseModel
    {
        protected IBaseModel _model;

        public virtual void GetModel(IBaseModel model)
        {
            this._model = model;
        }

        public abstract string Assembling();
    }

    public interface IBaseModel
    {
        string Assembling();
    }

    public class DataModel : ModleOnAttribute
    {
        public override void GetModel(IBaseModel model)
        {
            this._model = model;
        }

        public override string Assembling()
        {
            return _model.Assembling() + "初始化DataModel";
        }
    }

    public class EvntbusModel : ModleOnAttribute
    {
        public override void GetModel(IBaseModel model)
        {
            this._model = model;
        }

        public override string Assembling()
        {
            return _model.Assembling() + "初始化EvntbusModel";
        }
    }
}