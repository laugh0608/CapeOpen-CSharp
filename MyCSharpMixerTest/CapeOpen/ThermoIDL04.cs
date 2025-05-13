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
// ThermoIDL 文件拆分，序号 04

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

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
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.InCapeTheProPerRouComIid)]  // "678C0A9F-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoPropertyRoutine Interface")]
internal interface ICapeThermoPropertyRoutineCOM
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
    void CalcAndGetLnPhi(
        [In] string phaseLabel,
        [In] double temperature,
        [In] double pressure,
        [In] object moleNumbers,
        [In] int fFlags,
        [In][Out]ref  object lnPhi,
        [In][Out]ref  object lnPhiDt,
        [In][Out]ref  object lnPhiDp,
        [In][Out]ref  object lnPhiDn);

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
    void CalcSinglePhaseProp(
        [In] object props,
        [In] string phaseLabel);

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
    void CalcTwoPhaseProp(
        [In] object props,
        [In] object phaseLabels);

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
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool CheckSinglePhasePropSpec(
        [In] string property,
        [In] string phaseLabel);

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
    bool CheckTwoPhasePropSpec(
        [In] string property,
        [In] object phaseLabels);

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
    object GetSinglePhasePropList();

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
    object GetTwoPhasePropList();
}

/// <summary>
/// Implemented by any component or object that can perform an Equilibrium Calculation.
/// </summary>
/// <remarks>
/// <para>Any component or object that can perform an Equilibrium Calculation must 
/// implement the ICapeThermoEquilibriumRoutine interface. Within the scope of this 
/// specification, this means that it must be implemented by Equilibrium Calculator 
/// components, Property Package components and by Material Object implementations 
/// that will be passed to clients which may need to perform Equilibrium Calculations, 
/// such as Unit Operations [2].</para>
/// <para>When a Material Object implements the ICapeThermoEquilibriumRoutine 
/// interface, it is expected that the methods will be delegated either to proprietary 
/// methods within a PME, or to methods in an associated CAPE-OPEN Property Package or 
/// Equilibrium Calculator component.</para>
/// </remarks>
[ComVisible(false)]
[Description("ICapeThermoEquilibriumRoutine Interface")]
public interface ICapeThermoEquilibriumRoutine
{
    /// <summary> CalcEquilibrium is used to calculate the amounts and compositions 
    /// of Phases at equilibrium. CalcEquilibrium will calculate temperature and/or 
    /// pressure if these are not among the two specifications that are mandatory for 
    /// each Equilibrium Calculation considered.</summary>
    /// <remarks>
    /// <para>The pSpecification and mSmSpecification arguments provide the information 
    /// necessary to retrieve the values of two specifications, for example the 
    /// pressure and temperature, for the Equilibrium Calculation. The CheckEquilibriumSpec 
    /// method can be used to check for supported specifications. Each specification 
    /// variable contains a sequence of strings in the order defined in the following 
    /// table (hence, the specification arguments may have 3 or 4 items):<para>
    /// <para>property identifier The property identifier can be any of the identifiers 
    /// listed in section 7.5.5 but only certain property specifications will normally 
    /// be supported by any Equilibrium Routine.</para>
    /// basis The basis for the property value. Valid settings for basis are given in 
    /// section 7.4. Use UNDEFINED as a placeholder for a property for which basis does
    /// not apply. For most Equilibrium Specifications, the result of the calculation
    /// is not dependent on the basis, but, for example, for phase fraction 
    /// specifications the basis (Mole or Mass) does make a difference.</para>
    /// <para>phase label The phase label denotes the Phase to which the specification 
    /// applies. It must either be one of the labels returned by GetPresentPhases, or 
    /// the special value “Overall”.</para>
    /// compound identifier (optional)The compound identifier allows for specifications 
    /// that depend on a particular Compound. This item of the specification array is 
    /// optional and may be omitted. In case of a specification without compound 
    /// identifier, the array element may be present and empty, or may be absent.</para>
    /// <para>Some examples of typical phase equilibrium specifications are given in 
    /// the table below.</para>
    /// <para>The values corresponding to the specifications in the argument list and 
    /// the overall composition of the mixture must be set in the associated Material 
    /// Object before a call to CalcEquilibrium.</para>
    /// <para>Components such as a Property Package or an Equilibrium Calculator must 
    /// implement the ICapeThermoMaterialContext interface, so that an 
    /// ICapeThermoMaterial interface can be passed via the SetMaterial method. It is 
    /// the responsibility of the implementation of CalcEquilibrium to validate the 
    /// Material Object before attempting a calculation.</para>
    /// <para>The Phases that will be considered in the Equilibrium Calculation are 
    /// those that exist in the Material Object, i.e. the list of phases specified in 
    /// a SetPresentPhases call. This provides a way for a client to specify whether, 
    /// for example, a vapour-liquid, liquid-liquid, or vapourliquid-liquid calculation 
    /// is required. CalcEquilibrium must use the GetPresentPhases method to retrieve 
    /// the list of Phases and the associated Phase status flags. The Phase status 
    /// flags may be used by the client to provide information about the Phases, for 
    /// example whether estimates of the equilibrium state are provided. See the 
    /// description of the GetPresentPhases and SetPresentPhases methods of the 
    /// ICapeThermoMaterial interface for details. When the Equilibrium Calculation 
    /// has been completed successfully, the SetPresentPhases method must be used to 
    /// specify which Phases are present at equilibrium and the Phase status flags for 
    /// the phases should be set to Cape_AtEquilibrium. This must include any Phases 
    /// that are present in zero amount such as the liquid Phase in a dew point 
    /// calculation.</para>
    /// <para>Some types of Phase equilibrium specifications may result in more than 
    /// one solution. A common example of this is the case of a dew point calculation. 
    /// However, CalcEquilibrium can provide only one solution through the Material 
    /// Object. The solutionType argument allows the “Normal” or “Retrograde” solution 
    /// to be explicitly requested. When none of the specifications includes a phase 
    /// fraction, the solutionType argument should be set to “Unspecified”.</para>
    /// <para>The definition of “Normal” is</para>
    /// <para>where V F is the vapour phase fraction and the derivatives are at 
    /// equilibrium states. For “Retrograde” behaviour,</para>
    /// <para>CalcEquilibrium must set the amounts, compositions, temperature and 
    /// pressure for all Phases present at equilibrium, as well as the temperature and 
    /// pressure for the overall mixture if not set as part of the calculation 
    /// specifications. CalcEquilibrium must not set any other Physical Properties.</para>
    /// <para>As an example, the following sequence of operations might be performed 
    /// by CalcEquilibrium in the case of an Equilibrium Calculation at fixed pressure 
    /// and temperature:</para>
    /// <para>- With the ICapeThermoMaterial interface of the supplied Material Object:
    /// </para>
    /// <para>- Use the GetPresentPhases method to find the list of Phases that the 
    /// Equilibrium Calculation should consider.</para>
    /// <para>- With the ICapeThermoCompounds interface of the Material Object use the
    /// GetCompoundIds method to find which Compounds are present.</para>
    /// <para>- Use the GetOverallProp method to get the temperature, pressure and 
    /// composition for the overall mixture.</para>
    /// <para>- Perform the Equilibrium Calculation.</para>
    /// <para>- Use SetPresentPhases to specify the Phases present at equilibrium and 
    /// set the Phase status flags to Cape_AtEquilibrium.</para>
    /// <para>- Use SetSinglePhaseProp to set pressure, temperature, Phase amount 
    /// (or Phase fraction) and composition for all Phases present.</para>
    /// </remarks>
    /// <param name = "pSpecification">First specification for the Equilibrium 
    /// Calculation. The specification information is used to retrieve the value of
    /// the specification from the Material Object. See below for details.</param>
    /// <param name = "mSpecification">Second specification for the Equilibrium 
    /// Calculation in the same format as pSpecificationpSpecification.</param>
    /// <param name = "solutionType"><para>The identifier for the required solution type. 
    /// The standard identifiers are given in the following list:</para>
    /// <para>Unspecified</para>
    /// <para>Normal</para>
    /// <para>Retrograde</para>
    /// <para>The meaning of these terms is defined below in the notes. Other 
    /// identifiers may be supported but their interpretation is not part of the CO 
    /// standard.</para></param>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref ="ECapeBadInvOrder">The necessary pre-requisite operation has 
    /// not been called prior to the operation request. The ICapeThermoMaterial interface 
    /// has not been passed via a SetMaterial call prior to calling this method.</exception>
    /// <exception cref ="ECapeSolvingError">The Equilibrium Calculation could not be 
    /// solved. For example if the solver has run out of iterations, or has converged 
    /// to a trivial solution.</exception>
    /// <exception cref ="ECapeLimitedImpl">Would be raised if the Equilibrium Routine 
    /// is not able to perform the flash it has been asked to perform. For example, 
    /// the values given to the input specifications are valid, but the routine is not 
    /// able to perform a flash given a temperature and a Compound fraction. That 
    /// would imply a bad usage or no usage of CheckEquilibriumSpec method, which is 
    /// there to prevent calling CalcEquilibrium for a calculation which cannot be
    /// performed.</exception>
    /// <exception cref ="ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed. It would be raised, for example, if a specification 
    /// identifier does not belong to the list of recognised identifiers. It would 
    /// also be raised if the value given to argument solutionType is not among 
    /// the three defined, or if UNDEFINED was used instead of a specification identifier.</exception>
    /// <exception cref ="ECapeFailedInitialisation"><para>The pre-requisites for the Equilibrium 
    /// Calculation are not valid. For example:</para>
    /// <para>• The overall composition of the mixture is not defined.</para>
    /// <para>• The Material Object (set by a previous call to the SetMaterial method of the
    /// ICapeThermoMaterialContext interface) is not valid. This could be because no 
    /// Phases are present or because the Phases present are not recognised by the
    /// component that implements the ICapeThermoEquilibriumRoutine interface.</para>
    /// <para>• Any other necessary input information is not available.</para></exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method CalcEquilibrium")]
    void CalcEquilibrium(string[] pSpecification, string[] mSpecification, string solutionType);
    
    /// <summary>Checks whether the Property Package can support a particular type of 
    /// Equilibrium Calculation.</summary>
    /// <remarks>
    /// <para>The meaning of the pSpecificationpSpecification, mSpecificationmSpecification and solutionType 
    /// arguments is the same as for the CalcEquilibrium method.</para>
    /// <para>The result of the check should only depend on the capabilities and 
    /// configuration (compounds and phases present) of the component that implements 
    /// the ICapeThermoEquilibriumRoutine interface (eg. a Property package). It should 
    /// not depend on whether a Material Object has been set nor on the state 
    /// (temperature, pressure, composition etc.) or configuration of a Material 
    /// Object that might be set.</para>
    /// <para>If solutionType, pSpecificationpSpecification and mSpecificationmSpecification arguments appear 
    /// valid but the actual specifications are not supported or not recognised a 
    /// False value should be returned.</para>
    /// </remarks>
    /// <param name = "pSpecification">First specification for the Equilibrium 
    /// Calculation.</param>
    /// <param name = "mSpecification">Second specification for the Equilibrium 
    /// Calculation.</param>
    /// <param name = "solutionType">The required solution type.</param>
    /// <returns>Set to True if the combination of specifications and solutionType is 
    /// supported or False if not supported.</returns>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example UNDEFINED for solutionType, pSpecification or 
    /// mSpecification argument.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000002)]
    [Description("Method CheckEquilibriumSpec")]
    bool CheckEquilibriumSpec(string[]  pSpecification, string[] mSpecification, string solutionType);
}

/// <summary>
/// Implemented by any component or object that can perform an Equilibrium Calculation.
/// </summary>
/// <remarks>
/// <para>Any component or object that can perform an Equilibrium Calculation must 
/// implement the ICapeThermoEquilibriumRoutine interface. Within the scope of this 
/// specification, this means that it must be implemented by Equilibrium Calculator 
/// components, Property Package components and by Material Object implementations 
/// that will be passed to clients which may need to perform Equilibrium Calculations, 
/// such as Unit Operations [2].</para>
/// <para>When a Material Object implements the ICapeThermoEquilibriumRoutine 
/// interface, it is expected that the methods will be delegated either to proprietary 
/// methods within a PME, or to methods in an associated CAPE-OPEN Property Package or 
/// Equilibrium Calculator component.</para>
/// </remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.InCapeTheEqBrRouComIid)]  // "678C0AA0-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoEquilibriumRoutine Interface")]
internal interface ICapeThermoEquilibriumRoutineCOM
{
    /// <summary> CalcEquilibrium is used to calculate the amounts and compositions 
    /// of Phases at equilibrium. CalcEquilibrium will calculate temperature and/or 
    /// pressure if these are not among the two specifications that are mandatory for 
    /// each Equilibrium Calculation considered.</summary>
    /// <remarks>
    /// <para>The specification1 and specification2 arguments provide the information 
    /// necessary to retrieve the values of two specifications, for example the 
    /// pressure and temperature, for the Equilibrium Calculation. The CheckEquilibriumSpec 
    /// method can be used to check for supported specifications. Each specification 
    /// variable contains a sequence of strings in the order defined in the following 
    /// table (hence, the specification arguments may have 3 or 4 items):<para>
    /// <para>property identifier The property identifier can be any of the identifiers 
    /// listed in section 7.5.5 but only certain property specifications will normally 
    /// be supported by any Equilibrium Routine.</para>
    /// basis The basis for the property value. Valid settings for basis are given in 
    /// section 7.4. Use UNDEFINED as a placeholder for a property for which basis does
    /// not apply. For most Equilibrium Specifications, the result of the calculation
    /// is not dependent on the basis, but, for example, for phase fraction 
    /// specifications the basis (Mole or Mass) does make a difference.</para>
    /// <para>phase label The phase label denotes the Phase to which the specification 
    /// applies. It must either be one of the labels returned by GetPresentPhases, or 
    /// the special value “Overall”.</para>
    /// compound identifier (optional)The compound identifier allows for specifications 
    /// that depend on a particular Compound. This item of the specification array is 
    /// optional and may be omitted. In case of a specification without compound 
    /// identifier, the array element may be present and empty, or may be absent.</para>
    /// <para>Some examples of typical phase equilibrium specifications are given in 
    /// the table below.</para>
    /// <para>The values corresponding to the specifications in the argument list and 
    /// the overall composition of the mixture must be set in the associated Material 
    /// Object before a call to CalcEquilibrium.</para>
    /// <para>Components such as a Property Package or an Equilibrium Calculator must 
    /// implement the ICapeThermoMaterialContext interface, so that an 
    /// ICapeThermoMaterial interface can be passed via the SetMaterial method. It is 
    /// the responsibility of the implementation of CalcEquilibrium to validate the 
    /// Material Object before attempting a calculation.</para>
    /// <para>The Phases that will be considered in the Equilibrium Calculation are 
    /// those that exist in the Material Object, i.e. the list of phases specified in 
    /// a SetPresentPhases call. This provides a way for a client to specify whether, 
    /// for example, a vapour-liquid, liquid-liquid, or vapourliquid-liquid calculation 
    /// is required. CalcEquilibrium must use the GetPresentPhases method to retrieve 
    /// the list of Phases and the associated Phase status flags. The Phase status 
    /// flags may be used by the client to provide information about the Phases, for 
    /// example whether estimates of the equilibrium state are provided. See the 
    /// description of the GetPresentPhases and SetPresentPhases methods of the 
    /// ICapeThermoMaterial interface for details. When the Equilibrium Calculation 
    /// has been completed successfully, the SetPresentPhases method must be used to 
    /// specify which Phases are present at equilibrium and the Phase status flags for 
    /// the phases should be set to Cape_AtEquilibrium. This must include any Phases 
    /// that are present in zero amount such as the liquid Phase in a dew point 
    /// calculation.</para>
    /// <para>Some types of Phase equilibrium specifications may result in more than 
    /// one solution. A common example of this is the case of a dew point calculation. 
    /// However, CalcEquilibrium can provide only one solution through the Material 
    /// Object. The solutionType argument allows the “Normal” or “Retrograde” solution 
    /// to be explicitly requested. When none of the specifications includes a phase 
    /// fraction, the solutionType argument should be set to “Unspecified”.</para>
    /// <para>The definition of “Normal” is</para>
    /// <para>where V F is the vapour phase fraction and the derivatives are at 
    /// equilibrium states. For “Retrograde” behaviour,</para>
    /// <para>CalcEquilibrium must set the amounts, compositions, temperature and 
    /// pressure for all Phases present at equilibrium, as well as the temperature and 
    /// pressure for the overall mixture if not set as part of the calculation 
    /// specifications. CalcEquilibrium must not set any other Physical Properties.</para>
    /// <para>As an example, the following sequence of operations might be performed 
    /// by CalcEquilibrium in the case of an Equilibrium Calculation at fixed pressure 
    /// and temperature:</para>
    /// <para>- With the ICapeThermoMaterial interface of the supplied Material Object:
    /// </para>
    /// <para>- Use the GetPresentPhases method to find the list of Phases that the 
    /// Equilibrium Calculation should consider.</para>
    /// <para>- With the ICapeThermoCompounds interface of the Material Object use the
    /// GetCompoundIds method to find which Compounds are present.</para>
    /// <para>- Use the GetOverallProp method to get the temperature, pressure and 
    /// composition for the overall mixture.</para>
    /// <para>- Perform the Equilibrium Calculation.</para>
    /// <para>- Use SetPresentPhases to specify the Phases present at equilibrium and 
    /// set the Phase status flags to Cape_AtEquilibrium.</para>
    /// <para>- Use SetSinglePhaseProp to set pressure, temperature, Phase amount 
    /// (or Phase fraction) and composition for all Phases present.</para>
    /// </remarks>
    /// <param name = "specification1">First specification for the Equilibrium 
    /// Calculation. The specification information is used to retrieve the value of
    /// the specification from the Material Object. See below for details.</param>
    /// <param name = "specification2">Second specification for the Equilibrium 
    /// Calculation in the same format as specification1.</param>
    /// <param name = "solutionType"><para>The identifier for the required solution type. 
    /// The standard identifiers are given in the following list:</para>
    /// <para>Unspecified</para>
    /// <para>Normal</para>
    /// <para>Retrograde</para>
    /// <para>The meaning of these terms is defined below in the notes. Other 
    /// identifiers may be supported but their interpretation is not part of the CO 
    /// standard.</para></param>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref ="ECapeBadInvOrder">The necessary pre-requisite operation has 
    /// not been called prior to the operation request. The ICapeThermoMaterial interface 
    /// has not been passed via a SetMaterial call prior to calling this method.</exception>
    /// <exception cref ="ECapeSolvingError">The Equilibrium Calculation could not be 
    /// solved. For example if the solver has run out of iterations, or has converged 
    /// to a trivial solution.</exception>
    /// <exception cref ="ECapeLimitedImpl">Would be raised if the Equilibrium Routine 
    /// is not able to perform the flash it has been asked to perform. For example, 
    /// the values given to the input specifications are valid, but the routine is not 
    /// able to perform a flash given a temperature and a Compound fraction. That 
    /// would imply a bad usage or no usage of CheckEquilibriumSpec method, which is 
    /// there to prevent calling CalcEquilibrium for a calculation which cannot be
    /// performed.</exception>
    /// <exception cref ="ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed. It would be raised, for example, if a specification 
    /// identifier does not belong to the list of recognised identifiers. It would 
    /// also be raised if the value given to argument solutionType is not among 
    /// the three defined, or if UNDEFINED was used instead of a specification identifier.</exception>
    /// <exception cref ="ECapeFailedInitialisation"><para>The pre-requisites for the Equilibrium 
    /// Calculation are not valid. For example:</para>
    /// <para>• The overall composition of the mixture is not defined.</para>
    /// <para>• The Material Object (set by a previous call to the SetMaterial method of the
    /// ICapeThermoMaterialContext interface) is not valid. This could be because no 
    /// Phases are present or because the Phases present are not recognised by the
    /// component that implements the ICapeThermoEquilibriumRoutine interface.</para>
    /// <para>• Any other necessary input information is not available.</para></exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method CalcEquilibrium")]
    void CalcEquilibrium(
        [In] object specification1,
        [In] object specification2,
        [In] string solutionType);

    /// <summary>Checks whether the Property Package can support a particular type of 
    /// Equilibrium Calculation.</summary>
    /// <remarks>
    /// <para>The meaning of the specification1, specification2 and solutionType 
    /// arguments is the same as for the CalcEquilibrium method.</para>
    /// <para>The result of the check should only depend on the capabilities and 
    /// configuration (compounds and phases present) of the component that implements 
    /// the ICapeThermoEquilibriumRoutine interface (eg. a Property package). It should 
    /// not depend on whether a Material Object has been set nor on the state 
    /// (temperature, pressure, composition etc.) or configuration of a Material 
    /// Object that might be set.</para>
    /// <para>If solutionType, specification1 and specification2 arguments appear 
    /// valid but the actual specifications are not supported or not recognised a 
    /// False value should be returned.</para>
    /// </remarks>
    /// <param name = "specification1">First specification for the Equilibrium 
    /// Calculation.</param>
    /// <param name = "specification2">Second specification for the Equilibrium 
    /// Calculation.</param>
    /// <param name = "solutionType">The required solution type.</param>
    /// <returns>Set to True if the combination of specifications and solutionType is 
    /// supported or False if not supported.</returns>
    /// <exception cref ="ECapeNoImpl">The operation is “not” implemented even if this 
    /// method can be called for reasons of compatibility with the CAPE-OPEN standards. 
    /// That is to say that the operation exists, but it is not supported by the 
    /// current implementation.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument 
    /// value is passed, for example UNDEFINED for solutionType, specification1 or 
    /// specification2 argument.</exception>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), 
    /// specified for this operation, are not suitable.</exception>
    [DispId(0x00000002)]
    [Description("Method CheckEquilibriumSpec")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool CheckEquilibriumSpec(
        [In] object specification1,
        [In] object specification2,
        [In] string solutionType);
}

/// <summary>Implemented by a component that can return the value of a Universal 
/// Constant.</summary>
/// <remarks>Any component that can return the value of a Universal Constant can 
/// implement the ICapeThermoUniversalConstants interface in order that clients can 
/// access these values. This interface is optional for all components. It is 
/// recommended that it is implemented by Property Package components and Material 
/// Objects being used by CAPE-OPEN Unit Operations.</remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.InCapeTheUnSalConComIid)]  // "678C0AA1-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoUniversalConstant Interface")]
internal interface ICapeThermoUniversalConstantCOM
{
    /// <summary>Retrieves the value of a Universal Constant.</summary>
    /// <param name = "constantId">Identifier of Universal Constant. The list of 
    /// constants supported should be obtained by using the GetUniversalConstList 
    /// method.</param>
    /// <returns>Value of Universal Constant. This could be a numeric or a string 
    /// value. For numeric values the units of measurement are specified in section 
    /// 7.5.1.</returns>
    /// <remarks>Universal Constants (often called fundamental constants) are 
    /// quantities like the gas constant, or the Avogadro constant.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetUniversalConstant is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeInvalidArgument">For example, UNDEFINED for constantId 
    /// argument is used, or value for constantId argument does not belong to the 
    /// list of recognised values.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetUniversalConstant operation, are not suitable.</exception>	
    [DispId(0x00000001)]
    [Description("Method GetUniversalConstant")]
    object GetUniversalConstant([In] string constantId);

    /// <summary>Returns the identifiers of the supported Universal Constants.</summary>
    /// <returns>List of identifiers of Universal Constants. The list of standard 
    /// identifiers is given in section 7.5.1.</returns>
    /// <remarks>A component may return Universal Constant identifiers that do not 
    /// belong to the list defined in section 7.5.1. However, these proprietary 
    /// identifiers may not be understood by most of the clients of this component.
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetUniversalConstantList is 
    /// “not” implemented even if this method can be called for reasons of 
    /// compatibility with the CAPE-OPEN standards. That is to say that the operation 
    /// exists, but it is not supported by the current implementation. This may occur 
    /// when the Property Package does not support any Universal Constants, or if it
    /// does not want to provide values for any Universal Constants which may be used 
    /// within the Property Package.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetUniversalConstantList operation, are not suitable.
    /// </exception>
    [DispId(0x00000002)]
    [Description("Method GetUniversalConstantList")]
    object GetUniversalConstantList();
}

/// <summary>Implemented by a component that can return the value of a Universal 
/// Constant.</summary>
/// <remarks>Any component that can return the value of a Universal Constant can 
/// implement the ICapeThermoUniversalConstants interface in order that clients can 
/// access these values. This interface is optional for all components. It is 
/// recommended that it is implemented by Property Package components and Material 
/// Objects being used by CAPE-OPEN Unit Operations.</remarks>
[ComVisible(false)]
[Description("ICapeThermoUniversalConstant Interface")]
public interface ICapeThermoUniversalConstant
{
    /// <summary>Retrieves the value of a Universal Constant.</summary>
    /// <param name = "constantId">Identifier of Universal Constant. The list of 
    /// constants supported should be obtained by using the GetUniversalConstList 
    /// method.</param>
    /// <returns>Value of Universal Constant. This could be a numeric or a string 
    /// value. For numeric values the units of measurement are specified in section 
    /// 7.5.1.</returns>
    /// <remarks>Universal Constants (often called fundamental constants) are 
    /// quantities like the gas constant, or the Avogadro constant.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetUniversalConstant is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but 
    /// it is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeInvalidArgument">For example, UNDEFINED for constantId 
    /// argument is used, or value for constantId argument does not belong to the 
    /// list of recognised values.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetUniversalConstant operation, are not suitable.</exception>	
    [DispId(0x00000001)]
    [Description("Method GetUniversalConstant")]
    object GetUniversalConstant(string constantId);

    /// <summary>Returns the identifiers of the supported Universal Constants.</summary>
    /// <returns>List of identifiers of Universal Constants. The list of standard 
    /// identifiers is given in section 7.5.1.</returns>
    /// <remarks>A component may return Universal Constant identifiers that do not 
    /// belong to the list defined in section 7.5.1. However, these proprietary 
    /// identifiers may not be understood by most of the clients of this component.
    /// </remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetUniversalConstantList is 
    /// “not” implemented even if this method can be called for reasons of 
    /// compatibility with the CAPE-OPEN standards. That is to say that the operation 
    /// exists, but it is not supported by the current implementation. This may occur 
    /// when the Property Package does not support any Universal Constants, or if it
    /// does not want to provide values for any Universal Constants which may be used 
    /// within the Property Package.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetUniversalConstantList operation, are not suitable.
    /// </exception>
    [DispId(0x00000002)]
    [Description("Method GetUniversalConstantList")]
    string[] GetUniversalConstantList();
}

/// <summary>The ICapeThermoPropertyPackageManager interface should only be implemented 
/// by a Property Package Manager component. This interface is used to access the 
/// Property Packages managed by such a component.</summary>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.InCapeTheProPackManComIid)]  // "678C0AA2-7D66-11D2-A67D-00105A42887F"
[Description("ICapeThermoPropertyPackageManager Interface")]
internal interface ICapeThermoPropertyPackageManagerCOM
{
    /// <summary>Retrieves the names of the Property Packages being managed by a 
    /// Property Package Manager component.</summary>
    /// <returns>The names of the managed Property Packages</returns>
    /// <remarks>If no packages are managed by the Property Package Manager UNDEFINED 
    /// should be returned.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetPropertyPackageList is “not” 
    /// implemented even if this method can be called for reasons of compatibility with 
    /// the CAPE-OPEN standards. That is to say that the operation exists, but it is 
    /// not supported by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetPropertyPackageList operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method GetPropertyPackageList")]
    object GetPropertyPackageList();

    /// <summary>Creates a new instance of a Property Package with the configuration 
    /// specified by the PackageName argument.</summary>
    /// <param name = "packageName">The name of one of the Property Packages managed 
    /// by this Property Package Manager component.</param>
    /// <returns>The ICapeThermoPropertyRoutine interface of the named Property 
    /// Package.</returns>
    /// <remarks><para>The Property Package Manager is only an indirect mechanism to create 
    /// Property Packages.</para>
    /// <para>After the Property Package has been created, the Property Package Manager 
    /// instance can be destroyed, and this will not affect the normal behaviour of 
    /// the Property Packages.</para>
    /// </remarks>
    /// <exception cref ="ECapeNoImpl">The operation GetPropertyPackage is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but it 
    /// is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeFailedInitialisation">This error should be returned if 
    /// the Property Package cannot be created for any reason.</exception>
    /// <exception cref = "ECapeInvalidArgument">This error will be returned if the 
    /// name of the Property Package asked for does not belong to the list of 
    /// recognised names. Comparison of names is not case sensitive.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetPropertyPackage operation, are not suitable.</exception>
    [DispId(0x00000002)]
    [Description("Method GetPropertyPackage")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetPropertyPackage([In] string packageName);
}

/// <summary>The ICapeThermoPropertyPackageManager interface should only be implemented 
/// by a Property Package Manager component. This interface is used to access the 
/// Property Packages managed by such a component.</summary>
[ComVisible(false)]
[Description("ICapeThermoPropertyPackageManager Interface")]
public interface ICapeThermoPropertyPackageManager
{
    /// <summary>Retrieves the names of the Property Packages being managed by a 
    /// Property Package Manager component.</summary>
    /// <returns>The names of the managed Property Packages</returns>
    /// <remarks>If no packages are managed by the Property Package Manager UNDEFINED 
    /// should be returned.</remarks>
    /// <exception cref = "ECapeNoImpl">The operation GetPropertyPackageList is “not” 
    /// implemented even if this method can be called for reasons of compatibility with 
    /// the CAPE-OPEN standards. That is to say that the operation exists, but it is 
    /// not supported by the current implementation.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetPropertyPackageList operation, are not suitable.</exception>
    [DispId(0x00000001)]
    [Description("Method GetPropertyPackageList")]
    string[] GetPropertyPackageList();

    /// <summary>Creates a new instance of a Property Package with the configuration 
    /// specified by the PackageName argument.</summary>
    /// <param name = "packageName">The name of one of the Property Packages managed 
    /// by this Property Package Manager component.</param>
    /// <returns>The ICapeThermoPropertyRoutine interface of the named Property 
    /// Package.</returns>
    /// <remarks><para>The Property Package Manager is only an indirect mechanism to create 
    /// Property Packages.</para>
    /// <para>After the Property Package has been created, the Property Package Manager 
    /// instance can be destroyed, and this will not affect the normal behaviour of 
    /// the Property Packages.</para>
    /// </remarks>
    /// <exception cref ="ECapeNoImpl">The operation GetPropertyPackage is “not” 
    /// implemented even if this method can be called for reasons of compatibility 
    /// with the CAPE-OPEN standards. That is to say that the operation exists, but it 
    /// is not supported by the current implementation.</exception>
    /// <exception cref = "ECapeFailedInitialisation">This error should be returned if 
    /// the Property Package cannot be created for any reason.</exception>
    /// <exception cref = "ECapeInvalidArgument">This error will be returned if the 
    /// name of the Property Package asked for does not belong to the list of 
    /// recognised names. Comparison of names is not case sensitive.</exception>
    /// <exception cref = "ECapeUnknown">The error to be raised when other error(s), 
    /// specified for the GetPropertyPackage operation, are not suitable.</exception>
    [DispId(0x00000002)]
    [Description("Method GetPropertyPackage")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    ICapeThermoPropertyRoutine GetPropertyPackage(string packageName);
}

/// <summary>
/// A flag that indicates the desired calculations for the <see cref = "ICapeThermoPropertyRoutine.CalcAndGetLnPhi">ICapeThermoPropertyRoutine.CalcAndGetLnPhi</see> method.
/// </summary>
/// <remarks>
/// <para>The quantities actually calculated and returned by this method are 
/// controlled by an integer code fFlags. The code is formed by summing contributions 
/// for the property and each derivative required using the enumerated constants 
/// CapeCalculationCode (defined in the Thermo version 1.1 IDL) shown in the following 
/// table. For example, to calculate log fugacity coefficients and their T-derivatives 
/// the fFlags argument would be set to CAPE_LOG_FUGACITY_COEFFICIENTS | CAPE_T_DERIVATIVE (bitwise "or' operator).</para>
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
/// property values are returned. </para>
/// </remarks>
[Serializable]
public enum CapeCalculationCode
{
    /// <summary>Do not calulate any proeprty values.</summary>
    CAPE_NO_CALCULATION = 0,
    /// <summary>Calculate the value of the log of the fugacity coefficient.</summary>
    CAPE_LOG_FUGACITY_COEFFICIENTS = 1,
    /// <summary>Calculate the value of the temperature derivates.</summary>
    CAPE_T_DERIVATIVE = 2,
    /// <summary>Calculate the value of the pressure derivates.</summary>
    CAPE_P_DERIVATIVE = 4,
    /// <summary>Calculate the value of the mole number derivates.</summary>
    CAPE_MOLE_NUMBERS_DERIVATIVES = 8
}

/// <summary>
/// Status of the phases present in the material object.
/// </summary>
/// <remarks>All the Phases with a status of Cape_AtEquilibrium have values of 
/// temperature, pressure, composition and Phase fraction set that correspond to an 
/// equilibrium state, i.e. equal temperature, pressure and fugacities of each 
/// Compound. Phases with a Cape_Estimates status have values of temperature, pressure, 
/// composition and Phase fraction set in the Material Object. These values are 
/// available for use by an Equilibrium Calculator component to initialise an 
/// Equilibrium Calculation. The stored values are available but there is no guarantee 
/// that they will be used.
/// </remarks>
[Serializable]
public enum CapePhaseStatus
{
    /// <summary>This is the normal setting when a Phase is specified as being available for 
    /// an Equilibrium Calculation.</summary>
    CAPE_UNKNOWNPHASESTATUS = 0,
    /// <summary>The Phase has been set as present as a result of an Equilibrium Calculation.</summary>
    CAPE_ATEQUILIBRIUM = 1,
    /// <summary>Estimates of the equilibrium state have been set in the Material Object.</summary>
    CAPE_ESTIMATES = 2
}