using System.ComponentModel;

namespace PolyCore.Internal;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_String
{
    extension(string @this)
    {
        internal static int MaxLength => 0x3FFFFFDF;

        internal static int StackallocCharBufferSizeLimit => 256;

        /// <summary>Copies the contents of this string into the destination span.</summary>
        /// <param name="destination">The span into which to copy this string's contents.</param>
        /// <exception cref="ArgumentException">The destination span is shorter than the source string.</exception>
        public void CopyTo(Span<char> destination) => @this.AsSpan().CopyTo(destination);

        /// <summary>Copies the contents of this string into the destination span.</summary>
        /// <param name="destination">The span into which to copy this string's contents.</param>
        /// <returns><see langword="true"/> if the data was copied; <see langword="false"/> if the destination was too short to fit the contents of the string.</returns>
        public bool TryCopyTo(Span<char> destination) => @this.AsSpan().TryCopyTo(destination);
    }
}
