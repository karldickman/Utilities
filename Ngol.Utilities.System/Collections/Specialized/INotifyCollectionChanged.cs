using System;
using System.ComponentModel;

namespace Ngol.Utilities.Collections.Specialized
{
    /// <summary>
    /// Notifies listeners of dynamic changes, such as when items get added and removed or the
    /// whole list is refereshed.
    /// </summary>
    public interface INotifyCollectionChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}

