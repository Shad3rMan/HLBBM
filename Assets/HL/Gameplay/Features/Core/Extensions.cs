using Leopotam.EcsLite;

namespace HL.Gameplay.Features.Core
{
	public static class Extensions
	{
		public static ref T NewEntityWithComponent<T>(this EcsWorld world, out int entity) where T : struct
		{
			entity = world.NewEntity();
			var tPool = world.GetPool<T>();
			return ref tPool.Add(entity);
		}
	}
}