using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Windows.Threading
{
    static class DispatcherExtensions
    {
        public static DispatcherAwaiter GetAwaiter(this Dispatcher dispatcher)
        {
            return new DispatcherAwaiter(dispatcher);
        }

        public readonly struct DispatcherAwaiter : INotifyCompletion
        {
            private readonly DispatcherPriority _priority;
            [NotNull] private readonly Dispatcher _dispatcher;

            public bool IsCompleted => _dispatcher.CheckAccess();

            public DispatcherAwaiter([NotNull] Dispatcher dispatcher)
            {
                _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
                _priority = DispatcherPriority.Normal;
            }

            public DispatcherAwaiter([NotNull] Dispatcher dispatcher, DispatcherPriority priority)
            {
                _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
                _priority = priority;
            }

            public void OnCompleted(Action continuation)
            {
                if (_priority == DispatcherPriority.Normal)
                {
                    _dispatcher.Invoke(continuation);
                }
                else
                {
                    _dispatcher.Invoke(continuation, _priority);
                }
            }

            public void GetResult() 
            { 
            }
        }
    }
}
