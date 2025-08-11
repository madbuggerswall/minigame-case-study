using System.Collections.Generic;

namespace Utilities.Collections.Generic {
	public class UniqueList<T> {
		private readonly List<T> items;            // Contiguous storage
		private readonly Dictionary<T, int> index; // Maps item -> index in List

		public int Count => items.Count;
		public T this[int i] => items[i];

		public UniqueList(int capacity = 4) {
			items = new List<T>(capacity);
			index = new Dictionary<T, int>(capacity);
		}

		/// <summary> Adds a unique item. Returns true if added, false if already present. </summary>
		public bool Add(T value) {
			if (index.ContainsKey(value))
				return false;

			index[value] = items.Count;
			items.Add(value);
			return true;
		}

		/// <summary> Removes the item in O(1) time without preserving order. </summary>
		public bool Remove(T value) {
			if (!index.TryGetValue(value, out int removeIndex))
				return false;

			int lastIndex = items.Count - 1;
			T lastItem = items[lastIndex];

			// Swap with last if not already last
			if (removeIndex != lastIndex) {
				items[removeIndex] = lastItem;
				index[lastItem] = removeIndex;
			}

			// Remove last
			items.RemoveAt(lastIndex);
			index.Remove(value);

			return true;
		}

		/// <summary> Checks if the item exists. </summary>
		public bool Contains(T value) => index.ContainsKey(value);

		/// <summary> Returns the index of an item, or -1 if not found. </summary>
		public int IndexOf(T value) => index.GetValueOrDefault(value, -1);

		/// <summary> Returns the underlying list (read-only). </summary>
		public IReadOnlyList<T> AsReadOnly() => items;

		/// <summary> Clears all items. </summary>
		public void Clear() {
			items.Clear();
			index.Clear();
		}
	}
}
