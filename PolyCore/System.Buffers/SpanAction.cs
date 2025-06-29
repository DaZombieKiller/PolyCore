#if !NETSTANDARD2_1_OR_GREATER
namespace System.Buffers;

public delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);

public delegate void ReadOnlySpanAction<T, in TArg>(ReadOnlySpan<T> span, TArg arg);
#endif
