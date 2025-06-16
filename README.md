# PolyCore
A .NET BCL polyfill library for Unity.

# Requirements
This library makes use of C# 14 extensions to provide polyfills for static methods. You will need to use [UnityRoslynUpdater](https://github.com/DaZombieKiller/UnityRoslynUpdater) or a similar tool to enable C# 14 in Unity.

# Compatibility Notice
`UnmanagedCallersOnlyAttribute` is provided by PolyCore, but it is only implemented by the runtime as of Unity 6.
