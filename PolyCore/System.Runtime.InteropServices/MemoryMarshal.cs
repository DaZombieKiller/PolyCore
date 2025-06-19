using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PolyCore.Internal;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_Runtime_InteropServices_MemoryMarshal
{
    extension(MemoryMarshal)
    {
        /// <summary>
        /// Returns a reference to the 0th element of <paramref name="array"/>. If the array is empty, returns a reference to where the 0th element
        /// would have been stored. Such a reference may be used for pinning but must never be dereferenced.
        /// </summary>
        /// <exception cref="NullReferenceException"><paramref name="array"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method does not perform array variance checks. The caller must manually perform any array variance checks
        /// if the caller wishes to write to the returned reference.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T GetArrayDataReference<T>(T[] array)
        {
            fixed (T* pin = array)
            {
                return ref *(T*)Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
            }
        }

        /// <summary>
        /// Returns a reference to the 0th element of <paramref name="array"/>. If the array is empty, returns a reference to where the 0th element
        /// would have been stored. Such a reference may be used for pinning but must never be dereferenced.
        /// </summary>
        /// <exception cref="NullReferenceException"><paramref name="array"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The caller must manually reinterpret the returned <em>ref byte</em> as a ref to the array's underlying elemental type,
        /// perhaps utilizing an API such as <em>System.Runtime.CompilerServices.Unsafe.As</em> to assist with the reinterpretation.
        /// This technique does not perform array variance checks. The caller must manually perform any array variance checks
        /// if the caller wishes to write to the returned reference.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref byte GetArrayDataReference(Array array)
        {
            fixed (byte* pin = &Unsafe.As<StrongBox<byte>>(array).Value)
            {
                return ref *(byte*)Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
            }
        }

        /// <summary>Creates a new read-only span for a null-terminated string.</summary>
        /// <param name="value">The pointer to the null-terminated string of characters.</param>
        /// <returns>A read-only span representing the specified null-terminated string, or an empty span if the pointer is null.</returns>
        /// <remarks>The returned span does not include the null terminator.</remarks>
        /// <exception cref="ArgumentException">The string is longer than <see cref="int.MaxValue"/>.</exception>
        public static unsafe ReadOnlySpan<char> CreateReadOnlySpanFromNullTerminated(char* value)
        {
            int length = 0;

            if (value == null)
                return default;

            while (value[length] != 0)
                length++;

            return new ReadOnlySpan<char>(value, length);
        }

        /// <summary>Creates a new read-only span for a null-terminated UTF-8 string.</summary>
        /// <param name="value">The pointer to the null-terminated string of bytes.</param>
        /// <returns>A read-only span representing the specified null-terminated string, or an empty span if the pointer is null.</returns>
        /// <remarks>The returned span does not include the null terminator, nor does it validate the well-formedness of the UTF-8 data.</remarks>
        /// <exception cref="ArgumentException">The string is longer than <see cref="int.MaxValue"/>.</exception>
        public static unsafe ReadOnlySpan<byte> CreateReadOnlySpanFromNullTerminated(byte* value)
        {
            int length = 0;

            if (value == null)
                return default;

            while (value[length] != 0)
                length++;

            return new ReadOnlySpan<byte>(value, length);
        }

        /// <summary>
        /// Re-interprets a span of bytes as a reference to structure of type T.
        /// The type may not contain pointers or references. This is checked at runtime in order to preserve type safety.
        /// </summary>
        /// <remarks>
        /// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other means.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T AsRef<T>(Span<byte> span)
            where T : struct
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
            }
            if (sizeof(T) > (uint)span.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
            }
            return ref Unsafe.As<byte, T>(ref MemoryMarshal.GetReference(span));
        }

        /// <summary>
        /// Re-interprets a span of bytes as a reference to structure of type T.
        /// The type may not contain pointers or references. This is checked at runtime in order to preserve type safety.
        /// </summary>
        /// <remarks>
        /// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other means.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref readonly T AsRef<T>(ReadOnlySpan<byte> span)
            where T : struct
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
            }
            if (sizeof(T) > (uint)span.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
            }
            return ref Unsafe.As<byte, T>(ref MemoryMarshal.GetReference(span));
        }
    }
}
