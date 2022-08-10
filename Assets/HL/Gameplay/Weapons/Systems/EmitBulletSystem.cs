using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Weapons.Components;
using HL.Gameplay.Weapons.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Weapons.Systems
{
	public class EmitBulletSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _emitFilter;
		private EcsFilter _pooledFilter;
		private EcsFilter _weaponFilter;
		private EcsPool<InPool> _inPoolPool;
		private EcsPool<Weapon> _weaponPool;
		private EcsPool<EmitBulletEvent> _emitPool;
		private EcsPool<Bullet> _bulletsPool;
		private EcsPool<Movement> _movePool;
		private EcsPool<TransformComponent> _transformPool;
		private EcsPool<TimeToLive> _ttlPool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_emitFilter = _world.Filter<EmitBulletEvent>().End();
			_pooledFilter = _world.Filter<Bullet>().Inc<InPool>().End();
			_weaponFilter = _world.Filter<Weapon>().End();
			_inPoolPool = _world.GetPool<InPool>();
			_weaponPool = _world.GetPool<Weapon>();
			_emitPool = _world.GetPool<EmitBulletEvent>();
			_movePool = _world.GetPool<Movement>();
			_bulletsPool = _world.GetPool<Bullet>();
			_transformPool = _world.GetPool<TransformComponent>();
			_ttlPool = _world.GetPool<TimeToLive>();

			foreach (var entity in _weaponFilter)
			{
				var wep = _weaponPool.Get(entity);
				for (var i = 0; i < 10000; i++)
				{
					var bulletView = Object.Instantiate(wep.BulletPrefab);
					bulletView.gameObject.SetActive(false);
					bulletView.gameObject.hideFlags = HideFlags.HideInHierarchy;
					_inPoolPool.Add(bulletView.Entity);
				}
			}
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _emitFilter)
			{
				var emit = _emitPool.Get(entity);
				var pooledCount = _pooledFilter.GetEntitiesCount();
				var required = emit.Count;
				var instantiateCount = emit.Count - pooledCount;
				if (instantiateCount > 0)
				{
				}

				foreach (var pooledBulletEntity in _pooledFilter)
				{
					ref var bullet = ref _bulletsPool.Get(pooledBulletEntity);
					_inPoolPool.Del(pooledBulletEntity);
					ref var tr = ref _world.GetPool<TransformComponent>().Get(pooledBulletEntity);
					tr.Value.position = emit.Position;
					bullet.View.gameObject.SetActive(true);
					ref var move = ref _movePool.Add(pooledBulletEntity);
					move.Value = emit.Direction * emit.Speed;
					ref var ttl = ref _ttlPool.Add(pooledBulletEntity);
					ttl.Done = false;
					ttl.Elapsed = 1;

					required--;

					if (required == 0) break;
				}

				_emitPool.Del(entity);
			}
		}
	}
}