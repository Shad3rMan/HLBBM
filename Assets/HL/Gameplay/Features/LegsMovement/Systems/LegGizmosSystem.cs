using HL.Gameplay.Features.LegsMovement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.LegsMovement.Systems
{
	public class LegGizmosSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<LegComponent> _legs;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<LegComponent>().End();
			_legs = _world.GetPool<LegComponent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var id in _filter)
			{
				ref var leg = ref _legs.Get(id);

				Gizmos.color = Color.cyan;
				Gizmos.DrawWireSphere(leg.Leg.position, 1f);
				Gizmos.color = Color.magenta;
				Gizmos.DrawWireSphere(leg.FixedPosition, leg.StepDistance);
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(leg.DesiredPosition, 1f);
				Gizmos.DrawLine(leg.FixedPosition, leg.DesiredPosition);
			}
		}
	}
}