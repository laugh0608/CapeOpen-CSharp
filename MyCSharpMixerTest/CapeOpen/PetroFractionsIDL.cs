// 大白萝卜重构于 2025.05.14，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2007.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org
*/

// This file was developed/modified by Ignasi-Palou-Rivera for CO-LaN organisation - November 2007

// ---- The scope of Petroleum Fractions
// Reference document: Petroleum Fractions
/**************************************
Petroleum Fractions interfaces
**************************************/

/// <summary>
/// ICapePetroFractions interface
/// Provides methods to identify a CAPE-OPEN component.
/// </summary>
[ComImport,ComVisible(false)]
[Guid(CapeGuids.InCapePetFractionsIid)]  // "72A94DE9-9A69-4369-B508-C033CDFD4F81"
[Description("ICapePetroFractions Interface")]
public interface ICapePetroFractions
{
    /// <summary>
    /// Sets bulk characterization properties for the complete set of petroleum fractions
    /// </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    // CAPE-OPEN exceptions
    // ECapeUnknown, ECapeInvalidArgument
    [DispId(1),Description("Method SetPetroBulkProp")]
    void SetPetroBulkProp([In] string propertyId,
        [In] string basis,
        [In] double value);

    /// <summary>
    /// Sets characterization properties for individual petroleum fractions
    /// </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [DispId(2),Description("Method SetPetroCompoundProp")]
    void SetPetroCompoundProp([In] string propertyId,
        [In] object compId,
        [In] string basis,
        [In] object values);

    /// <summary>
    /// Sets characterization property cruves for the complete set of petroleum fractions
    /// </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [DispId(3),Description("Method SetPetroCurveProp")]
    void SetPetroCurveProp([In] string propertyId,
        [In] string basis,
        [In] object xValues,
        [In] object yValues);

    /// <summary>
    /// Gets bulk characterization properties for the complete set of petroleum fractions
    /// </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [DispId(4),Description("Method GetPetroBulkProp")]
    double GetPetroBulkProp([In] string propertyId,
        [In] string basis);

    /// <summary>
    /// Gets characterization properties for individual petroleum fractions
    /// </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [DispId(5),Description("Method GetPetroCompoundProp")]
    object GetPetroCompoundProp([In] string propertyId,
        [In] object compId,
        [In] string basis);

    /// <summary>
    /// Gets characterization property cruves for the complete set of petroleum fractions
    /// </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [DispId(6),Description("Method GetPetroCurveProp")]
    object GetPetroCurveProp([In] string propertyId,
        [In] string basis);
}

/// <summary>
/// The type of compound for use in petroleum fractions
/// </summary>
[Serializable]
[Guid(CapeGuids.CapeCompoundTypeIid)]  // "8091E285-3CFA-4a41-A5C4-141D0D709D87"
public enum CapeCompoundType
{
    CAPE_COMPOUND_REAL,

    CAPE_COMPOUND_ION,

    CAPE_COMPOUND_ASSAY
}