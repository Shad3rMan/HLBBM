using Core.AI;

namespace HL.AI.Conditions
{
	public class IsActorAlive : Condition
	{
		public IsActorAlive(bool expectedValue) : base(expectedValue)
		{
		}

		protected override bool CheckInternal()
		{
			throw new System.NotImplementedException();
		}
	}
}