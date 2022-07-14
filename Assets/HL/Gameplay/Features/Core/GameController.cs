using System.Diagnostics;
using HL.Gameplay.Features.Core.Config;
using HL.Gameplay.Features.Core.Systems;
using HL.Gameplay.Features.LegsMovement.Systems;
using HL.Gameplay.Features.Player.Config;
using HL.Gameplay.Features.Player.Systems;
using HL.Gameplay.Weapons.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Profiling;

namespace HL.Gameplay.Features.Core
{
	public class GameController : MonoBehaviour
	{
		[SerializeField]
		private InputConfig _inputConfig;
		[SerializeField]
		private PlayerConfig _playerConfig;
	
		private EcsSystems _systems;
		private EcsSystems _gizmosSystems;
		private EcsWorld _world;
		private Stopwatch _stopwatch;
		private long _ms;

		private void Awake()
		{
			_stopwatch = new Stopwatch();
			_world = WorldProvider.World;
			_systems = new EcsSystems(_world);
			_systems.Add(new InputSystem(_inputConfig));
			_systems.Add(new PlayerControlSystem(_playerConfig));
			_systems.Add(new LegAnimationSystem());
			_systems.Add(new MovementSystem());
			_systems.Add(new RotationSystem());
			_systems.Add(new ShootingSystem());
			_systems.Add(new GravitySystem());
			_systems.Add(new CharacterMovementSystem());
			//_systems.Add(new EmitBulletSystem());
			_systems.Add(new TimeToLiveSystem());
			_systems.Add(new PoolBulletSystem());
			_systems.Add(new PlayerHeadControlSystem());
#if UNITY_EDITOR
			_systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
			_systems.Init();
			_gizmosSystems = new EcsSystems(_world);
			_gizmosSystems.Add(new LegGizmosSystem());
			_gizmosSystems.Init();
		}

		private void Update()
		{
			Profiler.BeginSample("ECS Loop", this);
			_stopwatch.Start();
			_systems.Run();
			_ms = _stopwatch.ElapsedMilliseconds;
			_stopwatch.Stop();
			_stopwatch.Reset();
			Profiler.EndSample();
		}

		private void OnDrawGizmos()
		{
			_gizmosSystems?.Run();
		}

		private void OnGUI()
		{
			GUILayout.Label((_ms * 1000).ToString("F"));
		}

		private void OnDestroy()
		{
			_systems?.Destroy();
			_systems = null;
			WorldProvider.DestroyWorld();
		}
	}
}