using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Rendering
{
	public class FastPipeline : RenderPipeline
	{
		private readonly bool _dynamicBatching;
		private readonly bool _instancing;
		private readonly FastPipelineAsset _asset;
		private static ShaderTagId _shaderTagId = new("Basic");
		private const string BufferName = "Render Camera";
		private static readonly int _worldSpaceCameraPosId = Shader.PropertyToID("_WorldSpaceCameraPos");
		private static int _dirLightColorId = Shader.PropertyToID("_DirectionalLightColor");
		static int _dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");
		static int _dirShadowAtlasId = Shader.PropertyToID("_DirectionalShadowAtlas");
		private readonly CommandBuffer _cameraBuffer;
		private ScriptableRenderContext _context;
		private Camera[] _cameras;
		private CullingResults _cullingResults;

		public FastPipeline(bool dynamicBatching, bool instancing, bool srpBatching)
		{
			_dynamicBatching = dynamicBatching;
			_instancing = instancing;
			GraphicsSettings.useScriptableRenderPipelineBatching = srpBatching;
			_cameraBuffer = new CommandBuffer()
			{
				name = BufferName
			};
		}

		protected override void Render(ScriptableRenderContext context, Camera[] cameras)
		{
			_cameras = cameras;
			_context = context;

			foreach (var camera in _cameras)
			{
				if (!Cull(camera))
				{
					return;
				}

				Setup(camera);
				var light = RenderSettings.sun;
				_cameraBuffer.SetGlobalVector(_dirLightColorId, light.color.linear);
				_cameraBuffer.SetGlobalVector(_dirLightDirectionId, -light.transform.forward);
				Draw(camera);
				_cameraBuffer.GetTemporaryRT(_dirShadowAtlasId, 1024, 1024);
				DrawGizmos(camera);
				Submit();
			}
		}

		private void Draw(Camera camera)
		{
			var sortingSettings = new SortingSettings(camera) { criteria = SortingCriteria.CommonOpaque };
			var drawingSettings = new DrawingSettings(_shaderTagId, sortingSettings)
			{
				enableDynamicBatching = _dynamicBatching,
				enableInstancing = _instancing
			};

			var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

			_context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
			_context.DrawSkybox(camera);

			sortingSettings.criteria = SortingCriteria.CommonTransparent;
			drawingSettings.sortingSettings = sortingSettings;
			filteringSettings.renderQueueRange = RenderQueueRange.transparent;

			_context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings
			);
		}

		private void Setup(Camera camera)
		{
			_context.SetupCameraProperties(camera);
			_cameraBuffer.ClearRenderTarget(
				camera.clearFlags.HasFlag(CameraClearFlags.Color),
				camera.clearFlags.HasFlag(CameraClearFlags.Depth),
				camera.backgroundColor
			);
			_cameraBuffer.BeginSample(BufferName);
			ExecuteBuffer();
		}

		private void Submit()
		{
			_cameraBuffer.EndSample(BufferName);
			ExecuteBuffer();
			_context.Submit();
		}

		private void ExecuteBuffer()
		{
			_context.ExecuteCommandBuffer(_cameraBuffer);
			_cameraBuffer.Clear();
		}

		private bool Cull(Camera camera)
		{
			if (camera.TryGetCullingParameters(out var p))
			{
				_cullingResults = _context.Cull(ref p);
				p.shadowDistance = Mathf.Min(100, camera.farClipPlane);
				return true;
			}

			return false;
		}

#if UNITY_EDITOR
		private void DrawGizmos(Camera camera)
		{
			if (Handles.ShouldRenderGizmos())
			{
				_context.DrawGizmos(camera, GizmoSubset.PreImageEffects);
				_context.DrawGizmos(camera, GizmoSubset.PostImageEffects);
			}
		}
#endif
	}
}