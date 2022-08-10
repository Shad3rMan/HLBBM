using System.Collections.Generic;

namespace Core.AI
{
	public abstract class Objective : IActionQueueModifier
	{
		private readonly List<Decision> _decisions;

		protected Objective()
		{
			_decisions = new List<Decision>();
		}

		public void ModifyQueue(IActionQueue queue)
		{
			foreach (var decision in _decisions)
			{
				if (decision.TryGetResult(out var result))
				{
					result.ModifyQueue(queue);
				}
			}
		}

		protected void AddDecision(Condition condition, IActionQueueModifier itemIfTrue)
		{
			_decisions.Add(new Decision(condition, itemIfTrue));
		}
	}
}