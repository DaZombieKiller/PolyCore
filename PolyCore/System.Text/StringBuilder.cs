using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PolyCore.Internal;

[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class _System_Text_StringBuilder
{
    extension(StringBuilder @this)
    {
#if !NETSTANDARD2_1_OR_GREATER
        internal Span<char> RemainingCurrentChunk => @this.m_ChunkChars.AsSpan(@this.m_ChunkLength);

        // TODO: Add all of these for NS2.0 and make them public.
        internal StringBuilder Append(ReadOnlySpan<char> value)
        {
            @this.Append(ref MemoryMarshal.GetReference(value), value.Length);
            return @this;
        }
#endif

        /// <summary>
        /// GetChunks returns ChunkEnumerator that follows the IEnumerable pattern and
        /// thus can be used in a C# 'foreach' statements to retrieve the data in the StringBuilder
        /// as chunks (ReadOnlyMemory) of characters.  An example use is:
        ///
        ///      foreach (ReadOnlyMemory&lt;char&gt; chunk in sb.GetChunks())
        ///         foreach (char c in chunk.Span)
        ///             { /* operation on c }
        ///
        /// It is undefined what happens if the StringBuilder is modified while the chunk
        /// enumeration is incomplete.  StringBuilder is also not thread-safe, so operating
        /// on it with concurrent threads is illegal.  Finally the ReadOnlyMemory chunks returned
        /// are NOT guaranteed to remain unchanged if the StringBuilder is modified, so do
        /// not cache them for later use either.  This API's purpose is efficiently extracting
        /// the data of a CONSTANT StringBuilder.
        ///
        /// Creating a ReadOnlySpan from a ReadOnlyMemory  (the .Span property) is expensive
        /// compared to the fetching of the character, so create a local variable for the SPAN
        /// if you need to use it in a nested for statement.  For example
        ///
        ///    foreach (ReadOnlyMemory&lt;char&gt; chunk in sb.GetChunks())
        ///    {
        ///         var span = chunk.Span;
        ///         for (int i = 0; i &lt; span.Length; i++)
        ///             { /* operation on span[i] */ }
        ///    }
        /// </summary>
        public ChunkEnumerator GetChunks() => new ChunkEnumerator(@this);

        /// <summary>Appends the specified interpolated string to this instance.</summary>
        /// <param name="handler">The interpolated string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(
#if STRINGBUILDER_INTERPOLATEDSTRINGS
            [InterpolatedStringHandlerArgument("")]
#endif
            ref AppendInterpolatedStringHandler handler) => @this;

        /// <summary>Appends the specified interpolated string to this instance.</summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="handler">The interpolated string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(IFormatProvider? provider,
#if STRINGBUILDER_INTERPOLATEDSTRINGS
            [InterpolatedStringHandlerArgument("", nameof(provider))]
#endif
            ref AppendInterpolatedStringHandler handler) => @this;

        /// <summary>Appends the specified interpolated string followed by the default line terminator to the end of the current StringBuilder object.</summary>
        /// <param name="handler">The interpolated string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(
#if STRINGBUILDER_INTERPOLATEDSTRINGS
            [InterpolatedStringHandlerArgument("")]
#endif
            ref AppendInterpolatedStringHandler handler) => @this.AppendLine();

        /// <summary>Appends the specified interpolated string followed by the default line terminator to the end of the current StringBuilder object.</summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="handler">The interpolated string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(IFormatProvider? provider,
#if STRINGBUILDER_INTERPOLATEDSTRINGS
            [InterpolatedStringHandlerArgument("", nameof(provider))]
#endif
            ref AppendInterpolatedStringHandler handler) => @this.AppendLine();

        /// <summary>
        /// Concatenates the string representations of the elements in the provided span of objects, using the specified char separator between each member,
        /// then appends the result to the current instance of the string builder.
        /// </summary>
        /// <param name="separator">The character to use as a separator. <paramref name="separator"/> is included in the joined strings only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">A span that contains the strings to concatenate and append to the current instance of the string builder.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendJoin(char separator, params ReadOnlySpan<object?> values) =>
            @this.AppendJoinCore(ref separator, 1, values);

        /// <summary>
        /// Concatenates the strings of the provided span, using the specified char separator between each string,
        /// then appends the result to the current instance of the string builder.
        /// </summary>
        /// <param name="separator">The character to use as a separator. <paramref name="separator"/> is included in the joined strings only if <paramref name="values"/> has more than one element.</param>
        /// <param name="values">A span that contains the strings to concatenate and append to the current instance of the string builder.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendJoin(char separator, params ReadOnlySpan<string?> values) =>
            @this.AppendJoinCore(ref separator, 1, values);

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// Each format item is replaced by the string representation of a corresponding argument in a parameter span using a specified format provider.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">A span of objects to format.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="StringBuilder.MaxCapacity"/>.</exception>
        /// <exception cref="FormatException">
        /// <paramref name="format"/> is invalid.
        /// -or-
        /// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args"/> span.
        /// </exception>
        public StringBuilder AppendFormat(IFormatProvider? provider, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, params ReadOnlySpan<object?> args)
        {
            ArgumentNullException.ThrowIfNull(format);

            // Undocumented exclusive limits on the range for Argument Hole Index and Argument Hole Alignment.
            const int IndexLimit = 1_000_000; // Note:            0 <= ArgIndex < IndexLimit
            const int WidthLimit = 1_000_000; // Note:  -WidthLimit <  ArgAlign < WidthLimit

            // Query the provider (if one was supplied) for an ICustomFormatter.  If there is one,
            // it needs to be used to transform all arguments.
            ICustomFormatter? cf = (ICustomFormatter?)provider?.GetFormat(typeof(ICustomFormatter));

            // Repeatedly find the next hole and process it.
            int pos = 0;
            char ch;
            while (true)
            {
                // Skip until either the end of the input or the first unescaped opening brace, whichever comes first.
                // Along the way we need to also unescape escaped closing braces.
                while (true)
                {
                    // Find the next brace.  If there isn't one, the remainder of the input is text to be appended, and we're done.
                    if ((uint)pos >= (uint)format.Length)
                    {
                        return @this;
                    }

                    ReadOnlySpan<char> remainder = format.AsSpan(pos);
                    int countUntilNextBrace = remainder.IndexOfAny('{', '}');
                    if (countUntilNextBrace < 0)
                    {
                        @this.Append(remainder);
                        return @this;
                    }

                    // Append the text until the brace.
                    @this.Append(remainder.Slice(0, countUntilNextBrace));
                    pos += countUntilNextBrace;

                    // Get the brace.  It must be followed by another character, either a copy of itself in the case of being
                    // escaped, or an arbitrary character that's part of the hole in the case of an opening brace.
                    char brace = format[pos];
                    ch = MoveNext(format, ref pos);
                    if (brace == ch)
                    {
                        @this.Append(ch);
                        pos++;
                        continue;
                    }

                    // This wasn't an escape, so it must be an opening brace.
                    if (brace != '{')
                    {
                        ThrowHelper.ThrowFormatInvalidString(pos, ExceptionResource.Format_UnexpectedClosingBrace);
                    }

                    // Proceed to parse the hole.
                    break;
                }

                // We're now positioned just after the opening brace of an argument hole, which consists of
                // an opening brace, an index, an optional width preceded by a comma, and an optional format
                // preceded by a colon, with arbitrary amounts of spaces throughout.
                int width = 0;
                bool leftJustify = false;
                ReadOnlySpan<char> itemFormatSpan = default; // used if itemFormat is null

                // First up is the index parameter, which is of the form:
                //     at least on digit
                //     optional any number of spaces
                // We've already read the first digit into ch.
                Debug.Assert(format[pos - 1] == '{');
                Debug.Assert(ch != '{');
                int index = ch - '0';
                if ((uint)index >= 10u)
                {
                    ThrowHelper.ThrowFormatInvalidString(pos, ExceptionResource.Format_ExpectedAsciiDigit);
                }

                // Common case is a single digit index followed by a closing brace.  If it's not a closing brace,
                // proceed to finish parsing the full hole format.
                ch = MoveNext(format, ref pos);
                if (ch != '}')
                {
                    // Continue consuming optional additional digits.
                    while (char.IsAsciiDigit(ch) && index < IndexLimit)
                    {
                        index = index * 10 + ch - '0';
                        ch = MoveNext(format, ref pos);
                    }

                    // Consume optional whitespace.
                    while (ch == ' ')
                    {
                        ch = MoveNext(format, ref pos);
                    }

                    // Parse the optional alignment, which is of the form:
                    //     comma
                    //     optional any number of spaces
                    //     optional -
                    //     at least one digit
                    //     optional any number of spaces
                    if (ch == ',')
                    {
                        // Consume optional whitespace.
                        do
                        {
                            ch = MoveNext(format, ref pos);
                        }
                        while (ch == ' ');

                        // Consume an optional minus sign indicating left alignment.
                        if (ch == '-')
                        {
                            leftJustify = true;
                            ch = MoveNext(format, ref pos);
                        }

                        // Parse alignment digits. The read character must be a digit.
                        width = ch - '0';
                        if ((uint)width >= 10u)
                        {
                            ThrowHelper.ThrowFormatInvalidString(pos, ExceptionResource.Format_ExpectedAsciiDigit);
                        }
                        ch = MoveNext(format, ref pos);
                        while (char.IsAsciiDigit(ch) && width < WidthLimit)
                        {
                            width = width * 10 + ch - '0';
                            ch = MoveNext(format, ref pos);
                        }

                        // Consume optional whitespace
                        while (ch == ' ')
                        {
                            ch = MoveNext(format, ref pos);
                        }
                    }

                    // The next character needs to either be a closing brace for the end of the hole,
                    // or a colon indicating the start of the format.
                    if (ch != '}')
                    {
                        if (ch != ':')
                        {
                            // Unexpected character
                            ThrowHelper.ThrowFormatInvalidString(pos, ExceptionResource.Format_UnclosedFormatItem);
                        }

                        // Search for the closing brace; everything in between is the format,
                        // but opening braces aren't allowed.
                        int startingPos = pos;
                        while (true)
                        {
                            ch = MoveNext(format, ref pos);

                            if (ch == '}')
                            {
                                // Argument hole closed
                                break;
                            }

                            if (ch == '{')
                            {
                                // Braces inside the argument hole are not supported
                                ThrowHelper.ThrowFormatInvalidString(pos, ExceptionResource.Format_UnclosedFormatItem);
                            }
                        }

                        startingPos++;
                        itemFormatSpan = format.AsSpan(startingPos, pos - startingPos);
                    }
                }

                // Construct the output for this arg hole.
                Debug.Assert(format[pos] == '}');
                pos++;
                string? s = null;
                string? itemFormat = null;

                if ((uint)index >= (uint)args.Length)
                {
                    ThrowHelper.ThrowFormatIndexOutOfRange();
                }
                object? arg = args[index];

                if (cf != null)
                {
                    if (!itemFormatSpan.IsEmpty)
                    {
                        itemFormat = itemFormatSpan.ToString();
                    }

                    s = cf.Format(itemFormat, arg, provider);
                }

                if (s == null)
                {
                    // If arg is ISpanFormattable and the beginning doesn't need padding,
                    // try formatting it into the remaining current chunk.
                    if ((leftJustify || width == 0) &&
                        arg is ISpanFormattable spanFormattableArg &&
                        spanFormattableArg.TryFormat(@this.RemainingCurrentChunk, out int charsWritten, itemFormatSpan, provider))
                    {
                        if ((uint)charsWritten > (uint)@this.RemainingCurrentChunk.Length)
                        {
                            // Untrusted ISpanFormattable implementations might return an erroneous charsWritten value,
                            // and m_ChunkLength might end up being used in Unsafe code, so fail if we get back an
                            // out-of-range charsWritten value.
                            ThrowHelper.ThrowFormatInvalidString();
                        }

                        @this.m_ChunkLength += charsWritten;

                        // Pad the end, if needed.
                        if (leftJustify && width > charsWritten)
                        {
                            @this.Append(' ', width - charsWritten);
                        }

                        // Continue to parse other characters.
                        continue;
                    }

                    // Otherwise, fallback to trying IFormattable or calling ToString.
                    if (arg is IFormattable formattableArg)
                    {
                        if (itemFormatSpan.Length != 0)
                        {
                            itemFormat ??= itemFormatSpan.ToString();
                        }
                        s = formattableArg.ToString(itemFormat, provider);
                    }
                    else
                    {
                        s = arg?.ToString();
                    }

                    s ??= string.Empty;
                }

                // Append it to the final output of the Format String.
                if (width <= s.Length)
                {
                    @this.Append(s);
                }
                else if (leftJustify)
                {
                    @this.Append(s);
                    @this.Append(' ', width - s.Length);
                }
                else
                {
                    @this.Append(' ', width - s.Length);
                    @this.Append(s);
                }

                // Continue parsing the rest of the format string.
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static char MoveNext(string format, ref int pos)
            {
                pos++;
                if ((uint)pos >= (uint)format.Length)
                {
                    ThrowHelper.ThrowFormatInvalidString(pos, ExceptionResource.Format_UnclosedFormatItem);
                }
                return format[pos];
            }
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// Each format item is replaced by the string representation of any of the arguments using a specified format provider.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public StringBuilder AppendFormat<TArg0>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(1);
            return AppendFormat(@this, provider, format, arg0, 0, 0, default);
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// Each format item is replaced by the string representation of any of the arguments using a specified format provider.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public StringBuilder AppendFormat<TArg0, TArg1>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0, TArg1 arg1)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(2);
            return AppendFormat(@this, provider, format, arg0, arg1, 0, default);
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// Each format item is replaced by the string representation of any of the arguments using a specified format provider.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
        /// <typeparam name="TArg2">The type of the third object to format.</typeparam>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public StringBuilder AppendFormat<TArg0, TArg1, TArg2>(IFormatProvider? provider, CompositeFormat format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(3);
            return AppendFormat(@this, provider, format, arg0, arg1, arg2, default);
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// Each format item is replaced by the string representation of any of the arguments using a specified format provider.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public StringBuilder AppendFormat(IFormatProvider? provider, CompositeFormat format, params object?[] args)
        {
            ArgumentNullException.ThrowIfNull(format);
            ArgumentNullException.ThrowIfNull(args);
            return AppendFormat(@this, provider, format, (ReadOnlySpan<object?>)args);
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
        /// Each format item is replaced by the string representation of any of the arguments using a specified format provider.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="args">A span of objects to format.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public StringBuilder AppendFormat(IFormatProvider? provider, CompositeFormat format, params ReadOnlySpan<object?> args)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(args.Length);
            return args.Length switch
            {
                0 => AppendFormat(@this, provider, format, 0, 0, 0, args),
                1 => AppendFormat(@this, provider, format, args[0], 0, 0, args),
                2 => AppendFormat(@this, provider, format, args[0], args[1], 0, args),
                _ => AppendFormat(@this, provider, format, args[0], args[1], args[2], args),
            };
        }

        private StringBuilder AppendJoinCore<T>(ref char separator, int separatorLength, ReadOnlySpan<T> values)
        {
            if (values.IsEmpty)
            {
                return @this;
            }

            if (values[0] != null)
            {
                @this.Append(values[0]!.ToString());
            }

            for (int i = 1; i < values.Length; i++)
            {
                @this.Append(ref separator, separatorLength);

                if (values[i] != null)
                {
                    @this.Append(values[i]!.ToString());
                }
            }

            return @this;
        }

        private StringBuilder Append(ref char value, int valueCount)
        {
#if NETSTANDARD2_1_OR_GREATER
            return @this.Append(MemoryMarshal.CreateSpan(ref value, valueCount));
#else
            unsafe
            {
                fixed (char* pValue = &value)
                {
                    return @this.Append(pValue, valueCount);
                }
            }
#endif
        }
    }

    private static StringBuilder AppendFormat<TArg0, TArg1, TArg2>(StringBuilder @this, IFormatProvider? provider, CompositeFormat format, TArg0 arg0, TArg1 arg1, TArg2 arg2, ReadOnlySpan<object?> args)
    {
        // Create the interpolated string handler.
        var handler = new AppendInterpolatedStringHandler(format._literalLength, format._formattedCount, @this, provider);

        // Append each segment.
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
        return @this.Append(ref handler);
    }

    /// <summary>
    /// ChunkEnumerator supports both the IEnumerable and IEnumerator pattern so foreach
    /// works (see GetChunks).  It needs to be public (so the compiler can use it
    /// when building a foreach statement) but users typically don't use it explicitly.
    /// (which is why it is a nested type).
    /// </summary>
    public struct ChunkEnumerator
    {
        private readonly StringBuilder _firstChunk; // The first Stringbuilder chunk (which is the end of the logical string)
        private StringBuilder? _currentChunk;        // The chunk that this enumerator is currently returning (Current).
        private readonly ManyChunkInfo? _manyChunks; // Only used for long string builders with many chunks (see constructor)

        /// <summary>
        /// Implement IEnumerable.GetEnumerator() to return  'this' as the IEnumerator
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)] // Only here to make foreach work
        public ChunkEnumerator GetEnumerator() { return this; }

        /// <summary>
        /// Implements the IEnumerator pattern.
        /// </summary>
        public bool MoveNext()
        {
            if (_currentChunk == _firstChunk)
            {
                return false;
            }


            if (_manyChunks != null)
            {
                return _manyChunks.MoveNext(ref _currentChunk);
            }

            StringBuilder next = _firstChunk;
            while (next.m_ChunkPrevious != _currentChunk)
            {
                Debug.Assert(next.m_ChunkPrevious != null);
                next = next.m_ChunkPrevious;
            }
            _currentChunk = next;
            return true;
        }

        /// <summary>
        /// Implements the IEnumerator pattern.
        /// </summary>
        public ReadOnlyMemory<char> Current
        {
            get
            {
                if (_currentChunk == null)
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                }

                return new ReadOnlyMemory<char>(_currentChunk.m_ChunkChars, 0, _currentChunk.m_ChunkLength);
            }
        }

        #region private
        internal ChunkEnumerator(StringBuilder stringBuilder)
        {
            Debug.Assert(stringBuilder != null);
            _firstChunk = stringBuilder;
            _currentChunk = null;   // MoveNext will find the last chunk if we do this.
            _manyChunks = null;

            // There is a performance-vs-allocation tradeoff.   Because the chunks
            // are a linked list with each chunk pointing to its PREDECESSOR, walking
            // the list FORWARD is not efficient.   If there are few chunks (< 8) we
            // simply scan from the start each time, and tolerate the N*N behavior.
            // However above this size, we allocate an array to hold reference to all
            // the chunks and we can be efficient for large N.
            int chunkCount = ChunkCount(stringBuilder);
            if (8 < chunkCount)
            {
                _manyChunks = new ManyChunkInfo(stringBuilder, chunkCount);
            }
        }

        private static int ChunkCount(StringBuilder? stringBuilder)
        {
            int ret = 0;
            while (stringBuilder != null)
            {
                ret++;
                stringBuilder = stringBuilder.m_ChunkPrevious;
            }
            return ret;
        }

        /// <summary>
        /// Used to hold all the chunks indexes when you have many chunks.
        /// </summary>
        private sealed class ManyChunkInfo
        {
            private readonly StringBuilder[] _chunks;    // These are in normal order (first chunk first)
            private int _chunkPos;

            public bool MoveNext(ref StringBuilder? current)
            {
                int pos = ++_chunkPos;
                if (_chunks.Length <= pos)
                {
                    return false;
                }
                current = _chunks[pos];
                return true;
            }

            public ManyChunkInfo(StringBuilder? stringBuilder, int chunkCount)
            {
                _chunks = new StringBuilder[chunkCount];
                while (0 <= --chunkCount)
                {
                    Debug.Assert(stringBuilder != null);
                    _chunks[chunkCount] = stringBuilder;
                    stringBuilder = stringBuilder.m_ChunkPrevious;
                }
                _chunkPos = -1;
            }
        }
        #endregion
    }
}
