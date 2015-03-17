using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq {
    public static class Extensions {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action) {
            foreach (T item in enumeration) {
                action(item);
                yield return item;
            }
        }
    }
}
