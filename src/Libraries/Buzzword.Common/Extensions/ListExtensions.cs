using System;
using System.Collections.Generic;

namespace Buzzword.Common.Extensions
{
    public static class ListExtensions
    {
        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsNotEmpty<T>(this IList<T> list)
        {
            return !IsEmpty(list);
        }

        public static void DisposeItems<T>(this IEnumerable<T> items) where T : IDisposable
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }
        }
    }
}
