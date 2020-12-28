using System.Collections.Generic;
using System.Linq;

namespace Core.AI
{
	public class ActionQueue : IActionQueue
	{
		private readonly List<IExecutable> _executables;

		public ActionQueue(int capacity)
		{
			_executables = new List<IExecutable>(capacity);
		}
		
		public void Add(IExecutable executable)
		{
			_executables.Add(executable);
		}

		public IExecutable GetNext()
		{
			return _executables.Last();
		}
	}
}