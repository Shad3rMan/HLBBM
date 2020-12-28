namespace Core.AI
{
	public interface IActionQueueModifier
	{
		void ModifyQueue(IActionQueue queue);
	}
}