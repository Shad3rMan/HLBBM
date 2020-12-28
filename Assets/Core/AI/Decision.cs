namespace Core.AI
{
	public class Decision : IActionQueueModifier
	{
		private readonly Condition _condition;
		private readonly IActionQueueModifier _itemIfTrue;

		public Decision(Condition condition, IActionQueueModifier itemIfTrue)
		{
			_condition = condition;
			_itemIfTrue = itemIfTrue;
		}

		public bool TryGetResult(out IActionQueueModifier result)
		{
			var checkResult = _condition.Check();

			result = checkResult ? _itemIfTrue : null;

			return checkResult;
		}

		public void ModifyQueue(IActionQueue queue)
		{
			if (_condition.Check())
			{
				_itemIfTrue.ModifyQueue(queue);
			}
		}
	}
}