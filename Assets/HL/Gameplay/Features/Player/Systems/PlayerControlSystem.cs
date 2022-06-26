using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Player.Systems
{
	public class PlayerControlSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<Input> _inputPool;
		private EcsPool<Movement> _movePool;
		private EcsPool<Rotation> _rotatePool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Input>().Inc<Components.Player>().End();
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

				move.Value = input.Move;
				rotate.Value = new Vector3(0, input.MouseDelta.x, 0);
			}
		}
	}
}