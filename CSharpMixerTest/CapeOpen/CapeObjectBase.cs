// 大白萝卜重构于 2025.05.10，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CapeOpen;

/// <summary>实现 ICapeIdentification 和 ICapeUtilities 的抽象基类。</summary>
/// <remarks>这个抽象类包含了 ICapeIdentification 和 ICapeUtilities 所需的所有功能。它可以被继承并使用，
/// 就像任何通用的 PMC 一样。派生类将作为 CAPE-OPEN 组件（CapeOpenComponent_CATID 的类别 GUID：678c09a1-7d66-11d2-a67d-00105a42887f）
/// 和流程图监控对象（CATID_MONITORING_OBJECT 的类别 GUID：7BA1AF89-B2E4-493d-BD80-2970BF4CBE99）注册。</remarks>
[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public abstract class CapeObjectBase : CapeIdentification,
    ICapeUtilities, ICapeUtilitiesCOM,
    ECapeUser, ECapeRoot
{
    /// <summary>上次验证单元操作时返回的消息。</summary>
    protected string MValidationMessage;

    [NonSerialized] private Exception _pException;

    // 跟踪是否调用了 Dispose。
    private bool _disposed;

    /// <summary>可由 PMC 使用的模拟上下文。</summary>
    /// <remarks>模拟上下文提供对 PME 的访问，使 PMC 能够访问 PME的 COSE 接口
    /// <see cref="ICapeDiagnostic"/>, <see cref="ICapeMaterialTemplateSystem"/> 和 <see cref="ICapeCOSEUtilities"/>。</remarks>
    [NonSerialized] private ICapeSimulationContext _mSimulationContext;
    
    private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
    {
        return typeof(CapeObjectBase).Assembly;
    }

    /// <summary>显示 PMC 图形界面(如果有)。</summary>
    /// <remarks>默认情况下，此方法会抛出一个 <see cref="CapeNoImplException">CapeNoImplException</see>，
    /// 根据 CAPE-OPEN 规范，该异常被过程建模环境解释为表示 PMC 没有编辑器 GUI，并且 PME 必须执行编辑步骤。
    /// 为了使 PMC 提供自己的编辑器，需要重写 Edit 方法以创建图形编辑器。当用户请求流程图显示编辑器时，
    /// 将调用此方法以编辑单元。重写类不应返回失败（抛出和异常），因为这将被流程图工具解释为单元没有提供自己的编辑器。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    int ICapeUtilitiesCOM.Edit()
    {
        try
        {
            var result = Edit();
            return result == DialogResult.OK ? 0 : 1;
        }
        catch (Exception pEx)
        {
            throw new CapeNoImplException("No editor available");
        }
    }

    /// <summary>获取组件的参数集合。</summary>
    /// <value>返回类型为 System.Object，此方法仅用于基于经典的 COM 的 CAPE-OPEN 互操作。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [Browsable(false)]
    object ICapeUtilitiesCOM.parameters => Parameters;

    /// <summary>设置组件的模拟上下文。</summary>
    /// <remarks>此方法提供对 COSE 接口的访问 <see cref ="ICapeDiagnostic"/>, 
    /// <see cref ="ICapeMaterialTemplateSystem"/> 和 <see cref ="ICapeCOSEUtilities"/>.</remarks>
    /// <value>由流图环境分配的模拟环境。</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [Browsable(false)]
    object ICapeUtilitiesCOM.simulationContext
    {
        set
        {
            if (value is ICapeSimulationContext mContext)
                _mSimulationContext = mContext;
        }
    }
    
    /// <summary>可以在这里执行清理任务。</summary>
    /// <remarks>CAPE-OPEN 对象应该在调用期间释放所有分配的资源。这是由PME在对象析构函数之前调用的。
    /// Terminate 可以检查数据是否已保存，如果不保存则返回错误。此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    void ICapeUtilitiesCOM.Terminate()
    {
        Terminate();
    }

    /// <summary>可以在这里执行初始化。</summary>
    /// <remarks>CAPE_OPEN 对象可以在此方法中分配资源。这是由 PME 在对象构造函数之后调用的。此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    void ICapeUtilitiesCOM.Initialize()
    {
        Initialize();
    }

    /// <summary>单元操作的构造函数。</summary>
    /// <remarks>此方法正在创建对象的参数集合。因此，可以在派生对象的构造函数中或在 <c>Initialize()</c> 调用期间添加参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    protected CapeObjectBase()
    {
        Parameters = [];  // _mParameters = new ParameterCollection();
        _mSimulationContext = null;
        MValidationMessage = "This object has not been validated.";
        _disposed = false;
    }

    /// <summary> <see cref = "CapeObjectBase"/> 类的终结器。</summary>
    /// <remarks>这将最终确定类的当前实例。</remarks>
    ~CapeObjectBase()
    {
        Dispose();
    }

    /// <summary>单元操作的构造函数。</summary>
    /// <remarks>此方法正在创建对象的参数集合。因此，可以在派生对象的构造函数中或在 <c>Initialize()</c> 调用期间添加参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <param name = "name">The name of the PMC.</param>
    protected CapeObjectBase(string name)
        : base(name)
    {
        Parameters = [];  // _mParameters = new ParameterCollection();
        _mSimulationContext = null;
        MValidationMessage = "This object has not been validated.";
        _disposed = false;
    }

    /// <summary>单元操作的构造函数。</summary>
    /// <remarks>此方法正在创建对象的参数集合。因此，可以在派生对象的构造函数中或在 <c>Initialize()</c> 调用期间添加参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    /// <param name = "name">The name of the PMC.</param>
    /// <param name = "description">The description of the PMC.</param>
    protected CapeObjectBase(string name, string description)
        : base(name, description)
    {
        Parameters = [];  // _mParameters = new ParameterCollection();
        _mSimulationContext = null;
        MValidationMessage = "This object has not been validated.";
        _disposed = false;
    }

    /// <summary>创建一个新对象，该对象是当前实例的副本。</summary>
    /// <remarks>克隆可以以深度复制或浅度复制的方式实现。在深度复制中，所有对象都被复制；在浅度复制中，只有顶层对象被复制，
    /// 而较低级别的对象包含引用。生成的克隆对象必须与原始实例具有相同类型或兼容。有关克隆、深度复制与浅度复制以及示例的更多信息，
    /// 请参阅 <see cref="Object.MemberwiseClone"/>。</remarks>
    /// <param name = "objectToBeCopied">The object being copied.</param>
    public CapeObjectBase(CapeObjectBase objectToBeCopied)
        : base(objectToBeCopied)
    {
        _mSimulationContext = objectToBeCopied._mSimulationContext;
        Parameters.Clear();
        foreach (CapeParameter parameter in objectToBeCopied.Parameters)
        {
            Parameters.Add((CapeParameter)parameter.Clone());
        }

        MValidationMessage = "This object has not been validated.";
        _disposed = false;
    }

    /// <summary>创建一个新对象，该对象是当前实例的副本。</summary>
    /// <remarks>克隆可以以深度复制或浅度复制的方式实现。在深度复制中，所有对象都被复制；在浅度复制中，只有顶层对象被复制，
    /// 而较低级别的对象包含引用。生成的克隆对象必须与原始实例具有相同类型或兼容。有关克隆、深度复制与浅度复制以及示例的更多信息，
    /// 请参阅 <see cref="Object.MemberwiseClone"/>。</remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public override object Clone()
    {
        var retVal = (CapeObjectBase)AppDomain.CurrentDomain.
            CreateInstanceAndUnwrap(GetType().AssemblyQualifiedName, GetType().FullName);
        retVal.Parameters.Clear();
        foreach (CapeParameter param in Parameters)
        {
            retVal.Parameters.Add((CapeParameter)param.Clone());
        }

        retVal.SimulationContext = null;
        if (retVal.GetType().IsAssignableFrom(typeof(ICapeSimulationContext)))
            retVal.SimulationContext = _mSimulationContext;
        return retVal;
    }

    // Dispose (bool disposing) 方法在两种不同的情况下执行。如果 disposing 等于 true，则该方法是由用户代码直接或间接调用的。
    // 托管和非托管资源都可以被处理。如果 disposing 等于 false，则该方法是由运行时从析构函数内部调用的，
    // 您不应引用其他对象。只有非托管资源可以被处理。
    /// <summary>释放 CapeIdentification 对象使用的不受管资源，并可选地释放受管资源。</summary>
    /// <remarks>此方法由公共方法 <see href="">Dispose</see> 和 <see href="">Finalize</see>调用。
    /// Dispose() 方法会调用受保护的 Dispose(Boolean) 方法，并将 disposing 参数设置为 true。
    /// Finalize() 方法会调用 Dispose，并将 disposing 参数设置为 false。当 disposing 参数为 true 时，
    /// 此方法会释放此组件引用的任何托管对象持有的所有资源。此方法会调用每个引用对象的 Dispose() 方法。
    /// 留给继承者的注释：Dispose 可以被其他对象多次调用。在重写 Dispose(Boolean) 方法时，
    /// 要小心不要引用在之前调用 Dispose 时已经处理过的对象。有关如何实现 Dispose(Boolean) 的更多信息，
    /// 请参阅 <see href="">实现一个 Dispose 方法</see>。有关 Dispose 和 <see href="">Finalize</see> 的更多信息，
    /// 请参阅 <see href="">清理非托管资源</see> 和 <see href="">重写 Finalize 方法</see>。</remarks> 
    /// <param name = "disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        // 检查是否已调用 Dispose。
        if (_disposed) return;
        // 如果 disposing 等于 true，则会处置所有托管和非托管资源。
        if (disposing)
        {
            if (_mSimulationContext != null)
            {
                if (_mSimulationContext.GetType().IsCOMObject)
                    Marshal.FinalReleaseComObject(_mSimulationContext);
            }

            _mSimulationContext = null;
            Parameters.Clear();
            _disposed = true;
        }
        // 原写法：
        // if (!_disposed)
        // {
        //     if (disposing)
        //     {
        //         if (m_SimulationContext != null)
        //         {
        //             if (m_SimulationContext.GetType().IsCOMObject)
        //                 System.Runtime.InteropServices.Marshal.FinalReleaseComObject(m_SimulationContext);
        //         }
        //         m_SimulationContext = null;
        //         m_Parameters.Clear();
        //         _disposed = true;
        //     }
        //     base.Dispose(disposing);
        // }

        base.Dispose(disposing);
    }

    /// <summary>控制 COM 注册的函数。</summary>
    /// <remarks>这个函数添加了 CAPE-OPEN 方法和工具规范中指定的注册键。特别是，
    /// 它表明这个单元操作实现了 CAPE-OPEN 单元操作类别识别。它还使用 <see cref="CapeNameAttribute"/>、
    /// <see cref="CapeDescriptionAttribute"/>、<see cref="CapeVersionAttribute"/>、<see cref="CapeVendorUrlAttribute"/>、
    /// <see cref="CapeHelpUrlAttribute"/>、<see cref=" CapeAboutAttribute"/>等属性添加了 CapeDescription 注册键。</remarks>
    /// <param name = "t">The type of the class being registered.</param> 
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [ComRegisterFunction]
    public static void RegisterFunction(Type t)
    {
        RegistrationHelper(
            RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32), t);
        RegistrationHelper(
            RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64), t);
    }

    private static void RegistrationHelper(RegistryKey baseKey, Type t)
    {
        var assembly = t.Assembly;
        var versionNumber = (new AssemblyName(assembly.FullName)).Version.ToString();

        var keyname = string.Concat("CLSID\\{", t.GUID.ToString(), "}");
        var classKey = baseKey.CreateSubKey(keyname);
        var catidKey = classKey.CreateSubKey("Implemented Categories");
        catidKey.CreateSubKey(CapeGuids.CapeOpenComponent_CATID);

        var attributes = t.GetCustomAttributes(false);
        var nameInfoString = t.FullName;
        var descriptionInfoString = "";
        var versionInfoString = "";
        var companyUrlInfoString = "";
        var helpUrlInfoString = "";
        var aboutInfoString = "";
        foreach (var pt in attributes)
        {
            switch (pt)
            {
                case CapeUnitOperationAttribute:
                    catidKey.CreateSubKey(CapeGuids.CapeUnitOperation_CATID);
                    break;
                case CapeFlowsheetMonitoringAttribute:
                    catidKey.CreateSubKey(CapeGuids.CATID_MONITORING_OBJECT);
                    break;
                case CapeConsumesThermoAttribute:
                    catidKey.CreateSubKey(CapeGuids.Consumes_Thermo_CATID);
                    break;
                case CapeSupportsThermodynamics10Attribute:
                    catidKey.CreateSubKey(CapeGuids.SupportsThermodynamics10_CATID);
                    break;
                case CapeSupportsThermodynamics11Attribute:
                    catidKey.CreateSubKey(CapeGuids.SupportsThermodynamics11_CATID);
                    break;
                case CapeNameAttribute nameAttribute:
                    nameInfoString = nameAttribute.Name;
                    break;
                case CapeDescriptionAttribute descAttribute:
                    descriptionInfoString = descAttribute.Description;
                    break;
                case CapeVersionAttribute versionAttribute:
                    versionInfoString = versionAttribute.Version;
                    break;
                case CapeVendorUrlAttribute companyUrlAttribute:
                    companyUrlInfoString = companyUrlAttribute.VendorUrl;
                    break;
                case CapeHelpUrlAttribute helpUrlAttribute:
                    helpUrlInfoString = helpUrlAttribute.HelpUrl;
                    break;
                case CapeAboutAttribute aboutAttribute:
                    aboutInfoString = aboutAttribute.About;
                    break;
            }
        }

        var descriptionKey = classKey.CreateSubKey("CapeDescription");
        descriptionKey.SetValue("Name", nameInfoString);
        descriptionKey.SetValue("Description", descriptionInfoString);
        descriptionKey.SetValue("CapeVersion", versionInfoString);
        descriptionKey.SetValue("ComponentVersion", versionNumber);
        descriptionKey.SetValue("VendorURL", companyUrlInfoString);
        descriptionKey.SetValue("HelpURL", helpUrlInfoString);
        descriptionKey.SetValue("About", aboutInfoString);
        catidKey.Close();
        descriptionKey.Close();
        classKey.Close();
    }

    /// <summary>该函数控制在卸载类时从 COM 注册表中删除该类。</summary>
    /// <remarks>该方法将删除添加到类注册表中的所有子键，包括 <see cref="RegisterFunction"/> 方法中添加的 CAPE-OPEN 特定键。</remarks>
    /// <param name = "t">The type of the class being unregistered.</param> 
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [ComUnregisterFunction]
    public static void UnregisterFunction(Type t)
    {
        UnregisterHelper(
            RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32), t);
        UnregisterHelper(
            RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64), t);
    }

    private static void UnregisterHelper(RegistryKey baseKey, Type t)
    {
        var keyName = string.Concat("CLSID\\{", t.GUID.ToString(), "}");
        baseKey.DeleteSubKeyTree(keyName, false);
    }

    /// <summary>可在此处进行初始化。</summary>
    /// <remarks>CAPE-OPEN 对象可以在此方法中分配资源。这是由 PME 在对象构造函数之后调用的。此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    public virtual void Initialize() { }

    /// <summary>可以在这里执行清理任务。</summary>
    /// <remarks>CAPE-OPEN 对象应该在调用期间释放所有分配的资源。这是由 PME 在对象析构函数之前调用的。
    /// Terminate 可以检查数据是否已保存，如果不保存则返回错误。此方法没有输入或输出参数。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    public virtual void Terminate()
    {
        Dispose();
    }

    /// <summary>获取组件的参数集合。</summary>
    /// <remarks>返回公共参数集合 (i.e. <see cref="ICapeCollection"/>)。这些作为一组暴露接口 <see cref="ICapeParameter"/> 的元素交付。
    /// 从那里，客户端可以提取 <see cref="ICapeParameterSpec"/> 接口或任何已类型化的接口，例如 <see cref="ICapeRealParameterSpec"/>，
    /// 一旦客户端确定参数类型为 double。</remarks>
    /// <value>The parameter collection of the unit operation.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    //[System.ComponentModel.EditorAttribute(typeof(ParameterCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Category("Parameter Collection")]
    [TypeConverter(typeof(ParameterCollectionTypeConverter))]
    public ParameterCollection Parameters { get; }

    /// <summary>验证 PMC。</summary>
    /// <remarks>验证参数集合。此方法的基类实现会遍历参数集合，并调用每个成员参数的 <see cref="Validate"/> 方法。
    /// 如果所有参数都是有效的，则 PMC 是有效的，这由 Validate 方法返回 <c>true</c> 表示。</remarks>
    /// <returns>true, if the unit is valid. false, if the unit is not valid.</returns>
    /// <param name = "message">Reference to a string that will contain a message regarding the validation of the parameter.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeBadCOParameter">ECapeBadCOParameter</exception>
    /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
    public virtual bool Validate(ref string message)
    {
        message = "Object is valid.";
        MValidationMessage = message;
        foreach (var mt in Parameters)
        {
            var testString = string.Empty;
            if (mt.Validate(ref testString)) continue;
            message = testString;
            MValidationMessage = message;
            return false;
        }
        return true;
    }

    /// <summary>显示 PMC 图形界面（如果有）。</summary>
    /// <remarks>默认情况下，此方法抛出一个 <see cref="CapeNoImplException">CapeNoImplException</see>，根据 CAPE-OPEN 规范，
    /// 该异常被过程建模环境解释为表示 PMC 没有编辑器 GUI，并且 PME 必须执行编辑步骤。为了使 PMC 提供自己的编辑器，
    /// 需要重写 Edit 方法以创建图形编辑器。当用户请求显示编辑器的流程图时，将调用此方法来编辑单元。重写类不应返回失败（抛出和异常），
    /// 因为这将被流程图工具解释为单元没有提供自己的编辑器。</remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    public virtual DialogResult Edit()
    {
        throw new CapeNoImplException("No Object Editor");
    }


    /// <summary>获取和设置组件的模拟上下文。</summary>
    /// <remarks>该方法可访问 COSE 接口 <see cref="ICapeDiagnostic"/>, 
    /// <see cref="ICapeMaterialTemplateSystem"/> 和 <see cref="ICapeCOSEUtilities"/>。</remarks>
    /// <value>The simulation context assigned by the Flowsheeting Environment.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [Browsable(false)]
    public ICapeSimulationContext SimulationContext
    {
        get => _mSimulationContext;
        set => _mSimulationContext = value;
    }

    /// <summary>获取组件的流程表监控对象。</summary>
    /// <remarks>该方法可访问 COSE 接口 <see cref="ICapeDiagnostic"/>, 
    /// <see cref="ICapeMaterialTemplateSystem"/> 和 <see cref="ICapeCOSEUtilities"/>。</remarks>
    /// <value>The simulation context assigned by the Flowsheeting Environment.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
    [Browsable(false)]
    public ICapeFlowsheetMonitoring FlowsheetMonitoring
    {
        get
        {
            if (_mSimulationContext is ICapeFlowsheetMonitoring)
            {
                return (ICapeFlowsheetMonitoring)_mSimulationContext;
            }

            return null;
        }
    }

    /// <summary>抛出异常并公开异常对象。</summary>
    /// <remarks>这种方法允许派生类遵循 CAPE-OPEN 错误处理标准，同时仍然使用 .Net 异常处理。为了使用此类，
    /// 创建一个从 <see cref="ECapeUser"/> 派生的异常对象。将异常对象作为此函数的参数使用。
    /// 这样，异常中的信息将使用 CAPE-OPEN 异常处理进行暴露，并将被抛给 .Net 客户端。</remarks>
    /// <param name="exception">The exception that will the throw.</param>
    public void ThrowException(Exception exception)
    {
        _pException = exception;
        throw _pException;
    }

    // ECapeRoot 方法，返回 System.ApplicationException 中的消息字符串。
    /// <summary>抛出的异常名称。</summary>
    /// <value>The name of the exception being thrown.</value>
    [Browsable(false)]
    string ECapeRoot.Name => _pException is ECapeRoot exCapeRoot ? exCapeRoot.Name : "";

    /// <summary>指定错误子类别的代码。</summary>
    /// <remarks>值的分配留给每个实现。因此，这是 CO 组件提供程序特有的专有代码。默认情况下，
    /// 设置为 CAPE-OPEN 错误 HRESULT <see cref = "CapeErrorInterfaceHR"/>。</remarks>
    /// <value>The HRESULT value for the exception.</value>
    [Browsable(false)]
    int ECapeUser.code => ((ECapeUser)_pException).code;

    /// <summary>异常或错误的描述信息。</summary>
    /// <remarks>错误描述可以包括对导致错误的条件的详细描述。</remarks>
    /// <value>A string description of the exception.</value>
    [Browsable(false)]
    string ECapeUser.description => ((ECapeUser)_pException).description;

    /// <summary>异常或错误的范围。</summary>
    /// <remarks>此属性提供错误发生位置的包列表。例如 <see cref = "ICapeIdentification"/>。</remarks>
    /// <value>The source of the error.</value>
    [Browsable(false)]
    string ECapeUser.scope => ((ECapeUser)_pException).scope;

    /// <summary>发生错误的接口名称。这是一个必填字段。</summary>
    /// <remarks>发生错误的接口。</remarks>
    /// <value>The name of the interface.</value>
    [Browsable(false)]
    string ECapeUser.interfaceName => ((ECapeUser)_pException).interfaceName;

    /// <summary>发生错误的操作名称。这是必填字段。</summary>
    /// <remarks>该字段提供了异常发生时正在执行的操作的名称。</remarks>
    /// <value>The operation name.</value>
    [Browsable(false)]
    string ECapeUser.operation => ((ECapeUser)_pException).operation;

    /// <summary>一个页面的 URL，文档，网站，其中可以找到有关错误的更多信息。这些信息的内容显然依赖于实现。</summary>
    /// <remarks>This field provides an internet URL where more information about the error can be found.</remarks>
    /// <value>The URL.</value>
    [Browsable(false)]
    string ECapeUser.moreInfo => ((ECapeUser)_pException).moreInfo;

    /// <summary>Writes a message to the terminal.</summary>
    /// <remarks>将字符串写入终端。当需要引起用户注意的消息时调用此方法。实现应确保将字符串写入
    /// 对话框或消息列表，以便用户可以轻松查看。预先，此消息必须尽快显示给用户。</remarks>
    /// <param name = "message">The text to be displayed.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public void PopUpMessage(string message)
    {
        switch (_mSimulationContext)
        {
            case null:
                return;
            case ICapeDiagnostic mCapeDiagnostic:
                mCapeDiagnostic.PopUpMessage(message);
                break;
        }
    }

    /// <summary>将字符串写入 PME 的日志文件。</summary>
    /// <remarks>将字符串写入日志。当需要记录消息进行日志记录时，会调用此方法。预计实现会将字符串写入日志文件或其他日志设备。</remarks>
    /// <param name = "message">The text to be logged.</param>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    public void LogMessage(string message)
    {
        switch (_mSimulationContext)
        {
            case null:
                return;
            case ICapeDiagnostic mCapeDiagnostic:
                mCapeDiagnostic.LogMessage(message);
                break;
        }
    }
}
