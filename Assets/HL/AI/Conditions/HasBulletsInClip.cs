using Core.AI;

namespace HL.AI.Conditions
{
	public class HasBulletsInClip : Condition
	{
		public HasBulletsInClip(bool expectedValue) : base(expectedValue)
		{
		}

		protected override bool CheckInternal()
		{
			throw new System.NotImplementedException();
		}
	}
}