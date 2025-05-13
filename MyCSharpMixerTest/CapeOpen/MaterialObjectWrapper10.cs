using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>Wrapper class for COM-based CAPE-OPEN ICapeThermoMaterialObject material object.</summary>
/// <remarks><para>This class is a wrapper class for COM-based CAPE-OPEN ICapeThermoMaterialObject material object.
/// When you use this wrapper class, the lifecycle of the COM-based material is managed for you and the 
/// COM Release() method is called on the material.</para>
/// <para>In addition, this method converts the COM variants used in the <see cref ="ICapeThermoMaterialObject">
/// ICapeThermoMaterialObject</see> interface to the desired .Net object types. This eliminates the need to convert the data types
/// when you use a COM-based CAPE-OPEN material object.</para></remarks>
[ComVisible(false)]
[Guid(CapeOpenGuids.MaterObjWrap10Iid)]  // "5A65B4B2-2FDD-4208-813D-7CC527FB91BD"
[Description("ICapeThermoMaterialObject Interface")]
internal class MaterialObjectWrapper10 : CapeObjectBase, ICapeThermoMaterialObject
{
    [NonSerialized] private ICapeThermoMaterialObjectCOM _pMaterialObject;

    // Track whether Dispose has been called.
    private bool _disposed;

    /// <summary>Creates a new instance of the MaterialObjectWrapper class</summary>
    /// <param name="materialObject">The material Object to be wrapped.</param>
    public MaterialObjectWrapper10(object materialObject)
    {
        _disposed = false;

        if (materialObject is ICapeThermoMaterialObjectCOM materialObjectCom)
        {
            _pMaterialObject = materialObjectCom;
        }
    }

    // Use C# destructor syntax for finalization code.
    /// <summary>Finalizer for the <see cref = "MaterialObjectWrapper"/> class.</summary>
    /// <remarks>This will finalize the current instance of the class.</remarks>
    ~MaterialObjectWrapper10()
    {
        // Simply call Dispose(false).
        Dispose(false);
    }

    /// <summary>Releases the unmanaged resources used by the CapeIdentification object and optionally releases 
    /// the managed resources.</summary>
    /// <remarks><para>This method is called by the public <see href="http://msdn.microsoft.com/en-us/library/system.componentmodel.component.dispose.aspx">Dispose</see>see> 
    /// method and the <see href="http://msdn.microsoft.com/en-us/library/system.object.finalize.aspx">Finalize</see> method. 
    /// <bold>Dispose()</bold> invokes the protected <bold>Dispose(Boolean)</bold> method with the disposing
    /// parameter set to <bold>true</bold>. <see href="http://msdn.microsoft.com/en-us/library/system.object.finalize.aspx">Finalize</see> 
    /// invokes <bold>Dispose</bold> with disposing set to <bold>false</bold>.</para>
    /// <para>When the <italic>disposing</italic> parameter is <bold>true</bold>, this method releases all 
    /// resources held by any managed objects that this Component references. This method invokes the 
    /// <bold>Dispose()</bold> method of each referenced object.</para>
    /// <para><bold>Notes to Inheritors</bold></para>
    /// <para><bold>Dispose</bold> can be called multiple times by other objects. When overriding 
    /// <bold>Dispose(Boolean)</bold>, be careful not to reference objects that have been previously 
    /// disposed of in an earlier call to <bold>Dispose</bold>. For more information about how to 
    /// implement <bold>Dispose(Boolean)</bold>, see <see href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Implementing a Dispose Method</see>.</para>
    /// <para>For more information about <bold>Dispose</bold> and <see href="http://msdn.microsoft.com/en-us/library/system.object.finalize.aspx">Finalize</see>, 
    /// see <see href="http://msdn.microsoft.com/en-us/library/498928w2.aspx">Cleaning Up Unmanaged Resources</see> 
    /// and <see href="http://msdn.microsoft.com/en-us/library/ddae83kx.aspx">Overriding the Finalize Method</see>.</para></remarks> 
    /// <param name = "disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        // If you need thread safety, use a lock around these 
        // operations, as well as in your methods that use the resource.
        if (_disposed) return;
        if (disposing) { }

        // Indicate that the instance has been disposed.
        if (_pMaterialObject.GetType().IsCOMObject)
        {
            Marshal.FinalReleaseComObject(_pMaterialObject);
        }

        _pMaterialObject = null;
        _disposed = true;
    }

    /// <summary>Gets and sets the name of the component.</summary>
    /// <remarks><para>A particular Use Case in a system may contain several CAPE-OPEN components 
    /// of the same class. The user should be able to assign different names and 
    /// descriptions to each instance in order to refer to them unambiguously and in a 
    /// user-friendly way. Since not always the software components that are able to 
    /// set these identifications and the software components that require this information 
    /// have been developed by the same vendor, a CAPE-OPEN standard for setting and 
    /// getting this information is required.</para>
    /// <para>So, the component will not usually set its own name and description: the 
    /// user of the component will do it.</para></remarks>
    /// <value>The unique name of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("CapeIdentification")]
    public override string ComponentName
    {
        get => ((ICapeIdentification)_pMaterialObject).ComponentName;

        set => ((ICapeIdentification)_pMaterialObject).ComponentName = value;
    }

    /// <summary> Gets and sets the description of the component.</summary>
    /// <remarks><para>A particular Use Case in a system may contain several CAPE-OPEN components 
    /// of the same class. The user should be able to assign different names and 
    /// descriptions to each instance in order to refer to them unambiguously and in a 
    /// user-friendly way. Since not always the software components that are able to 
    /// set these identifications and the software components that require this information 
    /// have been developed by the same vendor, a CAPE-OPEN standard for setting and 
    /// getting this information is required.</para>
    /// <para>So, the component will not usually set its own name and description: the 
    /// user of the component will do it.</para></remarks>
    /// <value>The description of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("CapeIdentification")]
    public override string ComponentDescription
    {
        get => ((ICapeIdentification)_pMaterialObject).ComponentDescription;
        set => ((ICapeIdentification)_pMaterialObject).ComponentDescription = value;
    }

    /// <summary>Provides information regarding whether the object supports Thermodynamics version 1.0.</summary>
    /// <remarks>The <see cref = "MaterialObjectWrapper10"/> class checks to determine whether the wrapped material object
    /// supports CAPE-OPEN version 1.0 thermodynamics. This property indicates the result of that check.</remarks>
    /// <value>Indicates whether the wrapped material object supports CAPE-OPEN Thermodynamics version 1.0 interfaces.</value>
    public static bool SupportsThermo10 => true;

    /// <summary>Provides information regarding whether the object supports Thermodynamics version 1.1.</summary>
    /// <remarks>The <see cref = "MaterialObjectWrapper10"/> class checks to determine whether the wrapped material object
    /// supports CAPE-OPEN version 1.1 thermodynamics. This property indicates the result of that check.</remarks>
    /// <value>Indicates whether the wrapped material object supports CAPE-OPEN Thermodynamics version 1.1 interfaces.</value>
    public static bool SupportsThermo11 => false;

    /// <summary> Gets the wrapped Thermo Version 1.0 Material Object.</summary>
    /// <remarks><para>Provides direct access to the Thermo Version 1.0 material object.</para>
    /// <para>The material object exposes the COm version of the ICapeThermoMaterialObject interface.</para></remarks>
    /// <value>The wrapped Thermo Version 1.0 Material Object.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("CapeIdentification")]
    public object MaterialObject10 => _pMaterialObject;


    /// <summary> Gets the wrapped Thermo Version 1.1 Material Object.</summary>
    /// <remarks><para>Provides direct access to the Thermo Version 1.1 material object.</para>
    /// <para>The material object exposes the COM version of the Thermo 1.1 interfaces.</para></remarks>
    /// <value>The wrapped Thermo Version 1.1 Material Object.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("CapeIdentification")]
    public static object MaterialObject11 => null;

    /// <summary>Get the component ids for this MO</summary>
    /// <remarks>Returns the list of components Ids of a given Material Object.</remarks>
    /// <value>Te names of the compounds in the material object.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    string[] ICapeThermoMaterialObject.ComponentIds
    {
        get
        {
            try
            {
                return (string[])_pMaterialObject.ComponentIds;
            }
            catch (Exception pEx)
            {
                throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
            }
        }
    }

    /// <summary>Get the phase ids for this MO</summary>
    /// <remarks>It returns the phases existing in the MO at that moment. The Overall phase 
    /// and multiphase identifiers cannot be returned by this method. See notes on 
    /// Existence of a phase for more information.</remarks>
    /// <value>The phases present in the material.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    string[] ICapeThermoMaterialObject.PhaseIds
    {
        get
        {
            try
            {
                return (string[])_pMaterialObject.PhaseIds;
            }
            catch (Exception pEx)
            {
                throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
            }
        }
    }

    /// <summary>Get some universal constant(s)</summary>
    /// <remarks>Retrieves universal constants from the Property Package.</remarks>
    /// <returns>Values of the requested universal constants.</returns>
    /// <param name = "props">List of universal constants to be retrieved.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    double[] ICapeThermoMaterialObject.GetUniversalConstant(string[] props)
    {
        try
        {
            return (double[])_pMaterialObject.GetUniversalConstant(props);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }

    /// <summary>Get some pure component constant(s)</summary>
    /// <remarks>Retrieve component constants from the Property Package. See Notes for more 
    /// information.</remarks>
    /// <returns>Component Constant values returned from the Property Package for all the 
    /// components in the Material Object It is an Object containing a 1 dimensional 
    /// array of Objects. If we call P to the number of requested properties and C to 
    /// the number requested components the array will contain C*P Objects. The C 
    /// first ones (from position 0 to C-1) will be the values for the first requested 
    /// property (one Object for each component). After them (from position C to 2*C-1) 
    /// there will be the values of constants for the second requested property, and so on.</returns>
    /// <param name = "props">List of component constants.</param>
    /// <param name = "compIds">List of component IDs for which constants are to be retrieved. Use a null value 
    /// for all components in the Material Object.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    object[] ICapeThermoMaterialObject.GetComponentConstant(string[] props, string[] compIds)
    {
        try
        {
            return (object[])_pMaterialObject.GetComponentConstant(props, compIds);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }

    /// <summary>Calculate some properties</summary>
    /// <remarks>This method is responsible for doing all property calculations and delegating 
    /// these calculations to the associated thermo system. This method is further 
    /// defined in the descriptions of the CAPE-OPEN Calling Pattern and the User 
    /// Guide Section. See Notes for a more detailed explanation of the arguments and 
    /// CalcProp description in the notes for a general discussion of the method.</remarks>
    /// <param name = "props">The List of Properties to be calculated.</param>
    /// <param name = "phases">List of phases for which the properties are to be calculated.</param>
    /// <param name = "calcType">Type of calculation: Mixture Property or Pure Component Property. For partial 
    /// property, such as fugacity coefficients of components in a mixture, use 
    /// “Mixture” CalcType. For pure component fugacity coefficients, use “Pure” CalcType.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    void ICapeThermoMaterialObject.CalcProp(string[] props, string[] phases, string calcType)
    {
        try
        {
            _pMaterialObject.CalcProp(props, phases, calcType);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }

    /// <summary>Get some pure component constant(s)</summary>
    /// <remarks>This method is responsible for retrieving the results from calculations from 
    /// the MaterialObject. See Notes for a more detailed explanation of the arguments.</remarks>
    /// <returns>Results vector containing property values in SI units arranged by the defined 
    /// qualifiers. The array is one dimensional containing the properties, in order 
    /// of the "props" array for each of the compounds, in order of the compIds array.</returns>
    /// <param name = "property">The Property for which results are requested from the MaterialObject.</param>
    /// <param name = "phase">The qualified phase for the results.</param>
    /// <param name = "compIds">The qualified components for the results. Use a null value to specify all 
    /// components in the Material Object. For mixture property such as liquid 
    /// enthalpy, this qualifier is not required. Use emptyObject as placeholder.</param>
    /// <param name = "calcType">The qualified type of calculation for the results. (valid Calculation Types: Pure and Mixture)</param>
    /// <param name = "basis">Qualifies the basis of the result (i.e. mass/mole). Default is mole. Use 
    /// NULL for default or as placeholder for property for which basis does not 
    /// an apply (see also Specific properties).</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    double[] ICapeThermoMaterialObject.GetProp(string property,
        string phase, string[] compIds, string calcType, string basis)
    {
        try
        {
            return (double[])_pMaterialObject.GetProp(property, phase, compIds, calcType, basis);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Get some pure component constant(s)</summary>
    /// <remarks>This method is responsible for setting the values for properties of the 
    /// Material Object. See Notes for a more detailed explanation of the arguments.</remarks>
    /// <param name = "property">The Property for which results are requested from the MaterialObject.</param>
    /// <param name = "phase">The qualified phase for the results.</param>
    /// <param name = "compIds">The qualified components for the results. emptyObject to specify all 
    /// components in the Material Object. For mixture property such as liquid 
    /// enthalpy, this qualifier is not required. Use emptyObject as placeholder.</param>
    /// <param name = "calcType">The qualified type of calculation for the results. (valid Calculation Types: 
    /// Pure and Mixture)</param>
    /// <param name = "basis">Qualifies the basis of the result (i.e., mass /mole). Default is mole. Use 
    /// NULL for default or as placeholder for property for which basis does not 
    /// apply (see also Specific properties).</param>
    /// <param name = "values">Values to set for the property.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    void ICapeThermoMaterialObject.SetProp(string property,
        string phase, string[] compIds, string calcType, string basis, double[] values)
    {
        try
        {
            _pMaterialObject.SetProp(property, phase, compIds, calcType, basis, values);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Calculate some equilibrium values</summary>
    /// <remarks>This method is responsible for delegating flash calculations to the 
    /// associated Property Package or Equilibrium Server. It must set the amounts, 
    /// compositions, temperature and pressure for all phases present at equilibrium, 
    /// as well as the temperature and pressure for the overall mixture, if not set 
    /// as part of the calculation specifications. See CalcProp and CalcEquilibrium 
    /// for more information.</remarks>
    /// <param name = "flashType">The type of flash to be calculated.</param>
    /// <param name = "props">Properties to be calculated at equilibrium. emptyObject for no properties. 
    /// If a list, then the property values should be set for each phase present at equilibrium.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    void ICapeThermoMaterialObject.CalcEquilibrium(string flashType, string[] props)
    {
        try
        {
            _pMaterialObject.CalcEquilibrium(flashType, props);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }

    /// <summary>Set the independent variable for the state</summary>
    /// <remarks>Sets the independent variable for a given Material Object.</remarks>
    /// <param name = "indVars">Independent variables to be set (see names for state variables for list of 
    /// valid variables). A System.Object containing a String array marshalled from a COM Object.</param>
    /// <param name = "values">Values of independent variables.
    /// An array of doubles as a System.Object, which is marshalled as an Object 
    /// COM-based CAPE-OPEN.</param>
    void ICapeThermoMaterialObject.SetIndependentVar(string[] indVars, double[] values)
    {
        try
        {
            _pMaterialObject.SetIndependentVar(indVars, values);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }

    /// <summary>Get the independent variable for the state</summary>
    /// <remarks>Sets the independent variable for a given Material Object.</remarks>
    /// <param name = "indVars">Independent variables to be set (see names for state variables for list of 
    /// valid variables).</param>
    /// <returns>Values of independent variables. COM-based CAPE-OPEN.</returns>
    double[] ICapeThermoMaterialObject.GetIndependentVar(string[] indVars)
    {
        try
        {
            return (double[])_pMaterialObject.GetIndependentVar(indVars);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Check a property is valid</summary>
    /// <remarks>Checks to see if given properties can be calculated.</remarks>
    /// <returns>Returns Boolean List associated to list of properties to be checked.</returns>
    /// <param name = "props">Properties to check.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    bool[] ICapeThermoMaterialObject.PropCheck(string[] props)
    {
        try
        {
            return (bool[])_pMaterialObject.PropCheck(props);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Check which properties are available</summary>
    /// <remarks>Gets a list properties that have been calculated.</remarks>
    /// <returns>Properties for which results are available.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    string[] ICapeThermoMaterialObject.AvailableProps()
    {
        try
        {
            return (string[])_pMaterialObject.AvailableProps();
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Remove any previously calculated results for given properties</summary>
    /// <remarks>Remove all or specified property results in the Material Object.</remarks>
    /// <param name = "props">Properties to be removed. emptyObject to remove all properties.</param>
    void ICapeThermoMaterialObject.RemoveResults(string[] props)
    {
        try
        {
            _pMaterialObject.RemoveResults(props);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Create another empty material object</summary>
    /// <remarks>Create a Material Object from the parent Material Template of the current 
    /// Material Object. This is the same as using the CreateMaterialObject method 
    /// on the parent Material Template.</remarks> 
    /// <returns>The created/initialized Material Object.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    ICapeThermoMaterialObject ICapeThermoMaterialObject.CreateMaterialObject()
    {
        try
        {
            return (ICapeThermoMaterialObject)_pMaterialObject.CreateMaterialObject();
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Duplicate this material object</summary>
    /// <remarks>Create a duplicate of the current Material Object.</remarks>
    /// <returns>The created/initialized Material Object.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [DispId(15),Description("Method Duplicate")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    ICapeThermoMaterialObject ICapeThermoMaterialObject.Duplicate()
    {
        try
        {
            return new MaterialObjectWrapper(_pMaterialObject.Duplicate());
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Check the validity of the given properties</summary>
    /// <remarks>Checks the validity of the calculation.</remarks>
    /// <returns>Returns the reliability scale of the calculation.</returns>
    /// <param name = "props">The properties for which reliability is checked. Null value to remove all 
    /// properties.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    ICapeThermoReliability[] ICapeThermoMaterialObject.ValidityCheck(string[] props)
    {
        try
        {
            return (ICapeThermoReliability[])_pMaterialObject.ValidityCheck(props);
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }

    /// <summary>Get the list of properties</summary>
    /// <remarks>Returns list of properties supported by the property package and corresponding 
    /// CO Calculation Routines. The properties TEMPERATURE, PRESSURE, FRACTION, FLOW, 
    /// PHASE FRACTION, TOTAL FLOW cannot be returned by GetPropList, since all 
    /// components must support them. Although the property identifier of derivative 
    /// properties is formed from the identifier of another property, the GetPropList 
    /// method will return the identifiers of all supported derivative and 
    /// non-derivative properties. For instance, a Property Package could return 
    /// the following list: enthalpy, enthalpy.Temperature, entropy, entropy.Pressure.</remarks>
    /// <returns>String list of all supported properties of the property package.</returns>
    string[] ICapeThermoMaterialObject.GetPropList()
    {
        try
        {
            return (string[])_pMaterialObject.GetPropList();
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
    
    /// <summary>Get the number of components in this material object</summary>
    /// <remarks>Returns number of components in Material Object.</remarks>
    /// <returns>Number of components in the Material Object.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    int ICapeThermoMaterialObject.GetNumComponents()
    {
        try
        {
            return _pMaterialObject.GetNumComponents();
        }
        catch (Exception pEx)
        {
            throw COMExceptionHandler.ExceptionForHRESULT(_pMaterialObject, pEx);
        }
    }
}