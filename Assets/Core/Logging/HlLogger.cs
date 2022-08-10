using UnityEngine;

namespace Core.Logging
{
	public class HlLogger : ILogger
	{
		public void Log(LogType logType, string tag, string message)
		{
			Debug.unityLogger.Log(logType, tag, message);
		}
	}
}