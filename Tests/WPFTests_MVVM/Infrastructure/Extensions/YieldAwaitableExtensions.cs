using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Threading.Tasks
{
    static class YieldAwaitableExtensions
    {
        public static YeildAwaitableThreadPool ConfigureAwait(this YieldAwaitable _, bool lockContext)
        {
            return new YeildAwaitableThreadPool();
        }
    }
    public readonly ref struct YeildAwaitableThreadPool
    {
        private readonly bool _lockContext;
        public YeildAwaitableThreadPool(in bool lockContext)
        {
            _lockContext = lockContext;
        }
        public Awaiter GetAwaiter()
        {
            return new Awaiter(_lockContext);
        }
        public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
        {
            private readonly bool _lockContext;
            private static readonly WaitCallback _waitCallbackRunAction = RunAction;
            private static readonly SendOrPostCallback _sendOrPostCallbackRunAction = RunAction;

            public Awaiter(in bool lockContext)
            {
                _lockContext = lockContext;
            }
            public bool IsCompleted => false;

            public void GetResult()
            {
            }

            private static void RunAction(object? State) => ((Action)State!).Invoke();

            public void OnCompleted(Action continuation)
            {
                QueueContinuation(continuation, true, _lockContext);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                QueueContinuation(continuation, false, _lockContext);
            }
            private static void QueueContinuation(Action continuation, bool flowContext, bool lockContext)
            {
                if (continuation is null)
                {
                    throw new ArgumentNullException(nameof(continuation));
                }
                var context = SynchronizationContext.Current;
                if (lockContext && context != null && context.GetType() != typeof(SynchronizationContext))
                {
                    context.Post(_sendOrPostCallbackRunAction, continuation);
                }
                else 
                {
                    var scheduler = TaskScheduler.Current;
                    if (!lockContext || scheduler == TaskScheduler.Default)
                    {
                        if (flowContext)
                        {
                            ThreadPool.QueueUserWorkItem(_waitCallbackRunAction, continuation);
                        }
                        else
                        {
                            ThreadPool.UnsafeQueueUserWorkItem(_waitCallbackRunAction, continuation);
                        }
                    }
                    else 
                    {
                        Task.Factory.StartNew(continuation, default, TaskCreationOptions.PreferFairness, scheduler);
                    }
                }
            }
            public override bool Equals(object? obj)
            {
                return obj is Awaiter awaiter && awaiter._lockContext == _lockContext;
            }
            public override int GetHashCode()
            {
                return _lockContext.GetHashCode();
            }
            public static bool operator ==(Awaiter left, Awaiter right)
            {
                return left.Equals(right); 
            }
            public static bool operator !=(Awaiter left, Awaiter right)
            {
                return !left.Equals(right);
            }
            public bool Equals(Awaiter other) => other._lockContext == _lockContext;
        }
    }
}
