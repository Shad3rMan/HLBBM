using HL.Gameplay.Features.Core;
using HL.Gameplay.Features.LegsMovement.Components;
using HL.Gameplay.Features.Strider.Components;
using UnityEngine;

namespace HL.Gameplay.Features.Strider.Views
{
	public class LegView : Actor<LegComponent>
	{
		[SerializeField]
		private Transform _root;
		[SerializeField]
		private float _stepDistance;
		[SerializeField]
		private float _stepHeight;
		[SerializeField]
		private Vector2 _offset;

		protected override void Initialize()
		{
			Add<StriderComponent>();
			ref var ll = ref Get<LegComponent>();
			ll.Leg = transform;
			ll.Root = _root;
			ll.ProjectedOffset = _offset;
			ll.FixedPosition = ll.Leg.position;
			ll.StepDistance = _stepDistance;
			ll.StepHeight = _stepHeight;
		}
	}
}