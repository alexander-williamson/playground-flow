// Type: System.Dynamic.ExpandoObject
// Assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Core.dll

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime;

namespace System.Dynamic
{
    /// <summary>
    /// Represents an object whose members can be dynamically added and removed at run time.
    /// </summary>
    public sealed class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new ExpandoObject that does not have members.
        /// </summary>
        public ExpandoObject();

        /// <summary>
        /// The provided MetaObject will dispatch to the dynamic virtual methods. The object can be encapsulated inside another MetaObject to provide custom behavior for individual actions.
        /// </summary>
        /// 
        /// <returns>
        /// The object of the <see cref="T:System.Dynamic.DynamicMetaObject"/> type.
        /// </returns>
        /// <param name="parameter">The expression that represents the MetaObject to dispatch to the Dynamic virtual methods.</param>
        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void IDictionary<string, object>.Add(string key, object value);

        bool IDictionary<string, object>.ContainsKey(string key);
        bool IDictionary<string, object>.Remove(string key);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        bool IDictionary<string, object>.TryGetValue(string key, out object value);

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item);
        void ICollection<KeyValuePair<string, object>>.Clear();
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item);
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex);
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item);
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator();

        ICollection<string> IDictionary<string, object>.Keys { get; }
        ICollection<object> IDictionary<string, object>.Values { get; }
        object IDictionary<string, object>.this[string key] { get; set; }
        int ICollection<KeyValuePair<string, object>>.Count { get; }
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly { get; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;
    }
}
