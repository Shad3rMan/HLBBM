using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Rendering
{
	[CreateAssetMenu(menuName = "Rendering/Fast Pipeline Asset")]
	public class FastPipelineAsset : RenderPipelineAsset
	{
		[SerializeField]
		private bool _dynamicBatching;
		[SerializeField]
		private bool _instancing;
		[SerializeField]
		private bool _srpBatching;

		protected override RenderPipeline CreatePipeline()
		{
			return new FastPipeline(_dynamicBatching, _instancing, _srpBatching);
		}
	}
}