using Core;
using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Core
{
	public abstract class Actor<T> : MonoBehaviour where T : struct
	{
		private readonly EcsWorld _world = WorldProvider.World;

		public int Entity { get; private set; }

		protected virtual void Awake()
		{
			Entity = WorldProvider.World.NewEntity();
			_world.GetPool<T>().Add(Entity);
			_world.GetPool<TransformComponent>().Add(Entity).Value = transform;
			Registry<Actor<T>>.Register(this);
			Initialize();
		}

		private void OnDestroy()
		{
			Registry<Actor<T>>.Remove(this);
		}

		protected virtual void Initialize()
		{}
		
		protected ref TC Add<TC>() where TC : struct
		{
			return ref _world.GetPool<TC>().Add(Entity);
		}

		protected ref TC Get<TC>() where TC : struct
		{
			return ref _world.GetPool<TC>().Get(Entity);
		}

		protected void Del<TC>() where TC : struct
		{
			_world.GetPool<TC>().Del(Entity);
		}
	}
}