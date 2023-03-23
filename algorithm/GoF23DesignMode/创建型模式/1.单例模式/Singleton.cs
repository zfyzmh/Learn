using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode._1.单例模式
{
    public sealed class Singleton<T> where T : class, new()
    {
        private static T instance = null;
        private static readonly object padlock = new object();

        private Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        instance ??= new T();
                    }
                }
                return instance;
            }
        }
    }
}