// 大白萝卜重构于 2025.05.11，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

// This idl file was ported from the CAPE-OPEN common.idl file and 
// the interfaces were updated to conform to .NET format.

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

/// <summary>表示参数验证状态的枚举标志。</summary>
/// <remarks>枚举的含义如下：
/// (i) notValidated(CAPE_NOT_VALIDATED): The PMC's Validate() method has not been called after the last time that its value had been changed.
/// (ii) invalid(CAPE_INVALID): The last time that the PMC's Validate() method was called it returned false.
/// (iii) valid(CAPE_VALID): the last time that the PMC's Validate() method was called it returned true.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.CapeValidationStatus_IID)]
public enum CapeValidationStatus
{
    /// <summary>PMC 的 Validate() 方法在其值上次更改后未被调用。</summary>
    CAPE_NOT_VALIDATED = 0,

    /// <summary>上次调用 PMC 的 Validate() 方法时，返回的是 false。</summary>
    CAPE_INVALID = 1,

    /// <summary>上次调用 PMC 的 Validate() 方法时，返回值为 true。</summary>
    CAPE_VALID = 2
}

/// <summary>事件，表示组件名称已更改。</summary>
[ComVisible(true)]
[Guid(CapeOpenGuids.InComNameChEvArgsIid)] // F79EA405-4002-4fb2-AED0-C1E48793637D
[Description("CapeIdentificationEvents Interface")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
internal interface IComponentNameChangedEventArgs
{
    /// <summary>项目管理委员会更名前的名称。</summary>
    /// <remarks>设备的原名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The name of the unit prior to the name change.</value>
    string OldName { get; }

    /// <summary>项目管理委员会更名后的名称。</summary>
    /// <remarks>设备的新名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The name of the unit after the name change.</value>
    string NewName { get; }
}

/// <summary>抛出的事件，表示组件的描述已更改。</summary>
[ComVisible(true)]
[Guid(CapeOpenGuids.InComDescChEvArgsIid)] // 34C43BD3-86B2-46d4-8639-E0FA5721EC5C
[Description("CapeIdentificationEvents Interface")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
internal interface IComponentDescriptionChangedEventArgs
{
    /// <summary>项目管理委员会更改前的描述。</summary>
    /// <remarks>设备的新名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The description of the unit prior to the description change.</value>
    string OldDescription { get; }

    /// <summary>项目管理委员会更改后的描述。</summary>
    /// <remarks>设备的新名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The description of the unit after the description change.</value>
    string NewDescription { get; }
}

/// <summary>为 CapeIdentification.ComponentNameChanged 事件提供数据。</summary>
/// <remarks>CapeIdentification.NameChangedEventArgs 事件指定了 PMC 的新旧名称。</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ComNameChEvArgsIid)]  // D78014E7-FB1D-43ab-B807-B219FAB97E8B
[ClassInterface(ClassInterfaceType.None)]
public class ComponentNameChangedEventArgs : EventArgs //, IComponentNameChangedEventArgs
{
    /// <summary>用新旧名称创建 NameChangedEventArgs 类的实例。</summary>
    /// <remarks>当在运行时引发 NameChangedEvent 时，可以使用此构造函数指定其名称已更改的 PMC 的特定名称。</remarks>
    /// <param name = "oldName">The name of the PMC prior to the name change.</param>
    /// <param name = "newName">The name of the PMC after the name change.</param>
    public ComponentNameChangedEventArgs(string oldName, string newName)
    {
        OldName = oldName;
        NewName = newName;
    }

    /// <summary>项目管理委员会更名前的名称。</summary>
    /// <remarks>设备的原名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The name of the unit prior to the name change.</value>
    public string OldName { get; }

    /// <summary>项目管理委员会更名后的名称。</summary>
    /// <remarks>设备的新名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The name of the unit after the name change.</value>
    public string NewName { get; }
}

/// <summary>为 CapeIdentification.ComponentDescriptionChanged 事件提供数据。</summary>
/// <remarks>CapeIdentification.NameChangedEventArgs 事件指定了 PMC 的新旧名称。</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeOpenGuids.ComDescChEvArgsIid)] // 0C51C4F1-20E8-413d-93E1-4704B888354A
[ClassInterface(ClassInterfaceType.None)]
public class ComponentDescriptionChangedEventArgs : EventArgs, IComponentDescriptionChangedEventArgs
{
    /// <summary>用新旧名称创建 DescriptionChangedEventArgs 类的实例。</summary>
    /// <remarks>当在运行时引发 DescriptionChangedEvent 时，可以使用此构造函数来指定其名称已更改的 PMC 的特定描述。</remarks>
    /// <param name = "oldDescription">The description of the PMC prior to the description change.</param>
    /// <param name = "newDescription">The description of the PMC after the description change.</param>
    public ComponentDescriptionChangedEventArgs(string oldDescription, string newDescription)
    {
        OldDescription = oldDescription;
        NewDescription = newDescription;
    }

    /// <summary>项目管理委员会更改前的描述。</summary>
    /// <remarks>设备的新名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The description of the unit prior to the description change.</value>
    public string OldDescription { get; }

    /// <summary>项目管理委员会更改后的描述。</summary>
    /// <remarks>设备的新名称可用于更新有关 PMC 的图形用户界面信息。</remarks>
    /// <value>The description of the unit after the description change.</value>
    public string NewDescription { get; }
}

/// <summary>提供识别和描述 CAPE-OPEN 组件的方法。</summary>
/// <remarks>例如，我们提醒来自现有接口规范的要求以及与识别概念相关的要求：
/// 单位业务接口有以下要求：
/// 如果流程图中包含特定类型的单元操作的两种实例，COSE需要为用户提供一个文本标识符，以区分每个实例。例如，当 COSE 需要报告一个单元操作中发生错误时。
/// 当 COSE 向用户显示其 GUI 以将 COSE 的流连接到单元操作端口时，COSE 需要请求单元获取其可用端口列表。为了帮助用户识别端口，用户需要为每个端口提供一些独特的文本信息。
/// 当 COSE 向用户暴露其接口以浏览或设置单元操作内部参数的值时，COSE 需要请求单元获取其可用参数列表。无论此 COSE 的接口是图形用户界面还是编程接口，每个参数都必须由文本字符串标识。
/// ICapeThermoMaterialObject（单位接口和热敏接口都使用）：
/// 如果单元操作在访问流时遇到错误（<see cref="ICapeThermoMaterialObject"></see>），
/// 单元可能会决定向用户报告。理想情况下，流应该有一个文本标识符，以便用户能够快速知道哪个流失败。
/// 热力学接口有以下要求：
/// <see cref="ICapeThermoSystem"></see> 和 <see cref="ICapeThermoPropertyPackage"></see> 接口不需要一个标识接口，
/// 因为它们都被设计为单例（每个组件类只有一个实例）。这意味着不需要标识这个实例：其类描述就足够了。然而，用户可能仍然决定为其流程中
/// 使用的 CAPE-OPEN 属性系统或属性包分配一个名称或描述。此外，如果这些接口发生变化，单例方法可以被移除。在这种情况下，标识每个实例将是必须的。
/// 求解器接口有以下要求：许多对象都应提供来自识别通用接口的功能。
/// SMST 接口有以下要求：
/// CO 的 SMST组件包依赖于识别接口组件包。接口 ICapeSMSTFactory 必须提供识别功能。参考 Identification Common 接口。</remarks>
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeOpenGuids.InCapeIdentEvIid)] // 5F5087A7-B27B-4b4f-902D-5F66E34A0CBE
[Description("CapeIdentificationEvents Interface")]
internal interface ICapeIdentificationEvents
{
    /// <summary>获取和设置组件的名称。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的用户将这样做。</remarks>
    /// <value>The unique name of the component.</value>
    /// <exception cref="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <param name="sender">The PMC that raised the event.</param>
    /// <param name="args">A <see cref="ComponentNameChangedEventArgs">ParameterDefaultValueChanged</see> that contains information about the event.</param>
    void ComponentNameChanged(
        [MarshalAs(UnmanagedType.IDispatch)] object sender,
        [MarshalAs(UnmanagedType.IDispatch)] object args
    );

    /// <summary>获取和设置组件的描述。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的用户将这样做。</remarks>
    /// <value>The description of the component.</value>
    /// <exception cref="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <param name="sender">The PMC that raised the event.</param>
    /// <param name="args">A <see cref="ComponentDescriptionChangedEventArgs">ParameterDefaultValueChanged</see> that contains information about the event.</param>
    void ComponentDescriptionChanged(
        [MarshalAs(UnmanagedType.IDispatch)] object sender,
        [MarshalAs(UnmanagedType.IDispatch)] object args
    );
}

/// <summary>提供识别和描述 CAPE-OPEN 组件的方法。</summary>
/// <remarks>例如，我们提醒来自现有接口规范的要求以及与识别概念相关的要求：
/// 单位业务接口有以下要求：
/// 如果流程图中包含特定类型的单元操作的两种实例，COSE需要为用户提供一个文本标识符，以区分每个实例。例如，当 COSE 需要报告一个单元操作中发生错误时。
/// 当 COSE 向用户显示其 GUI 以将 COSE 的流连接到单元操作端口时，COSE 需要请求单元获取其可用端口列表。为了帮助用户识别端口，用户需要为每个端口提供一些独特的文本信息。
/// 当 COSE 向用户暴露其接口以浏览或设置单元操作内部参数的值时，COSE 需要请求单元获取其可用参数列表。无论此 COSE 的接口是图形用户界面还是编程接口，每个参数都必须由文本字符串标识。
/// ICapeThermoMaterialObject（单位接口和热敏接口都使用）：
/// 如果单元操作在访问流时遇到错误（<see cref="ICapeThermoMaterialObject"></see>），
/// 单元可能会决定向用户报告。理想情况下，流应该有一个文本标识符，以便用户能够快速知道哪个流失败。
/// 热力学接口有以下要求：
/// <see cref="ICapeThermoSystem"></see> 和 <see cref="ICapeThermoPropertyPackage"></see> 接口不需要一个标识接口，
/// 因为它们都被设计为单例（每个组件类只有一个实例）。这意味着不需要标识这个实例：其类描述就足够了。然而，用户可能仍然决定为其流程中
/// 使用的 CAPE-OPEN 属性系统或属性包分配一个名称或描述。此外，如果这些接口发生变化，单例方法可以被移除。在这种情况下，标识每个实例将是必须的。
/// 求解器接口有以下要求：许多对象都应提供来自识别通用接口的功能。
/// SMST 接口有以下要求：
/// CO 的 SMST组件包依赖于识别接口组件包。接口 ICapeSMSTFactory 必须提供识别功能。参考 Identification Common 接口。</remarks>
[ComImport, ComVisible(true)]
[Guid(CapeOpenGuids.CapeIdentification_IID)]
[Description("CapeIdentification Interface")]
public interface ICapeIdentification
{
    /// <summary>获取和设置组件的名称。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的用户将这样做。</remarks>
    /// <value>The unique name of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [DispId(1), Description("property ComponentName")]
    string ComponentName { get; set; }

    /// <summary>获取和设置组件的描述。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的用户将这样做。</remarks>
    /// <value>The description of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [DispId(1), Description("property ComponentName")]
    string ComponentDescription { get; set; }
}

/// <summary>代表处理更改组件名称的方法。</summary>
/// <remarks>当你创建一个 ComponentNameChangedHandler 委托时，你确定了将处理该事件的方法。要将事件与事件处理程序关联，
/// 请向事件添加委托的实例。除非你删除委托，否则每当事件发生时都会调用事件处理程序。有关委托的更多信息，请参阅“事件和委托”。</remarks>
/// <param name = "sender">The PMC that is the source .</param>
/// <param name = "args">A <see cref = "ComponentNameChangedEventArgs">NameChangedEventArgs</see> that provides information about the name change.</param>
[ComVisible(true)]
public delegate void ComponentNameChangedHandler(object sender, ComponentNameChangedEventArgs args);

/// <summary>代表处理更改组件描述的方法。</summary>
/// <remarks>当你创建一个 ComponentNameChangedHandler 委托时，你确定了将处理该事件的方法。要将事件与事件处理程序关联，
/// 请向事件添加委托的实例。除非你删除委托，否则每当事件发生时都会调用事件处理程序。有关委托的更多信息，请参阅“事件和委托”。</remarks>
/// <param name="sender">The PMC that is the source of the event.</param>
/// <param name="args">A <see cref="ComponentDescriptionChangedEventArgs">DescriptionChangedEventArgs</see> that provides information about the description change.</param>
[ComVisible(true)]
public delegate void ComponentDescriptionChangedHandler(object sender, ComponentDescriptionChangedEventArgs args);

/// <summary>提供识别和描述 CAPE-OPEN 组件的方法。</summary>
/// <remarks>允许用户为每个 PMC 实例分配不同的名称和描述，以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件
/// 和需要此信息的软件组件由同一供应商开发，因此需要设置和获取此信息的 CAPE-OPEN 标准。参考文档：标识通用接口。</remarks>
[Serializable]
[ComSourceInterfaces(typeof(ICapeIdentificationEvents), typeof(INotifyPropertyChanged))]
[ComVisible(true)]
[Guid(CapeOpenGuids.CapeIdentIid)] // BF54DF05-924C-49a5-8EBB-733E37C38085
[Description("CapeIdentification Interface")]
[ClassInterface(ClassInterfaceType.None)]
public abstract class CapeIdentification : // System.ComponentModel.Component,
    ICapeIdentification, IDisposable, ICloneable, INotifyPropertyChanged
{
    /// <summary>The name of the component.</summary>
    private string _mComponentName;

    /// <summary>The description of the component.</summary>
    private string _mComponentDescription;

    // Track whether Dispose has been called.
    private bool _disposed;

    /// <summary>创建 CapeIdentification 类的一个实例，其中包含 PMC 名称和描述的默认值。</summary>
    /// <remarks>这个构造函数使用正在构造的 PMC 对象的 <see cref="System.Type"/> 作为 ComponentName 和 ComponentDescription 属性的
    /// 默认值。如果 PMC 对象具有 <see cref="CapeNameAttribute"></see>，则使用 <see cref="CapeNameAttribute.Name"></see> 属性作为名称。同样，
    /// 如果对象具有 <see cref="CapeDescriptionAttribute"></see>，则使用 <see cref="CapeDescriptionAttribute.Description"/> 属性作为描述。</remarks>
    protected CapeIdentification()
    {
        _disposed = false;
        _mComponentName = GetType().FullName;
        _mComponentDescription = GetType().FullName;
        var attributes = GetType().GetCustomAttributes(false);
        foreach (var mt in attributes)
        {
            switch (mt)
            {
                case CapeNameAttribute nameAttribute:
                    _mComponentName = nameAttribute.Name;
                    break;
                case CapeDescriptionAttribute descriptionAttribute:
                    _mComponentDescription = descriptionAttribute.Description;
                    break;
            }
        }
    }

    /// <summary>创建 CapeIdentification 类的一个实例，其中包含 PMC 的名称和默认描述。</summary>
    /// <remarks>这个构造函数使用提供的名称来构建 PMC 对象的ComponentName。然后为 ComponentDescription 属性分配默认值。
    /// 如果 PMC 对象具有 <see cref="CapeDescriptionAttribute"></see>，则使用 <see cref="CapeDescriptionAttribute.Description"></see> 属性作为描述。</remarks>
    /// <param name = "name">The name of the PMC.</param>
    protected CapeIdentification(string name)
    {
        _disposed = false;
        _mComponentName = name;
        _mComponentDescription = GetType().FullName;
        var attributes = GetType().GetCustomAttributes(false);
        foreach (var mt in attributes)
        {
            if (mt is CapeDescriptionAttribute descriptionAttribute)
                _mComponentDescription = descriptionAttribute.Description;
        }
    }

    /// <summary>用 PMC 的名称和描述创建 CapeIdentification 类的实例。</summary>
    /// <remarks>您可以使用该构造函数指定 PMC 的具体名称和描述。</remarks>
    /// <param name = "name">The name of the PMC.</param>
    /// <param name = "description">The description of the PMC.</param>
    protected CapeIdentification(string name, string description)
    {
        _disposed = false;
        _mComponentName = name;
        _mComponentDescription = description;
    }


    /// <summary>CapeIdentification 类的复制构造函数。</summary>
    /// <remarks>创建一个 CapeIdentification 类的实例，其 ComponentName 等于原始 PMC 的 ComponentName + (Copy)。
    /// 该副本与原始副本具有相同的 CapeDescription。</remarks>
    /// <param name = "objectToBeCopied">The object being copied.</param>
    protected CapeIdentification(CapeIdentification objectToBeCopied)
    {
        _disposed = false;
        _mComponentName = objectToBeCopied.ComponentName + "(Copy)";
        _mComponentDescription = objectToBeCopied.ComponentDescription;
    }

    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <remarks>克隆可以以深度复制或浅度复制的方式实现。在深度复制中，所有对象都被复制；在浅度复制中，只有顶层对象被复制，
    /// 较低级别的对象包含引用。生成的克隆对象必须与原始实例具有相同类型或兼容。有关克隆、深度复制与浅度复制
    /// 以及示例的更多信息，请参阅 <see cref="Object.MemberwiseClone"/>。</remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public abstract object Clone();

    // Implement IDisposable.
    // 不要将此方法设为虚拟方法。
    // 派生类不能覆盖此方法。
    /// <summary>释放 CapeIdentification 对象使用的所有资源。</summary>
    /// <remarks>当您完成使用 CapeIdentification 对象时，请调用 Dispose 方法。
    /// Dispose 方法将 CapeIdentification 对象置于无法使用的状态。调用 Dispose 后，您必须释放所有对 Component 的引用，
    /// 以便垃圾回收器可以回收 CapeIdentification 对象占用的内存。有关更多信息，请参阅清理非托管资源和实现 Dispose 方法。</remarks> 
    public void Dispose()
    {
        Dispose(true);
        // 此对象将由 Dispose 方法清理。因此，您应该调用 GC.SuppressFinalize 将此对象从终结队列中移除，并防止此对象的终结代码再次执行。
        GC.SuppressFinalize(this);
    }

    // Dispose (bool disposing) 方法在两种不同的情况下执行。如果 disposing 等于 true，则该方法是由用户代码直接或间接调用的。
    // 托管和非托管资源都可以被处理。如果 disposing 等于 false，则该方法是由运行时从析构函数内部调用的，您不应引用其他对象。只有非托管资源可以被处理。
    /// <summary> 释放 CapeIdentification 对象使用的不受管资源，并可选地释放受管资源。</summary>
    /// <remarks>此方法由公共方法 <see href="">Dispose</see> 和 <see href="">Finalize</see>调用。
    /// Dispose() 方法会调用受保护的 Dispose(Boolean) 方法，并将 disposing 参数设置为 true。
    /// Finalize() 方法会调用 Dispose，并将 disposing 参数设置为 false。当 disposing 参数为 true 时，
    /// 此方法会释放此组件引用的任何托管对象持有的所有资源。此方法会调用每个引用对象的 Dispose() 方法。
    /// 留给继承者的注释：Dispose 可以被其他对象多次调用。在重写 Dispose(Boolean) 方法时，
    /// 要小心不要引用在之前调用 Dispose 时已经处理过的对象。有关如何实现 Dispose(Boolean) 的更多信息，
    /// 请参阅 <see href=""> 实现一个 Dispose 方法 </see>。有关 Dispose 和 <see href="">Finalize</see> 的更多信息，
    /// 请参阅 <see href=""> 清理非托管资源 </see> 和 <see href="">重写 Finalize 方法</see>。</remarks> 
    /// <param name = "disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        // 检查是否已调用 Dispose。
        if (_disposed) return;
        // 如果 disposing 等于 true，则会处置所有托管和非托管资源。
        if (disposing)
        {
            // 处置受管资源。
            //component.Dispose();
        }
        // Note disposing has been done.
        _disposed = true;
    }

    /// <summary>通知集合参数的属性值已更改。</summary>
    /// <remarks>PropertyChanged 事件可以使用 null 或 String.Empty 作为 PropertyChangedEventArgs 中的属性名来指示对象上的所有属性都已更改。</remarks>
    public event PropertyChangedEventHandler PropertyChanged;

    // 此方法由每个属性的 Set 访问器调用。应用于 optional propertyName 参数的 CallerMemberName 属性会导致调用者的属性名被替换为参数。
    /// <summary>通知集合参数的属性值已更改。</summary>
    /// <remarks>PropertyChanged 事件可以使用 null 或 String.Empty 作为 PropertyChangedEventArgs 中的属性名来指示对象上的所有属性都已更改。</remarks>
    /// <param name="propertyName">The name of the property that was changed.</param>
    protected void NotifyPropertyChanged( // .Net 4.5 [System.Runtime.CompilerServices.CallerMemberName]
        string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>当用户更改组件名称时发生。</summary>
    /// <remarks>PMC 名称更改时要处理的事件。</remarks> 
    public event ComponentNameChangedHandler ComponentNameChanged;

    /// <summary>当用户更改组件的描述时发生。</summary>
    /// <remarks>通过委托调用事件处理程序时，会引发事件。<c>OnComponentNameChanged</c> 方法还允许子类在不附加委托的情况下处理事件。这是子类处理事件的优选技术。
    /// 留给继承者的注释：当在派生类中重写 <c>OnComponentNameChanged</c> 时，请务必调用基类的 <c>OnComponentNameChanged</c> 方法，以便注册的委托接收事件。</remarks>
    /// <param name = "args">A <see cref="ComponentNameChangedEventArgs">NameChangedEventArgs</see> that contains information about the event.</param>
    protected void OnComponentNameChanged(ComponentNameChangedEventArgs args)
    {
        ComponentNameChanged?.Invoke(this, args);
    }

    /// <summary>当用户更改组件的描述时发生。</summary>
    /// <remarks>当 PMC 的描述发生变化时要处理的事件。</remarks> 
    public event ComponentDescriptionChangedHandler ComponentDescriptionChanged;

    /// <summary>当用户更改组件的描述时发生。</summary>
    /// <remarks>通过委托调用事件处理程序时，会引发事件。<c>OnComponentDescriptionChanged</c> 方法还允许子类在不附加委托的情况下处理事件。
    /// 这是子类处理事件的优选技术。对继承者的说明：在子类中重写<c>OnComponentDescriptionChanged</c>时，
    /// 请务必调用基类的<c>OnComponentDescriptionChanged()</c>方法，以便注册过的委托能够接收到事件。</remarks>
    /// <param name="args">A <see cref="ComponentDescriptionChangedEventArgs">DescriptionChangedEventArgs</see> that contains information about the event.</param>
    protected void OnComponentDescriptionChanged(ComponentDescriptionChangedEventArgs args)
    {
        ComponentDescriptionChanged?.Invoke(this, args);
    }

    /// <summary>获取和设置组件的名称。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的使用者会这样做。</remarks>
    /// <value>The unique name of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Description("Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("Identification")]
    public virtual string ComponentName
    {
        get => _mComponentName;
        set {
            var args = new ComponentNameChangedEventArgs(_mComponentName, value);
            _mComponentName = value;
            NotifyPropertyChanged(nameof(ComponentName));
            OnComponentNameChanged(args);
        }
    }

    /// <summary>获取和设置组件的描述。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的使用者会这样做。</remarks>
    /// <value>The description of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("Identification")]
    public virtual string ComponentDescription
    {
        get => _mComponentDescription;
        set {
            var args =
                new ComponentDescriptionChangedEventArgs(_mComponentDescription, value);
            _mComponentDescription = value;
            NotifyPropertyChanged(nameof(ComponentDescription));
            OnComponentDescriptionChanged(args);
        }
    }
}

/// <summary>此接口提供了只读集合的行为。它可用于存储端口或参数。</summary>
/// <remarks>Collection 接口的目的是为 CAPE-OPEN 组件提供向组件的任何客户端展示对象列表的可能性。
/// 客户端将无法修改集合，即删除、替换或添加元素。但是，由于客户端将能够访问由集合项展示的任何 CAPE-OPEN 接口，
/// 因此它将能够修改任何元素的状态。CAPE-OPEN 集合不允许暴露基本类型，如数值或字符串。实际上，
/// 在这里使用 CapeArrays 更方便。集合中的所有项目不一定属于同一类。如果它们实现了相同的接口或一组接口，
/// 那就足够了。一个暴露集合接口的 CAPE-OPEN 规范必须清楚地说明集合中的所有项目必须实现哪些接口。
/// 参考文档：Collection Common 接口。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeCollection_IID)]
[Description("ICapeCollection Interface")]
internal interface ICapeCollection
{
    /// <summary>获取集合中存储的特定项，该项由其 ICapeIdentification.ComponentName 或以参数形式传递的基于 1 的索引标识。</summary>
    /// <remarks>从集合中返回一个元素。请求的元素可以通过其实际名称（例如类型 CapeString）或其在集合中的位置（例如类型 CapeLong）来识别。
    /// 元素的名称是其 ICapeIdentification 接口的 ComponentName() 方法返回的值。通过名称而不是通过位置检索项的优势在于，它更高效。
    /// 这是因为从服务器部分检查所有名称比从客户端检查要快得多，在客户端需要大量的 COM/CORBA 调用。</remarks>
    /// <param name = "index"><para>Identifier for the requested item:</para>
    /// <para>name of item (the variant contains a string)</para> <para>position in collection (it contains a long)</para></param>
    /// <returns>System.Object containing the requested collection item.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    [DispId(1), Description("Gets an item specified by index or name")]
    [return: MarshalAs(UnmanagedType.IDispatch)] // 该属性说明返回值是 IDispatch 指针。
    object Item(object index);

    /// <summary>获取当前存储在集合中的项目数。</summary>
    /// <returns>Return the number of items in the collection.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [DispId(2), Description("Number of items in the collection")]
    int Count();
}

/// <summary>本接口它暴露 PMC 的参数，控制 PMC 的生命周期，通过模拟上下文提供对 PME 的访问，并为 PME 提供编辑 PMC 的手段。</summary>
/// <remarks>当 PME 需要某种功能时，在 CAPE-OPEN 类别的帮助下，用户可以选择和创建一个 CO 类，该 CO 类将暴露所需的 CO 接口。
/// PME 需要与 PMC 的此实例交换一些信息。这些信息包括一组简单的、不相关的功能，这些功能对任何类型的 CAPE-OPEN 组件都有用，
/// 因为它们将允许客户端和服务器之间最大程度的集成。所有这些功能都可以组合在一个接口中。一些需要实现的功能包括在 PMC 和 PME 之间
/// 交换接口引用。与其将这些属性添加到每个业务接口中，不如将它们添加到单个公共接口中，该接口引用整个 PMC。此外，还需要获取参数、编辑和生命周期。
/// 接口应满足以下要求:
/// Parameters:
/// 到目前为止，只有单元操作可以通过属性 ICapeUnit.Parameters 来暴露其公共参数，该属性返回一组参数。这个属性允许 COSEs 支持
/// 两个 CAPE-OPEN 单元操作之间的设计规范。这意味着 CAPE-OPEN 接口足够强大，可以允许给定单元操作（通过公共参数暴露）的设计规范
/// 依赖于其他 CAPE-OPEN 单元操作所暴露的公共参数的转换。如果其他组件，如材料对象，也能够暴露公共参数，则可以扩展上述功能。
/// 其他功能包括:
/// (i) 允许优化器使用任何 CAPE-OPEN 组件公开的公共变量。
/// (ii) 允许对 CAPE-OPEN 属性包的交互参数执行回归。
/// 将访问这些集合的属性集中在一个单一入口点，有助于澄清这些集合的生存周期使用标准。这意味着 PMC 客户将更容易知道他们需要多频繁地检查
/// 这些集合的内容是否已更改（尽管集合对象在 PMC 被销毁之前是有效的）。为这些集合的使用设置通用规则，可以使业务接口规范更加规范和简单。
/// 显然，由于过于笼统的规则可能会降低灵活性，PMC 规范可能会指出笼统规则的例外。让我们看看这会如何影响特定的 PMC 规格:
/// 模拟环境：
/// 到目前为止，大多数 CAPE-OPEN 接口的设计都是为了允许客户端访问 CAPE-OPEN 组件的功能。由于客户端通常是仿真环境，
/// 因此 CAPE-OPEN 组件将受益于使用其客户端提供的服务，例如 COSE。这些由任何 PMEs 提供的服务在仿真上下文 COSE 接口规范文档中进行了定义。
/// 设计了以下界面:
/// (i) 热材料模板系统：此接口允许 PMC 在 PME 支持的所有热材料工厂之间进行选择。这些工厂将允许 PMC 创建与所选属性包（可以是 CAPE-OPEN 或非 CAPE-OPEN）相关联的热材料对象。
/// (ii) 诊断：此接口将允许将任何PMC生成的诊断消息与PME支持的机制无缝集成，以向用户显示此信息。
/// (iii) COSEUtilities：与这个规范文档的相同理念，PME 也有自己的实用程序接口，以收集许多基本操作。例如，它允许 PME 提供标准化值列表。
/// Edit:
/// UNIT 规范定义的 Edit 方法被证明非常有用，因为它提供了高度定制化的图形用户界面（GUI）功能，以适应每种 UNIT 实现。
/// 没有理由其他 PMC 不能受益于这种功能。显然，当 PMC 提供 Edit 功能时，能够持久化其状态是一个期望的要求，以防止用户不得不反复重新配置 PMC。
/// LifeCycle:
/// 可能没有直接暴露初始化或销毁函数的严格必要性，因为这些函数应该由使用的中间件（COM/CORBA）自动调用。也就是说，初始化可以在类的构造函数中执行，
/// 销毁可以在其析构函数中执行。然而，在某些情况下，客户端可能需要显式调用它们。例如，所有可能失败的操作都应该由这些方法调用。如果这些操作放在构造函数
/// 或析构函数中，潜在的失败会导致内存泄漏，并且它们将难以追踪，因为不清楚组件是否已创建/销毁。有用的案例示例:
/// (i) 初始化：客户端可能需要按特定顺序初始化一组给定的 PMC，以防它们之间存在依赖关系。一些 PMC 可能是其他组件的包装器，或者可能需要外部文件才能初始化。这个初始化过程经常可能会失败，或者用户甚至可能会决定取消它。将这些操作从类构造函数移动到初始化方法中，可以让客户端知道在某些情况下必须中止组件的构造。
/// (ii) 析构函数：PMC 的主要对象应该在这里销毁所有次要对象。如果 PMC 对象之间存在循环引用，依赖原生析构函数可能会导致死锁。在下面的示例图中可以看到，在客户端释放其对 Unit Operation 的引用后，Unit 和 Parameter 都被其他对象使用。因此，如果没有显式的终止方法，它们都不会被终止。
/// 参考文档： Utilities Common 接口。</remarks>
[ComImport, ComVisible(false)]
[Guid(CapeOpenGuids.ICapeUtilities_IID)]
[Description("ICapeUtilities Interface")]
internal interface ICapeUtilitiesCOM
{
    /// <summary>获取组件的参数集合。</summary>
    /// <remarks>返回公共单元参数（即 <see cref="ICapeCollection"/>）的集合。这些参数作为暴露接口 <see cref="ICapeParameter"/> 的元素集合提供。
    /// 从那里，客户端可以提取 <see cref="ICapeParameterSpec"/> 接口或任何类型化接口，如 <see cref="ICapeRealParameterSpec"/>，一旦客户端确定参数类型为 double。</remarks>
    /// <value>The parameter collection of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Gets parameter collection")]
    //[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
    object parameters
    {
        [return: MarshalAs(UnmanagedType.IDispatch)] get;
    }

    /// <summary>设置组件的模拟上下文。</summary>
    /// <remarks>允许 PME 向 PMC传递其模拟上下文的引用。模拟上下文将是 PME 对象，这些对象将暴露一组给定的 CO 接口。
    /// 这些接口中的每一个都将允许 PMC 调用 PME，以利用其暴露的服务（例如创建材料模板、诊断或测量单位转换）。如果 PMC 不支持访问模拟上下文，
    /// 建议引发 ECapeNoImpl 错误。最初，此方法仅在 ICapeUnit 接口中可用。由于 ICapeUtilities.SetSimulationContext 现在可用于
    /// 任何类型的 PMC，因此 ICapeUnit.SetSimulationContext 已被弃用。</remarks>
    /// <value>引用 PME 的模拟上下文类。对于PMC要使用此类，此引用必须转换为每个定义的 CO 模拟上下文接口。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(2), Description("Set the simulation context")]
    object simulationContext
    {
        [param: MarshalAs(UnmanagedType.IDispatch)] set;
    }

    /// <summary>要求组件对自身进行配置。例如，一个单元操作可能在这个调用期间创建端口和参数。</summary>
    /// <remarks>最初，此方法仅存在于 ICapeUnit 接口中。由于 ICapeUtilities.Initialize 现在可用于任何类型的 PMC，
    /// 因此 ICapeUnit.Initialize 已被弃用。PME 将命令 PMC 通过此方法进行初始化。任何可能失败的初始化都必须放在这里。
    /// 初始化被保证是客户端调用的前一个方法（除了低级方法，如类构造函数或初始化持久性方法）。当 PMC 在特定流程图中实例化时，
    /// 初始化必须被调用一次。当初始化失败时，在发出错误信号之前，PMC 必须释放所有在失败发生前分配的资源。
    /// 当 PME 接收到此错误时，它可能不再使用 PMC。当前接口的终止方法也不能被调用。因此，PME 只能通过中间件原生机制释放 PMC。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(3),
     Description("Configuration has to take place here")]
    void Initialize();

    /// <summary>可以在这里执行清理任务。这里发布了参数和端口的引用。</summary>
    /// <remarks>最初，此方法仅存在于 ICapeUnit 接口中。由于 ICapeUtilities.Terminate 现在可用于任何类型的 PMC，
    /// 因此 ICapeUnit.Terminate 已被弃用。PME 将命令 PMC 通过此方法被销毁。任何可能失败的初始化必须放在这里。
    /// Terminate 保证是客户端调用的最后一个方法（除了低级方法如类析构函数）。
    /// Terminate 可以随时调用，但可能只调用一次。当此方法返回错误时，PME 应向用户报告。但是，之后 PME 将不再允许使用 PMC。
    /// Unit 规范指出，“Terminate 可以检查数据是否已保存，如果未保存则返回错误。”建议不要遵循此建议，
    /// 因为保存 PMC 状态是 PME 的责任，而不是在终止它之前。在用户想要关闭模拟案例而不保存它的情况下，
    /// 最好让 PME 来处理这种情况，而不是每个 PMC 提供不同的实现。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(4), Description("Clean up has to take place here")]
    void Terminate();

    /// <summary>Displays the PMC graphic interface, if available.</summary>
    /// <remarks>PMC 显示其用户界面，并允许流程图用户与之交互。如果没有可用的用户界面，则返回一个错误。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [DispId(5), Description("Displays the graphic interface")]
    [PreserveSig]
    int Edit();
}

/// <summary>本接口它暴露 PMC 的参数，控制 PMC 的生命周期，通过模拟上下文提供对 PME 的访问，并为 PME 提供编辑 PMC 的手段。</summary>
/// <remarks>当 PME 需要某种功能时，在 CAPE-OPEN 类别的帮助下，用户可以选择和创建一个 CO 类，该 CO 类将暴露所需的 CO 接口。
/// PME 需要与 PMC 的此实例交换一些信息。这些信息包括一组简单的、不相关的功能，这些功能对任何类型的 CAPE-OPEN 组件都有用，
/// 因为它们将允许客户端和服务器之间最大程度的集成。所有这些功能都可以组合在一个接口中。一些需要实现的功能包括在 PMC 和 PME 之间
/// 交换接口引用。与其将这些属性添加到每个业务接口中，不如将它们添加到单个公共接口中，该接口引用整个 PMC。此外，还需要获取参数、编辑和生命周期。
/// 接口应满足以下要求:
/// Parameters:
/// 到目前为止，只有单元操作可以通过属性 ICapeUnit.Parameters 来暴露其公共参数，该属性返回一组参数。这个属性允许 COSEs 支持
/// 两个 CAPE-OPEN 单元操作之间的设计规范。这意味着 CAPE-OPEN 接口足够强大，可以允许给定单元操作（通过公共参数暴露）的设计规范
/// 依赖于其他 CAPE-OPEN 单元操作所暴露的公共参数的转换。如果其他组件，如材料对象，也能够暴露公共参数，则可以扩展上述功能。
/// 其他功能包括:
/// (i) 允许优化器使用任何 CAPE-OPEN 组件公开的公共变量。
/// (ii) 允许对 CAPE-OPEN 属性包的交互参数执行回归。
/// 将访问这些集合的属性集中在一个单一入口点，有助于澄清这些集合的生存周期使用标准。这意味着 PMC 客户将更容易知道他们需要多频繁地检查
/// 这些集合的内容是否已更改（尽管集合对象在 PMC 被销毁之前是有效的）。为这些集合的使用设置通用规则，可以使业务接口规范更加规范和简单。
/// 显然，由于过于笼统的规则可能会降低灵活性，PMC 规范可能会指出笼统规则的例外。让我们看看这会如何影响特定的 PMC 规格:
/// 模拟环境：
/// 到目前为止，大多数 CAPE-OPEN 接口的设计都是为了允许客户端访问 CAPE-OPEN 组件的功能。由于客户端通常是仿真环境，
/// 因此 CAPE-OPEN 组件将受益于使用其客户端提供的服务，例如 COSE。这些由任何 PMEs 提供的服务在仿真上下文 COSE 接口规范文档中进行了定义。
/// 设计了以下界面:
/// (i) 热材料模板系统：此接口允许 PMC 在 PME 支持的所有热材料工厂之间进行选择。这些工厂将允许 PMC 创建与所选属性包（可以是 CAPE-OPEN 或非 CAPE-OPEN）相关联的热材料对象。
/// (ii) 诊断：此接口将允许将任何PMC生成的诊断消息与PME支持的机制无缝集成，以向用户显示此信息。
/// (iii) COSEUtilities：与这个规范文档的相同理念，PME 也有自己的实用程序接口，以收集许多基本操作。例如，它允许 PME 提供标准化值列表。
/// Edit:
/// UNIT 规范定义的 Edit 方法被证明非常有用，因为它提供了高度定制化的图形用户界面（GUI）功能，以适应每种 UNIT 实现。
/// 没有理由其他 PMC 不能受益于这种功能。显然，当 PMC 提供 Edit 功能时，能够持久化其状态是一个期望的要求，以防止用户不得不反复重新配置 PMC。
/// LifeCycle:
/// 可能没有直接暴露初始化或销毁函数的严格必要性，因为这些函数应该由使用的中间件（COM/CORBA）自动调用。也就是说，初始化可以在类的构造函数中执行，
/// 销毁可以在其析构函数中执行。然而，在某些情况下，客户端可能需要显式调用它们。例如，所有可能失败的操作都应该由这些方法调用。如果这些操作放在构造函数
/// 或析构函数中，潜在的失败会导致内存泄漏，并且它们将难以追踪，因为不清楚组件是否已创建/销毁。有用的案例示例:
/// (i) 初始化：客户端可能需要按特定顺序初始化一组给定的 PMC，以防它们之间存在依赖关系。一些 PMC 可能是其他组件的包装器，或者可能需要外部文件才能初始化。这个初始化过程经常可能会失败，或者用户甚至可能会决定取消它。将这些操作从类构造函数移动到初始化方法中，可以让客户端知道在某些情况下必须中止组件的构造。
/// (ii) 析构函数：PMC 的主要对象应该在这里销毁所有次要对象。如果 PMC 对象之间存在循环引用，依赖原生析构函数可能会导致死锁。在下面的示例图中可以看到，在客户端释放其对 Unit Operation 的引用后，Unit 和 Parameter 都被其他对象使用。因此，如果没有显式的终止方法，它们都不会被终止。
/// 参考文档： Utilities Common 接口。</remarks>
[ComVisible(false)]
[Description("ICapeUtilities Interface")]
public interface ICapeUtilities
{
    /// <summary>获取组件的参数集合。</summary>
    /// <remarks>返回公共单元参数（即 <see cref="ICapeCollection"/>）的集合。这些参数作为暴露
    /// 接口 <see cref="ICapeParameter"/> 的元素集合提供。从那里，客户端可以
    /// 提取 <see cref="ICapeParameterSpec"/> 接口或任何类型化接口，如 <see cref="ICapeRealParameterSpec"/>，
    /// 一旦客户端确定参数类型为 double。</remarks>
    /// <value>The parameter collection of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(1), Description("Gets parameter collection")]
    ParameterCollection Parameters { get; }

    /// <summary>设置组件的模拟上下文。</summary>
    /// <remarks>允许 PME 向 PMC 传递其模拟上下文的引用。模拟上下文将是 PME 对象，这些对象将暴露一组给定的 CO 接口。
    /// 这些接口中的每一个都将允许 PMC 调用 PME，以利用其暴露的服务（例如创建材料模板、诊断或测量单位转换）。
    /// 如果 PMC 不支持访问模拟上下文，建议引发 ECapeNoImpl 错误。最初，此方法仅在 ICapeUnit 接口中可用。
    /// 由于 ICapeUtilities.SetSimulationContext 现在可用于任何类型的 PMC，因此 ICapeUnit.SetSimulationContext 已被弃用。</remarks>
    /// <value>引用 PME 的模拟上下文类。对于PMC要使用此类，此引用必须转换为每个定义的 CO 模拟上下文接口。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [DispId(2), Description("Set the simulation context")]
    ICapeSimulationContext SimulationContext { get; set; }

    /// <summary>要求组件对自身进行配置。例如，一个单元操作可能在这个调用期间创建端口和参数。</summary>
    /// <remarks>最初，此方法仅存在于 ICapeUnit 接口中。由于 ICapeUtilities.Initialize 现在可用于任何类型的 PMC，
    /// 因此 ICapeUnit.Initialize 已被弃用。PME 将命令 PMC 通过此方法进行初始化。任何可能失败的初始化都必须放在这里。
    /// 初始化被保证是客户端调用的前一个方法（除了低级方法，如类构造函数或初始化持久性方法）。当 PMC 在特定流程图中实例化时，
    /// 初始化必须被调用一次。当初始化失败时，在发出错误信号之前，PMC 必须释放所有在失败发生前分配的资源。
    /// 当 PME 接收到此错误时，它可能不再使用 PMC。当前接口的终止方法也不能被调用。因此，PME 只能通过中间件原生机制释放 PMC。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(3), Description("Configuration has to take place here")]
    void Initialize();

    /// <summary>可以在这里执行清理任务。这里发布了参数和端口的引用。</summary>
    /// <remarks>最初，此方法仅存在于 ICapeUnit 接口中。由于 ICapeUtilities.Terminate 现在可用于任何类型的 PMC，
    /// 因此 ICapeUnit.Terminate 已被弃用。PME 将命令 PMC 通过此方法被销毁。任何可能失败的初始化必须放在这里。
    /// Terminate 保证是客户端调用的最后一个方法（除了低级方法如类析构函数）。
    /// Terminate 可以随时调用，但可能只调用一次。当此方法返回错误时，PME 应向用户报告。但是，之后 PME 将不再允许使用 PMC。
    /// Unit 规范指出，“Terminate 可以检查数据是否已保存，如果未保存则返回错误。”建议不要遵循此建议，
    /// 因为保存 PMC 状态是 PME 的责任，而不是在终止它之前。在用户想要关闭模拟案例而不保存它的情况下，
    /// 最好让 PME 来处理这种情况，而不是每个 PMC 提供不同的实现。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    [DispId(4), Description("Clean up has to take place here")]
    void Terminate();

    /// <summary>显示PMC图形界面(如果有)。</summary>
    /// <remarks>PMC 显示其用户界面，并允许流程图用户与之交互。如果没有可用的用户界面，则返回一个错误。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [DispId(5), Description("Displays the graphic interface")]
    DialogResult Edit();
}

/// <summary>表示将处理 PMC 模拟上下文更改的方法。</summary>
[ComVisible(false)]
public delegate void SimulationContextChangedHandler(object sender, EventArgs args);