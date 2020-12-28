using UnityEngine;

namespace Core.AI
{
	public class AIBehaviour : MonoBehaviour
	{
		protected ActionQueueExecutor Executor;

		public void SetExecutor(ActionQueueExecutor executor)
		{
			Executor = executor;
		}

		private void Start()
		{
			Executor.Start();
		}

		private void Update()
		{
			Executor.Update(Time.deltaTime);
		}
	}
}