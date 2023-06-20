using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode._1.命令模式
{
    // 命令接口
    public interface ICommand
    {
        void Execute();

        void Undo();
    }
}