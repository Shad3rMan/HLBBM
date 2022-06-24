using System;
using System.Diagnostics;
using HL.Gameplay.Features.Core.Systems;
using HL.Gameplay.Features.LegsMovement.Systems;
using HL.Gameplay.Weapons.Systems;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core
{
	public class GameController : MonoBehaviour
	{
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
			_systems.Add(new InputSystem());
			_systems.Add(new MovementControlSystem());
			_systems.Add(new StriderLegMovementSystem());
			_systems.Add(new LegAnimationSystem());
			_systems.Add(new MovementSystem());
			_systems.Add(new ShootingSystem());
			//_systems.Add(new EmitBulletSystem());
			_systems.Add(new TimeToLiveSystem());
			_systems.Add(new PoolBulletSystem());
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
			_stopwatch.Start();
			_systems.Run();
			_ms = _stopwatch.ElapsedMilliseconds;
			_stopwatch.Stop();
			_stopwatch.Reset();
		}

		private void OnDrawGizmos()
		{
			_gizmosSystems?.Run();
		}

		private void OnGUI()
		{
			GUILayout.Label(_ms.ToString());
		}

		private void OnDestroy()
		{
			_systems?.Destroy();
			_systems = null;
			WorldProvider.DestroyWorld();
		}
	}
}