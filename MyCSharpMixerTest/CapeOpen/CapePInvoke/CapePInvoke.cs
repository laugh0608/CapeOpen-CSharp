using System.Runtime.InteropServices;

namespace CapeOpen.CapePInvoke;

public abstract class Com
{
    [DllImport("oleaut32.dll", PreserveSig = false)]
    static extern void GetActiveObject(
        ref Guid rclsid,
        IntPtr pvReserved,
        [MarshalAs(UnmanagedType.IUnknown)] out Object ppunk
    );

    [DllImport("ole32.dll")]
    static extern int CLSIDFromProgID(
        [MarshalAs(UnmanagedType.LPWStr)] string lpszProgID,
        out Guid pclsid
    );

    public static object GetActiveObject(string progId)
    {
        Guid clsid;
        CLSIDFromProgID(progId, out clsid);

        object obj;
        GetActiveObject(ref clsid, IntPtr.Zero, out obj);

        return obj;
    }
}

// 空实现，完全为了 CapePInvoke.EnvDte.Dte dte = (CapePInvoke.EnvDte.Dte)obj; 不报错
public abstract class EnvDte
{
    public interface ISolution
    {
        IProjectCollection Projects { get; }
    }

    public interface IProjectCollection
    {
        IMyProject Item(int index);
    }

    public abstract class Dte(ISolution solution)
    {
        public ISolution Solution { get; } = solution;
    }

    public abstract class MyProject(string fullName)
    {
        public string FullName { get; } = fullName;
    }
}

public interface IMyProject { }