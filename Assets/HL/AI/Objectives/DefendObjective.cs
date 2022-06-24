using Core.AI;
using HL.AI.Conditions;

namespace HL.AI.Objectives
{
	public class DefendObjective : Objective
	{
		public DefendObjective()
		{
			AddDecision(new PlayerInRange(true), new KillActorObjective());
		}
	}
}