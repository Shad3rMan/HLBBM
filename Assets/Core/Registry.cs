using System.Collections.Generic;

namespace Core
{
	public static class Registry<T>
	{
		private const int MaxItems = 128;
		private static readonly List<T> _items = new List<T>(MaxItems);

		public static void Register(T item)
		{
			_items.Add(item);
		}

		public static IEnumerable<T> GetAll()
		{
			return _items;
		}

		public static void Remove(T item)
		{
			if (_items.Contains(item))
			{
				_items.Remove(item);
			}
		}
	}
}