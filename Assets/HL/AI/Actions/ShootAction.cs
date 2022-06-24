using Core.AI;
using UnityEngine;

namespace HL.AI.Actions
{
	public class ShootAction : Action, IActionQueueModifier
	{
		public override ExecutionStatus Execute(float deltaTime)
		{
			Debug.Log("Shooting");
			return ExecutionStatus.Success;
		}

		public void ModifyQueue(IActionQueue queue)
		{
			queue.Add(this);
		}
	}
}