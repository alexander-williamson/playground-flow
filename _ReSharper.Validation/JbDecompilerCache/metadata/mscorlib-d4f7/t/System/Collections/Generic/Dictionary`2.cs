// Type: System.Collections.Generic.Dictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a collection of keys and values.
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.
    ///                 </typeparam><typeparam name="TValue">The type of the values in the dictionary.
    ///                 </typeparam><filterpriority>1</filterpriority>
    [DebuggerDisplay("Count = {Count}")]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
    [Serializable]
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, ISerializable, IDeserializationCallback
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// 
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Dictionary();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// 
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"/> can contain.
        ///                 </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.
        ///                 </exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Dictionary(int capacity);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"/>.
        /// 
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> for the type of the key.
        ///                 </param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Dictionary(IEqualityComparer<TKey> comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"/>.
        /// 
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"/> can contain.
        ///                 </param><param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> for the type of the key.
        ///                 </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.
        ///                 </exception>
        public Dictionary(int capacity, IEqualityComparer<TKey> comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"/> and uses the default equality comparer for the key type.
        /// 
        /// </summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"/> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.
        ///                 </exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Dictionary(IDictionary<TKey, TValue> dictionary);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"/> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"/>.
        /// 
        /// </summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"/> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </param><param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> for the type of the key.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.
        ///                 </exception>
        public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class with serialized data.
        /// 
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </param><param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </param>
        protected Dictionary(SerializationInfo info, StreamingContext context);

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// 
        /// </summary>
        /// <param name="key">The key of the element to add.
        ///                 </param><param name="value">The value of the element to add. The value can be null for reference types.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </exception>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Add(TKey key, TValue value);

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair);

        /// <summary>
        /// Removes all keys and values from the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        public void Clear();

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains the specified key.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified key; otherwise, false.
        /// 
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        public bool ContainsKey(TKey key);

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains a specific value.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified value; otherwise, false.
        /// 
        /// </returns>
        /// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.Dictionary`2"/>. The value can be null for reference types.
        ///                 </param>
        public bool ContainsValue(TValue value);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator"/> structure for the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </returns>
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator();

        /// <summary>
        /// Implements the <see cref="T:System.Runtime.Serialization.ISerializable"/> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/> instance.
        /// 
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/> instance.
        ///                 </param><param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"/> instance.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="info"/> is null.
        ///                 </exception>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context);

        /// <summary>
        /// Implements the <see cref="T:System.Runtime.Serialization.ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
        /// 
        /// </summary>
        /// <param name="sender">The source of the deserialization event.
        ///                 </param><exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2"/> instance is invalid.
        ///                 </exception>
        public virtual void OnDeserialization(object sender);

        /// <summary>
        /// Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key"/> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </returns>
        /// <param name="key">The key of the element to remove.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        public bool Remove(TKey key);

        /// <summary>
        /// Gets the value associated with the specified key.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified key; otherwise, false.
        /// 
        /// </returns>
        /// <param name="key">The key of the value to get.
        ///                 </param><param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        public bool TryGetValue(TKey key, out TValue value);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an array, starting at the specified array index.
        /// 
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The array must have zero-based indexing.
        ///                 </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
        /// 
        ///                     -or-
        ///                 <paramref name="index"/> is less than 0.
        ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.
        /// 
        ///                     -or-
        ///                 <paramref name="array"/> does not have zero-based indexing.
        /// 
        ///                     -or-
        /// 
        ///                     The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
        /// 
        ///                     -or-
        /// 
        ///                     The type of the source <see cref="T:System.Collections.Generic.ICollection`1"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        ///                 </exception>
        void ICollection.CopyTo(Array array, int index);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> that can be used to iterate through the collection.
        /// 
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator();

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// 
        /// </summary>
        /// <param name="key">The object to use as the key.
        ///                 </param><param name="value">The object to use as the value.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="key"/> is of a type that is not assignable to the key type <paramref name="TKey"/> of the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        ///                     -or-
        ///                 <paramref name="value"/> is of a type that is not assignable to <paramref name="TValue"/>, the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        ///                     -or-
        /// 
        ///                     A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </exception>
        void IDictionary.Add(object key, object value);

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.IDictionary"/> contains an element with the specified key.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.IDictionary"/> contains an element with the specified key; otherwise, false.
        /// 
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary"/>.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        bool IDictionary.Contains(object key);

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IDictionaryEnumerator"/> for the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.IDictionaryEnumerator"/> for the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </returns>
        IDictionaryEnumerator IDictionary.GetEnumerator();

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </summary>
        /// <param name="key">The key of the element to remove.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception>
        void IDictionary.Remove(object key);

        /// <summary>
        /// Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> that is used to determine equality of keys for the dictionary.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> generic interface implementation that is used to determine equality of keys for the current <see cref="T:System.Collections.Generic.Dictionary`2"/> and to provide hash values for the keys.
        /// 
        /// </returns>
        public IEqualityComparer<TKey> Comparer { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </returns>
        public int Count { get; }

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </returns>
        public Dictionary<TKey, TValue>.KeyCollection Keys { get; }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys { get; }
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys { get; }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </returns>
        public Dictionary<TKey, TValue>.ValueCollection Values { get; }

        ICollection<TValue> IDictionary<TKey, TValue>.Values { get; }
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values { get; }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException"/>, and a set operation creates a new element with the specified key.
        /// 
        /// </returns>
        /// <param name="key">The key of the value to get or set.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> does not exist in the collection.
        ///                 </exception>
        public TValue this[TKey key] { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get; }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2"/>, this property always returns false.
        /// 
        /// </returns>
        bool ICollection.IsSynchronized { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// 
        /// </returns>
        object ICollection.SyncRoot { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary"/> has a fixed size.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.IDictionary"/> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2"/>, this property always returns false.
        /// 
        /// </returns>
        bool IDictionary.IsFixedSize { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary"/> is read-only.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Collections.IDictionary"/> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2"/>, this property always returns false.
        /// 
        /// </returns>
        bool IDictionary.IsReadOnly { get; }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ICollection"/> containing the keys of the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.ICollection"/> containing the keys of the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </returns>
        ICollection IDictionary.Keys { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ICollection"/> containing the values in the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.ICollection"/> containing the values in the <see cref="T:System.Collections.IDictionary"/>.
        /// 
        /// </returns>
        ICollection IDictionary.Values { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        /// <summary>
        /// Gets or sets the value with the specified key.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The value associated with the specified key, or null if <paramref name="key"/> is not in the dictionary or <paramref name="key"/> is of a type that is not assignable to the key type <paramref name="TKey"/> of the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </returns>
        /// <param name="key">The key of the value to get.
        ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        ///                 </exception><exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key"/> is of a type that is not assignable to the key type <paramref name="TKey"/> of the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        ///                     -or-
        /// 
        ///                     A value is being assigned, and <paramref name="value"/> is of a type that is not assignable to the value type <paramref name="TValue"/> of the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        ///                 </exception>
        object IDictionary.this[object key] { get; set; }

        /// <summary>
        /// Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// 
        /// </summary>
        [Serializable]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IDictionaryEnumerator, IEnumerator
        {
            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// 
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
            ///                 </exception>
            public bool MoveNext();

            /// <summary>
            /// Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator"/>.
            /// 
            /// </summary>
            public void Dispose();

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// 
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
            ///                 </exception>
            void IEnumerator.Reset();

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The element in the <see cref="T:System.Collections.Generic.Dictionary`2"/> at the current position of the enumerator.
            /// 
            /// </returns>
            public KeyValuePair<TKey, TValue> Current { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get; }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object"/>.
            /// 
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
            ///                 </exception>
            object IEnumerator.Current { get; }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The element in the dictionary at the current position of the enumerator, as a <see cref="T:System.Collections.DictionaryEntry"/>.
            /// 
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
            ///                 </exception>
            DictionaryEntry IDictionaryEnumerator.Entry { get; }

            /// <summary>
            /// Gets the key of the element at the current position of the enumerator.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The key of the element in the dictionary at the current position of the enumerator.
            /// 
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
            ///                 </exception>
            object IDictionaryEnumerator.Key { get; }

            /// <summary>
            /// Gets the value of the element at the current position of the enumerator.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The value of the element in the dictionary at the current position of the enumerator.
            /// 
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
            ///                 </exception>
            object IDictionaryEnumerator.Value { get; }
        }

        /// <summary>
        /// Represents the collection of keys in a <see cref="T:System.Collections.Generic.Dictionary`2"/>. This class cannot be inherited.
        /// 
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(typeof (Mscorlib_DictionaryKeyCollectionDebugView<,>))]
        [Serializable]
        public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, ICollection, IEnumerable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.Dictionary`2"/>.
            /// 
            /// </summary>
            /// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2"/> whose keys are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.
            ///                 </exception>
            public KeyCollection(Dictionary<TKey, TValue> dictionary);

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator"/> for the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// 
            /// </returns>
            public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator();

            /// <summary>
            /// Copies the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> elements to an existing one-dimensional <see cref="T:System.Array"/>, starting at the specified array index.
            /// 
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
            ///                 </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
            ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
            ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
            /// 
            ///                     -or-
            /// 
            ///                     The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
            ///                 </exception>
            public void CopyTo(TKey[] array, int index);

            void ICollection<TKey>.Add(TKey item);
            void ICollection<TKey>.Clear();
            bool ICollection<TKey>.Contains(TKey item);
            bool ICollection<TKey>.Remove(TKey item);
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator();

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> that can be used to iterate through the collection.
            /// 
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator();

            /// <summary>
            /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
            /// 
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
            ///                 </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
            ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
            ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.
            /// 
            ///                     -or-
            ///                 <paramref name="array"/> does not have zero-based indexing.
            /// 
            ///                     -or-
            ///                 <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
            /// 
            ///                     -or-
            /// 
            ///                     The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
            /// 
            ///                     -or-
            /// 
            ///                     The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
            ///                 </exception>
            void ICollection.CopyTo(Array array, int index);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// 
            ///                     Retrieving the value of this property is an O(1) operation.
            /// 
            /// </returns>
            public int Count { get; }

            bool ICollection<TKey>.IsReadOnly { get; }

            /// <summary>
            /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>, this property always returns false.
            /// 
            /// </returns>
            bool ICollection.IsSynchronized { get; }

            /// <summary>
            /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>, this property always returns the current instance.
            /// 
            /// </returns>
            object ICollection.SyncRoot { get; }

            /// <summary>
            /// Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
            /// 
            /// </summary>
            [Serializable]
            public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
            {
                /// <summary>
                /// Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator"/>.
                /// 
                /// </summary>
                public void Dispose();

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/>.
                /// 
                /// </summary>
                /// 
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
                /// 
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
                ///                 </exception>
                public bool MoveNext();

                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element in the collection.
                /// 
                /// </summary>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
                ///                 </exception>
                void IEnumerator.Reset();

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// 
                /// </summary>
                /// 
                /// <returns>
                /// The element in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection"/> at the current position of the enumerator.
                /// 
                /// </returns>
                public TKey Current { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get; }

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// 
                /// </summary>
                /// 
                /// <returns>
                /// The element in the collection at the current position of the enumerator.
                /// 
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
                ///                 </exception>
                object IEnumerator.Current { get; }
            }
        }

        /// <summary>
        /// Represents the collection of values in a <see cref="T:System.Collections.Generic.Dictionary`2"/>. This class cannot be inherited.
        /// 
        /// </summary>
        [DebuggerTypeProxy(typeof (Mscorlib_DictionaryValueCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        [Serializable]
        public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, ICollection, IEnumerable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> class that reflects the values in the specified <see cref="T:System.Collections.Generic.Dictionary`2"/>.
            /// 
            /// </summary>
            /// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2"/> whose values are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="dictionary"/> is null.
            ///                 </exception>
            public ValueCollection(Dictionary<TKey, TValue> dictionary);

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator"/> for the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// 
            /// </returns>
            public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator();

            /// <summary>
            /// Copies the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> elements to an existing one-dimensional <see cref="T:System.Array"/>, starting at the specified array index.
            /// 
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
            ///                 </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
            ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
            ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
            /// 
            ///                     -or-
            /// 
            ///                     The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
            ///                 </exception>
            public void CopyTo(TValue[] array, int index);

            void ICollection<TValue>.Add(TValue item);
            bool ICollection<TValue>.Remove(TValue item);
            void ICollection<TValue>.Clear();
            bool ICollection<TValue>.Contains(TValue item);
            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator();

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> that can be used to iterate through the collection.
            /// 
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator();

            /// <summary>
            /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
            /// 
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
            ///                 </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
            ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.
            ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.
            /// 
            ///                     -or-
            ///                 <paramref name="array"/> does not have zero-based indexing.
            /// 
            ///                     -or-
            ///                 <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
            /// 
            ///                     -or-
            /// 
            ///                     The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
            /// 
            ///                     -or-
            /// 
            ///                     The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
            ///                 </exception>
            void ICollection.CopyTo(Array array, int index);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// 
            /// </returns>
            public int Count { get; }

            bool ICollection<TValue>.IsReadOnly { get; }

            /// <summary>
            /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>, this property always returns false.
            /// 
            /// </returns>
            bool ICollection.IsSynchronized { get; }

            /// <summary>
            /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
            /// 
            /// </summary>
            /// 
            /// <returns>
            /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>, this property always returns the current instance.
            /// 
            /// </returns>
            object ICollection.SyncRoot { get; }

            /// <summary>
            /// Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
            /// 
            /// </summary>
            [Serializable]
            public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
            {
                /// <summary>
                /// Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator"/>.
                /// 
                /// </summary>
                public void Dispose();

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/>.
                /// 
                /// </summary>
                /// 
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
                /// 
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
                ///                 </exception>
                public bool MoveNext();

                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element in the collection.
                /// 
                /// </summary>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.
                ///                 </exception>
                void IEnumerator.Reset();

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// 
                /// </summary>
                /// 
                /// <returns>
                /// The element in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection"/> at the current position of the enumerator.
                /// 
                /// </returns>
                public TValue Current { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get; }

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// 
                /// </summary>
                /// 
                /// <returns>
                /// The element in the collection at the current position of the enumerator.
                /// 
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
                ///                 </exception>
                object IEnumerator.Current { get; }
            }
        }
    }
}
