using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Features.Player.Config;
using Leopotam.EcsLite;
using UnityEngine;
using CharacterController = HL.Gameplay.Features.Core.Components.CharacterController;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Player.Systems
{
	public class PlayerControlSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly PlayerConfig _config;
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<Input> _inputPool;
		private EcsPool<RigidbodyComponent> _rigidbodyPool;
		private EcsPool<Components.Player> _playerPool;

		public PlayerControlSystem(PlayerConfig config)
		{
			_config = config;
		}

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Input>().Inc<RigidbodyComponent>().Inc<Components.Player>().End();
			_inputPool = _world.GetPool<Input>();
			_rigidbodyPool = _world.GetPool<RigidbodyComponent>();
			_playerPool = _world.GetPool<Components.Player>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var input = _inputPool.Get(entity);
				ref var rb = ref _rigidbodyPool.Get(entity);
				ref var player = ref _playerPool.Get(entity);

				var speed = (input.Run ? _config.SprintSpeed : _config.MoveSpeed) * input.Move;
				rb.Value.AddForce(player.Head.rotation * new Vector3(speed.x, 0, speed.y), ForceMode.Force);
			}
		}
	}
}