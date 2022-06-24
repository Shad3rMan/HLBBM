using Core;
using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using HL.Gameplay.Features.LegsMovement.Components;
using HL.Gameplay.Features.Strider.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace HL.Gameplay.Features.Strider.Views
{
	public class StriderView : Actor<StriderComponent>
	{
		protected override void Initialize()
		{
			Add<Movement>().Speed = 0.1f;
			Add<InputComponent>();
		}
	}
}