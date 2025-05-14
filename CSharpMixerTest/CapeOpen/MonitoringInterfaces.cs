// 大白萝卜重构于 2025.05.12，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

/* IMPORTANT NOTICE

    (c) AsterCHEM, 2008.
    All rights are reserved unless specifically stated otherwise

    Visit the web site at www.amsterchem.com or www.cocosimulator.org

    */

// This file was developed/modified by Jasper van Baten.

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/*****************************************
 * CAPE-OPEN 流程监控界面接口
 * CAPE-OPEN Monitoring Interfaces
*****************************************/

/// <summary>显示受监控流样的解决方案状态。</summary>
/// <remarks>This enumeration provides the flowsheeting monitoring object with information about the solution status of the flowsheet.</remarks>
[Serializable]
[ComVisible(false)]
[Guid(CapeGuids.CapeSolutionStatusIid)]  // "D1B15843-C0F5-4CB7-B462-E1B80456808E"
public enum CapeSolutionStatus
{
    /// <summary>流程图无误解决。</summary>
    CAPE_SOLVED = 0,

    /// <summary>表示没有尝试解决流程图问题。</summary>
    CAPE_NOT_SOLVED = 1,

    /// <summary>最后一次尝试求解流程图时，结果并不理想。</summary>
    CAPE_FAILED_TO_CONVERGE = 2,

    /// <summary>最后一次尝试解算流程图时超时了。</summary>
    CAPE_TIMED_OUT = 3,

    /// <summary>由于内存不足，最后一次尝试解算流程图时失败了。</summary>
    CAPE_NO_MEMORY = 4,

    /// <summary>最后一次尝试解算流程图时，初始化失败。</summary>
    CAPE_FAILED_INITIALIZATION = 5,

    /// <summary>最后一次尝试求解流程图时出现了求解错误。</summary>
    CAPE_SOLVING_ERROR = 6,

    /// <summary>由于操作无效，最后一次尝试解算流程图失败。</summary>
    CAPE_INVALID_OPERATION = 7,

    /// <summary>由于调用顺序无效，最后一次尝试解算流程图失败。</summary>
    CAPE_BAD_INVOCATION_ORDER = 8,

    /// <summary>最后一次尝试求解流程图时出现了计算错误。</summary>
    CAPE_COMPUTATION_ERROR = 9
}

/// <summary>这个接口提供了关于由于值超出其范围而导致的错误的信息。它可以被抛出，以指示方法参数或对象参数值超出范围。</summary>
/// <remarks>Monitoring object should implement:
/// <list type="bullet">
/// <item>ICapeIdentification</item>
/// <item>IPersistStream or IPersistStreamInit in case of persistence implementation</item>
/// <item>ICapeUtilities for parameter collection and Edit and for accepting an ICapeSimulationContext</item>
/// <item>CAPE-OPEN error handling interfaces (ECape...)</item>
/// </list>
/// <para>Monitoring objects can possibly access the following interface via the ICapeSimulationContext interface:</para>
/// <list type="bullet">
/// <item>ICapeCOSEUtilities - for named values</item>
/// <item>ICapeDiagnostic - for logging and popping up messages</item>
/// <item>ICapeMaterialTemplateSystem - for accessing material templates and creation of material objects</item>
/// <item>ICapeFlowsheetMonitoring - for accessing collections of streams and unit operations</item>
/// </list>
/// <para>Monitoring object are NOT SUPPOSED to change flowsheet configuration by:</para>
/// <list type="bullet">
/// <item>modifying unit operation parameters</item>
/// <item>connecting or disconnecting streams to unit operations</item>
/// <item>calculating unit operations</item>
/// <item>modifying streams by setting values</item>
/// <item>any other action that will change the flowsheet state</item>
/// </list>
/// <para>Monitoring object can:</para>
/// <list type="bullet">
/// <item>obtain stream information</item>
/// <item>obtain unit operation information</item>
/// <item>duplicate material objects of streams for performing thermodynamic calculations</item>
/// <item>create material objects via the ICapeMaterialTemplateSystem for creating thermodynamic calculations</item>
/// <item>... </item>
/// </list>
/// <para>In addition to the CAPE-OPEN object category ID, monitoring objects 
/// should expose the Monitoring Object category ID:</para>
/// <para>CATID_MONITORING_OBJECT = 7BA1AF89-B2E4-493d-BD80-2970BF4CBE99</para>
/// </remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeGuids.InCapeFlShMoToIid)]  // "834F65CC-29AE-41c7-AA32-EE8B2BAB7FC2"
[Description("ICapeFlowsheetMonitoring Interface")]
public interface ICapeFlowsheetMonitoring
{
    /// <summary>获取数据流集合。</summary>
    /// <remarks>Get the collection of streams returns an ICapeCollection object
    /// enumerating streams each stream is identified via ICapeIdentification
    /// material streams expose ICapeThermoMaterial or ICapeThermoMaterialObject
    /// energy streams and information streams expose ICapeCollection, each item
    /// in the collection is an ICapeParameter object</remarks>
    /// <returns>An <see cref = "ICapeCollection"/> of unit operations.</returns>
    [DispId(1)]
    [Description("Method GetStreamCollection.")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetStreamCollection();

    /// <summary>获取单元操作集合。</summary>
    /// <remarks>Get the collection of unit operations returns an ICapeCollection object
    /// enumerating unit operations each unit operation is identified via ICapeIdentification
    /// unit operations also expose ICapeUnit, and possibly ICapeUtilities (for parameter access).</remarks>
    /// <returns>An <see cref = "ICapeCollection"/> of streams.</returns>
    [DispId(2)]
    [Description("Method GetUnitOperationCollection")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetUnitOperationCollection();

    /// <summary>获取流程表的当前解决方案状态。</summary>
    /// <remarks>Gets the <see cref = "CapeSolutionStatus"/> of the flowsheet.</remarks>
    /// <value>The <see cref = "CapeSolutionStatus"/> of the flowsheet.</value>
    // 获取解决方案状态
    //[id(3), prop get, help string("Solution status")]
    [DispId(3)]
    [Description("Solution status.")]
    CapeSolutionStatus SolutionStatus { get; }

    /// <summary>获取流程表的验证状态。</summary>
    /// <remarks>Gets the <see cref = "CapeValidationStatus"/> of the flowsheet.</remarks>
    /// <returns>The <see cref = "CapeValidationStatus"/> of the flowsheet.</returns>
    [DispId(4)]
    [Description("Get the flowsheet validation status.")]
    CapeValidationStatus ValStatus { get; }
}

/// <summary>这个接口提供了关于由于值超出其范围而导致的错误的信息。它可以被抛出，以指示方法参数或对象参数值超出范围。</summary>
/// <remarks>Monitoring object should implement:
/// <list type="bullet">
/// <item>ICapeIdentification</item>
/// <item>IPersistStream or IPersistStreamInit in case of persistence implementation</item>
/// <item>ICapeUtilities for parameter collection and Edit and for accepting an ICapeSimulationContext</item>
/// <item>CAPE-OPEN error handling interfaces (ECape...)</item>
/// </list>
/// <para>Monitoring objects can possibly access the following interface via the ICapeSimulationContext interface:</para>
/// <list type="bullet">
/// <item>ICapeCOSEUtilities - for named values</item>
/// <item>ICapeDiagnostic - for logging and popping up messages</item>
/// <item>ICapeMaterialTemplateSystem - for accessing material templates and creation of material objects</item>
/// <item>ICapeFlowsheetMonitoring - for accessing collections of streams and unit operations</item>
/// </list>
/// <para>Monitoring object are NOT SUPPOSED to change flowsheet configuration by:</para>
/// <list type="bullet">
/// <item>modifying unit operation parameters</item>
/// <item>connecting or disconnecting streams to unit operations</item>
/// <item>calculating unit operations</item>
/// <item>modifying streams by setting values</item>
/// <item>any other action that will change the flowsheet state</item>
/// </list>
/// <para>Monitoring object can:</para>
/// <list type="bullet">
/// <item>obtain stream information</item>
/// <item>obtain unit operation information</item>
/// <item>duplicate material objects of streams for performing thermodynamic calculations</item>
/// <item>create material objects via the ICapeMaterialTemplateSystem for creating thermodynamic calculations</item>
/// <item>... </item>
/// </list>
/// <para>In addition to the CAPE-OPEN object category ID, monitoring objects 
/// should expose the Monitoring Object category ID:</para>
/// <para>CATID_MONITORING_OBJECT = 7BA1AF89-B2E4-493d-BD80-2970BF4CBE99</para></remarks>
[ComImport, ComVisible(false)]
[Guid(CapeGuids.ICapeFlowsheetMonitoring_IID)]
[Description("ICapeFlowsheetMonitoring Interface")]
public interface ICapeFlowsheetMonitoringOld
{
    /// <summary>获取数据流集合。</summary>
    /// <remarks>Get the collection of streams returns an ICapeCollection object
    /// enumerating streams each stream is identified via ICapeIdentification
    /// material streams expose ICapeThermoMaterial or ICapeThermoMaterialObject
    /// energy streams and information streams expose ICapeCollection, each item
    /// in the collection is an ICapeParameter object.</remarks>
    /// <returns>An ICapeCollection of unit operations.</returns>
    [DispId(1)]
    [Description("Method GetStreamCollection.")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetStreamCollection();

    /// <summary>获取单元操作集合。</summary>
    /// <remarks>Get the collection of unit operations returns an ICapeCollection object
    /// enumerating unit operations each unit operation is identified via ICapeIdentification
    /// unit operations also expose ICapeUnit, and possibly ICapeUtilities (for parameter access).</remarks>
    /// <returns>An ICapeCollection of streams.</returns>
    [DispId(2)]
    [Description("Method GetUnitOperationCollection")]
    [return: MarshalAs(UnmanagedType.IDispatch)]
    object GetUnitOperationCollection();

    /// <summary>检查流程图当前是否已解决。</summary>
    /// <remarks>Check whether the flowsheet is currently solved.</remarks>
    /// <returns><para>true, if the unit is solved.</para>
    /// <para>false, if the unit is not solved.</para></returns>
    [DispId(3)]
    [Description("Method IsSolved")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsSolved();

    /// <summary>检查流程图是否有效。</summary>
    /// <remarks>Check whether the flowsheet is valid.</remarks>
    /// <value>The validation status of the flowsheet.</value>
    [DispId(4)]
    [Description("Get the flowsheet validation status.")]
    CapeValidationStatus ValStatus { get; }
}