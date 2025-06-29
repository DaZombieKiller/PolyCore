// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETSTANDARD2_1_OR_GREATER
namespace System.Buffers
{
    public sealed partial class ArrayBufferWriter<T> : System.Buffers.IBufferWriter<T>
    {
        public ArrayBufferWriter() { }
        public ArrayBufferWriter(int initialCapacity) { }
        public int Capacity { get { throw null; } }
        public int FreeCapacity { get { throw null; } }
        public int WrittenCount { get { throw null; } }
        public System.ReadOnlyMemory<T> WrittenMemory { get { throw null; } }
        public System.ReadOnlySpan<T> WrittenSpan { get { throw null; } }
        public void Advance(int count) { }
        public void Clear() { }
        public System.Memory<T> GetMemory(int sizeHint = 0) { throw null; }
        public System.Span<T> GetSpan(int sizeHint = 0) { throw null; }
    }
    public abstract partial class ArrayPool<T>
    {
        protected ArrayPool() { }
        public static System.Buffers.ArrayPool<T> Shared { get { throw null; } }
        public static System.Buffers.ArrayPool<T> Create() { throw null; }
        public static System.Buffers.ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket) { throw null; }
        public abstract T[] Rent(int minimumLength);
        public abstract void Return(T[] array, bool clearArray = false);
    }
    public static partial class BuffersExtensions
    {
        public static void CopyTo<T>(this in System.Buffers.ReadOnlySequence<T> source, System.Span<T> destination) { }
        public static System.Nullable<System.SequencePosition> PositionOf<T>(this in System.Buffers.ReadOnlySequence<T> source, T value) where T : System.IEquatable<T> { throw null; }
        public static T[] ToArray<T>(this in System.Buffers.ReadOnlySequence<T> sequence) { throw null; }
        public static void Write<T>(this System.Buffers.IBufferWriter<T> writer, System.ReadOnlySpan<T> value) { }
    }
    public partial interface IBufferWriter<T>
    {
        void Advance(int count);
        System.Memory<T> GetMemory(int sizeHint = 0);
        System.Span<T> GetSpan(int sizeHint = 0);
    }
    public partial interface IMemoryOwner<T> : System.IDisposable
    {
        System.Memory<T> Memory { get; }
    }
    public partial interface IPinnable
    {
        System.Buffers.MemoryHandle Pin(int elementIndex);
        void Unpin();
    }
    public partial struct MemoryHandle : System.IDisposable
    {
        private object _dummy;
        private int _dummyPrimitive;
        [System.CLSCompliantAttribute(false)]
        public unsafe MemoryHandle(void* pointer, System.Runtime.InteropServices.GCHandle handle = default(System.Runtime.InteropServices.GCHandle), System.Buffers.IPinnable pinnable = null) { throw null; }
        [System.CLSCompliantAttribute(false)]
        public unsafe void* Pointer { get { throw null; } }
        public void Dispose() { }
    }
    public abstract partial class MemoryManager<T> : System.Buffers.IMemoryOwner<T>, System.Buffers.IPinnable, System.IDisposable
    {
        protected MemoryManager() { }
        public virtual System.Memory<T> Memory { get { throw null; } }
        protected System.Memory<T> CreateMemory(int length) { throw null; }
        protected System.Memory<T> CreateMemory(int start, int length) { throw null; }
        protected abstract void Dispose(bool disposing);
        public abstract System.Span<T> GetSpan();
        public abstract System.Buffers.MemoryHandle Pin(int elementIndex = 0);
        void System.IDisposable.Dispose() { }
        protected internal virtual bool TryGetArray(out System.ArraySegment<T> segment) { throw null; }
        public abstract void Unpin();
    }
    public abstract partial class MemoryPool<T> : System.IDisposable
    {
        protected MemoryPool() { }
        public abstract int MaxBufferSize { get; }
        public static System.Buffers.MemoryPool<T> Shared { get { throw null; } }
        public void Dispose() { }
        protected abstract void Dispose(bool disposing);
        public abstract System.Buffers.IMemoryOwner<T> Rent(int minBufferSize = -1);
    }
    public enum OperationStatus
    {
        DestinationTooSmall = 1,
        Done = 0,
        InvalidData = 3,
        NeedMoreData = 2,
    }
    public readonly partial struct ReadOnlySequence<T>
    {
        private readonly object _dummy;
        private readonly int _dummyPrimitive;
        public static readonly System.Buffers.ReadOnlySequence<T> Empty;
        public ReadOnlySequence(System.Buffers.ReadOnlySequenceSegment<T> startSegment, int startIndex, System.Buffers.ReadOnlySequenceSegment<T> endSegment, int endIndex) { throw null; }
        public ReadOnlySequence(System.ReadOnlyMemory<T> memory) { throw null; }
        public ReadOnlySequence(T[] array) { throw null; }
        public ReadOnlySequence(T[] array, int start, int length) { throw null; }
        public System.SequencePosition End { get { throw null; } }
        public System.ReadOnlyMemory<T> First { get { throw null; } }
        public System.ReadOnlySpan<T> FirstSpan { get { throw null; } }
        public bool IsEmpty { get { throw null; } }
        public bool IsSingleSegment { get { throw null; } }
        public long Length { get { throw null; } }
        public System.SequencePosition Start { get { throw null; } }
        public System.Buffers.ReadOnlySequence<T>.Enumerator GetEnumerator() { throw null; }
        public System.SequencePosition GetPosition(long offset) { throw null; }
        public System.SequencePosition GetPosition(long offset, System.SequencePosition origin) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(int start, int length) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(int start, System.SequencePosition end) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(long start) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(long start, long length) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(long start, System.SequencePosition end) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(System.SequencePosition start) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(System.SequencePosition start, int length) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(System.SequencePosition start, long length) { throw null; }
        public System.Buffers.ReadOnlySequence<T> Slice(System.SequencePosition start, System.SequencePosition end) { throw null; }
        public override string ToString() { throw null; }
        public bool TryGet(ref System.SequencePosition position, out System.ReadOnlyMemory<T> memory, bool advance = true) { throw null; }
        public partial struct Enumerator
        {
            private object _dummy;
            public Enumerator(in System.Buffers.ReadOnlySequence<T> sequence) { throw null; }
            public System.ReadOnlyMemory<T> Current { get { throw null; } }
            public bool MoveNext() { throw null; }
        }
    }
    public abstract partial class ReadOnlySequenceSegment<T>
    {
        protected ReadOnlySequenceSegment() { }
        public System.ReadOnlyMemory<T> Memory { get { throw null; } protected set { } }
        public System.Buffers.ReadOnlySequenceSegment<T> Next { get { throw null; } protected set { } }
        public long RunningIndex { get { throw null; } protected set { } }
    }
    public delegate void ReadOnlySpanAction<T, in TArg>(System.ReadOnlySpan<T> span, TArg arg);
    public ref partial struct SequenceReader<T> where T : unmanaged, System.IEquatable<T>
    {
        private object _dummy;
        private int _dummyPrimitive;
        public SequenceReader(System.Buffers.ReadOnlySequence<T> sequence) { throw null; }
        public readonly long Consumed { get { throw null; } }
        public readonly System.ReadOnlySpan<T> CurrentSpan { get { throw null; } }
        public readonly int CurrentSpanIndex { get { throw null; } }
        public readonly bool End { get { throw null; } }
        public readonly long Length { get { throw null; } }
        public readonly System.SequencePosition Position { get { throw null; } }
        public readonly long Remaining { get { throw null; } }
        public readonly System.Buffers.ReadOnlySequence<T> Sequence { get { throw null; } }
        public readonly System.ReadOnlySpan<T> UnreadSpan { get { throw null; } }
        public void Advance(long count) { }
        public long AdvancePast(T value) { throw null; }
        public long AdvancePastAny(System.ReadOnlySpan<T> values) { throw null; }
        public long AdvancePastAny(T value0, T value1) { throw null; }
        public long AdvancePastAny(T value0, T value1, T value2) { throw null; }
        public long AdvancePastAny(T value0, T value1, T value2, T value3) { throw null; }
        public bool IsNext(System.ReadOnlySpan<T> next, bool advancePast = false) { throw null; }
        public bool IsNext(T next, bool advancePast = false) { throw null; }
        public void Rewind(long count) { }
        public bool TryAdvanceTo(T delimiter, bool advancePastDelimiter = true) { throw null; }
        public bool TryAdvanceToAny(System.ReadOnlySpan<T> delimiters, bool advancePastDelimiter = true) { throw null; }
        public readonly bool TryCopyTo(System.Span<T> destination) { throw null; }
        public readonly bool TryPeek(out T value) { throw null; }
        public bool TryRead(out T value) { throw null; }
        public bool TryReadTo(out System.Buffers.ReadOnlySequence<T> sequence, System.ReadOnlySpan<T> delimiter, bool advancePastDelimiter = true) { throw null; }
        public bool TryReadTo(out System.Buffers.ReadOnlySequence<T> sequence, T delimiter, bool advancePastDelimiter = true) { throw null; }
        public bool TryReadTo(out System.Buffers.ReadOnlySequence<T> sequence, T delimiter, T delimiterEscape, bool advancePastDelimiter = true) { throw null; }
        public bool TryReadTo(out System.ReadOnlySpan<T> span, T delimiter, bool advancePastDelimiter = true) { throw null; }
        public bool TryReadTo(out System.ReadOnlySpan<T> span, T delimiter, T delimiterEscape, bool advancePastDelimiter = true) { throw null; }
        public bool TryReadToAny(out System.Buffers.ReadOnlySequence<T> sequence, System.ReadOnlySpan<T> delimiters, bool advancePastDelimiter = true) { throw null; }
        public bool TryReadToAny(out System.ReadOnlySpan<T> span, System.ReadOnlySpan<T> delimiters, bool advancePastDelimiter = true) { throw null; }
    }
    public static partial class SequenceReaderExtensions
    {
        public static bool TryReadBigEndian(this ref System.Buffers.SequenceReader<byte> reader, out short value) { throw null; }
        public static bool TryReadBigEndian(this ref System.Buffers.SequenceReader<byte> reader, out int value) { throw null; }
        public static bool TryReadBigEndian(this ref System.Buffers.SequenceReader<byte> reader, out long value) { throw null; }
        public static bool TryReadLittleEndian(this ref System.Buffers.SequenceReader<byte> reader, out short value) { throw null; }
        public static bool TryReadLittleEndian(this ref System.Buffers.SequenceReader<byte> reader, out int value) { throw null; }
        public static bool TryReadLittleEndian(this ref System.Buffers.SequenceReader<byte> reader, out long value) { throw null; }
    }
    public delegate void SpanAction<T, in TArg>(System.Span<T> span, TArg arg);
    public readonly partial struct StandardFormat : System.IEquatable<System.Buffers.StandardFormat>
    {
        private readonly int _dummyPrimitive;
        public const byte MaxPrecision = (byte)99;
        public const byte NoPrecision = (byte)255;
        public StandardFormat(char symbol, byte precision = (byte)255) { throw null; }
        public bool HasPrecision { get { throw null; } }
        public bool IsDefault { get { throw null; } }
        public byte Precision { get { throw null; } }
        public char Symbol { get { throw null; } }
        public bool Equals(System.Buffers.StandardFormat other) { throw null; }
        public override bool Equals(object obj) { throw null; }
        public override int GetHashCode() { throw null; }
        public static bool operator ==(System.Buffers.StandardFormat left, System.Buffers.StandardFormat right) { throw null; }
        public static implicit operator System.Buffers.StandardFormat (char symbol) { throw null; }
        public static bool operator !=(System.Buffers.StandardFormat left, System.Buffers.StandardFormat right) { throw null; }
        public static System.Buffers.StandardFormat Parse(System.ReadOnlySpan<char> format) { throw null; }
        public static System.Buffers.StandardFormat Parse(string format) { throw null; }
        public override string ToString() { throw null; }
        public static bool TryParse(System.ReadOnlySpan<char> format, out System.Buffers.StandardFormat result) { throw null; }
    }
}
#endif
