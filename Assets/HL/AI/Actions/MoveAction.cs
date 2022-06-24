using Core.AI;
using UnityEngine;

namespace HL.AI.Actions
{
	public class MoveAction : Action
	{
		private readonly Transform _transform;
		private readonly Vector3 _position;
		private readonly float _speed;
		private readonly Vector3 _startPosition;

		private float _progress;

		public MoveAction(Transform transform, Vector3 position, float speed)
		{
			_transform = transform;
			_position = position;
			_speed = speed;
			_startPosition = _transform.position;
		}

		public override ExecutionStatus Execute(float deltaTime)
		{
			_progress += _speed * deltaTime;
			if (_progress < 1)
			{
				_transform.position = Vector3.Lerp(_startPosition, _position, _progress);
				return ExecutionStatus.Running;
			}
			else
			{
				_transform.position = _position;
				return ExecutionStatus.Success;
			}
		}
	}
}