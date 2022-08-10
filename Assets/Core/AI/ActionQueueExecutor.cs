namespace Core.AI
{
	public class ActionQueueExecutor
	{
		private readonly IActionQueue _queue;
		private IExecutable _current;

		public ActionQueueExecutor()
		{
			_queue = new ActionQueue(128);
		}

		public void Start()
		{
			_current = _queue.GetNext();
		}

		public void Update(float deltaTime)
		{
			if (_current.Execute(deltaTime) == ExecutionStatus.Success)
			{
				_current = _queue.GetNext();
			}
		}
	}
}