using UnityEngine;

namespace Core.TFRBar
{
	internal class TFRBar : MonoBehaviour
	{
		private const float TARGET = 0.02f;

		private static readonly int Size = Shader.PropertyToID("_Size");

		private float[] _profile = new float[600];
		private float _lastFrameTime;
		private float _tfr;
		private float _size = 1;
		private int _lastFrameCount;

		private Material _material;

		private void OnEnable()
		{
			var shader = Shader.Find("Debug/TFR");
			_material = new Material(shader);
			_material.hideFlags = HideFlags.HideAndDontSave;
		}

		private void OnDisable()
		{
			if (ReferenceEquals(_material, null)) return;
			if (Application.isPlaying) Destroy(_material);
			else DestroyImmediate(_material);
		}

		private void LateUpdate()
		{
			var delta = Time.time - _lastFrameTime;
			_lastFrameTime = Time.time;
			_tfr = (delta - TARGET) / TARGET;
			_tfr = Mathf.Clamp01(_tfr);

			_size = 1 - 1f / Screen.height * 10;
			_material.SetFloat(Size, _size);
		}

		private void OnPostRender()
		{
			GL.PushMatrix();
			_material.SetPass(0);

			GL.LoadOrtho();

			var width = Mathf.Min(600, Screen.width);
			var widthArea = (float)width / Screen.width;

			var frameCount = Mathf.CeilToInt(Time.time / TARGET);
			var sCount = frameCount / width;
			var frameIndex = frameCount - sCount * width;
			var frameNormal = (float)frameIndex / width * widthArea;

			var missedFrames = frameCount - _lastFrameCount - 1;

			while (missedFrames > 0)
			{
				if (frameIndex - missedFrames > 0)
					_profile[frameIndex - missedFrames] = 1;
				missedFrames--;
			}

			_profile[frameIndex] = _tfr;

			var step = 1f / width * widthArea;
			var stephalf = step * 0.5f;

			GL.Begin(GL.LINES);

			for (int i = 0; i < width; i++)
			{
				GL.Vertex3(i * step + stephalf, _size + (1 - _size) * _profile[i], 0);
				GL.Vertex3(i * step + stephalf, _size + (1 - _size) * _profile[i] + 1, 0);
			}

			GL.End();

			_material.SetPass(1);

			GL.Begin(GL.QUADS);
			GL.Color(Color.black);
			GL.Vertex3(frameNormal, _size, 0);
			GL.Vertex3(frameNormal, 1f, 0);
			GL.Vertex3(frameNormal + step * 2, 1f, 0);
			GL.Vertex3(frameNormal + step * 2, _size, 0);
			GL.End();

			GL.PopMatrix();

			_lastFrameCount = frameCount;
		}
	}
}