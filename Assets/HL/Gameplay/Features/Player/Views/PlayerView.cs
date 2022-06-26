using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using CharacterController = HL.Gameplay.Features.Core.Components.CharacterController;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Player.Views
{
	public class PlayerView : Actor<Components.Player>
	{
		protected override void Initialize()
		{
			Add<CharacterController>().Value = GetComponent<UnityEngine.CharacterController>();
			Add<Input>();
			Add<Movement>();
			Add<Rotation>();
			Add<Gravity>().Value = -9.8f;
		}
	}
}