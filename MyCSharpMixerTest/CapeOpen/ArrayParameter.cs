// 大白萝卜重构于 2025.05.10，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

internal class ArrayParameter { }

/// <summary>用于 CAPE-OPEN <see cref="ParameterCollection">ParameterCollection</see> 的数组值参数的包装器。</summary>
/// <remarks>包括一个 CAPE-OPEN 数组值参数，用于 CAPE-OPEN <see cref="ParameterCollection"/> 参数集合。</remarks>
[Serializable]
[ComSourceInterfaces(typeof(IRealParameterSpecEvents))]
[ComVisible(true)]
[Guid(CapeOpenGuids.ArrParaWrapperIid)] // ICapeThermoMaterialObject_IID 277E2E39-70E7-4FBA-89C9-2A19B9D5E576
[ClassInterface(ClassInterfaceType.None)]
internal class ArrayParameterWrapper : CapeParameter,
    ICapeParameter, ICapeParameterSpec, ICapeArrayParameterSpec
{
    [NonSerialized] private ICapeParameter _mParameter;

    /// <summary>为基于 COM 的数组值参数类创建包装类的新实例。</summary>
    /// <remarks>基于 COM 的数组参数被封装并暴露给基于 .NET 的 PME 和 PMC。</remarks>
    /// <param name = "parameter">要包装的基于 COM 的数组参数。</param>
    public ArrayParameterWrapper(ICapeParameter parameter) 
        : base(((ICapeIdentification)parameter).ComponentName, ((ICapeIdentification)parameter).ComponentDescription,
            parameter.Mode)
    {
        _mParameter = parameter;
    }

    // ICloneable
    /// <summary>创建参数的副本。这两个副本引用同一个基于 COM 的数组参数。</summary>
    /// <remarks>克隆方法用于创建参数的副本。原始对象和克隆对象都包裹相同的包裹参数实例。</remarks>
    /// <returns>A copy of the current parameter.</returns>
    public override object Clone()
    {
        return new ArrayParameterWrapper(_mParameter);
    }

    /// <summary>根据参数的规范验证参数的当前值。</summary>        
    /// <remarks>包装的参数根据其内部验证标准验证自身。</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public override bool Validate(ref string message)
    {
        var valStatus = _mParameter.ValStatus;
        var returnVal = _mParameter.Validate(message);
        var args = new ParameterValidatedEventArgs(ComponentName, message, ValStatus, _mParameter.ValStatus);
        OnParameterValidated(args);
        NotifyPropertyChanged("ValStatus");
        return returnVal;
    }

    /// <summary>将参数值设置为默认值。</summary>
    /// <remarks>此方法将参数值设置为默认值。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    public override void Reset()
    {
        var args = new ParameterResetEventArgs(ComponentName);
        _mParameter.Reset();
        NotifyPropertyChanged("Value");
        OnParameterReset(args);
    }

    // ICapeParameterSpec
    /// <summary>获取参数的类型。</summary>
    /// <remarks>Gets the <see cref = "CapeParamType"/> of the parameter.</remarks>
    /// <value>The parameter type. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(false)]
    [Category("ICapeParameterSpec")]
    public override CapeParamType Type => CapeParamType.CAPE_ARRAY;

    //ICapeArrayParameterSpec
    /// <summary>获取参数规范的数组。</summary>
    /// <remarks>获取参数值中每个项的规格的数组。Get 方法返回正确规格类型（<see cref="ICapeRealParameterSpec"/>,
    /// <see cref="ICapeOptionParameterSpec"/>, <see cref= "ICapeIntegerParameterSpec"/>，
    /// 或 <see cref="ICapeBooleanParameterSpec"/>）的接口数组。请注意，例如，也可以配置数组数组，这与二维矩阵类似但不完全相同。</remarks>
    /// <value>An array of parameter specifications. </value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Browsable(false)]
    object[] ICapeArrayParameterSpec.ItemsSpecifications => ((ICapeArrayParameterSpec)_mParameter.Specification).ItemsSpecifications;

    /// <summary>获取参数中数组值的维数。</summary>
    /// <remarks>参数中数组值的维数。</remarks>
    /// <value>参数中数组值的维数。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    int ICapeArrayParameterSpec.NumDimensions => ((ICapeArrayParameterSpec)_mParameter.Specification).NumDimensions;

    /// <summary>获取数组每个维度的大小。</summary>
    /// <remarks>获取数组每个维度的大小。</remarks>			
    /// <value>一个整数数组，包含数组中每个维度的大小。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed.</exception>
    [Category("Parameter Specification")]
    int[] ICapeArrayParameterSpec.Size => ((ICapeArrayParameterSpec)_mParameter.Specification).Size;

    /// <summary>确定值对于包装的参数是否有效。</summary>
    /// <remarks>验证数组是否符合参数的规范。它返回一个标志，指示验证是否成功或失败，
    /// 以及可用于向客户端/用户传达推理的文本消息。包裹的参数根据其内部验证标准验证值。</remarks>
    /// <returns>True if the parameter is valid, false if not valid.</returns>
    /// <param name = "mValue">The value to be checked.</param>
    /// <param name = "messages">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    object ICapeArrayParameterSpec.Validate(object mValue, ref string[] messages)
    {
        return ((ICapeArrayParameterSpec)_mParameter.Specification).Validate(mValue, ref messages);
    }
}