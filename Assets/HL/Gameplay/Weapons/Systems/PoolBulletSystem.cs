using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Weapons.Components;
using Leopotam.EcsLite;

namespace HL.Gameplay.Weapons.Systems
{
	public class PoolBulletSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<TimeToLive> _ttlPool;
		private EcsPool<InPool> _inPoolPool;
		private EcsPool<Movement> _movePool;
		private EcsPool<Bullet> _bulletPool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Bullet>().Inc<TimeToLive>().Exc<InPool>().End();
			_ttlPool = _world.GetPool<TimeToLive>();
			_inPoolPool = _world.GetPool<InPool>();
			_movePool = _world.GetPool<Movement>();
			_bulletPool = _world.GetPool<Bullet>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var ttl = _ttlPool.Get(entity);

				if (ttl.Done)
				{
					_inPoolPool.Add(entity);
					_ttlPool.Del(entity);
					_movePool.Del(entity);
					_bulletPool.Get(entity).View.gameObject.SetActive(false);
				}
			}
		}
	}
}