using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace @implicit;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_ObjectDisposedException
{
    extension(ObjectDisposedException)
    {
        /// <summary>Throws an <see cref="ObjectDisposedException"/> if the specified <paramref name="condition"/> is <see langword="true"/>.</summary>
        /// <param name="condition">The condition to evaluate.</param>
        /// <param name="instance">The object whose type's full name should be included in any resulting <see cref="ObjectDisposedException"/>.</param>
        /// <exception cref="ObjectDisposedException">The <paramref name="condition"/> is <see langword="true"/>.</exception>
        public static void ThrowIf([DoesNotReturnIf(true)] bool condition, object instance)
        {
            if (condition)
            {
                ThrowHelper.ThrowObjectDisposedException(instance);
            }
        }

        /// <summary>Throws an <see cref="ObjectDisposedException"/> if the specified <paramref name="condition"/> is <see langword="true"/>.</summary>
        /// <param name="condition">The condition to evaluate.</param>
        /// <param name="type">The type whose full name should be included in any resulting <see cref="ObjectDisposedException"/>.</param>
        /// <exception cref="ObjectDisposedException">The <paramref name="condition"/> is <see langword="true"/>.</exception>
        public static void ThrowIf([DoesNotReturnIf(true)] bool condition, Type type)
        {
            if (condition)
            {
                ThrowHelper.ThrowObjectDisposedException(type);
            }
        }
    }
}
