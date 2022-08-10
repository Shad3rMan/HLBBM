using HL.Gameplay.Weapons.Views;
using UnityEngine;

namespace HL.Gameplay.Weapons.Components
{
	public struct EmitBulletEvent
	{
		public BulletView BulletView;
		public int Count;
		public Vector3 Position;
		public Vector3 Direction;
		public float Speed;
	}
}