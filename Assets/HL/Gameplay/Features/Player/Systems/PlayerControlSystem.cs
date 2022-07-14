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
		private EcsPool<Movement> _movePool;
		private EcsPool<Rotation> _rotatePool;

		public PlayerControlSystem(PlayerConfig config)
		{
			_config = config;
		}

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Input>().Inc<CharacterController>().Inc<Components.Player>().End();
			_inputPool = _world.GetPool<Input>();
			_movePool = _world.GetPool<Movement>();
			_rotatePool = _world.GetPool<Rotation>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var input = _inputPool.Get(entity);
				ref var move = ref _movePool.Get(entity);
				ref var rotate = ref _rotatePool.Get(entity);

				var speed = (input.Run ? _config.SprintSpeed : _config.MoveSpeed) * input.Move;
				move.Value = new Vector3(speed.x, move.Value.y, speed.y);
				rotate.Value = new Vector3(0, input.MouseDelta.x * _config.HorizontalSensitivity, 0);
			}
		}
	}
}