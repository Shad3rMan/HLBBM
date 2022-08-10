using Core.Controllers;

namespace HL.Modules.MainMenu.Messages
{
	public class LevelSelectedMessage : BaseMessage
	{
		public string Level { get; }

		public LevelSelectedMessage(string level)
		{
			Level = level;
		}
	}
}