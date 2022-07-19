using Core.AI;
using HL.AI.Actions;
using HL.AI.Conditions;

namespace HL.AI.Objectives
{
	public class KillActorObjective : Objective
	{
		public KillActorObjective()
		{
			AddDecision(new PlayerInRange(true), new ShootAction());
		}
	}
}