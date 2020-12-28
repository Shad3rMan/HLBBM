using System.Collections.Generic;

namespace Core.AI
{
	public class Brain
	{
		private readonly IList<Objective> _objectives;

		public Brain()
		{
			_objectives = new List<Objective>();
		}

		public void AddObjective(Objective objective)
		{
			if (!_objectives.Contains(objective))
			{
				_objectives.Add(objective);
			}
		}

		public void RemoveObjective(Objective objective)
		{
			if (_objectives.Contains(objective))
			{
				_objectives.Remove(objective);
			}
		}

		public void UpdateActionQueue(IActionQueue actionQueue)
		{
			foreach (var objective in _objectives)
			{
				objective.ModifyQueue(actionQueue);
			}
		}
	}
}