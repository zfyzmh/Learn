using System.Collections.Generic;

namespace algorithm.GoF23DesignMode._1.命令模式
{
    // 具体的命令实现
    public class AddCommand : ICommand
    {
        private readonly List<int> _numbers;
        private readonly int _number;

        public AddCommand(List<int> numbers, int number)
        {
            _numbers = numbers;
            _number = number;
        }

        public void Execute()
        {
            _numbers.Add(_number);
        }

        public void Undo()
        {
            _numbers.Remove(_number);
        }
    }
}