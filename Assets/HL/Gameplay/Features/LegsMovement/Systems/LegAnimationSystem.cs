using HL.Gameplay.Features.LegsMovement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.LegsMovement.Systems
{
	public class LegAnimationSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<LegComponent> _legs;
		private EcsPool<LegAnimationComponent> _anims;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<LegComponent>().Inc<LegAnimationComponent>().End();
			_legs = _world.GetPool<LegComponent>();
			_anims = _world.GetPool<LegAnimationComponent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var anim = ref _anims.Get(entity);
				ref var leg = ref _legs.Get(entity);

				var midY = (anim.StartPosition.y + anim.EndPosition.y) / 2;
				if (anim.Time <= anim.Duration)
				{
					var t = anim.Time / anim.Duration;
					//leg.Leg.position = Vector3.Lerp(anim.StartPosition, anim.EndPosition, t);
					leg.Leg.position = new Vector3(
						Mathf.Lerp(anim.StartPosition.x, anim.EndPosition.x, t),
						midY + Mathf.Sin(t * Mathf.PI) * leg.StepHeight,
						0);
					anim.Time += Time.deltaTime;
				}
				else
				{
					leg.Leg.position = anim.EndPosition;
					leg.FixedPosition = anim.EndPosition;
					_anims.Del(entity);
				}
			}
		}
	}
}