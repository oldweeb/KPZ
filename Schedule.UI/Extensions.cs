using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Schedule.UI
{
    public static class Extensions
    {
        public static void RemoveIf<T>(this ObservableCollection<T> collection, Func<T, bool> predicate)
        {
            for (var i = 0; i < collection.Count; ++i)
            {
                var item = collection[i];
                if (predicate(item))
                {
                    collection.Remove(item);
                    --i;
                }
            }
        }

        public static void AddIf<T>(this ObservableCollection<T> collection, IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    collection.Add(item);
                }
            }
        }
    }
}
