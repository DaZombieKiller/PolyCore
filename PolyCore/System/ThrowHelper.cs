using System.Diagnostics;
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

    [DoesNotReturn]
    public static void ThrowFormatIndexOutOfRange()
    {
        throw new FormatException(SR.Format_IndexOutOfRange);
    }

    [DoesNotReturn]
    public static void ThrowFormatInvalidString(int offset, ExceptionResource resource)
    {
        throw new FormatException(SR.Format(SR.Format_InvalidStringWithOffsetAndReason, offset, GetResourceString(resource)));
    }

    private static string GetResourceString(ExceptionResource resource)
    {
        switch (resource)
        {
        case ExceptionResource.ArgumentOutOfRange_IndexMustBeLessOrEqual:
            return SR.ArgumentOutOfRange_IndexMustBeLessOrEqual;
        case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
            return SR.ArgumentOutOfRange_NeedNonNegNum;
        case ExceptionResource.Format_UnexpectedClosingBrace:
            return SR.Format_UnexpectedClosingBrace;
        case ExceptionResource.Format_UnclosedFormatItem:
            return SR.Format_UnclosedFormatItem;
        case ExceptionResource.Format_ExpectedAsciiDigit:
            return SR.Format_ExpectedAsciiDigit;
        default:
            Debug.Fail("The enum value is not defined, please check the ExceptionResource Enum.");
            return "";
        }
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

    public const string Format_IndexOutOfRange = "Index (zero based) must be greater than or equal to zero and less than the size of the argument list.";

    public const string Format_InvalidStringWithOffsetAndReason = "Input string was not in a correct format. Failure to parse near offset {0}. {1}";

    public const string Format_UnexpectedClosingBrace = "Unexpected closing brace without a corresponding opening brace.";

    public const string Format_UnclosedFormatItem = "Format item ends prematurely.";

    public const string Format_ExpectedAsciiDigit = "Expected an ASCII digit.";

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

internal enum ExceptionResource
{
    ArgumentOutOfRange_IndexMustBeLessOrEqual,
    ArgumentOutOfRange_IndexMustBeLess,
    ArgumentOutOfRange_IndexCount,
    ArgumentOutOfRange_IndexCountBuffer,
    ArgumentOutOfRange_Count,
    ArgumentOutOfRange_Year,
    Arg_ArrayPlusOffTooSmall,
    Arg_ByteArrayTooSmallForValue,
    NotSupported_ReadOnlyCollection,
    Arg_RankMultiDimNotSupported,
    Arg_NonZeroLowerBound,
    ArgumentOutOfRange_GetCharCountOverflow,
    ArgumentOutOfRange_ListInsert,
    ArgumentOutOfRange_NeedNonNegNum,
    ArgumentOutOfRange_NotGreaterThanBufferLength,
    ArgumentOutOfRange_SmallCapacity,
    Argument_InvalidOffLen,
    Argument_CannotExtractScalar,
    ArgumentOutOfRange_BiggerThanCollection,
    Serialization_MissingKeys,
    Serialization_NullKey,
    NotSupported_KeyCollectionSet,
    NotSupported_ValueCollectionSet,
    InvalidOperation_NullArray,
    TaskT_TransitionToFinal_AlreadyCompleted,
    TaskCompletionSourceT_TrySetException_NullException,
    TaskCompletionSourceT_TrySetException_NoExceptions,
    NotSupported_StringComparison,
    ConcurrentCollection_SyncRoot_NotSupported,
    Task_MultiTaskContinuation_NullTask,
    InvalidOperation_WrongAsyncResultOrEndCalledMultiple,
    Task_MultiTaskContinuation_EmptyTaskList,
    Task_Start_TaskCompleted,
    Task_Start_Promise,
    Task_Start_ContinuationTask,
    Task_Start_AlreadyStarted,
    Task_RunSynchronously_Continuation,
    Task_RunSynchronously_Promise,
    Task_RunSynchronously_TaskCompleted,
    Task_RunSynchronously_AlreadyStarted,
    AsyncMethodBuilder_InstanceNotInitialized,
    Task_ContinueWith_ESandLR,
    Task_ContinueWith_NotOnAnything,
    Task_InvalidTimerTimeSpan,
    Task_Delay_InvalidMillisecondsDelay,
    Task_Dispose_NotCompleted,
    Task_ThrowIfDisposed,
    Task_WaitMulti_NullTask,
    ArgumentException_OtherNotArrayOfCorrectLength,
    ArgumentNull_Array,
    ArgumentNull_SafeHandle,
    ArgumentOutOfRange_EndIndexStartIndex,
    ArgumentOutOfRange_Enum,
    ArgumentOutOfRange_HugeArrayNotSupported,
    Argument_AddingDuplicate,
    Argument_InvalidArgumentForComparison,
    Arg_LowerBoundsMustMatch,
    Arg_MustBeType,
    Arg_Need1DArray,
    Arg_Need2DArray,
    Arg_Need3DArray,
    Arg_NeedAtLeast1Rank,
    Arg_RankIndices,
    Arg_RanksAndBounds,
    InvalidOperation_IComparerFailed,
    NotSupported_FixedSizeCollection,
    Rank_MultiDimNotSupported,
    Arg_TypeNotSupported,
    Argument_SpansMustHaveSameLength,
    Argument_InvalidFlag,
    CancellationTokenSource_Disposed,
    Argument_AlignmentMustBePow2,
    InvalidOperation_SpanOverlappedOperation,
    InvalidOperation_TimeProviderNullLocalTimeZone,
    InvalidOperation_TimeProviderInvalidTimestampFrequency,
    Format_UnexpectedClosingBrace,
    Format_UnclosedFormatItem,
    Format_ExpectedAsciiDigit,
    Argument_HasToBeArrayClass,
    InvalidOperation_IncompatibleComparer,
}
