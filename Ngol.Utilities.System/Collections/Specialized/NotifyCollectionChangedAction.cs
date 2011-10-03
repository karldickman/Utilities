using System;

namespace Ngol.Utilities.Collections.Specialized
{
    /// <summary>
    /// Describes the action that caused an <see cref="INotifyCollectionChanged.CollectionChanged" />
    /// event.
    /// </summary>
    public enum NotifyCollectionChangedAction
    {
        /// <summary>
        /// One or more items were added to the collection.
        /// </summary>
        Add,

        /// <summary>
        /// One or more items were removed from the collection.
        /// </summary>
        Remove,

        /// <summary>
        /// One or more items were replaced in the collection.
        /// </summary>
        Replace,

        /// <summary>
        /// One or more items were moved within the collection.
        /// </summary>
        Move,

        /// <summary>
        /// The content of the collection changed dramatically.
        /// </summary>
        Reset
    }
}

