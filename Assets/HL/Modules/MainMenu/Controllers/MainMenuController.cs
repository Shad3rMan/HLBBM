using System.Threading;
using Core.Controllers;
using Core.Logging;
using Cysharp.Threading.Tasks;
using HL.Modules.MainMenu.Commands;
using HL.Modules.MainMenu.Messages;
using HL.Modules.MainMenu.Views;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace HL.Modules.MainMenu.Controllers
{
	public class MainMenuController : BaseController
	{
		private readonly MainMenuView _mainMenuView;
		private readonly MenuCanvasView _canvasView;

		public MainMenuController(
			MainMenuView mainMenuView,
			MenuCanvasView canvasView,
			IControllerFactory controllerFactory,
			ILogger logger) : base(controllerFactory, logger)
		{
			_mainMenuView = mainMenuView;
			_canvasView = canvasView;
		}

		protected override void OnStart()
		{
			_mainMenuView.PlayButton.onClick.AddListener(OnPlayButtonClick);
			_mainMenuView.ExitButton.onClick.AddListener(OnExitButtonClick);
			_mainMenuView.gameObject.SetActive(true);
		}

		protected override void OnStop()
		{
			_mainMenuView.PlayButton.onClick.RemoveListener(OnPlayButtonClick);
			_mainMenuView.ExitButton.onClick.RemoveListener(OnExitButtonClick);
			_mainMenuView.gameObject.SetActive(false);
		}

		protected override UniTask Running(CancellationToken token)
		{
			return UniTask.CompletedTask;
		}

		private async void OnPlayButtonClick()
		{
			await RunChildController<LevelSelectionController>();
			Stop();
		}

		private void OnExitButtonClick()
		{
			SendMessageUp(new QuitMessage());
		}

		private async void LoadLevel(string level)
		{
			await new LoadLevelCommand(level).Execute();
		}
		
		protected override bool HandleMessage(BaseMessage message)
		{
			if (message is LevelSelectedMessage m)
			{
				LoadLevel(m.Level);
				m.Process();
			}
			
			return base.HandleMessage(message);
		}
	}
}