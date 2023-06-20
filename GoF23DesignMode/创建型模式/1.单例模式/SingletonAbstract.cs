using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode._1.单例模式
{
    public abstract class SingletonAbstract<T> where T : class, new()
    {
        private static T instance = null;
        private static readonly object padlock = new object();

        protected SingletonAbstract()
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
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }
        }
    }
}