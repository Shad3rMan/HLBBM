using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Extensions
{
    public struct AsyncOperationAwaiter : INotifyCompletion
    {
        private readonly AsyncOperation _operation;

        public bool IsCompleted => _operation.isDone;

        public AsyncOperationAwaiter(AsyncOperation operation)
        {
            _operation = operation;
        }

        public void OnCompleted(Action continuation)
        {
            continuation?.Invoke();
        }

        public void GetResult() { }
    }
}