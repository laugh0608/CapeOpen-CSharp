// 大白萝卜重构于 2025.05.11，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2002.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org

This file has been edited using the editor from Microsoft Visual Studio 6.0
This file can view properly with any basic editors and browsers (validation done under MS Windows and Unix)
*/

// This file was developed/modified by JEAN-PIERRE-BELAUD for CO-LaN organisation - August 2003

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

// ---------------------------------------------------------
// ---- COSE 接口的职责范围 ----------------------------------
// ---------------------------------------------------------

/// <summary>包含诊断功能。</summary>
/// <remarks>由 PME 支持的接口，用于将指向 ICapeUtilities:SetSimulation 的引用传递给 PMC。
/// 然后，PMC 可以使用 PME COSE 接口中的任何接口。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeGuids.ICapeSimulationContext_IID)]
[Description("ICapeSimulation Context Interface")]
public interface ICapeSimulationContext
{
    // 这里没有暂时定义方法...
}

// ICapeDiagnostic接口的 .NET 翻译。
/// <summary>提供一种向用户提供详细消息的机制。</summary>
/// <remarks>从 PMC 向 PME（进而向用户）传递冗余信息。PMC 应该在执行流程图时能够向用户记录或显示信息。
/// 与其让每个 PMC 通过不同的机制执行这些任务，不如将它们全部重定向到 PME 服务以与用户通信。
/// 错误通用接口无法满足这些要求，因为它们会停止 PMC 代码的执行并向 PME 发出异常情况信号。
/// 该文件处理简单信息或警告消息的传递。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeGuids.ICapeDiagnostic_IID)]
[Description("ICapeDiagnostic Interface")]
public interface ICapeDiagnostic
{
    /// <summary>向终端写入消息。</summary>
    /// <remarks>将字符串写入终端。当需要引起用户注意的消息时调用此方法。实现应确保将字符串写入对话框或消息列表，
    /// 以便用户可以轻松查看。预先，此消息必须尽快显示给用户。</remarks>
    /// <param name = "message">The text to be displayed.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1), Description("Method PopUpMessage")]
    void PopUpMessage(string message);

    /// <summary>将字符串写入 PME 的日志文件。</summary>
    /// <remarks>将字符串写入日志。当需要记录消息进行日志记录时，会调用此方法。预计实现会将字符串写入日志文件或其他日志设备。</remarks>
    /// <param name = "message">The text to be logged.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Method LogMessage")]
    void LogMessage(string message);
}

/// <summary>创建指定类型的热力学流股对象模板。</summary>
/// <remarks>当一个单元操作需要获得热力学计算时，它通常会在连接到单元端口的流股对象上执行这些计算。
/// 然而，在某些情况下，例如蒸馏塔，可能需要使用不同的属性包。甚至可以要求用户选择必须使用的热力学模型。
/// 所有访问 CAPE-OPEN 属性包的机制都已经在 COSE 中，作为使用 CAPE-OPEN 属性包所需的功能的一部分。
/// 因此，将执行此选择和创建热力学引擎的责任委托给 COSE，而不是每个 PMC 实现支持，将导致更薄和更易于编码的单元操作组件。
/// 如果流股对象模板的配置在 PME 侧，则单元操作仅需要额外的功能即可访问已配置的物料模板列表并从中选择一个。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeGuids.ICapeMaterialTemplateSystem_IID)]
[Description("ICapeMaterialTemplateSystem Interface")]
public interface ICapeMaterialTemplateSystem
{
    /// <summary>创建指定类型的热力学流股对象模板。</summary>
    /// <remarks>当一个单元操作需要获得热力学计算时，它通常会在连接到单元端口的材料对象上执行这些计算。
    /// 然而，在某些情况下，例如蒸馏塔，可能需要使用不同的属性包。甚至可以要求用户选择必须使用的热力学模型。
    /// 所有访问 CAPE-OPEN 属性包的机制都已经在 COSE 接口中，作为使用 CAPE-OPEN 属性包所需的功能的一部分。
    /// 因此，将执行此选择和创建热力学引擎的责任委托给 COSE，而不是每个 PMC 实现支持，将导致更薄和更易于编码的单元操作组件。
    /// 如果流股对象模板的配置在 PME 侧，则单元操作仅需要额外的功能即可访问已配置的物料模板列表并从中选择一个。</remarks>
    /// <value>返回 COSE 支持的流股模板名称的字符串数组。这可以包括：
    /// <list type="bullet"><item>CAPE-OPEN 独立的物性包，</item>
    /// <item>CAPE-OPEN 依赖于热力学系统的物性包，</item>
    /// <item>COSE 本身自带的物性包。</item></list></value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(1)]
    [Description("property MaterialTemplates")]
    object MaterialTemplates { get; }

    /// <summary>创建指定类型的热力学流股对象模板。</summary>
    /// <remarks>当一个单元操作需要获得热力学计算时，它通常会在连接到单元端口的材料对象上执行这些计算。
    /// 然而，在某些情况下，例如蒸馏塔，可能需要使用不同的属性包。甚至可以要求用户选择必须使用的热力学模型。
    /// 所有访问 CAPE-OPEN 属性包的机制都已经在 COSE 接口中，作为使用 CAPE-OPEN 属性包所需的功能的一部分。
    /// 因此，将执行此选择和创建热力学引擎的责任委托给 COSE，而不是每个 PMC 实现支持，将导致更薄和更易于编码的单元操作组件。
    /// 如果流股对象模板的配置在 PME 侧，则单元操作仅需要额外的功能即可访问已配置的物料模板列表并从中选择一个。</remarks>
    /// <returns>返回COSE支持的流股对象模板名称的字符串数组。这可以包括：CAPE-OPEN 独立的物性包，
    /// CAPE-OPEN 依赖于热力学系统的物性包，COSE 本身自带的物性包。</returns>
    /// <param name = "materialTemplateName">The name of the material template to be resolved (which must be included in the list returned by MaterialTemplates)</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("Method CreateMaterialTemplate")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object CreateMaterialTemplate(string materialTemplateName);
}

// ICapeCOSEUtilities 接口的 .NET 翻译。
/// <summary>为 PMC 提供一种从 PME 获取自由 FORTRAN 通道的机制。</summary>
/// <remarks>当 PMC 封装 FORTRAN DLL 时，如果 PMC 与 PME（如 Simulator Execution）在同一进程中加载，
/// 可能会遇到技术问题。在这种情况下，如果两个 FORTRAN 模块都选择相同地输出通道进行 FORTRAN 消息传递，
/// 则它们之间可能会出现冲突。因此，PME 应集中生成每个可能需要它们的 PMC 的唯一输出通道。这一要求仅
/// 在 PME 和 PMC 属于同一计算进程时出现，显然，这种 FORTRAN 通道功能仅在架构不是分布式时适用。由于我们将来
/// 可以交换此类信息，因此必须建立一个通用且可扩展的机制。调用模式是一个很好的候选者。因此，FORTRAN 通道的特定字符串值将被标准化。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeGuids.ICapeCOSEUtilities_IID)]
[Description("ICapeCOSEUtilities Interface")]
public interface ICapeCOSEUtilities
{
    /// <summary>PME 支持的命名值列表。</summary>
    /// <value>COSE 支持的命名值字符串数组列表。此列表应包括 Free FORTRAN 通道的命名值，该值将提供 Free FORTRAN 通道的名称。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [DispId(1), Description("property NamedValueList")]
    object NamedValueList { get; }

    /// <summary>PME 支持的命名值列表。</summary>
    /// <remarks>返回与名为 name 的值相对应的值。请注意，连续两次传递相同名称的调用可能会返回不同的值。
    /// 每次为该属性调用 NamedValue 时，COSE 都会返回不同的 FORTRAN 通道。COSE 可能不会使用任何
    /// 返回的 FORTRAN 通道用于任何内部使用的 FORTRAN 模块。</remarks>
    /// <returns>Name of the requested value (which must be included in the list returned by NamedValueList).</returns>
    /// <param name = "value">Name of the requested value (which must be included in the list returned by 
    /// <see cref="ICapeCOSEUtilities.NamedValueList"/>).</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    [DispId(2), Description("property NamedValue")]
    object NamedValue(string value);
}