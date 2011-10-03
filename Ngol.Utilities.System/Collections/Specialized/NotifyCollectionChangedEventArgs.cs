using System;
using System.Collections;

namespace Ngol.Utilities.Collections.Specialized
{
    /// <summary>
    /// Provides data for the <see cref="INotifyCollectionChanged.CollectionChanged" /> event.
    /// </summary>
    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the action that caused the event.
        /// </summary>
        /// <value>
        /// A <see cref="NotifyCollectionChangedAction" /> value that describes teh action that caused the event.
        /// </value>
        public NotifyCollectionChangedAction Action
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the list of new items involved in the change.
        /// </summary>
        public IList NewItems
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the index at which the change occurred.
        /// </summary>
        /// <value>
        /// The zero-based index at which the change occurred.
        /// </value>
        public int NewStartingIndex
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the list of items affected by a replace, remove, or move action.
        /// </summary>
        public IList OldItems
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the index at which a move, remove, or replace action occurred.
        /// </summary>
        public int OldStartingIndex
        {
            get;
            protected set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="NotifyCollectionChangedEventArgs" /> that describes
        /// a reset change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event. This must be set to reset.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Reset" />.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            if(action != NotifyCollectionChangedAction.Reset)
                throw new ArgumentException("Parameter action must be Reset.");
            Action = action;
        }

        /// <summary>
        /// Initializes a new <see cref="NotifyCollectionChangedEventArgs" /> that describes
        /// a multi-item change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event. This can be reset, add, or remove.
        /// </param>
        /// <param name="changedItems">
        /// The items that are affected by the change.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Reset" />,
        /// <see cref="NotifyCollectionChangedAction.Add" />, or <see cref="NotifyCollectionChangedAction.Remove" />.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
        {
            Action = action;
            switch(Action)
            {
                case NotifyCollectionChangedAction.Add:
                    NewItems = changedItems;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OldItems = changedItems;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("Parameter action must be Reset, Add, or Remove.");
            }
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a one-item change.
        /// </summary>
        /// <param name="action">
        /// The ation that caused the event. This can be set to reset, add, or remove.
        /// </param>
        /// <param name="changedItem">
        /// The item that is affected by the change.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if:
        /// <list type="bullet">
        /// <item>
        /// <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Reset" />,
        /// <see cref="NotifyCollectionChangedAction.Add" />, or <see cref="NotifyCollectionChangedAction.Remove" />.
        /// </item>
        /// <item>
        /// <paramref name="action"/> is <see cref="NotifyCollectionChangedAction.Reset" /> and
        /// <paramref name="changedItem"/> is not <see langword="null" />.
        /// </item>
        /// </list>
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
        {
            Action = action;
            switch(action)
            {
                case NotifyCollectionChangedAction.Add:
                    NewItems = new ArrayList { changedItem, };
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OldItems = new ArrayList { changedItem, };
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if(changedItem != null)
                        throw new ArgumentException("Parameter changedItem must be null if parameter action is Reset.");
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("Parameter action must be one of Reset, Add, Remove.");
            }
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a multi-item replace change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  Must be replace.
        /// </param>
        /// <param name="newItems">
        /// The new items that are replacing the original items.
        /// </param>
        /// <param name="oldItems">
        /// The original items that are being replaced.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thronw if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Replace" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="newItems"/> or <paramref name="oldItems"/> is <see langword="null" />.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            if(action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException("Parameter action must be Replace.");
            if(newItems == null)
                throw new ArgumentException("newItems");
            if(oldItems == null)
                throw new ArgumentException("oldItems");
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a multi-item change or a reset change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  This can be reset, add, or remove.
        /// </param>
        /// <param name="changedItems">
        /// The items affected by the change.
        /// </param>
        /// <param name="startingIndex">
        /// The index where the change occurred.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// <paramref name="action"/> is not one of <see cref="NotifyCollectionChangedAction.Add" />,
        /// <see cref="NotifyCollectionChangedAction.Remove" />, or <see cref="NotifyCollectionChangedAction.Reset" />.
        /// </item>
        /// <item>
        /// <paramref name="action"/> is <see cref="NotifyCollectionChangedAction.Reset" />
        /// and either <paramref name="changedItems"/> is not <see langword="null" />
        /// or <paramref name="startingIndex "/> is not -1.
        /// </item>
        /// <item>
        /// <paramref name="action"/> is <see cref="NotifyCollectionChangedAction.Add" /> or <see cref="NotifyCollectionChangedAction.Remove" />
        /// and <paramref name="startingIndex"/> is less than -1.
        /// </item>
        /// </list>
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            Action = action;
            switch(action)
            {
                case NotifyCollectionChangedAction.Add:
                    if(startingIndex < -1)
                        throw new ArgumentException("When parameter action is Add parameter startIndex must be no less than -1.");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if(startingIndex < -1)
                        throw new ArgumentException("When parameter action is Remove parameter startIndex must be no less than -1.");
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if(changedItems != null || startingIndex != -1)
                        throw new ArgumentException("When parameter action is Reset, then either parameter changedItems must be null or parameter startingIndex must be -1.");
                    break;
                default:
                    throw new ArgumentException("Parameter action must be one of Reset, Add, Remove.");
            }
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a one-item change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  This can be set to reset, add, or remove.
        /// </param>
        /// <param name="changedItem">
        /// The item that is affected by the change.
        /// </param>
        /// <param name="index">
        /// The index where the change occurred.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Reset" />,
        /// <see cref="NotifyCollectionChangedAction.Add" />, or <see cref="NotifyCollectionChangedAction.Remove" />.
        /// </item>
        /// <item>
        /// <paramref name="action"/> is <see cref="NotifyCollectionChangedAction.Reset" /> and either
        /// <paramref name="changedItems"/> is not <see langword="null" /> or <paramref name="index"/>
        /// is not -1.
        /// </item>
        /// </list>
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            Action = action;
            switch(action)
            {
                case NotifyCollectionChangedAction.Add:
                    NewItems = new ArrayList { changedItem, };
                    OldItems = new ArrayList();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    NewItems = new ArrayList();
                    OldItems = new ArrayList { changedItem, };
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if(changedItem != null || index != -1)
                        throw new ArgumentException("When parameter action is Reset, either parameter changed item must be null or parameter index must be -1.");
                    break;
                default:
                    throw new ArgumentException("Parameter action must be one of Add, Remove, Reset.");
            }
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a one-item
        /// replace change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  This can be set to only replace.
        /// </param>
        /// <param name="newItem">
        /// The new item that is replaceing the original item.
        /// </param>
        /// <param name="oldItem">
        /// The original item that is replaced.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Replace" />.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            if(action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException("Parameter action must be Replace.");
            Action = action;
            NewItems = new ArrayList { newItem, };
            OldItems = new ArrayList { oldItem, };
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedAction" /> that describes a multi-item replace change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  This must be set to <see cref="NotifyCollectionChangedAction.Replace" />.
        /// </param>
        /// <param name="newItems">
        /// The new items that are replacing the original items.
        /// </param>
        /// <param name="oldItems">
        /// The old items that are replaced.
        /// </param>
        /// <param name="startingIndex">
        /// The index of the first iem of the items that are being replaced.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Replace" />.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
        {
            if(action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException("Parameter action must be Replace.");
            if(newItems == null)
                throw new ArgumentNullException("newItems");
            if(oldItems == null)
                throw new ArgumentNullException("oldItems");
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            OldStartingIndex = startingIndex;
            NewStartingIndex = startingIndex;
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs"/> that describes a multi-item move change.
        /// </summary>
        /// <param name="action">
        /// A <see cref="NotifyCollectionChangedAction"/>
        /// </param>
        /// <param name="changedItems">
        /// The items affected by the change.
        /// </param>
        /// <param name="index">
        /// The new index for the changed items.
        /// </param>
        /// <param name="oldIndex">
        /// The old index for the changed items.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Move" /> or if <paramref name="index"/> is negative.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
        {
            if(action != NotifyCollectionChangedAction.Move)
                throw new ArgumentException("Parameter action must be Move.");
            if(index < 0)
                throw new ArgumentException("Parameter index must not be negative");
            Action = action;
            OldItems = changedItems;
            OldStartingIndex = oldIndex;
            NewStartingIndex = index;
        }

        /// <summary>
        /// Initializes a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a one-item move change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  This must be move.
        /// </param>
        /// <param name="changedItem">
        /// The item affected by the change.
        /// </param>
        /// <param name="index">
        /// The new index for the changed item.
        /// </param>
        /// <param name="oldIndex">
        /// The old index for the changed item.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Move" /> or
        /// <paramref name="index"/> is negative.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
        {
            if(action != NotifyCollectionChangedAction.Move)
                throw new ArgumentException("Parameter action must be Move.");
            if(index < 0)
                throw new ArgumentException("Parameter index must not be negative");
            Action = action;
            OldItems = new ArrayList { changedItem, };
            OldStartingIndex = oldIndex;
            NewStartingIndex = index;
        }

        /// <summary>
        /// Initialize a new <see cref="NotifyCollectionChangedEventArgs" /> that describes a one-item replace change.
        /// </summary>
        /// <param name="action">
        /// The action that caused the event.  This must be set to replace.
        /// </param>
        /// <param name="newItem">
        /// The new item that is replacing the original item.
        /// </param>
        /// <param name="oldItem">
        /// The original item that is being replaced.
        /// </param>
        /// <param name="index">
        /// The index of the item being replace.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="action"/> is not <see cref="NotifyCollectionChangedAction.Replace" />.
        /// </exception>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
        {
            if(action != NotifyCollectionChangedAction.Replace)
                throw new ArgumentException("Parameter action must be Replace.");
            Action = action;
            NewItems = new ArrayList { newItem, };
            OldItems = new ArrayList { oldItem, };
            OldStartingIndex = index;
        }

        #endregion
    }
}

