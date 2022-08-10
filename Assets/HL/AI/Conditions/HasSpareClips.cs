using Core.AI;

namespace HL.AI.Conditions
{
	public class HasSpareClips : Condition
	{
		public HasSpareClips(bool expectedValue) : base(expectedValue)
		{
		}

		protected override bool CheckInternal()
		{
			throw new System.NotImplementedException();
		}
	}
}