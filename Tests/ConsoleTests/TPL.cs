using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    static class TPL
    {
        public static void Start()
        {
            //ThreadTests.Start();
            //CriticalSectionTest.Start();
            //ThreadPoolTests.Start();

            //Console.WriteLine("Главный поток работу закончил");
            //Console.ReadLine();
            /*
            new Thread(() =>
            {
                while (true)
                {
                    Console.Title = DateTime.Now.ToString();
                    Thread.Sleep(100);
                }
            })
            {
                IsBackground = true
            }.Start();
            new Task(() =>
            {
                while (true)
                {
                    Console.WriteLine(DateTime.Now);
                    Thread.Sleep(100);
                }
            }).Start();
            */

            /*
            var factorial = new MathTask(()=> Factorial(10));
            var sum = new MathTask(() => Sum(10));

            factorial.Start();
            sum.Start();

            Console.WriteLine($"Факториал {factorial.Result}; сумма {sum.Result}");
            */


            Action<string> printer = str =>
            {
                Console.WriteLine($"Сообщение [th id:{Thread.CurrentThread.ManagedThreadId,2}: {str}]");
                Thread.Sleep(100);
            };

            printer("Ok, Console!");
            printer.Invoke("No!");


            /*
            Parallel.Invoke(
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                () => Console.WriteLine("Еще один метод"));
            */

            //var processControl = printer
            //    .BeginInvoke("Action", result => { Console.WriteLine("Операция печати завершена"); }, 8800);

            /*
            Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 2 },
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                () => Console.WriteLine("Еще один метод"));
            */
            //Parallel.For(0, 100, i => printer(i.ToString()));
            //Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2 }, i => printer(i.ToString()));

            /*
            var result = Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (i, state) =>
            {
                printer(i.ToString());
                if (i > 10)
                {
                    state.Break();
                }
            });
            Console.WriteLine($"Выполнено {result.LowestBreakIteration} итераций");
            */

            var messages = Enumerable.Range(1, 500).Select(i => $"Messages {i}");

            //Parallel.ForEach(messages, 
            //    new ParallelOptions { MaxDegreeOfParallelism = 2 }, message => printer(message));
            /*
            foreach (var message in messages.Where(msg => msg.EndsWith("0")))
            {
                printer(message);
            }
            */

            /*
            var cancellationToken = new CancellationTokenSource();
            //cancellationToken.Token.ThrowIfCancellationRequested();
            var messagesCount = messages
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .WithCancellation(cancellationToken.Token)
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .Where(msg =>
                {
                    printer(msg);
                    return msg.EndsWith("0");
                })
                .AsSequential()
                .Count();
            */

            /*
            var task = new Task(() => printer("Ok, go!"));
            task.Start();

            var continuationTask = task.ContinueWith(t => Console.WriteLine($"Задача {t.Id} завершилась", TaskContinuationOptions.OnlyOnRanToCompletion));
            continuationTask.ContinueWith(t => { Console.WriteLine("Ok"); }, TaskContinuationOptions.OnlyOnFaulted);
            */

            var optionOne = Task.Run(() => printer("Run!"));
            var optionTwo = Task.Factory.StartNew(obj => printer((string)obj), "Factory!");//медленней Run


            var resultTask = Task.Run(() =>
            {
                Thread.Sleep(100);
                return 911;
            });
            var resultTaskSecond = Task.Run(() =>
            {
                Thread.Sleep(200);
                return 911;
            });
            var resultTaskThird = Task.Run(() =>
            {
                Thread.Sleep(300);
                return 911;
            });
            var result = resultTask.Result;
            Task.WaitAll(resultTask, resultTaskSecond, resultTaskThird);//не рекомендуется
            var index = Task.WaitAny(resultTask, resultTaskSecond, resultTaskThird);//не рекомендуется
        }
        private static void ParallelInvokeMethod()
        {
            Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId} - started");
            Thread.Sleep(250);
            Console.WriteLine($"thread id {Thread.CurrentThread.ManagedThreadId} - finished");
        }
        private static void ParallelInvokeMethod(string msg)
        {
            Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId} - started: {msg}");
            Thread.Sleep(250);
            Console.WriteLine($"thread id {Thread.CurrentThread.ManagedThreadId} - finished: {msg}");
        }
        private static long Factorial(int n)
        {
            //Console.WriteLine($"Расчет факториала {n}");
            var factorial = 1;
            for (var i = 1; i <= n; i++)
            {
                factorial *= n;
            }
            //Console.WriteLine($"Факториал {n} равен {factorial}");
            return factorial;
        }
        private static long Sum(int n)
        {
            //Console.WriteLine($"Расчет суммы {n}");
            var sum = 0;
            for (var i = 0; i <= n; i++)
            {
                sum += n;
            }
            //Console.WriteLine($"Сумма {n} равна {sum}");
            return sum;
        }

        class MathTask
        {
            private readonly Thread _calculationThread;
            private long _result;
            private bool _isCompleted;

            public bool IsCompleted => _isCompleted;
            public long Result
            {
                get
                {
                    if (!_isCompleted)
                    {
                        _calculationThread.Join();
                    }
                    return _result;
                }
            }
            public MathTask(Func<long> calculation)
            {
                _calculationThread = new Thread(() =>
                {
                    _result = calculation();
                    _isCompleted = true;
                })
                {
                    IsBackground = true
                };
            }
            public void Start() => _calculationThread.Start();
        }
        /*
        [DllImport("filename.dll")]
        private static extern void MethodName(string str);
        */
    }
}
