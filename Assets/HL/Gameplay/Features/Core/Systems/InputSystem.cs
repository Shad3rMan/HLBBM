using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core.Systems
{
	public class InputSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<InputComponent> _inputPool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<InputComponent>().End();
			_inputPool = _world.GetPool<InputComponent>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var input = ref _inputPool.Get(entity);
				input.Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
				input.MousePosition = Input.mousePosition;
				input.Action = Input.GetKeyDown(KeyCode.E);
				input.Jump = Input.GetKeyDown(KeyCode.Space);
				input.Shoot = Input.GetKey(KeyCode.F);
			}
		}
	}
}