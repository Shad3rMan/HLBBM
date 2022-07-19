using HL.Gameplay.Features.LegsMovement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.LegsMovement.Systems
{
	public class StriderLegMovementSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<LegComponent> _legs;
		private EcsPool<LegAnimationComponent> _anims;
		private RaycastHit2D[] _hitResults;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<LegComponent>().Exc<LegAnimationComponent>().End();
			_legs = _world.GetPool<LegComponent>();
			_anims = _world.GetPool<LegAnimationComponent>();
			_hitResults = new RaycastHit2D[1];
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var leg = ref _legs.Get(entity);
				var rootPosition = leg.Root.position;
				leg.DesiredPosition = rootPosition + leg.ProjectedOffset;
				var dir = (leg.DesiredPosition - leg.FixedPosition).normalized;
				leg.DesiredPosition += dir * (leg.StepDistance / 2);
				if (Physics2D.RaycastNonAlloc(leg.DesiredPosition + Vector3.up * 10, Vector2.down, _hitResults, 20) > 0)
				{
					leg.DesiredPosition = _hitResults[0].point;
				}

				if (Vector3.Distance(leg.FixedPosition, leg.DesiredPosition) >= leg.StepDistance)
				{
					ref var anim = ref _anims.Add(entity);
					anim.StartPosition = leg.FixedPosition;
					anim.EndPosition = leg.DesiredPosition;
					anim.Duration = 0.15f;
					anim.Time = 0;
				}
				else
				{
					leg.Leg.position = leg.FixedPosition;
				}
			}
		}
	}
}