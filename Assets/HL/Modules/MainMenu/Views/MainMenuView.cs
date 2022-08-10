using UnityEngine;
using UnityEngine.UI;

namespace HL.Modules.MainMenu.Views
{
	public class MainMenuView : MonoBehaviour
	{
		[SerializeField]
		private Button _playButton;
		[SerializeField]
		private Button _settingsButton;
		[SerializeField]
		private Button _exitButton;

		public Button PlayButton => _playButton;

		public Button SettingsButton => _settingsButton;

		public Button ExitButton => _exitButton;
	}
}