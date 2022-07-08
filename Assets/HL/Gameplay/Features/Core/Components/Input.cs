using UnityEngine;

namespace HL.Gameplay.Features.Core.Components
{
	public struct Input
	{
		public Vector3 Move;
		public Vector3 MousePosition;
		public Vector2 MouseDelta;
		public bool Shoot;
		public bool AltShoot;
		public bool Jump;
		public bool Action;
		public bool Run;
	}
}