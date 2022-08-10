using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core.Systems
{
	public class GravitySystem : IEcsRunSystem, IEcsInitSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<Movement> _movePool;
		private EcsPool<Gravity> _gravityPool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Movement>().Inc<Gravity>().End();
			_movePool = _world.GetPool<Movement>();
			_gravityPool = _world.GetPool<Gravity>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var move = ref _movePool.Get(entity);
				var gravity = _gravityPool.Get(entity);

				move.Value += Vector3.up * gravity.Value;
			}
		}
	}
}