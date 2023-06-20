namespace CSharp
{
    /// <summary>
    /// 迭代器
    /// </summary>
    public class yieldreturn
    {
        /// <summary>
        /// ToList之类的方法会使迭代器立即执行
        /// </summary>
        [Test]
        public void ToList()
        {
            var numbers = ProduceEvenNumbers(5).ToList();
            Console.WriteLine("Caller: about to iterate.");

            foreach (int i in numbers)
            {
                Console.WriteLine($"Caller: {i}");
            }

            IEnumerable<int> ProduceEvenNumbers(int upto)
            {
                Console.WriteLine("Iterator: start.");
                for (int i = 0; i <= upto; i += 2)
                {
                    Console.WriteLine($"Iterator: about to yield {i}");
                    yield return i;
                    Console.WriteLine($"Iterator: yielded {i}");
                }
                Console.WriteLine("Iterator: end.");
            }
        }

        /// <summary>
        /// 迭代器的默认执行模式
        /// </summary>
        [Test]
        public void Default()
        {
            var numbers = ProduceEvenNumbers(5);
            Console.WriteLine("Caller: about to iterate.");
            foreach (int i in numbers)
            {
                Console.WriteLine($"Caller: {i}");
            }

            IEnumerable<int> ProduceEvenNumbers(int upto)
            {
                Console.WriteLine("Iterator: start.");
                for (int i = 0; i <= upto; i += 2)
                {
                    Console.WriteLine($"Iterator: about to yield {i}");
                    yield return i;
                    Console.WriteLine($"Iterator: yielded {i}");
                }
                Console.WriteLine("Iterator: end.");
            }
            // Output:
            // Caller: about to iterate.
            // Iterator: start.
            // Iterator: about to yield 0
            // Caller: 0
            // Iterator: yielded 0
            // Iterator: about to yield 2
            // Caller: 2
            // Iterator: yielded 2
            // Iterator: about to yield 4
            // Caller: 4
            // Iterator: yielded 4
            // Iterator: end.
        }

        /// <summary>
        /// yield break手动结束迭代器
        /// </summary>
        [Test]
        public void YieldBreak()
        {
            Console.WriteLine(string.Join(" ", TakeWhilePositive(new[] { 2, 3, 4, 5, -1, 3, 4 })));
            // Output: 2 3 4 5

            Console.WriteLine(string.Join(" ", TakeWhilePositive(new[] { 9, 8, 7 })));
            // Output: 9 8 7

            IEnumerable<int> TakeWhilePositive(IEnumerable<int> numbers)
            {
                foreach (int n in numbers)
                {
                    if (n > 0)
                    {
                        yield return n;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// yield ssync  迭代器类型必须为IAsyncEnumerable或IAsyncEnumerable<T>
        /// </summary>
        [Test]
        public async Task YieldAsync()
        {
            await foreach (int n in GenerateNumbersAsync(5))
            {
                Console.Write(n);
                Console.Write(" ");
            }
            // Output: 0 2 4 6 8

            async IAsyncEnumerable<int> GenerateNumbersAsync(int count)
            {
                for (int i = 0; i < count; i++)
                {
                    yield return await ProduceNumberAsync(i);
                }
            }

            async Task<int> ProduceNumberAsync(int seed)
            {
                await Task.Delay(1000);
                return 2 * seed;
            }
        }
    }
}