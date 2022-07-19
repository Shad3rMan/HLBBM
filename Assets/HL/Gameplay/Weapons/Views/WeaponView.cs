using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Weapons.Components;
using UnityEngine;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Weapons.Views
{
	public class WeaponView : Actor<Weapon>
	{
		[SerializeField]
		private int _capacity;
		[SerializeField]
		private Transform _shootingRoot;
		[SerializeField]
		private BulletView _bulletPrefab;

		protected override void Initialize()
		{
			ref var weapon = ref Get<Weapon>();
			weapon.Capacity = _capacity;
			weapon.BulletPrefab = _bulletPrefab;
			weapon.ShootingRoot = _shootingRoot;
			Add<Input>();
		}
	}
}