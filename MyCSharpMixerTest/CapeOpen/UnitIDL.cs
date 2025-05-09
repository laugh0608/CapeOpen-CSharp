using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>连接端口的物体或信息的预期流动方向（如物质、能量或信息物体）。</summary>
/// <remarks>该枚举为流程表工具提供了与端口方向有关的信息，即端口是接收物料、信息或能量，还是输出物料、信息或能量。
/// 流程图可以利用这些信息来帮助选择连接材料、信息或能量对象的端口。</remarks>
[Serializable]
public enum CapePortDirection
{
    /// <summary>表示单元模块的进口端口。</summary>
    CAPE_INLET = 0,

    /// <summary>表示单元模块的出口端口。</summary>
    CAPE_OUTLET = 1,

    /// <summary>表示一个端口，可作为单元模块的入口或出口。</summary>
    CAPE_INLET_OUTLET = 2
}

/// <summary>可从连接对象流入单元操作的对象或信息类型。</summary>
/// <remarks>该枚举为流程设计工具提供了与端口类型有关的信息，即单元操作使用的端口所连接的对象是物 质、信息还是能量。
/// 流程图可以利用这些信息来帮助选择附加到物料、信息或能量对象上的端口。</remarks>
[Serializable]
public enum CapePortType
{
    /// <summary>表示预计有物料流通过该端口进入单元模块。</summary>
    CAPE_MATERIAL = 0,

    /// <summary>表示预计有能量流经此端口进入单元模块。</summary>
    CAPE_ENERGY = 1,

    /// <summary>表示预计将有信息流通过该端口进入单元模块。</summary>
    CAPE_INFORMATION = 2,

    /// <summary>表示物料、能量或信息可以通过该端口流向单元模块。</summary>
    CAPE_ANY = 3
}

/// <summary>代表处理单元操作验证的方法。</summary>
[ComVisible(false)]
public delegate void UnitOperationValidatedHandler(object sender, UnitOperationValidatedEventArgs args);

/// <summary>对单元模块运行状态进行了验证。</summary>
/// <remarks>提供有关单元模块运行验证的信息。</remarks>
[ComVisible(true)]
[Guid("50A759AF-5E38-4399-9050-93F823E5A6E6")]
[Description("IUnitOperationValidatedEventArgs Interface")]
internal interface IUnitOperationValidatedEventArgs
{
    /// <summary>更改单元操作的名称。</summary>
    string UnitOperationName { get; }

    /// <summary>单元操作验证产生的信息。</summary>
    /// <remarks>该信息提供有关验证过程结果的信息。</remarks>
    /// <value>关于验证过程的信息。</value>
    string Message { get; }

    /// <summary>验证前单元操作的验证状态。</summary>
    /// <remarks>通知用户验证过程的结果。</remarks>
    /// <value>验证前单元操作的验证状态。</value>
    CapeValidationStatus OldStatus { get; }

    /// <summary>验证后单元模块的验证状态。</summary>
    /// <remarks>告知用户验证过程的结果。</remarks>
    /// <value>验证后单元模块验证状态值。</value>
    CapeValidationStatus NewStatus { get; }
}

/// <summary>单元操作已验证。</summary>
/// <remarks>提供有关单元操作验证的信息。</remarks>
[Serializable, ComVisible(true)]
[Guid("9147E78B-29D6-4D91-956E-75D0FB90CEA7")]
[ClassInterface(ClassInterfaceType.None)]
public class UnitOperationValidatedEventArgs : EventArgs,
    IUnitOperationValidatedEventArgs
{
    /// <summary>为单元操作创建一个 UnitValidatedEventArgs 类实例。</summary>
    /// <remarks>在运行时触发 UnitValidatedEventArgs 时，可以使用此构造函数来获取参数验证信息。</remarks>
    /// <param name = "unitName">正在更改的单元操作名称。</param>
    /// <param name = "message">显示单元操作验证结果的消息。</param>
    /// <param name = "oldStatus">单元操作验证前的状态。</param>
    /// <param name = "newStatus">验证后的单元操作状态。</param>
    public UnitOperationValidatedEventArgs(string unitName, string message,
        CapeValidationStatus oldStatus, CapeValidationStatus newStatus)
    {
        UnitOperationName = unitName;
        Message = message;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }

    /// <summary>正在验证的单元操作的名称。</summary>
    /// <remarks>正在验证的单元操作名称。</remarks>
    /// <value>正在更改的单元操作名称。</value>
    public string UnitOperationName { get; }

    /// <summary>单元操作验证产生的报文。</summary>
    /// <remarks>该信息提供有关验证过程结果的信息。</remarks>
    /// <value>对验证过程进行评分的信息。</value>
    public string Message { get; }

    /// <summary>单元操作在验证前的验证状态。</summary>
    /// <remarks>通知用户验证过程的结果。</remarks>
    /// <value>单元操作在验证前的验证状态。</value>
    public CapeValidationStatus OldStatus { get; }

    /// <summary>验证后的单元操作的验证状态。</summary>
    /// <remarks>通知用户验证过程的结果。</remarks>
    /// <value>验证后的单元操作的验证状态。</value>
    public CapeValidationStatus NewStatus { get; }
}

/// <summary>表示将处理单位操作计算开始的方法。</summary>
[ComVisible(false)]
public delegate void UnitOperationCalculateHandler(object sender, UnitOperationCalculateEventArgs args);

/// <summary>在单元操作开始时触发的事件是计算。</summary>
/// <remarks>提供有关单元操作计算开始的信息。</remarks>
[ComVisible(true)]
[Guid("DDCA3348-074C-4860-AD00-58386327D9AC")]
[Description("IUnitOperationCalculateEventArgs Interface")]
interface IUnitOperationCalculateEventArgs
{
    /// <summary>The name of the unit operation being calculated.</summary>
    string UnitOperationName { get; }

    /// <summary>The message resulting from the start of the unit operation calculation.</summary>
    /// <remarks>The message provides information about the start of the unit operation calculation process.</remarks>
    /// <value>Information regrading the validation process.</value>
    string Message { get; }
}

/// <summary>事件在单元操作开始时触发，用于计算。</summary>
/// <remarks>提供有关单元操作计算开始的信息。</remarks>
[Serializable, ComVisible(true)]
[Guid("7831C38B-A1C6-40C5-B9FC-DAC43426AAD4")]
[ClassInterface(ClassInterfaceType.None)]
public class UnitOperationCalculateEventArgs : EventArgs,
    IUnitOperationCalculateEventArgs
{
    /// <summary>Creates an instance of the UnitOperationBeginCalculationEventArgs class for the unit operation .</summary>
    /// <remarks>You can use this constructor when raising the UnitOperationBeginCalculationEventArgs at run time to  
    /// the message about the parameter validation.</remarks>
    /// <param name = "unitName">The name of the unit operation  being calculated.</param>
    /// <param name = "message">The message indicating the conditions at the start of the unit operation calculation.</param>
    public UnitOperationCalculateEventArgs(string unitName, string message)
    {
        UnitOperationName = unitName;
        Message = message;
    }

    /// <summary>The name of the unit operation being calculated.</summary>
    /// <remarks>The name of the unit operation being calculated.</remarks>
    /// <value>The name of the unit operation being calculated.</value>
    public string UnitOperationName { get; }

    /// <summary>The message from the unit operation regarding the start of the calculation process.</summary>
    /// <remarks>The message provides information about the start of the calculated process.</remarks>
    /// <value>Information regarding the start of the calculated process.</value>
    public string Message { get; }
}

/// <summary>表示将处理单位操作计算开始的方法。</summary>
[ComVisible(false)]
public delegate void UnitOperationBeginCalculationHandler(object sender, UnitOperationBeginCalculationEventArgs args);

/// <summary>事件在单元操作开始时触发，用于计算。</summary>
/// <remarks>提供有关单元操作计算开始的信息。</remarks>
[ComVisible(true), Guid("3E827FD8-5BDB-41E4-81D9-AC438BC9B957")]
[Description("IUnitOperationBeginCalculationEventArgs Interface")]
internal interface IUnitOperationBeginCalculationEventArgs
{
    /// <summary>The name of the unit operation being calculated.</summary>
    string UnitOperationName { get; }

    /// <summary>The message resulting from the start of the unit operation calculation.</summary>
    /// <remarks>The message provides information about the start of the unit operation calculation process.</remarks>
    /// <value>Information regrading the validation process.</value>
    string Message { get; }
}

/// <summary>事件在单元操作开始时触发，用于计算。</summary>
/// <remarks>提供有关单元操作计算开始的信息。</remarks>
[Serializable, ComVisible(true)]
[Guid("763691E8-D792-4B97-A12A-D4AD7F66B5E4")]
[ClassInterface(ClassInterfaceType.None)]
public class UnitOperationBeginCalculationEventArgs : EventArgs,
    IUnitOperationBeginCalculationEventArgs
{
    /// <summary>Creates an instance of the UnitOperationBeginCalculationEventArgs class for the unit operation .</summary>
    /// <remarks>You can use this constructor when raising the UnitOperationBeginCalculationEventArgs at run time to  
    /// the message about the parameter validation.</remarks>
    /// <param name = "unitName">The name of the unit operation  being calculated.</param>
    /// <param name = "message">The message indicating the conditions at the start of the unit operation calculation.</param>
    public UnitOperationBeginCalculationEventArgs(string unitName, string message)
    {
        UnitOperationName = unitName;
        Message = message;
    }

    /// <summary>
    /// The name of the unit operation being calculated.</summary>
    /// <value>The name of the unit operation being calculated.</value>
    public string UnitOperationName { get; }

    /// <summary>
    /// The message from the unit operation regarding the start of the calculation process.</summary>
    /// <remarks>The message provides information about the start of the calculated process.</remarks>
    /// <value>Information regarding the start of the calculated process.</value>
    public string Message { get; }
};

/// <summary>表示将处理单元操作计算过程完成的方法。</summary>
[ComVisible(false)]
public delegate void UnitOperationEndCalculationHandler(object sender, UnitOperationEndCalculationEventArgs args);

/// <summary>单元操作计算过程已完成。</summary>
/// <remarks>提供有关单元操作计算过程完成的信息。</remarks>
[ComVisible(true)]
[Guid("951D755F-8831-4691-9B54-CC9935A5B7CC")]
[Description("IUnitOperationEndCalculationEventArgs Interface")]
internal interface IUnitOperationEndCalculationEventArgs
{
    /// <summary>The name of the unit operation being calculated.</summary>
    /// <value>The name of the unit operation being calculated.</value>
    string UnitOperationName { get; }

    /// <summary>The message from the unit operation regarding the completion of the calculation process.</summary>
    /// <remarks>The message provides information about the completion of the calculated process.</remarks>
    /// <value>Information regarding the completion of the calculated process.</value>
    string Message { get; }
}

/// <summary>单元操作已验证。</summary>
/// <remarks>提供有关单元操作验证的信息。</remarks>
[Serializable, ComVisible(true)]
[Guid("172F4D6E-65D1-4D9E-A275-7880FA3A40A5")]
[ClassInterface(ClassInterfaceType.None)]
public class UnitOperationEndCalculationEventArgs : EventArgs,
    IUnitOperationEndCalculationEventArgs
{
    /// <summary>Creates an instance of the UnitOperationEndCalculationEventArgs class for the unit operation .</summary>
    /// <remarks>You can use this constructor when raising the UnitOperationEndCalculationEventArgs at run time to  
    /// the message about the parameter validation.</remarks>
    /// <param name = "unitName">The name of the unit operation  being calculated.</param>
    /// <param name = "message">The message indicating the results of the unit operation calculation.</param>
    public UnitOperationEndCalculationEventArgs(string unitName, string message)
    {
        UnitOperationName = unitName;
        Message = message;
    }

    /// <summary>The name of the unit operation being calculated.</summary>
    /// <value>The name of the unit operation being calculated.</value>
    public string UnitOperationName { get; }

    /// <summary>The message from the unit operation regarding the completion of the calculation process.</summary>
    /// <remarks>The message provides information about the completion of the calculated process.</remarks>
    /// <value>Information regarding the completion of the calculated process.</value>
    public string Message { get; }
}

// 此接口为单元操作组件提供了基本功能。
// CAPE-OPEN v1.0
/// <summary>此接口处理与流程图单元的大部分交互。</summary>
/// <remarks>此接口为可以插入流程图包中的单元操作组件提供了基本功能要求。</remarks>
[ComVisible(false)]
[Description("ICapeUnit Interface")]
public interface ICapeUnit
{
    /// <summary>获取单元操作端口的集合。</summary>
    /// <remarks>将接口返回给包含单元端口列表的集合 (例如 ICapeCollection )。
    /// ICapeUnitPort 返回单元端口（即 ICapeUnitCollection）的集合。这些作为暴露接口的元素集合交付。</remarks>
    /// <value>The port collection of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(1), Description("Gets the whole list of ports")]
    PortCollection Ports
    {
        [return: MarshalAs(UnmanagedType.IDispatch)]
        get;
    }

    /// <summary>获取表示单位操作验证状态的标志 CapeValidationStatus。</summary>
    /// <remarks>Get the flag that indicates whether the Flowsheet Unit is valid (e.g. some 
    /// parameter values have changed, but they have not been validated by using Validate). 
    /// It has three possible values:
    /// (i) notValidated(CAPE_NOT_VALIDATED): The PMC's Validate()
    /// method has not been called after the last time that its value had been changed.
    /// (ii) invalid(CAPE_INVALID): The last time that the PMC's Validate() method was called it returned false.
    /// (iii) valid(CAPE_VALID): the last time that the PMC's Validate() method was called it returned true.</remarks>
    /// <value>A flag that indicates the validation status of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(2), Description("Get the unit's validation status")]
    CapeValidationStatus ValStatus { get; }

    /// <summary>执行单位操作模型中涉及的必要计算。</summary>
    /// <remarks>流程图单元执行其计算，即计算在输入和输出流的完整描述中在此阶段缺失的变量，
    /// 并计算需要显示的公共参数值。计算将能够根据需要使用模拟上下文进行进度监控和中断检查。目前，对此没有达成一致的标准。
    /// 建议流程图单元对所有输出流进行适当地闪蒸计算。在某些情况下，
    /// 模拟软件将能够进行闪蒸计算，但单元模块开发者最适合决定使用正确地闪蒸。
    /// 在进行计算之前，此方法应执行任何所需的最终验证测试。例如，此时可以检查连接到端口的材料对象的正确性。
    /// 此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeTimeOut">ECapeTimeOut</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [DispId(3), Description("Performs unit calculations")]
    void Calculate();

    /// <summary>验证单元操作，以验证参数和端口是否都有效。如果无效，此方法将返回一个消息，指示单元无效的原因。</summary>
    /// <remarks>设置标志，通过验证流程图单元的端口和参数来确定流程图单元是否有效。例如，
    /// 此方法可以检查所有必填端口是否已连接，并且所有参数值是否在范围内。
    /// 请注意，Simulation Executive 可以在任何时间调用 Validate 例行程序，
    /// 特别是在执行程序准备好调用 Calculate 方法之前。这意味着在调用 Validate 时，
    /// 与单元端口连接的物料对象可能无法正确配置。推荐的方法是，该方法验证参数和端口，
    /// 但不验证物料对象配置。可以在 Calculate 方法中实现检查物料对象的第二个验证级别，
    /// 如果合理预期与端口连接的物料对象将正确配置。</remarks>
    /// <returns>true, if the unit is valid. false, if the unit is not valid.</returns>
    /// <param name = "message">引用将包含有关参数验证消息的字符串。</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadCOParameter">ECapeBadCOParameter</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(4), Description("Validate the Unit")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(ref string message);
}

// 此接口为单元操作组件提供了基本功能。
// CAPE-OPEN v1.0
/// <summary>这个接口处理与流程图单元的大部分交互。</summary>
/// <remarks>此接口为可以插入流程图包中的单元操作组件提供了基本功能要求。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeUnit_IID)]
[Description("ICapeUnit Interface")]
internal interface ICapeUnitCOM
{
    /// <summary>获取单元操作端口的集合。</summary>
    /// <remarks>将接口返回给包含单元端口列表的集合 (例如 ICapeCollection )。
    /// ICapeUnitPort 返回单元端口（即 ICapeUnitCollection）的集合。这些作为暴露接口的元素集合交付。</remarks>
    /// <value>The port collection of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(1), Description("Gets the whole list of ports")]
    object ports
    {
        [return: MarshalAs(UnmanagedType.IDispatch)]
        get;
    }
    
    /// <summary>获取表示单位操作验证状态的标志 CapeValidationStatus。</summary>
    /// <remarks>Get the flag that indicates whether the Flowsheet Unit is valid (e.g. some 
    /// parameter values have changed, but they have not been validated by using Validate). 
    /// It has three possible values:
    /// (i) notValidated(CAPE_NOT_VALIDATED): The PMC's Validate()
    /// method has not been called after the last time that its value had been changed.
    /// (ii) invalid(CAPE_INVALID): The last time that the PMC's Validate() method was called it returned false.
    /// (iii) valid(CAPE_VALID): the last time that the PMC's Validate() method was called it returned true.</remarks>
    /// <value>A flag that indicates the validation status of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(2), Description("Get the unit's validation status")]
    CapeValidationStatus ValStatus { get; }

    /// <summary>执行单位操作模型中涉及的必要计算。</summary>
    /// <remarks>流程图单元执行其计算，即计算在输入和输出流的完整描述中在此阶段缺失的变量，
    /// 并计算需要显示的公共参数值。计算将能够根据需要使用模拟上下文进行进度监控和中断检查。目前，对此没有达成一致的标准。
    /// 建议流程图单元对所有输出流进行适当地闪蒸计算。在某些情况下，
    /// 模拟软件将能够进行闪蒸计算，但单元模块开发者最适合决定使用正确地闪蒸。
    /// 在进行计算之前，此方法应执行任何所需的最终验证测试。例如，此时可以检查连接到端口的材料对象的正确性。
    /// 此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeTimeOut">ECapeTimeOut</exception>
    /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    [DispId(3), Description("Performs unit calculations")]
    void Calculate();

    /// <summary>验证单元操作，以验证参数和端口是否都有效。如果无效，此方法将返回一个消息，指示单元无效的原因。</summary>
    /// <remarks>设置标志，通过验证流程图单元的端口和参数来确定流程图单元是否有效。例如，
    /// 此方法可以检查所有必填端口是否已连接，并且所有参数值是否在范围内。
    /// 请注意，Simulation Executive 可以在任何时间调用 Validate 例行程序，
    /// 特别是在执行程序准备好调用 Calculate 方法之前。这意味着在调用 Validate 时，
    /// 与单元端口连接的物料对象可能无法正确配置。推荐的方法是，该方法验证参数和端口，
    /// 但不验证物料对象配置。可以在 Calculate 方法中实现检查物料对象的第二个验证级别，
    /// 如果合理预期与端口连接的物料对象将正确配置。</remarks>
    /// <returns>true, if the unit is valid. false, if the unit is not valid.</returns>
    /// <param name = "message">引用将包含有关参数验证消息的字符串。</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadCOParameter">ECapeBadCOParameter</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(4), Description("Validate the Unit")]
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool Validate(ref string message);
}

/// <summary>此接口提供对活动单元报告和可用选项列表的访问。</summary>
/// <remarks>它还提供了一个触发器来创建报告。</remarks>
[ComVisible(false), Description("ICapeUnitReport Interface")]
public interface ICapeUnitReport
{
    /// <summary>Gets the list of possible reports for the unit operation.</summary>
    /// <remarks>Return the list of available Flowsheet Unit reports.</remarks>
    /// <value>The list of possible reports for the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Gets the list of unit reports")]
    List<string> Reports { get; }

    /// <summary>Gets and sets the current active report for the unit operation.</summary>
    /// <remarks>Return/set the active report in the Flowsheet Unit.</remarks>
    /// <value>The current active report for the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(2), Description("Gets the active unit report")]
    string selectedReport { get; set; }

    /// <summary>Produces the active report for the unit operation.</summary>
    /// <remarks>Produce the designated report. If no value has been set, it produces the default report.</remarks>
    /// <returns>String containing the text for the currently selected report.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(3), Description("Creates the active report")]
    string ProduceReport();
}

/// <summary>此接口提供对活动单元报告和可用选项列表的访问。</summary>
/// <remarks>它还提供了一个触发器来创建报告。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeUnitReport_IID)]
[Description("ICapeUnitReport Interface")]
internal interface ICapeUnitReportCOM
{
    /// <summary>Gets the list of possible reports for the unit operation.</summary>
    /// <remarks>Return the list of available Flowsheet Unit reports.</remarks>
    /// <value>The list of possible reports for the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Gets the list of unit reports")]
    object reports { get; }

    /// <summary>Gets and sets the current active report for the unit operation.</summary>
    /// <remarks>Return/set the active report in the Flowsheet Unit.</remarks>
    /// <value>The current active report for the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(2), Description("Gets the active unit report")]
    string selectedReport { get; set; }

    /// <summary>Produces the active report for the unit operation.</summary>
    /// <remarks>Produce the designated report. If no value has been set, it produces the default report.</remarks>
    /// <param name = "message">String containing the text for the currently selected report.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(3), Description("Creates the active report")]
    void ProduceReport(ref string message);
}

/// <summary>此接口表示单元操作连接点（单元操作端口）的行为。它提供了不同的属性来配置端口以及将其连接到材料、能源或信息对象。</summary>
/// <remarks>单元端口提供了将流程图单元连接到其流的方法。流是通过材料对象实现的。
/// 三种类型的端口：材料、能量和信息，具有许多共同的功能。通过将三者结合成一个，
/// 我们可以简化到一定程度。每种端口类型都通过属性的值来区分。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeUnitPort_IID)]
[Description("ICapeUnitPort Interface")]
internal interface ICapeUnitPortCOM
{
    /// <summary>Returns port type.</summary>
    /// <remarks>返回此端口的类型。允许的类型包括在CapePortType类型中。</remarks>
    /// <value>The type of the port.</value>
    /// <see cref="CapeOpen.CapePortType">CapePortType</see>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(1), Description("Type of port, e.g. material, energy or information")]
    CapePortType portType { get; }

    /// <summary>Returns port direction.</summary>
    /// <remarks>返回连接到此端口的对象预期流动的方向。允许的值包括在CapePortDirection类型中的值。</remarks>
    /// <value>The direction of the port.</value>
    /// <see cref="CapeOpen.CapePortDirection">CapePortDirection</see>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(2), Description("Direction of port, e.g. input, output or unspecified")]
    CapePortDirection direction { get; }

    /// <summary>Returns to the client the object that is connected to this port.</summary>
    /// <remarks>返回连接到端口的对象。客户端使用 Connect 方法提供先前已连接到端口的材料、能量或信息对象。</remarks>
    /// <value>The object connected to the port.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(3), Description(
         "Gets the objet connected to the port, e.g. material, energy or information")]
    object connectedObject
    {
        [return: MarshalAs(UnmanagedType.IDispatch)] get;
    }

    /// <summary>将对象连接到端口。对于材料端口，它必须是一个实现 ICapeThermoMaterialObject 接口的对象，
    /// 对于能源和信息端口，它必须是一个实现 ICapeParameter 接口的对象。</summary>
    /// <remarks>客户端使用的方法，当它们请求一个端口与作为方法参数传递的对象连接时使用。很可能在接收连接之前，
    /// 端口会检查作为参数发送的对象是否为其预期类型，并根据其属性 portType 的值。</remarks>
    /// <param name = "objectToConnect">The object to connect to the port.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(4), Description(
         "Connects the port to the object sent as argument, e.g. material, energy or information")]
    void Connect( [MarshalAs(UnmanagedType.IDispatch)] object objectToConnect);

    /// <summary>Disconnects whatever object is connected to this port.</summary>
    /// <remarks>将端口与连接到它的任何对象断开连接。此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [DispId(5), Description("Disconnects the port")]
    void Disconnect();
}

/// <summary>此接口表示单元操作连接点（单元操作端口）的行为。它提供了不同的属性来配置端口以及将其连接到材料、能源或信息对象。</summary>
/// <remarks>单元端口提供了将流程图单元连接到其流的方法。流是通过材料对象实现的。
/// 三种类型的端口：材料、能量和信息，具有许多共同的功能。通过将三者结合成一个，
/// 我们可以简化到一定程度。每种端口类型都通过属性的值来区分。</remarks>
[ComVisible(false)]
[Description("ICapeUnitPort Interface")]
public interface ICapeUnitPort
{
    /// <summary>Returns port type.</summary>
    /// <remarks>Returns the type of this port. Allowed types are among the ones included in the CapePortType type.</remarks>
    /// <value>The type of the port.</value>
    /// <see cref="CapeOpen.CapePortType">CapePortType</see>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(1), Description("Type of port, e.g. material, energy or information")]
    CapePortType portType { get; }

    /// <summary>Returns port direction.</summary>
    /// <remarks>返回连接到此端口的对象预期流动的方向。允许的值包括在 CapePortDirection 类型中的值。</remarks>
    /// <value>The direction of the port.</value>
    /// <see cref="CapeOpen.CapePortDirection">CapePortDirection</see>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(2), Description("Direction of port, e.g. input, output or unspecified")]
    CapePortDirection direction { get; }

    /// <summary>
    ///	Returns to the client the object that is connected to this port.
    /// </summary>
    /// <remarks>
    /// Returns the object that is connected to the Port. A client is provided with the 
    /// Material, Energy or Information object that was previously connected to the Port, 
    /// using the Connect method.
    /// </remarks>
    /// <value>The object connected to the port.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(3),
     Description(
         "gets the objet connected to the port, e.g. material, energy or information")]
    object connectedObject
    {
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
        get;
    }

    /// <summary>
    ///	Connects an object to the port. For a material port it must 
    /// be an object implementing the ICapeThermoMaterialObject interface, 
    /// for Energy and Information ports it must be an object implementing 
    /// the ICapeParameter interface. 
    /// </summary>
    /// <remarks>
    /// Method used by clients, when they request that a Port connect itself with the object 
    /// that is passed in as argument of the method. Probably, before accepting the connection, 
    /// a Port will check that the Object sent as argument is of the expected type and 
    /// according to the value of its attribute portType.
    /// </remarks>
    /// <param name = "objectToConnect">The object to connect to the port.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(4),
     Description(
         "connects the port to the object sent as argument, e.g. material, energy or information")]
    void Connect(
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
        object objectToConnect);

    /// <summary>
    ///	Disconnects whatever object is connected to this port.
    /// </summary>
    /// <remarks>
    /// <para>Disconnects the port from whichever object is connected to it.</para>
    /// <para>There are no input or output arguments for this method.</para>
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [DispId(5),
     Description("disconnects the port")]
    void Disconnect();
};

/// <summary>
/// An object was connected to the port.
/// </summary>
/// <remarks>
/// An object was connected to the port.
/// </remarks>
[ComVisible(true)]
[Guid("DC735166-8008-4B39-BE1C-6E94A723AD65")]
[Description("PortConnectedEventArgs Interface")]
interface IPortConnectedEventArgs
{
    /// <summary>
    /// The name of the port being connected.
    /// </summary>
    string PortName { get; }
};

/// <summary>
/// An object was connected to the port.
/// </summary>
/// <remarks>
/// An object was connected to the port.
/// </remarks>
[Serializable]
[ComVisible(true)]
[Guid("962B9FDE-842E-43F8-9280-41C5BF80DDEC")]
[ClassInterface(ClassInterfaceType.None)]
public class PortConnectedEventArgs : EventArgs,
    IPortConnectedEventArgs
{
    /// <summary>Creates an instance of the PortConnectedEventArgs class for the port.</summary>
    /// <remarks>You can use this constructor when raising the PortConnectedEventArgs at run time to  
    /// inform the system that the port was connected.
    /// </remarks>
    public PortConnectedEventArgs(string portName)
    {
        PortName = portName;
    }

    /// <summary>
    /// The name of the port being connected.</summary>
    /// <value>The name of the port being connected.</value>
    public string PortName { get; }
};

/// <summary>
/// Represents the method that will handle disconnecting an object from a unit port.
/// </summary>
[ComVisible(false)]
public delegate void PortDisconnectedHandler(object sender, PortDisconnectedEventArgs args);

/// <summary>
/// The port was disconnected.
/// </summary>
/// <remarks>
/// The port was disconnected.
/// </remarks>
[ComVisible(true)]
[Guid("5EFDEE16-7858-4119-B8BB-7394FFBCC02D")]
[Description("PortDisconnectedEventArgs Interface")]
interface IPortDisconnectedEventArgs
{
    /// <summary>
    /// The name of the port being disconnected.</summary>
    string PortName { get; }
};

/// <summary>
/// The port was disconnected.
/// </summary>
/// <remarks>
/// The port was disconnected.
/// </remarks>
[Serializable]
[ComVisible(true)]
[Guid("693F33AA-EE4A-4CDF-9BA1-8889086BC8AB")]
[ClassInterface(ClassInterfaceType.None)]
public class PortDisconnectedEventArgs : EventArgs,
    IPortDisconnectedEventArgs
{
    /// <summary>Creates an instance of the PortDisconnectedEventArgs class for the port.</summary>
    /// <remarks>You can use this constructor when raising the PortDisconnectedEventArgs at run time to  
    /// inform the system that the port was disconnected.
    /// </remarks>
    public PortDisconnectedEventArgs(string portName)
    {
        PortName = portName;
    }

    /// <summary>
    /// The name of the port being disconnected.</summary>
    /// <value>The name of the port being disconnected.</value>
    public string PortName { get; }
};

/// <summary>
///	Port variables for equation-oriented simulators.
/// </summary>
/// <remarks>
/// This interface is optional and would be implemented by a port object. It is intended 
/// to allow a port to describe which Equation-oriented variables are associated with it and 
/// should only be implemented for the ports contained in a unit operation which supports the
/// ICapeNumericESO interface described in “CAPE-OPEN Interface Specification – Numerical 
/// Solvers”.
/// </remarks>
[System.Runtime.InteropServices.ComImport()]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid(CapeOpenGuids.ICapeUnitPortVariables_IID)]
[Description("ICapeUnitPortVariables Interface")]
public interface ICapeUnitPortVariables
{
    /// <summary>
    ///	The position of a port variable in the EO model.
    /// </summary>
    /// <remarks>
    ///	Gets the position of a port variable in the EO model - used to 
    /// correctly build the equations representing a connection to this port.
    ///  Variable type can be - flow rate, temperature, pressure, 
    /// specificEnthalpy, VaporFraction and for Vapour fraction component 
    /// name must also be specified. 
    /// </remarks>
    /// <param name = "Variable_type">The Type of the variable.</param>
    /// <param name = "Component">The component of the variable.</param>
    /// <value>The position of the variable.</value>
    [DispId(1),
     Description("Return index of port variable in EO Model given its type")]
    int Variable(string Variable_type, string Component);

    /// <summary>
    /// Sets the position of port variables: this should ultimately 
    /// be a private member function.
    /// </summary>
    /// <remarks>
    /// Sets the position of port variables: this should ultimately 
    /// be a private member function.
    /// </remarks>
    /// <param name = "Variable_type">The Type of the variable.</param>
    /// <param name = "Component">The component of the variable.</param>
    /// <param name = "index">The index of the variable.</param>
    [DispId(2),
     Description("Set index of port variable in EO model given its type")]
    void SetIndex(string Variable_type, string Component, int index);
}

// 旧版本接口，已弃用
/*	[System.Runtime.InteropServices.ComImport()]
    [ComVisible(false)]
    [Guid(ICapeUnit_IID)]
    public interface class ICapeUnit093
    {
        /// <summary>
        /// Gets the collection of unit operation ports.
        /// </summary>
        /// <remarks>
        /// <para>Return an interface to a collection containing the list of unit ports (e.g.
        /// <see name = "ICapeCollection"/>).</para>
        /// <para>Return the collection of unit ports (i.e. ICapeUnitCollection). These are
        /// delivered as a collection of elements exposing the interfaces <see name = "ICapeUnitPort"/>
        /// </para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        [DispId(1), Description("Gets the whole list of ports")]
        property Object^ ports
        {
            [returnvalue: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
            Object^ get();
        };

        /// <summary>
        ///	Gets the component's collection of parameters.
        /// </summary>
        /// <remarks>
        /// <para>Return the collection of Public Unit Parameters (i.e.
        /// <see cref = "ICapeCollection"/>.</para>
        /// <para>These are delivered as a collection of elements exposing the interface
        /// <see cref = "ICapeParameter"/>. From there, the client could extract the
        /// <see cref = "ICapeParameterSpec"/> interface or any of the typed
        /// interfaces such as <see cref = "ICapeRealParameterSpec"/>, once the client
        /// establishes that the Parameter is of type double.</para>
        /// </remarks>
        /// <value>The parameter collection of the unit operation.</value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        [DispId(2)]
        [Description("Gets the whole list of parameters")]
        property Object^ parameters
        {
            [returnvalue: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
            Object^ get ();
        };

        /// <summary>
        /// Gets the flag to indicate the unit operation's validation status
        /// <see cref= "CapeValidationStatus">CapeValidationStatus</see>.
        /// </summary>
        /// <remarks>
        /// <para>Get the flag that indicates whether the Flowsheet Unit is valid (e.g. some
        /// parameter values have changed, but they have not been validated by using Validate).
        /// It has three possible values:</para>
        /// <para>   (i)   notValidated(CAPE_NOT_VALIDATED): The PMC's <c>Validate()</c>
        /// method has not been called after the last time that its value had been
        /// changed.</para>
        /// <para>   (ii)  invalid(CAPE_INVALID): The last time that the PMC's
        /// <c>Validate()</c> method was called it returned false.</para>
        /// <para>   (iii) valid(CAPE_VALID): the last time that the PMC's
        /// Validate() method was called it returned true.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
        [DispId(3), Description("Get the unit's validation status")]
        property CapeValidationStatus ValStatus
        {
            CapeValidationStatus get();
        };

        /// <summary>
        ///	Executes the necessary calculations involved in the unit operation model.
        /// </summary>
        /// <remarks>
        /// <para>The Flowsheet Unit performs its calculation, that is, computes the variables
        /// that are missing at this stage in the complete description of the input and output
        /// streams and computes any public parameter value that needs to be displayed. Calculate
        /// will be able to do progress monitoring and checks for interrupts as required using
        /// the simulation context. At present, there are no standards agreed for this.</para>
        /// <para>It is recommended that Flowsheet Units perform a suitable flash calculation on
        /// all output streams. In some cases a Simulation Executive will be able to perform a
        /// flash calculation but the writer of a Flowsheet Unit is in the best position to
        /// decide the correct flash to use. </para>
        /// <para>Before performing the calculation, this method should perform any final
        /// validation tests that are required. For example, at this point the validity of
        /// Material Objects connected to ports can be checked.</para>
        /// <para>There are no input or output arguments for this method.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
        /// <exception cref = "ECapeTimeOut">ECapeTimeOut</exception>
        /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
        /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
        [DispId(4), Description("Performs unit calculations")]
        void Calculate();

        /// <summary>
        /// The unit operation is asked to read its persistent state, from the storage location it has previously chosen in Save.
        /// </summary>
        /// <remarks>
        /// <para>The Flowsheet Unit is restored from a previously saved state. The [in] argument
        /// identifies the location from which the Flowsheet Unit should read its data. Here, data
        /// refers to whatever data the writer of the Flowsheet Unit chooses to save. This
        /// location may bear no relation to any location to which data has previously been saved.
        /// It can be a CapeString type, identifying a full path of an ASCII file, or it can be a
        /// type CapeVariant, hosting a reference to any standard COM interface for persistence
        /// such as IStorage or IStream.</para>
        /// <para>It is strongly recommended that the IStorage, or IStream variations are not used
        /// in a COM environment because a Flowsheet Unit written in Visual Basic (versions 6 and
        /// lower) is not able to make use of an IStorage or IStream pointer. In a COM environment
        /// the preferred solution is for the Flowsheet Unit and the COSE to support the standard
        /// COM persistence interfaces and protocols. See section 5.2.8 for a discussion of this
        /// recommendation.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
        /// <exception cref = "ECapePersistenceNotFound">ECapePersistenceNotFound</exception>
        /// <exception cref = "ECapeIllegalAccess">ECapeIllegalAccess</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        [DispId(5), Description("Recovers unit persistent state")]
        void Restore(Object^ storage);

        /// <summary>
        /// The unit operation is asked to save its persistent state, in a given
        /// location passed in as argument. This unit may choose to use that storage
        /// (e.g. structured storage) or to use its own internal storage mechanism
        /// (e.g. a plain ASCII text file). If the storage location is changed by
        /// the unit, the new location is sent back to the client.
        /// </summary>
        /// <remarks>
        /// <para>The Flowsheet Unit saves its private data in the location indicated by the
        /// simulator (i.e. storage), that can be a given position in a structured document or a
        /// file name (CapeString type). The Flowsheet Unit is free to use the storage mechanism
        /// that the simulator provides, or to use its own internal mechanisms. A valid scenario
        /// would be that the Flowsheet Unit checks the type of the storage parameter passed in,
        /// accepting it if it is of type CapeString (e.g. an ASCII file name) or rejecting it if
        /// it is of another type that the Flowsheet Unit does not handle. In the latter case, the
        /// Flowsheet Unit may decide to create its own text file, save its data there, and send
        /// back the full path of the file to the simulator (notice the [in, out] argument) which
        /// stores that string along with its own simulation data.</para>
        /// <para>It is recommended that in a COM implementation a component should support one
        /// of the standard COM persistence methods in addition to this method. See section
        /// 5.2.8 for a discussion of this recommendation.</para>
        /// <para>A Flowsheet Unit must save its state completely so that the Restore can
        /// recreate that state. Flowsheet Unit authors should not rely on Validate being called
        /// after Restore. The same requirement applies when COM persistence methods are
        /// implemented.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
        /// <exception cref = "ECapePersistenceNotFound">ECapePersistenceNotFound</exception>
        /// <exception cref = "ECapeIllegalAccess">ECapeIllegalAccess</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        [DispId(6), Description("Saves unit state")]
        void Save(Object^ storage);

        /// <summary>
        /// The unit operation is asked to configure itself. Typically, some ports
        /// and parameters are created here.
        /// </summary>
        /// <remarks>
        /// <para>The Flowsheet Unit initialises itself. Any initialisation that could fail must
        /// be placed here.</para>
        /// <para>Initialize is guaranteed to be the first method called by the client. Initialise
        /// has to be called once when the Flowsheet Unit is instantiated in a particular
        /// flowsheet.</para>
        /// <para>Note that the Initialise method is intended to implement software initialisation
        /// not to perform an initial solution of the Flowsheet Unit. It is expected that the
        /// method would be used to create and configure ports and parameters and to define
        /// default values for variables and parameters.</para>
        /// <para>There are no input or output arguments for this method.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
        [DispId(7), Description("Configuration has to take place here")]
        void Initialize();

        /// <summary>
        ///	Clean-up tasks for the unit operation can be performed here. In
        /// particular, references to parameters and objects connected to
        /// ports are released here.
        /// </summary>
        /// <remarks>
        /// <para>The Flowsheet Unit releases all of its allocated resources. This is called
        /// before the object destructor. Terminate may check if the data has been saved and return an
        /// error if not.</para>
        /// <para>There are no input or output arguments for this method.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        [DispId(8), Description("Clean up has to take place here")]
        void Terminate();

        /// <summary>
        ///	Validate the unit operation to verify that the parameters and ports are
        /// all valid. If invalid, this method returns a message indicating the
        /// reason that the unit is invalid.
        /// </summary>
        /// <remarks>
        /// <para>Sets the flag that indicates whether the Flowsheet Unit is valid by validating
        /// the ports and parameters of the Flowsheet Unit. For example, this method could check
        /// that all mandatory ports have connections and that the values of all parameters are
        /// within bounds.</para>
        /// <para>Note that the Simulation Executive can call the Validate routine at any time,
        /// in particular it may be called before the executive is ready to call the Calculate
        /// method. This means that Material Objects connected to unit ports may not be correctly
        /// configured when Validate is called. The recommended approach is for this method to
        /// validate parameters and ports but not Material Object configuration. A second level
        /// of validation to check Material Objects can be implemented as part of Calculate, when
        /// it is reasonable to expect that the Material Objects connected to ports will be
        /// correctly configured.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadCOParameter">ECapeBadCOParameter</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        [DispId(9), Description("Validate the Unit"), returnvalue: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.VariantBool)]
        bool Validate(System.String^ %message);

        /// <summary>
        ///	Set the simulation context for the unit operation.
        /// </summary>
        /// <remarks>
        /// <para>Allows the PME to convey the PMC a reference to the former’s
        /// simulation  context. The simulation context will be PME objects which will
        /// expose a given set of CO interfaces. Each of these interfaces will allow
        /// the PMC to call back the PME in order to benefit from its exposed services
        /// (such as creation of material templates, diagnostics or measurement unit
        /// conversion). If the PMC does not support accessing the simulation context,
        /// it is recommended to raise the ECapeNoImpl error.</para>
        /// <para>Initially, this method was only present in the ICapeUnit interface.
        /// Since ICapeUtilities.SetSimulationContext is now available for any kind of
        /// PMC, ICapeUnit. SetSimulationContext is deprecated.</para>
        /// </remarks>
        /// <param name = "simContext">
        /// The reference to the PME’s simulation context class. For the PMC to use
        /// this class, this reference will have to be converted to each of the
        /// defined CO Simulation Context interfaces.
        /// </param>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        [DispId(11)]
        [Description("Set the simulation context")]
        property Object^ simulationContext
        {
            void set([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]Object^ simContext);
        };
    };
*/
