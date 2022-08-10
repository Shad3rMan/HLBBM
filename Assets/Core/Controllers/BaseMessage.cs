namespace Core.Controllers
{
	public class BaseMessage
	{
		public bool Processed { get; private set; }

		public void Process()
		{
			Processed = true;
		}
	}
}