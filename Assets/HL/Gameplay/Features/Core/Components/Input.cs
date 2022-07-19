using UnityEngine;

namespace HL.Gameplay.Features.Core.Components
{
	public struct Input
	{
		public Vector2 Move;
		public Vector2 MousePosition;
		public Vector2 MouseDelta;
		public bool Shoot;
		public bool AltShoot;
		public bool Jump;
		public bool Action;
		public bool Run;
	}
}