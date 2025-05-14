// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>
/// Indicates that the specification of a boolean-valued parameter has been changed.
/// </summary>
/// <remarks>
/// <para>This interface is exposed to COM-based PMEs and serves as a source interface for events associated with changes
/// to the specification of a boolean-valued parameters.</para>
/// <para>This interface is not a part of the CAPE-OPEN specifications. This interface and its implementation is 
/// provided to give COM-based developers similar functionality as .NET-based developers.</para>
/// </remarks>
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeGuids.InCaBoolParaSpecEveIid)]  // "07D17ED3-B25A-48EA-8261-5ED2D076ABDD"
[Description("CapeRealParameterEvents Interface")]
internal interface ICapeBooleanParameterSpecEvents
{
    /// <summary>
    /// Occurs when the user changes of the default value of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterDefaultValueChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterDefaultValueChanged</c> in a derived class, be sure to call the base class's <c>OnParameterDefaultValueChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterDefaultValueChanged">ParameterDefaultValueChanged</see> that contains information about the event.</param>
    void ParameterDefaultValueChanged(object sender, object args);


    /// <summary>
    /// Occurs when a parameter is validated.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterValidated</c> method also allows derived classes to handle the event without 
    /// attaching a delegate. This is the preferred technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValidated</c> in a derived class, be sure to call the base class's 
    /// <c>OnParameterValidated</c> method so that registered delegates receive the event.</para>
    /// </remarks>
    /// <param name = "sender">The <see cref = "ICapeBooleanParameterSpec">ICapeBooleanParameterSpec</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValidatedEventArgs">ParameterValidatedEventArgs</see> that contains information about the event.</param>
    void ParameterValidated(object sender, object args);
}


/// <summary>
/// Boolean-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </summary>
/// <remarks>
/// Boolean-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </remarks>
[Serializable]
[ComSourceInterfaces(typeof(ICapeBooleanParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeGuids.PpBooleanParameterIid)]  // "8B8BC504-EEB5-4a13-B016-9614543E4536"
[ClassInterface(ClassInterfaceType.None)]
public class BooleanParameter : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeParameterSpecCOM,
    ICapeBooleanParameterSpec //, INotifyPropertyChanged
{
    private bool _mValue;
    private bool _mDefaultValue;

    /// <summary>
    /// Gets and sets the value for this Parameter.
    /// </summary>
    /// <remarks>
    /// This value uses the System.Object data type for compatibility with COM-based CAPE-OPEN. The value is marshalled 
    /// to COM as a boolean-valued variant, which is also called a VARIANT_BOOL.
    /// </remarks>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/cc235510.aspx"/>
    /// <seealso href="http://blogs.msdn.com/b/oldnewthing/archive/2004/12/22/329884.aspx"/>
    /// <value>A boxed boolean value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mValue;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, Convert.ToBoolean(value));
            _mValue = Convert.ToBoolean(value);
            OnParameterValueChanged(args);
        }
    }
        
    /// <summary>
    /// Constructor for the boolean-valued parameter
    /// </summary>
    /// <remarks>
    /// This constructor sets the <see cref = "ICapeIdentification.ComponentName"/> of the 
    /// parameter. The parameter's value and default value are set to the value. 
    /// Additionally, the parameters <see cref = "CapeOpen.CapeParamMode"/> is set.
    /// </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the inital and default value of the parameter.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    public BooleanParameter(string name, bool value, CapeParamMode mode)
        : base(name, string.Empty, mode)
    {
        _mValue = value;
        _mDefaultValue = value;
        Mode = mode;            
    }
    
    /// <summary>
    /// Constructor for the boolean-valued parameter
    /// </summary>
    /// <remarks>
    /// This constructor sets the <see cref = "ICapeIdentification.ComponentName"/> and 
    /// <see cref = "ICapeIdentification.ComponentDescription"/> of the 
    /// parameter. The parameter's value and default value are set to the value. 
    /// Additionally, the parameters CapeParamMode is set.
    /// </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "description">Sets as the ComponentDescription of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the inital value of the parameter.</param>
    /// <param name = "defaultValue">Sets the default value of the parameter.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    public BooleanParameter(string name, string description, bool value, bool defaultValue, CapeParamMode mode)
        : base(name, description, mode)
    {
        _mValue = value;
        Mode = mode;
        _mDefaultValue = defaultValue;
        MValStatus = CapeValidationStatus.CAPE_VALID;
    }

    // ICloneable
    /// <summary>
    /// Creates a copy of the parameter.
    /// </summary>
    /// <remarks><para>The clone method is used to create a deep copy of the parameter.</para>
    /// </remarks>
    /// <returns>A copy of the current parameter.</returns>
    public override object Clone()
    {
        return new BooleanParameter(ComponentName, ComponentDescription, _mValue, _mDefaultValue, Mode);
    }

    /// <summary>
    /// Gets and sets the value for this Parameter. 
    /// </summary>
    /// <remarks>
    /// The value of the parameter. This value of the parameter is only available through .NET, adn it is not made 
    /// available to COM.
    /// </remarks>
    /// <value>
    /// The value of the parameter.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeParameter")]
    public bool Value
    {
        get => _mValue;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            _mValue = value;
            OnParameterValueChanged(args);
        }
    }

    /// <summary>
    /// Validates the current value of the parameter against the specification of the parameter.
    /// </summary>
    /// <remarks>
    /// This method checks the current value of the parameter to determine if it is an allowed value. Any valid boolean value (true/false) 
    /// valid for the <see cref = "ICapeBooleanParameterSpec"/> paramaters.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "message">The message is used to return the reason that the parameter is invalid.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    public override bool Validate(ref string message)
    {
        message = "Value is valid.";
        MValStatus = CapeValidationStatus.CAPE_VALID;
        var args = new ParameterValidatedEventArgs(ComponentName, message, CapeValidationStatus.CAPE_VALID, CapeValidationStatus.CAPE_VALID);
        OnParameterValidated(args);
        return true;
    }

    /// <summary>
    /// Sets the value of the parameter to its default value.
    /// </summary>
    /// <remarks>
    /// Sets the value of the parameter to its default value.
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mValue = _mDefaultValue;
        OnParameterReset(args);
    }

    // ICapeParameterSpec
    /// <summary>
    /// Gets the type of the parameter. 
    /// </summary>
    /// <remarks>
    /// Gets the <see cref = "CapeParamType"/> of the parameter for which this is a specification: real 
    /// (CAPE_REAL), integer(CAPE_INT), option(CAPE_OPTION), boolean(CAPE_BOOLEAN) 
    /// or array(CAPE_ARRAY).
    /// </remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_BOOLEAN;

    //ICapeBooleanParameterSpec

    /// <summary>
    /// Gets and sets the default value of the parameter.
    /// </summary>
    /// <remarks>
    /// Gets and sets the default value of the parameter.
    /// </remarks>
    /// <value>
    /// The default value of the parameter.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeBooleanParameterSpec")]
    public bool DefaultValue
    {
        get => _mDefaultValue;
        set
        {
            var args = new ParameterDefaultValueChangedEventArgs(ComponentName, _mDefaultValue, value);
            _mDefaultValue = value;
            OnParameterDefaultValueChanged(args);
        }
    }

    /// <summary>
    /// Validates the value sent against the specification of the parameter.
    /// </summary>
    /// <remarks>
    /// Validates whether the argument is accepted by the parameter as a valid value. 
    /// It returns a flag to indicate the success or failure of the validation together 
    /// with a text message which can be used to convey the reasoning to the client/user.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "pValue">Boolean value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public bool Validate(bool pValue, ref string message)
    {
        message = "Value is valid.";
        return true;
    }
}

/// <summary>
/// Boolean-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </summary>
/// <remarks>
/// Boolean-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </remarks>
[Serializable]
[ComSourceInterfaces(typeof(ICapeBooleanParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeGuids.BooleanParameterWrapIid)]  // "A6751A39-8A2C-4AFC-AD57-6395FFE0A7FE"
[ClassInterface(ClassInterfaceType.None)]
internal class BooleanParameterWrapper : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeBooleanParameterSpec   
    //, INotifyPropertyChanged
{
    private ICapeParameter _mParameter;

    /// <summary>
    /// Gets and sets the value for this Parameter.
    /// </summary>
    /// <remarks>
    /// This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.
    /// </remarks>
    /// <value>A boxed boolean value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mParameter.value;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value, Convert.ToBoolean(value));
            _mParameter.value = value;
            OnParameterValueChanged(args);
        }
    }
        
    /// <summary>
    /// Constructor for the wrapper class for COM-based boolean-valued parameter.
    /// </summary>
    /// <remarks>
    /// This constructor creates an instance of a class that wraps a COM-based boolean-valued paramemter. This 
    /// wrapper exposes appropriate .NET-based parameter interfaces for the wrapped parameter.
    /// </remarks>
    /// <param name = "parameter">The parameter to be wrapped.</param>
    public BooleanParameterWrapper(ICapeParameter parameter)
        : base(string.Empty, string.Empty, parameter.Mode)
    {
        _mParameter = parameter;
        ComponentName = ((ICapeIdentification)parameter).ComponentName;
        ComponentDescription = ((ICapeIdentification)parameter).ComponentDescription;
        Mode = parameter.Mode;
        MValStatus = parameter.ValStatus;
    }
        
    // ICloneable
    /// <summary>
    /// Creates a copy of the parameter.
    /// </summary>
    /// <remarks><para>The clone method is used to create a copy of the parameter. Both the original version 
    /// and the clone refer to the same COM-based parameter.</para>
    /// </remarks>
    /// <returns>A copy of the current parameter.</returns>
    public override object Clone()
    {
        return new BooleanParameterWrapper(_mParameter);
    }

    /// <summary>
    /// Gets and sets the value for this Parameter. 
    /// </summary>
    /// <remarks>
    /// The value of the parameter.
    /// </remarks>
    /// <value>
    /// The value of the parameter.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeParameter")]
    public bool Value
    {
        get => (bool)_mParameter.value;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, (bool)_mParameter.value, value);
            _mParameter.value = value;
            OnParameterValueChanged(args);
        }
    }

    /// <summary>
    /// Validates the current value of the parameter against the 
    /// specification of the parameter.
    /// </summary>
    /// <remarks>
    /// This method checks the current value of the parameter to determine if it is an allowed value. Any valid 
    /// boolean value (true/false) is valid for the <see cref = "ICapeBooleanParameterSpec"/> paramaters.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "message">The message is used to return the reason that the parameter is invalid.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    public override bool Validate(ref string message)
    {
        var valid = _mParameter.ValStatus;
        var retVal = _mParameter.Validate(message);
        var args = new ParameterValidatedEventArgs(ComponentName, message, valid, _mParameter.ValStatus);
        OnParameterValidated(args);
        return retVal;
    }

    /// <summary>
    /// Sets the value of the parameter to its default value.
    /// </summary>
    /// <remarks>
    /// Sets the value of the parameter to its default value.
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mParameter.Reset();
        OnParameterReset(args);
    }

    // ICapeParameterSpec
    /// <summary>
    /// Gets the type of the parameter. 
    /// </summary>
    /// <remarks>
    /// Gets the <see cref = "CapeParamType"/> of the parameter.
    /// </remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_BOOLEAN;

    //ICapeBooleanParameterSpec

    /// <summary>
    /// Gets the default value of the wrapped parameter.
    /// </summary>
    /// <remarks>
    /// The COM-based <see cref="ICapeBooleanParameterSpec"/> boolean interface does not provide a means to change the 
    /// default value of the parameter. As such, the default value of the wrapped parameter can not be changed.
    /// </remarks>
    /// <value>
    /// The default value of the parameter.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeBooleanParameterSpec")]
    public bool DefaultValue
    {
        get => ((ICapeBooleanParameterSpec)_mParameter.Specification).DefaultValue;
        set
        {
        }
    }

    /// <summary>
    /// Validates the value sent against the specification of the parameter.
    /// </summary>
    /// <remarks>
    /// Validates whether the argument is accepted by the parameter as a valid value. 
    /// It returns a flag to indicate the success or failure of the validation together 
    /// with a text message which can be used to convey the reasoning to the client/user.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "pValue">Boolean value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public bool Validate(bool pValue, ref string message)
    {
        return ((ICapeBooleanParameterSpec)_mParameter.Specification).Validate(pValue, message);
    }
}
