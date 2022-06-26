using Leopotam.EcsLite;
using UnityEngine;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Core.Systems
{
	public class InputSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<Input> _inputPool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Input>().End();
			_inputPool = _world.GetPool<Input>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var input = ref _inputPool.Get(entity);
				input.Move = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
				input.MouseDelta = UnityEngine.Input.mousePosition - input.MousePosition;
				input.MousePosition = UnityEngine.Input.mousePosition;
				input.Action = UnityEngine.Input.GetKeyDown(KeyCode.E);
				input.Jump = UnityEngine.Input.GetKeyDown(KeyCode.Space);
				input.Shoot = UnityEngine.Input.GetMouseButton(0);
				input.AltShoot = UnityEngine.Input.GetMouseButton(1);
			}
		}
	}
}