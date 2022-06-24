using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core.Systems
{
	public class MovementSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<TransformComponent> _transformsPool;
		private EcsPool<Movement> _movePool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<TransformComponent>().Inc<Movement>().End();
			_transformsPool = _world.GetPool<TransformComponent>();
			_movePool = _world.GetPool<Movement>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var id in _filter)
			{
				ref var transform = ref _transformsPool.Get(id);
				ref var move = ref _movePool.Get(id);

				transform.Value.position += new Vector3(move.Direction.x, move.Direction.y) * move.Speed;
			}
		}
	}
}