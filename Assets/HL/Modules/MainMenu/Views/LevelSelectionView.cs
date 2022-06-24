using System;
using UnityEngine;

namespace HL.Modules.MainMenu.Views
{
	public class LevelSelectionView : MonoBehaviour
	{
		public event Action<int> Selected;
	}
}