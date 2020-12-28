using Cysharp.Threading.Tasks;

namespace Core.Commands
{
	public abstract class BaseCommand
	{
		public abstract void Execute();
	}
	
	public abstract class BaseCommand<T>
	{
		public abstract UniTask<T> Execute();
	}
}