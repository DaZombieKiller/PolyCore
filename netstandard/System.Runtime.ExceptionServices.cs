// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Runtime.ExceptionServices
{
    public sealed partial class ExceptionDispatchInfo
    {
        internal ExceptionDispatchInfo() { }
        public System.Exception SourceException { get { throw null; } }
        public static System.Runtime.ExceptionServices.ExceptionDispatchInfo Capture(System.Exception source) { throw null; }
#if NETSTANDARD2_1_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
#endif
        public void Throw() { }
#if NETSTANDARD2_1_OR_GREATER
        [System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
        public static void Throw(System.Exception source) { }
#endif
    }
    public partial class FirstChanceExceptionEventArgs : System.EventArgs
    {
        public FirstChanceExceptionEventArgs(System.Exception exception) { }
        public System.Exception Exception { get { throw null; } }
    }
    [System.AttributeUsageAttribute(System.AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
    public sealed partial class HandleProcessCorruptedStateExceptionsAttribute : System.Attribute
    {
        public HandleProcessCorruptedStateExceptionsAttribute() { }
    }
}
