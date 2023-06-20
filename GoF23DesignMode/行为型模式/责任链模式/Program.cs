using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode.行为型模式.责任链模式
{
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        [Test]
        public void Main()
        {
            IManager groupLeader = new GroupLeader();
            IManager projectManager = new ProjectManager();
            IManager boss = new Boss();

            groupLeader.SetSuperior(projectManager);
            projectManager.SetSuperior(boss);

            Request request = new Request();
            request.num = 3;
            groupLeader.HandleRequest(request);
        }
    }

    /// <summary>
    /// 抽象处理者
    /// </summary>
    public abstract class IManager
    {
        protected IManager _superior;

        public void SetSuperior(IManager superior)
        {
            _superior = superior;
        }

        public abstract void HandleRequest(Request request);
    }

    public class Request
    {
        public int num;
    }

    public class GroupLeader : IManager
    {
        public override void HandleRequest(Request request)
        {
            if (request.num <= 1)
            {
                Console.WriteLine("通过");
            }
            else
            {
                _superior.HandleRequest(request);
            }
        }
    }

    public class ProjectManager : IManager
    {
        public override void HandleRequest(Request request)
        {
            if (request.num <= 1)
            {
                Console.WriteLine("找我的下级处理这件事儿");
            }
            else if (request.num > 1 && request.num <= 3)
            {
                Console.WriteLine("通过");
            }
            else
            {
                _superior.HandleRequest(request);
            }
        }
    }

    public class Boss : IManager
    {
        public override void HandleRequest(Request request)
        {
            if (request.num <= 3)
            {
                Console.WriteLine("找我的下级处理这件事儿");
            }
            else
            {
                Console.WriteLine("通过");
            }
        }
    }
}