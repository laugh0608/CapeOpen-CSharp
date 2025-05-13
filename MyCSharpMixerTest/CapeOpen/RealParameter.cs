// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CapeOpen;

internal class UnitConverter : StringConverter
{
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        var unit = (RealParameter)context.Instance;
        var retVal = CDimensions.UnitsMatchingCategory(CDimensions.UnitCategory(unit.Unit));
        return new StandardValuesCollection(retVal);
    }
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
        return true;
    }
}

internal class RealParameterTypeConverter : ParameterTypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return typeof(RealParameter).IsAssignableFrom(destinationType) 
               || base.CanConvertTo(context, destinationType);
    }
    public override object ConvertTo(ITypeDescriptorContext context, 
        CultureInfo culture, object value, Type destinationType)
    {
        if (!typeof(string).IsAssignableFrom(destinationType) 
            // || !typeof(RealParameter).IsAssignableFrom(value.GetType()))
            || value is not RealParameter pRealParameter)
            return base.ConvertTo(context, culture, value, destinationType);
        return string.Concat(pRealParameter.DimensionedValue.ToString(), " ", pRealParameter.Unit);
    }
}

[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeOpenGuids.InRealPaSpecEveIid)]  // "058B416C-FC61-4E64-802A-19070CB39703"
[Description("CapeRealParameterEvents Interface")]
internal interface IRealParameterSpecEvents
{
    /// <summary>Occurs when the user changes of the default value of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterDefaultValueChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterDefaultValueChanged</c> in a derived class, be sure to call the base class's <c>OnParameterDefaultValueChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterDefaultValueChanged">ParameterDefaultValueChanged</see> that contains information about the event.</param>
    void ParameterDefaultValueChanged(object sender, object args);

    /// <summary>Occurs when the user changes of the lower bound of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    void ParameterLowerBoundChanged(object sender, object args);

    /// <summary>Occurs when the user changes of the upper bound of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterUpperBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterUpperBoundChanged</c> in a derived class, be sure to call the base class's <c>OnParameterUpperBoundChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    void ParameterUpperBoundChanged(object sender, object args);

    /// <summary>Occurs when a parameter is validated.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterValidated</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValidated</c> in a derived class, be sure to call the base class's <c>OnParameterValidated</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValidatedEventArgs">ParameterValidatedEventArgs</see> that contains information about the event.</param>
    void ParameterValidated(object sender, object args);
}

/// <summary>Real-Valued parameter for use in the CAPE-OPEN parameter collection.</summary>
[Serializable]
[ComSourceInterfaces(typeof(IParameterEvents), typeof(IRealParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeOpenGuids.PpRealParTerIid)]  //ICapeThermoMaterialObject_IID  "77E39C43-046B-4b1f-9EE0-AA9EFC55D2EE"
[ClassInterface(ClassInterfaceType.None)]
[TypeConverter(typeof(RealParameterTypeConverter))]
public class RealParameter : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeParameterSpecCOM,
    ICapeRealParameterSpec, ICapeRealParameterSpecCOM, IATCapeXRealParameterSpec
{
    private double _mValue;
    private double _mDefaultValue;
    private double _mLowerBound;
    private double _mUpperBound;
    //private String m_Category = String.Empty;
    //private String m_Unit = String.Empty;
    private string _mUnit;

    /// <summary>Creates a new instance of the double precision-valued parameter class. </summary>
    /// <remarks>The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT. The dimensionality 
    /// of the parameter is determined from the unit string.</remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "description">Sets as the ComponentDescription of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the initial value of the parameter.</param>
    /// <param name = "defaultValue">Sets the default value of the parameter.</param>
    /// <param name = "lowerBound">Sets the lower bound of the parameter.</param>
    /// <param name = "upperBound">Sets the upper bound of the parameter.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    /// <param name = "unit">Use to Set the dimensionality of the parameter.</param>
    public RealParameter(string name, string description, double value, 
        double defaultValue, double lowerBound, double upperBound, CapeParamMode mode, string unit)
        : base(name, description, mode)
    {
        _mValue = value;
        _mDefaultValue = defaultValue;
        MValStatus = CapeValidationStatus.CAPE_VALID;
        _mUnit = unit;
        _mLowerBound = lowerBound;
        _mUpperBound = upperBound;
    }
    
    /// <summary>Creates a new instance of the double precision-valued parameter class. </summary>
    /// <remarks>The upper bound is set to Double.MaxValue (1.79769313486232e308) and the 
    /// lower bound is set to Double.MinValue (negative 1.79769313486232e308). 
    /// The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT. 
    /// The dimensionality of the parameter is determined from the unit string.</remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "description">Sets as the ComponentDescription of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the initial value of the parameter.</param>
    /// <param name = "defaultValue">Sets the default value of the parameter.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    /// <param name = "unit">Use to Set the dimensionality of the parameter.</param>
    public RealParameter(string name, string description, double value, 
        double defaultValue, CapeParamMode mode, string unit)
        : base(name, description, mode)
    {
        _mValue = value;
        _mDefaultValue = defaultValue;
        Mode = mode;
        MValStatus = CapeValidationStatus.CAPE_VALID;
        _mUnit = unit;
        _mLowerBound = double.MinValue;
        _mUpperBound = double.MaxValue;
    }

    /// <summary>Creates a new instance of the double precision-valued parameter class. </summary>
    /// <remarks>The upper bound is set to Double.MaxValue (1.79769313486232e308) and the 
    /// lower bound is set to Double.MinValue (negative 1.79769313486232e308). 
    /// The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT. 
    /// The dimensionality of the parameter is determined from the unit string.</remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "value">Sets the initial value of the parameter.</param>
    /// <param name = "defaultValue">Sets the default value of the parameter.</param>
    /// <param name = "unit">Use to Set the dimensionality of the parameter.</param>
    public RealParameter(string name, double value, double defaultValue, string unit)
        : base(name, "", CapeParamMode.CAPE_INPUT_OUTPUT)
    {
        _mValue = value;
        _mDefaultValue = defaultValue;
        MValStatus = CapeValidationStatus.CAPE_VALID;
        _mUnit = unit;
        _mLowerBound = double.MinValue;
        _mUpperBound = double.MaxValue;
    }

    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <remarks><para>Clone can be implemented either as a deep copy or a shallow copy. In a deep copy, all objects are duplicated; 
    /// in a shallow copy, only the top-level objects are duplicated and the lower levels contain references.</para>
    /// <para>The resulting clone must be of the same type as, or compatible with, the original instance.</para>
    /// <para>See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples.</para></remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public override object Clone()
    {
        return new RealParameter(ComponentName, ComponentDescription, DimensionedValue, 
            DimensionedDefaultValue, DimensionedLowerBound, DimensionedUpperBound, Mode, Unit);
    }

    /// <summary>Gets the dimensionality of the parameter.</summary>
    /// <remarks><para>Gets the dimensionality of the parameter for which this is the 
    /// specification. The dimensionality represents the physical dimensional 
    /// axes of this parameter. It is expected that the dimensionality must cover 
    /// at least 6 fundamental axes (length, mass, time, angle, temperature and 
    /// charge). A possible implementation could consist in being a constant 
    /// length array vector that contains the exponents of each basic SI unit, 
    /// following directives of SI-brochure (from http://www.bipm.fr/). So if we 
    /// agree on order &lt;m kg s A K,&gt; ... velocity would be 
    /// &lt;1,0,-1,0,0,0&gt;: that is m1 * s-1 =m/s. We have suggested to the 
    /// CO Scientific Committee to use the SI base units plus the SI derived units 
    /// with special symbols (for a better usability and for allowing the 
    /// definition of angles).</para></remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    object ICapeParameterSpecCOM.Dimensionality => CDimensions.Dimensionality(_mUnit);
    
    /// <summary>Gets the dimensionality of the parameter.</summary>
    /// <remarks><para>Gets the dimensionality of the parameter for which this is the 
    /// specification. The dimensionality represents the physical dimensional 
    /// axes of this parameter. It is expected that the dimensionality must cover 
    /// at least 6 fundamental axes (length, mass, time, angle, temperature and 
    /// charge). A possible implementation could consist in being a constant 
    /// length array vector that contains the exponents of each basic SI unit, 
    /// following directives of SI-brochure (from http://www.bipm.fr/). So if we 
    /// agree on order &lt;m kg s A K,&gt; ... velocity would be 
    /// &lt;1,0,-1,0,0,0&gt;: that is m1 * s-1 =m/s. We have suggested to the 
    /// CO Scientific Committee to use the SI base units plus the SI derived units 
    /// with special symbols (for a better usability and for allowing the 
    /// definition of angles).</para></remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public override double[] Dimensionality => CDimensions.Dimensionality(_mUnit);

    /// <summary>Gets and sets the value for this Parameter.</summary>
    /// <remarks>This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.</remarks>
    /// <returns>A boxed boolean value of the parameter.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mValue;
        set {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            var message = string.Empty;
            if (SIValidate((double)value, ref message)) {
                _mValue = (double)value;
                NotifyPropertyChanged("Value");
                OnParameterValueChanged(args);
            } else {
                throw new CapeInvalidArgumentException(message, 1);
            }
        }
    }
    
    /// <summary>Validates the pValue sent against the specification of the parameter.</summary>
    /// <remarks>The pValue is considered valid if it is between the upper and lower 
    /// bound of the parameter. The message is used to return the reason that 
    /// the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "pValue">The name of the unit that the pValue should be converted to.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    bool ICapeRealParameterSpecCOM.Validate(double pValue, ref string message)
    {
        return SIValidate(pValue, ref message);
    }
        
    /// <summary>Returns the current value of the parameter in the dimensional unit
    /// specified.</summary>
    /// <remarks>The value of the parameter in the unit requested. The unit must be a valid
    /// parameter. For example, if the parameter was set as 101325 Pa, if the desiredUnit was "atm" the return
    /// value would be 1 (the value of 101325 Pa in atmospheres).</remarks>
    /// <returns>The value of the parameter in the requested unit of measurement.</returns>
    /// <param name = "desiredUnit">The unit that the parameter is desired to be reported in.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised unit identifier.</exception>
    public double ConvertTo(string desiredUnit)
    {
        var bUnitExisting = false;
        double pFactor = 1;
        double mFactor = 0;
        var retVal = _mValue;
        // if ((desiredUnit != null) || (desiredUnit == String.Empty))
        if (desiredUnit is not null or "")
        {
            var units = CDimensions.Units;
            if (units.Any(mt => mt == desiredUnit))
            {
                pFactor = CDimensions.ConversionsTimes(desiredUnit);
                mFactor = CDimensions.ConversionsPlus(desiredUnit);
                bUnitExisting = true;
            }
        }
        if (!bUnitExisting)
            MessageBox.Show("There is no unit named" + desiredUnit, "Unit Warning!");
        else
        {
            retVal = (Convert.ToDouble(retVal) - mFactor) / pFactor;
            _mUnit = desiredUnit;
        }
        return retVal;
    }

    /// <summary>Returns the pValue in the SI unit of the specified unit.</summary>
    /// <remarks>The pValue is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// pValue would be 101325 (the pValue of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <returns>The pValue of the parameter in SI units.</returns>
    /// <param name = "pValue">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <param name = "unit">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised unit identifier.</exception>
    public double ConvertToSI(double pValue, string unit)
    {
        var bUnitExisting = false;
        double pFactor = 1;
        double mFactor = 0;
        var retVal = pValue;
        // if (unit != null || unit == string.Empty)
        if (unit is not null or "")
        {
            var units = CDimensions.Units;
            if (units.Any(mt => mt == unit))
            {
                pFactor = CDimensions.ConversionsTimes(unit);
                mFactor = CDimensions.ConversionsPlus(unit);
                bUnitExisting = true;
            }
        }
        if (!bUnitExisting)
            MessageBox.Show("There is no unit named" + unit, "Unit Warning!");
        else
        {
            retVal = retVal * pFactor + mFactor;
        }
        return retVal;
    }

    /// <summary>Gets and sets the dimensional unit for the parameter.</summary>
    /// <remarks>The value is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// value would be 101325 (the value of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <value>The value of the parameter in SI units.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised unit identifier.</exception>
    [Browsable(true),Category("Value")]
    [TypeConverter(typeof(UnitConverter))]
    [Description("Dimensional unit of the parameter.")]
    public string Unit
    {
        get => _mUnit;
        set {
            _mUnit = value;
            //m_Category = CapeOpen.CDimensions.UnitCategory(m_unit); 
            NotifyPropertyChanged(nameof(Unit));
        }
    }

    /// <summary>Occurs when the user changes of the lower bound of the parameter changes.</summary>
    public event ParameterLowerBoundChangedHandler ParameterLowerBoundChanged;
    
    /// <summary>Occurs when the user changes of the lower bound of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterLowerBoundChanged(ParameterLowerBoundChangedEventArgs args)
    {
        ParameterLowerBoundChanged?.Invoke(this, args);
    }

    /// <summary>Occurs when the user changes of the upper bound of the parameter changes.</summary>
    public event ParameterUpperBoundChangedHandler ParameterUpperBoundChanged;
    
    /// <summary>Occurs when the user changes of the upper bound of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterUpperBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterUpperBoundChanged</c> in a derived class, be sure to call the base class's <c>OnParameterUpperBoundChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterUpperBoundChanged(ParameterUpperBoundChangedEventArgs args)
    {
        ParameterUpperBoundChanged?.Invoke(this, args);
    }

    /// <summary>Gets and sets the parameter value in the SI unit for its current dimensionality.</summary>
    /// <remarks>The value is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// value would be 101325 (the value of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <value>The value of the parameter in SI units.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(true),Category("Value")]
    public double SIValue
    {
        get => _mValue;
        set {
            var message = string.Empty;
            if (!SIValidate(value, ref message)) return;
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, value);
            OnParameterValueChanged(args);
            _mValue = value;
            NotifyPropertyChanged("Value");
        }
    }

    /// <summary>Gets and sets the parameter value in the current unit for its dimensionality.</summary>
    /// <remarks>The value is returned in the parameter's current unit. For example, if the 
    /// parameter was set as 101325 Pa and the unit was changed to atm, 
    /// the return value would be 1 (the value of 101325 Pa in atmospheres).</remarks>
    /// <value>The value of the parameter in the current unit.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(true),Category("Value")]
    public double DimensionedValue
    {
        get => ConvertTo(Unit);
        set
        {
            var newValue = ConvertToSI(value, Unit);
            var args = new ParameterValueChangedEventArgs(ComponentName, _mValue, newValue);
            var message = string.Empty;
            if (DimensionedValidate(value, ref message))
            {
                _mValue = newValue;
                NotifyPropertyChanged("Value");
                OnParameterValueChanged(args);
            }
            else
            {
                throw new CapeInvalidArgumentException(message, 1);
            }
        }
    }

    /// <summary>Validates the current value of the parameter against the 
    /// specification of the parameter. </summary>
    /// <remarks>The parameter is considered valid if the current value is 
    /// between the upper and lower bound. The message is used to 
    /// return the reason that the parameter is invalid. This function also
    /// sets the CapeValidationStatus of the parameter based upon the results
    /// of the validation.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public override bool Validate(ref string message)
    {
        message = "Value is valid.";
        var args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, 
            CapeValidationStatus.CAPE_VALID);            
        var retVal = true;
        if (_mValue < _mLowerBound)
        {
            message = "Value below the Lower Bound.";
            args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, 
                CapeValidationStatus.CAPE_INVALID);
            MValStatus = CapeValidationStatus.CAPE_INVALID;
            NotifyPropertyChanged("ValStatus");
            OnParameterValidated(args);
            retVal = false;
        }
        if (_mValue > _mUpperBound)
        {
            message = "Value greater than upper bound.";
            args = new ParameterValidatedEventArgs(ComponentName, message, MValStatus, 
                CapeValidationStatus.CAPE_INVALID);
            MValStatus = CapeValidationStatus.CAPE_INVALID;
            NotifyPropertyChanged("ValStatus");
            OnParameterValidated(args);
            retVal = false;
        }
        OnParameterValidated(args);
        MValStatus = CapeValidationStatus.CAPE_VALID;
        NotifyPropertyChanged("ValStatus");
        return retVal;
    }

    /// <summary>Sets the value of the parameter to its default value.</summary>
    /// <remarks> This method sets the parameter's value to the default value.</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mValue = _mDefaultValue;
        NotifyPropertyChanged("Value");
        OnParameterReset(args);
    }

    // ICapeParameterSpec
    /// <summary>Gets the type of the parameter. </summary>
    /// <remarks>Gets the <see cref = "CapeParamType"/> of the parameter for which this is a specification: real 
    /// (CAPE_REAL), integer(CAPE_INT), option(CAPE_OPTION), boolean(CAPE_BOOLEAN) 
    /// or array(CAPE_ARRAY).</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(false),Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_REAL;

    //ICapeRealParameterSpec
    /// <summary>Gets and sets the default value of the parameter.</summary>
    /// <remarks>The default value is the value that the specification is set to after
    /// the Reset() method is called.</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(false)]
    double ICapeRealParameterSpecCOM.DefaultValue => _mDefaultValue;

    /// <summary>Gets and sets the default value of the parameter.</summary>
    /// <remarks>The default value is the value that the specification is set to after
    /// the Reset() method is called.</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    public double DimensionedDefaultValue
    {
        get {
            var bUnitExisting = false;
            double pFactor = 1;
            double mFactor = 0;
            var retVal = _mDefaultValue;
            var units = CDimensions.Units;
            if (units.Any(mt => mt == Unit))
            {
                pFactor = CDimensions.ConversionsTimes(Unit);
                mFactor = CDimensions.ConversionsPlus(Unit);
                bUnitExisting = true;
            }
            retVal = (Convert.ToDouble(retVal) - mFactor) / pFactor;
            if (!bUnitExisting)
                MessageBox.Show("There is no unit named" + Unit, "Unit Warning!");
            return retVal;
        }
        set
        {
            ParameterDefaultValueChangedEventArgs args = new ParameterDefaultValueChangedEventArgs(ComponentName, _mDefaultValue, ConvertToSI(value, Unit));
            _mDefaultValue = ConvertToSI(value, Unit);
            NotifyPropertyChanged("DefaultValue");
            OnParameterDefaultValueChanged(args);
        }
    }

    /// <summary>Gets and sets the lower bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MinValue, negative 1.79769313486232e308.</remarks>			
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    public double DimensionedLowerBound
    {
        get
        {
            if (_mLowerBound == double.MinValue) return double.MinValue;
            var bUnitExisting = false;
            double pFactor = 1;
            double mFactor = 0;
            var retVal = _mLowerBound;
            var units = CDimensions.Units;
            if (units.Any(t => t == Unit))
            {
                pFactor = CDimensions.ConversionsTimes(Unit);
                mFactor = CDimensions.ConversionsPlus(Unit);
                bUnitExisting = true;
            }
            retVal = (Convert.ToDouble(retVal) - mFactor) / pFactor;
            if (!bUnitExisting)
                MessageBox.Show("There is no unit named" + Unit, "Unit Warning!");
            return retVal;
        }
        set
        {
            ParameterLowerBoundChangedEventArgs args;
            if (value == double.MinValue)
            {
                args = new ParameterLowerBoundChangedEventArgs(ComponentName, _mUpperBound, double.MinValue);
                _mLowerBound = double.MaxValue;
                NotifyPropertyChanged("LowerBound");
                OnParameterLowerBoundChanged(args);
                return;
            }
            args = new ParameterLowerBoundChangedEventArgs(ComponentName, _mUpperBound, ConvertToSI(value, Unit));
            _mLowerBound = ConvertToSI(value, Unit);
            NotifyPropertyChanged("LowerBound");
            OnParameterLowerBoundChanged(args);
        }
    }

    /// <summary>Gets and sets the default value of the parameter.</summary>
    /// <remarks>The default value is the value that the specification is set to after
    /// the Reset() method is called.</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    public double SIDefaultValue
    {
        get => _mDefaultValue;
        set
        {
            var args = new ParameterDefaultValueChangedEventArgs(ComponentName, _mDefaultValue, 
                ConvertToSI(value, Unit));
            _mDefaultValue = ConvertToSI(value, SIUnit);
            NotifyPropertyChanged("DefaultValue");
            OnParameterDefaultValueChanged(args);
        }
    }

    /// <summary>Gets and sets the lower bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MinValue, negative 1.79769313486232e308.</remarks>			
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    public double SILowerBound
    {
        get => _mLowerBound;
        set
        {
            ParameterLowerBoundChangedEventArgs args;
            if (value == double.MinValue)
            {
                args = new ParameterLowerBoundChangedEventArgs(ComponentName, _mUpperBound, double.MinValue);
                _mLowerBound = double.MaxValue;
                NotifyPropertyChanged("LowerBound");
                OnParameterLowerBoundChanged(args);
                return;
            }
            args = new ParameterLowerBoundChangedEventArgs(ComponentName, _mUpperBound, value);
            _mLowerBound = ConvertToSI(value, SIUnit);
            NotifyPropertyChanged("LowerBound");
            OnParameterLowerBoundChanged(args);
        }
    }

    /// <summary>Gets and sets the lower bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    double ICapeRealParameterSpecCOM.LowerBound => _mLowerBound;

    /// <summary>Gets and sets the upper bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Category("Parameter Specification")]
    public double DimensionedUpperBound
    {
        get
        {
            var bUnitExisting = false;
            double pFactor = 1;
            double mFactor = 0;
            var retVal = _mUpperBound;
            var units = CDimensions.Units;
            if (units.Any(t => t == Unit))
            {
                pFactor = CDimensions.ConversionsTimes(Unit);
                mFactor = CDimensions.ConversionsPlus(Unit);
                bUnitExisting = true;
            }
            retVal = (Convert.ToDouble(retVal) - mFactor) / pFactor;
            if (!bUnitExisting)
                MessageBox.Show("There is no unit named" + Unit, "Unit Warning!");
            return retVal;
        }
        set
        {
            ParameterUpperBoundChangedEventArgs args;
            if (value == double.MaxValue)
            {
                args = new ParameterUpperBoundChangedEventArgs(ComponentName, _mUpperBound, double.MaxValue);
                _mUpperBound = double.MaxValue;
                NotifyPropertyChanged("UpperBound");
                OnParameterUpperBoundChanged(args);
                return;
            }
            args = new ParameterUpperBoundChangedEventArgs(ComponentName, _mUpperBound, ConvertToSI(value, Unit));
            _mUpperBound = ConvertToSI(value, Unit);
            NotifyPropertyChanged("UpperBound");
            OnParameterUpperBoundChanged(args);
        }
    }
    
    /// <summary>Gets and sets the upper bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Category("Parameter Specification")]
    public double SIUpperBound
    {
        get => _mUpperBound;
        set
        {
            ParameterUpperBoundChangedEventArgs args;
            if (value == double.MaxValue)
            {
                args = new ParameterUpperBoundChangedEventArgs(ComponentName, _mUpperBound, double.MaxValue);
                _mUpperBound = double.MaxValue;
                NotifyPropertyChanged("UpperBound");
                OnParameterUpperBoundChanged(args);
                return;
            }
            args = new ParameterUpperBoundChangedEventArgs(ComponentName, _mUpperBound, value);
            _mUpperBound = value;
            NotifyPropertyChanged("UpperBound");
            OnParameterUpperBoundChanged(args);
        }
    }

    /// <summary>Gets and sets the upper bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    double ICapeRealParameterSpecCOM.UpperBound => _mUpperBound;

    /// <summary>Validates the SI pValue sent against the specification of the parameter.</summary>
    /// <remarks>The pValue, in the SI units of measurement appropriate to the <see cref="UnitCategory"/> of the parameter
    /// is considered valid if it is between the upper and lower bound of the parameter. The message is used to 
    /// return the reason that the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "pValue">The name of the unit that the pValue should be converted to.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public bool SIValidate(double pValue, ref string message)
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

    /// <summary>Validates the dimensioned pValue sent against the specification of the parameter.</summary>
    /// <remarks><para>The pValue sent in is converted from its dimensioned pValue using the current unit of the parameter 
    /// to an SI pValue and is then validated against the parameter specification.</para>
    /// <para>The pValue is considered valid if it is between the upper and lower 
    /// bound of the parameter. The message is used to return the reason that 
    /// the parameter is invalid.</para></remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "pValue">The name of the unit that the pValue should be converted to.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public bool DimensionedValidate(double pValue, ref string message)
    {
        var testVal = ConvertToSI(pValue, Unit);
        if (testVal < _mLowerBound)
        {
            message = "Value below the Lower Bound.";
            return false;
        }
        if (testVal > _mUpperBound)
        {
            message = "Value greater than upper bound.";
            return false;
        }
        message = "Value is valid.";
        return true;
    }
        
    /// <summary>Gets the SI unit for the <see cref="UnitCategory"/> of the parameter.</summary>
    /// <remarks><para>Provides the SI unit for the parameter. Parameter values are stored in their SI unit of measure for the 
    /// <see cref="UnitCategory"/>. This provides the user with a mechanism to determine the SI unit of measure 
    /// used for the current parameter.</para></remarks>
    /// <value>Defines the SI unit for the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Category("Value")]
    [Description("Provide the Aspen Plus display units for for this parameter.")]
    [Browsable(true)]
    [TypeConverter(typeof(StringConverter))]
    public string SIUnit => CDimensions.FindSIUnit(_mUnit);

    /// <summary>Gets the Aspen display unit for the parameter.</summary>
    /// <remarks><para>DisplayUnits defines the unit of measurement symbol for a parameter.</para>
    /// <para>Note: The symbol must be one of the uppercase strings recognized by Aspen
    /// Plus to ensure that it can perform unit of measurement conversions on the 
    /// parameter value. The system converts the parameter's value from SI units for
    /// display in the data browser and converts updated values back into SI.</para></remarks>
    /// <value>Defines the display unit for the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Category("Dimensionality")]
    [Description("Provide the Aspen Plus display units for for this parameter.")]
    [Browsable(false)]
    string IATCapeXRealParameterSpec.DisplayUnits => CDimensions.AspenUnit(_mUnit);
}

/// <summary>Real-Valued parameter for use in the CAPE-OPEN parameter collection.</summary>
/// <remarks>Real-Valued parameter for use in the CAPE-OPEN parameter collection.</remarks>
[Serializable]
[ComSourceInterfaces(typeof(IRealParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeOpenGuids.PpRealParTerWapIid)]  //ICapeThermoMaterialObject_IID)  "C7095FE4-E61D-4FFF-BA02-013FD38DBAE9"
[ClassInterface(ClassInterfaceType.None)]
[TypeConverter(typeof(RealParameterTypeConverter))]
internal class RealParameterWrapper : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeRealParameterSpec,
    ICloneable, IATCapeXRealParameterSpec
{
    private string _mUnit = string.Empty;
    [NonSerialized]
    private ICapeParameter _mParameter;
    
    /// <summary>Creates a new instance of the double precision-valued parameter class. </summary>
    /// <remarks>The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT. The dimensionality 
    /// of the parameter is determined from the unit string.</remarks>
    /// <param name = "parameter">Use to Set the dimensionality of the parameter.</param>
    public RealParameterWrapper(ICapeParameter parameter)
        : base(((ICapeIdentification)parameter).ComponentName, 
            ((ICapeIdentification)parameter).ComponentDescription, parameter.Mode)
    {
        _mParameter = parameter;
        var dims = ((ICapeParameterSpecCOM)parameter.Specification).Dimensionality;
        _mUnit = dims switch
        {
            // if (typeof(int[]).IsAssignableFrom(dims.GetType()))
            int[] mInts => CDimensions.SIUnit(mInts),
            // else if (typeof(double[]).IsAssignableFrom(dims.GetType()))
            double[] mDimVals => CDimensions.SIUnit(mDimVals),
            _ => _mUnit
        };
    }

    /// <summary>Gets the dimensionality of the parameter.</summary>
    /// <remarks><para>Gets the dimensionality of the parameter for which this is the 
    /// specification. The dimensionality represents the physical dimensional 
    /// axes of this parameter. It is expected that the dimensionality must cover 
    /// at least 6 fundamental axes (length, mass, time, angle, temperature and 
    /// charge). A possible implementation could consist in being a constant 
    /// length array vector that contains the exponents of each basic SI unit, 
    /// following directives of SI-brochure (from http://www.bipm.fr/). So if we 
    /// agree on order &lt;m kg s A K,&gt; ... velocity would be 
    /// &lt;1,0,-1,0,0,0&gt;: that is m1 * s-1 =m/s. We have suggested to the 
    /// CO Scientific Committee to use the SI base units plus the SI derived units 
    /// with special symbols (for a better usability and for allowing the 
    /// definition of angles).</para></remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    double[] ICapeParameterSpec.Dimensionality => CDimensions.Dimensionality(_mUnit);

    /// <summary>Gets and sets the value for this Parameter.</summary>
    /// <remarks>This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.</remarks>
    /// <returns>A boxed boolean value of the parameter.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public override object value
    {
        get => _mParameter.value;
        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value, value);
            var message = string.Empty;
            if (((ICapeRealParameterSpecCOM)_mParameter).Validate((double)value, ref message))
            {
                _mParameter.value = value;
                NotifyPropertyChanged("Value");
                OnParameterValueChanged(args);
            }
            else
            {
                throw new CapeInvalidArgumentException(message, 1);
            }
        }
    }

    /// <summary>Returns the current value of the parameter in the dimensional unit
    /// specified.</summary>
    /// <remarks>The value of the parameter in the unit requested. The unit must be a valid
    /// parameter. For example, if the parameter was set as 101325 Pa, if the desiredUnit was "atm" the return
    /// value would be 1 (the value of 101325 Pa in atmospheres).</remarks>
    /// <value>The value of the parameter in the requested unit of measurement.</value>
    /// <param name = "desiredUnit">The unit that the parameter is desired to be reported in.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised unit identifier.</exception>
    public double ConvertTo(string desiredUnit)
    {
        var bUnitExisting = false;
        double pFactor = 1;
        double mFactor = 0;
        var retVal = (double)_mParameter.value;
        // if ((desiredUnit != null) || (desiredUnit == string.Empty))
        if (desiredUnit is not null or "")
        {
            var units = CDimensions.Units;
            if (units.Any(t => t == desiredUnit))
            {
                pFactor = CDimensions.ConversionsTimes(desiredUnit);
                mFactor = CDimensions.ConversionsPlus(desiredUnit);
                bUnitExisting = true;
            }
        }
        if (!bUnitExisting)
            MessageBox.Show("There is no unit named" + desiredUnit, "Unit Warning!");
        else
        {
            retVal = (Convert.ToDouble(retVal) - mFactor) / pFactor;
            _mUnit = desiredUnit;
        }
        return retVal;
    }

    /// <summary>Returns the pValue in the SI unit of the specified unit.</summary>
    /// <remarks>The pValue is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// pValue would be 101325 (the pValue of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <pValue>The pValue of the parameter in SI units.</pValue>
    /// <param name = "pValue">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <param name = "unit">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised unit identifier.</exception>
    public double ConvertToSI(double pValue, string unit)
    {
        var bUnitExisting = false;
        double pFactor = 1;
        double mFactor = 0;
        var retVal = (double)_mParameter.value;
        // if ((unit != null) || (unit == string.Empty))
        if (unit is not null or "")
        {
            var units = CDimensions.Units;
            if (units.Any(t => t == unit))
            {
                pFactor = CDimensions.ConversionsTimes(unit);
                mFactor = CDimensions.ConversionsPlus(unit);
                bUnitExisting = true;
            }
        }
        if (!bUnitExisting)
            MessageBox.Show("There is no unit named" + unit, "Unit Warning!");
        else
        {
            retVal = (retVal * pFactor) + mFactor;
        }
        return retVal;
    }
    
    /// <summary>Returns the pValue in the SI unit of the specified unit.</summary>
    /// <remarks>The pValue is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// pValue would be 101325 (the pValue of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <pValue>The pValue of the parameter in SI units.</pValue>
    /// <param name = "pValue">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <param name = "unit">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised unit identifier.</exception>
    public double ConvertToDimensioned(double pValue, string unit)
    {
        var bUnitExisting = false;
        double pFactor = 1;
        double mFactor = 0;
        var retVal = (double)_mParameter.value;
        // if ((unit != null) || (unit == string.Empty))
        if (unit is not null or "")
        {
            var units = CDimensions.Units;
            if (units.Any(t => t == unit))
            {
                pFactor = CDimensions.ConversionsTimes(unit);
                mFactor = CDimensions.ConversionsPlus(unit);
                bUnitExisting = true;
            }
        }
        if (!bUnitExisting)
            MessageBox.Show("There is no unit named" + unit, "Unit Warning!");
        else
        {
            retVal = (retVal - mFactor)/pFactor;
        }
        return retVal;
    }
    
    /// <summary>Gets and sets the dimensional unit for the parameter.</summary>
    /// <remarks>The value is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// value would be 101325 (the value of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <value>The value of the parameter in SI units.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised unit identifier.</exception>
    [Browsable(true),Category("Value")]
    [TypeConverter(typeof(UnitConverter))]
    [Description("Dimensional unit of the parameter.")]
    public string Unit
    {
        get => _mUnit;
        set
        {
            _mUnit = value;
            //m_Category = CapeOpen.CDimensions.UnitCategory(m_unit); 
            NotifyPropertyChanged(nameof(Unit));
        }
    }

    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <remarks><para>Clone can be implemented either as a deep copy or a shallow copy. In a deep copy, all objects are duplicated; 
    /// in a shallow copy, only the top-level objects are duplicated and the lower levels contain references.</para>
    /// <para>
    /// The resulting clone must be of the same type as, or compatible with, the original instance.</para>
    /// <para>
    /// See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples.</para></remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public override object Clone()
    {
        return new RealParameterWrapper(_mParameter);
    }

    /// <summary>Occurs when the user changes of the lower bound of the parameter changes.</summary>
    public event ParameterLowerBoundChangedHandler ParameterLowerBoundChanged;
    
    /// <summary>Occurs when the user changes of the lower bound of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base class's <c>OnComponentNameChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterValueChangedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterLowerBoundChanged(ParameterLowerBoundChangedEventArgs args)
    {
        ParameterLowerBoundChanged?.Invoke(this, args);
    }

    /// <summary>Occurs when the user changes of the upper bound of the parameter changes.</summary>
    public event ParameterUpperBoundChangedHandler ParameterUpperBoundChanged;
    
    /// <summary>Occurs when the user changes of the upper bound of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterUpperBoundChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterUpperBoundChanged</c> in a derived class, be sure to call the base class's <c>OnParameterUpperBoundChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterLowerBoundChangedEventArgs">ParameterUpperBoundChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterUpperBoundChanged(ParameterUpperBoundChangedEventArgs args)
    {
        ParameterUpperBoundChanged?.Invoke(this, args);
    }
    
    /// <summary>Gets and sets the parameter value in the SI unit for its current dimensionality.</summary>
    /// <remarks>The value is returned in the SI units for the dimensionality of the 
    /// parameter. For example, if the parameter was set as 1 atm, the return
    /// value would be 101325 (the value of 1 atm in the SI pressure 
    /// unit of Pascals, Pa).</remarks>
    /// <value>The value of the parameter in SI units.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(true),Category("Value")]
    public double SIValue
    {
        get => (double)_mParameter.value;

        set
        {
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value, value);
            OnParameterValueChanged(args);
            _mParameter.value = value;
            NotifyPropertyChanged("Value");
        }
    }

    /// <summary>Gets and sets the parameter value in the current unit for its dimensionality.</summary>
    /// <remarks>The value is returned in the parameter's current unit. For example, if the 
    /// parameter was set as 101325 Pa and the unit was changed to atm, 
    /// the return value would be 1 (the value of 101325 Pa in atmospheres).</remarks>
    /// <value>The value of the parameter in the current unit.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(true),Category("Value")]
    public double DimensionedValue
    {
        get => ConvertTo(Unit);

        set
        {
            var newValue = ConvertToSI(value, Unit);
            var args = new ParameterValueChangedEventArgs(ComponentName, _mParameter.value, newValue);
            var message = string.Empty;
            if (((ICapeRealParameterSpecCOM)_mParameter).Validate(value, ref message))
            {
                _mParameter.value = newValue;
                NotifyPropertyChanged("Value");
                OnParameterValueChanged(args);
            }
            else
            {
                throw new CapeInvalidArgumentException(message, 1);
            }
        }
    }

    /// <summary>Validates the current value of the parameter against the 
    /// specification of the parameter. </summary>
    /// <remarks>The parameter is considered valid if the current value is 
    /// between the upper and lower bound. The message is used to 
    /// return the reason that the parameter is invalid. This function also
    /// sets the CapeValidationStatus of the parameter based upon the results
    /// of the validation.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public override bool Validate(ref string message)
    {
        var pValStatus = _mParameter.ValStatus;
        var retVal = _mParameter.Validate(message);
        var args = new ParameterValidatedEventArgs(ComponentName, message, ValStatus, pValStatus);
        OnParameterValidated(args);
        NotifyPropertyChanged("ValStatus");
        return retVal;
    }

    /// <summary>Sets the value of the parameter to its default value.</summary>
    /// <remarks> This method sets the parameter's value to the default value.</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mParameter.Reset();
        NotifyPropertyChanged("Value");
        OnParameterReset(args);
    }

    // ICapeParameterSpec
    /// <summary>Gets the type of the parameter. </summary>
    /// <remarks>Gets the <see cref = "CapeParamType"/> of the parameter for which this is a specification: real 
    /// (CAPE_REAL), integer(CAPE_INT), option(CAPE_OPTION), boolean(CAPE_BOOLEAN) 
    /// or array(CAPE_ARRAY).</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(false),Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_REAL;

    //ICapeRealParameterSpec
    /// <summary>Gets and sets the default value of the parameter.</summary>
    /// <remarks>The default value is the value that the specification is set to after
    /// the Reset() method is called.</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(false)]
    public double DimensionedDefaultValue
    {
        get => ConvertToDimensioned(((ICapeRealParameterSpecCOM)_mParameter.Specification)
            .DefaultValue, Unit);
        set { }
    }

    /// <summary>Gets and sets the default value of the parameter.</summary>
    /// <remarks>The default value is the value that the specification is set to after
    /// the Reset() method is called.</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    public double SIDefaultValue
    {
        get => ((ICapeRealParameterSpecCOM)_mParameter.Specification).DefaultValue;
        set { }
    }

    /// <summary>Gets and sets the lower bound of the parameter in SI units corresponding to the <see cref="UnitCategory"/> of 
    /// the parameter.</summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MinValue, negative 1.79769313486232e308.</remarks>			
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    public double SILowerBound
    {
        get => ((ICapeRealParameterSpecCOM)_mParameter.Specification).LowerBound;
        set { }
    }

    /// <summary>Gets and sets the lower bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public double DimensionedLowerBound
    {
        get => ConvertToDimensioned(((ICapeRealParameterSpecCOM)_mParameter.Specification).LowerBound, Unit);
        set { }
    }

    /// <summary>Gets and sets the upper bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Category("Parameter Specification")]
    public double SIUpperBound
    {
        get => ((ICapeRealParameterSpecCOM)_mParameter.Specification).UpperBound;
        set { }
    }

    /// <summary>Gets and sets the upper bound of the parameter. </summary>
    /// <remarks>The lower bound can be a valid double precision value. 
    /// By default, it is set to Double.MaxValue, 
    /// 1.79769313486232e308.</remarks>			
    /// <value>The upper bound for the parameter. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public double DimensionedUpperBound
    {
        get => ConvertToDimensioned(((ICapeRealParameterSpecCOM)_mParameter.Specification)
            .UpperBound, Unit);
        set { }
    }

    /// <summary>Validates the pValue sent against the specification of the parameter.</summary>
    /// <remarks>The pValue is considered valid if it is between the upper and lower 
    /// bound of the parameter. The message is used to return the reason that 
    /// the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "pValue">The name of the unit that the pValue should be converted to.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public bool SIValidate(double pValue, ref string message)
    {
        return ((ICapeRealParameterSpecCOM)_mParameter.Specification).Validate(pValue, message);
    }
        
    /// <summary>Validates the pValue sent against the specification of the parameter.</summary>
    /// <remarks>The pValue is considered valid if it is between the upper and lower 
    /// bound of the parameter. The message is used to return the reason that 
    /// the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "pValue">The name of the unit that the pValue should be converted to.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument pValue is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public bool DimensionedValidate(double pValue, ref string message)
    {
        return ((ICapeRealParameterSpecCOM)_mParameter.Specification).Validate(ConvertToSI(pValue, Unit), message);
    }

    /// <summary>Gets the Aspen display unit for the parameter.</summary>
    /// <remarks><para>DisplayUnits defines the unit of measurement symbol for a parameter.</para>
    /// <para>Note: The symbol must be one of the uppercase strings recognized by Aspen
    /// Plus to ensure that it can perform unit of measurement conversions on the 
    /// parameter value. The system converts the parameter's value from SI units for
    /// display in the data browser and converts updated values back into SI.</para></remarks>
    /// <value>Defines the display unit for the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Category("Value")]
    [Description("Provide the Aspen Plus display units for for this parameter.")]
    [Browsable(true)]
    [TypeConverter(typeof(StringConverter))]
    public string SIUnit => CDimensions.FindSIUnit(_mUnit);

    /// <summary>Gets the Aspen display unit for the parameter.</summary>
    /// <remarks><para>DisplayUnits defines the unit of measurement symbol for a parameter.</para>
    /// <para>Note: The symbol must be one of the uppercase strings recognized by Aspen
    /// Plus to ensure that it can perform unit of measurement conversions on the 
    /// parameter value. The system converts the parameter's value from SI units for
    /// display in the data browser and converts updated values back into SI.</para></remarks>
    /// <value>Defines the display unit for the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Category("Dimensionality")]
    [Description("Provide the Aspen Plus display units for for this parameter.")]
    [Browsable(false)]
    public string DisplayUnits => CDimensions.AspenUnit(_mUnit);
}
