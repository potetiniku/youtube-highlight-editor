using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YougeneHighlightEditor.Model;
public static class ObservableCollectionExtensions
{
	public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
	{
		T[] sorted = collection.OrderBy(x => x).ToArray();
		for (int i = 0; i < sorted.Length; i++)
		{
			collection.Move(collection.IndexOf(sorted[i]), i);
		}
	}
}
