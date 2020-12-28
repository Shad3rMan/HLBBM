namespace Core.AI
{
	public abstract class Condition
	{
		private readonly bool _expectedValue;

		protected Condition(bool expectedValue)
		{
			_expectedValue = expectedValue;
		}

		public bool Check()
		{
			return CheckInternal() == _expectedValue;
		}
		
		protected abstract bool CheckInternal();
	}
}