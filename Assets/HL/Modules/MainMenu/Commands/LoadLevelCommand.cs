using Core.Commands;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HL.Modules.MainMenu.Commands
{
	public class LoadLevelCommand : BaseCommand<SceneInstance>
	{
		private readonly string _levelName;

		public LoadLevelCommand(string levelName)
		{
			_levelName = levelName;
		}

		public override UniTask<SceneInstance> Execute()
		{
			return Addressables.LoadSceneAsync(_levelName, LoadSceneMode.Additive).Task.AsUniTask();
		}
	}
}