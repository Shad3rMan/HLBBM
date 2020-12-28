using UnityEngine;

namespace Core.AI
{
	public abstract class Action : IExecutable
	{
		public abstract ExecutionStatus Execute(float deltaTime);
	}
}