namespace Core.AI
{
	public interface IActionQueue
	{
		void Add(IExecutable executable);

		IExecutable GetNext();
	}
}