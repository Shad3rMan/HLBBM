using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;
using CharacterController = HL.Gameplay.Features.Core.Components.CharacterController;

namespace HL.Gameplay.Features.Player.Systems
{
	public class CharacterMovementSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<Movement> _movePool;
		private EcsPool<CharacterController> _charPool;
		private EcsPool<TransformComponent> _trPool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<CharacterController>().Inc<Movement>().End(4);
			_charPool = _world.GetPool<CharacterController>();
			_movePool = _world.GetPool<Movement>();
			_trPool = _world.GetPool<TransformComponent>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var ch = _charPool.Get(entity);
				var move = _movePool.Get(entity);
				var tr = _trPool.Get(entity);

				if (ch.Value.isGrounded)
				{
					move.Value.y = 0;
				}

				var vector = tr.Value.forward * move.Value.z + tr.Value.up * move.Value.y +
				             tr.Value.right * move.Value.x;

				ch.Value.Move(vector * UnityEngine.Time.deltaTime);
			}
		}
	}
}