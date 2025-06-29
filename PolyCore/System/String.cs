using System.Buffers;
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

#if !NETSTANDARD2_1_OR_GREATER
        public static string Create<TState>(int length, TState state, SpanAction<char, TState> action)
        {
            if (action is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
            }

            if (length <= 0)
            {
                if (length == 0)
                {
                    return string.Empty;
                }

                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
            }

            string result = new('\0', length);
            
            unsafe
            {
                fixed (char* pResult = result)
                {
                    action(new Span<char>(pResult, length), state);
                }
            }

            return result;
        }
#endif

        /// <summary>Creates a new string by using the specified provider to control the formatting of the specified interpolated string.</summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="handler">The interpolated string.</param>
        /// <returns>The string that results for formatting the interpolated string using the specified format provider.</returns>
        public static string Create(IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(provider))] ref DefaultInterpolatedStringHandler handler) =>
            handler.ToStringAndClear();

        /// <summary>Creates a new string by using the specified provider to control the formatting of the specified interpolated string.</summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="initialBuffer">The initial buffer that may be used as temporary space as part of the formatting operation. The contents of this buffer may be overwritten.</param>
        /// <param name="handler">The interpolated string.</param>
        /// <returns>The string that results for formatting the interpolated string using the specified format provider.</returns>
        public static string Create(IFormatProvider? provider, Span<char> initialBuffer, [InterpolatedStringHandlerArgument(nameof(provider), nameof(initialBuffer))] ref DefaultInterpolatedStringHandler handler) =>
            handler.ToStringAndClear();

        /// <summary>Copies the contents of this string into the destination span.</summary>
        /// <param name="destination">The span into which to copy this string's contents.</param>
        /// <exception cref="ArgumentException">The destination span is shorter than the source string.</exception>
        public void CopyTo(Span<char> destination) => @this.AsSpan().CopyTo(destination);

        /// <summary>Copies the contents of this string into the destination span.</summary>
        /// <param name="destination">The span into which to copy this string's contents.</param>
        /// <returns><see langword="true"/> if the data was copied; <see langword="false"/> if the destination was too short to fit the contents of the string.</returns>
        public bool TryCopyTo(Span<char> destination) => @this.AsSpan().TryCopyTo(destination);

        /// <summary>
        /// Replaces all newline sequences in the current string with <see cref="Environment.NewLine"/>.
        /// </summary>
        /// <returns>
        /// A string whose contents match the current string, but with all newline sequences replaced
        /// with <see cref="Environment.NewLine"/>.
        /// </returns>
        /// <remarks>
        /// This method searches for all newline sequences within the string and canonicalizes them to match
        /// the newline sequence for the current environment. For example, when running on Windows, all
        /// occurrences of non-Windows newline sequences will be replaced with the sequence CRLF. When
        /// running on Unix, all occurrences of non-Unix newline sequences will be replaced with
        /// a single LF character.
        ///
        /// It is not recommended that protocol parsers utilize this API. Protocol specifications often
        /// mandate specific newline sequences. For example, HTTP/1.1 (RFC 8615) mandates that the request
        /// line, status line, and headers lines end with CRLF. Since this API operates over a wide range
        /// of newline sequences, a protocol parser utilizing this API could exhibit behaviors unintended
        /// by the protocol's authors.
        ///
        /// This overload is equivalent to calling <see cref="ReplaceLineEndings(string)"/>, passing
        /// <see cref="Environment.NewLine"/> as the <em>replacementText</em> parameter.
        ///
        /// This method is guaranteed O(n) complexity, where <em>n</em> is the length of the input string.
        /// </remarks>
        public string ReplaceLineEndings() => ReplaceLineEndings(Environment.NewLine);

        /// <summary>
        /// Replaces all newline sequences in the current string with <paramref name="replacementText"/>.
        /// </summary>
        /// <returns>
        /// A string whose contents match the current string, but with all newline sequences replaced
        /// with <paramref name="replacementText"/>.
        /// </returns>
        /// <remarks>
        /// This method searches for all newline sequences within the string and canonicalizes them to the
        /// newline sequence provided by <paramref name="replacementText"/>. If <paramref name="replacementText"/>
        /// is <see cref="string.Empty"/>, all newline sequences within the string will be removed.
        ///
        /// It is not recommended that protocol parsers utilize this API. Protocol specifications often
        /// mandate specific newline sequences. For example, HTTP/1.1 (RFC 8615) mandates that the request
        /// line, status line, and headers lines end with CRLF. Since this API operates over a wide range
        /// of newline sequences, a protocol parser utilizing this API could exhibit behaviors unintended
        /// by the protocol's authors.
        ///
        /// The list of recognized newline sequences is CR (U+000D), LF (U+000A), CRLF (U+000D U+000A),
        /// NEL (U+0085), LS (U+2028), FF (U+000C), and PS (U+2029). This list is given by the Unicode
        /// Standard, Sec. 5.8, Recommendation R4 and Table 5-2.
        ///
        /// This method is guaranteed O(n * r) complexity, where <em>n</em> is the length of the input string,
        /// and where <em>r</em> is the length of <paramref name="replacementText"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReplaceLineEndings(string replacementText)
        {
            return replacementText == "\n"
                ? @this.ReplaceLineEndingsWithLineFeed()
                : @this.ReplaceLineEndingsCore(replacementText);
        }

        private string ReplaceLineEndingsCore(string replacementText)
        {
            ArgumentNullException.ThrowIfNull(replacementText);

            // Early-exit: do we need to do anything at all?
            // If not, return this string as-is.
            int idxOfFirstNewlineChar = IndexOfNewlineChar(@this, replacementText, out int stride);
            if (idxOfFirstNewlineChar < 0)
            {
                return @this;
            }

            // While writing to the builder, we don't bother memcpying the first
            // or the last segment into the builder. We'll use the builder only
            // for the intermediate segments, then we'll sandwich everything together
            // with one final string.Concat call.

            ReadOnlySpan<char> firstSegment = @this.AsSpan(0, idxOfFirstNewlineChar);
            ReadOnlySpan<char> remaining = @this.AsSpan(idxOfFirstNewlineChar + stride);

            var builder = new ValueStringBuilder(stackalloc char[string.StackallocCharBufferSizeLimit]);
            while (true)
            {
                int idx = IndexOfNewlineChar(remaining, replacementText, out stride);
                if (idx < 0) { break; } // no more newline chars
                builder.Append(replacementText);
                builder.Append(remaining.Slice(0, idx));
                remaining = remaining.Slice(idx + stride);
            }

            string retVal = string.Concat(firstSegment, builder.AsSpan(), replacementText, remaining);
            builder.Dispose();
            return retVal;
        }

        // Scans the input text, returning the index of the first newline char other than the replacement text.
        // Newline chars are given by the Unicode Standard, Sec. 5.8.
        private static int IndexOfNewlineChar(ReadOnlySpan<char> text, string replacementText, out int stride)
        {
            // !! IMPORTANT !!
            //
            // We expect this method may be called with untrusted input, which means we need to
            // bound the worst-case runtime of this method. We rely on MemoryExtensions.IndexOfAny
            // having worst-case runtime O(i), where i is the index of the first needle match within
            // the haystack; or O(n) if no needle is found. This ensures that in the common case
            // of this method being called within a loop, the worst-case runtime is O(n) rather than
            // O(n^2), where n is the length of the input text.

            stride = default;
            int offset = 0;

            while (true)
            {
                int idx = text.IndexOfAny(SearchValuesStorage.NewLineChars);

                if ((uint)idx >= (uint)text.Length)
                {
                    return -1;
                }

                offset += idx;
                stride = 1; // needle found

                // Did we match CR? If so, and if it's followed by LF, then we need
                // to consume both chars as a single newline function match.

                if (text[idx] == '\r')
                {
                    int nextCharIdx = idx + 1;
                    if ((uint)nextCharIdx < (uint)text.Length && text[nextCharIdx] == '\n')
                    {
                        stride = 2;

                        if (replacementText != "\r\n")
                        {
                            return offset;
                        }
                    }
                    else if (replacementText != "\r")
                    {
                        return offset;
                    }
                }
                else if (replacementText.Length != 1 || replacementText[0] != text[idx])
                {
                    return offset;
                }

                offset += stride;
                text = text.Slice(idx + stride);
            }
        }

        private string ReplaceLineEndingsWithLineFeed()
        {
            // If we are going to replace the new line with a line feed ('\n'),
            // we can skip looking for it to avoid breaking out of the vectorized path unnecessarily.
            int idxOfFirstNewlineChar = @this.AsSpan().IndexOfAny(SearchValuesStorage.NewLineCharsExceptLineFeed);
            if ((uint)idxOfFirstNewlineChar >= (uint)@this.Length)
            {
                return @this;
            }

            int stride = @this[idxOfFirstNewlineChar] == '\r' &&
                (uint)(idxOfFirstNewlineChar + 1) < (uint)@this.Length &&
                @this[idxOfFirstNewlineChar + 1] == '\n' ? 2 : 1;

            ReadOnlySpan<char> remaining = @this.AsSpan(idxOfFirstNewlineChar + stride);

            var builder = new ValueStringBuilder(stackalloc char[string.StackallocCharBufferSizeLimit]);
            while (true)
            {
                int idx = remaining.IndexOfAny(SearchValuesStorage.NewLineCharsExceptLineFeed);
                if ((uint)idx >= (uint)remaining.Length) break; // no more newline chars
                stride = remaining[idx] == '\r' && (uint)(idx + 1) < (uint)remaining.Length && remaining[idx + 1] == '\n' ? 2 : 1;
                builder.Append('\n');
                builder.Append(remaining.Slice(0, idx));
                remaining = remaining.Slice(idx + stride);
            }

            builder.Append('\n');
            string retVal = string.Concat(@this.AsSpan(0, idxOfFirstNewlineChar), builder.AsSpan(), remaining);
            builder.Dispose();
            return retVal;
        }

        public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1)
        {
            int length = checked(str0.Length + str1.Length);

            if (length == 0)
                return string.Empty;

            unsafe
            {
                nuint ptr0 = (nuint)(&str0);
                nuint ptr1 = (nuint)(&str1);

                return string.Create(str0.Length + str1.Length, (ptr0, ptr1), (resultSpan, tuple) =>
                {
                    var str0 = *(ReadOnlySpan<char>*)tuple.ptr0;
                    var str1 = *(ReadOnlySpan<char>*)tuple.ptr1;
                    str0.CopyTo(resultSpan);
                    str1.CopyTo(resultSpan.Slice(str0.Length));
                });
            }
        }

        public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1, ReadOnlySpan<char> str2)
        {
            int length = checked(str0.Length + str1.Length);

            if (length == 0)
                return string.Empty;

            unsafe
            {
                nuint ptr0 = (nuint)(&str0);
                nuint ptr1 = (nuint)(&str1);
                nuint ptr2 = (nuint)(&str2);

                return string.Create(str0.Length + str1.Length + str2.Length, (ptr0, ptr1, ptr2), (resultSpan, tuple) =>
                {
                    var str0 = *(ReadOnlySpan<char>*)tuple.ptr0;
                    var str1 = *(ReadOnlySpan<char>*)tuple.ptr1;
                    var str2 = *(ReadOnlySpan<char>*)tuple.ptr2;

                    str0.CopyTo(resultSpan);
                    resultSpan = resultSpan.Slice(str0.Length);

                    str1.CopyTo(resultSpan);
                    resultSpan = resultSpan.Slice(str1.Length);

                    str2.CopyTo(resultSpan);
                });
            }
        }

        public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1, ReadOnlySpan<char> str2, ReadOnlySpan<char> str3)
        {
            int length = checked(str0.Length + str1.Length);

            if (length == 0)
                return string.Empty;

            unsafe
            {
                nuint ptr0 = (nuint)(&str0);
                nuint ptr1 = (nuint)(&str1);
                nuint ptr2 = (nuint)(&str2);
                nuint ptr3 = (nuint)(&str3);

                return string.Create(str0.Length + str1.Length + str2.Length + str3.Length, (ptr0, ptr1, ptr2, ptr3), (resultSpan, tuple) =>
                {
                    var str0 = *(ReadOnlySpan<char>*)tuple.ptr0;
                    var str1 = *(ReadOnlySpan<char>*)tuple.ptr1;
                    var str2 = *(ReadOnlySpan<char>*)tuple.ptr2;
                    var str3 = *(ReadOnlySpan<char>*)tuple.ptr3;

                    str0.CopyTo(resultSpan);
                    resultSpan = resultSpan.Slice(str0.Length);

                    str1.CopyTo(resultSpan);
                    resultSpan = resultSpan.Slice(str1.Length);

                    str2.CopyTo(resultSpan);
                    resultSpan = resultSpan.Slice(str2.Length);

                    str3.CopyTo(resultSpan);
                });
            }
        }

        /// <summary>
        /// Concatenates the string representations of the elements in a specified span of objects.
        /// </summary>
        /// <param name="args">A span of objects that contains the elements to concatenate.</param>
        /// <returns>The concatenated string representations of the values of the elements in <paramref name="args"/>.</returns>
        public static string Concat(params ReadOnlySpan<object?> args)
        {
            if (args.Length <= 1)
            {
                return args.IsEmpty ?
                    string.Empty :
                    args[0]?.ToString() ?? string.Empty;
            }

            // We need to get an intermediary string array
            // to fill with each of the args' ToString(),
            // and then just concat that in one operation.

            // This way we avoid any intermediary string representations,
            // or buffer resizing if we use StringBuilder (although the
            // latter case is partially alleviated due to StringBuilder's
            // linked-list style implementation)

            var strings = new string[args.Length];

            int totalLength = 0;

            for (int i = 0; i < args.Length; i++)
            {
                object? value = args[i];

                string toString = value?.ToString() ?? string.Empty; // We need to handle both the cases when value or value.ToString() is null
                strings[i] = toString;

                totalLength += toString.Length;

                if (totalLength < 0) // Check for a positive overflow
                {
                    ThrowHelper.ThrowOutOfMemoryException_StringTooLong();
                }
            }

            // If all of the ToStrings are null/empty, just return string.Empty
            if (totalLength == 0)
            {
                return string.Empty;
            }

            return string.Create(totalLength, strings, (result, strings) =>
            {
                int position = 0; // How many characters we've copied so far

                for (int i = 0; i < strings.Length; i++)
                {
                    string s = strings[i];

                    Debug.Assert(s != null);
                    Debug.Assert(position <= totalLength - s.Length, "We didn't allocate enough space for the result string!");

                    s.CopyTo(result.Slice(position));
                    position += s.Length;
                }
            });
        }

        /// <summary>
        /// Concatenates the elements of a specified span of <see cref="string"/>.
        /// </summary>
        /// <param name="values">A span of <see cref="string"/> instances.</param>
        /// <returns>The concatenated elements of <paramref name="values"/>.</returns>
        public static string Concat(params ReadOnlySpan<string?> values)
        {
            if (values.Length <= 1)
            {
                return values.IsEmpty ?
                    string.Empty :
                    values[0] ?? string.Empty;
            }

            // It's possible that the input values array could be changed concurrently on another
            // thread, such that we can't trust that each read of values[i] will be equivalent.
            // Worst case, we can make a defensive copy of the array and use that, but we first
            // optimistically try the allocation and copies assuming that the array isn't changing,
            // which represents the 99.999% case, in particular since string.Concat is used for
            // string concatenation by the languages, with the input array being a params array.

            // Sum the lengths of all input strings
            long totalLengthLong = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string? value = values[i];
                if (value != null)
                {
                    totalLengthLong += value.Length;
                }
            }

            // If it's too long, fail, or if it's empty, return an empty string.
            if (totalLengthLong > int.MaxValue)
            {
                ThrowHelper.ThrowOutOfMemoryException_StringTooLong();
            }
            int totalLength = (int)totalLengthLong;
            if (totalLength == 0)
            {
                return string.Empty;
            }

            // Allocate a new string and copy each input string into it

            string result;
            int copiedLength = 0;

            unsafe
            {
                nuint valuesPtr = (nuint)(&values);
                nuint copiedLengthPtr = (nuint)(&copiedLength);

                result = string.Create(totalLength, (valuesPtr, copiedLengthPtr), (result, tuple) =>
                {
                    var values = *(ReadOnlySpan<string?>*)tuple.valuesPtr;
                    ref var copiedLength = ref *(int*)tuple.copiedLengthPtr;

                    for (int i = 0; i < values.Length; i++)
                    {
                        string? value = values[i];
                        if (!string.IsNullOrEmpty(value))
                        {
                            int valueLen = value.Length;
                            if (valueLen > totalLength - copiedLength)
                            {
                                copiedLength = -1;
                                return;
                            }

                            value.CopyTo(result.Slice(copiedLength));
                            copiedLength += valueLen;
                        }
                    }
                });
            }

            // If we copied exactly the right amount, return the new string.  Otherwise,
            // something changed concurrently to mutate the input array: fall back to
            // doing the concatenation again, but this time with a defensive copy. This
            // fall back should be extremely rare.
            return copiedLength == totalLength ? result : Concat((ReadOnlySpan<string?>)values.ToArray());
        }

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

    internal static class SearchValuesStorage
    {
        public const string NewLineCharsExceptLineFeed = "\r\f\u0085\u2028\u2029";
        public const string NewLineChars = NewLineCharsExceptLineFeed + "\n";
        public const string WhiteSpaceChars = "\t\n\v\f\r\u0020\u0085\u00a0\u1680\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u2028\u2029\u202f\u205f\u3000";
    }
}
