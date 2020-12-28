using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EditorTools
{
	public static class ScriptableAssetCreator
	{
		[MenuItem("Assets/Create/Scriptable Asset", false)]
		private static void Create()
		{
			var instance = ScriptableObject.CreateInstance(Selection.activeObject.name);
			var path = Path.ChangeExtension(AssetDatabase.GetAssetPath(Selection.activeObject), "asset");

			AssetDatabase.CreateAsset(instance, path);
		}

		[MenuItem("Assets/Create/Scriptable Asset", true)]
		private static bool Validate()
		{
			if (Selection.activeObject == null)
			{
				return false;
			}

			var type = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(t => t.IsSubclassOf(typeof(ScriptableObject)))
				.Where(t => !t.IsAbstract && !t.IsInterface)
				.FirstOrDefault(t => t.Name == Selection.activeObject.name);

			return type != null;
		}
	}
}