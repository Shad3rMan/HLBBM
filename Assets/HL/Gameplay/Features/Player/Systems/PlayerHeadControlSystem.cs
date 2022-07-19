using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Features.Player.Components;
using HL.Gameplay.Features.Player.Config;
using Leopotam.EcsLite;
using UnityEngine;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Player.Systems
{
	public class PlayerHeadControlSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly PlayerConfig _config;
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<TransformComponent> _trPool;
		private EcsPool<Input> _inputPool;

		public PlayerHeadControlSystem(PlayerConfig config)
		{
			_config = config;
		}

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<Components.Player>().Inc<Head>().End(1);
			_trPool = _world.GetPool<TransformComponent>();
			_inputPool = _world.GetPool<Input>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var tr = ref _trPool.Get(entity);
				var input = _inputPool.Get(entity);

				tr.Value.Rotate(tr.Value.right, -input.MouseDelta.y * _config.VerticalSensitivity, Space.World);
			}
		}
	}
}