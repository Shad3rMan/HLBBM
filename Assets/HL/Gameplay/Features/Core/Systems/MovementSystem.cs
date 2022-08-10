using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;
using CharacterController = HL.Gameplay.Features.Core.Components.CharacterController;

namespace HL.Gameplay.Features.Core.Systems
{
	public class MovementSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<TransformComponent> _transformsPool;
		private EcsPool<Movement> _movePool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<TransformComponent>().Inc<Movement>().Exc<CharacterController>().End();
			_transformsPool = _world.GetPool<TransformComponent>();
			_movePool = _world.GetPool<Movement>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var id in _filter)
			{
				ref var transform = ref _transformsPool.Get(id);
				ref var move = ref _movePool.Get(id);

				transform.Value.Translate(move.Value);
			}
		}
	}
}