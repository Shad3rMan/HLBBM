using UnityEngine;

namespace HL.Gameplay.Features.Player.Config
{
	public class PlayerConfig : ScriptableObject
	{
		[SerializeField]
		private float _moveSpeed = 10f;
		[SerializeField]
		private float _sprintSpeed = 20f;
		[SerializeField]
		private float _verticalSensitivity = 1f;
		[SerializeField]
		private float _horizontalSensitivity = 1f;

		public float MoveSpeed => _moveSpeed;
		public float SprintSpeed => _sprintSpeed;
		public float VerticalSensitivity => _verticalSensitivity;
		public float HorizontalSensitivity => _horizontalSensitivity;
	}
}