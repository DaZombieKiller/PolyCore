using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _MemoryExtensions
    {
        /// <summary>Writes the specified interpolated string to the character span.</summary>
        /// <param name="destination">The span to which the interpolated string should be formatted.</param>
        /// <param name="handler">The interpolated string.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <returns>true if the entire interpolated string could be formatted successfully; otherwise, false.</returns>
        public static bool TryWrite(this Span<char> destination, [InterpolatedStringHandlerArgument(nameof(destination))] ref TryWriteInterpolatedStringHandler handler, out int charsWritten)
        {
            // The span argument isn't used directly in the method; rather, it'll be used by the compiler to create the handler.
            // We could validate here that span == handler._destination, but that doesn't seem necessary.
            if (handler._success)
            {
                charsWritten = handler._pos;
                return true;
            }

            charsWritten = 0;
            return false;
        }

        /// <summary>Writes the specified interpolated string to the character span.</summary>
        /// <param name="destination">The span to which the interpolated string should be formatted.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="handler">The interpolated string.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <returns>true if the entire interpolated string could be formatted successfully; otherwise, false.</returns>
        public static bool TryWrite(this Span<char> destination, IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(destination), nameof(provider))] ref TryWriteInterpolatedStringHandler handler, out int charsWritten) =>
            // The provider is passed to the handler by the compiler, so the actual implementation of the method
            // is the same as the non-provider overload.
            TryWrite(destination, ref handler, out charsWritten);

        /// <summary>
        /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
        /// with the string representation of the corresponding arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <param name="destination">The span to which the string should be formatted.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static bool TryWrite<TArg0>(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(1);
            return TryWrite(destination, provider, format, out charsWritten, arg0, 0, 0, default);
        }

        /// <summary>
        /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
        /// with the string representation of the corresponding arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
        /// <param name="destination">The span to which the string should be formatted.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static bool TryWrite<TArg0, TArg1>(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(2);
            return TryWrite(destination, provider, format, out charsWritten, arg0, arg1, 0, default);
        }

        /// <summary>
        /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
        /// with the string representation of the corresponding arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
        /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
        /// <typeparam name="TArg2">The type of the third object to format.</typeparam>
        /// <param name="destination">The span to which the string should be formatted.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static bool TryWrite<TArg0, TArg1, TArg2>(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(3);
            return TryWrite(destination, provider, format, out charsWritten, arg0, arg1, arg2, default);
        }

        /// <summary>
        /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
        /// with the string representation of the corresponding arguments.
        /// </summary>
        /// <param name="destination">The span to which the string should be formatted.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static bool TryWrite(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, params object?[] args)
        {
            ArgumentNullException.ThrowIfNull(format);
            ArgumentNullException.ThrowIfNull(args);
            return TryWrite(destination, provider, format, out charsWritten, (ReadOnlySpan<object?>)args);
        }

        /// <summary>
        /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
        /// with the string representation of the corresponding arguments.
        /// </summary>
        /// <param name="destination">The span to which the string should be formatted.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A <see cref="CompositeFormat"/>.</param>
        /// <param name="charsWritten">The number of characters written to the span.</param>
        /// <param name="args">A span of objects to format.</param>
        /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
        public static bool TryWrite(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, params ReadOnlySpan<object?> args)
        {
            ArgumentNullException.ThrowIfNull(format);
            format.ValidateNumberOfArgs(args.Length);
            return args.Length switch
            {
                0 => TryWrite(destination, provider, format, out charsWritten, 0, 0, 0, args),
                1 => TryWrite(destination, provider, format, out charsWritten, args[0], 0, 0, args),
                2 => TryWrite(destination, provider, format, out charsWritten, args[0], args[1], 0, args),
                _ => TryWrite(destination, provider, format, out charsWritten, args[0], args[1], args[2], args),
            };
        }

        private static bool TryWrite<TArg0, TArg1, TArg2>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1, TArg2 arg2, ReadOnlySpan<object?> args)
        {
            // Create the interpolated string handler.
            var handler = new TryWriteInterpolatedStringHandler(format._literalLength, format._formattedCount, destination, provider, out bool shouldAppend);

            if (shouldAppend)
            {
                // Write each segment.
                foreach ((string? Literal, int ArgIndex, int Alignment, string? Format) segment in format._segments)
                {
                    bool appended;
                    if (segment.Literal is string literal)
                    {
                        appended = handler.AppendLiteral(literal);
                    }
                    else
                    {
                        int index = segment.ArgIndex;
                        switch (index)
                        {
                        case 0:
                            appended = handler.AppendFormatted(arg0, segment.Alignment, segment.Format);
                            break;

                        case 1:
                            appended = handler.AppendFormatted(arg1, segment.Alignment, segment.Format);
                            break;

                        case 2:
                            appended = handler.AppendFormatted(arg2, segment.Alignment, segment.Format);
                            break;

                        default:
                            Debug.Assert(index > 2);
                            appended = handler.AppendFormatted(args[index], segment.Alignment, segment.Format);
                            break;
                        }
                    }

                    if (!appended)
                    {
                        break;
                    }
                }
            }

            // Complete the operation.
            return TryWrite(destination, provider, ref handler, out charsWritten);
        }

        /// <summary>Provides a handler used by the language compiler to format interpolated strings into character spans.</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [InterpolatedStringHandler]
        public ref struct TryWriteInterpolatedStringHandler
        {
            // Implementation note:
            // As this type is only intended to be targeted by the compiler, public APIs eschew argument validation logic
            // in a variety of places, e.g. allowing a null input when one isn't expected to produce a NullReferenceException rather
            // than an ArgumentNullException.

            /// <summary>The destination buffer.</summary>
            private readonly Span<char> _destination;
            /// <summary>Optional provider to pass to IFormattable.ToString or ISpanFormattable.TryFormat calls.</summary>
            private readonly IFormatProvider? _provider;
            /// <summary>The number of characters written to <see cref="_destination"/>.</summary>
            internal int _pos;
            /// <summary>true if all formatting operations have succeeded; otherwise, false.</summary>
            internal bool _success;
            /// <summary>Whether <see cref="_provider"/> provides an ICustomFormatter.</summary>
            /// <remarks>
            /// Custom formatters are very rare.  We want to support them, but it's ok if we make them more expensive
            /// in order to make them as pay-for-play as possible.  So, we avoid adding another reference type field
            /// to reduce the size of the handler and to reduce required zero'ing, by only storing whether the provider
            /// provides a formatter, rather than actually storing the formatter.  This in turn means, if there is a
            /// formatter, we pay for the extra interface call on each AppendFormatted that needs it.
            /// </remarks>
            private readonly bool _hasCustomFormatter;

            /// <summary>Creates a handler used to write an interpolated string into a <see cref="Span{Char}"/>.</summary>
            /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
            /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
            /// <param name="destination">The destination buffer.</param>
            /// <param name="shouldAppend">Upon return, true if the destination may be long enough to support the formatting, or false if it won't be.</param>
            /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
            public TryWriteInterpolatedStringHandler(int literalLength, int formattedCount, Span<char> destination, out bool shouldAppend)
            {
                _destination = destination;
                _provider = null;
                _pos = 0;
                _success = shouldAppend = destination.Length >= literalLength;
                _hasCustomFormatter = false;
            }

            /// <summary>Creates a handler used to write an interpolated string into a <see cref="Span{Char}"/>.</summary>
            /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
            /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
            /// <param name="destination">The destination buffer.</param>
            /// <param name="provider">An object that supplies culture-specific formatting information.</param>
            /// <param name="shouldAppend">Upon return, true if the destination may be long enough to support the formatting, or false if it won't be.</param>
            /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
            public TryWriteInterpolatedStringHandler(int literalLength, int formattedCount, Span<char> destination, IFormatProvider? provider, out bool shouldAppend)
            {
                _destination = destination;
                _provider = provider;
                _pos = 0;
                _success = shouldAppend = destination.Length >= literalLength;
                _hasCustomFormatter = provider is not null && DefaultInterpolatedStringHandler.HasCustomFormatter(provider);
            }

            /// <summary>Writes the specified string to the handler.</summary>
            /// <param name="value">The string to write.</param>
            /// <returns>true if the value could be formatted to the span; otherwise, false.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool AppendLiteral(string value)
            {
                if (value.TryCopyTo(_destination.Slice(_pos)))
                {
                    _pos += value.Length;
                    return true;
                }

                return Fail();
            }

            #region AppendFormatted
            // Design note:
            // This provides the same set of overloads and semantics as DefaultInterpolatedStringHandler.

            #region AppendFormatted T
            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            /// <typeparam name="T">The type of the value to write.</typeparam>
            public bool AppendFormatted<T>(T value)
            {
                // This method could delegate to AppendFormatted with a null format, but explicitly passing
                // default as the format to TryFormat helps to improve code quality in some cases when TryFormat is inlined,
                // e.g. for Int32 it enables the JIT to eliminate code in the inlined method based on a length check on the format.

                // If there's a custom formatter, always use it.
                if (_hasCustomFormatter)
                {
                    return AppendCustomFormatter(value, format: null);
                }

                if (value is null)
                {
                    return true;
                }

                // Check first for IFormattable, even though we'll prefer to use ISpanFormattable, as the latter
                // derives from the former.  For value types, it won't matter as the type checks devolve into
                // JIT-time constants.  For reference types, they're more likely to implement IFormattable
                // than they are to implement ISpanFormattable: if they don't implement either, we save an
                // interface check over first checking for ISpanFormattable and then for IFormattable, and
                // if it only implements IFormattable, we come out even: only if it implements both do we
                // end up paying for an extra interface check.
                string? s;
                if (value is IFormattable)
                {
                    // If the value can format itself directly into our buffer, do so.

#if false
                    if (typeof(T).IsEnum)
                    {
                        if (Enum.TryFormatUnconstrained(value, _destination.Slice(_pos), out int charsWritten))
                        {
                            _pos += charsWritten;
                            return true;
                        }

                        return Fail();
                    }
#endif

                    if (value is ISpanFormattable)
                    {
                        if (((ISpanFormattable)value).TryFormat(_destination.Slice(_pos), out int charsWritten, default, _provider)) // constrained call avoiding boxing for value types
                        {
                            _pos += charsWritten;
                            return true;
                        }

                        return Fail();
                    }

                    s = ((IFormattable)value).ToString(format: null, _provider); // constrained call avoiding boxing for value types
                }
                else
                {
                    s = value.ToString();
                }

                return s is null || AppendLiteral(s);
            }

            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            /// <param name="format">The format string.</param>
            /// <typeparam name="T">The type of the value to write.</typeparam>
            public bool AppendFormatted<T>(T value, string? format)
            {
                // If there's a custom formatter, always use it.
                if (_hasCustomFormatter)
                {
                    return AppendCustomFormatter(value, format);
                }

                if (value is null)
                {
                    return true;
                }

                // Check first for IFormattable, even though we'll prefer to use ISpanFormattable, as the latter
                // derives from the former.  For value types, it won't matter as the type checks devolve into
                // JIT-time constants.  For reference types, they're more likely to implement IFormattable
                // than they are to implement ISpanFormattable: if they don't implement either, we save an
                // interface check over first checking for ISpanFormattable and then for IFormattable, and
                // if it only implements IFormattable, we come out even: only if it implements both do we
                // end up paying for an extra interface check.
                string? s;
                if (value is IFormattable)
                {
                    // If the value can format itself directly into our buffer, do so.

#if false
                    if (typeof(T).IsEnum)
                    {
                        if (Enum.TryFormatUnconstrained(value, _destination.Slice(_pos), out int charsWritten, format))
                        {
                            _pos += charsWritten;
                            return true;
                        }

                        return Fail();
                    }
#endif

                    if (value is ISpanFormattable)
                    {
                        if (((ISpanFormattable)value).TryFormat(_destination.Slice(_pos), out int charsWritten, format, _provider)) // constrained call avoiding boxing for value types
                        {
                            _pos += charsWritten;
                            return true;
                        }

                        return Fail();
                    }

                    s = ((IFormattable)value).ToString(format, _provider); // constrained call avoiding boxing for value types
                }
                else
                {
                    s = value.ToString();
                }

                return s is null || AppendLiteral(s);
            }

            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
            /// <typeparam name="T">The type of the value to write.</typeparam>
            public bool AppendFormatted<T>(T value, int alignment)
            {
                int startingPos = _pos;
                if (AppendFormatted(value))
                {
                    return alignment == 0 || TryAppendOrInsertAlignmentIfNeeded(startingPos, alignment);
                }

                return Fail();
            }

            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            /// <param name="format">The format string.</param>
            /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
            /// <typeparam name="T">The type of the value to write.</typeparam>
            public bool AppendFormatted<T>(T value, int alignment, string? format)
            {
                int startingPos = _pos;
                if (AppendFormatted(value, format))
                {
                    return alignment == 0 || TryAppendOrInsertAlignmentIfNeeded(startingPos, alignment);
                }

                return Fail();
            }
#endregion

            #region AppendFormatted ReadOnlySpan<char>
            /// <summary>Writes the specified character span to the handler.</summary>
            /// <param name="value">The span to write.</param>
            public bool AppendFormatted(scoped ReadOnlySpan<char> value)
            {
                // Fast path for when the value fits in the current buffer
                if (value.TryCopyTo(_destination.Slice(_pos)))
                {
                    _pos += value.Length;
                    return true;
                }

                return Fail();
            }

            /// <summary>Writes the specified string of chars to the handler.</summary>
            /// <param name="value">The span to write.</param>
            /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
            /// <param name="format">The format string.</param>
            public bool AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
            {
                bool leftAlign = false;
                if (alignment < 0)
                {
                    leftAlign = true;
                    alignment = -alignment;
                }

                int paddingRequired = alignment - value.Length;
                if (paddingRequired <= 0)
                {
                    // The value is as large or larger than the required amount of padding,
                    // so just write the value.
                    return AppendFormatted(value);
                }

                // Write the value along with the appropriate padding.
                Debug.Assert(alignment > value.Length);
                if (alignment <= _destination.Length - _pos)
                {
                    if (leftAlign)
                    {
                        value.CopyTo(_destination.Slice(_pos));
                        _pos += value.Length;
                        _destination.Slice(_pos, paddingRequired).Fill(' ');
                        _pos += paddingRequired;
                    }
                    else
                    {
                        _destination.Slice(_pos, paddingRequired).Fill(' ');
                        _pos += paddingRequired;
                        value.CopyTo(_destination.Slice(_pos));
                        _pos += value.Length;
                    }

                    return true;
                }

                return Fail();
            }
            #endregion

            #region AppendFormatted string
            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            public bool AppendFormatted(string? value)
            {
                if (_hasCustomFormatter)
                {
                    return AppendCustomFormatter(value, format: null);
                }

                if (value is null)
                {
                    return true;
                }

                if (value.TryCopyTo(_destination.Slice(_pos)))
                {
                    _pos += value.Length;
                    return true;
                }

                return Fail();
            }

            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
            /// <param name="format">The format string.</param>
            public bool AppendFormatted(string? value, int alignment = 0, string? format = null) =>
                // Format is meaningless for strings and doesn't make sense for someone to specify.  We have the overload
                // simply to disambiguate between ROS<char> and object, just in case someone does specify a format, as
                // string is implicitly convertible to both. Just delegate to the T-based implementation.
                AppendFormatted<string?>(value, alignment, format);
            #endregion

            #region AppendFormatted object
            /// <summary>Writes the specified value to the handler.</summary>
            /// <param name="value">The value to write.</param>
            /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
            /// <param name="format">The format string.</param>
            public bool AppendFormatted(object? value, int alignment = 0, string? format = null) =>
                // This overload is expected to be used rarely, only if either a) something strongly typed as object is
                // formatted with both an alignment and a format, or b) the compiler is unable to target type to T. It
                // exists purely to help make cases from (b) compile. Just delegate to the T-based implementation.
                AppendFormatted<object?>(value, alignment, format);
            #endregion
#endregion

            /// <summary>Formats the value using the custom formatter from the provider.</summary>
            /// <param name="value">The value to write.</param>
            /// <param name="format">The format string.</param>
            /// <typeparam name="T">The type of the value to write.</typeparam>
            [MethodImpl(MethodImplOptions.NoInlining)]
            private bool AppendCustomFormatter<T>(T value, string? format)
            {
                // This case is very rare, but we need to handle it prior to the other checks in case
                // a provider was used that supplied an ICustomFormatter which wanted to intercept the particular value.
                // We do the cast here rather than in the ctor, even though this could be executed multiple times per
                // formatting, to make the cast pay for play.
                Debug.Assert(_hasCustomFormatter);
                Debug.Assert(_provider != null);

                ICustomFormatter? formatter = (ICustomFormatter?)_provider.GetFormat(typeof(ICustomFormatter));
                Debug.Assert(formatter != null, "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

                if (formatter is not null && formatter.Format(format, value, _provider) is string customFormatted)
                {
                    return AppendLiteral(customFormatted);
                }

                return true;
            }

            /// <summary>Handles adding any padding required for aligning a formatted value in an interpolation expression.</summary>
            /// <param name="startingPos">The position at which the written value started.</param>
            /// <param name="alignment">Non-zero minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
            private bool TryAppendOrInsertAlignmentIfNeeded(int startingPos, int alignment)
            {
                Debug.Assert(startingPos >= 0 && startingPos <= _pos);
                Debug.Assert(alignment != 0);

                int charsWritten = _pos - startingPos;

                bool leftAlign = false;
                if (alignment < 0)
                {
                    leftAlign = true;
                    alignment = -alignment;
                }

                int paddingNeeded = alignment - charsWritten;
                if (paddingNeeded <= 0)
                {
                    return true;
                }

                if (paddingNeeded <= _destination.Length - _pos)
                {
                    if (leftAlign)
                    {
                        _destination.Slice(_pos, paddingNeeded).Fill(' ');
                    }
                    else
                    {
                        _destination.Slice(startingPos, charsWritten).CopyTo(_destination.Slice(startingPos + paddingNeeded));
                        _destination.Slice(startingPos, paddingNeeded).Fill(' ');
                    }

                    _pos += paddingNeeded;
                    return true;
                }

                return Fail();
            }

            /// <summary>Marks formatting as having failed and returns false.</summary>
            private bool Fail()
            {
                _success = false;
                return false;
            }
        }
    }
}

namespace PolyCore.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _System_MemoryExtensions
    {
        extension(MemoryExtensions)
        {
            /// <inheritdoc cref="_MemoryExtensions.TryWrite(Span{char}, ref _MemoryExtensions.TryWriteInterpolatedStringHandler, out int)"/>
            public static bool TryWrite(Span<char> destination, [InterpolatedStringHandlerArgument(nameof(destination))] ref _MemoryExtensions.TryWriteInterpolatedStringHandler handler, out int charsWritten)
                => _MemoryExtensions.TryWrite(destination, ref handler, out charsWritten);

            /// <inheritdoc cref="_MemoryExtensions.TryWrite(Span{char}, IFormatProvider?, ref _MemoryExtensions.TryWriteInterpolatedStringHandler, out int)"/>
            public static bool TryWrite(Span<char> destination, IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(destination), nameof(provider))] ref _MemoryExtensions.TryWriteInterpolatedStringHandler handler, out int charsWritten)
                => _MemoryExtensions.TryWrite(destination, provider, ref handler, out charsWritten);

            /// <inheritdoc cref="_MemoryExtensions.TryWrite{TArg0}(Span{char}, IFormatProvider?, CompositeFormat, out int, TArg0)"/>
            public static bool TryWrite<TArg0>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0)
                => _MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, arg0);

            /// <inheritdoc cref="_MemoryExtensions.TryWrite{TArg0, TArg1}(Span{char}, IFormatProvider?, CompositeFormat, out int, TArg0, TArg1)"/>
            public static bool TryWrite<TArg0, TArg1>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1)
                => _MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, arg0, arg1);

            /// <inheritdoc cref="_MemoryExtensions.TryWrite{TArg0, TArg1, TArg2}(Span{char}, IFormatProvider?, CompositeFormat, out int, TArg0, TArg1, TArg2)"/>
            public static bool TryWrite<TArg0, TArg1, TArg2>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1, TArg2 arg2)
                => _MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, arg0, arg1, arg2);

            /// <inheritdoc cref="_MemoryExtensions.TryWrite(Span{char}, IFormatProvider?, CompositeFormat, out int, object?[])"/>
            public static bool TryWrite(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, params object?[] args)
                => _MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, args);

            /// <inheritdoc cref="_MemoryExtensions.TryWrite(Span{char}, IFormatProvider?, CompositeFormat, out int, ReadOnlySpan{object?})"/>
            public static bool TryWrite(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, params ReadOnlySpan<object?> args)
                => _MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, args);
        }
    }
}
