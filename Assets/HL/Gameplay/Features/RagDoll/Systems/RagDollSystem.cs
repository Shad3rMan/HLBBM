using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Features.RagDoll.Components;
using Leopotam.EcsLite;

namespace HL.Gameplay.Features.RagDoll.Systems
{
	public class RagDollSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<RagDollComponent> _ragDollPool;
		private EcsPool<RigidbodyComponent> _rigidbodyPool;
		private EcsPool<ColliderComponent> _colliderPool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<RigidbodyComponent>().Inc<ColliderComponent>()
				.Inc<RagDollComponent>().End();

			foreach (var entity in _filter)
			{
				ref var rd = ref _ragDollPool.Get(entity);
				var rb = _rigidbodyPool.Get(entity);
				var c = _colliderPool.Get(entity);

				if (!rd.Initialized)
				{
					rb.Value.isKinematic = false;
					c.Value.enabled = false;
				}
			}
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var rd = ref _ragDollPool.Get(entity);
				if (!rd.Initialized)
				{
					var rb = _rigidbodyPool.Get(entity);
					var c = _colliderPool.Get(entity);
					rb.Value.isKinematic = true;
					c.Value.enabled = true;
					rd.Initialized = true;
				}
			}
		}
	}
}