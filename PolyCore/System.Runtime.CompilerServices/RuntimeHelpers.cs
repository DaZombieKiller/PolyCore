#if !NETSTANDARD2_1_OR_GREATER
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_Runtime_CompilerServices_RuntimeHelpers
{
    extension(RuntimeHelpers)
    {
        public static bool IsReferenceOrContainsReferences<T>()
        {
            return PerTypeValues<T>.IsReferenceOrContainsReferences;
        }

        public static object GetUninitializedObject(Type type)
        {
            return FormatterServices.GetUninitializedObject(type);
        }

        public static bool TryEnsureSufficientExecutionStack()
        {
            try
            {
                RuntimeHelpers.EnsureSufficientExecutionStack();
                return true;
            }
            catch (InsufficientExecutionStackException)
            {
                return false;
            }
        }

        public static T[] GetSubArray<T>(T[] array, Range range)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            (int offset, int length) = range.GetOffsetAndLength(array.Length);

            T[] dest;

            if (default(T) != null || typeof(T[]) == array.GetType())
            {
                // We know the type of the array to be exactly T[].

                if (length == 0)
                {
                    return Array.Empty<T>();
                }

                dest = new T[length];
            }
            else
            {
                // The array is actually a U[] where U:T. We'll make sure to create
                // an array of the exact same backing type. The cast to T[] will
                // never fail.

                dest = (T[])Array.CreateInstance(array.GetType().GetElementType(), length);
            }

            // In either case, the newly-allocated array is the exact same type as the
            // original incoming array. It's safe for us to SpanHelpers.Memmove the contents
            // from the source array to the destination array, otherwise the contents
            // wouldn't have been valid for the source array in the first place.

            Array.Copy(array, offset, dest, 0, length);
            return dest;
        }
    }

    private static bool IsReferenceOrContainsReferencesCore(Type type)
    {
        // This is hopefully the common case. All types that return true for this are value types w/out embedded references.
        if (type.GetTypeInfo().IsPrimitive)
            return false;

        // Deviation from portable Span<T> behavior to match CoreCLR behavior.
        if (type.GetTypeInfo().IsPointer)
            return false;

        if (!type.GetTypeInfo().IsValueType)
            return true;

        // If type is a Nullable<> of something, unwrap it first.
        Type? underlyingNullable = Nullable.GetUnderlyingType(type);

        if (underlyingNullable != null)
            type = underlyingNullable;

        if (type.GetTypeInfo().IsEnum)
            return false;

        foreach (FieldInfo field in type.GetTypeInfo().DeclaredFields)
        {
            if (field.IsStatic)
                continue;

            if (IsReferenceOrContainsReferencesCore(field.FieldType))
                return true;
        }

        return false;
    }

    private static class PerTypeValues<T>
    {
        public static readonly bool IsReferenceOrContainsReferences = IsReferenceOrContainsReferencesCore(typeof(T));
    }
}
#endif
