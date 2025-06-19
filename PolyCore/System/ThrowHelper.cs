using System.Diagnostics.CodeAnalysis;

namespace System;

internal static class ThrowHelper
{
    [DoesNotReturn]
    public static void ThrowNotSupportedException()
    {
        throw new NotSupportedException();
    }

    [DoesNotReturn]
    internal static void ThrowObjectDisposedException(object? instance)
    {
        throw new ObjectDisposedException(instance?.GetType().FullName);
    }

    [DoesNotReturn]
    public static void ThrowObjectDisposedException(Type? type)
    {
        throw new ObjectDisposedException(type?.FullName);
    }

    [DoesNotReturn]
    public static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
    {
        throw new ArgumentException(SR.Format(SR.Argument_InvalidTypeWithPointersNotSupported, targetType));
    }

    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException(string argument)
    {
        throw new ArgumentOutOfRangeException(argument);
    }

    [DoesNotReturn]
    public static void ThrowArgumentNullException(string argument)
    {
        throw new ArgumentNullException(argument);
    }

    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException_NeedNonNegNum(string paramName)
    {
        throw new ArgumentOutOfRangeException(paramName, SR.ArgumentOutOfRange_NeedNonNegNum);
    }

    [DoesNotReturn]
    public static void ThrowArgumentOutOfRange_IndexMustBeLessOrEqualException()
    {
        throw new ArgumentOutOfRangeException(ExceptionArgument.index, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual);
    }
}

internal static class SR
{
    public const string Argument_EmptyString = "The value cannot be an empty string.";

    public const string Argument_EmptyOrWhiteSpaceString = "The value cannot be an empty string or composed entirely of whitespace.";

    public const string Argument_InvalidTypeWithPointersNotSupported = "Cannot use type '{0}'. Only value types without pointers or references are supported.";

    public const string Argument_MinMaxValue = "'{0}' cannot be greater than {1}.";

    public const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";

    public const string ArgumentOutOfRange_IndexMustBeLessOrEqual = "Index was out of range. Must be non-negative and less than or equal to the size of the collection.";

    public static string Format(string resourceFormat, object? p1) => string.Format(resourceFormat, p1);

    public static string Format(string resourceFormat, object? p1, object? p2) => string.Format(resourceFormat, p1, p2);

    public static string Format(string resourceFormat, object? p1, object? p2, object? p3) => string.Format(resourceFormat, p1, p2, p3);

    public static string Format(string resourceFormat, object?[]? args) => string.Format(resourceFormat, args);
}

internal static class ExceptionArgument
{
    public const string array = nameof(array);

    public const string length = nameof(length);

    public const string list = nameof(list);

    public const string index = nameof(index);
}
