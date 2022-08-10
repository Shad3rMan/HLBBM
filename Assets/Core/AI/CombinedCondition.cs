using System;
using System.Linq;

namespace Core.AI
{
	public sealed class CombinedCondition : Condition
	{
		public enum CombinationType
		{
			All,
			Any
		}

		private readonly CombinationType _type;
		private readonly Condition[] _conditions;

		public CombinedCondition(CombinationType type, params Condition[] conditions) : base(
			true)
		{
			_type = type;
			_conditions = conditions;
		}

		protected override bool CheckInternal()
		{
			switch (_type)
			{
				case CombinationType.All:
					return _conditions.All(check => Check());
				case CombinationType.Any:
					return _conditions.Any(check => Check());
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}