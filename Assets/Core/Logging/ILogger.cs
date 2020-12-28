using UnityEngine;

namespace Core.Logging
{
	public interface ILogger
	{
		void Log(LogType logType, string tag, string message);
	}
}