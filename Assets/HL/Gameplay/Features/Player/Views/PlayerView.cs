using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Player.Views
{
	public class PlayerView : Actor<Components.Player>
	{
		protected override void Initialize()
		{
			Add<RigidbodyComponent>().Value = GetComponent<UnityEngine.Rigidbody>();
			Add<ColliderComponent>().Value = GetComponent<UnityEngine.Collider>();
			Add<Input>();
			//Add<Movement>();
			//Add<Rotation>();
			//Add<Gravity>().Value = -9.8f;
		}
	}
}