using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.Core.Components;
using UnityEngine;
using Input = HL.Gameplay.Features.Core.Components.Input;

namespace HL.Gameplay.Features.Player.Views
{
	public class PlayerView : Actor<Components.Player>
	{
		[SerializeField]
		private Transform _head;
		
		protected override void Initialize()
		{
			Get<Components.Player>().Head = _head;
			Add<RigidbodyComponent>().Value = GetComponent<UnityEngine.Rigidbody>();
			Add<ColliderComponent>().Value = GetComponent<UnityEngine.Collider>();
			Add<Input>();
			//Add<Movement>();
			//Add<Rotation>();
			//Add<Gravity>().Value = -9.8f;
		}
	}
}