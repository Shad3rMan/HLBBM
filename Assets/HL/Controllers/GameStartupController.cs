using System;
using System.Threading;
using Core.AI;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using HL.AI.Objectives;
using HL.Modules.MainMenu.Controllers;
using HL.Modules.MainMenu.Messages;
using ILogger = Core.Logging.ILogger;

namespace HL.Controllers
{
	public class GameStartupController : BaseController
	{
		private ActionQueueExecutor _executor;

		public GameStartupController(IControllerFactory controllerFactory, ILogger logger) : base(controllerFactory, logger)
		{
			var brain = new Brain();
			var queue = new ActionQueue(128);
			brain.AddObjective(new DefendObjective());
			brain.UpdateActionQueue(queue);
		}

		public void StartTree()
		{
			Start();
		}

		public void StopTree()
		{
			Stop();
		}

		protected override async UniTask Running(CancellationToken token)
		{
			
			await RunChildController<MainMenuController>();
			await UniTask.Delay(5000, cancellationToken:token);
		}

		protected override bool HandleMessage(BaseMessage message)
		{
			if (message is QuitMessage)
			{
				message.Process();
				StopTree();
			}

			return base.HandleMessage(message);
		}
	}
}