using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PolyCore.Internal;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class _System_Runtime_InteropServices_Marshal
{
    extension(Marshal)
    {
        /// <summary>
        /// Initializes the underlying handle of a newly created <see cref="SafeHandle" /> to the provided value.
        /// </summary>
        /// <param name="safeHandle">The <see cref="SafeHandle"/> instance to update.</param>
        /// <param name="handle">The pre-existing handle.</param>
        public static void InitHandle(SafeHandle safeHandle, IntPtr handle)
        {
            safeHandle.SetHandle(handle);
        }
    }
}
