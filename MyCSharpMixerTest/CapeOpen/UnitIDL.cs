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
    /// <remarks>获取指示流图单元是否有效的标志(例如，一些参数值已更改，但尚未使用 Validate 进行验证)。它有三个可能的值:
    /// (i) notValidated(CAPE_NOT_VALIDATED): The PMC's Validate() method has not been called after the last time that its value had been changed.
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

    /// <summary>Returns to the client the object that is connected to this port.</summary>
    /// <remarks>返回连接到端口的对象。客户端使用 Connect 方法提供先前已连接到端口的材料、能量或信息对象。</remarks>
    /// <value>The object connected to the port.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
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
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    [DispId(4), Description(
         "Connects the port to the object sent as argument, e.g. material, energy or information")]
    void Connect( [MarshalAs(UnmanagedType.IDispatch)] object objectToConnect);

    /// <summary>断开连接到该端口的任何对象。</summary>
    /// <remarks>Disconnects the port from whichever object is connected to it.
    /// There are no input or output arguments for this method.</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [DispId(5), Description("Disconnects the port")]
    void Disconnect();
}

/// <summary>对象连接到端口。</summary>
[ComVisible(true)]
[Guid("DC735166-8008-4B39-BE1C-6E94A723AD65")]
[Description("PortConnectedEventArgs Interface")]
internal interface IPortConnectedEventArgs
{
    /// <summary>The name of the port being connected.</summary>
    string PortName { get; }
};

/// <summary>对象连接到端口。</summary>
[Serializable, ComVisible(true)]
[Guid("962B9FDE-842E-43F8-9280-41C5BF80DDEC")]
[ClassInterface(ClassInterfaceType.None)]
public class PortConnectedEventArgs : EventArgs,
    IPortConnectedEventArgs
{
    /// <summary>Creates an instance of the PortConnectedEventArgs class for the port.</summary>
    /// <remarks>You can use this constructor when raising the PortConnectedEventArgs at run time to  
    /// inform the system that the port was connected.</remarks>
    public PortConnectedEventArgs(string portName)
    {
        PortName = portName;
    }

    /// <summary>The name of the port being connected.</summary>
    /// <value>The name of the port being connected.</value>
    public string PortName { get; }
}

/// <summary>表示将对象从单元端口断开连接的方法。</summary>
[ComVisible(false)]
public delegate void PortDisconnectedHandler(object sender, PortDisconnectedEventArgs args);

/// <summary>端口断开连接</summary>
[ComVisible(true)]
[Guid("5EFDEE16-7858-4119-B8BB-7394FFBCC02D")]
[Description("PortDisconnectedEventArgs Interface")]
internal interface IPortDisconnectedEventArgs
{
    /// <summary>The name of the port being disconnected.</summary>
    string PortName { get; }
}

/// <summary>端口断开连接</summary>
[Serializable, ComVisible(true)]
[Guid("693F33AA-EE4A-4CDF-9BA1-8889086BC8AB")]
[ClassInterface(ClassInterfaceType.None)]
public class PortDisconnectedEventArgs : EventArgs,
    IPortDisconnectedEventArgs
{
    /// <summary>Creates an instance of the PortDisconnectedEventArgs class for the port.</summary>
    /// <remarks>You can use this constructor when raising the PortDisconnectedEventArgs at run time to  
    /// inform the system that the port was disconnected.</remarks>
    public PortDisconnectedEventArgs(string portName)
    {
        PortName = portName;
    }

    /// <summary>The name of the port being disconnected.</summary>
    /// <value>The name of the port being disconnected.</value>
    public string PortName { get; }
}

/// <summary>面向联立方程 EO 的模拟器的端口变量。</summary>
/// <remarks>这个接口是可选的，将由端口对象实现。它旨在允许端口描述哪些面向方程的变量与之关联，
/// 并且仅应实现支持 “CAPE-OPEN 接口规范 - 数值求解器”中描述的 ICapeNumericESO 接口的单元操作中的端口。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeUnitPortVariables_IID)]
[Description("ICapeUnitPortVariables Interface")]
public interface ICapeUnitPortVariables
{
    /// <summary>端口变量在 EO 模型中的位置。</summary>
    /// <remarks>获取 EO 模型中端口变量的位置 - 用于正确构建表示与该端口连接的方程。
    /// 变量类型可以是 - 流速、温度、压力、比焓、蒸汽分率和蒸汽分率组件名称也必须指定。 </remarks>
    /// <param name = "Variable_type">The Type of the variable.</param>
    /// <param name = "Component">The component of the variable.</param>
    /// <value>The position of the variable.</value>
    [DispId(1), Description("Return index of port variable in EO Model given its type")]
    int Variable(string Variable_type, string Component);

    /// <summary>设置端口变量的位置：这最终应该是一个私有成员函数。</summary>
    /// <param name = "Variable_type">The Type of the variable.</param>
    /// <param name = "Component">The component of the variable.</param>
    /// <param name = "index">The index of the variable.</param>
    [DispId(2),
     Description("Set index of port variable in EO model given its type")]
    void SetIndex(string Variable_type, string Component, int index);
}
