// 大白萝卜重构于 2025.05.14，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

// using System.Runtime.InteropServices;
// using System.Runtime.InteropServices.ComTypes;
//
// namespace CapeOpen;
//
// [ComImport]
// [InterfaceType(1)]
// [Guid("0000010c-0000-0000-C000-000000000046")]
// [ComVisible(false)]
// public interface IPersist
// {
//     void GetClassID(out Guid pClassID);
// }
//
// [ComImport]
// [InterfaceType(1)]
// [Guid("00000109-0000-0000-C000-000000000046")]
// [ComVisible(false)]
// public interface IPersistStream : IPersist
// {
//     [PreserveSig]
//     int IsDirty();
//     void Load(IStream pStm);
//     void Save(IStream pStm,
//         [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);
//     void GetSizeMax(out long pcbSize);
// }
//
// [ComImport]
// [InterfaceType(1)]
// [Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
// [ComVisible(false)]
// public interface IPersistStreamInit : IPersist
// {
//     [PreserveSig]
//     int IsDirty();
//     void Load(IStream pStm);
//     void Save(IStream pStm,
//         [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);
//     void GetSizeMax(out long pcbSize);
//     void InitNew();
// }