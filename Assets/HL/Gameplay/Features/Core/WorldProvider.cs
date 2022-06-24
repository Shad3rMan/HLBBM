using Leopotam.EcsLite;

namespace HL.Gameplay.Features.Core
{
	public static class WorldProvider
	{
		public static EcsWorld World { get; private set; }

		static WorldProvider()
		{
			World = new EcsWorld();
		}

		public static void DestroyWorld()
		{
			World?.Destroy();
			World = null;
		}
	}
}