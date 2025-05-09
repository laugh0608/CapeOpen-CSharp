/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2002.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org

This file has been edited using the editor from Microsoft Visual Studio 6.0
This file can view properly with any basic editors and browsers (validation done under MS Windows and Unix)
*/

// This file was developed/modified by JEAN-PIERRE-BELAUD for CO-LaN organisation - March 2003
// This file was modified by Bill Barrett of USE PA to restore CAPE-OPENv0.93 interface - November 30, 2005.
// 大白萝卜重构于 2025.05.09，使用 Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

// ------ 错误信息接口的相关职责范围 ------------
// 接口文档: Error Common Interface

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

// Error Interfaces 接口 的 .NET 翻译：
/// <summary>CAPE-OPEN 错误处理接口的各种 HRESULT 值的枚举。</summary>
/// <remarks>摘自平台SDK帮助中的“COM+中处理错误的策略”。使用 FACILITY_ITF 错误范围来报告特定于接口的错误。
/// 特定于接口的错误应在 FACILITY_ITF 误差范围内，介于 0x0200 和 0xFFFF 之间。
/// 然而，由于微软在 0x0200 之后使用一些代码，CAPE-OPEN 错误代码将从 0x0500 开始。
/// 使用 C++ 中的 MAKE_HRESULT 宏引入接口特定的错误代码，如下面的示例所示：
/// const HRESULT ERROR_NUMBER = MAKE_HRESULT (SEVERITY_ERROR, FACILITY_ITF,10);
/// 因此，FIRST_E_INTERFACE_HR 的偏移必须在 1 到 64255（0xFFFF-0x0500）之间。我们保留 0 的偏移。
/// const int FIRST_E_INTERFACE_HR = (int)0x80040500;
/// 用于 CO 错误接口的最后一个 HR 值：
/// const int LAST_USED_E_INTERFACE_HR = (int)0x80040517;
/// 可用于表示 CO 误差接口的最高 HR 值：
/// const int LAST_E_INTERFACE_HR = (int)0x8004FFFF;</remarks>
[Serializable]
public enum CapeErrorInterfaceHR
{
    /// <summary>0x80040501</summary>
    ECapeUnknownHR = unchecked((int)0x80040501),

    /// <summary>0x80040502</summary>
    ECapeDataHR = unchecked((int)0x80040502),

    /// <summary>0x80040503</summary>
    ECapeLicenceErrorHR = unchecked((int)0x80040503),

    /// <summary>0x80040504</summary>
    ECapeBadCOParameterHR = unchecked((int)0x80040504),

    /// <summary>0x80040505</summary>
    ECapeBadArgumentHR = unchecked((int)0x80040505),

    /// <summary>0x80040506</summary>
    ECapeInvalidArgumentHR = unchecked((int)0x80040506),

    /// <summary>0x80040507</summary>
    ECapeOutOfBoundsHR = unchecked((int)0x80040507),

    /// <summary>0x80040508</summary>
    ECapeImplementationHR = unchecked((int)0x80040508),

    /// <summary>0x80040509</summary>
    ECapeNoImplHR = unchecked((int)0x80040509),

    /// <summary>0x8004050A</summary>
    ECapeLimitedImplHR = unchecked((int)0x8004050A),

    /// <summary>0x8004050B</summary>
    ECapeComputationHR = unchecked((int)0x8004050B),

    /// <summary>0x8004050C</summary>
    ECapeOutOfResourcesHR = unchecked((int)0x8004050C),

    /// <summary>0x8004050D</summary>
    ECapeNoMemoryHR = unchecked((int)0x8004050D),

    /// <summary>0x8004050E</summary>
    ECapeTimeOutHR = unchecked((int)0x8004050E),

    /// <summary>0x8004050F</summary>
    ECapeFailedInitialisationHR = unchecked((int)0x8004050F),

    /// <summary>0x80040510</summary>
    ECapeSolvingErrorHR = unchecked((int)0x80040510),

    /// <summary>0x80040511</summary>
    ECapeBadInvOrderHR = unchecked((int)0x80040511),

    /// <summary>0x80040512</summary>
    ECapeInvalidOperationHR = unchecked((int)0x80040512),

    /// <summary>0x80040513</summary>
    ECapePersistenceHR = unchecked((int)0x80040513),

    /// <summary>0x80040514</summary>
    ECapeIllegalAccessHR = unchecked((int)0x80040514),

    /// <summary>0x80040515</summary>
    ECapePersistenceNotFoundHR = unchecked((int)0x80040515),

    /// <summary>0x80040516</summary>
    ECapePersistenceSystemErrorHR = unchecked((int)0x80040516),

    /// <summary>0x80040517</summary>
    ECapePersistenceOverflowHR = unchecked((int)0x80040517),

    /// <summary>0x80040518, 专门针对混合整数非线性规划（MINLP）算法</summary>
    ECapeOutsideSolverScopeHR = unchecked((int)0x80040518),

    /// <summary>0x80040519, 专门针对混合整数非线性规划（MINLP）算法</summary>
    ECapeHessianInfoNotAvailableHR = unchecked((int)0x80040519),

    /// <summary>0x80040519, 专门针对混合整数非线性规划（MINLP）算法</summary>
    ECapeThrmPropertyNotAvailableHR = unchecked((int)0x80040520)
}

// ECapeBoundaries 接口
/// <summary>这个接口提供了关于由于值超出其范围而导致的错误的信息。它可以被抛出，以指示方法参数或对象参数值超出范围。</summary>
/// <remarks>ECapeBoundaries是一个“实用程序”接口，它分解了一个描述值、其类型和界限的状态。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeBoundaries_IID)]
[Description("ECapeBoundaries Interface")]
public interface ECapeBoundaries
{
    /// <summary>值下限</summary>
    /// <remarks>This provides the user with the acceptable lower bounds of the argument.</remarks>
    /// <value>The lower bound for the argument.</value>
    [DispId(1), Description("The value of the lower bound.")]
    double lowerBound { get; }

    /// <summary>值上限</summary>
    /// <remarks>This provides the user with the acceptable upper bounds of the argument.</remarks>
    /// <value>The upper bound for the argument.</value>
    [DispId(2), Description("The value of the upper bound.")]
    double upperBound { get; }

    /// <summary>导致错误的当前值。</summary>
    /// <remarks>This provides the user with the value that caused the error condition.</remarks>
    /// <value>The value that resulted in the error condition.</value>
    [DispId(3), Description("The current value which has led to an error..")]
    double value { get; }

    /// <summary>值的类型 / 性质。</summary>
    /// <remarks>该值可以表示热力学属性、数据库中的表数量、内存量等等"</remarks>
    /// <value>A string that indicates the nature or type of the value required.</value>
    [DispId(4), Description(
         "The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...")]
    string type { get; }
}

/// <summary>CAPE-OPEN “根”异常接口。</summary>
/// <remarks>CAPE-OPEN 错误层次结构的接口。系统包和 ECapeUser 接口依赖于此错误。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeRoot_IID)]
[Description("ECapeRoot Interface")]
public interface ECapeRoot
{
    /// <summary>The name of the error. This is a mandatory field.</summary>
    [DispId(1), Description("Error Name")]
    string Name { get; }
}

// ECapeUser 接口
/// <summary>CAPE-OPEN 错误层次结构的底层接口。</summary>
/// <remarks>ECapeUser 接口定义了 CAPE-OPEN 错误的最低状态。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeUser_IID)]
[Description("ECapeUser Interface")]
public interface ECapeUser
{
    /// <summary>指定错误子类别的代码。</summary>
    /// <remarks>错误代码在 COM 调用模式中用作函数返回值 HRESULT。当基于.NET的组件抛出异常时，
    /// 分配给异常的 HRESULT 值会返回给基于 COM 的调用者。重要的是要将异常 HRESULT 值设置为向 COM 调用者提供H RESULT 信息。
    /// 值的分配留给每个实现。因此，这是 CAPE-OPEN 组件提供程序特有的专有代码。默认情况下，
    /// 设置为 CAPE-OPEN 错误 HRESULT，详见 <see cref = "CapeErrorInterfaceHR"/>。</remarks>
    /// <value>The HRESULT value for the exception.</value>
    [DispId(1), Description(
         "Code to designate the subcategory of the error. The assignment of values is left to each implementation. So that is a proprietary code specific to the CO component provider.")]
    int code { get; }

    /// <summary>The description of the error.</summary>
    /// <remarks>错误描述可以包括对导致错误的条件的更详细的描述。</remarks>
    /// <value>A string description of the exception.</value>
    [DispId(2), Description("The description of the error.")]
    string description { get; }

    /// <summary>The scope of the error.</summary>
    /// <remarks>这个属性提供了一个由“.”分隔的错误发生位置的包列表。例如 CapeOpen.Common.Identification。</remarks>
    /// <value>The source of the error.</value>
    [DispId(3), Description(
         "The scope of the error. The list of packages where the error occurs separated by '.'. For example CapeOpen.Common.Identification.")]
    string scope { get; }

    /// <summary>The name of the interface where the error is thrown. This is a mandatory field.</summary>
    /// <remarks>The interface that the error was thrown.</remarks>
    /// <value>The name of the interface.</value>
    [DispId(4), Description(
         "The name of the interface where the error is thrown. This is a mandatory field.")]
    string interfaceName { get; }

    /// <summary>The name of the operation where the error is thrown. This is a mandatory field.</summary>
    /// <remarks>This field provides the name of the operation being performed when the exception was raised.</remarks>
    /// <value>The operation name.</value>
    [DispId(5), Description(
         "The name of the operation where the error is thrown. This is a mandatory field.")]
    string operation { get; }

    /// <summary>一个页面、文档、网站或网页的URL，在那里可以找到有关错误的更多信息。这些信息的内容显然取决于实现。</summary>
    /// <remarks>This field provides an internet URL where more information about the error can be found.</remarks>
    /// <value>The URL.</value>
    [DispId(6), Description(
         "An URL to a page, document, web site,  where more information on the error can be found. The content of this information is obviously implementation dependent.")]
    string moreInfo { get; }
}

// ECapeUnknown 接口
/// <summary>当由操作指定的其他错误不适用时，将引发此异常。</summary>
/// <remarks>CAPE-OPEN 对象可以抛出的标准异常，表示发生的错误不适合该对象支持的其他任何错误。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeUnknown_IID)]
[Description("ECapeUnknown Interface")]
public interface ECapeUnknown { }

// ECapeData 接口
/// <summary>与任何数据相关的错误层次结构的基接口。</summary>
/// <remarks> ECapeDataException 接口是与数据相关的错误的基础接口。数据是操作的参数，
/// 来自Parameter Common Interface 的参数和关于许可证密钥的信息。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeData_IID)]
[Description("ECapeData Interface")]
public interface ECapeData { }

// ECapeLicenceError 接口
/// <summary>操作无法完成，因为许可证协议未得到验证。</summary>
/// <remarks>当然，这种错误也可能出现在 CO 的范围之外。在这种情况下，错误不属于 CO 的错误处理。它是特定于平台的。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeLicenceError_IID)]
[Description("ECapeLicenceError Interface")]
public interface ECapeLicenceError { }

// ECapeBadCOParameter 接口
/// <summary>参数，即来自参数公共接口的对象，具有无效状态。</summary>
/// <remarks>无效参数的名称以及参数本身可以从异常中获取。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeBadCOParameter_IID)]
[Description("ECapeBadCOParameter Interface")]
public interface ECapeBadCOParameter
{
    /// <summary>抛出异常的 CO 参数的名称。</summary>
    /// <remarks>This provides the name of the parameter that threw the exception.</remarks>
    /// <value>The name of the parameter that threw the exception.</value>
    [DispId(1), Description("The name of the CO parameter")]
    string parameterName { get; }

    /// <summary>The parameter that threw the exception.</summary>
    /// <remarks>This method provides access directly to the parameter that threw the exception.</remarks>
    /// <value>A reference to the exception taht threw the exception.</value>
    [DispId(2), Description("The parameter")]
    object parameter { get; }
}

/// <summary>传递了无效的参数值。</summary>
/// <remarks>函数调用包含无效的参数值。例如，过渡阶段名称不属于 CO 阶段列表。</remarks>
[ComImport, ComVisible(false)]
[Guid("678c0b16-7d66-11d2-a67d-00105a42887f")]
[Description("ECapeBadArgument Interface")]
public interface ECapeBadArgument093
{
    /// <summary>操作签名中参数值的位置。第一个参数位于位置 1。</summary>
    /// <remarks>这提供了函数调用的参数列表中无效参数的位置。</remarks>
    /// <value>The position of the argument that is bad. The first argument is 1.</value>
    [DispId(1), Description(
         "The position of the argument value within the signature of the operation. First argument is as position 1.")]
    int position { get; }
}

// CAPE-OPEN v1.0 接口 ECapeBadArgument interface
/// <summary>传递了无效的参数值。</summary>
/// <remarks>函数调用包含无效的参数值。例如，过渡阶段名称不属于 CO 阶段列表。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeBadArgument_IID)]
[Description("ECapeBadArgument Interface")]
public interface ECapeBadArgument
{
    /// <summary>操作签名中参数值的位置。第一个参数位于位置 1。</summary>
    /// <remarks>这提供了函数调用的参数列表中无效参数的位置。</remarks>
    /// <value>The position of the argument that is bad. The first argument is 1.</value>
    [DispId(1), Description(
         "The position of the argument value within the signature of the operation. First argument is as position 1.")]
    short position { get; }
}

// ECapeInvalidArgument 接口
/// <summary>传递了无效的参数值。例如，传递的相位名称不属于 CO 相位列表。</summary>
/// <remarks>操作的一个参数值无效。参数值在操作签名中的位置。第一个参数是位置 1。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeInvalidArgument_IID)]
[Description("ECapeInvalidArgument Interface")]
public interface ECapeInvalidArgument { }

// ECapeOutOfBounds 接口
/// <summary>参数值超出范围。</summary>
/// <remarks>这个类是从 <see cref = "ECapeBoundaries"/> 接口派生的。它用于指示其中一个参数超出了其范围。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeOutOfBounds_IID)]
[Description("ECapeOutOfBounds Interface")]
public interface ECapeOutOfBounds { }

// ECapeNoImpl 接口
/// <summary>一个异常，表示当前对象没有实现请求的操作。</summary>
/// <remarks>即使由于与 CO 标准的兼容性可以调用此操作，该操作“不”被实现。也就是说，操作存在，但当前实现不支持它。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeNoImpl_IID)]
[Description("ECapeNoImpl Interface")]
public interface ECapeNoImpl { }

// ECapeLimitedImpl 接口
/// <summary>实施的限制已被违反。</summary>
/// <remarks>一个操作可以部分实现，例如，一个属性包可以实现 TP 闪蒸，但不能实现 PH 闪蒸。
/// 如果调用者请求 PH 闪蒸，则此错误表示支持特定的闪蒸计算，但不支持所请求的闪蒸计算。
/// 该工厂只能创建一个实例（因为组件是评估副本），当调用者请求第二个创建时，此错误表明此实现是有限的。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeLimitedImpl_IID)]
[Description("ECapeLimitedImpl Interface")]
public interface ECapeLimitedImpl { }

// ECapeImplementation 接口
/// <summary>与当前实现相关的错误层次结构的基类。</summary>
/// <remarks>该类用于表示在对象的实现过程中发生了错误。
/// <see cref = "ECapeNoImpl"/> 和 <see cref = "ECapeLimitedImpl"/> 等与实现相关的类都派生于该类。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeImplementation_IID)]
[Description("ECapeImplementation Interface")]
public interface ECapeImplementation { }

// ECapeOutOfResources 接口
/// <summary>一个异常，表示此操作所需的资源不可用。</summary>
/// <remarks>执行操作所需的实物资源超出了限制。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeOutOfResources_IID)]
[Description("ECapeOutOfResources Interface")]
public interface ECapeOutOfResources { }

// ECapeNoMemory 接口
/// <summary>一个异常，表示此操作所需的内存不可用。</summary>
/// <remarks>执行该操作所需的物理内存超出限制。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeNoMemory_IID)]
[Description("ECapeNoMemory Interface")]
public interface ECapeNoMemory { }

// ECapeTimeOut 接口
/// <summary>达到超时标准时引发异常。</summary>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeTimeOut_IID)]
[Description("ECapeTimeOut Interface")]
public interface ECapeTimeOut { }

// ECapeFailedInitialisation 接口
/// <summary>当必要地初始化没有执行或失败时，抛出该异常。</summary>
/// <remarks>先决条件操作无效。必要地初始化尚未执行或已失败。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeFailedInitialisation_IID)]
[Description("ECapeFailedInitialisation Interface")]
public interface ECapeFailedInitialisation { }

// ECapeSolvingError 接口
/// <summary>一种异常，表示数值算法由于某种原因而失败。</summary>
/// <remarks>表示数值算法因任何原因失败。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeSolvingError_IID)]
[Description("ECapeSolvingError Interface")]
public interface ECapeSolvingError { }

// ECapeBadInvOrder 接口
/// <summary>在操作请求之前，没有调用必要的先决条件操作。</summary>
/// <remarks>在引发此异常的操作之前，必须调用指定的先决条件操作。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeBadInvOrder_IID)]
[Description("ECapeBadInvOrder Interface")]
public interface ECapeBadInvOrder
{
    /// <summary>The necessary prerequisite operation.</summary>
    [DispId(1), Description("The necessary prerequisite operation.")]
    string requestedOperation { get; }
}

// ECapeInvalidOperation 接口
/// <summary>此操作在当前上下文中无效。</summary>
/// <remarks>当试图执行在当前上下文中无效的操作时，将引发此异常。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ECapeInvalidOperation_IID)]
[Description("ECapeInvalidOperation Interface")]
public interface ECapeInvalidOperation { }

// ECapeComputation 接口
/// <summary>与计算相关的错误层次结构的基本接口。</summary>
/// <remarks>此类用于指示计算过程中出现的错误。其他与计算相关的类别，如：
/// <see cref = "ECapeFailedInitialisation"/>, 
/// <see cref = "ECapeOutOfResources"/>, 
/// <see cref = "ECapeSolvingError"/>, 
/// <see cref = "ECapeBadInvOrder"/>, 
/// <see cref = "ECapeInvalidOperation"/>, 
/// <see cref = "ECapeNoMemory"/>, 和 
/// <see cref = "ECapeTimeOut"/> </remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.ECapeComputation_IID)]
[Description("ECapeComputation Interface")]
public interface ECapeComputation { }

// ECapePersistence interface
/// <summary>
/// An exception that indicates that the a persistence-related error has occurred.
/// </summary>
/// <remarks>
/// The base of the errors hierarchy related to the persistence.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid(CapeOpenGuids.ECapePersistence_IID)]
[Description("ECapePersistence Interface")]
public interface ECapePersistence
{
};

// ECapeIllegalAccess interface
/// <summary>
/// The access to something within the persistence system is not authorised.
/// </summary>
/// <remarks>
/// This exception is thrown when the access to something within the persistence system is not authorised.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid(CapeOpenGuids.ECapeIllegalAccess_IID)]
[Description("ECapeIllegalAccess Interface")]
public interface ECapeIllegalAccess
{
};

// ECapePersistenceNotFound interface
/// <summary>
/// An exception that indicates that the persistence was not found.
/// </summary>
/// <remarks>
/// The requested object, table, or something else within the persistence system was not found.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid(CapeOpenGuids.ECapePersistenceNotFound_IID)]
[Description("ECapePersistenceNotFound Interface")]
public interface ECapePersistenceNotFound
{
    /// <summary>
    /// The name of the item.
    /// </summary>
    /// <remarks>
    /// The name of the requested object, table, or something else within the persistence system 
    /// that was not found.
    /// </remarks>
    /// <value>
    /// The name of the item not found.
    /// </value>
    [DispId(1),
     Description("The name of the item")]
    string itemName { get; }
};

// ECapePersistenceSystemError interface
/// <summary>
/// An exception that indicates a severe error occurred within the persistence system.
/// </summary>
/// <remarks>
/// During the persistence process, a severe error occurred within the persistence system.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid(CapeOpenGuids.ECapePersistenceSystemError_IID)]
[Description("ECapePersistenceSystemError Interface")]
public interface ECapePersistenceSystemError
{
};

// ECapePersistenceOverflow interface
/// <summary>
/// An exception that indicates an overflow of internal persistence system.
/// </summary>
/// <remarks>
/// During the persistence process, an overflow of internal persistence system occurred.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid(CapeOpenGuids.ECapePersistenceOverflow_IID)]
[Description("ECapePersistenceOverflow Interface")]
public interface ECapePersistenceOverflow
{
};

/// <summary>
/// An exception that indicates the requested thermodynamic property was not available.
/// </summary>
/// <remarks>
/// At least one item in the requested properties cannot be returned. This could be 
/// because the property cannot be calculated at the specified conditions or for the 
/// specified Phase. If the property calculation is not implemented then 
/// <see cref = "ECapeLimitedImpl"/> should be returned.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid("678C09B6-7D66-11D2-A67D-00105A42887F")]
[Description("ECapeThrmPropertyNotAvailable Interface")]
public interface ECapeThrmPropertyNotAvailable
{
};

/// <summary>
/// Exception thrown when the Hessian for the MINLP problem is not available.
/// </summary>
/// <remarks>
/// Exception thrown when the Hessian for the MINLP problem is not available.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid("3FF0B24B-4299-4DAC-A46E-7843728AD205")]
[Description("ECapeHessianInfoNotAvailable Interface")]
public interface ECapeHessianInfoNotAvailable
{
    /// <summary>
    /// Code to designate the subcategory of the error. 
    /// </summary>
    /// <remarks>
    /// The assignment of values is left to each implementation. So that is a 
    /// proprietary code specific to the CO component provider. By default, set to 
    /// the CAPE-OPEN error HRESULT <see cref = "CapeErrorInterfaceHR"/>.
    /// </remarks>
    /// <value>
    /// The HRESULT value for the exception.
    /// </value>
    [DispId(1),
     Description(
         "Code to designate the subcategory of the error. The assignment of values is left to each implementation. So that is a proprietary code specific to the CO component provider.")]
    int code { get; }

    /// <summary>
    /// The description of the error.
    /// </summary>
    /// <remarks>
    /// The error description can include a more verbose description of the condition that
    /// caused the error.
    /// </remarks>
    /// <value>
    /// A string description of the exception.
    /// </value>
    [DispId(2),
     Description("The description of the error.")]
    string description { get; }

    /// <summary>
    /// The scope of the error.
    /// </summary>
    /// <remarks>
    /// This property provides a list of packages where the error occurs separated by '.'. 
    /// For example CapeOpen.Common.Identification.
    /// </remarks>
    /// <value>The source of the error.</value>
    [DispId(3),
     Description(
         "The scope of the error. The list of packages where the error occurs separated by '.'. For example CapeOpen.Common.Identification.")]
    string scope { get; }

    /// <summary>
    /// The name of the interface where the error is thrown. This is a mandatory field."
    /// </summary>
    /// <remarks>
    /// The interface that the error was thrown.
    /// </remarks>
    /// <value>The name of the interface.</value>
    [DispId(4),
     Description(
         "The name of the interface where the error is thrown. This is a mandatory field.")]
    string interfaceName { get; }

    /// <summary>
    /// The name of the operation where the error is thrown. This is a mandatory field.
    /// </summary>
    /// <remarks>
    /// This field provides the name of the operation being perfomed when the exception was raised.
    /// </remarks>
    /// <value>The operation name.</value>
    [DispId(5),
     Description(
         "The name of the operation where the error is thrown. This is a mandatory field.")]
    string operation { get; }

    /// <summary>
    /// An URL to a page, document, web site,  where more information on the error can be found. The content of this information is obviously implementation dependent.
    /// </summary>
    /// <remarks>
    /// This field provides an internet URL where more information about the error can be found.
    /// </remarks>
    /// <value>The URL.</value>
    [DispId(6),
     Description(
         "An URL to a page, document, web site,  where more information on the error can be found. The content of this information is obviously implementation dependent.")]
    string moreInfo { get; }
};

/// <summary>
/// Exception thrown when the problem is outside the scope of the solver.
/// </summary>
/// <remarks>
/// Exception thrown when the problem is outside the scope of the solver.
/// </remarks>
[ComImport]
[ComVisible(false)]
[System.Runtime.InteropServices.Guid("678c0b0f-7d66-11d2-a67d-00105a42887f")]
[Description("ECapeOutsideSolverScope Interface")]
public interface ECapeOutsideSolverScope
{
    /// <summary>
    /// Code to designate the subcategory of the error. 
    /// </summary>
    /// <remarks>
    /// The assignment of values is left to each implementation. So that is a 
    /// proprietary code specific to the CO component provider. By default, set to 
    /// the CAPE-OPEN error HRESULT <see cref = "CapeErrorInterfaceHR"/>.
    /// </remarks>
    /// <value>
    /// The HRESULT value for the exception.
    /// </value>
    [DispId(1),
     Description(
         "Code to designate the subcategory of the error. The assignment of values is left to each implementation. So that is a proprietary code specific to the CO component provider.")]
    int code { get; }

    /// <summary>
    /// The description of the error.
    /// </summary>
    /// <remarks>
    /// The error description can include a more verbose description of the condition that
    /// caused the error.
    /// </remarks>
    /// <value>
    /// A string description of the exception.
    /// </value>
    [DispId(2),
     Description("The description of the error.")]
    string description { get; }

    /// <summary>
    /// The scope of the error.
    /// </summary>
    /// <remarks>
    /// This property provides a list of packages where the error occurs separated by '.'. 
    /// For example CapeOpen.Common.Identification.
    /// </remarks>
    /// <value>The source of the error.</value>
    [DispId(3),
     Description(
         "The scope of the error. The list of packages where the error occurs separated by '.'. For example CapeOpen.Common.Identification.")]
    string scope { get; }

    /// <summary>
    /// The name of the interface where the error is thrown. This is a mandatory field."
    /// </summary>
    /// <remarks>
    /// The interface that the error was thrown.
    /// </remarks>
    /// <value>The name of the interface.</value>
    [DispId(4),
     Description(
         "The name of the interface where the error is thrown. This is a mandatory field.")]
    string interfaceName { get; }

    /// <summary>
    /// The name of the operation where the error is thrown. This is a mandatory field.
    /// </summary>
    /// <remarks>
    /// This field provides the name of the operation being perfomed when the exception was raised.
    /// </remarks>
    /// <value>The operation name.</value>
    [DispId(5),
     Description(
         "The name of the operation where the error is thrown. This is a mandatory field.")]
    string operation { get; }

    /// <summary>
    /// An URL to a page, document, web site,  where more information on the error can be found. The content of this information is obviously implementation dependent.
    /// </summary>
    /// <remarks>
    /// This field provides an internet URL where more information about the error can be found.
    /// </remarks>
    /// <value>The URL.</value>
    [DispId(6),
     Description(
         "An URL to a page, document, web site,  where more information on the error can be found. The content of this information is obviously implementation dependent.")]
    string moreInfo { get; }
};

// typedef CapeErrorInterfaceHR eCapeErrorInterfaceHR;

/// <summary>
/// The ECapeErrorDummy interface is not intended to be used. 
/// </summary>
/// <remarks>
/// It is only here to ensure that 
/// the MIDL compiler exports the CapeErrorInterfaceHR enumeration. The compiler only exports 
/// an enumeration if it is used in a method of an exported interface. 
/// </remarks>
[ComImport]
[ComVisible(false)]
[Guid(CapeOpenGuids.ECapeErrorDummy_IID)]
[Description("ECapeErrorDummy Interface")]
public interface ECapeErrorDummy
{
    /// <summary>
    /// The HRESULT of the Dummy Error.
    /// </summary>
    /// <remarks>
    /// The HRESULT of the Dummy Error.
    /// </remarks>
    /// <value>
    /// The HRESULT of the Dummy Error.
    /// </value>
    [DispId(1), Description("property Name")]
    int dummy { get; }
}