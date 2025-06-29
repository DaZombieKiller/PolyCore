using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

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

        /// <summary>
        /// Replaces the format item or items in a <see cref="CompositeFormat"/> with the string representation of the corresponding objects.
        /// A parameter supplies culture-specific formatting information.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static string Format<TArg0>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(1);
            return Format(provider, format, arg0, 0, 0, default);
        }

        /// <summary>
        /// Replaces the format item or items in a <see cref="CompositeFormat"/> with the string representation of the corresponding objects.
        /// A parameter supplies culture-specific formatting information.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static string Format<TArg0, TArg1>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0, TArg1 arg1)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(2);
            return Format(provider, format, arg0, arg1, 0, default);
        }

        /// <summary>
        /// Replaces the format item or items in a <see cref="CompositeFormat"/> with the string representation of the corresponding objects.
        /// A parameter supplies culture-specific formatting information.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
        /// <typeparam name="TArg2">The type of the third object to format.</typeparam>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static string Format<TArg0, TArg1, TArg2>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(3);
            return Format(provider, format, arg0, arg1, arg2, default);
        }

        /// <summary>
        /// Replaces the format item or items in a <see cref="CompositeFormat"/> with the string representation of the corresponding objects.
        /// A parameter supplies culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static string Format(IFormatProvider? provider, CompositeFormat format, params object?[] args)
        {
            ArgumentNullException.ThrowIfNull(format);
            ArgumentNullException.ThrowIfNull(args);
            return Format(provider, format, (ReadOnlySpan<object?>)args);
        }

        /// <summary>
        /// Replaces the format item or items in a <see cref="CompositeFormat"/> with the string representation of the corresponding objects.
        /// A parameter supplies culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="args">A span of objects to format.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static string Format(IFormatProvider? provider, CompositeFormat format, params ReadOnlySpan<object?> args)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(args.Length);
            return args.Length switch
            {
                0 => format.Format,
                1 => Format(provider, format, args[0], 0, 0, args),
                2 => Format(provider, format, args[0], args[1], 0, args),
                _ => Format(provider, format, args[0], args[1], args[2], args),
            };
        }

        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified span.
        /// </summary>
        /// <param name="format">A <see href="https://learn.microsoft.com/dotnet/standard/base-types/composite-formatting">composite format string</see>.</param>
        /// <param name="args">An object span that contains zero or more objects to format.</param>
        /// <returns>A copy of <paramref name="format"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.</returns>
        public static string Format([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params ReadOnlySpan<object?> args)
        {
            return FormatHelper(null, format, args);
        }

        /// <summary>
        /// Replaces the format items in a string with the string representations of corresponding objects in a specified span.
        /// A parameter supplies culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see href="https://learn.microsoft.com/dotnet/standard/base-types/composite-formatting">composite format string</see>.</param>
        /// <param name="args">An object span that contains zero or more objects to format.</param>
        /// <returns>A copy of <paramref name="format"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.</returns>
        public static string Format(IFormatProvider? provider, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params ReadOnlySpan<object?> args)
        {
            return FormatHelper(provider, format, args);
        }

        private static string Format<TArg0, TArg1, TArg2>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0, TArg1 arg1, TArg2 arg2, ReadOnlySpan<object?> args)
        {
            // If there's no formatting to be done, we can just return the original format string as the result.
            if (format._formattedCount == 0)
            {
                return format.Format;
            }

            // Create the interpolated string handler.
            var handler = new DefaultInterpolatedStringHandler(format._literalLength, format._formattedCount, provider, stackalloc char[string.StackallocCharBufferSizeLimit]);

            // Format each segment.
            foreach ((string? Literal, int ArgIndex, int Alignment, string? Format) segment in format._segments)
            {
                if (segment.Literal is string literal)
                {
                    handler.AppendLiteral(literal);
                }
                else
                {
                    int index = segment.ArgIndex;
                    switch (index)
                    {
                    case 0:
                        handler.AppendFormatted(arg0, segment.Alignment, segment.Format);
                        break;

                    case 1:
                        handler.AppendFormatted(arg1, segment.Alignment, segment.Format);
                        break;

                    case 2:
                        handler.AppendFormatted(arg2, segment.Alignment, segment.Format);
                        break;

                    default:
                        Debug.Assert(index > 2);
                        handler.AppendFormatted(args[index], segment.Alignment, segment.Format);
                        break;
                    }
                }
            }

            // Complete the operation.
            return handler.ToStringAndClear();
        }

        private static string FormatHelper(IFormatProvider? provider, string format, ReadOnlySpan<object?> args)
        {
            ArgumentNullException.ThrowIfNull(format);

            var sb = new ValueStringBuilder(stackalloc char[string.StackallocCharBufferSizeLimit]);
            sb.EnsureCapacity(format.Length + args.Length * 8);
            sb.AppendFormatHelper(provider, format, args);
            return sb.ToString();
        }
    }
}
