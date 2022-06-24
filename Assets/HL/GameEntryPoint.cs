using System;
using Core.Controllers;
using HL.Controllers;
using VContainer.Unity;

namespace HL
{
	public class GameEntryPoint : IStartable, IDisposable
	{
		private readonly IControllerFactory _controllerFactory;
		private GameStartupController _controller;

		public GameEntryPoint(IControllerFactory controllerFactory)
		{
			_controllerFactory = controllerFactory;
		}

		public void Start()
		{
			_controller = _controllerFactory.CreateController<GameStartupController>();
			_controller.StartTree();
		}

		public void Dispose()
		{
			_controller.StopTree();
		}
	}
}