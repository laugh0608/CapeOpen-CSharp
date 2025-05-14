// 大白萝卜重构于 2025.05.12，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace CapeOpen;

/// <summary>这是所有的抽象基类。基于 .NET 的 CAPE-OPEN 异常类。</summary>
/// <remarks>.NET 与 COM 相比的主要优点之一是在异常处理中包含了额外的信息。在 COM 中，
/// 异常是通过返回一个 HRESULT 值来处理的，它是一个整数，指示函数调用是否已经成功返回（Rogerson，1997）。
/// 因为 HRESULT 值是 32 位整数，它可以指示比单纯的成功或失败更多的信息，但它的局限性在于它不包括关于发生的异常的描述性信息。
/// 在 .NET 中，有一个可用的应用程序异常类（System.ApplicationException），可以用来提供消息和异常来源等信息。
/// CAPE-OPEN 异常定义都继承自 ECapeRoot 接口（Belaud et al. 2001）。在 CAPE-OPEN 异常类的当前实现中，
/// 所有异常类都继承自 CapeUserException 类，而 CapeUserException 类本身又继承自 .NET 的 System.ApplicationException 类。
/// CapeUserException 类暴露了 <see c="ECapeRoot"/> 和 <see c="ECapeUser"/> 接口。这样，由过程建模组件抛出的所有异常，
/// 除了作为派生异常类型被捕获外，还可以作为 CapeRootException 或 System.ApplicationException 被捕获。</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeGuids.CapeUserExIid)] // "28686562-77AD-448f-8A41-8CF9C3264A3E"
[Description("")]
[ClassInterface(ClassInterfaceType.None)]
public abstract class CapeUserException : ApplicationException,
    ECapeRoot, ECapeUser
{
    /// <summary>正在引发的异常的异常接口的名称。</summary>
    /// <remarks>MInterfaceName 字段在 <see c="Initialize">Initialize</see> 方法中为异常设置。
    /// 任何从 CapeUserException 类派生的异常都需要在 Initialize 方法中设置此值。</remarks>
    protected string MInterfaceName;

    /// <summary>抛出的异常名称。</summary>
    /// <remarks>MName 字段在 <see c="Initialize">Initialize</see> 方法中为异常设置。
    /// 任何从 CapeUserException 类派生的异常都需要在 Initialize 方法中设置此值。</remarks>
    protected string MName;

    /// <summary>抛出的异常描述。</summary>
    /// <remarks>MDescription 字段在 <see c="Initialize">Initialize</see> 方法中为异常设置。
    /// 任何从 CapeUserException 类派生的异常都需要在 Initialize 方法中设置此值。</remarks>
    protected string MDescription;

    /// <summary>初始化 CapeUserException 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system 
    /// culture.</remarks>
    protected CapeUserException()
    {
        MDescription = "An application error has occurred.";
        Initialize();
    }

    /// <summary>用指定的错误信息初始化 CapeUserException 类的新实例。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    protected CapeUserException(string message) : base(message)
    {
        MDescription = message;
        Initialize();
    }

    // 需要注意的是，该构造函数序列化方法已经过时，在 .NET8+ 中已经不需要执行序列化
    // 但这里仍然需要，用作其他函数的依赖。
    /// <summary>使用序列化数据初始化 CapeUserException 类的新实例。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    protected CapeUserException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Initialize();
    }

    /// <summary>初始化 CapeUserException 类的一个新实例，该实例带有指定的错误信息和导致该异常的内部异常地引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the same
    /// value that is passed into the constructor, or a null Terence if the InnerException
    /// property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    protected CapeUserException(string message, Exception inner)
        : base(message, inner)
    {
        MDescription = message;
        Initialize();
    }

    /// <summary>虚拟抽象函数，派生类继承该函数，用于初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Derived classes should implement this class and set the values of the HResult,
    /// interface name and exception name. <code>
    /// virtual void Initialize() override 
    /// {
    ///  HResult = (int)CapeErrorInterfaceHR.ECapeUnknownHR;
    ///  m_interfaceName = "ECapeUnknown";
    ///  m_name = "CUnknownException";
    /// }
    /// </code></remarks>
    protected abstract void Initialize();

    /// <summary>控制 COM 注册的函数。</summary>
    /// <remarks>This function adds the registration keys specified in the CAPE-OPEN Method and
    /// Tools specifications. In particular, it indicates that this unit operation implements
    /// the CAPE-OPEN Unit Operation Category Identification. It also adds the CapeDescription
    /// registry keys using the <see c ="CapeNameAttribute"/>, <see c ="CapeDescriptionAttribute"/>,
    /// <see c ="CapeVersionAttribute"/>, <see c ="CapeVendorURLAttribute"/>,
    /// <see c ="CapeHelpURLAttribute"/> and <see c ="CapeAboutAttribute"/> attributes.</remarks>
    /// <param name = "t">The type of the class being registered.</param> 
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [ComRegisterFunction]
    public static void RegisterFunction(Type t) { }

    /// <summary>该函数控制在卸载类时从 COM 注册表中删除该类。</summary>
    /// <remarks>The method will remove all sub-keys added to the class' registration,
    /// including the CAPE-OPEN specific keys added in the <see c ="RegisterFunction"/> method.</remarks>
    /// <param name = "t">The type of the class being unregistered.</param> 
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [ComUnregisterFunction]
    public static void UnregisterFunction(Type t) { }

    // ECapeRoot 方法，返回 System.ApplicationException 中的消息字符串。
    /// <summary>抛出的异常名称。</summary>
    /// <remarks>The name of the exception being thrown.</remarks>
    /// <value>The name of the exception being thrown.</value>
    public string Name => MName;

    /// <summary>指定错误子类别的代码。</summary>
    /// <remarks>The assignment of values is left to each implementation. So that is a 
    /// proprietary code specific to the CO component provider. By default, set to 
    /// the CAPE-OPEN error HRESULT <see c="CapeErrorInterfaceHR"/>.</remarks>
    /// <value>The HRESULT value for the exception.</value>
    public int code => HResult;

    /// <summary>错误描述。</summary>
    /// <remarks>The error description can include a more verbose description of the condition that
    /// caused the error.</remarks>
    /// <value>A string description of the exception.</value>
    public string description
    {
        get => MDescription;
        set => MDescription = value;
    }

    /// <summary>错误的范围。</summary>
    /// <remarks>This property provides a list of packages where the error occurred. 
    /// For example <see c = "ICapeIdentification"/>.</remarks>
    /// <value>The source of the error.</value>
    public string scope => Source;

    /// <summary>发生错误的接口名称。这是一个必填字段。</summary>
    /// <remarks>The interface that the error was thrown.</remarks>
    /// <value>The name of the interface.</value>
    public string interfaceName
    {
        get => MInterfaceName;
        set => MInterfaceName = value;
    }

    /// <summary>发生错误的操作名称。这是一个必填字段。</summary>
    /// <remarks>This field provides the name of the operation being performed when the exception was raised.</remarks>
    /// <value>The operation name.</value>
    public string operation => StackTrace;

    /// <summary>指向页面、文档或网站的 URL，可在其中找到有关该错误的更多信息。这些信息的内容显然取决于实施情况。</summary>
    /// <remarks>This field provides an internet URL where more information about the error can be found.</remarks>
    /// <value>The URL.</value>
    public string moreInfo => HelpLink;
}

/// <summary>当操作指定的其他错误不适合时，就会引发该异常。</summary>
/// <remarks>A standard exception that can be thrown by a CAPE-OPEN object to indicate that the error
/// that occurred was not one that was suitable for any of the other errors supported by the object.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeGuids.CapeUnKnExIid)] // "B550B2CA-6714-4e7f-813E-C93248142410"
[Description("")]
[ClassInterface(ClassInterfaceType.None)]
public class CapeUnknownException : CapeUserException, ECapeUnknown
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = unchecked((int)CapeErrorInterfaceHR.ECapeUnknownHR);
        MInterfaceName = "ECapeUnknown";
        MName = "CUnknownException";
    }
    
    /// <summary>初始化 CapeUnknownException 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system 
    /// culture.</remarks>
    public CapeUnknownException() { }

    /// <summary>用指定的错误信息初始化 CapeUnknownException 类的新实例。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeUnknownException(string message) : base(message) { }

    /// <summary>使用序列化数据初始化 CapeUnknownException 类的新实例。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeUnknownException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化一个新的 CapeUnknownException 类实例，该实例带有指定的错误信息和导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown 
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the InnerException
    /// property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeUnknownException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>当操作指定的其他错误不适合时，就会引发该异常。</summary>
/// <remarks>A standard exception that can be thrown by a CAPE-OPEN object to indicate that the error
/// that occurred was not one that was suitable for any of the other errors supported by the object.</remarks>
[Serializable, ComVisible(true)]
[Guid(CapeGuids.CapeUnPeExIid)] // "16049506-E086-4baf-9905-9ED13D50D0E3"
[Description("")]
[ClassInterface(ClassInterfaceType.None)]
public class CapeUnexpectedException : CapeUserException
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = unchecked((int)0x8000ffff);
        MInterfaceName = "IPersistStreamInit";
        MName = "CUnexpectedException";
    }

    /// <summary>初始化 CapeUnknownException 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeUnexpectedException() { }

    /// <summary>用指定的错误信息初始化 CapeUnknownException 类的新实例。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeUnexpectedException(string message) : base(message) { }

    /// <summary>使用序列化数据初始化 CapeUnknownException 类的新实例。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeUnexpectedException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeUnknownException 类实例，该实例带有指定的错误信息和导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the same
    /// value that is passed nto the constructor, or a null Terence if the InnerException
    /// property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeUnexpectedException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>与任何数据相关的错误层次结构的基类。</summary>
/// <remarks>The CapeDataException class is a base class for errors related to data. The data are the 
/// arguments of operations, the parameters coming from the Parameter Common Interface 
/// and information on licence key.</remarks>
[Serializable]
[Guid(CapeGuids.CapeDataExPtIid)] // "53551E7C-ECB2-4894-B71A-CCD1E7D40995"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeDataException : CapeUserException, ECapeData
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = unchecked((int)CapeErrorInterfaceHR.ECapeDataHR);
        MName = "CapeDataException";
    }

    /// <summary>初始化 CapeDataException 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeDataException() { }

    /// <summary>用指定的错误信息初始化 CapeDataException 类的新实例。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into
    /// account the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeDataException(string message) : base(message) { }

    /// <summary>使用序列化数据初始化 CapeDataException 类的新实例。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeDataException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化 CapeDataException 类的一个新实例，该实例带有指定的错误消息和导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the InnerException
    /// property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeDataException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>参数（参数公共接口的对象）的状态无效。</summary>
/// <remarks>The name of the invalid parameter, along with the parameter itself are available from the exception.</remarks>
[Serializable]
[Guid(CapeGuids.CapeBadCoParaIid)] // "667D34E9-7EF7-4ca8-8D17-C7577F2C5B62"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeBadCoParameter : CapeDataException, 
    ECapeBadCOParameter
{
    private string _mParameterName;
    private object _mParameter;

    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    /// <param name = "pParameterName">The name of the parameter with the invalid status.</param>
    /// <param name = "pParameter">The parameter with the invalid status.</param>
    protected void Initialize(string pParameterName, object pParameter)
    {
        _mParameterName = pParameterName;
        _mParameter = (ICapeParameter)(parameter);
        HResult = (int)CapeErrorInterfaceHR.ECapeBadArgumentHR;
        MInterfaceName = "ECapeBadArgument";
        MName = "CapeBadArgumentException";
    }

    /// <summary>用参数名称和导致异常的参数初始化 CapeBadCOParameter 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    /// <param name = "pParameterName">The name of the parameter with the invalid status.</param>
    /// <param name = "pParameter">The parameter with the invalid status.</param>
    public CapeBadCoParameter(string pParameterName, object pParameter)
    {
        Initialize(pParameterName, pParameter);
    }

    /// <summary>用指定的错误信息、参数名称和导致异常的参数初始化 CapeBadCOParameter 类的新实例。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into
    /// account the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "pParameterName">The name of the parameter with the invalid status.</param>
    /// <param name = "pParameter">The parameter with the invalid status.</param>
    public CapeBadCoParameter(string message, string pParameterName, object pParameter) : base(message)
    {
        Initialize(pParameterName, pParameter);
    }

    /// <summary>使用序列化数据、参数名称和导致异常的参数初始化 CapeBadCOParameter 类的新实例。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    /// <param name = "pParameterName">The name of the parameter with the invalid status.</param>
    /// <param name = "pParameter">The parameter with the invalid status.</param>
    public CapeBadCoParameter(SerializationInfo info, StreamingContext context, 
        string pParameterName, object pParameter) : base(info, context)
    {
        Initialize(pParameterName, pParameter);
    }

    /// <summary>初始化一个新的 CapeBadCOParameter 类实例，该实例带有指定的错误信息和内部异常、参数名称和导致异常的参数的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "pParameterName">The name of the parameter with the invalid status.</param>
    /// <param name = "pParameter">The parameter with the invalid status.</param>
    public CapeBadCoParameter(string message, Exception inner, string pParameterName, object pParameter) 
        : base(message, inner)
    {
        Initialize(pParameterName, pParameter);
    }

    /// <summary>产生异常的 CO 参数的名称。</summary>
    /// <remarks>This provides the name of the parameter that threw the exception.</remarks>
    /// <value>The name of the parameter that threw the exception.</value>
    public virtual object parameter => _mParameter;

    /// <summary>产生异常的 CO 参数的名称。</summary>
    /// <remarks>This provides access to the parameter that threw the exception.</remarks>
    /// <value>The parameter that threw the exception.</value>
    public virtual string parameterName => MName;
}

/// <summary>操作的参数值不正确。</summary>
/// <remarks>An argument value of the operation is not correct. The position of the 
/// argument value within the signature of the operation. First argument is as position 1.</remarks>
[Serializable]
[Guid(CapeGuids.CapeBadArExPtiIid)] // "D168E99F-C1EF-454c-8574-A8E26B62ADB1"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeBadArgumentException : CapeDataException,
    ECapeBadArgument, ECapeBadArgument093
{
    private int _mPosition;

    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    /// <param name = "pPosition">The pPosition of the argument value within the signature of the operation. First argument is as pPosition 1.</param>
    protected void Initialize(int pPosition)
    {
        HResult = ((int)CapeErrorInterfaceHR.ECapeBadArgumentHR);
        MInterfaceName = "ECapeBadArgument";
        MName = "CapeBadArgumentException";
        _mPosition = pPosition;
    }

    /// <summary>用错误位置初始化 CapeBadArgumentException 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    /// <param name = "pPosition">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeBadArgumentException(int pPosition)
    {
        Initialize(pPosition);
    }

    /// <summary>用指定的错误信息和错误位置初始化 CapeBadArgumentException 类的新实例。</summary>. 
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into
    /// account the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "pPosition">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeBadArgumentException(string message, int pPosition) : base(message)
    {
        Initialize(pPosition);
    }

    /// <summary>使用序列化数据和错误位置初始化 CapeBadArgumentException 类的新实例。 </summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    /// <param name = "pPosition">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeBadArgumentException(SerializationInfo info, StreamingContext context, 
        int pPosition) : base(info, context)
    {
        Initialize(pPosition);
    }

    /// <summary>初始化 CapeBadArgumentException 类的一个新实例，该实例带有指定的错误信息和引发该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the
    /// previous exception in the InnerException property. The InnerException property
    /// returns the same value that is passed into the constructor, or a null Terence
    /// if the InnerException property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeBadArgumentException(string message, Exception inner, 
        int position) : base(message, inner)
    {
        Initialize(position);
    }


    /// <summary>参数值在操作签名中的位置。第一个参数的位置为 1。</summary>
    /// <remarks>This provides the location of the invalid argument in the argument list for the function call.</remarks>
    /// <value>The position of the argument that is bad. The first argument is 1.</value>
    public virtual short position => (short)_mPosition;

    /// <summary>参数值在操作签名中的位置。第一个参数的位置为 1。</summary>
    /// <remarks>This provides the location of the invalid argument in the argument list for the function call.</remarks>
    /// <value>The position of the argument that is bad. The first argument is 1.</value>
    int ECapeBadArgument093.position => _mPosition;
}

/// <summary>这是一个抽象类，允许派生类提供有关超出其范围值导致的错误的信息。它可以被抛出，以指示方法参数或对象参数值超出范围。</summary>
/// <remarks>CapeBoundariesException is a "utility" class which factorises a state which
/// describes the value, its type and its boundaries. This is an abstract class. No real
/// error can be raised from this class.</remarks>
[Serializable]
[Guid(CapeGuids.CapeBoDaExPtIid)] // "62B1EE2F-E488-4679-AFA3-D490694D6B33"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public abstract class CapeBoundariesException : CapeUserException, ECapeBoundaries
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    /// <param name = "pLowerBound">The pValue of the lower bound.</param>
    /// <param name = "pUpperBound">The pValue of the upper bound.</param>
    /// <param name = "pValue">The current pValue which has led to an error.</param>
    /// <param name = "pType">The pType/nature of the pValue. The pValue could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    protected void SetBoundaries(double pLowerBound, double pUpperBound, double pValue, string pType)
    {
        lowerBound = pLowerBound;
        upperBound = pUpperBound;
        value = pValue;
        type = pType;
    }

    /// <summary>初始化一个新的 CapeBoundariesException 类实例，其中包含导致该异常的参数的下限、上限、值、类型和位置。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    protected CapeBoundariesException(double pLowerBound, double pUpperBound, double pValue, string pType)
    {
        SetBoundaries(pLowerBound, pUpperBound, pValue, pType);
    }

    /// <summary>初始化 CapeBoundariesException 类的一个新实例，该实例包含指定的错误信息、导致该异常的参数的下限、上限、值、类型和位置。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into
    /// account the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    protected CapeBoundariesException(string message, 
        double pLowerBound, double pUpperBound, double pValue, string pType) : base(message)
    {
        SetBoundaries(pLowerBound, pUpperBound, pValue, pType);
    }

    /// <summary>初始化一个新的 CapeBoundariesException 类实例，其中包含序列化数据、下限值、上限值、值、类型和导致此异常的参数的位置。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    protected CapeBoundariesException(SerializationInfo info, StreamingContext context, 
        double pLowerBound, double pUpperBound, double pValue, string pType) : base(info, context)
    {
        SetBoundaries(pLowerBound, pUpperBound, pValue, pType);
    }

    /// <summary>初始化 CapeBoundariesException 类的一个新实例，该实例包含指定的错误消息、参数的下限、上限、值、类型和位置，以及导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns
    /// the same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    protected CapeBoundariesException(string message, Exception inner, 
        double pLowerBound, double pUpperBound, double pValue, string pType) : base(message, inner)
    {
        SetBoundaries(pLowerBound, pUpperBound, pValue, pType);
    }

    /// <summary>下限值。</summary>
    /// <remarks>This provides the user with the acceptable lower bounds of the argument.</remarks>
    /// <value>The lower bound for the argument.</value>
    public double lowerBound { get; private set; }

    /// <summary>上限值。</summary>
    /// <remarks>This provides the user with the acceptable upper bounds of the argument.</remarks>
    /// <value>The upper bound for the argument.</value>
    public double upperBound { get; private set; }

    /// <summary>导致错误的当前值。</summary>
    /// <remarks>This provides the user with the value that caused the error condition.</remarks>
    /// <value>The value that resulted in the error condition.</value>
    public double value { get; private set; }

    /// <summary>值的类型/性质。</summary>
    /// <remarks>The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</remarks>
    /// <value>A string that indicates the nature or type of the value required.</value>
    public string type { get; private set; }
}

/// <summary>一个参数超出界限。</summary>
/// <remarks>This class is derived from the <see c="CapeBoundariesException">CapeBoundariesException</see>
/// class. It is used to indicate that one of the parameters is outside its bounds.</remarks>
[Serializable]
[Guid(CapeGuids.CapeOutOfBoExPtIid)] // "4438458A-1659-48c2-9138-03AD8B4C38D8"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeOutOfBoundsException : CapeBoundariesException,
    ECapeOutOfBounds, ECapeBadArgument, ECapeBadArgument093, ECapeData
{
    private int _mPosition;

    /// <summary>从 CapeOutOfBoundsException 派生的所有类的初始化方法都需要包含与边界相关的信息。</summary>
    /// <remarks>This method is sealed so that classes that derive from CapeOutOfBoundsException include the required information about the position of the argument.</remarks>
    protected override void Initialize()
    {
        HResult = ((int)CapeErrorInterfaceHR.ECapeOutOfBoundsHR);
        MInterfaceName = "ECapeOutOfBounds";
        MName = "CapeOutOfBoundsException";
    }

    /// <summary>用错误位置初始化 CapeOutOfBoundsException 类的新实例。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    public CapeOutOfBoundsException(int position, 
        double pLowerBound, double pUpperBound, double pValue, string pType) :
        base(pLowerBound, pUpperBound, pValue, pType)
    {
        _mPosition = position;
    }

    /// <summary>用指定的错误信息和错误位置初始化 CapeOutOfBoundsException 类的新实例。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into
    /// account the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    public CapeOutOfBoundsException(string message, int position, 
        double pLowerBound, double pUpperBound, double pValue, string pType) :
        base(message, pLowerBound, pUpperBound, pValue, pType)
    {
        _mPosition = position;
    }

    /// <summary>使用序列化数据和错误位置初始化 CapeOutOfBoundsException 类的新实例。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination.</param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    public CapeOutOfBoundsException(SerializationInfo info, StreamingContext context, 
        int position, double pLowerBound, double pUpperBound, double pValue, string pType) :
        base(info, context, pLowerBound, pUpperBound, pValue, pType)
    {
        _mPosition = position;
    }

    /// <summary>初始化 CapeOutOfBoundsException 类的一个新实例，该实例带有指定的错误消息和导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the
    /// previous exception in the InnerException property. The InnerException property
    /// returns the same value that is passed into the constructor, or a null Terence if
    /// the InnerException property does not supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    /// <param name = "pLowerBound">The value of the lower bound.</param>
    /// <param name = "pUpperBound">The value of the upper bound.</param>
    /// <param name = "pValue">The current value which has led to an error.</param>
    /// <param name = "pType">The type/nature of the value. The value could represent a thermodynamic property, a number of tables in a database, a quantity of memory, ...</param>
    public CapeOutOfBoundsException(string message, Exception inner, int position, 
        double pLowerBound, double pUpperBound, double pValue, string pType) :
        base(message, inner, pLowerBound, pUpperBound, pValue, pType)
    {
        _mPosition = position;
    }

    /// <summary>参数值在操作签名中的位置。第一个参数的位置为 1。</summary>
    /// <remarks>This provides the location of the invalid argument in the argument list for the function call.</remarks>
    /// <value>The position of the argument that is bad. The first argument is 1.</value>
    public short position => (short)_mPosition;

    /// <summary>参数值在操作签名中的位置。第一个参数的位置为 1。</summary>
    /// <remarks>This provides the location of the invalid argument in the argument list for the function call.</remarks>
    /// <value>The position of the argument that is bad. The first argument is 1.</value>
    int ECapeBadArgument093.position => _mPosition;
}

/// <summary>与计算相关的误差等级的基类。</summary>
/// <remarks>This class is used to indicate that an error occurred in the performance of a calculation. 
/// Other calculation-related classes such as 
/// <see c = "CapeFailedInitialisationException">CapeOpen.CapeFailedInitialisationException</see>, 
/// <see c = "CapeOutOfResourcesException">CapeOpen.CapeOutOfResourcesException</see>, 
/// <see c = "CapeSolvingErrorException">CapeOpen.CapeSolvingErrorException</see>, 
/// <see c = "CapeBadInvOrderException">CapeOpen.CapeBadInvOrderException</see>, 
/// <see c = "CapeInvalidOperationException">CapeOpen.CapeInvalidOperationException</see>, 
/// <see c = "CapeNoMemoryException">CapeOpen.CapeNoMemoryException</see>, and 
/// <see c = "CapeTimeOutException">CapeOpen.CapeTimeOutException</see> 
/// derive from this class.</remarks>
[Serializable]
[Guid(CapeGuids.CapeComPuExPtIid)]  // "9D416BF5-B9E3-429a-B13A-222EE85A92A7"
[ComVisible(true), ClassInterface(ClassInterfaceType.None)]
public class CapeComputationException : CapeUserException, ECapeComputation
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeComputationHR;
        MInterfaceName = "ECapeComputation";
        MName = "CapeComputationException";
    }

    /// <summary>初始化一个新的 CapeComputationException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeComputationException() { }

    /// <summary>初始化一个新的 CapeComputationException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into
    /// account the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeComputationException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeComputationException 类，使用序列化数据。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeComputationException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeComputationException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeComputationException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>当未执行必要地初始化或初始化失败时，会抛出此异常。</summary>
/// <remarks>The pre-requisites operations are not valid. The necessary initialisation has not been performed or has failed.</remarks>
[Serializable]
[Guid(CapeGuids.CapeFaInLiExPtIid)]  // "E407595C-6D1C-4b8c-A29D-DB0BE73EFDDA"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeFailedInitialisationException : CapeComputationException, ECapeFailedInitialisation
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeFailedInitialisationHR;
        MInterfaceName = "ECapeFailedInitialisation";
        MName = "CapeFailedInitialisationException";
    }

    /// <summary>初始化一个新的 CapeFailedInitialisationException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeFailedInitialisationException() { }

    /// <summary>初始化一个新的 CapeFailedInitialisationException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeFailedInitialisationException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeFailedInitialisationException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeFailedInitialisationException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

    /// <summary>初始化一个新的 CapeFailedInitialisationException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeFailedInitialisationException(string message, Exception inner) 
        : base(message, inner) { }
}

/// <summary>与当前实现相关的错误层次结构的基类。</summary>
/// <remarks>This class is used to indicate that an error occurred in the with the implementation
/// of an object. The implementation-related classes such as 
/// <see c = "CapeNoImplException ">CapeOpen.CapeNoImplException </see> and 
/// <see c = "CapeLimitedImplException ">CapeOpen.CapeLimitedImplException </see>
/// derive from this class.</remarks>
[Serializable]
[Guid(CapeGuids.CapeImTaExPtIid)]  // "7828A87E-582D-4947-9E8F-4F56725B6D75"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeImplementationException : CapeUserException, ECapeImplementation
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeImplementationHR;
        MInterfaceName = "ECapeImplementation";
        MName = "CapeImplementationException";
    }

    /// <summary>初始化一个新的 CapeImplementationException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeImplementationException() { }

    /// <summary>初始化一个新的 CapeImplementationException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeImplementationException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeImplementationException 类，使用序列化数据。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeImplementationException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化一个新的 CapeImplementationException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeImplementationException(string message, Exception inner) 
        : base(message, inner) { }
}

/// <summary>传递的参数值无效。例如，传递的阶段名称不属于 CO 阶段列表。</summary>
/// <remarks>An argument value of the operation is invalid. The position of the 
/// argument value within the signature of the operation. First argument is as position 1.</remarks>
[Serializable]
[Guid(CapeGuids.CapeInValArExPtIid)] // "B30127DA-8E69-4d15-BAB0-89132126BAC9"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeInvalidArgumentException : CapeBadArgumentException, ECapeInvalidArgument
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeInvalidArgumentHR;
        MInterfaceName = "ECapeInvalidArgument";
        MName = "CapeInvalidArgumentException";
    }

    /// <summary>初始化一个新的 CapeInvalidArgumentException class with the position of the error.</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeInvalidArgumentException(int position) : base(position) { }

    /// <summary>初始化一个新的 CapeInvalidArgumentException class with a specified error message and the position of the error. </summary>. 
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeInvalidArgumentException(string message, int position) : base(message, position) { }

    /// <summary>初始化一个新的 CapeInvalidArgumentException 类，使用序列化数据和错误位置。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeInvalidArgumentException(SerializationInfo info,
        StreamingContext context, int position) : base(info, context, position) { }

    /// <summary>初始化一个新的 CapeInvalidArgumentException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "position">The position of the argument value within the signature of the operation. First argument is as position 1.</param>
    public CapeInvalidArgumentException(string message, Exception inner, int position) 
        : base(message, inner, position) { }
}

/// <summary>该操作在当前情况下无效。</summary>
/// <remarks>This exception is thrown when an operation is attempted that is not valid in the current context.</remarks>
[Serializable]
[Guid(CapeGuids.CapeInValOpTiExPtIid)]  // "C0B943FE-FB8F-46b6-A622-54D30027D18B"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeInvalidOperationException : CapeComputationException, ECapeInvalidOperation
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeInvalidOperationHR;
        MInterfaceName = "ECapeInvalidOperation";
        MName = "CapeInvalidOperationException";
    }

    /// <summary>初始化一个新的 CapeInvalidOperationException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeInvalidOperationException() { }

    /// <summary>初始化一个新的 CapeInvalidOperationException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeInvalidOperationException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeInvalidOperationException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeInvalidOperationException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeInvalidOperationException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeInvalidOperationException(string message, Exception inner) 
        : base(message, inner) { }
}

/// <summary>在操作请求之前，尚未调用必要的先决操作。</summary>
/// <remarks>The specified prerequisite operation must be called prior to the operation throwing this exception.</remarks>
[Serializable]
[Guid(CapeGuids.CapeBadInvOrExPtIid)]  // "07EAD8B4-4130-4ca6-94C1-E8EC4E9B23CB"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeBadInvOrderException : CapeComputationException, ECapeBadInvOrder
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeBadInvOrderHR;
        MInterfaceName = "ECapeBadInvOrder";
        MName = "CapeBadInvOrderException";
    }
    
    /// <summary>初始化一个新的 CapeBadInvOrderException 类，使用指定的错误信息和引发异常的操作名称。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "operation">The necessary prerequisite operation.</param>
    public CapeBadInvOrderException(string operation)
    {
        requestedOperation = operation;
    }

    /// <summary>初始化一个新的 CapeBadInvOrderException 类，使用指定的错误信息和引发异常的操作名称。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "operation">The necessary prerequisite operation.</param>
    public CapeBadInvOrderException(string message, string operation)
        : base(message)
    {
        requestedOperation = operation;
    }

    /// <summary>初始化一个新的 CapeBadInvOrderException 类，使用序列化数据和引发异常的操作名称。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    /// <param name = "operation">The necessary prerequisite operation.</param>
    public CapeBadInvOrderException(SerializationInfo info, StreamingContext context, string operation)
        : base(info, context)
    {
        requestedOperation = operation;
    }

    /// <summary>初始化一个新的 CapeBadInvOrderException 类并附带指定的错误消息、内部异常的特伦斯（Terence）以及引发该异常的操作名称。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "operation">The necessary prerequisite operation.</param>
    public CapeBadInvOrderException(string message, Exception inner, string operation)
        : base(message, inner)
    {
        requestedOperation = operation;
    }

    /// <summary>必要的前提操作。</summary>
    /// <remarks>The prerquisite operation must be called prior to calling the current operation.</remarks>
    /// <value>The name of the necessary prerequisite operation.</value>
    public string requestedOperation { get; }
}

/// <summary>由于未遵守许可协议，操作无法完成。</summary>
/// <remarks>Of course, this type of error could also appear outside the CO scope. In this case, 
/// the error does not belong to the CO error handling. It is specific to the platform.</remarks>
[Serializable]
[Guid(CapeGuids.CapeLiCeErExPtIid)] // "CF4C55E9-6B0A-4248-9A33-B8134EA393F6"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeLicenceErrorException : CapeDataException, ECapeLicenceError
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeLicenceErrorHR;
        MInterfaceName = "ECapeLicenceError";
        MName = "CapeLicenceErrorException";
    }

    /// <summary>初始化一个新的 CapeLicenceErrorException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeLicenceErrorException() { }

    /// <summary>初始化一个新的 CapeLicenceErrorException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeLicenceErrorException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeLicenceErrorException 类，使用序列化数据。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeLicenceErrorException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeLicenceErrorException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeLicenceErrorException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>已违反执行限制。</summary>
/// <remarks>An operation may be partially implemented for example a Property Package could 
/// implement TP flash but not PH flash. If a caller requests for a PH flash, then 
/// this error indicates that some flash calculations are supported but not the 
/// requested one. The factory can only create one instance (because the component is an 
/// evaluation copy), when the caller requests for a second creation this error shows 
/// that this implementation is limited.</remarks>
[Serializable]
[Guid(CapeGuids.CapeLimitedImplExIid)] // "5E6B74A2-D603-4e90-A92F-608E3F1CD39D"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeLimitedImplException : CapeImplementationException, ECapeLimitedImpl
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeLimitedImplHR;
        MInterfaceName = "ECapeLimitedImpl";
        MName = "CapeLimitedImplException";
    }

    /// <summary>初始化一个新的 CapeLimitedImplException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeLimitedImplException() { }

    /// <summary>初始化一个新的 CapeLimitedImplException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeLimitedImplException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeLimitedImplException 类，使用序列化数据。</summary>
    /// <remarks>This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeLimitedImplException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeLimitedImplException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeLimitedImplException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>表示当前对象未执行所请求操作的异常类。</summary>
/// <remarks>The operation is “not” implemented even if this operation can be called due 
/// to the compatibility with the CO standard. That is to say that the operation 
/// exists, but it is not supported by the current implementation.</remarks>
[Serializable]
[Guid(CapeGuids.CapeNoImplExPtIid)]  // "1D2488A6-C428-4e38-AFA6-04F2107172DA"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeNoImplException : CapeImplementationException, ECapeNoImpl
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeNoImplHR;
        MInterfaceName = "ECapeNoImpl";
        MName = "CapeNoImplException";
    }

    /// <summary>初始化一个新的 CapeNoImplException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeNoImplException() { }

    /// <summary>初始化一个新的 CapeNoImplException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeNoImplException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeNoImplException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeNoImplException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeNoImplException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeNoImplException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>表示该操作所需的资源不可用的异常类。</summary>
/// <remarks>The physical resources necessary to the execution of the operation are out of limits.</remarks>
[Serializable]
[Guid(CapeGuids.CapeOutOfReCeExPtIid)]  // "42B785A7-2EDD-4808-AC43-9E6E96373616"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeOutOfResourcesException : CapeUserException, ECapeOutOfResources, ECapeComputation
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeOutOfResourcesHR;
        MInterfaceName = "ECapeOutOfResources";
        MName = "CapeOutOfResourcesException";
    }

    /// <summary>初始化一个新的 CapeOutOfResourcesException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeOutOfResourcesException() { }

    /// <summary>初始化一个新的 CapeOutOfResourcesException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeOutOfResourcesException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeOutOfResourcesException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeOutOfResourcesException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

    /// <summary>初始化一个新的 CapeOutOfResourcesException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeOutOfResourcesException(string message, Exception inner)
        : base(message, inner) { }
}

/// <summary>表示该操作所需的内存不可用的异常类。</summary>
/// <remarks>he physical memory necessary to the execution of the operation is out of limit.</remarks>
[Serializable]
[Guid(CapeGuids.CapeNoMeMoExPtIid)]  // "1056A260-A996-4a1e-8BAE-9476D643282B"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeNoMemoryException : CapeUserException, ECapeNoMemory
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeNoMemoryHR;
        MInterfaceName = "ECapeNoMemory";
        MName = "CapeNoMemoryException";
    }

    /// <summary>初始化一个新的 CapeNoMemoryException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeNoMemoryException() { }

    /// <summary>初始化一个新的 CapeNoMemoryException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeNoMemoryException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeNoMemoryException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeNoMemoryException(SerializationInfo info, StreamingContext context) { }

    /// <summary>初始化一个新的 CapeNoMemoryException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeNoMemoryException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>异常类，表示发生了与持久性相关的错误。</summary>
/// <remarks>The base class of the errors hierarchy related to the persistence.</remarks>
[Serializable]
[Guid(CapeGuids.CapePerSiTeExPtIid)]  // "3237C6F8-3D46-47ee-B25F-52485A5253D8"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapePersistenceException : CapeUserException, ECapePersistence
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapePersistenceHR;
        MInterfaceName = "ECapePersistence";
        MName = "CapePersistenceException";
    }

    /// <summary>初始化一个新的 CapePersistenceException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapePersistenceException() { }

    /// <summary>初始化一个新的 CapePersistenceException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapePersistenceException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapePersistenceException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapePersistenceException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapePersistenceException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapePersistenceException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>表示未找到持久性的异常类。</summary>
/// <remarks>The requested object, table, or something else within the persistence system was not found.</remarks>
[Serializable]
[Guid(CapeGuids.CapePeSiTeNotFoExPtIid)]  // "271B9E29-637E-4eb0-9588-8A53203A3959"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapePersistenceNotFoundException : CapePersistenceException, ECapePersistenceNotFound
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    /// <param name = "pItemName">Name of the item that was not found that is the cause of this exception. </param>
    protected void Initialize(string pItemName)
    {
        itemName = pItemName;
        HResult = (int)CapeErrorInterfaceHR.ECapePersistenceNotFoundHR;
        MInterfaceName = "ECapePersistenceNotFound";
        MName = "CapePersistenceNotFoundException";
    }

    /// <summary>初始化一个新的 CapePersistenceNotFoundException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    /// <param name = "itemName">Name of the item that was not found that is the cause of this exception. </param>
    public CapePersistenceNotFoundException(string itemName)
    {
        Initialize(itemName);
    }

    /// <summary>初始化一个新的 CapePersistenceNotFoundException 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    /// <param name = "itemName">Name of the item that was not found that is the cause of this exception. </param>
    public CapePersistenceNotFoundException(string message, string itemName) : base(message)
    {
        Initialize(itemName);
    }

    /// <summary>初始化一个新的 CapePersistenceNotFoundException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    /// <param name = "itemName">Name of the item that was not found that is the cause of this exception.</param>
    public CapePersistenceNotFoundException(SerializationInfo info, StreamingContext context, 
        string itemName) : base(info, context)
    {
        Initialize(itemName);
    }

    /// <summary>初始化一个新的 CapePersistenceNotFoundException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    /// <param name = "itemName">Name of the item that was not found that is the cause of this exception.</param>
    public CapePersistenceNotFoundException(string message, Exception inner, string itemName) 
        : base(message, inner)
    {
        Initialize(itemName);
    }

    /// <summary>导致异常的未找到项目的名称。</summary>
    /// <remarks>ame of the item that was not found that is the cause of this exception.</remarks>
    /// <value>Name of the item that was not found that is the cause of this exception.</value>
    public string itemName { get; private set; }
}

/// <summary>表示内部持久性系统溢出的异常类。</summary>
/// <remarks>During the persistence process, an overflow of internal persistence system occurred.</remarks>
[Serializable]
[Guid(CapeGuids.CapePeSiOverFlExcPtIid)]  // "A119DE0B-C11E-4a14-BA5E-9A2D20B15578"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapePersistenceOverflowException : CapeUserException,
    ECapePersistenceOverflow, ECapePersistence
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapePersistenceOverflowHR;
        MInterfaceName = "ECapePersistenceOverflow";
        MName = "CapePersistenceOverflowException";
    }

    /// <summary>初始化一个新的 CapePersistenceOverflowException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapePersistenceOverflowException() { }

    /// <summary>初始化一个新的 CapePersistenceOverflowException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapePersistenceOverflowException(string message) { }

    /// <summary>初始化一个新的 CapePersistenceOverflowException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapePersistenceOverflowException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化一个新的 CapePersistenceOverflowException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapePersistenceOverflowException(string message, Exception inner) 
        : base(message, inner) { }
}

/// <summary>表示持久性系统发生严重错误的异常类。</summary>
/// <remarks>During the persistence process, a severe error occurred within the persistence system.</remarks>
[Serializable]
[Guid(CapeGuids.CapePeSiSysErExIid)]  // "85CB2D40-48F6-4c33-BF0C-79CB00684440"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapePersistenceSystemErrorException : CapePersistenceException,
    ECapePersistenceSystemError
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapePersistenceSystemErrorHR;
        MInterfaceName = "ECapePersistenceSystemError";
        MName = "CapePersistenceSystemErrorException";
    }

    /// <summary>初始化一个新的 CapePersistenceSystemErrorException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapePersistenceSystemErrorException() { }

    /// <summary>初始化一个新的 CapePersistenceSystemErrorException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapePersistenceSystemErrorException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapePersistenceSystemErrorException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapePersistenceSystemErrorException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化一个新的 CapePersistenceSystemErrorException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapePersistenceSystemErrorException(string message, Exception inner) 
        : base(message, inner) { }
}

/// <summary>对持久性系统内某些内容的访问未获授权。</summary>
/// <remarks>This exception is thrown when the access to something within the persistence system is not authorised.</remarks>
[Serializable]
[Guid(CapeGuids.CapeIllAccEssExIid)]  // "45843244-ECC9-495d-ADC3-BF9980A083EB"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeIllegalAccessException : CapePersistenceException, ECapeIllegalAccess
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeIllegalAccessHR;
        MInterfaceName = "ECapeIllegalAccess";
        MName = "CapeIllegalAccessException";
    }

    /// <summary>初始化一个新的 CapeIllegalAccessException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeIllegalAccessException() { }

    /// <summary>初始化一个新的 CapeIllegalAccessException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeIllegalAccessException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeIllegalAccessException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeIllegalAccessException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化一个新的 CapeIllegalAccessException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeIllegalAccessException(string message, Exception inner) 
        : base(message, inner) { }
}

/// <summary>表示数值算法因故失败的异常类。</summary>
/// <remarks>Indicates that a numerical algorithm failed for any reason.</remarks>
[Serializable]
[Guid(CapeGuids.CapeSoLvErrExIid)]  // "F617AFEA-0EEE-4395-8C82-288BF8F2A136"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeSolvingErrorException : CapeComputationException, ECapeSolvingError
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeSolvingErrorHR;
        MInterfaceName = "ECapeSolvingError";
        MName = "CapeSolvingErrorException";
    }

    /// <summary>初始化一个新的 CapeSolvingErrorException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeSolvingErrorException() { }

    /// <summary>初始化一个新的 CapeSolvingErrorException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeSolvingErrorException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeSolvingErrorException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeSolvingErrorException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

    /// <summary>初始化一个新的 CapeSolvingErrorException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeSolvingErrorException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>当 MINLP 算法的 Hessian 矩阵不可用时抛出的异常。</summary>
/// <remarks>Exception thrown when the Hessian for the MINLP problem is not available.</remarks>
[Serializable]
[Guid(CapeGuids.CapeHeInoNotAvBlExIid)]  // "3044EA08-F054-4315-B67B-4E3CD2CF0B1E"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeHessianInfoNotAvailableException : CapeSolvingErrorException,
    ECapeHessianInfoNotAvailable
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeHessianInfoNotAvailableHR;
        MInterfaceName = "ECapeHessianInfoNotAvailable";
        MName = "CapeHessianInfoNotAvailableHR";
    }

    /// <summary>初始化一个新的 CapeHessianInfoNotAvailableException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeHessianInfoNotAvailableException() { }

    /// <summary>初始化一个新的 CapeHessianInfoNotAvailableException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeHessianInfoNotAvailableException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeHessianInfoNotAvailableException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeHessianInfoNotAvailableException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeHessianInfoNotAvailableException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeHessianInfoNotAvailableException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>达到超时标准时抛出的异常。</summary>
/// <remarks>Exception thrown when the time-out criterion is reached.</remarks>
[Serializable]
[Guid(CapeGuids.CapeTimeOutExPtIid)]  // "0D5CA7D8-6574-4c7b-9B5F-320AA8375A3C"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeTimeOutException : CapeUserException, ECapeTimeOut
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeTimeOutHR;
        MInterfaceName = "ECapeTimeOut";
        MName = "CapeTimeOutException";
    }

    /// <summary>初始化一个新的 CapeTimeOutException 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeTimeOutException() { }

    /// <summary>初始化一个新的 CapeTimeOutException class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeTimeOutException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeTimeOutException 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeTimeOutException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeTimeOutException 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeTimeOutException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>基于 COM 的异常的封装类。</summary>
/// <remarks>This class can be used when a COM-based CAPE-OPEN component returns a failure HRESULT.
/// A failure HRESULT indicates an error condition has occurred. This class is used by the
/// <see c = "COMExceptionHandler"/> to rethrow the COM-based error condition as a .Net-based exception.
/// The CAPE-OPEN error handling process chose not to use the COM IErrorInfo API due to
/// limitation of the Visual Basic programming language at the time that the error
/// handling protocols were developed. Instead, the CAPE-OPEN error handling protocol 
/// requires that component in which the error occurs expose the appropriate error
/// interfaces. In practice, this typically means that all CAPE-OPEN objects
/// implement the <see c="ECapeRoot">ECapeRoot</see>, <see c="ECapeUser">ECapeUser</see>, 
/// and sometimes the <see c="ECapeUnknown">ECapeUnknown</see> error interfaces.
/// This class wraps the CAPE-OPEN object that threw the exception and creates the 
/// appropriate .Net exception so users can use the .Net exception handling protocols.</remarks>
/// <see c="COMExceptionHandler">COMExceptionHandler</see> 
[Serializable]
[Guid(CapeGuids.ComCaOpExPtWrPerIid)]  // "31CD55DE-AEFD-44ff-8BAB-F6252DD43F16"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class COMCapeOpenExceptionWrapper : CapeUserException
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize() { }

    /// <summary>Creates a new instance of the COMCapeOpenExceptionWrapper class.</summary>
    /// <remarks>Creates a .Net based exception wrapper for COM-based CAPE-OPEN components to 
    /// enable users to utilize .Net structured exception handling.</remarks>
    /// <param name = "message">The error message text from the COM-based component.</param>
    /// <param name = "exceptionObject">The CAPE-OPEN object that raised the error.</param>
    /// <param name = "hresult">The COM HResult value.</param>
    /// <param name = "inner">An inner .Net-based exception obtained from the IErrorInfo
    /// object, if implemented or an accompanying .Net exception.</param>
    public COMCapeOpenExceptionWrapper(string message, object exceptionObject, int hresult, Exception inner)
        : base(message, inner)
    {
        HResult = hresult;
        if (exceptionObject is ECapeRoot pRoot)
        {
            MName = string.Concat("CAPE-OPEN Error: ", pRoot.Name);
        }

        if (exceptionObject is not ECapeUser pUser) return;
        MDescription = pUser.description;
        MInterfaceName = pUser.interfaceName;
        Source = pUser.scope;
        HelpLink = pUser.moreInfo;
    }
}

/// <summary>当所请求的热力学属性不可用时抛出的异常。</summary>
/// <remarks>Exception thrown when a requested thermodynamic property is not available.</remarks>
[Serializable]
[Guid(CapeGuids.CapeThProPeNotAvBlExIid)]  // "5BA36B8F-6187-4e5e-B263-0924365C9A81"
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
public class CapeThrmPropertyNotAvailableException : CapeUserException,
    ECapeThrmPropertyNotAvailable
{
    /// <summary>初始化该异常的描述、接口名称和名称字段。</summary>
    /// <remarks>Sets the values of the HResult, interface name and exception name.</remarks>
    protected override void Initialize()
    {
        HResult = (int)CapeErrorInterfaceHR.ECapeThrmPropertyNotAvailableHR;
        MInterfaceName = "ECapePersistence";
        MName = "CapeThrmPropertyNotAvailable";
    }

    /// <summary>初始化一个新的 CapeThrmPropertyNotAvailable 类。</summary>
    /// <remarks>This constructor initializes the Message property of the new instance to a 
    /// system-supplied message that describes the error, such as "An application 
    /// error has occurred." This message takes into account the current system culture.</remarks>
    public CapeThrmPropertyNotAvailableException() { }

    /// <summary>初始化一个新的 CapeThrmPropertyNotAvailable class 类，使用指定的错误信息。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. This message takes into account
    /// the current system culture.</remarks>
    /// <param name = "message">A message that describes the error.</param>
    public CapeThrmPropertyNotAvailableException(string message) : base(message) { }

    /// <summary>初始化一个新的 CapeThrmPropertyNotAvailable 类，使用序列化数据。</summary>
    /// <remarks> This constructor is called during deserialization to reconstitute the 
    /// exception object transmitted over a stream. For more information, see XML and 
    /// SOAP Serialization.</remarks>
    /// <param name = "info">The object that holds the serialized object data.</param>
    /// <param name = "context">The contextual information about the source or destination. </param>
    public CapeThrmPropertyNotAvailableException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }

    /// <summary>初始化一个新的 CapeThrmPropertyNotAvailable 类，并带有指定的错误信息和对导致该异常的内部异常的引用。</summary>
    /// <remarks>The content of the message parameter is intended to be understood by 
    /// humans. The caller of this constructor is required to ensure that this string 
    /// has been localized for the current system culture. An exception that is thrown
    /// as a direct result of a previous exception should include a Terence to the previous
    /// exception in the InnerException property. The InnerException property returns the
    /// same value that is passed into the constructor, or a null Terence if the
    /// InnerException property does ot supply the inner exception value to the constructor.</remarks>
    /// <param name = "message">The error message string.</param>
    /// <param name = "inner">The inner exception Terence.</param>
    public CapeThrmPropertyNotAvailableException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>一个辅助类，用于处理来自基于 COM 的 CAPE-OPEN 组件的异常。</summary>
/// <remarks>This class can be used when a COM-based CAPE-OPEN component returns a failure HRESULT.
/// A failure HRESULT indicates an error condition has occurred. The
/// <see c="ExceptionForHRESULT">ExceptionForHRESULT</see> formats the .Net-based exception object
/// and the COM-based CAPE-OPEN component to rethrow the COM-based error condition as a .Net-based
/// exception using the <see c = "COMCapeOpenExceptionWrapper">COMCapeOpenExceptionWrapper</see> 
/// wrapper class. The CAPE-OPEN error handling process chose not to use the COM IErrorInfo API due to
/// limitation of the Visual Basic programming language at the time that the error
/// handling protocols were developed. Instead, the CAPE-OPEN error handling protocol 
/// requires that component in which the error occurs expose the appropriate error
/// interfaces. In practice, this typically means that all CAPE-OPEN objects
/// implement the <see c = "ECapeRoot">ECapeRoot</see>, <see c = "ECapeUser">ECapeUser</see>, 
/// and sometimes the <see c = "ECapeUnknown">ECapeUnknown</see> error interfaces.</remarks>
/// <see c = "COMCapeOpenExceptionWrapper">COMCapeOpenExceptionWrapper</see> 
[ComVisible(false)]
public class COMExceptionHandler
{
    /// <summary>创建并返回 COMCapeOpenExceptionWrapper 类的新实例。</summary>
    /// <remarks>Creates a .Net-based exception wrapper for COM-based CAPE-OPEN components to 
    /// enable users to utilize .Net structured exception handling. This method formats 
    /// the .Net-based exception object and the COM-based CAPE-OPEN component to rethrow 
    /// the COM-based error condition as a .Net-based exception using the 
    /// <see c = "COMCapeOpenExceptionWrapper">COMCapeOpenExceptionWrapper</see> wrapper class.</remarks>
    /// <returns>The COM-based object that returned the error HRESULT wrapper as the appropriate .Net-based exception.</returns>
    /// <param name = "exceptionObject">The CAPE-OPEN object that raised the error.</param>
    /// <param name = "inner">An inner .Net-based exception obtained from the IErrorInfo
    /// object, if implemented or an accompanying .Net exception.</param>
    /// <see c = "COMCapeOpenExceptionWrapper">COMCapeOpenExceptionWrapper</see> 
    public static Exception ExceptionForHRESULT(object exceptionObject, Exception inner)
    {
        var hresult = ((COMException)inner).ErrorCode;
        const string message = "Exception thrown by COM PMC.";
        var exception =
            new COMCapeOpenExceptionWrapper(message, exceptionObject, hresult, inner);
        switch (hresult)
        {
            case unchecked((int)0x80040501): //ECapeUnknownHR
                return new CapeUnknownException(message, exception);
            case unchecked((int)0x80040502): //ECapeDataHR
                return new CapeDataException(message, exception);
            case unchecked((int)0x80040503): //ECapeLicenceErrorHR = 0x80040503 ,
                return new CapeLicenceErrorException(message, exception);
            case unchecked((int)0x80040504): //ECapeBadCOParameterHR = 0x80040504 ,
                return new CapeBadCoParameter(message, exception);
            case unchecked((int)0x80040505): //ECapeBadArgumentHR = 0x80040505 ,
                return new CapeBadArgumentException(message, exception,
                    ((ECapeBadArgument)exception).position);
            case unchecked((int)0x80040506): //ECapeInvalidArgumentHR = 0x80040506 ,
                return new CapeInvalidArgumentException(message, exception,
                    ((ECapeBadArgument)exception).position);
            case unchecked((int)0x80040507): //ECapeOutOfBoundsHR = 0x80040507 
            {
                Exception pEx = exception;
                if (exception is not ECapeBoundaries pBoundaries)
                    return new CapeOutOfBoundsException(
                        message, pEx, 0, 0.0, 0.0, 0.0, "");
                if (pBoundaries is not ECapeBadArgument pArgument)
                    return new CapeOutOfBoundsException(
                        message, pEx, 0, pBoundaries.lowerBound,
                        pBoundaries.upperBound, pBoundaries.value, pBoundaries.type);
                return new CapeOutOfBoundsException(
                    message, pEx, pArgument.position,
                    pBoundaries.lowerBound, pBoundaries.upperBound, pBoundaries.value, pBoundaries.type);

            }
            case unchecked((int)(0x80040508)): //ECapeImplementationHR = 0x80040508
                return new CapeImplementationException(message, exception);
            case unchecked((int)0x80040509): //ECapeNoImplHR = 0x80040509
                return new CapeNoImplException(message, exception);
            case unchecked((int)0x8004050A): //ECapeLimitedImplHR = 0x8004050A
                return new CapeLimitedImplException(message, exception);
            case unchecked((int)0x8004050B): //ECapeComputationHR = 0x8004050B 
                return new CapeComputationException(message, exception);
            case unchecked((int)0x8004050C): //ECapeOutOfResourcesHR = 0x8004050C
                return new CapeOutOfResourcesException(message, exception);
            case unchecked((int)0x8004050D): //ECapeNoMemoryHR = 0x8004050D
                return new CapeNoMemoryException(message, exception);
            case unchecked((int)0x8004050E): //ECapeTimeOutHR = 0x8004050E
                return new CapeTimeOutException(message, exception);
            case unchecked((int)0x8004050F): //ECapeFailedInitialisationHR = 0x8004050F 
                return new CapeFailedInitialisationException(message, exception);
            case unchecked((int)0x80040510): //ECapeSolvingErrorHR = 0x80040510
                return new CapeSolvingErrorException(message, exception);
            case unchecked((int)0x80040511): //ECapeBadInvOrderHR = 0x80040511 
            {
                return exception is not ECapeBadInvOrder pOrder 
                    ? new CapeBadInvOrderException(message, exception, "") 
                    : new CapeBadInvOrderException(message, exception, pOrder.requestedOperation);
            }
            case unchecked((int)0x80040512): //ECapeInvalidOperationHR = 0x80040512
                return new CapeInvalidOperationException(message, exception);
            case unchecked((int)0x80040513): //ECapePersistenceHR = 0x80040513
                return new CapePersistenceException(message, exception);
            case unchecked((int)0x80040514): //ECapeIllegalAccessHR = 0x80040514
                return new CapeIllegalAccessException(message, exception);
            case unchecked((int)0x80040515): //ECapePersistenceNotFoundHR = 0x80040515
                return new CapePersistenceNotFoundException(message, exception,
                    ((ECapePersistenceNotFound)exception).itemName);
            case unchecked((int)0x80040516): //ECapePersistenceSystemErrorHR = 0x80040516
                return new CapePersistenceSystemErrorException(message, exception);
            case unchecked((int)0x80040517): //ECapePersistenceOverflowHR = 0x80040517
                return new CapePersistenceOverflowException(message, exception);
            case unchecked((int)0x80040518): //ECapeOutsideSolverScopeHR = 0x80040518
                return new CapeSolvingErrorException(message, exception);
            case unchecked((int)0x80040519): //ECapeHessianInfoNotAvailableHR = 0x80040519
                return new CapeHessianInfoNotAvailableException(message, exception);
            case unchecked((int)0x80040520): //ECapeThrmPropertyNotAvailable = 0x80040520
                return new CapeThrmPropertyNotAvailableException(message, exception);
            default: //ECapeDataHR
                return new CapeUnknownException(message, exception);
        }
    }
}