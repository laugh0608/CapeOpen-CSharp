// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2002.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org

This file has been edited using the editor from Microsoft Visual Studio 6.0
This file can view properly with any basic editors and browsers (validation done under MS Windows and Unix)
*/

// This file was developed/modified by JEAN-PIERRE-BELAUD for CO-LaN organisation - March 2003

// ---- The scope of thermodynamic and physical properties interface
// Reference document: Thermodynamic and physical properties
// ThermoIDL 文件拆分，序号 03

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>This interface should be implemented by all Thermodynamic and Physical 
/// Properties components that need an ICapeThermoMaterial interface in order to set 
/// and get a Material’s property values.</summary>
[ComImport,ComVisible(false)]
[Guid(CapeOpenGuids.InCapeTheMateConComIid)]  // "678C0A9C-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoMaterialContext Interface")]
internal interface ICapeThermoMaterialContextCOM
{
    /// <summary>Allows the client of a component that implements this interface to 
    /// pass an ICapeThermoMaterial interface to the component, so that it can 
    /// access the properties of a Material.</summary>
    /// <remarks><para>	The SetMaterial method allows a Thermodynamic and 
    /// Physical Properties component, such as a Property Package, to be given the 
    /// ICapeThermoMaterial interface of a Material Object. This interface gives the 
    /// component access to the description of the Material for which Property 
    /// Calculations or Equilibrium Calculations are required. The component can 
    /// access property values directly using this interface. A client can also use 
    /// the ICapeThermoMaterial interface to query a Material Object for its 
    /// ICapeThermoCompounds and ICapeThermoPhases interfaces, which provide access 
    /// to Compound and Phase information, respectively.</para>
    /// <para>It is envisaged that the SetMaterial method will be used to check that 
    /// the Material Interface supplied is valid and useable. For example, a 
    /// Property Package may check that there are some Compounds in a Material 
    /// Object and that those Compounds can be identified by the Property Package. 
    /// In addition a Property Package may perform any initialisation that depends 
    /// on the configuration of a Material Object. A Property Calculator component 
    /// might typically use this method to query the Material Object for any required 
    /// information concerning the Compounds.</para>
    /// <para>Calling the UnsetMaterial method of the ICapeThermoMaterialContext 
    /// interface has the effect of removing the interface set by the SetMaterial 
    /// method.</para></remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation.</exception>
    /// <exception cref = "ECapeInvalidArgument">The input argument is not a valid 
    /// CapeInterface.</exception>
    /// <exception cref ="ECapeFailedInitialisation"><para>The pre-requisites for the 
    /// property calculation are not valid. For example:</para>
    /// <para>• There are no Compounds in the object that implements the 
    /// ICapeThermoMaterial interface.</para>
    /// <para>• The Compounds cannot be identified by the client (e.g. a Property 
    /// Package). This case is a possibility if the way a Material Object has been 
    /// configured by a PME is not consistent with the Property Package being used.</para>
    /// </exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method SetMaterial")]
    void SetMaterial([In]
        [MarshalAs(UnmanagedType.IDispatch)]object material);

    /// <summary>Removes any previously set Material interface.</summary>
    /// <remarks><para>The UnsetMaterial method removes any Material interface previously 
    /// set by a call to the SetMaterial method of the ICapeThermoMaterialContext 
    /// interface. This means that any methods of other interfaces that depend on having 
    /// a valid Material Interface, for example methods of the ICapeThermoPropertyRoutine 
    /// or ICapeThermoEquilibriumRoutine interfaces, should behave in the same way as if 
    /// the SetMaterial method had never been called.</para>
    /// <para>If UnsetMaterial is called before a call to SetMaterial it has no effect 
    /// and no exception should be raised.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the current 
    /// implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    [DispId(0x00000002)]
    [Description("Method UnsetMaterial")]
    void UnsetMaterial();
}

/// <summary>This interface should be implemented by all Thermodynamic and Physical 
/// Properties components that need an ICapeThermoMaterial interface in order to set 
/// and get a Material’s property values.</summary>
[ComVisible(false)]
[Description("ICapeThermoMaterialContext Interface")]
public interface ICapeThermoMaterialContext
{
    /// <summary>Allows the client of a component that implements this interface to 
    /// pass an ICapeThermoMaterial interface to the component, so that it can 
    /// access the properties of a Material.</summary>
    /// <remarks><para>	The SetMaterial method allows a Thermodynamic and 
    /// Physical Properties component, such as a Property Package, to be given the 
    /// ICapeThermoMaterial interface of a Material Object. This interface gives the 
    /// component access to the description of the Material for which Property 
    /// Calculations or Equilibrium Calculations are required. The component can 
    /// access property values directly using this interface. A client can also use 
    /// the ICapeThermoMaterial interface to query a Material Object for its 
    /// ICapeThermoCompounds and ICapeThermoPhases interfaces, which provide access 
    /// to Compound and Phase information, respectively.</para>
    /// <para>It is envisaged that the SetMaterial method will be used to check that 
    /// the Material Interface supplied is valid and useable. For example, a 
    /// Property Package may check that there are some Compounds in a Material 
    /// Object and that those Compounds can be identified by the Property Package. 
    /// In addition a Property Package may perform any initialisation that depends 
    /// on the configuration of a Material Object. A Property Calculator component 
    /// might typically use this method to query the Material Object for any required 
    /// information concerning the Compounds.</para>
    /// <para>Calling the UnsetMaterial method of the ICapeThermoMaterialContext 
    /// interface has the effect of removing the interface set by the SetMaterial 
    /// method.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation.</exception>
    /// <exception cref = "ECapeInvalidArgument">The input argument is not a valid 
    /// CapeInterface.</exception>
    /// <exception cref ="ECapeFailedInitialisation"><para>The pre-requisites for the 
    /// property calculation are not valid. For example:</para>
    /// <para>• There are no Compounds in the object that implements the 
    /// ICapeThermoMaterial interface.</para>
    /// <para>• The Compounds cannot be identified by the client (e.g. a Property 
    /// Package). This case is a possibility if the way a Material Object has been 
    /// configured by a PME is not consistent with the Property Package being used.</para>
    /// </exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method SetMaterial")]
    void SetMaterial(ICapeThermoMaterial material);

    /// <summary>Removes any previously set Material interface.</summary>
    /// <remarks><para>The UnsetMaterial method removes any Material interface previously 
    /// set by a call to the SetMaterial method of the ICapeThermoMaterialContext 
    /// interface. This means that any methods of other interfaces that depend on having 
    /// a valid Material Interface, for example methods of the ICapeThermoPropertyRoutine 
    /// or ICapeThermoEquilibriumRoutine interfaces, should behave in the same way as if 
    /// the SetMaterial method had never been called.</para>
    /// <para>If UnsetMaterial is called before a call to SetMaterial it has no effect 
    /// and no exception should be raised.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the current 
    /// implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    [DispId(0x00000002)]
    [Description("Method UnsetMaterial")]
    void UnsetMaterial();
}

/// <summary>When implemented by a Property Package, this 
/// interface is used to access the list of Compounds that the Property Package can 
/// deal with, as well as the Compounds Physical Properties. When implemented by a 
/// Material Object, the interface is used for the same purpose but is applied to 
/// the Compounds present in the Material.</summary>
/// <remarks><para>Any component or object that maintains a list of Compounds must 
/// implement the ICapeThermoCompounds interface. Within the scope of this 
/// specification this means that it must be implemented by Property Package 
/// components and Material Objects. When implemented by a Property Package, this 
/// interface is used to access the list of Compounds that the Property Package can 
/// deal with, as well as the Compounds Physical Properties. When implemented by a 
/// Material Object, the interface is used for the same purpose but is applied to 
/// the Compounds present in the Material.</para>
/// <para>It is recommended for the SetMaterial method of the ICapeThermoMaterialContext 
/// interface to be called prior to calling any of the methods described below. A 
/// Property Package may contain Physical Property values for all the Compounds that 
/// it supports or it may rely on the PME to provide these data through the Material 
/// Object.</para>
/// </remarks>
[ComVisible(false)]
[Description("ICapeThermoCompounds Interface")]
public interface ICapeThermoCompounds
{
    /// <summary>Returns the values of constant Physical Properties for the specified Compounds.</summary>
    /// <remarks><para>The GetConstPropList method can be used in order to check 
    /// which constant Physical Properties are available.</para>
    /// <para>If the number of requested Physical Properties is P and the number of 
    /// Compounds is C, the propvals array will contain C*P variants. The first C 
    /// variants will be the values for the first requested Physical Property (one 
    /// variant for each Compound) followed by C values of constants for the second 
    /// Physical Property, and so on. The actual type of values returned (Double, 
    /// String, etc.) depends on the Physical Property as specified in section 7.5.2.</para>
    /// <para>Physical Properties are returned in a fixed set of units as specified 
    /// in section 7.5.2.</para>
    /// <para>If the compIds argument is set to UNDEFINED this is a request to return 
    /// property values for all compounds in the component that implements the 
    /// ICapeThermoCompounds interface with the compound order the same as that 
    /// returned by the GetCompoundList method. For example, if the interface is 
    /// implemented by a Property Package component the property request with compIds 
    /// set to UNDEFINED means all compounds in the Property Package rather than all 
    /// compounds in the Material Object passed to the Property package.</para>
    /// <para>If any Physical Property is not available for one or more Compounds, 
    /// then undefined values must be returned for those combinations and an 
    /// ECapeThrmPropertyNotAvailable exception must be raised. If the exception is 
    /// raised, the client should check all the values returned to determine which 
    /// is undefined.</para>
    /// </remarks>
    /// <param name = "props">The list of Physical Property identifiers. Valid
    /// identifiers for constant Physical Properties are listed in
    /// section 7.5.2.</param>
    /// <param name = "compIds">List of Compound identifiers for which constants are 
    /// to be retrieved. Set compIds = UNDEFINED to denote all Compounds in the 
    /// component that implements the ICapeThermoCompounds interface.</param>
    /// <returns>Values of constants for the specified Compounds.</returns>
    /// <exception cref = "ECapeNoImpl">The operation GetCompoundConstant is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation. This exception should be 
    /// raised if no compounds or no properties are supported.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">At least one item in the 
    /// list of Physical Properties is not available for a particular Compound. This 
    /// exception is meant to be treated as a warning rather than as an error.</exception>
    /// <exception cref = "ECapeLimitedImpl">One or more Physical Properties are not 
    /// supported by the component that implements this interface. This exception 
    /// should also be raised if any element of the props argument is not recognised 
    /// since the list of Physical Properties in section 7.5.2 is not intended to be 
    /// exhaustive and an unrecognised Physical Property identifier may be valid. If
    /// no Physical Properties at all are supported ECapeNoImpl should be raised 
    /// (see above).</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example, an unrecognised Compound identifier or 
    /// UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetCompoundConstant method. The error would not be raised when the 
    /// GetCompoundConstant method is implemented by a Material Object.</exception>
    [DispId(0x00000001)]
    [Description("Method GetCompoundConstant")]
    object[] GetCompoundConstant(string[] props, string[] compIds);

    /// <summary>Returns the list of all Compounds. This includes the Compound 
    /// identifiers recognised and extra information that can be used to further 
    /// identify the Compounds.</summary>
    /// <remarks><para>If any item cannot be returned then the value should be set 
    /// to UNDEFINED. The same information can also be extracted using the 
    /// GetCompoundConstant method. The equivalences between GetCompoundList 
    /// arguments and Compound constant Physical Properties, as specified in section 
    /// 7.5.2, is as follows:</para>
    /// <para>compIds - No equivalence. compIds is an artefact, which is assigned by 
    /// the component that implements the GetCompoundList method. This string will 
    /// normally contain a unique Compound identifier such as "benzene". It must be 
    /// used in all the arguments which are named “compIds” in the methods of the
    ///ICapeThermoCompounds and ICapeThermoMaterial interfaces.</para>
    /// <para>Formulae - chemicalFormula</para>
    /// <para>names - iupacName</para>
    /// <para>boilTemps - normalBoilingPoint</para>
    /// <para>molwts - molecularWeight</para>
    /// <para>casnos casRegistryNumber</para>
    /// <para>When the ICapeThermoCompounds interface is implemented by a Material 
    /// Object, the list of Compounds returned is fixed when the Material Object is 
    /// configured.</para>
    /// <para>For a Property Package component, the Property Package will normally 
    /// contain a limited set of Compounds selected for a particular application, 
    /// rather than all possible Compounds that could be available to a proprietary 
    /// Properties System.</para>
    /// <para>In order to identify the Compounds of a Property Package, the PME, or 
    /// other client, will use the casnos argument rather than the compIds. This is 
    /// because different PMEs may give different names to the same Compounds and the
    /// casnos is (almost always) unique. If the casnos is not available (e.g. for 
    /// petroleum fractions), or not unique, the other pieces of information returned 
    /// by GetCompoundList can be used to distinguish the Compounds. It should be 
    /// noted, however, that for communication with a Property Package a client must 
    /// use the Compound identifiers returned in the compIds argument.</para>
    /// </remarks>
    /// <param name = "compIds">List of Compound identifiers</param>
    /// <param name = "formulae">List of Compound formulae</param>
    /// <param name = "names">List of Compound names.</param>
    /// <param name = "boilTemps">List of boiling point temperatures.</param>
    /// <param name = "molwts">List of molecular weights.</param>
    /// <param name = "casnos">List of Chemical Abstract Service (CAS) Registry
    /// numbers.</param>
    /// <exception cref = "ECapeNoImpl">The operation GetCompoundList is “not” 
    /// implemented even if this method can be called for reasons of compatibility
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetCompoundList operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the Property 
    /// Package required the SetMaterial method to be called before calling the 
    /// GetCompoundList method. The error would not be raised when the 
    /// GetCompoundList method is implemented by a Material Object.</exception>
    [DispId(0x00000002)]
    [Description("Method GetCompoundList")]
    void GetCompoundList(ref string[] compIds, ref string[] formulae, 
        ref string[] names, ref double[] boilTemps, ref double[] molwts, ref string[] casnos);

    /// <summary>
    /// Returns the list of supported constant Physical Properties.
    /// </summary>
    /// <returns>List of identifiers for all supported constant Physical Properties. 
    /// The standard constant property identifiers are listed in section 7.5.2.
    /// </returns>
    /// <remarks>
    /// <para>MGetConstPropList returns identifiers for all the constant Physical 
    /// Properties that can be retrieved by the GetCompoundConstant method. If no 
    /// properties are supported, UNDEFINED should be returned. The CAPE-OPEN 
    /// standards do not define a minimum list of Physical Properties to be made 
    /// available by a software component that implements the ICapeThermoCompounds 
    /// interface.</para>
    /// <para>A component that implements the ICapeThermoCompounds interface may 
    /// return constant Physical Property identifiers which do not belong to the 
    /// list defined in section 7.5.2.</para>
    /// <para>However, these proprietary identifiers may not be understood by most 
    /// of the clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetConstPropList is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the Get-ConstPropList operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetConstPropList method. The error would not be raised when the 
    /// GetConstPropList method is implemented by a Material Object.</exception>
    [DispId(0x00000003)]
    [Description("Method GetConstPropList")]
    string[] GetConstPropList();

    /// <summary>Returns the number of Compounds supported.</summary>
    /// <returns>Number of Compounds supported.</returns>
    /// <remarks>The number of Compounds returned by this method must be equal to 
    /// the number of Compound identifiers that are returned by the GetCompoundList 
    /// method of this interface. It must be zero or a positive number.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    /// <exception cref ="ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetNumCompounds method. The error would not be raised when the 
    /// GetNumCompounds method is implemented by a Material Object.</exception>
    [DispId(0x00000004)]
    [Description("Method GetNumCompounds")]
    int GetNumCompounds();

    /// <summary>Returns the values of pressure-dependent Physical Properties for 
    /// the specified pure Compounds.</summary>
    /// <param name = "props">The list of Physical Property identifiers. Valid
    /// identifiers for pressure-dependent Physical Properties are listed in section 
    /// 7.5.4</param>
    /// <param name ="pressure">Pressure (in Pa) at which Physical Properties are
    /// evaluated</param>
    /// <param name ="compIds">List of Compound identifiers for which Physical
    /// Properties are to be retrieved. Set compIds = UNDEFINED to denote all 
    /// Compounds in the component that implements the ICapeThermoCompounds 
    /// interface.</param>
    /// <param name = "propVals">>Property values for the Compounds specified.</param>
    /// <remarks><para>The GetPDependentPropList method can be used in order to 
    /// check which Physical Properties are available.</para>
    /// <para>If the number of requested Physical Properties is P and the number 
    /// Compounds is C, the propvals array will contain C*P values. The first C 
    /// will be the values for the first requested Physical Property followed by C 
    /// values for the second Physical Property, and so on.</para>
    /// <para>Physical Properties are returned in a fixed set of units as specified 
    /// in section 7.5.4.</para>
    /// <para>If the compIds argument is set to UNDEFINED this is a request to return 
    /// property values for all compounds in the component that implements the 
    /// ICapeThermoCompounds interface with the compound order the same as that 
    /// returned by the GetCompoundList method. For example, if the interface is 
    /// implemented by a Property Package component the property request with compIds 
    /// set to UNDEFINED means all compounds in the Property Package rather than all 
    /// compounds in the Material Object passed to the Property package.</para>
    /// <para>If any Physical Property is not available for one or more Compounds, 
    /// then undefined valuesm must be returned for those combinations and an 
    /// ECapeThrmPropertyNotAvailable exception must be raised. If the exception is 
    /// raised, the client should check all the values returned to determine which is 
    /// undefined.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation. This exception should be raised if no Compounds 
    /// or no Physical Properties are supported.</exception>
    /// <exception cref ="ECapeLimitedImpl">One or more Physical Properties are not 
    /// supported by the component that implements this interface. This exception 
    /// should also be raised (rather than ECapeInvalidArgument) if any element of 
    /// the props argument is not recognised since the list of Physical Properties 
    /// in section 7.5.4 is not intended to be exhaustive and an unrecognised
    /// Physical Property identifier may be valid. If no Physical Properties at all 
    /// are supported, ECapeNoImpl should be raised (see above).</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example UNDEFINED for argument props.</exception>
    /// <exception cref = "ECapeOutOfBounds">The value of the pressure is outside of
    /// the range of values accepted by the Property Package.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">At least one item in the 
    /// properties list is not available for a particular compound.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetPDependentProperty method. The error would not be raised when the 
    /// GetPDependentProperty method is implemented by a Material Object.</exception>
    [DispId(0x00000005)]
    [Description("Method GetPDependentProperty")]
    void GetPDependentProperty(string[] props, double pressure, string[] compIds, ref double[] propVals);

    ///<summary>Returns the list of supported pressure-dependent properties.</summary>
    ///<returns>The list of Physical Property identifiers for all supported 
    /// pressure-dependent properties. The standard identifiers are listed in 
    /// section 7.5.4</returns>
    /// <remarks>
    /// <para>GetPDependentPropList returns identifiers for all the pressure-dependent 
    /// properties that can be retrieved by the GetPDependentProperty method. If no 
    /// properties are supported UNDEFINED should be returned. The CAPE-OPEN standards 
    /// do not define a minimum list of Physical Properties to be made available by 
    /// a software component that implements the ICapeThermoCompounds interface.</para>
    /// <para>A component that implements the ICapeThermoCompounds interface may 
    /// return identifiers which do not belong to the list defined in section 7.5.4. 
    /// However, these proprietary identifiers may not be understood by most of the 
    /// clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref ="ECapeBadInvOrder">The error to be raised if the Property 
    /// Package required the SetMaterial method to be called before calling the 
    /// GetPDependentPropList method. The error would not be raised when the 
    /// GetPDependentPropList method is implemented by a Material Object.</exception>
    [DispId(0x00000006)]
    [Description("Method GetPDependentPropList")]
    string[] GetPDependentPropList();

    /// <summary>Returns the values of temperature-dependent Physical Properties for 
    /// the specified pure Compounds.</summary>
    /// <param name ="props">The list of Physical Property identifiers. Valid
    /// identifiers for temperature-dependent Physical Properties are listed in 
    /// section 7.5.3</param>
    /// <param name = "temperature">Temperature (in K) at which properties are 
    /// evaluated.</param>
    /// <param name ="compIds">List of Compound identifiers for which Physical
    /// Properties are to be retrieved. Set compIds = UNDEFINED to denote all 
    /// Compounds in the component that implements the ICapeThermoCompounds 
    /// interface .</param>
    /// <param name = "propVals">Physical Property values for the Compounds specified.
    /// </param>
    /// <remarks> <para>The GetTDependentPropList method can be used in order to 
    /// check which Physical Properties are available.</para>
    /// <para>If the number of requested Physical Properties is P and the number of 
    /// Compounds is C, the propvals array will contain C*P values. The first C will 
    /// be the values for the first requested Physical Property followed by C values 
    /// for the second Physical Property, and so on.</para>
    /// <para>Properties are returned in a fixed set of units as specified in 
    /// section 7.5.3.</para>
    /// <para>If the compIds argument is set to UNDEFINED this is a request to return 
    /// property values for all compounds in the component that implements the 
    /// ICapeThermoCompounds interface with the compound order the same as that 
    /// returned by the GetCompoundList method. For example, if the interface is 
    /// implemented by a Property Package component the property request with compIds 
    /// set to UNDEFINED means all compounds in the Property Package rather than all 
    /// compounds in the Material Object passed to the Property package.</para>
    /// <para>If any Physical Property is not available for one or more Compounds, 
    /// then undefined values must be returned for those combinations and an 
    /// ECapeThrmPropertyNotAvailable exception must be raised. If the exception is 
    /// raised, the client should check all the values returned to determine which is 
    /// undefined.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl"> – The operation is “not” implemented even 
    /// if this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation. This exception should be raised if no 
    /// Compounds or no Physical Properties are supported.</exception>
    /// <exception cref = "ECapeLimitedImpl">One or more Physical Properties are not
    /// supported by the component that implements this interface. This exception 
    /// should also be raised (rather than ECapeInvalidArgument) if any element of 
    /// the props argument is not recognised since the list of properties in section 
    /// 7.5.3 is not intended to be exhaustive and an unrecognised Physical Property 
    /// identifier may be valid. If no properties at all are supported ECapeNoImpl 
    /// should be raised (see above).</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example UNDEFINED for argument props.</exception> 
    /// <exception cref = "ECapeOutOfBounds">The value of the temperature is outside
    /// of the range of values accepted by the Property Package.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">at least one item in the 
    /// properties list is not available for a particular compound.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder"> The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetTDependentProperty method. The error would not be raised when the 
    /// GetTDependentProperty method is implemented by a Material Object.</exception>
    [DispId(0x00000007)]
    [Description("Method GetTDependentProperty")]
    void GetTDependentProperty(string[] props, double temperature, string[] compIds, ref double[] propVals);

    /// <summary>Returns the list of supported temperature-dependent Physical 
    /// Properties.</summary>
    /// <returns>The list of Physical Property identifiers for all supported 
    /// temperature-dependent properties. The standard identifiers are listed in 
    /// section 7.5.3</returns>
    /// <remarks><para>GetTDependentPropList returns identifiers for all the 
    /// temperature-dependent Physical Properties that can be retrieved by the 
    /// GetTDependentProperty method. If no properties are supported UNDEFINED 
    /// should be returned. The CAPE-OPEN standards do not define a minimum list of 
    /// properties to be made available by a software component that implements the 
    /// ICapeThermoCompounds interface.</para>
    /// <para>A component that implements the ICapeThermoCompounds interface may 
    /// return identifiers which do not belong to the list defined in section 
    /// 7.5.3. However, these proprietary identifiers may not be understood by most 
    /// of the clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported
    /// by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s),
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the Property 
    /// Package required the SetMaterial method to be called before calling the 
    /// GetTDependentPropList method. The error would not be raised when the 
    /// GetTDependentPropList method is implemented by a Material Object.</exception>
    [DispId(0x00000008)]
    [Description("Method GetTDependentPropList")]
    string[] GetTDependentPropList();
}

/// <summary>When implemented by a Property Package, this 
/// interface is used to access the list of Compounds that the Property Package can 
/// deal with, as well as the Compounds Physical Properties. When implemented by a 
/// Material Object, the interface is used for the same purpose but is applied to 
/// the Compounds present in the Material.</summary>
/// <remarks><para>Any component or object that maintains a list of Compounds must 
/// implement the ICapeThermoCompounds interface. Within the scope of this 
/// specification this means that it must be implemented by Property Package 
/// components and Material Objects. When implemented by a Property Package, this 
/// interface is used to access the list of Compounds that the Property Package can 
/// deal with, as well as the Compounds Physical Properties. When implemented by a 
/// Material Object, the interface is used for the same purpose but is applied to 
/// the Compounds present in the Material.</para>
/// <para>It is recommended for the SetMaterial method of the ICapeThermoMaterialContext 
/// interface to be called prior to calling any of the methods described below. A 
/// Property Package may contain Physical Property values for all the Compounds that 
/// it supports or it may rely on the PME to provide these data through the Material 
/// Object.</para>
/// </remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.InCapeTheCompUndComIid)]  // "678C0A9D-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoCompounds Interface")]
internal interface ICapeThermoCompoundsCOM
{
    /// <summary>Returns the values of constant Physical Properties for the specified Compounds.</summary>
    /// <remarks><para>The GetConstPropList method can be used in order to check 
    /// which constant Physical Properties are available.</para>
    /// <para>If the number of requested Physical Properties is P and the number of 
    /// Compounds is C, the propvals array will contain C*P variants. The first C 
    /// variants will be the values for the first requested Physical Property (one 
    /// variant for each Compound) followed by C values of constants for the second 
    /// Physical Property, and so on. The actual type of values returned (Double, 
    /// String, etc.) depends on the Physical Property as specified in section 7.5.2.</para>
    /// <para>Physical Properties are returned in a fixed set of units as specified 
    /// in section 7.5.2.</para>
    /// <para>If the compIds argument is set to UNDEFINED this is a request to return 
    /// property values for all compounds in the component that implements the 
    /// ICapeThermoCompounds interface with the compound order the same as that 
    /// returned by the GetCompoundList method. For example, if the interface is 
    /// implemented by a Property Package component the property request with compIds 
    /// set to UNDEFINED means all compounds in the Property Package rather than all 
    /// compounds in the Material Object passed to the Property package.</para>
    /// <para>If any Physical Property is not available for one or more Compounds, 
    /// then undefined values must be returned for those combinations and an 
    /// ECapeThrmPropertyNotAvailable exception must be raised. If the exception is 
    /// raised, the client should check all the values returned to determine which 
    /// is undefined.</para>
    /// </remarks>
    /// <param name = "props">The list of Physical Property identifiers. Valid
    /// identifiers for constant Physical Properties are listed in
    /// section 7.5.2.</param>
    /// <param name = "compIds">List of Compound identifiers for which constants are 
    /// to be retrieved. Set compIds = UNDEFINED to denote all Compounds in the 
    /// component that implements the ICapeThermoCompounds interface.</param>
    /// <returns>Values of constants for the specified Compounds.</returns>
    /// <exception cref = "ECapeNoImpl">The operation GetCompoundConstant is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation. This exception should be 
    /// raised if no compounds or no properties are supported.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">At least one item in the 
    /// list of Physical Properties is not available for a particular Compound. This 
    /// exception is meant to be treated as a warning rather than as an error.</exception>
    /// <exception cref = "ECapeLimitedImpl">One or more Physical Properties are not 
    /// supported by the component that implements this interface. This exception 
    /// should also be raised if any element of the props argument is not recognised 
    /// since the list of Physical Properties in section 7.5.2 is not intended to be 
    /// exhaustive and an unrecognised Physical Property identifier may be valid. If
    /// no Physical Properties at all are supported ECapeNoImpl should be raised 
    /// (see above).</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example, an unrecognised Compound identifier or 
    /// UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetCompoundConstant method. The error would not be raised when the 
    /// GetCompoundConstant method is implemented by a Material Object.</exception>
    [DispId(0x00000001)]
    [Description("Method GetCompoundConstant")]
    object GetCompoundConstant(
        [In] object props, [In] object compIds);

    /// <summary>Returns the list of all Compounds. This includes the Compound 
    /// identifiers recognised and extra information that can be used to further 
    /// identify the Compounds.</summary>
    /// <remarks><para>If any item cannot be returned then the value should be set 
    /// to UNDEFINED. The same information can also be extracted using the 
    /// GetCompoundConstant method. The equivalences between GetCompoundList 
    /// arguments and Compound constant Physical Properties, as specified in section 
    /// 7.5.2, is as follows:</para>
    /// <para>compIds - No equivalence. compIds is an artefact, which is assigned by 
    /// the component that implements the GetCompoundList method. This string will 
    /// normally contain a unique Compound identifier such as "benzene". It must be 
    /// used in all the arguments which are named “compIds” in the methods of the
    ///ICapeThermoCompounds and ICapeThermoMaterial interfaces.</para>
    /// <para>Formulae - chemicalFormula</para>
    /// <para>names - iupacName</para>
    /// <para>boilTemps - normalBoilingPoint</para>
    /// <para>molwts - molecularWeight</para>
    /// <para>casnos casRegistryNumber</para>
    /// <para>When the ICapeThermoCompounds interface is implemented by a Material 
    /// Object, the list of Compounds returned is fixed when the Material Object is 
    /// configured.</para>
    /// <para>For a Property Package component, the Property Package will normally 
    /// contain a limited set of Compounds selected for a particular application, 
    /// rather than all possible Compounds that could be available to a proprietary 
    /// Properties System.</para>
    /// <para>In order to identify the Compounds of a Property Package, the PME, or 
    /// other client, will use the casnos argument rather than the compIds. This is 
    /// because different PMEs may give different names to the same Compounds and the
    /// casnos is (almost always) unique. If the casnos is not available (e.g. for 
    /// petroleum fractions), or not unique, the other pieces of information returned 
    /// by GetCompoundList can be used to distinguish the Compounds. It should be 
    /// noted, however, that for communication with a Property Package a client must 
    /// use the Compound identifiers returned in the compIds argument.</para>
    /// </remarks>
    /// <param name = "compIds">List of Compound identifiers</param>
    /// <param name = "formulae">List of Compound formulae</param>
    /// <param name = "names">List of Compound names.</param>
    /// <param name = "boilTemps">List of boiling point temperatures.</param>
    /// <param name = "molwts">List of molecular weights.</param>
    /// <param name = "casnos">List of Chemical Abstract Service (CAS) Registry
    /// numbers.</param>
    /// <exception cref = "ECapeNoImpl">The operation GetCompoundList is “not” 
    /// implemented even if this method can be called for reasons of compatibility
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetCompoundList operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the Property 
    /// Package required the SetMaterial method to be called before calling the 
    /// GetCompoundList method. The error would not be raised when the 
    /// GetCompoundList method is implemented by a Material Object.</exception>
    [DispId(0x00000002)]
    [Description("Method GetCompoundList")]
    void GetCompoundList(
        [In][Out]ref  object compIds,
        [In][Out]ref  object formulae,
        [In][Out]ref  object names,
        [In][Out]ref  object boilTemps,
        [In][Out]ref  object molwts,
        [In][Out]ref  object casnos);

    /// <summary>
    /// Returns the list of supported constant Physical Properties.
    /// </summary>
    /// <returns>List of identifiers for all supported constant Physical Properties. 
    /// The standard constant property identifiers are listed in section 7.5.2.
    /// </returns>
    /// <remarks>
    /// <para>MGetConstPropList returns identifiers for all the constant Physical 
    /// Properties that can be retrieved by the GetCompoundConstant method. If no 
    /// properties are supported, UNDEFINED should be returned. The CAPE-OPEN 
    /// standards do not define a minimum list of Physical Properties to be made 
    /// available by a software component that implements the ICapeThermoCompounds 
    /// interface.</para>
    /// <para>A component that implements the ICapeThermoCompounds interface may 
    /// return constant Physical Property identifiers which do not belong to the 
    /// list defined in section 7.5.2.</para>
    /// <para>However, these proprietary identifiers may not be understood by most 
    /// of the clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetConstPropList is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the Get-ConstPropList operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetConstPropList method. The error would not be raised when the 
    /// GetConstPropList method is implemented by a Material Object.</exception>
    [DispId(0x00000003)]
    [Description("Method GetConstPropList")]
    object GetConstPropList();

    /// <summary>Returns the number of Compounds supported.</summary>
    /// <returns>Number of Compounds supported.</returns>
    /// <remarks>The number of Compounds returned by this method must be equal to 
    /// the number of Compound identifiers that are returned by the GetCompoundList 
    /// method of this interface. It must be zero or a positive number.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    /// <exception cref ="ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetNumCompounds method. The error would not be raised when the 
    /// GetNumCompounds method is implemented by a Material Object.</exception>
    [DispId(0x00000004)]
    [Description("Method GetNumCompounds")]
    int GetNumCompounds();

    /// <summary>Returns the values of pressure-dependent Physical Properties for 
    /// the specified pure Compounds.</summary>
    /// <param name = "props">The list of Physical Property identifiers. Valid
    /// identifiers for pressure-dependent Physical Properties are listed in section 
    /// 7.5.4</param>
    /// <param name ="pressure">Pressure (in Pa) at which Physical Properties are
    /// evaluated</param>
    /// <param name ="compIds">List of Compound identifiers for which Physical
    /// Properties are to be retrieved. Set compIds = UNDEFINED to denote all 
    /// Compounds in the component that implements the ICapeThermoCompounds 
    /// interface.</param>
    /// <param name = "propVals">>Property values for the Compounds specified.</param>
    /// <remarks><para>The GetPDependentPropList method can be used in order to 
    /// check which Physical Properties are available.</para>
    /// <para>If the number of requested Physical Properties is P and the number 
    /// Compounds is C, the propvals array will contain C*P values. The first C 
    /// will be the values for the first requested Physical Property followed by C 
    /// values for the second Physical Property, and so on.</para>
    /// <para>Physical Properties are returned in a fixed set of units as specified 
    /// in section 7.5.4.</para>
    /// <para>If the compIds argument is set to UNDEFINED this is a request to return 
    /// property values for all compounds in the component that implements the 
    /// ICapeThermoCompounds interface with the compound order the same as that 
    /// returned by the GetCompoundList method. For example, if the interface is 
    /// implemented by a Property Package component the property request with compIds 
    /// set to UNDEFINED means all compounds in the Property Package rather than all 
    /// compounds in the Material Object passed to the Property package.</para>
    /// <para>If any Physical Property is not available for one or more Compounds, 
    /// then undefined valuesm must be returned for those combinations and an 
    /// ECapeThrmPropertyNotAvailable exception must be raised. If the exception is 
    /// raised, the client should check all the values returned to determine which is 
    /// undefined.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation. This exception should be raised if no Compounds 
    /// or no Physical Properties are supported.</exception>
    /// <exception cref ="ECapeLimitedImpl">One or more Physical Properties are not 
    /// supported by the component that implements this interface. This exception 
    /// should also be raised (rather than ECapeInvalidArgument) if any element of 
    /// the props argument is not recognised since the list of Physical Properties 
    /// in section 7.5.4 is not intended to be exhaustive and an unrecognised
    /// Physical Property identifier may be valid. If no Physical Properties at all 
    /// are supported, ECapeNoImpl should be raised (see above).</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example UNDEFINED for argument props.</exception>
    /// <exception cref = "ECapeOutOfBounds">The value of the pressure is outside of
    /// the range of values accepted by the Property Package.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">At least one item in the 
    /// properties list is not available for a particular compound.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetPDependentProperty method. The error would not be raised when the 
    /// GetPDependentProperty method is implemented by a Material Object.</exception>
    [DispId(0x00000005)]
    [Description("Method GetPDependentProperty")]
    void GetPDependentProperty(
        [In] object props,
        [In] double pressure,
        [In] object compIds,
        [In][Out]ref  object propVals);

    ///<summary>Returns the list of supported pressure-dependent properties.</summary>
    ///<returns>The list of Physical Property identifiers for all supported 
    /// pressure-dependent properties. The standard identifiers are listed in 
    /// section 7.5.4</returns>
    /// <remarks>
    /// <para>GetPDependentPropList returns identifiers for all the pressure-dependent 
    /// properties that can be retrieved by the GetPDependentProperty method. If no 
    /// properties are supported UNDEFINED should be returned. The CAPE-OPEN standards 
    /// do not define a minimum list of Physical Properties to be made available by 
    /// a software component that implements the ICapeThermoCompounds interface.</para>
    /// <para>A component that implements the ICapeThermoCompounds interface may 
    /// return identifiers which do not belong to the list defined in section 7.5.4. 
    /// However, these proprietary identifiers may not be understood by most of the 
    /// clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref ="ECapeBadInvOrder">The error to be raised if the Property 
    /// Package required the SetMaterial method to be called before calling the 
    /// GetPDependentPropList method. The error would not be raised when the 
    /// GetPDependentPropList method is implemented by a Material Object.</exception>
    [DispId(0x00000006)]
    [Description("Method GetPDependentPropList")]
    object GetPDependentPropList();

    /// <summary>Returns the values of temperature-dependent Physical Properties for 
    /// the specified pure Compounds.</summary>
    /// <param name ="props">The list of Physical Property identifiers. Valid
    /// identifiers for temperature-dependent Physical Properties are listed in 
    /// section 7.5.3</param>
    /// <param name = "temperature">Temperature (in K) at which properties are 
    /// evaluated.</param>
    /// <param name ="compIds">List of Compound identifiers for which Physical
    /// Properties are to be retrieved. Set compIds = UNDEFINED to denote all 
    /// Compounds in the component that implements the ICapeThermoCompounds 
    /// interface .</param>
    /// <param name = "propVals">Physical Property values for the Compounds specified.
    /// </param>
    /// <remarks> <para>The GetTDependentPropList method can be used in order to 
    /// check which Physical Properties are available.</para>
    /// <para>If the number of requested Physical Properties is P and the number of 
    /// Compounds is C, the propvals array will contain C*P values. The first C will 
    /// be the values for the first requested Physical Property followed by C values 
    /// for the second Physical Property, and so on.</para>
    /// <para>Properties are returned in a fixed set of units as specified in 
    /// section 7.5.3.</para>
    /// <para>If the compIds argument is set to UNDEFINED this is a request to return 
    /// property values for all compounds in the component that implements the 
    /// ICapeThermoCompounds interface with the compound order the same as that 
    /// returned by the GetCompoundList method. For example, if the interface is 
    /// implemented by a Property Package component the property request with compIds 
    /// set to UNDEFINED means all compounds in the Property Package rather than all 
    /// compounds in the Material Object passed to the Property package.</para>
    /// <para>If any Physical Property is not available for one or more Compounds, 
    /// then undefined values must be returned for those combinations and an 
    /// ECapeThrmPropertyNotAvailable exception must be raised. If the exception is 
    /// raised, the client should check all the values returned to determine which is 
    /// undefined.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl"> – The operation is “not” implemented even 
    /// if this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported 
    /// by the current implementation. This exception should be raised if no 
    /// Compounds or no Physical Properties are supported.</exception>
    /// <exception cref = "ECapeLimitedImpl">One or more Physical Properties are not
    /// supported by the component that implements this interface. This exception 
    /// should also be raised (rather than ECapeInvalidArgument) if any element of 
    /// the props argument is not recognised since the list of properties in section 
    /// 7.5.3 is not intended to be exhaustive and an unrecognised Physical Property 
    /// identifier may be valid. If no properties at all are supported ECapeNoImpl 
    /// should be raised (see above).</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example UNDEFINED for argument props.</exception> 
    /// <exception cref = "ECapeOutOfBounds">The value of the temperature is outside
    /// of the range of values accepted by the Property Package.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">at least one item in the 
    /// properties list is not available for a particular compound.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder"> The error to be raised if the 
    /// Property Package required the SetMaterial method to be called before calling 
    /// the GetTDependentProperty method. The error would not be raised when the 
    /// GetTDependentProperty method is implemented by a Material Object.</exception>
    [DispId(0x00000007)]
    [Description("Method GetTDependentProperty")]
    void GetTDependentProperty(
        [In] object props,
        [In] double temperature,
        [In] object compIds,
        [In][Out]ref  object propVals);

    /// <summary>Returns the list of supported temperature-dependent Physical 
    /// Properties.</summary>
    /// <returns>The list of Physical Property identifiers for all supported 
    /// temperature-dependent properties. The standard identifiers are listed in 
    /// section 7.5.3</returns>
    /// <remarks><para>GetTDependentPropList returns identifiers for all the 
    /// temperature-dependent Physical Properties that can be retrieved by the 
    /// GetTDependentProperty method. If no properties are supported UNDEFINED 
    /// should be returned. The CAPE-OPEN standards do not define a minimum list of 
    /// properties to be made available by a software component that implements the 
    /// ICapeThermoCompounds interface.</para>
    /// <para>A component that implements the ICapeThermoCompounds interface may 
    /// return identifiers which do not belong to the list defined in section 
    /// 7.5.3. However, these proprietary identifiers may not be understood by most 
    /// of the clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported
    /// by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s),
    /// specified for the operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">The error to be raised if the Property 
    /// Package required the SetMaterial method to be called before calling the 
    /// GetTDependentPropList method. The error would not be raised when the 
    /// GetTDependentPropList method is implemented by a Material Object.</exception>
    [DispId(0x00000008)]
    [Description("Method GetTDependentPropList")]
    object GetTDependentPropList();
}

/// <summary>Provides information about the number and types of Phases supported by 
/// the component that implements it.</summary>
/// <remarks>This interface is designed to provide information about the number and 
/// types of Phases supported by the component that implements it. It defines all the
/// Phases that a component such as a Physical Property Calculator can handle. It 
/// does not provide information about the Phases that are actually present in a 
/// Material Object. This function is provided by the Get-PresentPhases method of the 
/// ICapeThermoMaterial interface.</remarks>
[ComVisible(false)]
[Description("ICapeThermoPhases Interface")]
public interface ICapeThermoPhases
{
    /// <summary>Returns the number of Phases.</summary>
    /// <returns>The number of Phases supported.</returns>
    /// <remarks>The number of Phases returned by this method must be equal to the 
    /// number of Phase labels that are returned by the GetPhaseList method of this
    /// interface. It must be zero, or a positive number.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported
    /// by the current implementation.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method GetNumPhases")]
    int GetNumPhases();

    /// <summary>Returns information on an attribute associated with a Phase for the 
    /// purpose of understanding what lies behind a Phase label.</summary>
    /// <param name ="phaseLabel">A (single) Phase label. This must be one of the 
    /// values returned by GetPhaseList method.</param>
    /// <param name ="phaseAttribute">One of the Phase attribute identifiers from the 
    /// table below.</param>
    /// <returns>The value corresponding to the Phase attribute identifier – see 
    /// table below.</returns>
    /// <remarks>
    /// <para>GetPhaseInfo is intended to allow a PME, or other client, to identify a
    /// Phase with an arbitrary label. A PME, or other client, will need to do this 
    /// to map stream data into a Material Object, or when importing a Property 
    /// Package. If the client cannot identify the Phase, it can ask the user to 
    /// provide a mapping based on the values of these properties.</para>
    /// <para>The list of supported Phase attributes is defined in the following 
    /// table:</para>
    /// <para>For example, the following information might be returned by a Property 
    /// Package component that supports a vapour Phase, an organic liquid Phase and 
    /// an aqueous liquid Phase:
    /// Phase label Gas Organic Aqueous
    /// StateOfAggregation Vapor Liquid Liquid
    /// KeyCompoundId UNDEFINED UNDEFINED Water
    /// ExcludedCompoundId UNDEFINED Water UNDEFINED
    /// DensityDescription UNDEFINED Light Heavy
    /// UserDescription The gas Phase The organic liquid
    /// Phase
    /// The aqueous liquid
    /// Phase 
    /// TypeOfSolid UNDEFINED UNDEFINED UNDEFINED</para>
    /// </remarks>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists but it is not supported 
    /// by the current implementation..</exception>
    /// <exception cref ="ECapeInvalidArgument"> – phaseLabel is not recognised, or 
    /// UNDEFINED, or phaseAttribute is not recognised.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable..</exception>
    [DispId(0x00000002)]
    [Description("Method GetPhaseInfo")]
    string[] GetPhaseInfo(string phaseLabel, string phaseAttribute);

    /// <summary>
    /// Returns Phase labels and other important descriptive information for all the 
    /// Phases supported.
    /// </summary>
    /// <param name = "phaseLabels">The list of Phase labels for the Phases supported. 
    /// A Phase label can be any string but each Phase must have a unique label. If, 
    /// for some reason, no Phases are supported an UNDEFINED value should be returned 
    /// for the phaseLabels. The number of Phase labels must also be equal to the 
    /// number of Phases returned by the GetNumPhases method.
    /// </param>
    /// <param name = "stateOfAggregation">The physical State of Aggregation associated 
    /// with each of the Phases. This must be one of the following strings: ”Vapor”, 
    /// “Liquid”, “Solid” or “Unknown”. Each Phase must have a single State of 
    /// Aggregation. The value must not be left undefined, but may be set to “Unknown”.
    /// </param>
    /// <param name = "keyCompoundId">The key Compound for the Phase. This must be the
    /// Compound identifier (as returned by GetCompoundList), or it may be undefined 
    /// in which case a UNDEFINED value is returned. The key Compound is an indication 
    /// of the Compound that is expected to be present in high concentration in the 
    /// Phase, e.g. water for an aqueous liquid phase. Each Phase can have a single 
    /// key Compound.
    /// </param>
    /// <remarks>
    /// <para>The Phase label allows the phase to be uniquely identified in methods of
    /// the ICapeThermoPhases interface and other CAPE-OPEN interfaces. The State of 
    /// Aggregation and key Compound provide a way for the PME, or other client, to 
    /// interpret the meaning of a Phase label in terms of the physical characteristics 
    /// of the Phase.</para>
    /// <para>All arrays returned by this method must be of the same length, i.e. 
    /// equal to the number of Phase labels.</para>
    /// <para>To get further information about a Phase, use the GetPhaseInfo method.
    /// </para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000003)]
    [Description("Method GetPhaseList")]
    void GetPhaseList(ref string[] phaseLabels, ref string[] stateOfAggregation, ref string[] keyCompoundId);
}

/// <summary>Provides information about the number and types of Phases supported by 
/// the component that implements it.</summary>
/// <remarks>This interface is designed to provide information about the number and 
/// types of Phases supported by the component that implements it. It defines all the
/// Phases that a component such as a Physical Property Calculator can handle. It 
/// does not provide information about the Phases that are actually present in a 
/// Material Object. This function is provided by the Get-PresentPhases method of the 
/// ICapeThermoMaterial interface.</remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.InCapeThePhasesComIid)]  // "678C0A9E-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoPhases Interface")]
internal interface ICapeThermoPhasesCOM
{
    /// <summary>Returns the number of Phases.</summary>
    /// <returns>The number of Phases supported.</returns>
    /// <remarks>The number of Phases returned by this method must be equal to the 
    /// number of Phase labels that are returned by the GetPhaseList method of this
    /// interface. It must be zero, or a positive number.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported
    /// by the current implementation.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method GetNumPhases")]
    int GetNumPhases();

    /// <summary>Returns information on an attribute associated with a Phase for the 
    /// purpose of understanding what lies behind a Phase label.</summary>
    /// <param name ="phaseLabel">A (single) Phase label. This must be one of the 
    /// values returned by GetPhaseList method.</param>
    /// <param name ="phaseAttribute">One of the Phase attribute identifiers from the 
    /// table below.</param>
    /// <returns>The value corresponding to the Phase attribute identifier – see 
    /// table below.</returns>
    /// <remarks>
    /// <para>GetPhaseInfo is intended to allow a PME, or other client, to identify a
    /// Phase with an arbitrary label. A PME, or other client, will need to do this 
    /// to map stream data into a Material Object, or when importing a Property 
    /// Package. If the client cannot identify the Phase, it can ask the user to 
    /// provide a mapping based on the values of these properties.</para>
    /// <para>The list of supported Phase attributes is defined in the following 
    /// table:</para>
    /// <para>For example, the following information might be returned by a Property 
    /// Package component that supports a vapour Phase, an organic liquid Phase and 
    /// an aqueous liquid Phase:
    /// Phase label Gas Organic Aqueous
    /// StateOfAggregation Vapor Liquid Liquid
    /// KeyCompoundId UNDEFINED UNDEFINED Water
    /// ExcludedCompoundId UNDEFINED Water UNDEFINED
    /// DensityDescription UNDEFINED Light Heavy
    /// UserDescription The gas Phase The organic liquid
    /// Phase
    /// The aqueous liquid
    /// Phase 
    /// TypeOfSolid UNDEFINED UNDEFINED UNDEFINED</para>
    /// </remarks>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists but it is not supported 
    /// by the current implementation..</exception>
    /// <exception cref ="ECapeInvalidArgument"> – phaseLabel is not recognised, or 
    /// UNDEFINED, or phaseAttribute is not recognised.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable..</exception>
    [DispId(0x00000002)]
    [Description("Method GetPhaseInfo")]
    object GetPhaseInfo(
        [In] string phaseLabel,
        [In] string phaseAttribute);

    /// <summary>
    /// Returns Phase labels and other important descriptive information for all the 
    /// Phases supported.
    /// </summary>
    /// <param name = "phaseLabels">The list of Phase labels for the Phases supported. 
    /// A Phase label can be any string but each Phase must have a unique label. If, 
    /// for some reason, no Phases are supported an UNDEFINED value should be returned 
    /// for the phaseLabels. The number of Phase labels must also be equal to the 
    /// number of Phases returned by the GetNumPhases method.
    /// </param>
    /// <param name = "stateOfAggregation">The physical State of Aggregation associated 
    /// with each of the Phases. This must be one of the following strings: ”Vapor”, 
    /// “Liquid”, “Solid” or “Unknown”. Each Phase must have a single State of 
    /// Aggregation. The value must not be left undefined, but may be set to “Unknown”.
    /// </param>
    /// <param name = "keyCompoundId">The key Compound for the Phase. This must be the
    /// Compound identifier (as returned by GetCompoundList), or it may be undefined 
    /// in which case a UNDEFINED value is returned. The key Compound is an indication 
    /// of the Compound that is expected to be present in high concentration in the 
    /// Phase, e.g. water for an aqueous liquid phase. Each Phase can have a single 
    /// key Compound.
    /// </param>
    /// <remarks>
    /// <para>The Phase label allows the phase to be uniquely identified in methods of
    /// the ICapeThermoPhases interface and other CAPE-OPEN interfaces. The State of 
    /// Aggregation and key Compound provide a way for the PME, or other client, to 
    /// interpret the meaning of a Phase label in terms of the physical characteristics 
    /// of the Phase.</para>
    /// <para>All arrays returned by this method must be of the same length, i.e. 
    /// equal to the number of Phase labels.</para>
    /// <para>To get further information about a Phase, use the GetPhaseInfo method.
    /// </para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000003)]
    [Description("Method GetPhaseList")]
    void GetPhaseList(
        [In][Out] ref object phaseLabels,
        [In][Out] ref object stateOfAggregation,
        [In][Out] ref object keyCompoundId);
}

/// <remarks>
/// <para>Any Component or object that can calculate a Physical Property must 
/// implement the ICapeThermoPropertyRoutine interface. Within the scope of this 
/// specification this means that it must be implemented by Calculation Routine 
/// components, Property Package components and Material Object implementations that 
/// will be passed to clients which may need to perform Property Calculations, such 
/// as Unit Operations [2] and Reaction Package components [3].</para>
/// <para>When the ICapeThermoPropertyRoutine interface is implemented by a Material 
/// Object, it is expected that the actual Calculate, Check and Get functions will be 
/// delegated either to proprietary methods within a PME or to methods in an 
/// associated CAPE-OPEN Property Package or Calculation Routine component.</para>
///</remarks>
[ComVisible(false)]
[Description("ICapeThermoPropertyRoutine Interface")]
public interface ICapeThermoPropertyRoutine
{
    /// <summary>This method is used to calculate the natural logarithm of the 
    /// fugacity coefficients (and optionally their derivatives) in a single Phase 
    /// mixture. The values of temperature, pressure and composition are specified in 
    /// the argument list and the results are also returned through the argument list.
    /// </summary>
    /// <param name ="phaseLabel">Phase label of the Phase for which the properties 
    /// are to be calculated. The Phase label must be one of the strings returned by 
    /// the GetPhaseList method on the ICapeThermoPhases interface.</param>
    /// <param name = "temperature">The temperature (K) for the calculation.</param>
    /// <param name = "pressure">The pressure (Pa) for the calculation.</param>
    /// <param name ="lnPhiDt">Derivatives of natural logarithm of the fugacity
    /// coefficients w.r.t. temperature (if requested).</param>
    /// <param name ="moleNumbers">Number of moles of each Compound in the mixture.</param>
    /// <param name = "fFlags">Code indicating whether natural logarithm of the 
    /// fugacity coefficients and/or derivatives should be calculated (see notes).
    /// </param>
    /// <param name = "lnPhi">Natural logarithm of the fugacity coefficients (if
    /// requested).</param>
    /// <param anem = "lnPhiDT">Derivatives of natural logarithm of the fugacity
    /// coefficients w.r.t. temperature (if requested).</param>
    /// <param name ="lnPhiDp">Derivatives of natural logarithm of the fugacity
    /// coefficients w.r.t. pressure (if requested).</param>
    /// <param name ="lnPhiDn">Derivatives of natural logarithm of the fugacity
    /// coefficients w.r.t. mole numbers (if requested).</param>
    /// <remarks>
    /// <para>This method is provided to allow the natural logarithm of the fugacity 
    /// coefficient, which is the most commonly used thermodynamic property, to be 
    /// calculated and returned in a highly efficient manner.</para>
    /// <para>The temperature, pressure and composition (mole numbers) for the 
    /// calculation are specified by the arguments and are not obtained from the 
    /// Material Object by a separate request. Likewise, any quantities calculated are 
    /// returned through the arguments and are not stored in the Material Object. The 
    /// state of the Material Object is not affected by calling this method. It should 
    /// be noted however, that prior to calling CalcAndGetLnPhi a valid Material 
    /// Object must have been defined by calling the SetMaterial method on the
    /// ICapeThermoMaterialContext interface of the component that implements the
    /// ICapeThermoPropertyRoutine interface. The compounds in the Material Object 
    /// must have been identified and the number of values supplied in the moleNumbers
    /// argument must be equal to the number of Compounds in the Material Object.
    /// </para>
    /// <para>The fugacity coefficient information is returned as the natural 
    /// logarithm of the fugacity coefficient. This is because thermodynamic models 
    /// naturally provide the natural logarithm of this quantity and also a wider 
    /// range of values may be safely returned.</para>
    /// <para>The quantities actually calculated and returned by this method are 
    /// controlled by an integer code fFlags. The code is formed by summing 
    /// contributions for the property and each derivative required using the 
    /// enumerated constants eCapeCalculationCode (defined in the
    /// Thermo version 1.1 IDL) shown in the following table. For example, to 
    /// calculate log fugacity coefficients and their T-derivatives the fFlags 
    /// argument would be set to CAPE_LOG_FUGACITY_COEFFICIENTS + CAPE_T_DERIVATIVE.</para>
    /// <table border="1">
    /// <tr>
    /// <th>Calculation Type</th>
    /// <th>Enumeration Value</th>
    /// <th>Numerical Value</th>
    /// </tr>
    /// <tr>
    /// <td>no calculation</td>
    /// <td>CAPE_NO_CALCULATION</td>
    /// <td>0</td>
    /// </tr>
    /// <tr>
    /// <td>log fugacity coefficients</td>
    /// <td>CAPE_LOG_FUGACITY_COEFFICIENTS</td>
    /// <td>1</td>
    /// </tr>
    /// <tr>
    /// <td>T-derivative</td>
    /// <td>CAPE_T_DERIVATIVE</td>
    /// <td>2</td>
    /// </tr>
    /// <tr>
    /// <td>P-derivative</td>
    /// <td>CAPE_P_DERIVATIVE</td>
    /// <td>4</td>
    /// </tr>
    /// <tr>
    /// <td>mole number derivatives</td>
    /// <td>CAPE_MOLE_NUMBERS_DERIVATIVES</td>
    /// <td>8</td>
    /// </tr>
    /// </table>	
    /// <para>If CalcAndGetLnPhi is called with fFlags set to CAPE_NO_CALCULATION no 
    /// property values are returned.</para>
    /// <para>A typical sequence of operations for this method when implemented by a 
    /// Property Package component would be:
    /// </para>
    /// <para>
    /// - Check that the phaseLabel specified is valid.
    /// </para>
    /// <para>
    /// - Check that the moleNumbers array contains the number of values expected
    /// (should be consistent with the last call to the SetMaterial method).
    /// </para>
    /// <para>
    /// - Calculate the requested properties/derivatives at the T/P/composition specified in the argument list.
    /// </para>
    /// <para>
    /// - Store values for the properties/derivatives in the corresponding arguments.
    /// </para>
    /// <para>Note that this calculation can be carried out irrespective of whether the Phase actually exists in the Material Object.
    /// </para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported by 
    /// the current implementation.</exception>
    /// <exception cref ="ECapeLimitedImpl">Would be raised if the one or more of the 
    /// properties requested cannot be returned because the calculation is not 
    /// implemented.</exception>
    /// <exception cref = "ECapeBadInvOrder">The necessary pre-requisite operation has 
    /// not been called prior to the operation request. For example, the 
    /// ICapeThermoMaterial interface has not been passed via a SetMaterial call prior
    /// to calling this method.</exception>
    /// <exception cref = "ECapeFailedInitialisation">The pre-requisites for the 
    ///	Property Calculation are not valid. Forexample, the composition of the phase is 
    /// not defined, the number of Compounds in the Material Object is zero or not 
    /// consistent with the moleNumbers argument or any other necessary input information 
    /// is not available.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">At least one item in the 
    /// requested properties cannot be returned. This could be because the property 
    /// cannot be calculated at the specified conditions or for the specified Phase. 
    /// If the property calculation is not implemented then ECapeLimitedImpl should 
    /// be returned.</exception>
    /// <exception cref = "ECapeSolvingError">One of the property calculations has 
    /// failed. For example if one of the iterative solution procedures in the model 
    /// has run out of iterations, or has converged to a wrong solution.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example an unrecognised value, or UNDEFINED for the 
    /// phaseLabel argument.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method CalcAndGetLnPhi")]
    void CalcAndGetLnPhi(string phaseLabel, double temperature,
        double pressure,
        double[] moleNumbers,
        CapeFugacityFlag fFlags,
        ref double[] lnPhi,
        ref double[] lnPhiDt,
        ref double[] lnPhiDp,
        ref double[] lnPhiDn);

    /// <summary>CalcSinglePhaseProp is used to calculate properties and property 
    /// derivatives of a mixture in a single Phase at the current values of 
    /// temperature, pressure and composition set in the Material Object. 
    /// CalcSinglePhaseProp does not perform phase Equilibrium Calculations.
    /// </summary>
    /// <param name = "props">The list of identifiers for the single-phase properties 
    /// or derivatives to be calculated. See sections 7.5.5 and 7.6 for the standard 
    /// identifiers.</param>
    /// <param name = "phaseLabel">Phase label of the Phase for which the properties 
    /// are to be calculated. The Phase label must be one of the strings returned by 
    /// the GetPhaseList method on the ICapeThermoPhases interface.</param>
    /// <remarks>
    /// <para>CalcSinglePhaseProp calculates properties, such as enthalpy or viscosity 
    /// that are defined for a single Phase. Physical Properties that depend on more 
    /// than one Phase, for example surface tension or K-values, are handled by 
    /// CalcTwoPhaseProp method.</para>
    /// <para>Components that implement this method must get the input specification 
    /// for the calculation (temperature, pressure and composition) from the associated 
    /// Material Object and set the results in the Material Object.</para>
    /// <para>Thermodynamic and Physical Properties Components, such as a Property 
    /// Package or Property Calculator, must implement the ICapeThermoMaterialContext 
    /// interface so that an ICapeThermoMaterial interface can be passed via the 
    /// SetMaterial method.</para>
    /// <para>A typical sequence of operations for CalcSinglePhaseProp when implemented
    /// by a Property Package component would be:</para>
    /// <para>- Check that the phaseLabel specified is valid.</para>
    /// <para>- Use the GetTPFraction method (of the Material Object specified in the 
    /// last call to the SetMaterial method) to get the temperature, pressure and 
    /// composition of the specified Phase.</para>
    /// <para>- Calculate the properties.</para>
    /// <para>- Store values for the properties of the Phase in the Material Object 
    /// using the SetSinglePhaseProp method of the ICapeThermoMaterial interface.</para>
    /// <para>CalcSinglePhaseProp will request the input Property values it requires 
    /// from the Material Object through GetSinglePhaseProp calls. If a requested 
    /// property is not available, the exception raised will be 
    /// ECapeThrmPropertyNotAvailable. If this error occurs then the Property Package 
    /// can return it to the client, or request a different property. Material Object
    /// implementations must be able to supply property values using the client’s 
    /// choice of basis by implementing conversion from one basis to another.</para>
    /// <para>Clients should not assume that Phase fractions and Compound fractions in 
    /// a Material Object are normalised. Fraction values may also lie outside the 
    /// range 0 to 1. If fractions are not normalised, or are outside the expected 
    /// range, it is the responsibility of the Property Package to decide how to deal 
    /// with the situation.</para>
    /// <para>It is recommended that properties are requested one at a time in order 
    /// to simplify error handling. However, it is recognised that there are cases 
    /// where the potential efficiency gains of requesting several properties 
    /// simultaneously are more important. One such example might be when a property 
    /// and its derivatives are required.</para>
    /// <para>If a client uses multiple properties in a call and one of them fails 
    /// then the whole call should be considered to have failed. This implies that no 
    /// value should be written back to the Material Object by the Property Package 
    /// until it is known that the whole request can be satisfied.</para>
    /// <para>It is likely that a PME might request values of properties for a Phase at 
    /// conditions of temperature, pressure and composition where the Phase does not 
    /// exist (according to the mathematical/physical models used to represent 
    /// properties). The exception ECapeThrmPropertyNotAvailable may be raised or an 
    /// extrapolated value may be returned.</para>
    /// <para>It is responsibility of the implementer to decide how to handle this 
    /// circumstance.</para>
    /// </remarks>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if this
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref ="ECapeLimitedImpl">Would be raised if the one or more of the 
    /// properties requested cannot be returned because the calculation (of the 
    /// particular property) is not implemented. This exception should also be raised 
    /// (rather than ECapeInvalidArgument) if the props argument is not recognised 
    /// because the list of properties in section 7.5.5 is not intended to be 
    /// exhaustive and an unrecognised property identifier may be valid. If no 
    /// properties at all are supported ECapeNoImpl should be raised (see above).</exception>
    /// <exception cref ="ECapeBadInvOrder">The necessary pre-requisite operation has 
    /// not been called prior to the operation request. For example, the 
    /// ICapeThermoMaterial interface has not been passed via a SetMaterial call prior 
    /// to calling this method.</exception> 
    /// <exception cref ="ECapeFailedInitialisation">The pre-requisites for the 
    /// property calculation are not valid. For example, the composition of the phases
    /// is not defined or any other necessary input information is not available.</exception>
    /// <exception cref ="ECapeThrmPropertyNotAvailable">At least one item in the 
    /// requested properties cannot be returned. This could be because the property 
    /// cannot be calculated at the specified conditions or for the specified phase. 
    /// If the property calculation is not implemented then ECapeLimitedImpl should be 
    /// returned.</exception>
    [DispId(0x00000002)]
    [Description("Method CalcSinglePhaseProp")]
    void CalcSinglePhaseProp(string[] props, string phaseLabel);

    /// <summary>CalcTwoPhaseProp is used to calculate mixture properties and property 
    /// derivatives that depend on two Phases at the current values of temperature, 
    /// pressure and composition set in the Material Object. It does not perform 
    /// Equilibrium Calculations.</summary>
    /// <param name ="props">The list of identifiers for properties to be calculated.
    /// This must be one or more of the supported two-phase properties and derivatives 
    /// (as given by the GetTwoPhasePropList method). The standard identifiers for 
    /// two-phase properties are given in section 7.5.6 and 7.6.</param>
    /// <param name = "phaseLabels">Phase labels of the phases for which the properties 
    /// are to be calculated. The phase labels must be two of the strings returned by 
    /// the GetPhaseList method on the ICapeThermoPhases interface.</param>
    /// <remarks>
    /// <para>CalcTwoPhaseProp calculates the values of properties such as surface 
    /// tension or K-values. Properties that pertain to a single Phase are handled by 
    /// the CalcSinglePhaseProp method of the ICapeThermoPropertyRoutine interface.
    /// Components that implement this method must get the input specification for the 
    /// calculation (temperature, pressure and composition) from the associated 
    /// Material Object and set the results in the Material Object.</para>
    /// <para>Components such as a Property Package or Property Calculator must 
    /// implement the ICapeThermoMaterialContext interface so that an 
    /// ICapeThermoMaterial interface can be passed via the SetMaterial method.</para>
    /// <para>A typical sequence of operations for CalcTwoPhaseProp when implemented by
    /// a Property Package component would be:</para>
    /// <para>- Check that the phaseLabels specified are valid.</para>
    /// <para>- Use the GetTPFraction method (of the Material Object specified in the 
    /// last call to the SetMaterial method) to get the temperature, pressure and 
    /// composition of the specified Phases.</para>
    /// <para>- Calculate the properties.</para>
    /// <para>- Store values for the properties in the Material Object using the 
    /// SetTwoPhaseProp method of the ICapeThermoMaterial interface.</para>
    /// <para>CalcTwoPhaseProp will request the values it requires from the Material Object 
    /// through GetTPFraction or GetSinglePhaseProp calls. If a requested property is 
    /// not available, the exception raised will be ECapeThrmPropertyNotAvailable. If 
    /// this error occurs, then the Property Package can return it to the client, or 
    /// request a different property. Material Object implementations must be able to 
    /// supply property values using the client choice of basis by implementing 
    /// conversion from one basis to another.</para>
    /// <para>Clients should not assume that Phase fractions and Compound fractions in 
    /// a Material Object are normalised. Fraction values may also lie outside the 
    /// range 0 to 1. If fractions are not normalised, or are outside the expected 
    /// range, it is the responsibility of the Property Package to decide how to deal 
    /// with the situation.</para>
    /// <para>It is recommended that properties are requested one at a time in order to 
    /// simplify error handling. However, it is recognised that there are cases where 
    /// the potential efficiency gains of requesting several properties simultaneously 
    /// are more important. One such example might be when a property and its 
    /// derivatives are required.</para>
    /// <para>If a client uses multiple properties in a call and one of them fails, then the 
    /// whole call should be considered to have failed. This implies that no value 
    /// should be written back to the Material Object by the Property Package until 
    /// it is known that the whole request can be satisfied.</para>
    /// <para>CalcTwoPhaseProp must be called separately for each combination of Phase
    /// groupings. For example, vapour-liquid K-values have to be calculated in a 
    /// separate call from liquid-liquid K-values.</para>
    /// <para>Two-phase properties may not be meaningful unless the temperatures and 
    /// pressures of all Phases are identical. It is the responsibility of the Property 
    /// Package to check such conditions and to raise an exception if appropriate.</para>
    /// <para>It is likely that a PME might request values of properties for Phases at 
    /// conditions of temperature, pressure and composition where one or both of the 
    /// Phases do not exist (according to the mathematical/physical models used to 
    /// represent properties). The exception ECapeThrmPropertyNotAvailable may be 
    /// raised or an extrapolated value may be returned. It is responsibility of the 
    /// implementer to decide how to handle this circumstance.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref = "ECapeLimitedImpl">Would be raised if the one or more of the 
    /// properties requested cannot be returned because the calculation (of the 
    /// particular property) is not implemented. This exception should also be raised 
    /// (rather than ECapeInvalidArgument) if the props argument is not recognised 
    /// because the list of properties in section 7.5.6 is not intended to be 
    /// exhaustive and an unrecognised property identifier may be valid. If no 
    /// properties at all are supported ECapeNoImpl should be raised (see above).</exception>
    /// <exception cref = "ECapeBadInvOrder">The necessary pre-requisite operation has 
    /// not been called prior to the operation request. For example, the 
    /// ICapeThermoMaterial interface has not been passed via a SetMaterial call 
    /// prior to calling this method.</exception>
    /// <exception cref = "ECapeFailedInitialisation">The pre-requisites for the 
    /// property calculation are not valid. For example, the composition of one of the 
    /// Phases is not defined, or any other necessary input information is not 
    /// available.</exception>
    /// <exception cref = "ECapeThrmPropertyNotAvailable">At least one item in the 
    /// requested properties cannot be returned. This could be because the property 
    /// cannot be calculated at the specified conditions or for the specified Phase. 
    /// If the property calculation is not implemented then ECapeLimitedImpl should be 
    /// returned.</exception>
    /// <exception cref = "ECapeSolvingError">One of the property calculations has 
    /// failed. For example if one of the iterative solution procedures in the model 
    /// has run out of iterations, or has converged to a wrong solution.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example an unrecognised value or UNDEFINED for the 
    /// phaseLabels argument or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000003)]
    [Description("Method CalcTwoPhaseProp")]
    void CalcTwoPhaseProp(string[] props, string[] phaseLabels);

    /// <summary>Checks whether it is possible to calculate a property with the 
    /// CalcSinglePhaseProp method for a given Phase.</summary>
    /// <param name = "property">The identifier of the property to check. To be valid 
    /// this must be one of the supported single-phase properties or derivatives (as 
    /// given by the GetSinglePhasePropList method).</param>
    /// <param name = "phaseLabel">The Phase label for the calculation check. This must
    /// be one of the labels returned by the GetPhaseList method on the 
    /// ICapeThermoPhases interface.</param>
    /// <returns> A boolean set to True if the combination of property and phaseLabel
    /// is supported or False if not supported.</returns>
    /// <remarks>
    /// <para>The result of the check should only depend on the capabilities and 
    /// configuration (Compounds and Phases present) of the component that implements 
    /// the ICapeThermoPropertyRoutine interface (eg. a Property Package). It should 
    /// not depend on whether a Material Object has been set nor on the state 
    /// (temperature, pressure, composition etc.), or configuration of a Material 
    /// Object that might be set.</para>
    /// <para>It is expected that the PME, or other client, will use this method to 
    /// check whether the properties it requires are supported by the Property Package
    /// when the package is imported. If any essential properties are not available, 
    /// the import process should be aborted.</para>
    /// <para>If either the property or the phaseLabel arguments are not recognised by 
    /// the component that implements the ICapeThermoPropertyRoutine interface this 
    /// method should return False.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation CheckSinglePhasePropSpec is 
    /// “not” implemented even if this method can be called for reasons of 
    /// compatibility with the CAPE-OPEN standards. That is to say that the operation 
    /// exists, but it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeBadInvOrder">The necessary pre-requisite operation has
    /// not been called prior to the operation request. The ICapeThermoMaterial 
    /// interface has not been passed via a SetMaterial call prior to calling this 
    /// method.</exception>
    /// <exception cref = "ECapeFailedInitialisation">The pre-requisites for the 
    /// property calculation are not valid. For example, if a prior call to the 
    /// SetMaterial method of the ICapeThermoMaterialContext interface has failed to 
    /// provide a valid Material Object.</exception>
    /// <exception cref = "ECapeInvalidArgument">One or more of the input arguments is 
    /// not valid: for example, UNDEFINED value for the property argument or the 
    /// phaseLabel argument.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the CheckSinglePhasePropSpec operation, are not suitable.</exception>
    [DispId(0x00000004)]
    [Description("Method CheckSinglePhasePropSpec")]
    bool CheckSinglePhasePropSpec(string property, string phaseLabel);

    /// <summary>Checks whether it is possible to calculate a property with the 
    /// CalcTwoPhaseProp method for a given set of Phases.</summary>
    /// <param name = "property">The identifier of the property to check. To be valid 
    /// this must be one of the supported two-phase properties (including derivatives), 
    /// as given by the GetTwoPhasePropList method.</param>
    /// <param name ="phaseLabels">Phase labels of the Phases for which the properties 
    /// are to be calculated. The Phase labels must be two of the identifiers returned 
    /// by the GetPhaseList method on the ICapeThermoPhases interface.</param>
    /// <returns> A boolean Set to True if the combination of property and
    /// phaseLabels is supported, or False if not supported.</returns>
    /// <remarks>
    /// <para>The result of the check should only depend on the capabilities and 
    /// configuration (Compounds and Phases present) of the component that implements 
    /// the ICapeThermoPropertyRoutine interface (eg. a Property Package). It should 
    /// not depend on whether a Material Object has been set nor on the state 
    /// (temperature, pressure, composition etc.), or configuration of a Material 
    /// Object that might be set.</para>
    /// <para>It is expected that the PME, or other client, will use this method to 
    /// check whether the properties it requires are supported by the Property Package 
    /// when the Property Package is imported. If any essential properties are not 
    /// available, the import process should be aborted.</para>
    /// <para>If either the property argument or the values in the phaseLabels 
    /// arguments are not recognised by the component that implements the 
    /// ICapeThermoPropertyRoutine interface this method should return False.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation CheckTwoPhasePropSpec is “not” 
    /// implemented even if this method can be called for reasons of compatibility with 
    /// the CAPE-OPEN standards. That is to say that the operation exists, but it is 
    /// not supported by the current implementation. This may be the case if no 
    /// two-phase property is supported.</exception>
    /// <exception cref = "ECapeBadInvOrder">The necessary pre-requisite operation has 
    /// not been called prior to the operation request. The ICapeThermoMaterial 
    /// interface has not been passed via a SetMaterial call prior to calling this 
    /// method.</exception>
    /// <exception cref = "ECapeFailedInitialisation">The pre-requisites for the 
    /// property calculation are not valid. For example, if a prior call to the 
    /// SetMaterial method of the ICapeThermoMaterialContext interface has failed to 
    /// provide a valid Material Object.</exception>
    /// <exception cref = "ECapeInvalidArgument">One or more of the input arguments is 
    /// not valid. For example, UNDEFINED value for the property argument or the 
    /// phaseLabels argument or number of elements in phaseLabels array not equal to 
    /// two.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the CheckTwoPhasePropSpec operation, are not suitable.</exception>
    [DispId(0x00000005)]
    [Description("Method CheckTwoPhasePropSpec")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool CheckTwoPhasePropSpec(string property, string[] phaseLabels);

    /// <summary>Returns the list of supported non-constant single-phase Physical 
    /// Properties.</summary>
    /// <returns>List of all supported non-constant single-phase property identifiers. 
    /// The standard single-phase property identifiers are listed in section 7.5.5.
    /// </returns>
    /// <remarks>
    /// <para>A non-constant property depends on the state of the Material Object. </para>
    /// <para>Single-phase properties, e.g. enthalpy, only depend on the state of one 
    /// phase. GetSinglePhasePropList must return all the single-phase properties that 
    /// can be calculated by CalcSinglePhaseProp. If derivatives can be calculated 
    /// these must also be returned.</para>
    /// <para>If no single-phase properties are supported this method should return 
    /// UNDEFINED.</para>
    /// <para>To get the list of supported two-phase properties, use 
    /// GetTwoPhasePropList.</para>
    /// <para>A component that implements this method may return non-constant 
    /// single-phase property identifiers which do not belong to the list defined in 
    /// section 7.5.5. However, these proprietary identifiers may not be understood by 
    /// most of the clients of this component.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if 
    /// this method can be called for reasons of compatibility with the CAPE-OPEN 
    /// standards. That is to say that the operation exists, but it is not supported by 
    /// the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetSinglePhasePropList operation, are not suitable.</exception>
    [DispId(0x00000006)]
    [Description("Method GetSinglePhasePropList")]
    string[] GetSinglePhasePropList();

    /// <summary>Returns the list of supported non-constant two-phase properties.</summary>
    /// <returns>List of all supported non-constant two-phase property identifiers. 
    /// The standard two-phase property identifiers are listed in section 7.5.6.</returns>
    /// <remarks>
    /// <para>A non-constant property depends on the state of the Material Object. 
    /// Two-phase properties are those that depend on more than one co-existing phase, 
    /// e.g. K-values.</para>
    /// <para>GetTwoPhasePropList must return all the properties that can be calculated 
    /// by CalcTwoPhaseProp. If derivatives can be calculated, these must also be 
    /// returned.</para>
    /// <para>If no two-phase properties are supported this method should return 
    /// UNDEFINED.</para>
    /// <para>To check whether a property can be evaluated for a particular set of 
    /// phase labels use the CheckTwoPhasePropSpec method.</para>
    /// <para>A component that implements this method may return non-constant 
    /// two-phase property identifiers which do not belong to the list defined in 
    /// section 7.5.6. However, these proprietary identifiers may not be understood by 
    /// most of the clients of this component.</para>
    /// <para>To get the list of supported single-phase properties, use 
    /// GetSinglePhasePropList.</para>
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation is “not” implemented even if this
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetTwoPhasePropList operation, are not suitable.</exception>
    [DispId(0x00000007)]
    [Description("Method GetTwoPhasePropList")]
    string[] GetTwoPhasePropList();
}
