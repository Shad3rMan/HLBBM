using UnityEngine;

namespace HL.Gameplay.Features.LegsMovement.Components
{
	public struct LegComponent
	{
		public Transform Leg;
		public Transform Root;
		public Vector3 FixedPosition;
		public Vector3 ProjectedOffset;
		public Vector3 DesiredPosition;
		public float StepDistance;
		public float StepHeight;
	}
}