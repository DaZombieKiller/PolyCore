﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace PolyCore.Internal;

public static partial class _System_Text_StringBuilder
{
	/// <summary>Provides a handler used by the language compiler to append interpolated strings into <see cref="StringBuilder"/> instances.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[InterpolatedStringHandler]
	public struct AppendInterpolatedStringHandler
	{
		// Implementation note:
		// As this type is only intended to be targeted by the compiler, public APIs eschew argument validation logic
		// in a variety of places, e.g. allowing a null input when one isn't expected to produce a NullReferenceException rather
		// than an ArgumentNullException.

		/// <summary>The associated StringBuilder to which to append.</summary>
		internal readonly StringBuilder _stringBuilder;
		/// <summary>Optional provider to pass to IFormattable.ToString or ISpanFormattable.TryFormat calls.</summary>
		private readonly IFormatProvider? _provider;
		/// <summary>Whether <see cref="_provider"/> provides an ICustomFormatter.</summary>
		/// <remarks>
		/// Custom formatters are very rare.  We want to support them, but it's ok if we make them more expensive
		/// in order to make them as pay-for-play as possible.  So, we avoid adding another reference type field
		/// to reduce the size of the handler and to reduce required zero'ing, by only storing whether the provider
		/// provides a formatter, rather than actually storing the formatter.  This in turn means, if there is a
		/// formatter, we pay for the extra interface call on each AppendFormatted that needs it.
		/// </remarks>
		private readonly bool _hasCustomFormatter;

		/// <summary>Creates a handler used to append an interpolated string into a <see cref="StringBuilder"/>.</summary>
		/// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
		/// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
		/// <param name="stringBuilder">The associated StringBuilder to which to append.</param>
		/// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
		public AppendInterpolatedStringHandler(int literalLength, int formattedCount, StringBuilder stringBuilder)
		{
			_stringBuilder = stringBuilder;
			_provider = null;
			_hasCustomFormatter = false;
		}

		/// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
		/// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
		/// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
		/// <param name="stringBuilder">The associated StringBuilder to which to append.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
		public AppendInterpolatedStringHandler(int literalLength, int formattedCount, StringBuilder stringBuilder, IFormatProvider? provider)
		{
			_stringBuilder = stringBuilder;
			_provider = provider;
			_hasCustomFormatter = provider is not null && DefaultInterpolatedStringHandler.HasCustomFormatter(provider);
		}

		/// <summary>Writes the specified string to the handler.</summary>
		/// <param name="value">The string to write.</param>
		public void AppendLiteral(string value) => _stringBuilder.Append(value);

		#region AppendFormatted
		// Design note:
		// This provides the same set of overloads and semantics as DefaultInterpolatedStringHandler.

		#region AppendFormatted T
		/// <summary>Writes the specified value to the handler.</summary>
		/// <param name="value">The value to write.</param>
		/// <typeparam name="T">The type of the value to write.</typeparam>
		public void AppendFormatted<T>(T value)
		{
			// This method could delegate to AppendFormatted with a null format, but explicitly passing
			// default as the format to TryFormat helps to improve code quality in some cases when TryFormat is inlined,
			// e.g. for Int32 it enables the JIT to eliminate code in the inlined method based on a length check on the format.

			if (_hasCustomFormatter)
			{
				// If there's a custom formatter, always use it.
				AppendCustomFormatter(value, format: null);
				return;
			}

			if (value is null)
			{
				return;
			}

			if (value is IFormattable)
			{
				// Check first for IFormattable, even though we'll prefer to use ISpanFormattable, as the latter
				// requires the former.  For value types, it won't matter as the type checks devolve into
				// JIT-time constants.  For reference types, they're more likely to implement IFormattable
				// than they are to implement ISpanFormattable: if they don't implement either, we save an
				// interface check over first checking for ISpanFormattable and then for IFormattable, and
				// if it only implements IFormattable, we come out even: only if it implements both do we
				// end up paying for an extra interface check.

#if false
				if (typeof(T).IsEnum)
				{
					if (Enum.TryFormatUnconstrained(value, _stringBuilder.RemainingCurrentChunk, out int charsWritten))
					{
						_stringBuilder.m_ChunkLength += charsWritten;
					}
					else
					{
						AppendFormattedWithTempSpace(value, 0, format: null);
					}
				}
				else
#endif
				if (value is ISpanFormattable)
				{
					Span<char> destination = _stringBuilder.RemainingCurrentChunk;
					if (((ISpanFormattable)value).TryFormat(destination, out int charsWritten, default, _provider)) // constrained call avoiding boxing for value types
					{
						if ((uint)charsWritten > (uint)destination.Length)
						{
							// Protect against faulty ISpanFormattable implementations returning invalid charsWritten values.
							// Other code in _stringBuilder uses Unsafe manipulation, and we want to ensure m_ChunkLength remains safe.
							ThrowHelper.ThrowFormatInvalidString();
						}

						_stringBuilder.m_ChunkLength += charsWritten;
					}
					else
					{
						// Not enough room in the current chunk.  Take the slow path that formats into temporary space
						// and then copies the result into the StringBuilder.
						AppendFormattedWithTempSpace(value, 0, format: null);
					}
				}
				else
				{
					_stringBuilder.Append(((IFormattable)value).ToString(format: null, _provider)); // constrained call avoiding boxing for value types
				}
			}
			else
			{
				_stringBuilder.Append(value.ToString());
			}
		}

		/// <summary>Writes the specified value to the handler.</summary>
		/// <param name="value">The value to write.</param>
		/// <param name="format">The format string.</param>
		/// <typeparam name="T">The type of the value to write.</typeparam>
		public void AppendFormatted<T>(T value, string? format)
		{
			if (_hasCustomFormatter)
			{
				// If there's a custom formatter, always use it.
				AppendCustomFormatter(value, format);
				return;
			}

			if (value is null)
			{
				return;
			}

			if (value is IFormattable)
			{
				// Check first for IFormattable, even though we'll prefer to use ISpanFormattable, as the latter
				// requires the former.  For value types, it won't matter as the type checks devolve into
				// JIT-time constants.  For reference types, they're more likely to implement IFormattable
				// than they are to implement ISpanFormattable: if they don't implement either, we save an
				// interface check over first checking for ISpanFormattable and then for IFormattable, and
				// if it only implements IFormattable, we come out even: only if it implements both do we
				// end up paying for an extra interface check.

#if false
				if (typeof(T).IsEnum)
				{
					if (Enum.TryFormatUnconstrained(value, _stringBuilder.RemainingCurrentChunk, out int charsWritten, format))
					{
						_stringBuilder.m_ChunkLength += charsWritten;
					}
					else
					{
						AppendFormattedWithTempSpace(value, 0, format);
					}
				}
				else
#endif
				if (value is ISpanFormattable)
				{
					Span<char> destination = _stringBuilder.RemainingCurrentChunk;
					if (((ISpanFormattable)value).TryFormat(destination, out int charsWritten, format, _provider)) // constrained call avoiding boxing for value types
					{
						if ((uint)charsWritten > (uint)destination.Length)
						{
							// Protect against faulty ISpanFormattable implementations returning invalid charsWritten values.
							// Other code in _stringBuilder uses Unsafe manipulation, and we want to ensure m_ChunkLength remains safe.
							ThrowHelper.ThrowFormatInvalidString();
						}

						_stringBuilder.m_ChunkLength += charsWritten;
					}
					else
					{
						// Not enough room in the current chunk.  Take the slow path that formats into temporary space
						// and then copies the result into the StringBuilder.
						AppendFormattedWithTempSpace(value, 0, format);
					}
				}
				else
				{
					_stringBuilder.Append(((IFormattable)value).ToString(format, _provider)); // constrained call avoiding boxing for value types
				}
			}
			else
			{
				_stringBuilder.Append(value.ToString());
			}
		}

		/// <summary>Writes the specified value to the handler.</summary>
		/// <param name="value">The value to write.</param>
		/// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
		/// <typeparam name="T">The type of the value to write.</typeparam>
		public void AppendFormatted<T>(T value, int alignment) =>
			AppendFormatted(value, alignment, format: null);

		/// <summary>Writes the specified value to the handler.</summary>
		/// <param name="value">The value to write.</param>
		/// <param name="format">The format string.</param>
		/// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
		/// <typeparam name="T">The type of the value to write.</typeparam>
		public void AppendFormatted<T>(T value, int alignment, string? format)
		{
			if (alignment == 0)
			{
				// This overload is used as a fallback from several disambiguation overloads, so special-case 0.
				AppendFormatted(value, format);
			}
			else if (alignment < 0)
			{
				// Left aligned: format into the handler, then append any additional padding required.
				int start = _stringBuilder.Length;
				AppendFormatted(value, format);
				int paddingRequired = -alignment - (_stringBuilder.Length - start);
				if (paddingRequired > 0)
				{
					_stringBuilder.Append(' ', paddingRequired);
				}
			}
			else
			{
				// Right aligned: format into temporary space and then copy that into the handler, appropriately aligned.
				AppendFormattedWithTempSpace(value, alignment, format);
			}
		}

		/// <summary>Formats into temporary space and then appends the result into the StringBuilder.</summary>
		private void AppendFormattedWithTempSpace<T>(T value, int alignment, string? format)
		{
			// It's expected that either there's not enough space in the current chunk to store this formatted value,
			// or we have a non-0 alignment that could require padding inserted. So format into temporary space and
			// then append that written span into the StringBuilder: StringBuilder.Append(span) is able to split the
			// span across the current chunk and any additional chunks required.

			var handler = new DefaultInterpolatedStringHandler(0, 0, _provider, stackalloc char[string.StackallocCharBufferSizeLimit]);
			handler.AppendFormatted(value, format);
			AppendFormatted(handler.Text, alignment);
			handler.Clear();
		}
#endregion

		#region AppendFormatted ReadOnlySpan<char>
		/// <summary>Writes the specified character span to the handler.</summary>
		/// <param name="value">The span to write.</param>
		public void AppendFormatted(ReadOnlySpan<char> value) => _stringBuilder.Append(value);

		/// <summary>Writes the specified string of chars to the handler.</summary>
		/// <param name="value">The span to write.</param>
		/// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
		/// <param name="format">The format string.</param>
		public void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string? format = null)
		{
			if (alignment == 0)
			{
				_stringBuilder.Append(value);
			}
			else
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
					_stringBuilder.Append(value);
				}
				else if (leftAlign)
				{
					_stringBuilder.Append(value);
					_stringBuilder.Append(' ', paddingRequired);
				}
				else
				{
					_stringBuilder.Append(' ', paddingRequired);
					_stringBuilder.Append(value);
				}
			}
		}
		#endregion

		#region AppendFormatted string
		/// <summary>Writes the specified value to the handler.</summary>
		/// <param name="value">The value to write.</param>
		public void AppendFormatted(string? value)
		{
			if (!_hasCustomFormatter)
			{
				_stringBuilder.Append(value);
			}
			else
			{
				AppendFormatted<string?>(value);
			}
		}

		/// <summary>Writes the specified value to the handler.</summary>
		/// <param name="value">The value to write.</param>
		/// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
		/// <param name="format">The format string.</param>
		public void AppendFormatted(string? value, int alignment = 0, string? format = null) =>
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
		public void AppendFormatted(object? value, int alignment = 0, string? format = null) =>
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
		private void AppendCustomFormatter<T>(T value, string? format)
		{
			// This case is very rare, but we need to handle it prior to the other checks in case
			// a provider was used that supplied an ICustomFormatter which wanted to intercept the particular value.
			// We do the cast here rather than in the ctor, even though this could be executed multiple times per
			// formatting, to make the cast pay for play.
			Debug.Assert(_hasCustomFormatter);
			Debug.Assert(_provider != null);

			ICustomFormatter? formatter = (ICustomFormatter?)_provider.GetFormat(typeof(ICustomFormatter));
			Debug.Assert(formatter != null, "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

			if (formatter is not null)
			{
				_stringBuilder.Append(formatter.Format(format, value, _provider));
			}
		}
	}
}
