using Core.AI;

namespace HL.AI.Conditions
{
	public class PlayerInRange : Condition
	{
		public PlayerInRange(bool expectedValue) : base(expectedValue)
		{
		}

		protected override bool CheckInternal()
		{
			throw new System.NotImplementedException();
		}
	}
}