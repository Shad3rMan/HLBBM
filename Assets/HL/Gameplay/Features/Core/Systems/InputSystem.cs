using HL.Gameplay.Features.Core.Config;
using Leopotam.EcsLite;
using UnityEngine;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Core.Systems
{
	public class InputSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly InputConfig _config;
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<Input> _inputPool;

		public InputSystem(InputConfig config)
		{
			_config = config;
		}

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
				input.Move = new Vector3(UnityEngine.Input.GetAxis(_config.HorizontalAxis),
					UnityEngine.Input.GetAxis(_config.VerticalAxis));
				input.MouseDelta = new Vector2(UnityEngine.Input.GetAxis(_config.MouseX),
					UnityEngine.Input.GetAxis(_config.MouseY));
				input.MousePosition = UnityEngine.Input.mousePosition;
				input.Action = UnityEngine.Input.GetKeyDown(_config.ActionKey);
				input.Jump = UnityEngine.Input.GetKeyDown(_config.JumpKey);
				input.Shoot = UnityEngine.Input.GetKey(_config.FireKey);
				input.AltShoot = UnityEngine.Input.GetKey(_config.AlternateFireKey);
				input.Run = UnityEngine.Input.GetKey(_config.RunKey);
			}
		}
	}
}