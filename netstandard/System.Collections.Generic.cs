// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Collections.Generic
{
#if NETSTANDARD2_1_OR_GREATER
    public static partial class CollectionExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) { throw null; }
        public static TValue GetValueOrDefault<TKey, TValue>(this System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) { throw null; }
        public static bool Remove<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, out TValue value) { throw null; }
        public static bool TryAdd<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue value) { throw null; }
    }
#endif
    public abstract partial class Comparer<T> : System.Collections.Generic.IComparer<T>, System.Collections.IComparer
    {
        protected Comparer() { }
        public static System.Collections.Generic.Comparer<T> Default { get { throw null; } }
        public abstract int Compare(T x, T y);
        public static System.Collections.Generic.Comparer<T> Create(System.Comparison<T> comparison) { throw null; }
        int System.Collections.IComparer.Compare(object x, object y) { throw null; }
    }
    public partial class Dictionary<TKey, TValue> : System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IDictionary<TKey, TValue>, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>, System.Collections.ICollection, System.Collections.IDictionary, System.Collections.IEnumerable, System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
    {
#if EXPOSE_IMPLEMENTATION_DETAILS
        public System.Collections.Generic.Dictionary<TKey, TValue>.Entry[] _entries;
        public int FindEntry(TKey key) => throw null;

        public struct Entry
        {
            public TValue value;
        }
#endif
        public Dictionary() { }
        public Dictionary(System.Collections.Generic.IDictionary<TKey, TValue> dictionary) { }
        public Dictionary(System.Collections.Generic.IDictionary<TKey, TValue> dictionary, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
#if NETSTANDARD2_1_OR_GREATER
        public Dictionary(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> collection) { }
        public Dictionary(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> collection, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
#endif
        public Dictionary(System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        public Dictionary(int capacity) { }
        public Dictionary(int capacity, System.Collections.Generic.IEqualityComparer<TKey> comparer) { }
        protected Dictionary(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public System.Collections.Generic.IEqualityComparer<TKey> Comparer { get { throw null; } }
        public int Count { get { throw null; } }
        public TValue this[TKey key] { get { throw null; } set { } }
        public System.Collections.Generic.Dictionary<TKey, TValue>.KeyCollection Keys { get { throw null; } }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.IsReadOnly { get { throw null; } }
        System.Collections.Generic.ICollection<TKey> System.Collections.Generic.IDictionary<TKey,TValue>.Keys { get { throw null; } }
        System.Collections.Generic.ICollection<TValue> System.Collections.Generic.IDictionary<TKey,TValue>.Values { get { throw null; } }
        System.Collections.Generic.IEnumerable<TKey> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Keys { get { throw null; } }
        System.Collections.Generic.IEnumerable<TValue> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Values { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        bool System.Collections.IDictionary.IsFixedSize { get { throw null; } }
        bool System.Collections.IDictionary.IsReadOnly { get { throw null; } }
        object System.Collections.IDictionary.this[object key] { get { throw null; } set { } }
        System.Collections.ICollection System.Collections.IDictionary.Keys { get { throw null; } }
        System.Collections.ICollection System.Collections.IDictionary.Values { get { throw null; } }
        public System.Collections.Generic.Dictionary<TKey, TValue>.ValueCollection Values { get { throw null; } }
        public void Add(TKey key, TValue value) { }
        public void Clear() { }
        public bool ContainsKey(TKey key) { throw null; }
        public bool ContainsValue(TValue value) { throw null; }
#if NETSTANDARD2_1_OR_GREATER
        public int EnsureCapacity(int capacity) { throw null; }
#endif
        public System.Collections.Generic.Dictionary<TKey, TValue>.Enumerator GetEnumerator() { throw null; }
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public virtual void OnDeserialization(object sender) { }
        public bool Remove(TKey key) { throw null; }
#if NETSTANDARD2_1_OR_GREATER
        public bool Remove(TKey key, out TValue value) { throw null; }
#endif
        void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { throw null; }
        void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int index) { }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { throw null; }
        System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey,TValue>>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
        void System.Collections.IDictionary.Add(object key, object value) { }
        bool System.Collections.IDictionary.Contains(object key) { throw null; }
        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() { throw null; }
        void System.Collections.IDictionary.Remove(object key) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
#if NETSTANDARD2_1_OR_GREATER
        public void TrimExcess() { }
        public void TrimExcess(int capacity) { }
        public bool TryAdd(TKey key, TValue value) { throw null; }
#endif
        public bool TryGetValue(TKey key, out TValue value) { throw null; }
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator, System.IDisposable
        {
            private object _dummy;
            public System.Collections.Generic.KeyValuePair<TKey, TValue> Current { get { throw null; } }
            System.Collections.DictionaryEntry System.Collections.IDictionaryEnumerator.Entry { get { throw null; } }
            object System.Collections.IDictionaryEnumerator.Key { get { throw null; } }
            object System.Collections.IDictionaryEnumerator.Value { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
        }
        public sealed partial class KeyCollection : System.Collections.Generic.ICollection<TKey>, System.Collections.Generic.IEnumerable<TKey>, System.Collections.Generic.IReadOnlyCollection<TKey>, System.Collections.ICollection, System.Collections.IEnumerable
        {
            public KeyCollection(System.Collections.Generic.Dictionary<TKey, TValue> dictionary) { }
            public int Count { get { throw null; } }
            bool System.Collections.Generic.ICollection<TKey>.IsReadOnly { get { throw null; } }
            bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
            object System.Collections.ICollection.SyncRoot { get { throw null; } }
            public void CopyTo(TKey[] array, int index) { }
            public System.Collections.Generic.Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator() { throw null; }
            void System.Collections.Generic.ICollection<TKey>.Add(TKey item) { }
            void System.Collections.Generic.ICollection<TKey>.Clear() { }
            bool System.Collections.Generic.ICollection<TKey>.Contains(TKey item) { throw null; }
            bool System.Collections.Generic.ICollection<TKey>.Remove(TKey item) { throw null; }
            System.Collections.Generic.IEnumerator<TKey> System.Collections.Generic.IEnumerable<TKey>.GetEnumerator() { throw null; }
            void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
            public partial struct Enumerator : System.Collections.Generic.IEnumerator<TKey>, System.Collections.IEnumerator, System.IDisposable
            {
                private object _dummy;
                public TKey Current { get { throw null; } }
                object System.Collections.IEnumerator.Current { get { throw null; } }
                public void Dispose() { }
                public bool MoveNext() { throw null; }
                void System.Collections.IEnumerator.Reset() { }
            }
        }
        public sealed partial class ValueCollection : System.Collections.Generic.ICollection<TValue>, System.Collections.Generic.IEnumerable<TValue>, System.Collections.Generic.IReadOnlyCollection<TValue>, System.Collections.ICollection, System.Collections.IEnumerable
        {
            public ValueCollection(System.Collections.Generic.Dictionary<TKey, TValue> dictionary) { }
            public int Count { get { throw null; } }
            bool System.Collections.Generic.ICollection<TValue>.IsReadOnly { get { throw null; } }
            bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
            object System.Collections.ICollection.SyncRoot { get { throw null; } }
            public void CopyTo(TValue[] array, int index) { }
            public System.Collections.Generic.Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator() { throw null; }
            void System.Collections.Generic.ICollection<TValue>.Add(TValue item) { }
            void System.Collections.Generic.ICollection<TValue>.Clear() { }
            bool System.Collections.Generic.ICollection<TValue>.Contains(TValue item) { throw null; }
            bool System.Collections.Generic.ICollection<TValue>.Remove(TValue item) { throw null; }
            System.Collections.Generic.IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator() { throw null; }
            void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
            public partial struct Enumerator : System.Collections.Generic.IEnumerator<TValue>, System.Collections.IEnumerator, System.IDisposable
            {
                private object _dummy;
                public TValue Current { get { throw null; } }
                object System.Collections.IEnumerator.Current { get { throw null; } }
                public void Dispose() { }
                public bool MoveNext() { throw null; }
                void System.Collections.IEnumerator.Reset() { }
            }
        }
    }
    public abstract partial class EqualityComparer<T> : System.Collections.Generic.IEqualityComparer<T>, System.Collections.IEqualityComparer
    {
        protected EqualityComparer() { }
        public static System.Collections.Generic.EqualityComparer<T> Default { get { throw null; } }
        public abstract bool Equals(T x, T y);
        public abstract int GetHashCode(T obj);
        bool System.Collections.IEqualityComparer.Equals(object x, object y) { throw null; }
        int System.Collections.IEqualityComparer.GetHashCode(object obj) { throw null; }
    }
    public partial class HashSet<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.Generic.ISet<T>, System.Collections.IEnumerable, System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
    {
        public HashSet() { }
        public HashSet(System.Collections.Generic.IEnumerable<T> collection) { }
        public HashSet(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        public HashSet(System.Collections.Generic.IEqualityComparer<T> comparer) { }
#if NETSTANDARD2_1_OR_GREATER
        public HashSet(int capacity) { }
        public HashSet(int capacity, System.Collections.Generic.IEqualityComparer<T> comparer) { }
#endif
        protected HashSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public System.Collections.Generic.IEqualityComparer<T> Comparer { get { throw null; } }
        public int Count { get { throw null; } }
        bool System.Collections.Generic.ICollection<T>.IsReadOnly { get { throw null; } }
        public bool Add(T item) { throw null; }
        public void Clear() { }
        public bool Contains(T item) { throw null; }
        public void CopyTo(T[] array) { }
        public void CopyTo(T[] array, int arrayIndex) { }
        public void CopyTo(T[] array, int arrayIndex, int count) { }
        public static System.Collections.Generic.IEqualityComparer<System.Collections.Generic.HashSet<T>> CreateSetComparer() { throw null; }
#if NETSTANDARD2_1_OR_GREATER
        public int EnsureCapacity(int capacity) { throw null; }
#endif
        public void ExceptWith(System.Collections.Generic.IEnumerable<T> other) { }
        public System.Collections.Generic.HashSet<T>.Enumerator GetEnumerator() { throw null; }
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public void IntersectWith(System.Collections.Generic.IEnumerable<T> other) { }
        public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool IsSubsetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool IsSupersetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public virtual void OnDeserialization(object sender) { }
        public bool Overlaps(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool Remove(T item) { throw null; }
        public int RemoveWhere(System.Predicate<T> match) { throw null; }
        public bool SetEquals(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other) { }
        void System.Collections.Generic.ICollection<T>.Add(T item) { }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { throw null; }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        public void TrimExcess() { }
#if NETSTANDARD2_1_OR_GREATER
        public bool TryGetValue(T equalValue, out T actualValue) { throw null; }
#endif
        public void UnionWith(System.Collections.Generic.IEnumerable<T> other) { }
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.Collections.IEnumerator, System.IDisposable
        {
            private object _dummy;
            public T Current { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
        }
    }
#if NETSTANDARD2_1_OR_GREATER
    public partial interface IAsyncEnumerable<out T>
    {
        System.Collections.Generic.IAsyncEnumerator<T> GetAsyncEnumerator(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    public partial interface IAsyncEnumerator<out T> : System.IAsyncDisposable
    {
        T Current { get; }
        System.Threading.Tasks.ValueTask<bool> MoveNextAsync();
    }
#endif
    public partial interface ICollection<T> : System.Collections.Generic.IEnumerable<T>, System.Collections.IEnumerable
    {
        int Count { get; }
        bool IsReadOnly { get; }
        void Add(T item);
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
        bool Remove(T item);
    }
    public partial interface IComparer<in T>
    {
        int Compare(T x, T y);
    }
    public partial interface IDictionary<TKey, TValue> : System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.IEnumerable
    {
        TValue this[TKey key] { get; set; }
        System.Collections.Generic.ICollection<TKey> Keys { get; }
        System.Collections.Generic.ICollection<TValue> Values { get; }
        void Add(TKey key, TValue value);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue value);
    }
    public partial interface IEnumerable<out T> : System.Collections.IEnumerable
    {
        new System.Collections.Generic.IEnumerator<T> GetEnumerator();
    }
    public partial interface IEnumerator<out T> : System.Collections.IEnumerator, System.IDisposable
    {
        new T Current { get; }
    }
    public partial interface IEqualityComparer<in T>
    {
        bool Equals(T x, T y);
        int GetHashCode(T obj);
    }
    public partial interface IList<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.IEnumerable
    {
        T this[int index] { get; set; }
        int IndexOf(T item);
        void Insert(int index, T item);
        void RemoveAt(int index);
    }
    public partial interface IReadOnlyCollection<out T> : System.Collections.Generic.IEnumerable<T>, System.Collections.IEnumerable
    {
        int Count { get; }
    }
    public partial interface IReadOnlyDictionary<TKey, TValue> : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.IEnumerable
    {
        TValue this[TKey key] { get; }
        System.Collections.Generic.IEnumerable<TKey> Keys { get; }
        System.Collections.Generic.IEnumerable<TValue> Values { get; }
        bool ContainsKey(TKey key);
        bool TryGetValue(TKey key, out TValue value);
    }
    public partial interface IReadOnlyList<out T> : System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.IEnumerable
    {
        T this[int index] { get; }
    }
    public partial interface ISet<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.IEnumerable
    {
        new bool Add(T item);
        void ExceptWith(System.Collections.Generic.IEnumerable<T> other);
        void IntersectWith(System.Collections.Generic.IEnumerable<T> other);
        bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other);
        bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other);
        bool IsSubsetOf(System.Collections.Generic.IEnumerable<T> other);
        bool IsSupersetOf(System.Collections.Generic.IEnumerable<T> other);
        bool Overlaps(System.Collections.Generic.IEnumerable<T> other);
        bool SetEquals(System.Collections.Generic.IEnumerable<T> other);
        void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other);
        void UnionWith(System.Collections.Generic.IEnumerable<T> other);
    }
    public partial class KeyNotFoundException : System.SystemException
#if !NETSTANDARD2_1_OR_GREATER
        , System.Runtime.Serialization.ISerializable
#endif
    {
        public KeyNotFoundException() { }
        protected KeyNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public KeyNotFoundException(string message) { }
        public KeyNotFoundException(string message, System.Exception innerException) { }
    }
#if NETSTANDARD2_1_OR_GREATER
    public static partial class KeyValuePair
    {
        public static System.Collections.Generic.KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) { throw null; }
    }
#endif
    public readonly partial struct KeyValuePair<TKey, TValue>
    {
        private readonly TKey key;
        private readonly TValue value;
        private readonly int _dummyPrimitive;
        public KeyValuePair(TKey key, TValue value) { throw null; }
        public TKey Key { get { throw null; } }
        public TValue Value { get { throw null; } }
#if NETSTANDARD2_1_OR_GREATER
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public void Deconstruct(out TKey key, out TValue value) { throw null; }
#endif
        public override string ToString() { throw null; }
    }
    public partial class LinkedList<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.ICollection, System.Collections.IEnumerable, System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
    {
        public LinkedList() { }
        public LinkedList(System.Collections.Generic.IEnumerable<T> collection) { }
        protected LinkedList(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public int Count { get { throw null; } }
        public System.Collections.Generic.LinkedListNode<T> First { get { throw null; } }
        public System.Collections.Generic.LinkedListNode<T> Last { get { throw null; } }
        bool System.Collections.Generic.ICollection<T>.IsReadOnly { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        public void AddAfter(System.Collections.Generic.LinkedListNode<T> node, System.Collections.Generic.LinkedListNode<T> newNode) { }
        public System.Collections.Generic.LinkedListNode<T> AddAfter(System.Collections.Generic.LinkedListNode<T> node, T value) { throw null; }
        public void AddBefore(System.Collections.Generic.LinkedListNode<T> node, System.Collections.Generic.LinkedListNode<T> newNode) { }
        public System.Collections.Generic.LinkedListNode<T> AddBefore(System.Collections.Generic.LinkedListNode<T> node, T value) { throw null; }
        public void AddFirst(System.Collections.Generic.LinkedListNode<T> node) { }
        public System.Collections.Generic.LinkedListNode<T> AddFirst(T value) { throw null; }
        public void AddLast(System.Collections.Generic.LinkedListNode<T> node) { }
        public System.Collections.Generic.LinkedListNode<T> AddLast(T value) { throw null; }
        public void Clear() { }
        public bool Contains(T value) { throw null; }
        public void CopyTo(T[] array, int index) { }
        public System.Collections.Generic.LinkedListNode<T> Find(T value) { throw null; }
        public System.Collections.Generic.LinkedListNode<T> FindLast(T value) { throw null; }
        public System.Collections.Generic.LinkedList<T>.Enumerator GetEnumerator() { throw null; }
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public virtual void OnDeserialization(object sender) { }
        public void Remove(System.Collections.Generic.LinkedListNode<T> node) { }
        public bool Remove(T value) { throw null; }
        public void RemoveFirst() { }
        public void RemoveLast() { }
        void System.Collections.Generic.ICollection<T>.Add(T value) { }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.Collections.IEnumerator, System.IDisposable, System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
        {
            private object _dummy;
            public T Current { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
            void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(object sender) { }
            void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        }
    }
    public sealed partial class LinkedListNode<T>
    {
        public LinkedListNode(T value) { }
        public System.Collections.Generic.LinkedList<T> List { get { throw null; } }
        public System.Collections.Generic.LinkedListNode<T> Next { get { throw null; } }
        public System.Collections.Generic.LinkedListNode<T> Previous { get { throw null; } }
        public T Value { get { throw null; } set { } }
    }
    public partial class List<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IList<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.Generic.IReadOnlyList<T>, System.Collections.ICollection, System.Collections.IEnumerable, System.Collections.IList
    {
#if EXPOSE_IMPLEMENTATION_DETAILS
        public T[] _items;
        public int _size;
        public int _version;
        public void EnsureCapacity(int min) { }
#endif
        public List() { }
        public List(System.Collections.Generic.IEnumerable<T> collection) { }
        public List(int capacity) { }
        public int Capacity { get { throw null; } set { } }
        public int Count { get { throw null; } }
        public T this[int index] { get { throw null; } set { } }
        bool System.Collections.Generic.ICollection<T>.IsReadOnly { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        bool System.Collections.IList.IsFixedSize { get { throw null; } }
        bool System.Collections.IList.IsReadOnly { get { throw null; } }
        object System.Collections.IList.this[int index] { get { throw null; } set { } }
        public void Add(T item) { }
        public void AddRange(System.Collections.Generic.IEnumerable<T> collection) { }
        public System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly() { throw null; }
        public int BinarySearch(int index, int count, T item, System.Collections.Generic.IComparer<T> comparer) { throw null; }
        public int BinarySearch(T item) { throw null; }
        public int BinarySearch(T item, System.Collections.Generic.IComparer<T> comparer) { throw null; }
        public void Clear() { }
        public bool Contains(T item) { throw null; }
        public System.Collections.Generic.List<TOutput> ConvertAll<TOutput>(System.Converter<T, TOutput> converter) { throw null; }
        public void CopyTo(int index, T[] array, int arrayIndex, int count) { }
        public void CopyTo(T[] array) { }
        public void CopyTo(T[] array, int arrayIndex) { }
        public bool Exists(System.Predicate<T> match) { throw null; }
        public T Find(System.Predicate<T> match) { throw null; }
        public System.Collections.Generic.List<T> FindAll(System.Predicate<T> match) { throw null; }
        public int FindIndex(int startIndex, int count, System.Predicate<T> match) { throw null; }
        public int FindIndex(int startIndex, System.Predicate<T> match) { throw null; }
        public int FindIndex(System.Predicate<T> match) { throw null; }
        public T FindLast(System.Predicate<T> match) { throw null; }
        public int FindLastIndex(int startIndex, int count, System.Predicate<T> match) { throw null; }
        public int FindLastIndex(int startIndex, System.Predicate<T> match) { throw null; }
        public int FindLastIndex(System.Predicate<T> match) { throw null; }
        public void ForEach(System.Action<T> action) { }
        public System.Collections.Generic.List<T>.Enumerator GetEnumerator() { throw null; }
        public System.Collections.Generic.List<T> GetRange(int index, int count) { throw null; }
        public int IndexOf(T item) { throw null; }
        public int IndexOf(T item, int index) { throw null; }
        public int IndexOf(T item, int index, int count) { throw null; }
        public void Insert(int index, T item) { }
        public void InsertRange(int index, System.Collections.Generic.IEnumerable<T> collection) { }
        public int LastIndexOf(T item) { throw null; }
        public int LastIndexOf(T item, int index) { throw null; }
        public int LastIndexOf(T item, int index, int count) { throw null; }
        public bool Remove(T item) { throw null; }
        public int RemoveAll(System.Predicate<T> match) { throw null; }
        public void RemoveAt(int index) { }
        public void RemoveRange(int index, int count) { }
        public void Reverse() { }
        public void Reverse(int index, int count) { }
        public void Sort() { }
        public void Sort(System.Collections.Generic.IComparer<T> comparer) { }
        public void Sort(System.Comparison<T> comparison) { }
        public void Sort(int index, int count, System.Collections.Generic.IComparer<T> comparer) { }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int arrayIndex) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        int System.Collections.IList.Add(object item) { throw null; }
        bool System.Collections.IList.Contains(object item) { throw null; }
        int System.Collections.IList.IndexOf(object item) { throw null; }
        void System.Collections.IList.Insert(int index, object item) { }
        void System.Collections.IList.Remove(object item) { }
        public T[] ToArray() { throw null; }
        public void TrimExcess() { }
        public bool TrueForAll(System.Predicate<T> match) { throw null; }
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.Collections.IEnumerator, System.IDisposable
        {
            private object _dummy;
            public T Current { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
        }
    }
    public partial class Queue<T> : System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.ICollection, System.Collections.IEnumerable
    {
        public Queue() { }
        public Queue(System.Collections.Generic.IEnumerable<T> collection) { }
        public Queue(int capacity) { }
        public int Count { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        public void Clear() { }
        public bool Contains(T item) { throw null; }
        public void CopyTo(T[] array, int arrayIndex) { }
        public T Dequeue() { throw null; }
        public void Enqueue(T item) { }
        public System.Collections.Generic.Queue<T>.Enumerator GetEnumerator() { throw null; }
        public T Peek() { throw null; }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        public T[] ToArray() { throw null; }
        public void TrimExcess() { }
#if NETSTANDARD2_1_OR_GREATER
        public bool TryDequeue(out T result) { throw null; }
        public bool TryPeek(out T result) { throw null; }
#endif
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.Collections.IEnumerator, System.IDisposable
        {
            private object _dummy;
            public T Current { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
        }
    }
    public partial class SortedDictionary<TKey, TValue> : System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IDictionary<TKey, TValue>, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>, System.Collections.ICollection, System.Collections.IDictionary, System.Collections.IEnumerable
    {
        public SortedDictionary() { }
        public SortedDictionary(System.Collections.Generic.IComparer<TKey> comparer) { }
        public SortedDictionary(System.Collections.Generic.IDictionary<TKey, TValue> dictionary) { }
        public SortedDictionary(System.Collections.Generic.IDictionary<TKey, TValue> dictionary, System.Collections.Generic.IComparer<TKey> comparer) { }
        public System.Collections.Generic.IComparer<TKey> Comparer { get { throw null; } }
        public int Count { get { throw null; } }
        public TValue this[TKey key] { get { throw null; } set { } }
        public System.Collections.Generic.SortedDictionary<TKey, TValue>.KeyCollection Keys { get { throw null; } }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.IsReadOnly { get { throw null; } }
        System.Collections.Generic.ICollection<TKey> System.Collections.Generic.IDictionary<TKey,TValue>.Keys { get { throw null; } }
        System.Collections.Generic.ICollection<TValue> System.Collections.Generic.IDictionary<TKey,TValue>.Values { get { throw null; } }
        System.Collections.Generic.IEnumerable<TKey> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Keys { get { throw null; } }
        System.Collections.Generic.IEnumerable<TValue> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Values { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        bool System.Collections.IDictionary.IsFixedSize { get { throw null; } }
        bool System.Collections.IDictionary.IsReadOnly { get { throw null; } }
        object System.Collections.IDictionary.this[object key] { get { throw null; } set { } }
        System.Collections.ICollection System.Collections.IDictionary.Keys { get { throw null; } }
        System.Collections.ICollection System.Collections.IDictionary.Values { get { throw null; } }
        public System.Collections.Generic.SortedDictionary<TKey, TValue>.ValueCollection Values { get { throw null; } }
        public void Add(TKey key, TValue value) { }
        public void Clear() { }
        public bool ContainsKey(TKey key) { throw null; }
        public bool ContainsValue(TValue value) { throw null; }
        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int index) { }
        public System.Collections.Generic.SortedDictionary<TKey, TValue>.Enumerator GetEnumerator() { throw null; }
        public bool Remove(TKey key) { throw null; }
        void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { throw null; }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { throw null; }
        System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey,TValue>>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
        void System.Collections.IDictionary.Add(object key, object value) { }
        bool System.Collections.IDictionary.Contains(object key) { throw null; }
        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() { throw null; }
        void System.Collections.IDictionary.Remove(object key) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        public bool TryGetValue(TKey key, out TValue value) { throw null; }
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator, System.IDisposable
        {
            private object _dummy;
            public System.Collections.Generic.KeyValuePair<TKey, TValue> Current { get { throw null; } }
            System.Collections.DictionaryEntry System.Collections.IDictionaryEnumerator.Entry { get { throw null; } }
            object System.Collections.IDictionaryEnumerator.Key { get { throw null; } }
            object System.Collections.IDictionaryEnumerator.Value { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
        }
        public sealed partial class KeyCollection : System.Collections.Generic.ICollection<TKey>, System.Collections.Generic.IEnumerable<TKey>, System.Collections.Generic.IReadOnlyCollection<TKey>, System.Collections.ICollection, System.Collections.IEnumerable
        {
            public KeyCollection(System.Collections.Generic.SortedDictionary<TKey, TValue> dictionary) { }
            public int Count { get { throw null; } }
            bool System.Collections.Generic.ICollection<TKey>.IsReadOnly { get { throw null; } }
            bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
            object System.Collections.ICollection.SyncRoot { get { throw null; } }
            public void CopyTo(TKey[] array, int index) { }
            public System.Collections.Generic.SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator() { throw null; }
            void System.Collections.Generic.ICollection<TKey>.Add(TKey item) { }
            void System.Collections.Generic.ICollection<TKey>.Clear() { }
            bool System.Collections.Generic.ICollection<TKey>.Contains(TKey item) { throw null; }
            bool System.Collections.Generic.ICollection<TKey>.Remove(TKey item) { throw null; }
            System.Collections.Generic.IEnumerator<TKey> System.Collections.Generic.IEnumerable<TKey>.GetEnumerator() { throw null; }
            void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
            public partial struct Enumerator : System.Collections.Generic.IEnumerator<TKey>, System.Collections.IEnumerator, System.IDisposable
            {
                private object _dummy;
                public TKey Current { get { throw null; } }
                object System.Collections.IEnumerator.Current { get { throw null; } }
                public void Dispose() { }
                public bool MoveNext() { throw null; }
                void System.Collections.IEnumerator.Reset() { }
            }
        }
        public sealed partial class ValueCollection : System.Collections.Generic.ICollection<TValue>, System.Collections.Generic.IEnumerable<TValue>, System.Collections.Generic.IReadOnlyCollection<TValue>, System.Collections.ICollection, System.Collections.IEnumerable
        {
            public ValueCollection(System.Collections.Generic.SortedDictionary<TKey, TValue> dictionary) { }
            public int Count { get { throw null; } }
            bool System.Collections.Generic.ICollection<TValue>.IsReadOnly { get { throw null; } }
            bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
            object System.Collections.ICollection.SyncRoot { get { throw null; } }
            public void CopyTo(TValue[] array, int index) { }
            public System.Collections.Generic.SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator() { throw null; }
            void System.Collections.Generic.ICollection<TValue>.Add(TValue item) { }
            void System.Collections.Generic.ICollection<TValue>.Clear() { }
            bool System.Collections.Generic.ICollection<TValue>.Contains(TValue item) { throw null; }
            bool System.Collections.Generic.ICollection<TValue>.Remove(TValue item) { throw null; }
            System.Collections.Generic.IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator() { throw null; }
            void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
            public partial struct Enumerator : System.Collections.Generic.IEnumerator<TValue>, System.Collections.IEnumerator, System.IDisposable
            {
                private object _dummy;
                public TValue Current { get { throw null; } }
                object System.Collections.IEnumerator.Current { get { throw null; } }
                public void Dispose() { }
                public bool MoveNext() { throw null; }
                void System.Collections.IEnumerator.Reset() { }
            }
        }
    }
    public partial class SortedList<TKey, TValue> : System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IDictionary<TKey, TValue>, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>, System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>, System.Collections.ICollection, System.Collections.IDictionary, System.Collections.IEnumerable
    {
        public SortedList() { }
        public SortedList(System.Collections.Generic.IComparer<TKey> comparer) { }
        public SortedList(System.Collections.Generic.IDictionary<TKey, TValue> dictionary) { }
        public SortedList(System.Collections.Generic.IDictionary<TKey, TValue> dictionary, System.Collections.Generic.IComparer<TKey> comparer) { }
        public SortedList(int capacity) { }
        public SortedList(int capacity, System.Collections.Generic.IComparer<TKey> comparer) { }
        public int Capacity { get { throw null; } set { } }
        public System.Collections.Generic.IComparer<TKey> Comparer { get { throw null; } }
        public int Count { get { throw null; } }
        public TValue this[TKey key] { get { throw null; } set { } }
        public System.Collections.Generic.IList<TKey> Keys { get { throw null; } }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.IsReadOnly { get { throw null; } }
        System.Collections.Generic.ICollection<TKey> System.Collections.Generic.IDictionary<TKey,TValue>.Keys { get { throw null; } }
        System.Collections.Generic.ICollection<TValue> System.Collections.Generic.IDictionary<TKey,TValue>.Values { get { throw null; } }
        System.Collections.Generic.IEnumerable<TKey> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Keys { get { throw null; } }
        System.Collections.Generic.IEnumerable<TValue> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Values { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        bool System.Collections.IDictionary.IsFixedSize { get { throw null; } }
        bool System.Collections.IDictionary.IsReadOnly { get { throw null; } }
        object System.Collections.IDictionary.this[object key] { get { throw null; } set { } }
        System.Collections.ICollection System.Collections.IDictionary.Keys { get { throw null; } }
        System.Collections.ICollection System.Collections.IDictionary.Values { get { throw null; } }
        public System.Collections.Generic.IList<TValue> Values { get { throw null; } }
        public void Add(TKey key, TValue value) { }
        public void Clear() { }
        public bool ContainsKey(TKey key) { throw null; }
        public bool ContainsValue(TValue value) { throw null; }
        public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() { throw null; }
        public int IndexOfKey(TKey key) { throw null; }
        public int IndexOfValue(TValue value) { throw null; }
        public bool Remove(TKey key) { throw null; }
        public void RemoveAt(int index) { }
        void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { throw null; }
        void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex) { }
        bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> keyValuePair) { throw null; }
        System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey,TValue>>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int arrayIndex) { }
        void System.Collections.IDictionary.Add(object key, object value) { }
        bool System.Collections.IDictionary.Contains(object key) { throw null; }
        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() { throw null; }
        void System.Collections.IDictionary.Remove(object key) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        public void TrimExcess() { }
        public bool TryGetValue(TKey key, out TValue value) { throw null; }
    }
    public partial class SortedSet<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.Generic.ISet<T>, System.Collections.ICollection, System.Collections.IEnumerable, System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
    {
        public SortedSet() { }
        public SortedSet(System.Collections.Generic.IComparer<T> comparer) { }
        public SortedSet(System.Collections.Generic.IEnumerable<T> collection) { }
        public SortedSet(System.Collections.Generic.IEnumerable<T> collection, System.Collections.Generic.IComparer<T> comparer) { }
        protected SortedSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public System.Collections.Generic.IComparer<T> Comparer { get { throw null; } }
        public int Count { get { throw null; } }
        public T Max { get { throw null; } }
        public T Min { get { throw null; } }
        bool System.Collections.Generic.ICollection<T>.IsReadOnly { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        public bool Add(T item) { throw null; }
        public virtual void Clear() { }
        public virtual bool Contains(T item) { throw null; }
        public void CopyTo(T[] array) { }
        public void CopyTo(T[] array, int index) { }
        public void CopyTo(T[] array, int index, int count) { }
        public static System.Collections.Generic.IEqualityComparer<System.Collections.Generic.SortedSet<T>> CreateSetComparer() { throw null; }
        public static System.Collections.Generic.IEqualityComparer<System.Collections.Generic.SortedSet<T>> CreateSetComparer(System.Collections.Generic.IEqualityComparer<T> memberEqualityComparer) { throw null; }
        public void ExceptWith(System.Collections.Generic.IEnumerable<T> other) { }
        public System.Collections.Generic.SortedSet<T>.Enumerator GetEnumerator() { throw null; }
        protected virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        public virtual System.Collections.Generic.SortedSet<T> GetViewBetween(T lowerValue, T upperValue) { throw null; }
        public virtual void IntersectWith(System.Collections.Generic.IEnumerable<T> other) { }
        public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool IsSubsetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool IsSupersetOf(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        protected virtual void OnDeserialization(object sender) { }
        public bool Overlaps(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public bool Remove(T item) { throw null; }
        public int RemoveWhere(System.Predicate<T> match) { throw null; }
        public System.Collections.Generic.IEnumerable<T> Reverse() { throw null; }
        public bool SetEquals(System.Collections.Generic.IEnumerable<T> other) { throw null; }
        public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<T> other) { }
        void System.Collections.Generic.ICollection<T>.Add(T item) { }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int index) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(object sender) { }
        void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
#if NETSTANDARD2_1_OR_GREATER
        public bool TryGetValue(T equalValue, out T actualValue) { throw null; }
#endif
        public void UnionWith(System.Collections.Generic.IEnumerable<T> other) { }
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.Collections.IEnumerator, System.IDisposable, System.Runtime.Serialization.IDeserializationCallback, System.Runtime.Serialization.ISerializable
        {
            private object _dummy;
            public T Current { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
            void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(object sender) { }
            void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        }
    }
    public partial class Stack<T> : System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.ICollection, System.Collections.IEnumerable
    {
        public Stack() { }
        public Stack(System.Collections.Generic.IEnumerable<T> collection) { }
        public Stack(int capacity) { }
        public int Count { get { throw null; } }
        bool System.Collections.ICollection.IsSynchronized { get { throw null; } }
        object System.Collections.ICollection.SyncRoot { get { throw null; } }
        public void Clear() { }
        public bool Contains(T item) { throw null; }
        public void CopyTo(T[] array, int arrayIndex) { }
        public System.Collections.Generic.Stack<T>.Enumerator GetEnumerator() { throw null; }
        public T Peek() { throw null; }
        public T Pop() { throw null; }
        public void Push(T item) { }
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { throw null; }
        void System.Collections.ICollection.CopyTo(System.Array array, int arrayIndex) { }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { throw null; }
        public T[] ToArray() { throw null; }
        public void TrimExcess() { }
#if NETSTANDARD2_1_OR_GREATER
        public bool TryPeek(out T result) { throw null; }
        public bool TryPop(out T result) { throw null; }
#endif
        public partial struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.Collections.IEnumerator, System.IDisposable
        {
            private object _dummy;
            public T Current { get { throw null; } }
            object System.Collections.IEnumerator.Current { get { throw null; } }
            public void Dispose() { }
            public bool MoveNext() { throw null; }
            void System.Collections.IEnumerator.Reset() { }
        }
    }
}
