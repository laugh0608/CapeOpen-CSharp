// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

internal class OptionParameterValueConverter : StringConverter
{

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        var param = (OptionParameter)(context.Instance);
        return new StandardValuesCollection(param.OptionList);
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
        var param = (OptionParameter)context.Instance;
        return param.RestrictedToList;
    }
}

[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeGuids.InOptParSpecEveIid)]  // "991F95FB-2210-4E44-99B3-4AB793FF46C2"
[Description("CapeRealParameterEvents Interface")]
internal interface IOptionParameterSpecEvents
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
    /// Occurs when the user changes of the lower bound of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    void ParameterOptionListChanged(object sender, object args);

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
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    void ParameterRestrictedToListChanged(object sender, object args);

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
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValidatedEventArgs">ParameterValidatedEventArgs</see> that contains information about the event.</param>
    void ParameterValidated(object sender, object args);
}
    
/// <summary>
/// String Parameter class that implements the ICapeParameter and ICapeOptionParameterSpec CAPE-OPEN interfaces.
/// </summary>
/// <remarks>
/// This class implements ICapeParameter, ICapeParameterSpec, ICapeOptionParameterSpec, and ICapeIdentification. 
/// It returns either a string or a System.Object, which is converted to a Variant containing a BSTR by COM Interop.
/// </remarks>
[Serializable]
[ComSourceInterfaces(typeof(IOptionParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeGuids.PpOptionParameterIid)]  // "8EB0F647-618C-4fcc-A16F-39A9D57EA72E"
[ClassInterface(ClassInterfaceType.None)]
public class OptionParameter : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeParameterSpecCOM, ICapeOptionParameterSpec,
    ICapeOptionParameterSpecCOM, ICloneable //, INotifyPropertyChanged
{
    private string _mValue;
    private string _mDefaultValue;
    private string[] _mOptionList;
    private bool _mRestricted;
        
    /// <summary>
    /// Gets and sets the value for this Parameter.
    /// </summary>
    /// <remarks>
    /// This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.
    /// </remarks>
    /// <value>System.Object</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mValue;
        set
        {
            var message = string.Empty;
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            if (!Validate(value.ToString() ?? string.Empty, ref message)) 
                throw new CapeInvalidArgumentException(message, 0);
            _mValue = value.ToString() ?? string.Empty;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));
        }
    }

    /// <summary>
    /// Gets the list of valid values for the parameter if 'RestrictedtoList' public is true.
    /// </summary>
    /// <remarks>
    /// Used in validating the parameter if the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>.
    /// </remarks>
    /// <value>
    /// String array as a System.Object, COM Variant containing a SafeArray of BSTR.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    [Description("Gets and Sets the list of valid values for the parameter if 'RestrictedtoList' public is true.")]
    object ICapeOptionParameterSpecCOM.OptionList => _mOptionList;

    /// <summary>
    /// Constructor for the String-valued parameter
    /// </summary>
    /// <remarks>
    /// This constructor sets the ICapeIdentification.ComponentName of the 
    /// parameter. The parameter's value and default value are set to the value. 
    /// </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the inital value of the parameter.</param>
    public OptionParameter(string name, string value)
        : base(name, string.Empty, CapeParamMode.CAPE_INPUT_OUTPUT)
    {
        _mValue = value;
        Mode = CapeParamMode.CAPE_INPUT_OUTPUT;
        _mDefaultValue = value;
        MValStatus = CapeValidationStatus.CAPE_VALID;
    }
    /// <summary>
    /// Constructor for the boolean-valued parameter
    /// </summary>
    /// <remarks>
    /// This constructor sets the ICapeIdentification.ComponentName and 
    /// ICapeIdentification.ComponentDescription of the 
    /// parameter. The parameter's value and default value are set to the value. 
    /// Additionally, the parameters CapeParameterMode is set.
    /// </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "description">Sets as the ComponentDescription of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the inital value of the parameter.</param>
    /// <param name = "defaultValue">Sets the default value of the parameter.</param>
    /// <param name = "options">String array used as the list acceptable options.</param>
    /// <param name = "restricted">Sets whether the parameter value is restricted to values in the option list.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    public OptionParameter(string name, string description, string value, string defaultValue, string[] options, bool restricted, CapeParamMode mode)
        : base(name, description, mode)
    {
        _mValue = value;
        Mode = mode;
        _mDefaultValue = defaultValue;
        _mOptionList = options;
        _mRestricted = restricted;
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
        return new OptionParameter(ComponentName, ComponentDescription, Value, DefaultValue, OptionList, RestrictedToList, Mode);
    }

    /// <summary>
    /// Occurs when the user changes of the lower bound of the parameter changes.
    /// </summary>
    public event ParameterOptionListChangedHandler ParameterOptionListChanged;
    /// <summary>
    /// Occurs when the user changes of the option list of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterOptionListChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterOptionListChanged</c> in a derived class, be sure to call the base class's <c>OnParameterOptionListChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterOptionListChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterOptionListChanged(ParameterOptionListChangedEventArgs args)
    {
        ParameterOptionListChanged?.Invoke(this, args);
    }

    /// <summary>
    /// Occurs when the user changes of the upper bound of the parameter changes.
    /// </summary>
    public event ParameterRestrictedToListChangedHandler ParameterRestrictedToListChanged;
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
    protected void OnParameterRestrictedToListChanged(ParameterRestrictedToListChangedEventArgs args)
    {
        ParameterRestrictedToListChanged?.Invoke(this, args);
    }

    /// <summary>
    /// Gets and sets the value of the parameter.
    /// </summary>
    /// <remarks>
    /// The value is returned as a String, which marshals as a BSTR to COM.
    /// </remarks>
    /// <returns>
    /// System.String
    /// </returns>
    /// <value>
    /// The value of the parameter.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [TypeConverter(typeof(OptionParameterValueConverter))]
    [Category("ICapeParameter")]
    [Description("Gets and sets the value of the parameter.")]
    public string Value
    {
        [Description("Gets the value of the parameter.")]
        get => _mValue;
        [Description("Sets the value of the parameter.")]
        set
        {
            var message = string.Empty;
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            if (!Validate(value, ref message)) throw new CapeInvalidArgumentException(message, 0);
            _mValue = value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));
        }
    }

    /// <summary>
    /// Validates the current value of the parameter against the parameter's specification.
    /// </summary>
    /// <remarks>
    /// If the value of the <see cref = "RestrictedToList">RestrictedToList</see>
    /// public is set to <c>true</c>, the parameter is valid only if the current value
    /// is included in the <see cref = "OptionList">OptionList</see>. If the 
    /// value of <see cref = "RestrictedToList">RestrictedToList</see> public is <c>false</c>
    /// any valid String is a valid value for the parameter.
    /// </remarks>
    /// <returns>True if the string argument is valid, false if it is not.</returns>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public override bool Validate(ref string message)
    {
        ParameterValidatedEventArgs args;
        if (_mRestricted)
        {
            var inList = false;
            foreach (var mt in _mOptionList)
            {
                if (mt == _mValue)
                {
                    inList = true;
                }
            }
            if (!inList)
            {
                message = "Value is not in the option list.";
                args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, CapeValidationStatus.CAPE_INVALID);
                MValStatus = CapeValidationStatus.CAPE_INVALID;
                NotifyPropertyChanged("ValStatus");
                OnParameterValidated(args);
                return false;
            }
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
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_OPTION;

    //ICapeOptionParameterSpec

    /// <summary>
    /// Gets and Sets the default value of the parameter.
    /// </summary>
    /// <remarks>
    /// Gets and sets the default value of the parameter.
    /// </remarks>
    /// <value>The default value for the parameter. </value>
    [Category("ICapeOptionParameterSpec")]
    [Description("Gets and Sets the default value of the parameter.")]
    public string DefaultValue
    {
        get => _mDefaultValue;
        set
        {
            var message = string.Empty;
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            if (!Validate(value, ref message)) throw new CapeInvalidArgumentException(message, 0);
            _mDefaultValue = value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(DefaultValue));
        }
    }

    /// <summary>
    /// Gets and Sets the list of valid values for the parameter if 'RestrictedtoList' public is true.
    /// </summary>
    /// <remarks>
    /// Used in validating the parameter if the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>.
    /// </remarks>
    /// <value>
    /// The option list.
    /// </value>
    [Category("ICapeOptionParameterSpec")]
    [Description("Gets and Sets the list of valid values for the parameter if 'RestrictedtoList' public is true.")]
    public string[] OptionList
    {
        get => _mOptionList;
        set
        {
            var args = new ParameterOptionListChangedEventArgs(ComponentName);
            _mOptionList = value;
            OnParameterOptionListChanged(args);
            NotifyPropertyChanged(nameof(OptionList));
        }
    }


    /// <summary>
    /// A list of Strings that the valueo f the parameter will be validated against.
    /// </summary>
    /// <remarks>
    /// If <c>true</c>, the parameter's value will be validated against the Strings
    /// in the <see cref = "OptionList">OptionList</see>.
    /// </remarks>
    /// <value>
    /// If <c>true</c>, the parameter's value will be validated against the Strings
    /// in the <see cref = "OptionList">OptionList</see>.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeOptionParameterSpec")]
    [Description("Limits values of the parameter to the values in the option list if true.")]
    public bool RestrictedToList
    {
        get => _mRestricted;
        set
        {
            var args = new ParameterRestrictedToListChangedEventArgs(ComponentName, _mRestricted, value);
            _mRestricted = value;
            OnParameterRestrictedToListChanged(args);
            NotifyPropertyChanged(nameof(RestrictedToList));
        }
    }

    /// <summary>
    /// Validates the value against the parameter's specification.
    /// </summary>
    /// <remarks>
    /// If the value of the <see cref = "RestrictedToList">RestrictedToList</see>
    /// public is set to <c>true</c>, the value to be tested is a valid value for the 
    /// parameter if it is included in the 
    /// <see cref = "OptionList">OptionList</see>. If the 
    /// value of <see cref = "RestrictedToList">RestrictedToList</see> public is <c>false</c>
    /// any valid String is a valid value for the parameter.
    /// </remarks>
    /// <returns>True if the string argument is valid, false if it is not.</returns>
    /// <param name = "pValue">The string to be tested for validity.</param>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Description("Checks whether the value is an accepatble value for the parameter.")]
    public bool Validate(string pValue, ref string message)
    {
        if (_mRestricted)
        {
            var inList = false;
            foreach (var mt in _mOptionList)
            {
                if (mt == pValue)
                {
                    inList = true;
                }
            }
            if (!inList)
            {
                message = "Value is not in the option list.";
                return false;
            }
        }
        message = "Value is valid.";
        return true;
    }
}


/// <summary>
/// Option(string)-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </summary>
/// <remarks>
/// Option(string)-Valued parameter for use in the CAPE-OPEN parameter collection.
/// </remarks>
[Serializable]
[ComSourceInterfaces(typeof(IOptionParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeGuids.OptParaWrapperIid)]  // "70994E8C-179E-40E1-A51B-54A5C0F64A84"
[ClassInterface(ClassInterfaceType.None)]
class OptionParameterWrapper : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeOptionParameterSpec, ICapeOptionParameterSpecCOM,
    ICloneable //, INotifyPropertyChanged
{
    [NonSerialized]
    private ICapeParameter _mParameter;

    /// <summary>
    /// Gets and sets the value for this Parameter.
    /// </summary>
    /// <remarks>
    /// This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.
    /// </remarks>
    /// <value>System.Object</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mParameter.value;
        set
        {
            var message = string.Empty;
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value.ToString() ?? message, value);
            _mParameter.value = value.ToString() ?? message;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));                
        }
    }

    /// <summary>
    /// Gets the list of valid values for the parameter if 'RestrictedtoList' public is true.
    /// </summary>
    /// <remarks>
    /// Used in validating the parameter if the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>.
    /// </remarks>
    /// <value>
    /// String array as a System.Object, COM Variant containing a SafeArray of BSTR.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Browsable(false)]
    [Description("Gets the list of valid values for the parameter if 'RestrictedtoList' public is true.")]
    object ICapeOptionParameterSpecCOM.OptionList => ((ICapeOptionParameterSpecCOM)_mParameter.Specification).OptionList;


    /// <summary>
    /// Constructor for the String-valued parameter
    /// </summary>
    /// <remarks>
    /// This constructor sets the ICapeIdentification.ComponentName of the 
    /// parameter. The parameter's value and default value are set to the value. 
    /// </remarks>
    /// <param name = "parameter">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    public OptionParameterWrapper(ICapeParameter parameter)
        : base(string.Empty, string.Empty, parameter.Mode)
    {
        _mParameter = parameter;
        ComponentName = ((ICapeIdentification)parameter).ComponentName;
        ComponentDescription = ((ICapeIdentification)parameter).ComponentDescription;
        Mode = _mParameter.Mode;
        MValStatus = _mParameter.ValStatus;            
    }        

    /// <summary>
    /// Occurs when the user changes of the lower bound of the parameter changes.
    /// </summary>
    public event ParameterOptionListChangedHandler ParameterOptionListChanged;
    /// <summary>
    /// Occurs when the user changes of the option list of a parameter.
    /// </summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterOptionListChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterOptionListChanged</c> in a derived class, be sure to call the base class's <c>OnParameterOptionListChanged</c> method so that registered 
    /// delegates receive the event.</para>
    /// </remarks>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterOptionListChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterOptionListChanged(ParameterOptionListChangedEventArgs args)
    {
        ParameterOptionListChanged?.Invoke(this, args);
    }

    /// <summary>
    /// Occurs when the user changes of the upper bound of the parameter changes.
    /// </summary>
    public event ParameterRestrictedToListChangedHandler ParameterRestrictedToListChanged;
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
    protected void OnParameterRestrictedToListChanged(ParameterRestrictedToListChangedEventArgs args)
    {
        ParameterRestrictedToListChanged?.Invoke(this, args);
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
        return new OptionParameterWrapper(_mParameter);
    }

    /// <summary>
    /// Gets and sets the value of the parameter.
    /// </summary>
    /// <remarks>
    /// The value is returned as a String, which marshals as a BSTR to COM.
    /// </remarks>
    /// <returns>
    /// System.String
    /// </returns>
    /// <value>
    /// The value of the parameter.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [TypeConverter(typeof(OptionParameterValueConverter))]
    [Category("ICapeParameter")]
    [Description("Gets and sets the value of the parameter.")]
    public string Value
    {
        [Description("Gets the value of the parameter.")]
        get => _mParameter.value.ToString() ?? string.Empty;
        [Description("Sets the value of the parameter.")]
        set
        {
            var message = string.Empty;
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value.ToString() ?? message, value);
            _mParameter.value = value;
            OnParameterValueChanged(args);
            NotifyPropertyChanged(nameof(Value));                
        }
    }
    
    /// <summary>
    /// Validates the current value of the parameter against the parameter's specification.
    /// </summary>
    /// <remarks>
    /// If the value of the <see cref = "RestrictedToList">RestrictedToList</see>
    /// public is set to <c>true</c>, the parameter is valid only if the current value
    /// is included in the <see cref = "OptionList">OptionList</see>. If the 
    /// value of <see cref = "RestrictedToList">RestrictedToList</see> public is <c>false</c>
    /// any valid String is a valid value for the parameter.
    /// </remarks>
    /// <returns>True if the string argument is valid, false if it is not.</returns>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public override bool Validate(ref string message)
    {
        var valid = _mParameter.ValStatus;
        // TODO 这里的 retVal 值和下面 args 传递的两个参数的关系
        // var retVal = _mParameter.Validate(message);
        var args = new ParameterValidatedEventArgs(ComponentName, message, valid, valid);
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
        _mParameter.Reset();
        OnParameterReset(args);
        NotifyPropertyChanged(nameof(Value));
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
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_OPTION;

    //ICapeOptionParameterSpec

    /// <summary>
    /// Gets and Sets the default value of the parameter.
    /// </summary>
    /// <remarks>
    /// Gets and sets the default value of the parameter.
    /// </remarks>
    /// <value>The default value for the parameter. </value>
    [Category("ICapeOptionParameterSpec")]
    [Description("Gets and Sets the default value of the parameter.")]
    public string DefaultValue
    {
        get => ((ICapeOptionParameterSpecCOM)_mParameter.Specification).DefaultValue;
        set
        {
        }
    }

    /// <summary>
    /// Gets and Sets the list of valid values for the parameter if 'RestrictedtoList' public is true.
    /// </summary>
    /// <remarks>
    /// Used in validating the parameter if the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>.
    /// </remarks>
    /// <value>
    /// The option list.
    /// </value>
    [Category("ICapeOptionParameterSpec")]
    [Description("Gets and Sets the list of valid values for the parameter if 'RestrictedtoList' public is true.")]
    public string[] OptionList
    {
        get => (string[])((ICapeOptionParameterSpecCOM)_mParameter.Specification).OptionList;
        set
        {
        }
    }


    /// <summary>
    /// A list of Strings that the valueo f the parameter will be validated against.
    /// </summary>
    /// <remarks>
    /// If <c>true</c>, the parameter's value will be validated against the Strings
    /// in the <see cref = "OptionList">OptionList</see>.
    /// </remarks>
    /// <value>
    /// If <c>true</c>, the parameter's value will be validated against the Strings
    /// in the <see cref = "OptionList">OptionList</see>.
    /// </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Category("ICapeOptionParameterSpec")]
    [Description("Limits values of the parameter to the values in the option list if true.")]
    public bool RestrictedToList
    {
        get => ((ICapeOptionParameterSpecCOM)_mParameter.Specification).RestrictedToList;
        set
        {
        }
    }

    /// <summary>
    /// Validates the value against the parameter's specification.
    /// </summary>
    /// <remarks>
    /// If the value of the <see cref = "RestrictedToList">RestrictedToList</see>
    /// public is set to <c>true</c>, the value to be tested is a valid value for the 
    /// parameter if it is included in the 
    /// <see cref = "OptionList">OptionList</see>. If the 
    /// value of <see cref = "RestrictedToList">RestrictedToList</see> public is <c>false</c>
    /// any valid String is a valid value for the parameter.
    /// </remarks>
    /// <returns>True if the string argument is valid, false if it is not.</returns>
    /// <param name = "pValue">The string to be tested for validity.</param>
    /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    [Description("Checks whether the value is an accepatble value for the parameter.")]
    public bool Validate(string pValue, ref string message)
    {
        return ((ICapeOptionParameterSpecCOM)_mParameter.Specification).Validate(pValue, ref message);
    }
}