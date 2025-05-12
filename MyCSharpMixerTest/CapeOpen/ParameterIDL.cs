// 大白萝卜重构于 2025.05.12，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2002.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org

This file has been edited using the editor from Microsoft Visual Studio 6.0
This file can view properly with any basic editors and browsers (validation done under MS Windows and Unix)
*/

// This file was developed/modified by JEAN-PIERRE-BELAUD for CO-LaN organisation - March 2003

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>获取指定参数的类型：</summary>
/// <remarks>
///    double-precision Real (CAPE_REAL),
///    integer(CAPE_INT),
///    String (or option)(CAPE_OPTION), 
///    boolean(CAPE_BOOLEAN)
///    array(CAPE_ARRAY)
/// Reference document: Parameter Common Interface
/// </remarks>
[Serializable]
public enum CapeParamType
{
    /// <summary>Double-precision real-valued parameter</summary>
    /// <value>0</value>
    CAPE_REAL = 0,

    /// <summary>Integer-valued parameter</summary>
    CAPE_INT = 1,

    /// <summary>String/option parameter</summary>
    CAPE_OPTION = 2,

    /// <summary>Boolean-valued parameter</summary>
    CAPE_BOOLEAN = 3,

    /// <summary>Array parameter</summary>
    CAPE_ARRAY = 4
}

/// <summary>参数模式。</summary>
/// <remarks><para>It allows the following values:</para>
/// <list type="number">
/// <item>Input (CAPE_INPUT): the Unit(or whichever owner component) will use its value to calculate.</item>
/// <item>Output (CAPE_OUTPUT): the Unit will place in the parameter a result of its calculations.</item>
/// <item>Input-Output (CAPE_INPUT_OUTPUT): the user inputs an initial estimation value and
/// the user outputs a calculated value.</item>
/// </list>
/// Reference document: Parameter Common Interface
/// </remarks>
[Serializable]
public enum CapeParamMode
{
    /// <summary>单位（或任何所有者组件）将使用参数值作为其计算的 作为其计算的输入。</summary>
    CAPE_INPUT = 0,

    /// <summary>单元（或任何所有者组件）将把参数值设置为 作为计算输出。</summary>
    CAPE_OUTPUT = 1,

    /// <summary>单位（或任何所有者组件）将使用参数的初始值作为估计值，并计算最终值。</summary>
    CAPE_INPUT_OUTPUT = 2
}

/// <remarks>参考文件： 参数通用接口</remarks>
[ComVisible(false)]
[Description("ICapeParameterSpec Interface")]
public interface ICapeParameterSpec
{
    /// <summary>获取参数的类型。</summary>
    /// <remarks>Gets the <see cref = "CapeParamType"/> of the parameter for which
    /// this is a specification: real (CAPE_REAL), integer(CAPE_INT), option(CAPE_OPTION),
    /// boolean(CAPE_BOOLEAN) or array(CAPE_ARRAY).</remarks>
    /// <value>The parameter type.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Property Type")]
    CapeParamType Type { get; }

    /// <summary>获取参数的维度。</summary>
    /// <remarks><para>Gets the dimensionality of the parameter for which this is the 
    /// specification. The dimensionality represents the physical dimensional 
    /// axes of this parameter. It is expected that the dimensionality must cover 
    /// at least 6 fundamental axes (length, mass, time, angle, temperature and 
    /// charge). A possible implementation could consist in being a constant 
    /// length array vector that contains the exponents of each basic SI unit, 
    /// following directives of SI-brochure (from http://www.bipm.fr/). So if we 
    /// agree on order (m kg s A K) ... velocity would be 
    /// (1,0,-1,0,0,0) : that is m1 * s-1 =m/s. We have suggested to the 
    /// CO Scientific Committee to use the SI base units plus the SI derived units 
    /// with special symbols (for a better usability and for allowing the 
    /// definition of angles).</para></remarks>
    /// <value>an integer array indicating the exponents of the various dimensional axes.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Property Dimensionality")]
    double[] Dimensionality { get; }
}

/// <remarks>参考文件： 参数通用接口</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeParameterSpec_IID)]
[Description("ICapeParameterSpec Interface")]
internal interface ICapeParameterSpecCOM
{
    /// <summary>获取参数的类型。</summary>
    /// <remarks>Gets the <see cref = "CapeParamType"/> of the parameter for which this is a specification: real 
    /// (CAPE_REAL), integer(CAPE_INT), option(CAPE_OPTION), boolean(CAPE_BOOLEAN) or array(CAPE_ARRAY).</remarks>
    /// <value>The parameter type.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Property Type")]
    CapeParamType Type { get; }

    /// <summary>获取参数的维度。</summary>
    /// <remarks><para>Gets the dimensionality of the parameter for which this is the 
    /// specification. The dimensionality represents the physical dimensional 
    /// axes of this parameter. It is expected that the dimensionality must cover 
    /// at least 6 fundamental axes (length, mass, time, angle, temperature and 
    /// charge). A possible implementation could consist in being a constant 
    /// length array vector that contains the exponents of each basic SI unit, 
    /// following directives of SI-brochure (from http://www.bipm.fr/). So if we 
    /// agree on order (m kg s A K) ... velocity would be 
    /// (1,0,-1,0,0,0) : that is m1 * s-1 =m/s. We have suggested to the 
    /// CO Scientific Committee to use the SI base units plus the SI derived units 
    /// with special symbols (for a better usability and for allowing the 
    /// definition of angles).</para></remarks>
    /// <value>an integer array indicating the exponents of the various dimensional axes.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Property Dimensionality")]
    object Dimensionality { get; }
}

/// <summary>当参数具有双精度浮点数值时，该接口用于参数规范。参数具有双精度浮点数值时的参数规格。</summary>
[ComVisible(false)]
[Description("ICapeRealParameterSpec Interface")]
public interface ICapeRealParameterSpec
{
    /// <summary>获取参数的默认值。</summary>
    /// <remarks>A default value for the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Property Default")]
    double SIDefaultValue { get; set; }

    /// <summary>获取参数的下限。</summary>
    /// <remarks>A lower bound value for the parameter.</remarks>
    /// <value>The lower bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Property LowerBound")]
    double SILowerBound { get; set; }

    /// <summary>获取参数的上限。</summary>
    /// <remarks>An upper bound value for the parameter.</remarks>
    /// <value>The upper bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(3), Description("Property UpperBound")]
    double SIUpperBound { get; set; }

    /// <summary>根据参数说明验证值。信息用于返回参数无效的原因。</summary>
    /// <remarks>The parameter is considered valid if the current value is between 
    /// the upper and lower bound. The message is used to return the reason 
    /// that the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">Integer value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4)]
    [Description("Check if value is OK for this spec as double")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool SIValidate(double value, ref string message);

    /// <summary>获取参数的默认值。</summary>
    /// <remarks>A default value for the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1)]
    [Description("Property Default")]
    double DimensionedDefaultValue { get; set; }

    /// <summary>获取参数的下限。</summary>
    /// <remarks>A lower bound value for the parameter.</remarks>
    /// <value>The lower bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2)]
    [Description("Property LowerBound")]
    double DimensionedLowerBound { get; set; }

    /// <summary>获取参数的上限。</summary>
    /// <remarks>An upper bound value for the parameter.</remarks>
    /// <value>The upper bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(3)]
    [Description("Property UpperBound")]
    double DimensionedUpperBound { get; set; }

    /// <summary>根据参数说明验证值。消息用于返回参数无效的原因。</summary>
    /// <remarks>The parameter is considered valid if the current value is between 
    /// the upper and lower bound. The message is used to return the reason 
    /// that the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">Integer value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4)]
    [Description("Check if value is OK for this spec as double")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool DimensionedValidate(double value, ref string message);
}

/// <summary>当参数具有双精度浮点数值时，该接口用于参数规范。 参数具有双精度浮点数值时的参数规格。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeRealParameterSpec_IID)]
[Description("ICapeRealParameterSpec Interface")]
internal interface ICapeRealParameterSpecCOM
{
    /// <summary>获取参数的默认值。</summary>
    /// <remarks>A default value for the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1),Description("Property Default")]
    double DefaultValue { get; }

    /// <summary>获取参数的下限。</summary>
    /// <remarks>A lower bound value for the parameter.</remarks>
    /// <value>The lower bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2),Description("Property LowerBound")]
    double LowerBound { get; }

    /// <summary>获取参数的上限。</summary>
    /// <remarks>An upper bound value for the parameter.</remarks>
    /// <value>The upper bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(3),Description("Property UpperBound")]
    double UpperBound { get; }

    /// <summary>根据参数说明验证值。信息用于返回参数无效的原因。</summary>
    /// <remarks>The parameter is considered valid if the current value is between 
    /// the upper and lower bound. The message is used to return the reason 
    /// that the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">Integer value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4),Description("Check if value is OK for this spec as double")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(double value, ref string message);
}

/// <summary>当参数为整数值时，该接口用于参数说明。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeIntegerParameterSpec_IID)]
[Description("ICapeIntegerParameterSpec Interface")]
public interface ICapeIntegerParameterSpec
{
    /// <summary>获取参数的默认值。</summary>
    /// <remarks>A default value for the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Property Default")]
    int DefaultValue { get; set; }

    /// <summary>获取参数的下限。</summary>
    /// <remarks>A lower bound value for the parameter.</remarks>
    /// <value>The lower bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Property LowerBound")]
    int LowerBound { get; set; }

    /// <summary>获取参数的上限。</summary>
    /// <remarks>An upper bound value for the parameter.</remarks>
    /// <value>The upper bound of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(3), Description("Property UpperBound")]
    int UpperBound { get; set; }

    /// <summary>根据参数说明验证发送的值。</summary>
    /// <remarks>The parameter is considered valid if the current value is between 
    /// the upper and lower bound. The message is used to return the reason 
    /// that the parameter is invalid.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">Integer value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4),Description("Check if value is OK for this spec as double")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(int value, ref string message);
}

/// <summary>当参数是一个选项时，该接口用于参数说明。一个字符串列表，从中选择一个。</summary>
[ComVisible(false)]
[Description("ICapeOptionParameterSpec Interface")]
public interface ICapeOptionParameterSpec
{
    /// <summary>获取参数的默认值。</summary>
    /// <remarks>A default string value for the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Property Default")]
    string DefaultValue { get; set; }

    /// <summary>如果 "RestrictedToList" 属性为 true，则获取参数的有效值列表。</summary>
    /// <remarks>Used in validating the parameter if the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>.</remarks>
    /// <value>String array as a System.Object, COM Variant containing a SafeArray of BSTR.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("The list of names of the items")]
    string[] OptionList { get; set; }

    /// <summary>验证参数值的字符串列表。</summary>
    /// <remarks>If <c>true</c>, the parameter's value will be validated against the Strings
    /// in the <see cref = "OptionList">OptionList</see>.</remarks>
    /// <value>Converted by COM interop to a COM-based CAPE-OPEN VARIANT_BOOL.</value>
    [DispId(3), Description("True if it only accepts values from the option list.")]
    bool RestrictedToList { get; set; }

    /// <summary>根据参数说明验证值。</summary>
    /// <remarks>If the value of the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>, the value is valid value for the parameter if it is included in the 
    /// <see cref = "OptionList">OptionList</see>. If the 
    /// value of <see cref = "RestrictedToList">RestrictedToList</see> is <c>false</c>
    /// any valid String is a valid value for the parameter.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">A candidate value for the parameter to be tested to determine whether the value is valid.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4), Description("Check if value is OK for this spec as string")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(string value, ref string message);
}

/// <summary>当参数是选项时，此接口用于参数规范，它表示从其中选择一个的字符串列表。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeOptionParameterSpec_IID)]
[Description("ICapeOptionParameterSpec Interface")]
internal interface ICapeOptionParameterSpecCOM
{
    /// <summary>获取参数的默认值。</summary>
    /// <remarks>A default string value for the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("property Default")]
    string DefaultValue { get; }

    /// <summary>如果 "RestrictedToList" 属性为 true，则获取参数的有效值列表。</summary>
    /// <remarks>Used in validating the parameter if the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>.</remarks>
    /// <value>String array as a System.Object, COM Variant containing a SafeArray of BSTR.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("The list of names of the items")]
    object OptionList { get; }

    /// <summary>验证参数值的字符串列表。</summary>
    /// <remarks>If <c>true</c>, the parameter's value will be validated against the Strings
    /// in the <see cref = "OptionList">OptionList</see>.</remarks>
    /// <value>Converted by COM interop to a COM-based CAPE-OPEN VARIANT_BOOL.</value>
    [DispId(3), Description("True if it only accepts values from the option list.")]
    bool RestrictedToList { get; }

    /// <summary>根据参数说明验证值。</summary>
    /// <remarks>If the value of the <see cref = "RestrictedToList">RestrictedToList</see>
    /// is set to <c>true</c>, the value is valid value for the 
    /// parameter if it is included in the 
    /// <see cref = "OptionList">OptionList</see>. If the 
    /// value of <see cref = "RestrictedToList">RestrictedToList</see> is <c>false</c>
    /// any valid String is a valid value for the parameter.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">A candidate value for the parameter to be tested to determine whether the value is valid.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4), Description("Check if value is OK for this spec as string")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(string value, ref string message);
}

/// <summary>当参数是布尔值时，该接口用于参数规范。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeBooleanParameterSpec_IID)]
[Description("ICapeBooleanParameterSpec Interface")]
public interface ICapeBooleanParameterSpec
{
    /// <summary>获取参数的默认值。</summary>
    /// <remarks>Gets the default value of the parameter.</remarks>
    /// <value>The default value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Property Default")]
    bool DefaultValue { get; set; }

    /// <summary>根据参数说明验证发送的值。</summary>
    /// <remarks>Validates whether the argument is accepted by the parameter as a valid value. 
    /// It returns a flag to indicate the success or failure of the validation together 
    /// with a text message which can be used to convey the reasoning to the client/user.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "value">Boolean value that will be validated against the parameter's current specification.</param>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Check if value is OK for this spec")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(bool value, ref string message);
}

/// <summary>该接口用于指定参数 当参数是一个数值数组时（可能是整数、实数、布尔值或数组），由其表示。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeArrayParameterSpec_IID)]
[Description("ICapeArrayParameterSpec Interface")]
public interface ICapeArrayParameterSpec
{
    /// <summary>获取数组的维数。</summary>
    /// <remarks>The number of dimensions of the parameter array.</remarks>
    /// <value>The number of dimensions of the array.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Get the number of dimensions of the array")]
    int NumDimensions { get; }

    /// <summary>获取数组各维度的大小。</summary>
    /// <remarks>An array containing the specification of each member of the parameter array. </remarks>
    /// <value>An integer array containing the size of each dimension of the array.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2),
     Description("Get the size of each one of the dimensions of the array")]
    int[] Size { get; }

    /// <summary>获取参数值中每个项目的规格数组。</summary>
    /// <remarks>An array of interfaces to the correct specification type (<see cref = "ICapeRealParameterSpec"/> ,
    /// <see cref = "ICapeIntegerParameterSpec"/> , <see cref = "ICapeBooleanParameterSpec"/> , 
    /// <see cref = "ICapeOptionParameterSpec"/> ). Note that it is also possible, for 
    /// example, to configure an array of arrays of integers, which would a similar 
    /// but not identical concept to a two-dimensional matrix of integers.</remarks>
    /// <value>An array of <see cref = "ICapeParameterSpec"/> objects.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(3), Description("Get the specification of each of the values in the array")]
    object[] ItemsSpecifications { get; }

    /// <summary>根据参数说明验证值。信息用于返回参数无效的原因。</summary>
    /// <remarks>This method checks the current value of the parameter to determine if it is an allowed value. </remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "inputArray">The message is used to return the reason that the parameter is invalid.</param>
    /// <param name = "messages">A string array containing the message is used to return the reason that the parameter is invalid.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4), Description("Check if value is OK for this spec ")]
    object Validate(object inputArray, ref string[] messages);
}

/// <summary>定义实际参数数量的界面。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeParameter_IID)]
[Description("ICapeParameter Interface")]
public interface ICapeParameter
{
    /// <summary>获取该参数的规格说明。</summary>
    /// <remarks>Gets the specification of the parameter. The Get method returns the 
    /// specification as an interface to the correct specification type.</remarks>
    /// <value>An object implementing the <see cref = "ICapeParameterSpec"/>, as well as the
    /// appropriate specification for the parameter type, <see cref = "ICapeRealParameterSpec"/> ,
    /// <see cref = "ICapeIntegerParameterSpec"/> , <see cref = "ICapeBooleanParameterSpec"/> , 
    /// <see cref = "ICapeOptionParameterSpec"/> , or <see cref = "ICapeArrayParameterSpec"/> .</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Gets and sets the specification for the parameter.")]
    object Specification
    {
        [return: MarshalAs(UnmanagedType.IDispatch)] get;
    }

    /// <summary>获取和设置该参数的值。</summary>
    /// <remarks>Gets and sets the value of this parameter. Passed as a CapeVariant that 
    /// should be the same type as the Parameter type.</remarks>
    /// <value>The boxed value of the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2),Description("Get and sets the value of the parameter.")]
    object value { get; set; }

    /// <summary>获取表示参数验证状态的标志。</summary>
    /// <remarks><para>Gets the flag to indicate parameter validation status. It has three possible values:</para>
    /// <para>(i) notValidated(CAPE_NOT_VALIDATED): The PMC's <c>Validate()</c>
    /// method has not been called after the last time that its value had been changed.</para>
    /// <para>(ii) invalid(CAPE_INVALID): The last time that the PMC's <c>Validate()</c> method
    /// was called it returned false.</para>
    /// <para>(iii) valid(CAPE_VALID): the last time that the PMC's <c>Validate()</c> method
    /// was called it returned true.</para></remarks>
    /// <value>The validity status of the parameter, either valid, invalid, or "not validated".</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(3), Description("Get the parameter validation status")]
    CapeValidationStatus ValStatus { get; }

    /// <summary>获取和设置参数的模式。</summary>
    /// <remarks><para>Modes of parameters. It allows the following values:</para>
    /// <para>(i) Input (CAPE_INPUT): the Unit(or whichever owner component) will use 
    /// its value to calculate.</para>
    /// <para>(ii) Output (CAPE_OUTPUT): the Unit will place in the parameter a result 
    /// of its calculations.</para>
    /// <para>(iii) Input-Output (CAPE_INPUT_OUTPUT): the user inputs an 
    /// initial estimation value and the user outputs a calculated value.</para></remarks>
    /// <value>The mode of the parameter, input, output, or input/output.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(4), Description("Get the Mode - input,output - of the parameter.")]
    CapeParamMode Mode { get; set; }

    /// <summary>根据参数说明验证参数的当前值。</summary>
    /// <remarks>This method checks the current value of the parameter to determine if it is an allowed value. In the case of 
    /// numeric parameters (<see cref = "ICapeRealParameterSpec"/> and <see cref = "ICapeIntegerParameterSpec"/>),
    /// the value is valid if it is between the upper and lower bound. For String (<see cref = "ICapeOptionParameterSpec"/>),
    /// if the <see cref = "ICapeOptionParameterSpec.RestrictedToList"/> property is true, the value must be included as one of the
    /// members of the <see cref = "ICapeOptionParameterSpec.OptionList"/>. Otherwise, any string value is valid. Any boolean value (true/false) 
    /// valid for the <see cref = "ICapeBooleanParameterSpec"/> paramaters.</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "message">The message is used to return the reason that the parameter is invalid.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(5),Description("Validate the parameter's current value.")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(ref string message);

    /// <summary>将参数值设置为默认值。</summary>
    /// <remarks>This method sets the parameter to its default value.</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [DispId(6),Description("Reset the value of the parameter to its default.")]
    void Reset();
}

[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeOpenGuids.InParameterEventsIid)]  // "3C32AD8E-490D-4822-8A8E-073F5EDFF3F5"
[Description("CapeParameterEvents Interface")]
internal interface IParameterEvents
{
    /// <summary>当用户更改参数值时发生。</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValueChanged</c> in a derived class, be sure to call the base class's <c>OnParameterValueChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValueChanged">ParameterValueChanged</see> that contains information about the event.</param>
    void ParameterValueChanged(object sender, object args);

    /// <summary>用户更改参数模式时发生。</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterModeChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterModeChanged</c> in a derived class, be sure to call the base class's <c>OnParameterModeChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterModeChangedEventArgs">ParameterModeChangedEventArgs</see> that contains information about the event.</param>
    void ParameterModeChanged(object sender, object args);

    /// <summary>验证参数时出现。</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterValidated</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValidated</c> in a derived class, be sure to call the base class's <c>OnParameterValidated</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterValidatedEventArgs">ParameterValidatedEventArgs</see> that contains information about the event.</param>
    void ParameterValidated(object sender, object args);

    /// <summary>用户重置参数时出现。</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterReset</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterReset</c> in a derived class, be sure to call the base class's <c>OnParameterReset</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "RealParameter">RealParameter</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "ParameterResetEventArgs">ParameterResetEventArgs</see> that contains information about the event.</param>
    void ParameterReset(object sender, object args);
}

internal class ParameterTypeConverter : ExpandableObjectConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (typeof(ICapeParameter).IsAssignableFrom(destinationType))
            return true;
        return typeof(ICapeIdentification).IsAssignableFrom(destinationType) 
               || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context,
        System.Globalization.CultureInfo culture, object parameter, Type destinationType)
    {
        if (typeof(string).IsAssignableFrom(destinationType) 
            // && typeof(ArrayParameterWrapper).IsAssignableFrom(parameter.GetType()))
            && parameter is ArrayParameterWrapper)
        {
            return ""; //((CapeOpen.ArrayParameterWrapper)parameter)
        }

        if (typeof(string).IsAssignableFrom(destinationType) 
            // && typeof(ICapeParameter).IsAssignableFrom(parameter.GetType()))
            && parameter is ICapeParameter pCapeParameter)
        {
            return pCapeParameter.value.ToString();
        }

        if (typeof(string).IsAssignableFrom(destinationType) 
            // && typeof(ICapeIdentification).IsAssignableFrom(parameter.GetType()))
            && parameter is ICapeIdentification pIdentification)
        {
            return pIdentification.ComponentName;
        }

        return base.ConvertTo(context, culture, parameter, destinationType);
    }
}

/// <summary>Aspen(TM) 界面，用于提供实值参数的维度。</summary>
/// <remarks><para>Aspen Plus(TM) does not use the <see cref = "CapeOpen.ICapeParameterSpec.Dimensionality">ICapeParameterSpec.Dimensionality</see> method.
/// Instead, a parameter can implement the IATCapeXRealParameterSpec interface which can be used to define the
/// display unit for a parameter value.</para></remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.IatCapeXRealPaTeSpecIid)]  // "B777A1BD-0C88-11D3-822E-00C04F4F66C9"
[Description("IATCapeXRealParameterSpec Interface")]
internal interface IATCapeXRealParameterSpec
{
    /// <summary>获取参数的显示单位。由 AspenPlus(TM) 使用。</summary>
    /// <remarks><para>DisplayUnits defines the unit of measurement symbol for a parameter.</para>
    /// <para>Note: The symbol must be one of the uppercase strings recognized by Aspen
    /// Plus to ensure that it can perform unit of measurement conversions on the 
    /// parameter value. The system converts the parameter's value from SI units for
    /// display in the data browser and converts updated values back into SI.
    /// </para></remarks>
    /// <value>Defines the display unit for the parameter.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [DispId(0x60040003), Description("Provide the Aspen Plus display units for for this parameter.")]
    string DisplayUnits { get; }
}

/// <summary>代表处理参数值更改的方法。</summary>
[ComVisible(false)]
public delegate void ParameterValueChangedHandler(object sender, ParameterValueChangedEventArgs args);

/// <summary>为与参数相关的值更改事件提供数据。</summary>
/// <remarks>The IParameterValueChangedEventArgs interface specifies the old and new value of the parameter.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParTeVaChEvArgsIid)]  // "41E1A3C4-F23C-4B39-BC54-39851A1D09C9"
[Description("CapeIdentificationEvents Interface")]
internal interface IParameterValueChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    string ParameterName { get; }

    /// <summary>参数更改前的值。</summary>
    /// <remarks>The former value of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The value of the parameter prior to the change.</value>
    object OldValue { get; }

    /// <summary>参数更改后的值。</summary>
    /// <remarks>The new value of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The value of the parameter after the change.</value>
    object NewValue { get; }
}

/// <summary>为与参数相关的值更改事件提供数据。</summary>
/// <remarks>The ParameterValueChangedEventArgs event specifies the old and new value of the parameter.</remarks>
[Serializable,ComVisible(true)]
[Guid(CapeOpenGuids.ParTeVaChEvArgsIid)]  // "C3592B59-92E8-4A24-A2EB-E23C38F88E7F"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterValueChangedEventArgs : EventArgs, IParameterValueChangedEventArgs
{
    /// <summary>创建一个带有旧值和参数值的 ParameterValueChangedEventArgs 类实例。</summary>
    /// <remarks>You can use this constructor when raising the ParameterValueChangedEvent at run time to specify a 
    /// specific the parameter having its value changed.</remarks>
    /// <param name = "paramName">The name of the parameter being changed.</param>
    /// <param name = "oldValue">The name of the PMC prior to the name change.</param>
    /// <param name = "newValue">The name of the PMC after the name change.</param>
    public ParameterValueChangedEventArgs(string paramName, object oldValue, object newValue)
    {
        ParameterName = paramName;
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>参数更名前的值。</summary>
    /// <remarks>The former value of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The value of the parameter prior to the change.</value>
    public object OldValue { get; }

    /// <summary>参数更名后的值。</summary>
    /// <remarks>The new name of the unit can be used to update GUI information about the PMC.</remarks>
    /// <value>The value of the parameter after the change.</value>
    public object NewValue { get; }
}

/// <summary>代表用于处理更改参数默认值的方法。</summary>
[ComVisible(false)]
public delegate void ParameterDefaultValueChangedHandler(object sender, ParameterDefaultValueChangedEventArgs args);

/// <summary>为与参数相关的值更改事件提供数据。</summary>
/// <remarks>The IParameterDefaultValueChangedEventArgs interface specifies the old and new default value of the parameter.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParDefValChaEveArgsIid)]  // "E5D9CE6A-9B10-4A81-9E06-1B6C6C5257F3"
[Description("CapeIdentificationEvents Interface")]
internal interface IParameterDefaultValueChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    string ParameterName { get; }

    /// <summary>参数更改前的默认值。</summary>
    /// <remarks>The default value of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The default value of the parameter prior to the change.</value>
    object OldDefaultValue { get; }

    /// <summary>参数更改后的默认值。</summary>
    /// <remarks>The new default value of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The default value of the parameter after the change.</value>
    object NewDefaultValue { get; }
}

/// <summary>为与参数相关的值更改事件提供数据。</summary>
/// <remarks>The ParameterDefaultValueChangedEventArgs event specifies the old and new default value of the parameter.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ParDefValChaEveArgsIid)]  // "355A5BDD-F6B5-4EEE-97C7-F1533DD28889"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterDefaultValueChangedEventArgs : EventArgs,
    IParameterDefaultValueChangedEventArgs
{
    /// <summary>创建一个带有新旧默认值的 ParameterDefaultValueChangedEventArgs 类实例。</summary>
    /// <remarks>You can use this constructor when raising the ParameterDefaultValueChangedEventArgs at run time to specify  
    /// that the default value of the parameter has changed.</remarks>
    /// <param name = "paramName">The name of the parameter being changed.</param>
    /// <param name = "oldDefaultValue">The default value of the parameter prior to the change.</param>
    /// <param name = "newDefaultValue">The default value of the parameter after the change.</param>
    public ParameterDefaultValueChangedEventArgs(string paramName, 
        object oldDefaultValue, object newDefaultValue)
    {
        ParameterName = paramName;
        OldDefaultValue = oldDefaultValue;
        NewDefaultValue = newDefaultValue;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>PMC 更名前的名称。</summary>
    /// <remarks>The former name of the unit can be used to update GUI information about the PMC.</remarks>
    /// <value>The default of the parameter prior to the change.</value>
    public object OldDefaultValue { get; }

    /// <summary>参数名称更改后的默认值。</summary>
    /// <remarks>The new default value for the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The default value of the parameter after the change.</value>
    public object NewDefaultValue { get; }
}

/// <summary>表示处理参数下限变化的方法。</summary>
[ComVisible(false)]
public delegate void ParameterLowerBoundChangedHandler(object sender, ParameterLowerBoundChangedEventArgs args);

/// <summary>为与参数相关的值更改事件提供数据。</summary>
/// <remarks>The IParameterLowerBoundChangedEventArgs interface specifies the old and new lower bound of the parameter.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParLowBoChaEveArgsIid)]  // "FBCE7FC9-0F58-492B-88F9-8A23A23F93B1"
[Description("CapeIdentificationEvents Interface")]
internal interface IParameterLowerBoundChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    string ParameterName { get; }

    /// <summary>更改前参数的下限。</summary>
    /// <remarks>The former lower bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The lower bound of the parameter prior to the change.</value>
    object OldLowerBound { get; }

    /// <summary>更改后参数的下限。</summary>
    /// <remarks>The former lower bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The lower bound of the parameter after to the change.</value>
    object NewLowerBound { get; }
}

/// <summary>为与参数相关的值更改事件提供数据。</summary>
/// <remarks>The ParameterLowerBoundChangedEventArgs event specifies the old and new lower bound of the parameter.</remarks>
[Serializable,ComVisible(true)]
[Guid(CapeOpenGuids.ParLowBoChaEveArgsIid)]  // "A982AD29-10B5-4C86-AF74-3914DD902384"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterLowerBoundChangedEventArgs : EventArgs,
    IParameterLowerBoundChangedEventArgs
{
    /// <summary>创建 ParameterLowerBoundChangedEventArgs 类的实例，其中包含参数的新旧下限。</summary>
    /// <remarks>You can use this constructor when raising the ParameterLowerBoundChangedEvent at run time to specify that 
    /// the lower bound of the parameter has changed.</remarks>
    /// <param name = "paramName">The name of the parameter being changed.</param>
    /// <param name = "oldLowerBound">The name of the PMC prior to the name change.</param>
    /// <param name = "newLowerBound">The name of the PMC after the name change.</param>
    public ParameterLowerBoundChangedEventArgs(
        string paramName, object oldLowerBound, object newLowerBound)
    {
        ParameterName = paramName;
        OldLowerBound = oldLowerBound;
        NewLowerBound = newLowerBound;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>更改前参数的下限。</summary>
    /// <remarks>The former lower bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The lower bound of the parameter prior to the change.</value>
    public object OldLowerBound { get; }

    /// <summary>更改后参数的下限。</summary>
    /// <remarks>The new lower bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The lower bound of the parameter after the change.</value>
    public object NewLowerBound { get; }
}

/// <summary>Represents the method that will handle the changing of the upper bound of a parameter.</summary>
[ComVisible(false)]
public delegate void ParameterUpperBoundChangedHandler(
    object sender, ParameterUpperBoundChangedEventArgs args);

/// <summary>Represents the method that will handle the changing of the upper bound of a parameter.</summary>
[ComVisible(true)]
delegate void ParameterUpperBoundChangedHandlerCOM(object sender, object args);

/// <summary>Provides data for the upper bound changed event associated with the parameters.</summary>
/// <remarks>The IParameterUpperBoundChangedEventArgs interface specifies the old and new lower bound of the parameter.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParUpBoChaEveArgsIid)]  // "A2D0FAAB-F30E-48F5-82F1-4877F61950E9"
[Description("CapeIdentificationEvents Interface")]
internal interface IParameterUpperBoundChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    string ParameterName { get; }

    /// <summary>The upper bound of the parameter prior to the change.</summary>
    /// <remarks>The former upper bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The upper bound of the parameter prior to the change.</value>
    object OldUpperBound { get; }

    /// <summary>The upper bound of the parameter after to the change.</summary>
    /// <remarks>The former upper bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The upper bound of the parameter after to the change.</value>
    object NewUpperBound { get; }
}

/// <summary>Provides data for the upper bound changed event associated with the parameters.</summary>
/// <remarks>The ParameterUpperBoundChangedEventArgs event specifies the old and new lower bound of the parameter.</remarks>
[Serializable,ComVisible(true)]
[Guid(CapeOpenGuids.ParUpBoChaEveArgsIid)]  // "92BF83FE-0855-4382-A15E-744890B5BBF2"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterUpperBoundChangedEventArgs : EventArgs,
    IParameterUpperBoundChangedEventArgs
{
    /// <summary>Creates an instance of the ParameterUpperBoundChangedEventArgs class with the old and new upper bound for the parameter.</summary>
    /// <remarks>You can use this constructor when raising the ParameterUpperBoundChangedEvent at run time to specify 
    /// that the upper bound of the parameter has changed.</remarks>
    /// <param name = "paramName">The name of the parameter being changed.</param>
    /// <param name = "oldUpperBound">The upper bound of the parameter prior to the change.</param>
    /// <param name = "newUpperBound">The upper bound of the parameter after the change.</param>
    public ParameterUpperBoundChangedEventArgs(
        string paramName, object oldUpperBound, object newUpperBound)
    {
        ParameterName = paramName;
        OldUpperBound = oldUpperBound;
        NewUpperBound = newUpperBound;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>The upper bound of the parameter prior to the change.</summary>
    /// <remarks>The former upper bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The upper bound of the parameter prior to the change.</value>
    public object OldUpperBound { get; }

    /// <summary>The upper bound of the parameter after the change.</summary>
    /// <remarks>The new upper bound of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The upper bound of the parameter after the change.</value>
    public object NewUpperBound { get; }
}

/// <summary>Represents the method that will handle the changing of the mode of a parameter.</summary>
[ComVisible(false)]
public delegate void ParameterModeChangedHandler(object sender, ParameterModeChangedEventArgs args);

/// <summary>Provides data for the mode changed event associated with the parameters.</summary>
/// <remarks>The IParameterModeChangedEventArgs interface specifies the old and new mode of the parameter.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParModeChaEveArgsIid)]  // "5405E831-4B5F-4A57-A410-8E91BBF9FFD3"
[Description("CapeIdentificationEvents Interface")]
internal interface IParameterModeChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    string ParameterName { get; }

    /// <summary>The mode of the parameter prior to the change.</summary>
    /// <remarks>The former mode of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The mode of the parameter prior to the change.</value>
    object OldMode { get; }

    /// <summary>The mode of the parameter after to the change.</summary>
    /// <remarks>The former mode of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The mode of the parameter after to the change.</value>
    object NewMode { get; }
}

/// <summary>Provides data for the mode changed event associated with the parameters.</summary>
/// <remarks>The ParameterModeChangedEventArgs event specifies the old and new mode of the parameter.</remarks>
[Serializable,ComVisible(true)]
[Guid(CapeOpenGuids.ParModeChaEveArgsIid)]  // "3C953F15-A1F3-47A9-829A-9F7590CEB5E9"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterModeChangedEventArgs : EventArgs, IParameterModeChangedEventArgs
{
    /// <summary>Creates an instance of the ParameterModeChangedEventArgs class with the old and new upper bound for the parameter.</summary>
    /// <remarks>You can use this constructor when raising the ParameterModeChangedEvent at run time to specify 
    /// that the mode of the parameter has changed.</remarks>
    /// <param name = "paramName">The name of the parameter being changed.</param>
    /// <param name = "oldMode">The mode of the parameter prior to the change.</param>
    /// <param name = "newMode">The mode of the parameter after the change.</param>
    public ParameterModeChangedEventArgs(string paramName, object oldMode, object newMode)
    {
        ParameterName = paramName;
        OldMode = oldMode;
        NewMode = newMode;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>The mode of the parameter prior to the change.</summary>
    /// <remarks>The former mode of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The mode of the parameter prior to the change.</value>
    public object OldMode { get; }

    /// <summary>The mode of the parameter after the change.</summary>
    /// <remarks>The new mode of the parameter can be used to update GUI information about the PMC.</remarks>
    /// <value>The mode of the parameter after the change.</value>
    public object NewMode { get; }
}

/// <summary>Represents the method that will handle the validation of a parameter.</summary>
[ComVisible(false)]
public delegate void ParameterValidatedHandler(object sender, ParameterValidatedEventArgs args);

/// <summary>The parameter was validated.</summary>
/// <remarks>Provides information about the validation of the parameter.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParValEveArgsIid)]  // "EFD819A4-E4EC-462E-90E6-5D994CA44F8E"
[Description("ParameterValidatedEvent Interface")]
internal interface IParameterValidatedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    string ParameterName { get; }

    /// <summary>The message resulting from the parameter validation.</summary>
    /// <remarks>The message provides information about the results of the validation process.</remarks>
    /// <value>Information regrading the validation process.</value>
    string Message { get; }

    /// <summary>The validation status of the parameter prior to the validation.</summary>
    /// <remarks>Informs the user of the results of the validation process.</remarks>
    /// <value>The validation status of the parameter prior to the validation.</value>
    CapeValidationStatus OldStatus { get; }

    /// <summary>The validation status of the parameter after the validation.</summary>
    /// <remarks>Informs the user of the results of the validation process.</remarks>
    /// <value>The validation status of the parameter after the validation.</value>
    CapeValidationStatus NewStatus { get; }
}

/// <summary>The parameter was validated.</summary>
/// <remarks>Provides information about the validation of the parameter.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ParValEveArgsIid)]  // "5727414A-838D-49F8-AFEF-1CC8C578D756"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterValidatedEventArgs : EventArgs, IParameterValidatedEventArgs
{
    /// <summary>Creates an instance of the ParameterValidatedEventArgs class for the parameter.</summary>
    /// <remarks>You can use this constructor when raising the ParameterValidatedEventArgs at run time to  
    /// the message about the parameter validation.</remarks>
    /// <param name = "paramName">The name of the parameter being changed.</param>
    /// <param name = "message">The message indicating the results of the parameter validation.</param>
    /// <param name = "oldStatus">The status of the parameter prior to validation.</param>
    /// <param name = "newStatus">The status of the parameter after the validation.</param>
    public ParameterValidatedEventArgs(
        string paramName, string message, CapeValidationStatus oldStatus, CapeValidationStatus newStatus)
    {
        ParameterName = paramName;
        Message = message;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>The message resulting from the parameter validation.</summary>
    /// <remarks>The message provides information about the results of the validation process.</remarks>
    /// <value>Information regrading the validation process.</value>
    public string Message { get; }

    /// <summary>The validation status of the parameter prior to the validation.</summary>
    /// <remarks>Informs the user of the results of the validation process.</remarks>
    /// <value>The validation status of the parameter prior to the validation.</value>
    public CapeValidationStatus OldStatus { get; }

    /// <summary>The validation status of the parameter after the validation.</summary>
    /// <remarks>Informs the user of the results of the validation process.</remarks>
    /// <value>The validation status of the parameter after the validation.</value>
    public CapeValidationStatus NewStatus { get; }
}

/// <summary>Represents the method that will handle the resetting of a parameter.</summary>
[ComVisible(false)]
public delegate void ParameterResetHandler(object sender, ParameterResetEventArgs args);

/// <summary>The parameter was reset.</summary>
/// <remarks>The parameter was reset.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParResetEveArgsIid)]  // "12067518-B797-4895-9B26-EA71C60A8803"
[Description("ParameterResetEventArgs Interface")]
internal interface IParameterResetEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    string ParameterName { get; }
}

/// <summary>The parameter was reset.</summary>
/// <remarks>The parameter was reset.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ParResetEveArgsIid)]  // "01BF391B-415E-4F5E-905D-395A707DC125"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterResetEventArgs : EventArgs, IParameterResetEventArgs
{
    /// <summary>Creates an instance of the ParameterResetEventArgs class for the parameter.</summary>
    /// <remarks>You can use this constructor when raising the ParameterResetEventArgs at run time to  
    /// inform the system that the parameter was reset.</remarks>
    public ParameterResetEventArgs(string paramName)
    {
        ParameterName = paramName;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being reset.</value>
    public string ParameterName { get; }
}

/// <summary>Represents the method that will handle the changing of the option list of a parameter.</summary>
[ComVisible(false)]
public delegate void ParameterOptionListChangedHandler(object sender, ParameterOptionListChangedEventArgs args);

/// <summary>The parameter was reset.</summary>
/// <remarks>The parameter was reset.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParOpListChEvArgsIid)]  // "78E06E7B-00AB-4295-9915-546DC1CD64A6"
[Description("ParameterOptionListChangedEventArgs Interface")]
internal interface IParameterOptionListChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    string ParameterName { get; }
}

///// <summary>The parameter was reset.</summary>
///// <remarks>The parameter was reset.</remarks>
//[ComVisibleAttribute(true)]
//[Guid(CapeOpenGuids.PpParOpListChEvArgsIid)]  // "7B4DE7D2-1E39-4239-B8C5-4F876DDB15A4"
//[Description("ParameterOptionListChangedEventArgs Interface")]
//public interface IParameterOptionsListChangedEventArgs
//{
//    /// <summary>要更改的参数名称。</summary>
//    String ParameterName { get; }
//};

/// <summary>The parameter option list was changed.</summary>
/// <remarks>The parameter option list was changed.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ParOpListChEvArgsIid)]  // "2AEC279F-EBEC-4806-AA00-CC215432DB82"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterOptionListChangedEventArgs : EventArgs,
    IParameterOptionListChangedEventArgs
{
    /// <summary>Creates an instance of the ParameterOptionListChangedEventArgs class for the parameter.</summary>
    /// <remarks>You can use this constructor when raising the ParameterOptionListChangedEventArgs at run time to  
    /// inform the system that the parameter's option list was changed.</remarks>
    public ParameterOptionListChangedEventArgs(string paramName)
    {
        ParameterName = paramName;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }
}

/// <summary>The restriction to the options list of a parameter was changed.</summary>
/// <remarks>The restriction to the options list of a parameter was changed.</remarks>
[ComVisible(true)]
[Guid(CapeOpenGuids.InParRestToLiChEvArgsIid)]  // "7F357261-095A-4FD4-99C1-ACDAEDA36141"
[Description("ParameterOptionListChangedEventArgs Interface")]
internal interface IParameterRestrictedToListChangedEventArgs
{
    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    string ParameterName { get; }
}

/// <summary>The parameter restriction to the option list was changed.</summary>
/// <remarks>The parameter restriction to the option list was changed.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ParRestToLiChEvArgsIid)]  // "82E0E6C2-3103-4B5A-A5BC-EBAB971B069A"
[ClassInterface(ClassInterfaceType.None)]
public class ParameterRestrictedToListChangedEventArgs : EventArgs,
    IParameterRestrictedToListChangedEventArgs
{
    /// <summary>Creates an instance of the ParameterRestrictedToListChangedEventArgs class for the parameter.</summary>
    /// <remarks>You can use this constructor when raising the ParameterRestrictedToListChangedEventArgs at run time to  
    /// inform the system that the parameter's option list was changed.</remarks>
    public ParameterRestrictedToListChangedEventArgs(
        string paramName, bool wasRestricted, bool isRestricted)
    {
        ParameterName = paramName;
        IsRestricted = isRestricted;
        WasRestricted = wasRestricted;
    }

    /// <summary>要更改的参数名称。</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>The name of the parameter being changed.</value>
    public string ParameterName { get; }

    /// <summary>States whether the value of the parameter is restricted to the values in the options list.</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>Is the parameter value restricted to the list?.</value>
    public bool IsRestricted { get; }

    /// <summary>States whether the value of the parameter was restricted to the values in the options list prior to the 
    /// change to the rested to list property.</summary>
    /// <remarks>The name of the parameter being updated can be used to update GUI information about the PMC.</remarks>
    /// <value>Is the parameter value restricted to the list?.</value>
    public bool WasRestricted { get; }
}

/// <summary>Represents the method that will handle the changing of whether a parameter's value is restricted to those in the option list.</summary>
[ComVisible(false)]
public delegate void ParameterRestrictedToListChangedHandler(object sender,
    ParameterRestrictedToListChangedEventArgs args);

/// <summary>Represents the method that will handle the changing of the Kinetic Reaction Chemistry of a PMC.</summary>
[ComVisible(false)]
public delegate void KineticReactionsChangedHandler(object sender, EventArgs args);

/// <summary>Represents the method that will handle the changing of the Equilibrium Reaction Chemistry of a PMC.</summary>
[ComVisible(false)]
public delegate void EquilibriumReactionsChangedHandler(object sender, EventArgs args);

/// <summary>Base Class defining the actual Parameter quantity.</summary>
[Serializable]
[ComSourceInterfaces(typeof(IParameterEvents))]
[ComVisible(true)]
[Guid(CapeOpenGuids.CapeParameterIid)]  // "F027B4D1-A215-4107-AA75-34E929DD00A5"
[Description("CapeIdentification Interface")]
[ClassInterface(ClassInterfaceType.None)]
[TypeConverter(typeof(ParameterTypeConverter))]
public abstract class CapeParameter 
    : CapeIdentification, ICapeParameter, ICapeParameterSpec, ICapeParameterSpecCOM
{
    // private CapeParamMode _mMode = CapeParamMode.CAPE_INPUT_OUTPUT;
    private CapeParamMode _mMode;

    /// <summary>The flag to indicate parameter validation's status.</summary>
    /// <remarks><para>The flag to indicate parameter validation status. It has three possible values:</para>
    /// <para>(i) notValidated(CAPE_NOT_VALIDATED): The PMC's <c>Validate()</c>
    /// method has not been called after the last time that its value had been changed.</para>
    /// <para>(ii) invalid(CAPE_INVALID): The last time that the PMC's 
    /// <c>Validate()</c> method was called it returned false.</para>
    /// <para>(iii) valid(CAPE_VALID): the last time that the PMC's
    /// Validate() method was called it returned true.</para></remarks>
    protected CapeValidationStatus MValStatus = CapeValidationStatus.CAPE_NOT_VALIDATED;

    /// <summary>Creates a new instance of the abstract parameter base class. </summary>
    /// <remarks>The mode is set to CapeParamMode.CAPE_INPUT_OUTPUT. </remarks>
    /// <param name = "name">Sets as the ComponentName of the parameter's ICapeIdentification interface.</param>
    /// <param name = "description">Sets as the ComponentDescription of the parameter's ICapeIdentification interface.</param>
    /// <param name = "mode">Sets the CapeParamMode mode of the parameter.</param>
    protected CapeParameter(string name, string description, CapeParamMode mode)
        : base(name, description)
    {
        _mMode = mode;
    }

    /// <summary>Occurs when the user validates the parameter.</summary>
    /// <remarks>Raises the ParameterValidated event through a delegate.</remarks>
    public event ParameterValidatedHandler ParameterValidated;

    /// <summary>Occurs when a parameter is validated.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterValidated</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValidated</c> in a derived class, be sure to call the base class's <c>OnParameterValidated</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterValidatedEventArgs">ParameterValidatedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterValidated(ParameterValidatedEventArgs args)
    {
        ParameterValidated?.Invoke(this, args);
    }

    /// <summary>Gets the Specification for this Parameter</summary>
    /// <remarks>Gets the specification of the parameter. The Get method returns the 
    /// specification as an interface to the correct specification type.</remarks>
    /// <value>An object implementing the <see cref = "ICapeParameterSpec"/>, as well as the
    /// appropriate specification for the parameter type, <see cref = "ICapeRealParameterSpec"/> ,
    /// <see cref = "ICapeIntegerParameterSpec"/> , <see cref = "ICapeBooleanParameterSpec"/> , 
    /// <see cref = "ICapeOptionParameterSpec"/> , or <see cref = "ICapeArrayParameterSpec"/> .</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    object ICapeParameter.Specification => this;

    /// <summary>Occurs when the user changes of the value of the parameter.</summary>
    /// <remarks>Raises the ParameterValueChanged event through a delegate.</remarks>
    public event ParameterValueChangedHandler ParameterValueChanged;

    /// <summary>Occurs when the user changes of the value of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterValueChanged</c> in a derived class, be sure to call the base class's <c>OnParameterValueChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "OnParameterValueChanged">OnParameterValueChanged</see> that contains information about the event.</param>
    protected void OnParameterValueChanged(ParameterValueChangedEventArgs args)
    {
        ParameterValueChanged?.Invoke(this, args);
    }

    /// <summary>Gets and sets the value for this Parameter.</summary>
    /// <remarks>This value uses the System.Object data type for compatibility with 
    /// COM-based CAPE-OPEN.</remarks>
    /// <value>System.Object</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public virtual object value { get; set; }
    
    /// <summary>Gets the dimensionality of the parameter.</summary>
    /// <remarks>Physical dimensions are only applicable to real-valued parameters.</remarks>
    /// <value>Null pointer.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    public virtual double[] Dimensionality => null;

    /// <summary>Gets the dimensionality of the parameter.</summary>
    /// <remarks>Physical dimensions are only applicable to real-valued parameters.</remarks>
    /// <value>Null pointer.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Browsable(false)]
    object ICapeParameterSpecCOM.Dimensionality => null;

    /// <summary>Gets the flag to indicate parameter validation's status.</summary>
    /// <remarks><para>Gets the flag to indicate parameter validation status. It has three possible values:</para>
    /// <para>(i) notValidated(CAPE_NOT_VALIDATED): The PMC's <c>Validate()</c>
    /// method has not been called after the last time that its value had been changed.</para>
    /// <para>(ii) invalid(CAPE_INVALID): The last time that the PMC's 
    /// <c>Validate()</c> method was called it returned false.</para>
    /// <para>(iii) valid(CAPE_VALID): the last time that the PMC's
    /// Validate() method was called it returned true.</para></remarks>
    /// <value>The validity status of the parameter, either valid, invalid, or "not validated".</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Category("ICapeParameter")]
    public CapeValidationStatus ValStatus => MValStatus;

    /// <summary>Occurs when the user changes of the default value of the parameter changes.</summary>
    /// <remarks>Raises the ParameterDefaultValueChanged event through a delegate.</remarks>
    public event ParameterDefaultValueChangedHandler ParameterDefaultValueChanged;

    /// <summary>Occurs when the user changes of the default value of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterDefaultValueChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterDefaultValueChanged</c> in a derived class, be sure to call the base class's <c>OnParameterDefaultValueChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "OnParameterDefaultValueChanged">OnParameterDefaultValueChanged</see> that contains information about the event.</param>
    protected void OnParameterDefaultValueChanged(ParameterDefaultValueChangedEventArgs args)
    {
        ParameterDefaultValueChanged?.Invoke(this, args);

        NotifyPropertyChanged("DefaultValue");
    }

    /// <summary>Occurs when the user changes of the mode of the parameter changes.</summary>
    /// <remarks>Raises the ParameterModeChanged event through a delegate.</remarks>
    public event ParameterModeChangedHandler ParameterModeChanged;

    /// <summary>Occurs when the user changes of the mode of a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterModeChanged</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterModeChanged</c> in a derived class, be sure to call the base class's <c>OnParameterModeChanged</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterModeChangedEventArgs">ParameterModeChangedEventArgs</see> that contains information about the event.</param>
    protected void OnParameterModeChanged(ParameterModeChangedEventArgs args)
    {
        ParameterModeChanged?.Invoke(this, args);
    }

    /// <summary>Gets and sets the mode of the parameter.</summary>
    /// <remarks><para>Modes of parameters. It allows the following values:</para>
    /// <para>(i) Input (CAPE_INPUT): the Unit(or whichever owner component) will use 
    /// its value to calculate.</para>
    /// <para>(ii) Output (CAPE_OUTPUT): the Unit will place in the parameter a result 
    /// of its calculations.</para>
    /// <para>(iii) Input-Output (CAPE_INPUT_OUTPUT): the user inputs an 
    /// initial estimation value and the user outputs a calculated value.</para></remarks>
    /// <value>The mode of the parameter, input, output, or input/output.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [Category("ICapeParameter")]
    public CapeParamMode Mode
    {
        get => _mMode;
        set {
            var args = new ParameterModeChangedEventArgs(ComponentName, _mMode, value);
            OnParameterModeChanged(args);
            _mMode = value;
            NotifyPropertyChanged(nameof(Mode));
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
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public abstract bool Validate(ref string message);


    /// <summary>Occurs when the user changes of the parameter value is reset to the default value.</summary>
    /// <remarks>Raises the ParameterReset event through a delegate.</remarks>
    public event ParameterResetHandler ParameterReset;

    /// <summary>Occurs when the user resets a parameter.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnParameterReset</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnParameterReset</c> in a derived class, be sure to call the base class's <c>OnParameterReset</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "ParameterResetEventArgs">ParameterResetEventArgs</see> that contains information about the event.</param>
    protected void OnParameterReset(ParameterResetEventArgs args)
    {
        ParameterReset?.Invoke(this, args);
    }

    /// <summary>Sets the value of the parameter to its default value.</summary>
    /// <remarks> This method sets the parameter's value to the default value.</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    public abstract void Reset();
    
    // ICapeParameterSpec
    /// <summary>Gets the type of the parameter. </summary>
    /// <remarks>Gets the <see cref = "CapeParamType"/> of the parameter for which this is a specification: real 
    /// (CAPE_REAL), integer(CAPE_INT), option(CAPE_OPTION), boolean(CAPE_BOOLEAN) 
    /// or array(CAPE_ARRAY).</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("ICapeParameterSpec")]
    public abstract CapeParamType Type { get; }
}
