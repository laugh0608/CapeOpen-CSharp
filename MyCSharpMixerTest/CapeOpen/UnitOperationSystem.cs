// 大白萝卜重构于 2025.05.14，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CapeOpen;

/// <summary>
/// Interface that provides access to unit operation packages available on the computer.
/// </summary>
/// <remarks>
/// <para>This interface is used to access the various substiuent unit operation models 
/// available within the current system.</para>
/// <para>In the class library, the <see cref ="UnitOperationSystem">UnitOperationSystem</see> class provides a list of all
/// unit operation classes registered with COM and all .Net-based property unit operations that are contained in the Global Assembly Cache.</para>
/// </remarks>
[ComVisible(false)]
[Guid(CapeGuids.InUnitOperaSystemIid)]  // "08D7828D-40FD-4CA1-A71D-2F25617DA133"
[Description("IUnitOperationSystem Interface")]
public interface IUnitOperationSystem
{
    /// <summary>
    /// Get the list of available unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names available in the GAC and COM.
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Method GetUnitOperations")]
    object GetUnitOperations();

    /// <summary>
    /// Get the list of all available unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names registered with COM using the CAPE-OPEN unit operation Category (CATID).
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Method GetUnitOperations")]
    object GetComUnitOperations();

    /// <summary>
    /// Get the list of all .Net CAPE-OPEN unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names available in the GAC and in the program files "Common Files\Cape-Open" directory.
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Method GetUnitOperations")]
    object GetDotNetUnitOperations();

    /// <summary>
    /// Resolve a particular unit operation
    /// </summary>
    /// <remarks>
    /// Resolves referenced unit operation PMC to a unit operation interface.
    /// </remarks>
    /// <returns>
    /// The Unit Operation via the ICapeUnit Interface.
    /// </returns>
    /// <param name = "unitOperation">
    /// The unit operation to be resolved.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(2)]
    [Description("Method ResolveUnitOperation")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object ResolveUnitOperation(string unitOperation);
}

internal struct UnitOperationInformation
{
    public string Name;
    public string Description;
    public string CapeVersion;
    public string ComponentVersion;
    public string VendorUrl;
    public string HelpUrl;
    public string About;
    public Type Type;
    public string ProgId;
    public Guid Clsid;
    public string Assembly;
}

/// <summary>
/// A class that implements the <see cref ="ICapeThermoSystem">ICapeThermoSystem</see> interface and provides access 
/// to COM and .Net-based property packages available on the current computer.
/// </summary>
/// <remarks>
/// <para>This class provides a list of all
/// classes Property Packages registered with COM and all .Net-based property packages that are contained in the Global Assembly Cache.</para>
/// </remarks>
[ComVisible(true)]
[Guid(CapeGuids.PpUnitOperaSystemIid)]  // "3A223DEE-8414-4802-8391-D1B11B276A0F"
[Description("CapeThermoSystem Wrapper")]
[ClassInterface(ClassInterfaceType.None)]
public class UnitOperationSystem : IUnitOperationSystem
{
    private List<UnitOperationInformation> _mUnitOperations = [];
    private List<UnitOperationInformation> _mComBasedUnitOperations = [];
    private List<UnitOperationInformation> _mDotNetUnitOperations = [];

    /// <summary>
    /// A class that implements the <see cref ="IUnitOperationSystem">IUnitOperationSystem</see> interface and provides access 
    /// to COM and .Net-based unit operation models available on the current computer.
    /// </summary>
    /// <remarks>
    /// <para>This class provides a list of all
    /// cunit operation classes registered with COM and all .Net-based property packages 
    /// that are contained in the Global Assembly Cache.</para>
    /// </remarks>
    public UnitOperationSystem()
    {
        var domain = AppDomain.CurrentDomain;
        domain.AssemblyResolve += MyResolveEventHandler;
        InsertAvailableCom();
        InsertAvailableNet();
    }


    /// <summary>
    /// Get the list of all available unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names registered with COM using the CAPE-OPEN unit operation Category (CATID).
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    object IUnitOperationSystem.GetComUnitOperations()
    {
        var retVal = new string[_mComBasedUnitOperations.Count];
        for (var i = 0; i < _mComBasedUnitOperations.Count; i++)
        {
            retVal[i] = _mComBasedUnitOperations[i].ProgId;
        }
        return retVal;
    }

    /// <summary>
    /// Get the list of all .Net CAPE-OPEN unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names available in the GAC and in the program files "Common Files\Cape-Open" directory.
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    object IUnitOperationSystem.GetDotNetUnitOperations()
    {
        var retVal = new string[_mDotNetUnitOperations.Count];
        for (var i = 0; i < _mDotNetUnitOperations.Count; i++)
        {
            retVal[i] = _mDotNetUnitOperations[i].ProgId;
        }
        return retVal;
    }
        
    /// <summary>
    /// Get the list of available unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names available in the GAC and COM.
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    object IUnitOperationSystem.GetUnitOperations()
    {
        return GetUnitOperations();
    }

    /// <summary>
    /// Resolve a particular unit operation
    /// </summary>
    /// <remarks>
    /// Resolves referenced unit operation PMC to a unit operation interface.
    /// </remarks>
    /// <returns>
    /// The Unit Operation via the ICapeUnit Interface.
    /// </returns>
    /// <param name = "unitOperation">
    /// The unit operation to be resolved.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    object IUnitOperationSystem.ResolveUnitOperation(string unitOperation)
    {
        return ResolveUnitOperation(unitOperation);
    }

    /// <summary>
    /// Get the list of available unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names available in the GAC and COM.
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    public string[] GetUnitOperations()
    {
        var retVal = new string[_mUnitOperations.Count];
        for (var i = 0; i < _mUnitOperations.Count; i++)
        {
            retVal[i] = _mUnitOperations[i].ProgId;
        }
        return retVal;
    }

    /// <summary>
    /// Get the list of all available unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names registered with COM using the CAPE-OPEN unit operation Category (CATID).
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    public string[] GetComUnitOperations()
    {
        var retVal = new string[_mComBasedUnitOperations.Count];
        for (var i = 0; i < _mComBasedUnitOperations.Count; i++)
        {
            retVal[i] = _mComBasedUnitOperations[i].ProgId;
        }
        return retVal;
    }

    /// <summary>
    /// Get the list of all .Net CAPE-OPEN unit operation PMCs
    /// </summary>
    /// <remarks>
    /// Returns StringArray of unit operation PMC names available in the GAC and in the program files "Common Files\Cape-Open" directory.
    /// </remarks>
    /// <returns>
    /// The returned set of available unit operation PMCs.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    public string[] GetDotNetUnitOperations()
    {
        var retVal = new string[_mDotNetUnitOperations.Count];
        for (var i = 0; i < _mDotNetUnitOperations.Count; i++)
        {
            retVal[i] = _mDotNetUnitOperations[i].ProgId;
        }
        return retVal;
    }

    /// <summary>
    /// Resolve a particular unit operation
    /// </summary>
    /// <remarks>
    /// Resolves referenced unit operation PMC to a unit operation interface.
    /// </remarks>
    /// <returns>
    /// The Unit Operation via the ICapeUnit Interface.
    /// </returns>
    /// <param name = "unitOperation">
    /// The unit operation to be resolved.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    public object ResolveUnitOperation(string unitOperation)
    {
        for (var i = 0; i < _mUnitOperations.Count; i++)
        {
            var type = _mUnitOperations[i].Type;
            if (_mUnitOperations[i].ProgId != unitOperation) continue;
            var unitType = _mUnitOperations[i].Type;
            if (typeof(CapeUnitBase).IsAssignableFrom(unitType))
            {
                return AppDomain.CurrentDomain.CreateInstanceAndUnwrap(
                    _mUnitOperations[i].Type.Assembly.FullName, _mUnitOperations[i].Type.FullName);
            }
            return _mUnitOperations[i].Type.IsCOMObject 
                ? new UnitOperationWrapper(_mUnitOperations[i].Clsid) 
                : new UnitOperationWrapper(AppDomain.CurrentDomain.CreateInstanceAndUnwrap(
                    _mUnitOperations[i].Type.Assembly.FullName, _mUnitOperations[i].Type.FullName));
        }
        return null;
    }

    /// <summary>
    /// Used to resolve assemblies available to the <see cref = "UnitOperationSystem"/>
    /// </summary>
    /// <remarks>
    /// <para>This method resolves the assembly locations for types being created. The priority for locating 
    /// assemblies is the current applcation domain, then the Cape-Open directory located in the Common Program
    /// Files environment directory (typically C:\Program Files\Common Files\Cape-Open).</para>
    /// <para></para>
    /// </remarks>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">Information about the item to be resolved</param>
    /// <returns>The assembly that resolves the type, assembly, or resource; or null if the assembly cannot be resolved.</returns>
    public static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (args.Name == assembly.FullName)
            {
                return assembly;
            }
        }
        var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + "\\Cape-Open";
        return SearchPathForAssemblies(path, args.Name);
    }

    /// <summary>
    /// Searches a path for the desired assembly.
    /// </summary>
    /// <remarks>When this method is called, the path is scanned for the desired assembly.</remarks>
    /// <param name="path">Path to be search. The search includes all subdirectories.</param>
    /// <param name="assemblyName">The name of the deisred assembly.</param>
    /// <returns>The desired Assembly.</returns>
    public static Assembly SearchPathForAssemblies(string path, string assemblyName)
    {
        var directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            return SearchPathForAssemblies(directory, assemblyName);
        }
        var files = Directory.GetFiles(path, "dll");
        return files.Select(file => Assembly.LoadFrom(file)).FirstOrDefault(assembly => assembly.FullName == assemblyName);
    }

    private void InsertAvailableCom()
    {
        //initialize namespace Controls Category namespace Controlsds
        // CAPE-OPEN Category - namespace ControlsComponent_CATID
        // string rgcId5;
        //rgcId1 = "";//{678c09a1-7d66-11d2-a67d-00105a42887f}";
        // External CAPE-OPEN Thermo Routines - CapeExternalThermoRoutine_CATID
        //rgcId2 = "";//{678c09a2-7d66-11d2-a67d-00105a42887f}";
        // CAPE-OPEN Thermo System - CapeThermoSystem_CATID
        //rgcId3 = "";//{678c09a3-7d66-11d2-a67d-00105a42887f}";
        // CAPE-OPEN Thermo Property Package - CapeThermoPropertyPackage_CATID
        //rgcId4 = "";//{678c09a4-7d66-11d2-a67d-00105a42887f}";
        // CAPE-OPEN Unit Operation - CapeUnitOperation_CATID
        const string rgcId5 = "{678c09a5-7d66-11d2-a67d-00105a42887f}";
        // CAPE-OPEN Thermo Equilibrium Server - CapeThermoEquilibriumServer_CATID
        //rgcId6 = "";//{678c09a6-7d66-11d2-a67d-00105a42887f}";

        var key = Registry.ClassesRoot.OpenSubKey("CLSID");
        // get the classes that implement the various Cape Open categories
        // CapeUnit category
        var skNames = key.GetSubKeyNames();
        key.Close();
        foreach (var mt in skNames)
        {
            key = Registry.ClassesRoot.OpenSubKey(string.Concat("CLSID\\", mt));
            var subNames = key.GetSubKeyNames();
            var found = false;
            string name = null;//, image = null;
            Type type = null;
            var clsid = Guid.Empty;
            //System.Drawing.Bitmap bitty = null;
            foreach (var pt in subNames)
            {
                if (pt == "Implemented Categories")
                {
                    var subKey = Registry.ClassesRoot.OpenSubKey("CLSID\\" + mt + "\\Implemented Categories");
                    var vals = subKey.GetSubKeyNames();
                    foreach (var kt in vals)
                    {
                        if (string.Compare(kt, rgcId5, true) == 0)
                            found = true;
                    }
                    subKey.Close();
                }
                else if (string.Compare(pt, "progid", true) == 0 && found)
                {
                    var subKey = Registry.ClassesRoot.OpenSubKey(string.Concat("CLSID\\", mt, "\\ProgID"));
                    var vals = subKey.GetValueNames();
                    if (vals.Length >= 1)
                    {
                        name = (string)(subKey.GetValue(vals[0], typeof(string)));
                        clsid = new Guid(mt);
                        type = Type.GetTypeFromCLSID(clsid);
                    }
                    subKey.Close();
                    var descriptionKey = Registry.ClassesRoot.OpenSubKey(string.Concat("CLSID\\", mt, "\\CapeDescription"));
                    if (descriptionKey == null 
                        || typeof(CapeUnitBase).IsAssignableFrom(type) 
                        || clsid == new Guid("48c6795c-a67d-4807-8881-c4c9e02418d0")) continue;
                    var unit = new UnitOperationInformation();
                    var descriptions = descriptionKey.GetValueNames();
                    unit.Name = descriptionKey.GetValue("Name", string.Empty).ToString();
                    unit.Description = descriptionKey.GetValue("Description", string.Empty).ToString();
                    unit.CapeVersion = descriptionKey.GetValue("CapeVersion", string.Empty).ToString();
                    unit.ComponentVersion = descriptionKey.GetValue("ComponentVersion", string.Empty).ToString();
                    unit.VendorUrl = descriptionKey.GetValue("VendorURL", string.Empty).ToString();
                    unit.HelpUrl = descriptionKey.GetValue("HelpURL", string.Empty).ToString();
                    unit.About = descriptionKey.GetValue("About", string.Empty).ToString();
                    unit.Type = type;
                    unit.ProgId = name;
                    unit.Clsid = clsid;
                    unit.Assembly = "";
                    _mUnitOperations.Add(unit);
                    _mComBasedUnitOperations.Add(unit);
                    descriptionKey.Close();
                    //infos.Add(null);
                }
                //else if (found == true) //&& ((subNames[i].CompareTo("ToolboxBitmap32") == 0) ||	subNames[i].CompareTo("DefaultIcon")))
                //{
                //    Microsoft.Win32.RegistryKey subKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(String.Concat("CLSID\\", subNames[n], "\\ToolboxBitmap32"));
                //    string[] vals = subKey.GetValueNames();
                //    if (vals.Length >= 1)
                //    {
                //        image = (String)(subKey.GetValue(vals[0], typeof(String)));
                //        int pos = image.IndexOf(",");
                //        if (pos == -1)
                //        {
                //            System.Drawing.Icon ic = null;// System::Drawing::Icon::FromHandle(ExtractIcon(System::Diagnostics::Process::GetCurrentProcess()->Handle,image,0));
                //            bitty = ic.ToBitmap();
                //        }
                //        else
                //        {
                //            /*
                //            first = image->Substring(0,pos);
                //            second = image->Substring(pos+1);
                //            IntPtr ptr = System::Diagnostics::Process::GetCurrentProcess()->Handle;
                //            UInt32 hic = ExtractIcon(ptr,first,Convert::ToUInt32(second));
                //            if(hic ==0)
                //            bitty = gcnew Bitmap(imageList2->Images[0]);
                //            else
                //            {
                //            System::Drawing::Icon^ ic = System::Drawing::Icon::FromHandle(IntPtr(hic));
                //            bitty = ic->ToBitmap();
                //            }
                //            */
                //        }
                //    }
                //    subkey.Close();
                //}
            }
            if (found && name != null)
            {
                //if(!listBox1.Items.Contains(new ListViewItem(name)))
                //{
                //    listBox1.Items.Add(name);
                //    if(bitty == nullptr)
                //        bitty = gcnew Bitmap(imageList2.Images[0]);
                //    imageList1.Images.Add(bitty);
                //    ToolStripButton button = new ToolStripButton(name, bitty);
                //    this.toolStrip1.Items.Add(button);
                //    button.Visible = true;
                //    button.Tag = type;
                //    button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                //}
            }
            key.Close();
        }
    }

    private void InsertAvailableNet()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            InsertUnitsFromAssembly(assembly);
        }
        var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
        path += "\\Cape-Open";
        InsertsUnitsFromPath(path);
    }

    private void InsertUnitsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes();
        foreach (var type in types)
        {
            if (!typeof(ICapeUnit).IsAssignableFrom(type) 
                || type.IsAbstract
                || type == typeof(UnitOperationWrapper)) continue;
            var unit = new UnitOperationInformation();
            var attributes = type.GetCustomAttributes(true);
            foreach (var mt in attributes)
            {
                switch (mt)
                {
                    case CapeNameAttribute nameAttribute:
                        unit.Name = nameAttribute.Name;
                        break;
                    case CapeDescriptionAttribute descriptionAttribute:
                        unit.Description = descriptionAttribute.Description;
                        break;
                    case CapeVersionAttribute versionAttribute:
                        unit.CapeVersion = versionAttribute.Version;
                        break;
                    case CapeVendorUrlAttribute vendorUrlAttribute:
                        unit.VendorUrl = vendorUrlAttribute.VendorUrl;
                        break;
                    case CapeHelpUrlAttribute helpUrlAttribute:
                        unit.HelpUrl = helpUrlAttribute.HelpUrl;
                        break;
                    case CapeAboutAttribute aboutAttribute:
                        unit.About = aboutAttribute.About;
                        break;
                }

                unit.ProgId = type.FullName;
                unit.Assembly = assembly.FullName;
            }
            unit.Type = type;
            _mUnitOperations.Add(unit);
            _mDotNetUnitOperations.Add(unit);
        }
    }

    private void InsertUnitsFromSpecialFolder(Environment.SpecialFolder folder)
    {
        var path = Environment.GetFolderPath(folder);
        InsertsUnitsFromPath(path);
    }

    private void InsertsUnitsFromPath(string path)
    {
        var directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            InsertsUnitsFromPath(directory);
        }
        var files = Directory.GetFiles(path, "dll");
        foreach (var file in files)
        {
            var assembly = Assembly.LoadFrom(file);
            InsertUnitsFromAssembly(assembly);
        }
    }
}