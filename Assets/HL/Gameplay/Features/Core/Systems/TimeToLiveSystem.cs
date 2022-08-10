using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core.Systems
{
	public class TimeToLiveSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<TimeToLive> _ttlPool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<TimeToLive>().End();
			_ttlPool = _world.GetPool<TimeToLive>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var ttl = ref _ttlPool.Get(entity);
				ttl.Elapsed -= Time.deltaTime;

				if (ttl.Elapsed < 0)
				{
					ttl.Done = true;
				}
			}
		}
	}
}