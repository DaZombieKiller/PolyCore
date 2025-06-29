using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
#if NETSTANDARD2_1_OR_GREATER
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _CollectionExtensions
#else
    public static class CollectionExtensions
#endif
    {
        /// <summary>Increase the capacity of this list to at least the specified <paramref name="capacity"/>.</summary>
        /// <param name="capacity">The minimum capacity to ensure.</param>
        private static void Grow<T>(List<T> list, int capacity)
        {
            list.Capacity = GetNewCapacity(list, capacity);
        }

        /// <summary>
        /// Enlarge this list so it may contain at least <paramref name="insertionCount"/> more elements
        /// And copy data to their after-insertion positions.
        /// This method is specifically for insertion, as it avoids 1 extra array copy.
        /// You should only call this method when Count + insertionCount > Capacity.
        /// </summary>
        /// <param name="indexToInsert">Index of the first insertion.</param>
        /// <param name="insertionCount">How many elements will be inserted.</param>
        private static void GrowForInsertion<T>(List<T> list, int indexToInsert, int insertionCount = 1)
        {
            Debug.Assert(insertionCount > 0);

            int requiredCapacity = checked(list._size + insertionCount);
            int newCapacity = GetNewCapacity(list, requiredCapacity);

            // Inline and adapt logic from set_Capacity

            T[] newItems = new T[newCapacity];
            if (indexToInsert != 0)
            {
                Array.Copy(list._items, newItems, length: indexToInsert);
            }

            if (list._size != indexToInsert)
            {
                Array.Copy(list._items, indexToInsert, newItems, indexToInsert + insertionCount, list._size - indexToInsert);
            }

            list._items = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetNewCapacity<T>(List<T> list, int capacity)
        {
            Debug.Assert(list._items.Length < capacity);

            const int DefaultCapacity = 4;
            int newCapacity = list._items.Length == 0 ? DefaultCapacity : 2 * list._items.Length;

            // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            if ((uint)newCapacity > Array.MaxLength) newCapacity = Array.MaxLength;

            // If the computed capacity is still less than specified, set to the original argument.
            // Capacities exceeding Array.MaxLength will be surfaced as OutOfMemoryException by Array.Resize.
            if (newCapacity < capacity) newCapacity = capacity;

            return newCapacity;
        }

        /// <summary>Adds the elements of the specified span to the end of the <see cref="List{T}"/>.</summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to which the elements should be added.</param>
        /// <param name="source">The span whose elements should be added to the end of the <see cref="List{T}"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="list"/> is null.</exception>
        public static void AddRange<T>(this List<T> list, params ReadOnlySpan<T> source)
        {
            if (list is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
            }

            if (!source.IsEmpty)
            {
                if (list._items.Length - list._size < source.Length)
                {
                    Grow(list, checked(list._size + source.Length));
                }

                source.CopyTo(list._items.AsSpan(list._size));
                list._size += source.Length;
                list._version++;
            }
        }

        /// <summary>Inserts the elements of a span into the <see cref="List{T}"/> at the specified index.</summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list into which the elements should be inserted.</param>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="source">The span whose elements should be added to the <see cref="List{T}"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="list"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <paramref name="list"/>'s <see cref="List{T}.Count"/>.</exception>
        public static void InsertRange<T>(this List<T> list, int index, scoped ReadOnlySpan<T> source)
        {
            if (list is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
            }

            if ((uint)index > (uint)list._size)
            {
                ThrowHelper.ThrowArgumentOutOfRange_IndexMustBeLessOrEqualException();
            }

            if (!source.IsEmpty)
            {
                if (list._items.Length - list._size < source.Length)
                {
                    GrowForInsertion(list, index, source.Length);
                }
                else if (index < list._size)
                {
                    // If the index at which to insert is less than the number of items in the list,
                    // shift all items past that location in the list down to the end, making room
                    // to copy in the new data.
                    Array.Copy(list._items, index, list._items, index + source.Length, list._size - index);
                }

                // Copy the source span into the list.
                // Note that this does not handle the unsafe case of trying to insert a CollectionsMarshal.AsSpan(list)
                // or some slice thereof back into the list itself; such an operation has undefined behavior.
                source.CopyTo(list._items.AsSpan(index));
                list._size += source.Length;
                list._version++;
            }
        }

        /// <summary>Copies the entire <see cref="List{T}"/> to a span.</summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list from which the elements are copied.</param>
        /// <param name="destination">The span that is the destination of the elements copied from <paramref name="list"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="list"/> is null.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="List{T}"/> is greater than the number of elements that the destination span can contain.</exception>
        public static void CopyTo<T>(this List<T> list, Span<T> destination)
        {
            if (list is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
            }

            new ReadOnlySpan<T>(list._items, 0, list._size).CopyTo(destination);
        }

        /// <summary>
        /// Returns a read-only <see cref="ReadOnlyCollection{T}"/> wrapper
        /// for the specified list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <returns>An object that acts as a read-only wrapper around the current <see cref="IList{T}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

        /// <summary>
        /// Returns a read-only <see cref="ReadOnlyDictionary{TKey, TValue}"/> wrapper
        /// for the current dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to wrap.</param>
        /// <returns>An object that acts as a read-only wrapper around the current <see cref="IDictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null.</exception>
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
            where TKey : notnull
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}

#if NETSTANDARD2_1_OR_GREATER
namespace PolyCore.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _System_Collections_Generic_CollectionExtensions
    {
        extension(CollectionExtensions)
        {
            /// <inheritdoc cref="_CollectionExtensions.AddRange{T}(List{T}, ReadOnlySpan{T})"/>
            public static void AddRange<T>(List<T> list, ReadOnlySpan<T> items) => _CollectionExtensions.AddRange(list, items);

            /// <inheritdoc cref="_CollectionExtensions.InsertRange{T}(List{T}, int, ReadOnlySpan{T})"/>
            public static void InsertRange<T>(List<T> list, int index, scoped ReadOnlySpan<T> source) => _CollectionExtensions.InsertRange(list, index, source);

            /// <inheritdoc cref="_CollectionExtensions.CopyTo{T}(List{T}, Span{T})"/>
            public static void CopyTo<T>(List<T> list, Span<T> destination) => _CollectionExtensions.CopyTo(list, destination);

            /// <inheritdoc cref="_CollectionExtensions.AsReadOnly{T}(IList{T})"/>
            public static ReadOnlyCollection<T> AsReadOnly<T>(IList<T> list) => _CollectionExtensions.AsReadOnly(list);

            /// <inheritdoc cref="_CollectionExtensions.AsReadOnly{TKey, TValue}(IDictionary{TKey, TValue})"/>
            public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue> dictionary) where TKey : notnull => _CollectionExtensions.AsReadOnly(dictionary);
        }
    }
}
#endif
