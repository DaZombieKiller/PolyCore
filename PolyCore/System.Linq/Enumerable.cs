using System.ComponentModel;

namespace System.Linq
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _Enumerable
    {
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[] source) => Enumerable.Reverse(source);
    }
}

namespace PolyCore.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _System_Linq_Enumerable
    {
        extension(Enumerable)
        {
            public static IEnumerable<TSource> Reverse<TSource>(TSource[] source) => Enumerable.Reverse(source);
        }
    }
}
