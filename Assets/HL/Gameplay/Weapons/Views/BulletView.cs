using HL.Gameplay.Features.Core;
using HL.Gameplay.Weapons.Components;
using UnityEngine;
using Collision = HL.Gameplay.Weapons.Components.Collision;

namespace HL.Gameplay.Weapons.Views
{
	public class BulletView : Actor<Bullet>
	{
		protected override void Initialize()
		{
			Get<Bullet>().View = this;
		}

		private void OnTriggerEnter(Collider col)
		{
			Add<Collision>();
		}
	}
}