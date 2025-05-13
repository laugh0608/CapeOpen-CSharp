// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>
/// Indicates that the specification of an interger-valued parameter has been changed.
/// </summary>
/// <remarks>
/// <para>This interface is exposed to COM-based PMEs and serves as a source interface for events associated with changes
/// to the specification of an integer-valued parameters.</para>
/// <para>This interface is not a part of the CAPE-OPEN specifications. This interface and its implementation is 
/// provided to give COM-based developers similar functionality as .NET-based developers.</para>
/// </remarks>
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeOpenGuids.InCapeIntParaSpecEveIid)]  // "2EA7C47A-A4E0-47A2-8AC1-658F96A0B79D"
[Description("CapeIntegerParameterEvents Interface")]
internal interface ICapeIntegerParameterSpecEvents
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
    /// <param name = "sender">The <see cref = "IntegerParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterDefaultValueChanged">ParameterDefaultValueChanged</see> that contains information about the event.</param>
    void ParameterDefaultValueChanged(
        [MarshalAs(UnmanagedType.IDispatch)]object sender, 
        [MarshalAs(UnmanagedType.IDispatch)]object args
    );

    /// <summary>
    /// Occurs when the user changes of the lower bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "sender">The <see cref = "IntegerParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    void ParameterLowerBoundChanged(
        [MarshalAs(UnmanagedType.IDispatch)]object sender, 
        [MarshalAs(UnmanagedType.IDispatch)]object args
    );

    /// <summary>
    /// Occurs when the user changes of the upper bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterUpperBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterUpperBoundChanged</c> in a derived class, be sure to call the base class's <c>OnParameterUpperBoundChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "sender">The <see cref = "IntegerParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    void ParameterUpperBoundChanged(object sender, object args);

    /// <summary>
    /// Occurs when a parameter is validated.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterValidated</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValidated</c> in a derived class, be sure to call the base class's <c>OnParameterValidated</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "sender">The <see cref = "IntegerParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValidatedEventArgs">ParameterValidatedEventArgs</see> that contains information about the event.</param>
    void ParameterValidated(object sender, object args);
}


/// <summary>
/// Intger-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </summary>
/// <remarks>
/// Intger-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </remarks>
[Serializable]
[ComSourceInterfaces(typeof(ICapeIntegerParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeOpenGuids.PpIntegerParameterIid)]  // "2C57DC9F-1368-42eb-888F-5BC6ED7DDFA7"
[ClassInterface(ClassInterfaceType.None)]
public class IntegerParameter : CapeParameter, ICapeParameter, ICapeParameterSpec,
    ICapeParameterSpecCOM, ICapeIntegerParameterSpec //, INotifyPropertyChanged
{

    private int _mValue;
    private int _mDefaultValue, _mLowerBound, _mUpperBound;
        
    /// <summary>
    /// Gets and sets the value for this Parameter.
    /// </summary>
    /// <remarks>
    /// This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.
    /// </remarks>
    /// <value>The value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mValue;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            _mValue = (int)value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));
        }
    }

    /// <summary>
    /// Creates a new instance of the integer-valued parameter class.
    /// </summary>
    /// <remarks>
    /// <para>The default value is set to the inital value of the parameter. The upper
    /// bound is set to Int32.MaxValue (2,147,483,647) and the lower bound is set to 
    /// Int32.MinValue (-2,147,483,648). The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT.</para>
    /// </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the inital value of the parameter.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter</param>
    public IntegerParameter(string name, int value, CapeParamMode mode)
        : base(name, string.Empty, mode)
    {
        _mValue = value;
        Mode = mode;
        _mLowerBound = int.MinValue;
        _mUpperBound = int.MaxValue;
        _mDefaultValue = value;
    }
    /// <summary>
    /// Creates a new instance of the integer-valued parameter class using the values enterred. 
    /// </summary>
    /// <remarks>
    /// The default value, upper and lower 
    /// bound, as well as the mode of the parameter are specified in this constructor.
    /// </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "description">Sets as the ComponentDescription of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the inital value of the parameter.</param>
    /// <param name = "defaultValue">Sets the default value of the parameter.</param>
    /// <param name = "minValue">Sets the lower bound of the parameter.</param>
    /// <param name = "maxValue">Sets the upper bound of the parameter.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    public IntegerParameter(string name, string description, int value, int defaultValue, int minValue, int maxValue, CapeParamMode mode)
        : base(name, description, mode)
    {
        _mValue = value;
        Mode = mode;
        _mLowerBound = minValue;
        _mUpperBound = maxValue;
        _mDefaultValue = defaultValue;
        var message = "";
        if (!Validate(ref message))
        {
            MessageBox.Show(message, string.Concat("Invalid Parameter Value: ", ComponentName));
        }
    }

    /// <summary>
    /// Occurs when the user changes of the lower bound of the parameter changes.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// </remarks>
    public event ParameterLowerBoundChangedHandler ParameterLowerBoundChanged;
    /// <summary>
    /// Occurs when the user changes of the lower bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterLowerBoundChanged(ParameterLowerBoundChangedEventArgs args)
    {
        ParameterLowerBoundChanged?.Invoke(this, args);
    }

    /// <summary>
    /// Occurs when the user changes of the upper bound of the parameter changes.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// </remarks>
    public event ParameterUpperBoundChangedHandler ParameterUpperBoundChanged;
    /// <summary>
    /// Occurs when the user changes of the upper bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterUpperBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterUpperBoundChanged</c> in a derived class, be sure to call the base class's <c>OnParameterUpperBoundChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterUpperBoundChanged(ParameterUpperBoundChangedEventArgs args)
    {
        ParameterUpperBoundChanged?.Invoke(this, args);
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
        return new IntegerParameter(ComponentName, ComponentDescription, _mValue, _mDefaultValue, _mLowerBound, _mUpperBound, Mode);
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
    public int Value
    {
        get => _mValue;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            _mValue = value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));
        }
    }

    /// <summary>
    /// Validates the current value of the parameter against the 
    /// specification of the parameter.
    /// </summary>
    /// <remarks>
    /// The parameter is considered valid if the current value is between the 
    /// upper and lower bound. The message is used to return the reason that 
    /// the parameter is invalid.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public override bool Validate(ref string message)
    {
        ParameterValidatedEventArgs args;
        if (_mValue < _mLowerBound)
        {
            message = "Value below the Lower Bound.";
            args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, CapeValidationStatus.CAPE_INVALID);
            MValStatus = CapeValidationStatus.CAPE_INVALID;
            NotifyPropertyChanged("ValStatus");
            OnParameterValidated(args);
            return false;
        }
        if (_mValue > _mUpperBound)
        {
            message = "Value greater than upper bound.";
            args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, CapeValidationStatus.CAPE_INVALID);
            MValStatus = CapeValidationStatus.CAPE_INVALID;
            NotifyPropertyChanged("ValStatus");
            OnParameterValidated(args);
            return false;
        }
        message = "Value is valid.";
        args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, CapeValidationStatus.CAPE_VALID);
        MValStatus = CapeValidationStatus.CAPE_VALID;
        NotifyPropertyChanged("ValStatus");
        OnParameterValidated(args);
        return true;
    }

    /// <summary>
    /// Sets the value of the parameter to its default value.
    /// </summary>
    /// <remarks>
    ///  This method sets the parameter's value to the default value.
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mValue = _mDefaultValue;
        OnParameterReset(args);
        NotifyPropertyChanged(nameof(Value));
    }

    // ICapeParameterSpec
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
    public override CapeParamType Type => CapeParamType.CAPE_INT;

    //ICapeIntegerParameterSpec

    /// <summary>
    /// Gets and sets the default value of the parameter.
    /// </summary>
    /// <remarks>
    /// Gets and sets the default value of the parameter.
    /// </remarks>
    /// <value>The default value for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeIntegerParameterSpec")]
    public int DefaultValue
    {
        get => _mDefaultValue;
        set
        {
            var args = new ParameterDefaultValueChangedEventArgs(ComponentName, _mDefaultValue, value);
            _mDefaultValue = value;
            OnParameterDefaultValueChanged(args);
            NotifyPropertyChanged(nameof(DefaultValue));
        }
    }

    /// <summary>
    /// Gets and sets the lower bound of the parameter.
    /// </summary>
    /// <remarks>
    /// The lower bound can be an valid integer. By default, it is set to 
    /// Int32.MinValue, 2,147,483,648; that is, hexadecimal 0x80000000
    /// </remarks>
    /// <value>The lower bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeIntegerParameterSpec")]
    public int LowerBound
    {
        get => _mLowerBound;
        set
        {
            var args = new ParameterLowerBoundChangedEventArgs(ComponentName, _mLowerBound, value);
            _mLowerBound = value;
            OnParameterLowerBoundChanged(args);
            NotifyPropertyChanged(nameof(LowerBound));
        }
    }

    /// <summary>
    /// Gets and sets the upper bound of the parameter.
    /// </summary>
    /// <remarks>
    /// The lower bound can be an valid integer. By default, it is set to 
    /// Int32.MaxValue, 2,147,483,647; that is, hexadecimal 0x7FFFFFFF.
    /// </remarks>
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeIntegerParameterSpec")]
    public int UpperBound
    {
        get => _mUpperBound;
        set
        {
            var args = new ParameterUpperBoundChangedEventArgs(ComponentName, _mUpperBound, value);
            _mUpperBound = value;
            OnParameterUpperBoundChanged(args);
            NotifyPropertyChanged(nameof(UpperBound));
        }
    }

    /// <summary>
    /// Validates the value sent against the specification of the parameter. 
    /// </summary>
    /// <remarks>
    /// The parameter is considered valid if the current value is between 
    /// the upper and lower bound. The message is used to return the reason 
    /// that the parameter is invalid.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "pValue">Integer value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public bool Validate(int pValue, ref string message)
    {
        if (pValue < _mLowerBound)
        {
            message = "Value below the Lower Bound.";
            return false;
        }
        if (pValue > _mUpperBound)
        {
            message = "Value greater than upper bound.";
            return false;
        }
        message = "Value is valid.";
        return true;
    }
}

/// <summary>
/// Integer-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </summary>
/// <remarks>
/// Integer-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </remarks>
[Serializable]
[ComVisible(true)]
[ComSourceInterfaces(typeof(ICapeIntegerParameterSpecEvents))]
[Guid(CapeOpenGuids.IntegerParaWrapperIid)]  // ICapeThermoMaterialObject_IID "EFC01B53-9A6A-4AD9-97BE-3F0294B3BBFB"
[ClassInterface(ClassInterfaceType.None)]
internal class IntegerParameterWrapper : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeIntegerParameterSpec, ICloneable //, INotifyPropertyChanged
{
    private ICapeParameter _mParameter;

    /// <summary>
    /// Gets and sets the value for this Parameter.
    /// </summary>
    /// <remarks>
    /// This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.
    /// </remarks>
    /// <value>The value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mParameter.value;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value, value);
            _mParameter.value = (int)value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));
        }
    }

    /// <summary>
    /// Creates a new instance of the integer-valued parameter class.
    /// </summary>
    /// <remarks>
    /// <para>The default value is set to the inital value of the parameter. The upper
    /// bound is set to Int32.MaxValue (2,147,483,647) and the lower bound is set to 
    /// Int32.MinValue (-2,147,483,648). The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT.</para>
    /// </remarks>
    /// <param name = "parameter">Sets the inital value of the parameter.</param>
    public IntegerParameterWrapper(ICapeParameter parameter)
        : base(string.Empty, string.Empty, parameter.Mode)
    {
        _mParameter = parameter;
        ComponentName = ((ICapeIdentification)parameter).ComponentName;
        ComponentDescription = ((ICapeIdentification)parameter).ComponentDescription;
        Mode = parameter.Mode;
        MValStatus = parameter.ValStatus;
    }


    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <remarks>
    /// <para>
    /// Clone can be implemented either as a deep copy or a shallow copy. In a deep copy, all objects are duplicated; 
    /// in a shallow copy, only the top-level objects are duplicated and the lower levels contain references.
    /// </para>
    /// <para>
    /// The resulting clone must be of the same type as, or compatible with, the original instance.
    /// </para>
    /// <para>
    /// See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples.
    /// </para>
    /// </remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public override object Clone()
    {
        return new IntegerParameter(ComponentName, ComponentDescription, Value, DefaultValue, LowerBound, UpperBound, Mode);
    }

    /// <summary>
    /// Occurs when the user changes of the lower bound of the parameter changes.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// </remarks>
    public event ParameterLowerBoundChangedHandler ParameterLowerBoundChanged;
    /// <summary>
    /// Occurs when the user changes of the lower bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterLowerBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterLowerBoundChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterLowerBoundChanged(ParameterLowerBoundChangedEventArgs args)
    {
        ParameterLowerBoundChanged?.Invoke(this, args);
    }

    /// <summary>
    /// Occurs when the user changes of the upper bound of the parameter changes.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// </remarks>
    public event ParameterUpperBoundChangedHandler ParameterUpperBoundChanged;
    /// <summary>
    /// Occurs when the user changes of the upper bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterUpperBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterUpperBoundChanged</c> in a derived class, be sure to call the base class's <c>OnParameterUpperBoundChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterUpperBoundChanged(ParameterUpperBoundChangedEventArgs args)
    {
        ParameterUpperBoundChanged?.Invoke(this, args);
    }
    // ICloneable
    /// <summary>
    /// Creates a copy of the parameter.
    /// </summary>
    /// <remarks><para>The clone method is used to create a deep copy of the parameter.</para>
    /// </remarks>
    /// <returns>A copy of the current parameter.</returns>
    object ICloneable.Clone()
    {
        return new IntegerParameterWrapper(_mParameter);
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
    public int Value
    {
        get => (int)_mParameter.value;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value, value);
            _mParameter.value = value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged("Value");
        }
    }

    /// <summary>
    /// Validates the current value of the parameter against the 
    /// specification of the parameter.
    /// </summary>
    /// <remarks>
    /// The parameter is considered valid if the current value is between the 
    /// upper and lower bound. The message is used to return the reason that 
    /// the parameter is invalid.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public override bool Validate(ref string message)
    {
        var valStatus = _mParameter.ValStatus;
        var retVal = _mParameter.Validate(message);
        var args = new ParameterValidatedEventArgs(ComponentName, message, ValStatus, valStatus);
        OnParameterValidated(args);
        NotifyPropertyChanged("ValStatus");
        return retVal;
    }

    /// <summary>
    /// Sets the value of the parameter to its default value.
    /// </summary>
    /// <remarks>
    ///  This method sets the parameter's value to the default value.
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mParameter.Reset();
        OnParameterReset(args);
        NotifyPropertyChanged(nameof(Value));
    }

    // ICapeParameterSpec
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
    public override CapeParamType Type => CapeParamType.CAPE_INT;

    //ICapeIntegerParameterSpec

    /// <summary>
    /// Gets and sets the default value of the parameter.
    /// </summary>
    /// <remarks>
    /// Gets and sets the default value of the parameter.
    /// </remarks>
    /// <value>The default value for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeIntegerParameterSpec")]
    public int DefaultValue
    {
        get => ((ICapeIntegerParameterSpec)_mParameter.Specification).DefaultValue;
        set { }
    }

    /// <summary>
    /// Gets and sets the lower bound of the parameter.
    /// </summary>
    /// <remarks>
    /// The lower bound can be an valid integer. By default, it is set to 
    /// Int32.MinValue, 2,147,483,648; that is, hexadecimal 0x80000000
    /// </remarks>
    /// <value>The lower bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeIntegerParameterSpec")]
    public int LowerBound
    {
        get => ((ICapeIntegerParameterSpec)_mParameter.Specification).LowerBound;
        set { }
    }

    /// <summary>
    /// Gets and sets the upper bound of the parameter.
    /// </summary>
    /// <remarks>
    /// The lower bound can be an valid integer. By default, it is set to 
    /// Int32.MaxValue, 2,147,483,647; that is, hexadecimal 0x7FFFFFFF.
    /// </remarks>
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeIntegerParameterSpec")]
    public int UpperBound
    {
        get => ((ICapeIntegerParameterSpec)_mParameter.Specification).UpperBound;
        set { }
    }

    /// <summary>
    /// Validates the value sent against the specification of the parameter. 
    /// </summary>
    /// <remarks>
    /// The parameter is considered valid if the current value is between 
    /// the upper and lower bound. The message is used to return the reason 
    /// that the parameter is invalid.
    /// </remarks>
    /// <returns>
    /// True if the parameter is valid, false if not valid.
    /// </returns>
    /// <param name = "pValue">Integer value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public bool Validate(int pValue, ref string message)
    {
        return ((ICapeIntegerParameterSpec)_mParameter.Specification).Validate(pValue, message);
    }
}
