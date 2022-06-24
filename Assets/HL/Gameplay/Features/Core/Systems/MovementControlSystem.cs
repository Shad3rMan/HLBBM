using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;

namespace HL.Gameplay.Features.Core.Systems
{
	public class MovementControlSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<InputComponent> _inputPool;
		private EcsPool<Movement> _movePool;

		public void Init(EcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world.Filter<InputComponent>().Inc<Movement>().End();
			_inputPool = _world.GetPool<InputComponent>();
			_movePool = _world.GetPool<Movement>();
		}

		public void Run(EcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var input = _inputPool.Get(entity);
				ref var move = ref _movePool.Get(entity);

				move.Direction = input.Move;
			}
		}
	}
}