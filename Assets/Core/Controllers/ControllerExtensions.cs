using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Core.Controllers
{
	public static class ControllerExtensions
	{
		public struct ControllerAwaiter : INotifyCompletion
		{
			private readonly BaseController _controller;

			public ControllerAwaiter(BaseController controller)
			{
				_controller = controller;
			}

			public void OnCompleted(Action continuation)
			{
				continuation();
			}

			public bool IsCompleted => false;

			public void GetResult()
			{
			}
		}

		public static UniTask.Awaiter GetAwaiter(this BaseController controller)
		{
			return controller.Task.GetAwaiter();
		}
	}
}