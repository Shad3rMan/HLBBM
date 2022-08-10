using HL.Gameplay.Weapons.Views;
using UnityEngine;

namespace HL.Gameplay.Weapons.Components
{
	public struct Weapon
	{
		public BulletView BulletPrefab;
		public Transform ShootingRoot;
		public int Capacity;
	}
}