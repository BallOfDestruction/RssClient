#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Shared.Extensions
{
    public static class ObservableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col) => new ObservableCollection<T>(col.NotNull());

        public static ReadOnlyObservableCollection<T> ToReadonlyObservableCollection<T>(this ObservableCollection<T> col) =>
            new ReadOnlyObservableCollection<T>(col.NotNull());
    }
}
