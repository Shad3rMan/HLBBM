using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Weapons.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Weapons.Systems
{
	public class ShootingSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<InputComponent> _inputPool;
		private EcsPool<Weapon> _weaponPool;
		private EcsPool<EmitBulletEvent> _emitPool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<InputComponent>().Inc<Weapon>().End();
			_inputPool = _world.GetPool<InputComponent>();
			_weaponPool = _world.GetPool<Weapon>();
			_emitPool = _world.GetPool<EmitBulletEvent>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var input = _inputPool.Get(entity);
				var weapon = _weaponPool.Get(entity);

				if (input.Shoot)
				{
					var emitPosition = weapon.ShootingRoot.position;
					ref var emit = ref _emitPool.Add(entity);
					emit.BulletView = weapon.BulletPrefab;
					emit.Count = 1;
					emit.Position = emitPosition;
					emit.Direction = (Camera.main.ScreenToWorldPoint(input.MousePosition) - emitPosition).normalized;
					emit.Speed = 2;
				}
			}
		}
	}
}