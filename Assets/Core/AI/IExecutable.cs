namespace Core.AI
{
	public interface IExecutable
	{
		ExecutionStatus Execute(float deltaTime);
	}
}