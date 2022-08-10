using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core.Systems
{
	public class RotationSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<TransformComponent> _transformsPool;
		private EcsPool<Rotation> _rotatePool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<TransformComponent>().Inc<Rotation>().End();
			_transformsPool = _world.GetPool<TransformComponent>();
			_rotatePool = _world.GetPool<Rotation>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var id in _filter)
			{
				ref var transform = ref _transformsPool.Get(id);
				ref var rotation = ref _rotatePool.Get(id);

				transform.Value.Rotate(rotation.Value);
			}
		}
	}
}