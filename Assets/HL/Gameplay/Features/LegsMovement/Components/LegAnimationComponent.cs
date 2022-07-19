using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.LegsMovement.Components
{
	public struct LegAnimationComponent : IEcsAutoReset<LegAnimationComponent>
	{
		public Vector3 StartPosition;
		public Vector3 EndPosition;
		public float Time;
		public float Duration;

		public void AutoReset(ref LegAnimationComponent c)
		{
			c.Time = 0;
		}
	}
}