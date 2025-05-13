// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

// Type definition of the interface for subsequent use
// typedef DISPATCHER CapeThermoEquilibriumServerInterface;
/// <summary>
/// A class that implements the <see cref ="ICapeThermoSystem">ICapeThermoSystem</see> interface and provides access 
/// to COM and .Net-based property packages available on the current computer.
/// </summary>
/// <remarks>
/// <para>This class provides a list of all
/// classes Property Packages registered with COM and all .Net-based property packages that are contained in the Global Assembly Cache.</para>
/// </remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.PpCapeThermoSystemIid)]  // "B5483FD2-E8AB-4ba4-9EA6-53BBDB77CE81"
[Description("CapeThermoSystem Wrapper")]
[ClassInterface(ClassInterfaceType.None)]
public abstract class CapeThermoSystem : CapeIdentification, ICapeThermoSystem, ICapeThermoSystemCOM
{
    /// <summary>
    /// Get the list of available property packages
    /// </summary>
    /// <remarks>
    /// Returns StringArray of property package names supported by the thermo system.
    /// </remarks>
    /// <returns>
    /// The returned set of supported property packages.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    object ICapeThermoSystemCOM.GetPropertyPackages()
    {
        return GetPropertyPackages();
    }

    /// <summary>
    /// Resolve a particular property package
    /// </summary>
    /// <remarks>
    /// Resolves referenced property package to a property package interface.
    /// </remarks>
    /// <returns>
    /// The Property Package Interface.
    /// </returns>
    /// <param name = "propertyPackage">
    /// The property package to be resolved.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    object ICapeThermoSystemCOM.ResolvePropertyPackage(string propertyPackage)
    {
        return ResolvePropertyPackage(propertyPackage);
    }
    
    /// <summary>Creates an instance of the CapeThermoSystem class with the name and description provided.</summary>
    /// <remarks>You can use this constructor to specify a 
    /// specific the name and description of the thermo system.
    /// </remarks>
    /// <param name = "name">The name of the PMC.</param>
    /// <param name = "description">The description of the PMC.</param>
    protected CapeThermoSystem(string name, string description)
        : base(name, description) { }

    /// <summary>
    /// Get the list of available property packages
    /// </summary>
    /// <remarks>
    /// Returns StringArray of property package names supported by the thermo system.
    /// </remarks>
    /// <returns>
    /// The returned set of supported property packages.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    public abstract string[] GetPropertyPackages();

    /// <summary>
    /// Resolve a particular property package
    /// </summary>
    /// <remarks>
    /// Resolves referenced property package to a property package interface.
    /// </remarks>
    /// <returns>
    /// The Property Package Interface.
    /// </returns>
    /// <param name = "propertyPackage">
    /// The property package to be resolved.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    public abstract ICapeThermoPropertyPackage ResolvePropertyPackage(string propertyPackage);
}