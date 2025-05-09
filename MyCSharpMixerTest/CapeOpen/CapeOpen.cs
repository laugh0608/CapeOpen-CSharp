using System.Runtime.InteropServices;

// <summary>
// CapeOpen 命名空间提供了 CAPE-OPEN 接口的 .Net 翻译，
// 并实现了 CAPE-OPEN 对象模型的核心组件。
// 有关 CAPE-OPEN 的更多信息，请访问 CO-Lan 网站：http://www.co-lan.org。
// </summary>

namespace CapeOpen;

// 通用代码示例：
// [Serializable]
// 控制程序集中单独托管类型或成员，或所有类型对 COM 的访问性，使其可见。
// [System.Runtime.InteropServices.ComVisible(true)]
// 为 ICapeThermoMaterialObject_IID 设置 GUID 值，详见 CAPE-OPEN 接口文档。
// [System.Runtime.InteropServices.Guid("C79CAFD3-493B-46dc-8585-1118A0B5B4AB")] // 必需项
// 该类在新版本中已弃用，请使用 System.ComponentModel.DescriptionAttribute 代替。
// 指定属性或事件的说明。
// [System.ComponentModel.Description("")]
// 对 CAPE-OPEN 组件的描述，将被用来设置 CapeDescription[Description] 注册键的值。
// [CapeOpen.CapeNameAttribute("MixerExample")]
// 同上。（luobo 注：我也不明白为什么要重复定义一个属性，应该是官方文档中的描述有问题）
// [CapeOpen.CapeDescriptionAttribute("An example mixer unit operation.")]
// 该组件所支持的 CAPE-OPEN 版本号，用来设置 CapeDescription[CapeVersion] 注册键的值。
// [CapeOpen.CapeVersionAttribute("1.0")]
// 该组件的供应商 URL，用来设置 CapeDescription[VendorURL] 注册键的值。
// [CapeOpen.CapeVendorURLAttribute("http:\\www.epa.gov")]
// 该组件的帮助 URL，用来设置 CapeDescription[HelpURL] 注册键的值。
// [CapeOpen.CapeHelpURLAttribute("http:\\www.epa.gov")]
// 该组件的关于信息，用来设置 CapeDescription[About] 注册键的值。
// [CapeOpen.CapeAboutAttribute("US Environmental Protection Agency\nCincinnati, Ohio")]

/// <summary>
/// 为注册 CAPE-OPEN 对象提供文本名称
/// </summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeName[Name] 注册键的值
/// </para></remarks>
// 特性代码示例：
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeNameAttribute : Attribute
{
    /// <summary>初始化 CapeDescriptionAttribute 类的新实例</summary>
    /// <remarks>注册功能使用描述值设置 CapeDescription[Description] 注册密钥的值</remarks>
    /// <param name = "name">CAPE-OPEN 组件说明</param>
    public CapeNameAttribute(String name)
    {
        Name = name;
    }

    /// <summary>获取名称信息</summary>
    /// <remarks>名称的值</remarks>
    /// <value>CAPE-OPEN 组件名称</value>
    public String Name { get; }
}

/// <summary>为 CAPE-OPEN 对象的注册提供文字说明</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeDescription[Description] 注册键的值
/// </para></remarks>
// 特性代码示例：
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeDescriptionAttribute : Attribute
{
    /// <summary>初始化 CapeDescriptionAttribute 类的新实例</summary>
    /// <remarks>注册功能使用描述值设置 CapeDescription[Description] 注册密钥的值</remarks>
    /// <param name = "description">CAPE-OPEN 组件说明</param>
    public CapeDescriptionAttribute(String description)
    {
        Description = description;
    }

    /// <summary>获取描述</summary>
    /// <remarks>描述的值</remarks>
    /// <value>CAPE-OPEN 组件描述</value>
    public String Description { get; }
}

/// <summary>为注册 CAPE-OPEN 对象提供 CAPE-OPEN 版本号</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeDescription[CapeVersion] 注册键的值
/// </para></remarks>
// 特性代码示例：
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeVersionAttribute : Attribute
{
    /// <summary>初始化 CapeVersionAttribute 类的新实例</summary>
    /// <remarks>设置 CapeDescription[CapeVersion] 注册键值</remarks>
    /// <param name = "version">该对象支持的 CAPE-OPEN 接口的版本</param>
    public CapeVersionAttribute(String version)
    {
        Version = version;
    }

    /// <summary>获取版本号</summary>
    /// <remarks>版本号的值</remarks>
    /// <value>CAPE-OPEN 组件支持的版本号</value>
    public String Version { get; }
}

/// <summary>为注册 CAPE-OPEN 对象提供供应商链接</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeDescription[VendorURL] 注册密钥的值
/// </para></remarks>
// 特性代码示例：
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeVendorUrlAttribute : Attribute
{
    /// <summary>初始化 CapeVendorURLAttribute 类的新实例</summary>
    /// <remarks>注册功能使用描述值来设置 CapeDescription[VendorURL] 注册密钥的值。</remarks>
    /// <param name = "vendorUrl">CAPE-OPEN 组件的供应商链接</param>
    public CapeVendorUrlAttribute(String vendorUrl)
    {
        VendorUrl = vendorUrl;
    }

    /// <summary>获取供应商链接信息</summary>
    /// <remarks>供应商链接的值</remarks>
    /// <value>CAPE-OPEN 组件的供应商链接</value>
    public String VendorUrl { get; }
}

/// <summary>为注册 CAPE-OPEN 对象提供帮助 URL。</summary>
/// <remarks><para>
/// 该属性用于在 CAPE-OPEN 对象与 COM 注册表注册 CAPE-OPEN 对象时使用，以设置 CapeDescription[HelpURL] 注册键的值。
/// </para></remarks>
// 特性代码示例：
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeHelpUrlAttribute : Attribute
{
    /// <summary>初始化 CapeHelpURLAttribute 类的新实例。</summary>
    /// <remarks>注册函数使用帮助 URL 的值来设置 CapeDescription[HelpURL] 注册密钥的值。</remarks>
    /// <param name = "helpUrl">CAPE-OPEN 组件帮助链接</param>
    public CapeHelpUrlAttribute(String helpUrl)
    {
        HelpUrl = helpUrl;
    }

    /// <summary>获取帮助链接信息</summary>
    /// <remarks>帮助链接的值</remarks>
    /// <value>CAPE-OPEN 组件的帮助链接</value>
    public String HelpUrl { get; }
}

/// <summary>提供有关 CAPE-OPEN 对象注册信息的文本。</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeDescription[About] 注册键的值。
/// </para></remarks>
// 特性代码示例：
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeAboutAttribute : Attribute
{
    /// <summary>初始化 CapeAboutAttribute 类的新实例。</summary>
    /// <remarks>注册功能使用关于文本的值来设置 CapeDescription[About] 注册密钥的值。</remarks>
    /// <param name = "about">CAPE-OPEN 组件关于信息</param>
    public CapeAboutAttribute(String about)
    {
        About = about;
    }

    /// <summary>获取关于信息</summary>
    /// <remarks>关于信息的值</remarks>
    /// <value>CAPE-OPEN 组件关于信息</value>
    public String About { get; }
}

/// <summary>提供有关对象是否为 CAPE-OPEN 单元的信息，在注册 CAPE-OPEN 对象时使用。</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时，将 CapeUnitOperation_CATID 添加到对象的注册密钥中。
/// </para></remarks>
// 特性代码示例：
// [CapeOpen.CapeUnitOperation_CATID(true)]
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeUnitOperationAttribute : Attribute
{
    /// <summary>初始化 CapeUnitOperationAttribute 类的新实例。</summary>
    /// <remarks>该属性用于指示对象是否为 CAPE-OPEN 单元操作。COM 注册功能也使用该属性在系统注册表中为该对象设置适当的 CATID 值。</remarks>
    /// <param name = "isUnit">CAPE-OPEN 组件是一个 CAPE-OPEN 单元操作对象。</param>
    public CapeUnitOperationAttribute(bool isUnit)
    {
        IsUnit = isUnit;
    }

    /// <summary>获取是否是单元操作对象值</summary>
    /// <remarks>该属性表示对象是否使用 CAPE-OPEN Unti 操作接口。</remarks>
    /// <value>布尔值，表示 CAPE-OPEN 组件是否为单元操作。</value>
    public bool IsUnit { get; }
}

/// <summary>提供在注册 CAPE-OPEN 对象时使用的有关对象是否支持流表监控的信息。</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表中注册 CAPE-OPEN 对象时，将 CATID_MONITORING_OBJECT 添加到对象的注册密钥中。
/// </para></remarks>
// 特性代码示例：
// [CapeOpen.CapeFlowsheetMonitoringAttribute(true)]
// public class WARAddIn : CapeObjectBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeFlowsheetMonitoringAttribute : Attribute
{
    /// <summary>初始化 CapeFlowsheetMonitoringAttribute 类的新实例。</summary>
    /// <remarks>该属性用于指示对象是否使用了 PME 的流量表监控功能。COM 注册功能也使用该属性在系统注册表中为该对象设置适当的 CATID 值。</remarks>
    /// <param name = "monitors">CAPE-OPEN 组件是一个流量表监控对象。</param>
    public CapeFlowsheetMonitoringAttribute(bool monitors)
    {
        Monitors = monitors;
    }

    /// <summary>获取是否是流量表监控对象值</summary>
    /// <remarks>该属性表示对象是否使用 PME 的流表监控接口。</remarks>
    /// <value>布尔值，表示 CAPE-OPEN 组件是否支持流量表监控。</value>
    public bool Monitors { get; }
}

/// <summary>提供有关对象在注册 CAPE-OPEN 对象时是否消耗热力学的信息。</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表中注册 CAPE-OPEN 对象时，将 Consumes_Thermo_CATID 添加到对象的注册密钥中。
/// </para></remarks>
// 特性代码示例：
// [CapeOpen.CapeConsumesThermoAttribute(true)]
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeConsumesThermoAttribute : Attribute
{
    /// <summary>初始化 CapeConsumesThermoAttribute 类的新实例。</summary>
    /// <remarks>该属性用于指示对象是否消耗热力学模型。COM 注册功能也使用它在系统注册表中放置适当的 CATID 值。</remarks>
    /// <param name = "consumes">布尔值，表示 CAPE-OPEN 组件是否消耗热力学。</param>
    public CapeConsumesThermoAttribute(bool consumes)
    {
        ConsumesThermo = consumes;
    }

    /// <summary>获取对象是否消耗热力学模型的信息。</summary>
    /// <remarks>该属性表示对象是否消耗热动力。</remarks>
    /// <value>该组件是否消耗热动力值</value>
    public bool ConsumesThermo { get; }
}

/// <summary>在注册 CAPE-OPEN 对象时，提供对象是否支持热力学 1.0 版本的信息。</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表中注册 CAPE-OPEN 对象时，将 SupportsThermodynamics10_CATID 添加到对象的注册密钥中。
/// </para></remarks>
// 特性代码示例：
// [CapeOpen.CapeConsumesThermoAttribute(true)]
// [CapeOpen.CapeSupportsThermodynamics10Attribute(true)]
// public class CMixerExample : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeSupportsThermodynamics10Attribute : Attribute
{
    /// <summary>初始化 CapeSupportsThermodynamics10Attribute 类的新实例。</summary>
    /// <remarks>该属性用于指示对象是否支持热力学 1.0 版本。COM 注册功能也使用它在系统注册表中放置适当的 CATID 值。</remarks>
    /// <param name = "supported">CAPE-OPEN 组件是否支持热力学 1.0 版本</param>
    public CapeSupportsThermodynamics10Attribute(bool supported)
    {
        Supported = supported;
    }

    /// <summary>是否支持热力学 1.0 版本的信息</summary>
    /// <remarks>表示该组件是否支持热力学 1.0 版本</remarks>
    /// <value>是否支持热力学 1.0 版本的布尔值</value>
    public bool Supported { get; }
}

/// <summary>在注册 CAPE-OPEN 对象时，提供对象是否支持热力学 1.1 版本的信息。</summary>
/// <remarks><para>
/// 该属性用于在 COM 注册表中注册 CAPE-OPEN 对象时，将 SupportsThermodynamics11_CATID 添加到对象的注册密钥中。
/// </para></remarks>
// 特性代码示例：
// [CapeOpen.CapeConsumesThermoAttribute(true)]
// [CapeOpen.CapeSupportsThermodynamics11Attribute(true)]
// public class CMixerExample110 : public CUnitBase
[ComVisible(false)]
[AttributeUsage(AttributeTargets.Class)]
public class CapeSupportsThermodynamics11Attribute : Attribute
{
    /// <summary>初始化 CapeSupportsThermodynamics11Attribute 类的新实例。</summary>
    /// <remarks>该属性用于指示对象是否支持热力学 1.1 版本。COM 注册功能也使用它在系统注册表中放置适当的 CATID 值。</remarks>
    /// <param name = "supported">CAPE-OPEN 组件是否支持热力学 1.1 版本</param>
    public CapeSupportsThermodynamics11Attribute(bool supported)
    {
        Supported = supported;
    }

    /// <summary>是否支持热力学 1.1 版本的信息</summary>
    /// <remarks>表示该组件是否支持热力学 1.1 版本</remarks>
    /// <value>是否支持热力学 1.1 版本的布尔值</value>
    public bool Supported { get; }
}

// 原始相关信息：
/*
[odl,uuid(B777A1BD-0C88-11D3-822E-00C04F4F66C9),version(20.0),
helpstring("IATCapeXRealParameterSpec Interface"),
dual,oleautomation]
interface IATCapeXRealParameterSpec : IDispatch {
[id(0x60040003), propget, helpstring(" Provide the Aspen Plus display units for this parameter.")]
HRESULT DisplayUnits([out, retval] BSTR* bsUOM);
};

typedef [version(1.0)]
enum {
ErrorSeverityTerminal = 0,
ErrorSeveritySevere = 1,
ErrorSeverityError = 2,
ErrorSeverityWarning = 3
} __MIDL___MIDL_itf_AspenCapeX_0244_0001;

[odl,uuid(B777A1B9-0C88-11D3-822E-00C04F4F66C9),version(1.0),
hidden,dual,nonextensible,oleautomation]
interface IATCapeXDiagnostic : IDispatch {
[id(0x60040000), helpstring("Print a message to the history device.")]
HRESULT SendMsgToHistory([in] BSTR message);
[id(0x60040001), helpstring("Print a message to the terminal device.")]
HRESULT SendMsgToTerminal([in] BSTR message);
[id(0x60040002), helpstring("Signal a simulation error.")]
HRESULT RaiseError(
[in] ErrorSeverity severity,
[in] BSTR context,
[in] BSTR message);
}
*/