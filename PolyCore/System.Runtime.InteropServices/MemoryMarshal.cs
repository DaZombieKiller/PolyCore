using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace @implicit;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_Runtime_InteropServices_MemoryMarshal
{
    extension(MemoryMarshal)
    {
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
