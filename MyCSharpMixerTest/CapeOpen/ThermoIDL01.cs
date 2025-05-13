/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2002.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org

This file has been edited using the editor from Microsoft Visual Studio 6.0
This file can viewed properly with any basic editors and browsers (validation done under MS Windows and Unix)
*/

// This file was developed/modified by JEAN-PIERRE BELAUD for CO-LaN organisation - March 2003


// ---- The scope of thermodynamic and physical properties interface
// Reference document: Thermodynamic and physical properties

namespace CapeOpen;

/// <summary>
/// Interface for the reliability of the Thermo Object.
/// </summary>
/// <remarks>
/// The ThermoReliability object is still an uncertain
/// interface. This object holds some measure of the reliability of
/// the physical property calculation.  It might be a boolean.  It
/// might be an enumerated type, or it might be a real number.
/// </remarks>
[System.Runtime.InteropServices.ComImport()]
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.Runtime.InteropServices.GuidAttribute(CapeOpenGuids.ICapeThermoReliability_IID)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoReliability Interface")]
public interface ICapeThermoReliability
{
    // TO BE DEFINED
};

/// <summary>
/// Material Template interface
/// </summary>
[System.Runtime.InteropServices.ComImport()]
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.Runtime.InteropServices.GuidAttribute(CapeOpenGuids.ICapeThermoMaterialTemplate_IID)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoMaterialTemplate Interface")]
public interface ICapeThermoMaterialTemplate
{
    /// <summary>
    /// Create a material object from this Template
    /// </summary>
    /// <remarks>
    /// Allows a Material Object to be created from the Material Template interface.
    /// </remarks>
    /// <returns>
    /// The created/initialized Material Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(1)]
    [System.ComponentModel.DescriptionAttribute("method CreateMaterialObject")]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
    ICapeThermoMaterialObject CreateMaterialObject();

    /// <summary>
    /// Set some property value(s)
    /// </summary>
    /// <remarks>
    /// Allows custom property and values to be set on the Material Template to 
    /// support pseudo components.
    /// </remarks>
    /// <param name = "property">
    /// The custom property to set.
    /// </param>
    /// <param name = "values">
    /// The actual values of the property. A System.Object containing a double 
    /// array marshalled from a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(2)]
    [System.ComponentModel.DescriptionAttribute("method SetProp")]
    void SetProp(String property, Object values);
};

/// <summary>
/// Material object interface
/// </summary>
[System.Runtime.InteropServices.ComImport()]
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.Runtime.InteropServices.GuidAttribute(CapeOpenGuids.ICapeThermoMaterialObject_IID)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoMaterialObject Interface")]
interface ICapeThermoMaterialObjectCOM
{
    /// <summary>
    /// Get the component ids for this MO
    /// </summary>
    /// <remarks>
    /// Returns the list of components Ids of a given Material Object.
    /// </remarks>
    /// <value>
    /// The names of the compounds in the matieral object in a String array 
    /// as a System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(1)]
    [System.ComponentModel.DescriptionAttribute("property ComponentIds")]
    Object ComponentIds
    {
        get;
    }

    /// <summary>
    /// Get the phase ids for this MO
    /// </summary>
    /// <remarks>
    /// It returns the phases existing in the MO at that moment. The Overall phase 
    /// and multiphase identifiers cannot be returned by this method. See notes on 
    /// Existence of a phase for more information.
    /// </remarks>
    /// <value>
    /// The phases present in the material in a String array as a 
    /// System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(2)]
    [System.ComponentModel.DescriptionAttribute("property PhaseIds")]
    Object PhaseIds
    {
        get;
    }

    /// <summary>
    /// Get some universal constant(s)
    /// </summary>
    /// <remarks>
    /// Retrieves universal constants from the Property Package.
    /// </remarks>
    /// <returns>
    /// Values of the requested universal constants in an array of doubles as a 
    /// System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </returns>
    /// <param name = "props">
    /// List of universal constants to be retrieved. A System.Object containing a 
    /// String array.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [System.Runtime.InteropServices.DispIdAttribute(3)]
    [System.ComponentModel.DescriptionAttribute("method GetUniversalConstant")]
    Object GetUniversalConstant(Object props);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// Retrieve component constants from the Property Package. See Notes for more 
    /// information.
    /// </remarks>
    /// <returns>
    /// Component Constant values returned from the Property Package for all the 
    /// components in the Material Object It is a Object containing a 1 dimensional 
    /// array of Objects. If we call P to the number of requested properties and C to 
    /// the number requested components the array will contain C*P Objects. The C 
    /// first ones (from position 0 to C-1) will be the values for the first requested 
    /// property (one Object for each component). After them (from position C to 2*C-1) 
    /// there will be the values of constants for the second requested property, and 
    /// so on. An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN.
    /// </returns>
    /// <param name = "props">
    /// List of component constants. A System.Object containing a String array 
    /// marshalled from a COM Object.
    /// </param>
    /// <param name = "compIds">
    /// List of component IDs for which constants are to be retrieved. emptyObject 
    /// for all components in the Material Object. A System.Object containing a String 
    /// array marshalled from a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [System.Runtime.InteropServices.DispIdAttribute(4)]
    [System.ComponentModel.DescriptionAttribute("method GetComponentConstant")]
    Object GetComponentConstant(Object props, Object compIds);

    /// <summary>
    /// Calculate some properties
    /// </summary>
    /// <remarks>
    /// This method is responsible for doing all property calculations and delegating 
    /// these calculations to the associated thermo system. This method is further 
    /// defined in the descriptions of the CAPE-OPEN Calling Pattern and the User 
    /// Guide Section. See Notes for a more detailed explanation of the arguments and 
    /// CalcProp description in the notes for a general discussion of the method.
    /// </remarks>
    /// <param name = "props">
    /// The List of Properties to be calculated. A System.Object containing a String 
    /// array.
    /// </param>
    /// <param name = "phases">
    /// List of phases for which the properties are to be calculated. A System.Object 
    /// containing a String array.
    /// </param>
    /// <param name = "calcType">
    /// Type of calculation: Mixture Property or Pure Component Property. For partial 
    /// property, such as fugacity coefficients of components in a mixture, use 
    /// “Mixture” CalcType. For pure component fugacity coefficients, use “Pure” 
    /// CalcType.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(5)]
    [System.ComponentModel.DescriptionAttribute("method CalcProp")]
    void CalcProp(Object props, Object phases, System.String calcType);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// This method is responsible for retrieving the results from calculations from 
    /// the MaterialObject. See Notesfor a more detailed explanation of the arguments.
    /// </remarks>
    /// <returns>
    /// Results vector containing property values in SI units arranged by the defined 
    /// qualifiers. The array is one dimensional containing the properties, in order 
    /// of the "props" array for each of the compounds, in order of the compIds array. 
    /// An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN. 
    /// </returns>
    /// <param name = "property">
    /// The Property for which results are requested from the MaterialObject.
    /// </param>
    /// <param name = "phase">
    /// The qualified phase for the results.
    /// </param>
    /// <param name = "compIds">
    /// The qualified components for the results. emptyObject to specify all 
    /// components in the Material Object. For mixture property such as liquid 
    /// enthalpy, this qualifier is not required. Use emptyObject as place holder.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </param>
    /// <param name = "calcType">
    /// The qualified type of calculation for the results. (valid Calculation Types: 
    /// Pure and Mixture)
    /// </param>
    /// <param name = "basis">
    /// Qualifies the basis of the result (i.e., mass /mole). Default is mole. Use 
    /// NULL for default or as place holder for property for which basis does not 
    /// apply (see also Specific properties.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(6)]
    [System.ComponentModel.DescriptionAttribute("method GetProp")]
    Object GetProp(System.String property,
        System.String phase,
        Object compIds,
        System.String calcType,
        System.String basis);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// This method is responsible for setting the values for properties of the 
    /// Material Object. See Notes for a more detailed explanation of the arguments.
    /// </remarks>
    /// <param name = "property">
    /// The Property for which results are requested from the MaterialObject.
    /// </param>
    /// <param name = "phase">
    /// The qualified phase for the results.
    /// </param>
    /// <param name = "compIds">
    /// The qualified components for the results. emptyObject to specify all 
    /// components in the Material Object. For mixture property such as liquid 
    /// enthalpy, this qualifier is not required. Use emptyObject as place holder.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </param>
    /// <param name = "calcType">
    /// The qualified type of calculation for the results. (valid Calculation Types: 
    /// Pure and Mixture)
    /// </param>
    /// <param name = "basis">
    /// Qualifies the basis of the result (i.e., mass /mole). Default is mole. Use 
    /// NULL for default or as place holder for property for which basis does not 
    /// apply (see also Specific properties.
    /// </param>
    /// <param name = "values">
    /// Values to set for the property.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(7)]
    [System.ComponentModel.DescriptionAttribute("method SetProp")]
    void SetProp(System.String property,
        System.String phase,
        Object compIds,
        System.String calcType,
        System.String basis,
        Object values);

    /// <summary>
    /// Calculate some equilibrium values
    /// </summary>
    /// <remarks>
    /// This method is responsible for delegating flash calculations to the 
    /// associated Property Package or Equilibrium Server. It must set the amounts, 
    /// compositions, temperature and pressure for all phases present at equilibrium, 
    /// as well as the temperature and pressure for the overall mixture, if not set 
    /// as part of the calculation specifications. See CalcProp and CalcEquilibrium 
    /// for more information.
    /// </remarks>
    /// <param name = "flashType">
    /// The type of flash to be calculated.
    /// </param>
    /// <param name = "props">
    /// Properties to be calculated at equilibrium. emptyObject for no properties. 
    /// If a list, then the property values should be set for each phase present at 
    /// equilibrium. A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(8)]
    [System.ComponentModel.DescriptionAttribute("method CalcEquilibrium")]
    void CalcEquilibrium(System.String flashType, Object props);

    /// <summary>
    /// Set the independent variable for the state
    /// </summary>
    /// <remarks>
    /// Sets the independent variable for a given Material Object.
    /// </remarks>
    /// <param name = "indVars">
    /// Independent variables to be set (see names for state variables for list of 
    /// valid variables). A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <param name = "values">
    /// Values of independent variables.
    /// An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN. 
    /// </param>
    [System.Runtime.InteropServices.DispIdAttribute(9)]
    [System.ComponentModel.DescriptionAttribute("method SetIndependentVar")]
    void SetIndependentVar(Object indVars, Object values);

    /// <summary>
    /// Get the independent variable for the state
    /// </summary>
    /// <remarks>
    /// Sets the independent variable for a given Material Object.
    /// </remarks>
    /// <param name = "indVars">
    /// Independent variables to be set (see names for state variables for list of 
    /// valid variables). A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <returns>
    /// Values of independent variables.
    /// An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN. 
    /// </returns>
    [System.Runtime.InteropServices.DispIdAttribute(10)]
    [System.ComponentModel.DescriptionAttribute("method GetIndependentVar")]
    Object GetIndependentVar(Object indVars);

    /// <summary>
    /// Check a property is valid
    /// </summary>
    /// <remarks>
    /// Checks to see if given properties can be calculated.
    /// </remarks>
    /// <returns>
    /// Returns Boolean List associated to list of properties to be checked.
    /// An array of booleans (VT_BOOL) as a System.Object, which is marshalled as a 
    /// Object COM-based CAPE-OPEN. 
    /// </returns>
    /// <param name = "props">
    /// Properties to check. A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(11)]
    [System.ComponentModel.DescriptionAttribute("method PropCheck")]
    Object PropCheck(Object props);

    /// <summary>
    /// Check which properties are available
    /// </summary>
    /// <remarks>
    /// Gets a list properties that have been calculated.
    /// </remarks>
    /// <returns>
    /// Properties for which results are available.in a String array as a 
    /// System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(12)]
    [System.ComponentModel.DescriptionAttribute("method AvailableProps")]
    Object AvailableProps();

    /// <summary>
    /// Remove any previously calculated results for given properties
    /// </summary>
    /// <remarks>
    /// Remove all or specified property results in the Material Object.
    /// </remarks>
    /// <param name = "props">
    /// Properties to be removed. emptyObject to remove all properties. A 
    /// System.Object containing a String array marshalled from a COM Object.
    /// </param>
    [System.Runtime.InteropServices.DispIdAttribute(13)]
    [System.ComponentModel.DescriptionAttribute("method RemoveResults")]
    void RemoveResults(Object props);

    /// <summary>
    /// Create another empty material object
    /// </summary>
    /// <remarks>
    /// Create a Material Object from the parent Material Template of the current 
    /// Material Object. This is the same as using the CreateMaterialObject method 
    /// on the parent Material Template.
    /// </remarks> 
    /// <returns>
    /// The created/initialized Material Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(14)]
    [System.ComponentModel.DescriptionAttribute("method CreateMaterialObject")]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
    Object CreateMaterialObject();

    /// <summary>
    /// Duplicate this material object
    /// </summary>
    /// <remarks>
    /// Create a duplicate of the current Material Object.
    /// </remarks>
    /// <returns>
    /// The created/initialized Material Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(15)]
    [System.ComponentModel.DescriptionAttribute("method Duplicate")]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
    Object Duplicate();

    /// <summary>
    /// Check the validity of the given properties
    /// </summary>
    /// <remarks>
    /// Checks the validity of the calculation.
    /// </remarks>
    /// <returns>
    /// Returns the reliability scale of the calculation.
    /// </returns>
    /// <param name = "props">
    /// The properties for which reliability is checked. emptyObject to remove all 
    /// properties. A System.Object containing a String array marshalled from a COM 
    /// Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(16)]
    [System.ComponentModel.DescriptionAttribute("method ValidityCheck")]
    Object ValidityCheck(Object props);

    /// <summary>
    /// Get the list of properties
    /// </summary>
    /// <remarks>
    /// Returns list of properties supported by the property package and corresponding 
    /// CO Calculation Routines. The properties TEMPERATURE, PRESSURE, FRACTION, FLOW, 
    /// PHASEFRACTION, TOTALFLOW cannot be returned by GetPropList, since all 
    /// components must support them. Although the property identifier of derivative 
    /// properties is formed from the identifier of another property, the GetPropList 
    /// method will return the identifiers of all supported derivative and 
    /// non-derivative properties. For instance, a Property Package could return 
    /// the following list: enthalpy, enthalpy.Dtemperature, entropy, entropy.Dpressure.
    /// </remarks>
    /// <returns>
    /// String list of all supported properties of the property package.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    [System.Runtime.InteropServices.DispIdAttribute(17)]
    [System.ComponentModel.DescriptionAttribute("method GetPropList")]
    Object GetPropList();

    /// <summary>
    /// Get the number of components in this material object
    /// </summary>
    /// <remarks>
    /// Returns number of components in Material Object.
    /// </remarks>
    /// <value>
    /// Number of components in the Material Object.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(18)]
    [System.ComponentModel.DescriptionAttribute("method GetNumComponents")]
    int GetNumComponents();
};

/// <summary>
/// Material object interface
/// </summary>
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoMaterialObject Interface")]
public interface ICapeThermoMaterialObject
{
    /// <summary>
    /// Get the component ids for this MO
    /// </summary>
    /// <remarks>
    /// Returns the list of components Ids of a given Material Object.
    /// </remarks>
    /// <returns>
    /// The names of the compounds in the matieral object in a String array 
    /// as a System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(1)]
    [System.ComponentModel.DescriptionAttribute("property ComponentIds")]
    String[] ComponentIds
    {
        get;
    }

    /// <summary>
    /// Get the phase ids for this MO
    /// </summary>
    /// <remarks>
    /// It returns the phases existing in the MO at that moment. The Overall phase 
    /// and multiphase identifiers cannot be returned by this method. See notes on 
    /// Existence of a phase for more information.
    /// </remarks>
    /// <value>
    /// The phases present in the material in a String array as a 
    /// System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(2)]
    [System.ComponentModel.DescriptionAttribute("property PhaseIds")]
    String[] PhaseIds
    {
        get;
    }

    /// <summary>
    /// Get some universal constant(s)
    /// </summary>
    /// <remarks>
    /// Retrieves universal constants from the Property Package.
    /// </remarks>
    /// <returns>
    /// Values of the requested universal constants in an array of doubles as a 
    /// System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </returns>
    /// <param name = "props">
    /// List of universal constants to be retrieved. A System.Object containing a 
    /// String array.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [System.Runtime.InteropServices.DispIdAttribute(3)]
    [System.ComponentModel.DescriptionAttribute("method GetUniversalConstant")]
    double[] GetUniversalConstant(string[] props);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// Retrieve component constants from the Property Package. See Notes for more 
    /// information.
    /// </remarks>
    /// <returns>
    /// Component Constant values returned from the Property Package for all the 
    /// components in the Material Object It is a Object containing a 1 dimensional 
    /// array of Objects. If we call P to the number of requested properties and C to 
    /// the number requested components the array will contain C*P Objects. The C 
    /// first ones (from position 0 to C-1) will be the values for the first requested 
    /// property (one Object for each component). After them (from position C to 2*C-1) 
    /// there will be the values of constants for the second requested property, and 
    /// so on. An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN.
    /// </returns>
    /// <param name = "props">
    /// List of component constants. A System.Object containing a String array 
    /// marshalled from a COM Object.
    /// </param>
    /// <param name = "compIds">
    /// List of component IDs for which constants are to be retrieved. emptyObject 
    /// for all components in the Material Object. A System.Object containing a String 
    /// array marshalled from a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [System.Runtime.InteropServices.DispIdAttribute(4)]
    [System.ComponentModel.DescriptionAttribute("method GetComponentConstant")]
    object[] GetComponentConstant(string[] props, string[] compIds);

    /// <summary>
    /// Calculate some properties
    /// </summary>
    /// <remarks>
    /// This method is responsible for doing all property calculations and delegating 
    /// these calculations to the associated thermo system. This method is further 
    /// defined in the descriptions of the CAPE-OPEN Calling Pattern and the User 
    /// Guide Section. See Notes for a more detailed explanation of the arguments and 
    /// CalcProp description in the notes for a general discussion of the method.
    /// </remarks>
    /// <param name = "props">
    /// The List of Properties to be calculated. A System.Object containing a String 
    /// array.
    /// </param>
    /// <param name = "phases">
    /// List of phases for which the properties are to be calculated. A System.Object 
    /// containing a String array.
    /// </param>
    /// <param name = "calcType">
    /// Type of calculation: Mixture Property or Pure Component Property. For partial 
    /// property, such as fugacity coefficients of components in a mixture, use 
    /// “Mixture” CalcType. For pure component fugacity coefficients, use “Pure” 
    /// CalcType.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(5)]
    [System.ComponentModel.DescriptionAttribute("method CalcProp")]
    void CalcProp(string[] props, string[] phases, System.String calcType);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// This method is responsible for retrieving the results from calculations from 
    /// the MaterialObject. See Notesfor a more detailed explanation of the arguments.
    /// </remarks>
    /// <returns>
    /// Results vector containing property values in SI units arranged by the defined 
    /// qualifiers. The array is one dimensional containing the properties, in order 
    /// of the "props" array for each of the compounds, in order of the compIds array. 
    /// An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN. 
    /// </returns>
    /// <param name = "property">
    /// The Property for which results are requested from the MaterialObject.
    /// </param>
    /// <param name = "phase">
    /// The qualified phase for the results.
    /// </param>
    /// <param name = "compIds">
    /// The qualified components for the results. emptyObject to specify all 
    /// components in the Material Object. For mixture property such as liquid 
    /// enthalpy, this qualifier is not required. Use emptyObject as place holder.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </param>
    /// <param name = "calcType">
    /// The qualified type of calculation for the results. (valid Calculation Types: 
    /// Pure and Mixture)
    /// </param>
    /// <param name = "basis">
    /// Qualifies the basis of the result (i.e., mass /mole). Default is mole. Use 
    /// NULL for default or as place holder for property for which basis does not 
    /// apply (see also Specific properties.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(6)]
    [System.ComponentModel.DescriptionAttribute("method GetProp")]
    double[] GetProp(System.String property,
        System.String phase,
        string[] compIds,
        System.String calcType,
        System.String basis);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// This method is responsible for setting the values for properties of the 
    /// Material Object. See Notes for a more detailed explanation of the arguments.
    /// </remarks>
    /// <param name = "property">
    /// The Property for which results are requested from the MaterialObject.
    /// </param>
    /// <param name = "phase">
    /// The qualified phase for the results.
    /// </param>
    /// <param name = "compIds">
    /// The qualified components for the results. emptyObject to specify all 
    /// components in the Material Object. For mixture property such as liquid 
    /// enthalpy, this qualifier is not required. Use emptyObject as place holder.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </param>
    /// <param name = "calcType">
    /// The qualified type of calculation for the results. (valid Calculation Types: 
    /// Pure and Mixture)
    /// </param>
    /// <param name = "basis">
    /// Qualifies the basis of the result (i.e., mass /mole). Default is mole. Use 
    /// NULL for default or as place holder for property for which basis does not 
    /// apply (see also Specific properties.
    /// </param>
    /// <param name = "values">
    /// Values to set for the property.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(7)]
    [System.ComponentModel.DescriptionAttribute("method SetProp")]
    void SetProp(System.String property,
        System.String phase,
        string[] compIds,
        System.String calcType,
        System.String basis,
        double[] values);

    /// <summary>
    /// Calculate some equilibrium values
    /// </summary>
    /// <remarks>
    /// This method is responsible for delegating flash calculations to the 
    /// associated Property Package or Equilibrium Server. It must set the amounts, 
    /// compositions, temperature and pressure for all phases present at equilibrium, 
    /// as well as the temperature and pressure for the overall mixture, if not set 
    /// as part of the calculation specifications. See CalcProp and CalcEquilibrium 
    /// for more information.
    /// </remarks>
    /// <param name = "flashType">
    /// The type of flash to be calculated.
    /// </param>
    /// <param name = "props">
    /// Properties to be calculated at equilibrium. emptyObject for no properties. 
    /// If a list, then the property values should be set for each phase present at 
    /// equilibrium. A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(8)]
    [System.ComponentModel.DescriptionAttribute("method CalcEquilibrium")]
    void CalcEquilibrium(System.String flashType, string[] props);

    /// <summary>
    /// Set the independent variable for the state
    /// </summary>
    /// <remarks>
    /// Sets the independent variable for a given Material Object.
    /// </remarks>
    /// <param name = "indVars">
    /// Independent variables to be set (see names for state variables for list of 
    /// valid variables). A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <param name = "values">
    /// Values of independent variables.
    /// An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN. 
    /// </param>
    [System.Runtime.InteropServices.DispIdAttribute(9)]
    [System.ComponentModel.DescriptionAttribute("method SetIndependentVar")]
    void SetIndependentVar(string[] indVars, double[] values);

    /// <summary>
    /// Get the independent variable for the state
    /// </summary>
    /// <remarks>
    /// Sets the independent variable for a given Material Object.
    /// </remarks>
    /// <param name = "indVars">
    /// Independent variables to be set (see names for state variables for list of 
    /// valid variables). A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <returns>
    /// Values of independent variables.
    /// An array of doubles as a System.Object, which is marshalled as a Object 
    /// COM-based CAPE-OPEN. 
    /// </returns>
    [System.Runtime.InteropServices.DispIdAttribute(10)]
    [System.ComponentModel.DescriptionAttribute("method GetIndependentVar")]
    double[] GetIndependentVar(string[] indVars);

    /// <summary>
    /// Check a property is valid
    /// </summary>
    /// <remarks>
    /// Checks to see if given properties can be calculated.
    /// </remarks>
    /// <returns>
    /// Returns Boolean List associated to list of properties to be checked.
    /// An array of booleans (VT_BOOL) as a System.Object, which is marshalled as a 
    /// Object COM-based CAPE-OPEN. 
    /// </returns>
    /// <param name = "props">
    /// Properties to check. A System.Object containing a String array marshalled from 
    /// a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(11)]
    [System.ComponentModel.DescriptionAttribute("method PropCheck")]
    bool[] PropCheck(string[] props);

    /// <summary>
    /// Check which properties are available
    /// </summary>
    /// <remarks>
    /// Gets a list properties that have been calculated.
    /// </remarks>
    /// <returns>
    /// Properties for which results are available.in a String array as a 
    /// System.Object, which is marshalled as a Object COM-based CAPE-OPEN.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(12)]
    [System.ComponentModel.DescriptionAttribute("method AvailableProps")]
    string[] AvailableProps();

    /// <summary>
    /// Remove any previously calculated results for given properties
    /// </summary>
    /// <remarks>
    /// Remove all or specified property results in the Material Object.
    /// </remarks>
    /// <param name = "props">
    /// Properties to be removed. emptyObject to remove all properties. A 
    /// System.Object containing a String array marshalled from a COM Object.
    /// </param>
    [System.Runtime.InteropServices.DispIdAttribute(13)]
    [System.ComponentModel.DescriptionAttribute("method RemoveResults")]
    void RemoveResults(string[] props);

    /// <summary>
    /// Create another empty material object
    /// </summary>
    /// <remarks>
    /// Create a Material Object from the parent Material Template of the current 
    /// Material Object. This is the same as using the CreateMaterialObject method 
    /// on the parent Material Template.
    /// </remarks> 
    /// <returns>
    /// The created/initialized Material Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(14)]
    [System.ComponentModel.DescriptionAttribute("method CreateMaterialObject")]
    ICapeThermoMaterialObject CreateMaterialObject();

    /// <summary>
    /// Duplicate this material object
    /// </summary>
    /// <remarks>
    /// Create a duplicate of the current Material Object.
    /// </remarks>
    /// <returns>
    /// The created/initialized Material Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(15)]
    [System.ComponentModel.DescriptionAttribute("method Duplicate")]
    ICapeThermoMaterialObject Duplicate();

    /// <summary>
    /// Check the validity of the given properties
    /// </summary>
    /// <remarks>
    /// Checks the validity of the calculation.
    /// </remarks>
    /// <returns>
    /// Returns the reliability scale of the calculation.
    /// </returns>
    /// <param name = "props">
    /// The properties for which reliability is checked. emptyObject to remove all 
    /// properties. A System.Object containing a String array marshalled from a COM 
    /// Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(16)]
    [System.ComponentModel.DescriptionAttribute("method ValidityCheck")]
    ICapeThermoReliability[] ValidityCheck(string[] props);

    /// <summary>
    /// Get the list of properties
    /// </summary>
    /// <remarks>
    /// Returns list of properties supported by the property package and corresponding 
    /// CO Calculation Routines. The properties TEMPERATURE, PRESSURE, FRACTION, FLOW, 
    /// PHASEFRACTION, TOTALFLOW cannot be returned by GetPropList, since all 
    /// components must support them. Although the property identifier of derivative 
    /// properties is formed from the identifier of another property, the GetPropList 
    /// method will return the identifiers of all supported derivative and 
    /// non-derivative properties. For instance, a Property Package could return 
    /// the following list: enthalpy, enthalpy.Dtemperature, entropy, entropy.Dpressure.
    /// </remarks>
    /// <returns>
    /// String list of all supported properties of the property package.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    [System.Runtime.InteropServices.DispIdAttribute(17)]
    [System.ComponentModel.DescriptionAttribute("method GetPropList")]
    string[] GetPropList();

    /// <summary>
    /// Get the number of components in this material object
    /// </summary>
    /// <remarks>
    /// Returns number of components in Material Object.
    /// </remarks>
    /// <returns>
    /// Number of components in the Material Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(18)]
    [System.ComponentModel.DescriptionAttribute("method GetNumComponents")]
    int GetNumComponents();
};

//class CapeThermoSystem
/// <summary>
/// Interface that provides access to property packages supported by a Thermodynamics Package.
/// </summary>
/// <remarks>
/// <para>This interface is used to access the various substiuent Property Packages provided by a thermodynamic system.</para>
/// <para>In the class library, the <see cref ="CapeThermoSystem">CapeThermoSystem</see> class provides a list of all
/// classes Property Packages registered with COM and all .Net-based property packages that are contained in the Global Assembly Cache.</para>
/// </remarks>
[System.Runtime.InteropServices.ComImport()]
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.Runtime.InteropServices.GuidAttribute(CapeOpenGuids.ICapeThermoSystem_IID)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoSystem Interface")]
interface ICapeThermoSystemCOM
{
    /// <summary>
    /// Get the list of available property packages
    /// </summary>
    /// <remarks>
    /// Returns StringArray of property pacakge names supported by the thermo system.
    /// </remarks>
    /// <returns>
    /// The returned set of supported property packages.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [System.Runtime.InteropServices.DispIdAttribute(1), System.ComponentModel.DescriptionAttribute("method GetPropertyPackages")]
    Object GetPropertyPackages();

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
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [System.Runtime.InteropServices.DispIdAttribute(2)]
    [System.ComponentModel.DescriptionAttribute("method ResolvePropertyPackage")]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
    Object ResolvePropertyPackage(System.String propertyPackage);
};


//class CapeThermoSystem
/// <summary>
/// Interface that provides access to property packages supported by a Thermodynamics Package.
/// </summary>
/// <remarks>
/// <para>This interface is used to access the various substiuent Property Packages provided by a thermodynamic system.</para>
/// <para>In the class library, the <see cref ="CapeThermoSystem">CapeThermoSystem</see> class provides a list of all
/// classes Property Packages registered with COM and all .Net-based property packages that are contained in the Global Assembly Cache.</para>
/// </remarks>
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoSystem Interface")]
public interface ICapeThermoSystem
{
    /// <summary>
    /// Get the list of available property packages
    /// </summary>
    /// <remarks>
    /// Returns StringArray of property pacakge names supported by the thermo system.
    /// </remarks>
    /// <returns>
    /// The returned set of supported property packages.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [System.Runtime.InteropServices.DispIdAttribute(1), System.ComponentModel.DescriptionAttribute("method GetPropertyPackages")]
    string[] GetPropertyPackages();

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
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [System.Runtime.InteropServices.DispIdAttribute(2)]
    [System.ComponentModel.DescriptionAttribute("method ResolvePropertyPackage")]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
    ICapeThermoPropertyPackage ResolvePropertyPackage(System.String propertyPackage);
};

/// <summary>
/// Interface implemented by a CAPE-OPEN version 1.0 Physical Property Package.
/// </summary>
/// <remarks>
/// <para>A Simple Properties Package (SPP) is a complete, consistent, reusable, ready-to-use collection of 
/// methods, chemical components and model parameters for calculating any of a set of known properties for
/// the phases of a multiphase system. It includes all the pure component methods and data, together with 
/// the relevant mixing rules and interaction parameters. A package normally covers only a small subset of 
/// the chemical components and methods accessible through a Properties System. It is thus established by 
/// selecting methods etc from within a larger system, possibly adding to or replacing these methods by 
/// third party components.
/// </para> 
/// <para>These additional methods will normally be CAPE-OPEN compliant methods which may have been specially 
/// written, or may come from another properties system. (They can only come from another system where that 
/// system provides them as CAPE-OPEN compliant components). A Properties Package may be a Simple 
/// Properties Package, or at a vendors discretion, made up from Option Sets (see definition of Option Set).
/// </para>
/// </remarks>
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoPropertyPackage Interface")]
public interface ICapeThermoPropertyPackage
{
    // 
    //
    // CAPE-OPEN exceptions
    // ECapeUnknown
    /// <summary>
    /// Get the phase list
    /// </summary>
    /// <remarks>
    /// Provides the list of the supported phases. When supported, the Overall phase 
    /// and multiphase identifiers must be returned by this method.
    /// </remarks>
    /// <returns>
    /// The list of phases supported by the property package.
    /// A System.Object containing a String array marshalled from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(1)]
    [System.ComponentModel.DescriptionAttribute("method GetPhaseList")]
    Object GetPhaseList();

    /// <summary>
    /// Get the component list
    /// </summary>
    /// <remarks>
    /// <para>Returns the list of components of a given property package.</para>
    /// <para>In order to identify the components of a Property Package, the 
    /// Executive will use the ‘casno’ argument instead of the compIds. The reason is 
    /// that different COSEs may give different names to the same chemical compounds, 
    /// whereas CAS Numbers are universal. Nevertheless, GetProp/SetProp... will still 
    /// require their compIds argument to have the usual contents ("hydrogen",
    /// "methane",...). Be aware that some simulators may have a limitation on the 
    /// length of the names for pure components. Hence, it is recommended that each 
    /// identifier returned by the compIds argument should not contain more than 8 
    /// characters. See notes on Description of component constants for more 
    /// information.</para>
    /// <para>If the package does not return a value for the ‘casno’ argument, or its 
    /// value is not recognised by the Executive, then the compIds will be interpreted 
    /// as the component’s English name: such as "benzene", "water",... Obviously, it 
    /// is recommended to provide a value for the casno argument.</para>
    /// <para>The same information can also be extracted using the 
    /// ICapeThermoPropertyPackage GetComponentConstant method, using the 
    /// casRegistryNumber property identifier.</para>
    /// </remarks>
    /// <param name = "compIds">
    /// Reference value to the list of component IDs.
    /// A reference to a System.Object containing a String array marshalled from a 
    /// COM Object.
    /// </param>
    /// <param name = "formulae">
    /// List of component formulae.
    /// A reference to a System.Object containing a String array marshalled from a 
    /// COM Object.
    /// </param>
    /// <param name = "names">
    /// List of component names.
    /// A reference to a System.Object containing a String array marshalled from a 
    /// COM Object.
    /// </param>
    /// <param name = "boilTemps">
    /// List of boiling point temperatures.
    /// A reference to a System.Object containing a double array marshalled from a 
    /// COM Object.
    /// </param>
    /// <param name = "molWt">
    /// List of molecular weight.
    /// A reference to a System.Object containing a double array marshalled from a 
    /// COM Object.
    /// </param>
    /// <param name = "casNo">
    /// List of CAS number.
    /// A reference to a System.Object containing a String array marshalled from a 
    /// COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(2)]
    [System.ComponentModel.DescriptionAttribute("method GetComponentList")]
    void GetComponentList(ref string[] compIds,
        ref string[] formulae,
        ref string[] names,
        ref double[] boilTemps,
        ref double[] molWt,
        ref string[] casNo);

    /// <summary>
    /// Get some universal constant(s)
    /// </summary>
    /// <remarks>
    /// Returns the values of the Universal Constants.
    /// </remarks>
    /// <param name = "materialObject">
    /// The Material object.
    /// </param>
    /// <param name = "props">
    /// List of requested universal constants.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <returns>
    /// Values of universal constants.
    /// A reference to a System.Object containing an System.Object array marshalled 
    /// from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(3)]
    [System.ComponentModel.DescriptionAttribute("method GetUniversalConstant")]
    Object GetUniversalConstant([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object materialObject, Object props);

    /// <summary>
    /// Get some pure component constant(s)
    /// </summary>
    /// <remarks>
    /// Returns the values of the Constant properties of the components contained in 
    /// the passed Material Object.
    /// </remarks>
    /// <param name = "materialObject">
    /// The Material object.
    /// </param>
    /// <param name = "props">
    /// The list of properties.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <returns>
    /// Component Constant values. See description of return value of the 
    /// <see cref = "ICapeThermoMaterialObject.GetComponentConstant"/> method.
    /// A reference to a System.Object containing an System.Object array marshalled 
    /// from a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(4)]
    [System.ComponentModel.DescriptionAttribute("method GetComponentConstant")]
    Object GetComponentConstant([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object materialObject, Object props);

    /// <summary>
    /// Calculate some proeprties.
    /// </summary>
    /// <remarks>
    /// This method is responsible for doing all calculations and is implemented by 
    /// the associated thermo system. This method is further defined in the 
    /// descriptions of the CAPE-OPEN Calling Pattern and the User Guide
    /// Section.
    /// </remarks>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// The List of Properties to be calculated.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <param name = "phases">
    /// List of phases for which the properties are to be calculated.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <param name = "calcType">
    /// Type of calculation: Mixture Property or Pure Component Property. For partial 
    /// property, such as fugacity coefficients of components in a mixture, use 
    /// “Mixture” CalcType. For pure component fugacity coefficients, use “Pure” 
    /// CalcType.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(5)]
    [System.ComponentModel.DescriptionAttribute("method CalcProp")]
    void CalcProp([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object materialObject, Object props, Object phases, System.String calcType);

    /// <summary>
    /// Calculate some equilibrium values
    /// </summary>
    /// <remarks>
    /// Method responsible for calculating/delegating flash calculation requests. It 
    /// must set the amounts, compositions, temperature and pressure for all phases 
    /// present at equilibrium, as well as the temperature and pressure for the overall 
    /// mixture, if not set as part of the calculation specifications. See CalcProp 
    /// and CalcEquilibrium for more information.
    /// </remarks>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// Properties to be calculated at equilibrium. emptyObject for no properties. 
    /// If a list, then the property values should be set for each phase present at 
    /// equilibrium.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <param name = "flashType">
    /// Flash calculation type.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [System.Runtime.InteropServices.DispIdAttribute(6)]
    [System.ComponentModel.DescriptionAttribute("method CalcEquilibrium")]
    void CalcEquilibrium([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object materialObject, System.String flashType, Object props);


    /// <summary>
    /// Check a property is valid
    /// </summary>
    /// <remarks>
    /// Check to see if properties can be calculated.
    /// </remarks>
    /// <returns>
    /// The array of booleans for each property.
    /// A System.Object containing an System.Boolean (marshalled as VT_BOOL) array 
    /// marshalled as a COM Object.
    /// </returns>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// List of Properties to check.
    /// A System.Object containing a String array marshalled as a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(7), System.ComponentModel.DescriptionAttribute("method PropCheck")]
    Object PropCheck([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object materialObject, Object props);

    /// <summary>
    /// Check the validity of the given properties
    /// </summary>
    /// <remarks>
    /// Checks the validity of the calculation.
    /// </remarks>
    /// <returns>
    /// The properties for which reliability is checked.
    /// A System.Object containing an System.Boolean (marshalled as VT_BOOL) array 
    /// marshalled as a COM Object.
    /// </returns>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// List of Properties to check.
    /// A System.Object containing a CapeArrayThermoReliability marshalled as a 
    /// COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(8), System.ComponentModel.DescriptionAttribute("method ValidityCheck")]
    Object ValidityCheck([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object materialObject, Object props);

    /// <summary>
    /// Get the list of properties
    /// </summary>
    /// <remarks>
    /// <para>Returns list of Thermo System supported properties. The properties TEMPERATURE, 
    /// PRESSURE, FRACTION, FLOW, PHASEFRACTION, TOTALFLOW cannot be returned by 
    /// GetPropList, since all components must support them. Although the property 
    /// identifier of derivative properties is formed from the identifier of another 
    /// property, the GetPropList method will return the identifiers of all supported 
    /// derivative and non-derivative properties. For instance, a Property Package 
    /// could return the following list:
    /// </para>
    /// <para>
    /// enthalpy, enthalpy.Dtemperature, entropy, entropy.Dpressure.
    /// </para>
    /// </remarks>
    /// <returns>
    /// String list of all supported Properties.
    /// A System.Object containing an System.String array marshalled as a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(9), System.ComponentModel.DescriptionAttribute("method GetPropList")]
    Object GetPropList();
};


/// <summary>
/// ICapeThermoCalculationRoutine is a mechanism for adding foreign calculation 
/// routines to a physical property package.
/// </summary>
[System.Runtime.InteropServices.ComVisibleAttribute(false)]
[System.ComponentModel.DescriptionAttribute("ICapeThermoCalculationRoutine Interface")]
public interface ICapeThermoCalculationRoutine
{

    /// <summary>
    /// Calculate some properties
    /// </summary>
    /// <remarks>
    /// This method is responsible for doing all calculations on behalf of the 
    /// calculation routine component. This method is further defined in the 
    /// descriptions of the CAPE-OPEN Calling Pattern and the User Guide Section.
    /// </remarks>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// The List of Properties to be calculated.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <param name = "phases">
    /// List of phases for which the properties are to be calculated.
    /// A reference to a System.Object containing a String array marshalled as a 
    /// COM Object.
    /// </param>
    /// <param name = "calcType">
    /// Type of calculation: Mixture Property or Pure Component Property. For partial 
    /// property, such as fugacity coefficients of components in a mixture, use 
    /// “Mixture” CalcType. For pure component fugacity coefficients, use “Pure” 
    /// CalcType.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    void CalcProp(ICapeThermoMaterialObject materialObject,
        string[] props,
        string[] phases,
        System.String calcType);

    /// <summary>
    /// Check a property is valid
    /// </summary>
    /// <remarks>
    /// Check to see if properties can be calculated.
    /// </remarks>
    /// <returns>
    /// The array of booleans for each property.
    /// A System.Object containing an System.Boolean (marshalled as VT_BOOL) array 
    /// marshalled as a COM Object.
    /// </returns>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// List of Properties to check.
    /// A System.Object containing a String array marshalled as a COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    bool[] PropCheck(ICapeThermoMaterialObject materialObject, string[] props);

    /// <summary>
    /// Get the list of properties
    /// </summary>
    /// <remarks>
    /// <para>Returns list of Thermo System supported properties. The properties TEMPERATURE, 
    /// PRESSURE, FRACTION, FLOW, PHASEFRACTION, TOTALFLOW cannot be returned by 
    /// GetPropList, since all components must support them. Although the property 
    /// identifier of derivative properties is formed from the identifier of another 
    /// property, the GetPropList method will return the identifiers of all supported 
    /// derivative and non-derivative properties. For instance, a Property Package 
    /// could return the following list:
    /// </para>
    /// <para>
    /// enthalpy, enthalpy.Dtemperature, entropy, entropy.Dpressure.
    /// </para>
    /// </remarks>
    /// <returns>
    /// String list of all supported Properties.
    /// A System.Object containing an System.String array marshalled as a COM Object.
    /// </returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(3), System.ComponentModel.DescriptionAttribute("method GetPropList")]
    void GetPropList(ref string[] props, ref string[] phases, ref string[] calcType);

    /// <summary>
    /// Check the validity of the given properties
    /// </summary>
    /// <remarks>
    /// Checks the validity of the calculation.
    /// </remarks>
    /// <returns>
    /// The properties for which reliability is checked.
    /// A System.Object containing an System.Boolean (marshalled as VT_BOOL) array 
    /// marshalled as a COM Object.
    /// </returns>
    /// <param name = "materialObject">
    /// The MaterialObject for the Calculation.
    /// </param>
    /// <param name = "props">
    /// List of Properties to check.
    /// A System.Object containing a CapeArrayThermoReliability marshalled as a 
    /// COM Object.
    /// </param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [System.Runtime.InteropServices.DispIdAttribute(4), System.ComponentModel.DescriptionAttribute("method ValidityCheck")]
    ICapeThermoReliability[] ValidityCheck(ICapeThermoMaterialObject materialObject, string[] props);
};
