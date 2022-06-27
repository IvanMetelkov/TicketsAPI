using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace tickets
{
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        public AsyncLazy(Func<Task<T>> taskFactory, LazyThreadSafetyMode lazyThreadSafetyMode) :
            base(() => Task.Factory.StartNew(() => taskFactory()).Unwrap(), lazyThreadSafetyMode)
        { }

        public TaskAwaiter<T> GetAwaiter() { return Value.GetAwaiter(); }
    }
}
