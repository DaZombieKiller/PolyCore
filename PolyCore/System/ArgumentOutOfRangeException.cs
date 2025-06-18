using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace @implicit;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_ArgumentOutOfRangeException
{
    extension(ArgumentOutOfRangeException)
    {
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.</summary>
        /// <param name="value">The argument to validate as non-zero.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IEquatable<T>
        {
            if (value.Equals(default!))
            {
                ThrowZero(value, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.</summary>
        /// <param name="value">The argument to validate as non-negative.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(default!) < 0)
            {
                ThrowNegative(value, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.</summary>
        /// <param name="value">The argument to validate as non-zero or non-negative.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfNegativeOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>, IEquatable<T>
        {
            if (value.CompareTo(default!) < 0 || value.Equals(default!))
            {
                ThrowNegativeOrZero(value, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is equal to <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as not equal to <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null) where T : IEquatable<T>?
        {
            if (EqualityComparer<T>.Default.Equals(value, other))
            {
                ThrowEqual(value, other, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not equal to <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as equal to <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfNotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null) where T : IEquatable<T>?
        {
            if (!EqualityComparer<T>.Default.Equals(value, other))
            {
                ThrowNotEqual(value, other, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) > 0)
            {
                ThrowGreater(value, other, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as less than <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfGreaterThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) >= 0)
            {
                ThrowGreaterEqual(value, other, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as greater than or equal than <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) < 0)
            {
                ThrowLess(value, other, paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as greater than than <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        public static void ThrowIfLessThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) <= 0)
            {
                ThrowLessEqual(value, other, paramName);
            }
        }
    }

    [DoesNotReturn]
    private static void ThrowZero<T>(T value, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be a non-zero value.", paramName, value));

    [DoesNotReturn]
    private static void ThrowNegative<T>(T value, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be a non-negative value.", paramName, value));

    [DoesNotReturn]
    private static void ThrowNegativeOrZero<T>(T value, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be a non-negative and non-zero value.", paramName, value));

    [DoesNotReturn]
    private static void ThrowGreater<T>(T value, T other, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be less than or equal to '{2}'.", paramName, value, other));

    [DoesNotReturn]
    private static void ThrowGreaterEqual<T>(T value, T other, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be less than '{2}'.", paramName, value, other));

    [DoesNotReturn]
    private static void ThrowLess<T>(T value, T other, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be greater than or equal to '{2}'.", paramName, value, other));

    [DoesNotReturn]
    private static void ThrowLessEqual<T>(T value, T other, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be greater than '{2}'.", paramName, value, other));

    [DoesNotReturn]
    private static void ThrowEqual<T>(T value, T other, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must not be equal to '{2}'.", paramName, (object?)value ?? "null", (object?)other ?? "null"));

    [DoesNotReturn]
    private static void ThrowNotEqual<T>(T value, T other, string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName, value, string.Format("{0} ('{1}') must be equal to '{2}'.", paramName, (object?)value ?? "null", (object?)other ?? "null"));
}
