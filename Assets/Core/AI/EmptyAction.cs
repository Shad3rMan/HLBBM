namespace Core.AI
{
	public class EmptyAction : Action
	{
		private readonly ExecutionStatus _status;

		public EmptyAction(ExecutionStatus status)
		{
			_status = status;
		}
		
		public override ExecutionStatus Execute(float deltaTime)
		{
			return _status;
		}
	}
}