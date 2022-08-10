using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ILogger = Core.Logging.ILogger;

namespace Core.Controllers
{
	public abstract class BaseController
	{
		public UniTask Task { get; private set; }
		private readonly ILogger _logger;
		private readonly IControllerFactory _controllerFactory;
		private readonly List<BaseController> _children;
		private readonly CancellationTokenSource _cts;
		private BaseController _parent;
		
		protected BaseController(IControllerFactory controllerFactory, ILogger logger)
		{
			_controllerFactory = controllerFactory;
			_logger = logger;
			_children = new List<BaseController>();
			_cts = new CancellationTokenSource();
		}

		protected void Start()
		{
			Log(LogType.Log, "Start");
			foreach (var child in _children)
			{
				child.Start();
			}

			OnStart();

			Task = Running(_cts.Token);
		}

		protected void Stop()
		{
			foreach (var child in _children)
			{
				child.Stop();
			}

			_cts.Cancel();
			OnStop();
			Log(LogType.Log, "Stop");
		}

		protected virtual void OnStart()
		{
		}

		protected virtual void OnStop()
		{
		}

		protected abstract UniTask Running(CancellationToken token);

		protected T RunChildController<T>() where T : BaseController
		{
			var controller = _controllerFactory.CreateController<T>();
			RunChildController(controller);
			return controller;
		}

		protected void RemoveChildController(BaseController controller)
		{
			if (_children.Contains(controller))
			{
				_children.Remove(controller);
				controller.Stop();
			}
		}

		protected virtual bool HandleMessage(BaseMessage message)
		{
			if (!message.Processed)
			{
				throw new SystemException();
			}

			return true;
		}

		protected void SendMessageUp(BaseMessage message)
		{
			if (_parent != null)
			{
				if (!_parent.HandleMessage(message))
				{
					_parent.SendMessageUp(message);
				}
			}
			else
			{
				Log(LogType.Warning, "Message was not processed: " + message.GetType());
			}
		}

		protected void SendMessageDown(BaseMessage message)
		{
			foreach (var child in _children)
			{
				if (!child.HandleMessage(message))
				{
					child.SendMessageDown(message);
				}
			}
		}

		protected void Log(LogType logType, string message)
		{
			_logger.Log(logType, GetType().Name, message);
		}

		protected void Log(string message)
		{
			Log(LogType.Log, message);
		}

		private void RunChildController(BaseController controller)
		{
			_children.Add(controller);
			controller._parent = this;
			controller.Start();
		}
	}
}