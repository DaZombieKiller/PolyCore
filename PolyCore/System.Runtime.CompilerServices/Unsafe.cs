using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace @implicit;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_Runtime_CompilerServices_Unsafe
{
    extension(Unsafe)
    {
        /// <summary>
        /// Reinterprets the given value of type <typeparamref name="TFrom" /> as a value of type <typeparamref name="TTo" />.
        /// </summary>
        /// <exception cref="NotSupportedException">The sizes of <typeparamref name="TFrom" /> and <typeparamref name="TTo" /> are not the same
        /// or the type parameters are not <see langword="struct"/>s.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TTo BitCast<TFrom, TTo>(TFrom source)
        {
            unsafe
            {
                if (sizeof(TFrom) != sizeof(TTo) || !typeof(TFrom).IsValueType || !typeof(TTo).IsValueType)
                {
                    ThrowHelper.ThrowNotSupportedException();
                }
            }

            return Unsafe.ReadUnaligned<TTo>(ref Unsafe.As<TFrom, byte>(ref source));
        }
    }
}
