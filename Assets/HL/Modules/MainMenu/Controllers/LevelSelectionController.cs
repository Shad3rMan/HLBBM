using System.Threading;
using Core.Controllers;
using Core.Logging;
using Cysharp.Threading.Tasks;
using HL.Modules.MainMenu.Messages;
using HL.Modules.MainMenu.Views;

namespace HL.Modules.MainMenu.Controllers
{
	public class LevelSelectionController : BaseController
	{
		private readonly LevelSelectionView _view;

		public LevelSelectionController(LevelSelectionView view, IControllerFactory controllerFactory, ILogger logger) : base(controllerFactory, logger)
		{
			_view = view;
		}

		protected override void OnStart()
		{
			_view.Selected += OnLevelSelected;
		}

		protected override void OnStop()
		{
			_view.Selected -= OnLevelSelected;
		}

		private void OnLevelSelected(int index)
		{
			SendMessageUp(new LevelSelectedMessage($"lv{index}"));
		}

		protected override UniTask Running(CancellationToken token)
		{
			OnLevelSelected(1);
			return UniTask.CompletedTask;
		}
	}
}