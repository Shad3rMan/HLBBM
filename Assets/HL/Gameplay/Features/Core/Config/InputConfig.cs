using UnityEngine;

namespace HL.Gameplay.Features.Core.Config
{
	public class InputConfig : ScriptableObject
	{
		[SerializeField]
		private string _horizontalAxis = "Horizontal";
		[SerializeField]
		private string _verticalAxis = "Vertical";
		[SerializeField]
		private string _mouseX = "Mouse X";
		[SerializeField]
		private string _mouseY = "Mouse Y";
		[SerializeField]
		private KeyCode _jumpKey = KeyCode.Space;
		[SerializeField]
		private KeyCode _actionKey = KeyCode.E;
		[SerializeField]
		private KeyCode _runKey = KeyCode.LeftShift;
		[SerializeField]
		private KeyCode _fireKey = KeyCode.Mouse0;
		[SerializeField]
		private KeyCode _alternateFireKey = KeyCode.Mouse1;

		public string HorizontalAxis => _horizontalAxis;

		public string VerticalAxis => _verticalAxis;

		public string MouseX => _mouseX;

		public string MouseY => _mouseY;

		public KeyCode JumpKey => _jumpKey;

		public KeyCode ActionKey => _actionKey;

		public KeyCode RunKey => _runKey;

		public KeyCode FireKey => _fireKey;

		public KeyCode AlternateFireKey => _alternateFireKey;
	}
}