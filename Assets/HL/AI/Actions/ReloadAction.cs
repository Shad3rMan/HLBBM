using Core.AI;
using UnityEngine;

namespace HL.AI.Actions
{
	public class ReloadAction : Action
	{
		public override ExecutionStatus Execute(float deltaTime)
		{
			Debug.Log("Reloading");
			return ExecutionStatus.Success;
		}
	}
}