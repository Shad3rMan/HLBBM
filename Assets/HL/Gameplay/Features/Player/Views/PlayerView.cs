using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Player.Views
{
	public class PlayerView : Actor<Components.Player>
	{
		[SerializeField]
		private Transform _aimBone;
		[SerializeField]
		private Transform _headBone;
		[SerializeField]
		private Transform _leftLeg;
		[SerializeField]
		private Transform _rightLeg;

		private static readonly EcsWorld _ecsWorld = WorldProvider.World;

		protected override void Initialize()
		{
			Add<Movement>().Speed = 0.1f;
			Add<InputComponent>();
		}
	}
}