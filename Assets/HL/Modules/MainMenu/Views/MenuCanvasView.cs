using UnityEngine;

namespace HL.Modules.MainMenu.Views
{
	public class MenuCanvasView : MonoBehaviour
	{
		[SerializeField]
		private RectTransform _uiRoot;

		public RectTransform UIRoot => _uiRoot;
	}
}