using System.Runtime.InteropServices;

/// <summary>
/// 该命名空间提供了 CAPE-OPEN 接口的 .Net 翻译，
/// 并实现了 CAPE-OPEN 对象模型的核心组件。
/// 有关 CAPE-OPEN 的更多信息，请访问 CO-Lan 网站：http://www.co-lan.org。
/// </summary>
namespace CapeOpen
{
    // 应用代码示例：
    /// <code>
    /// [Serializable]
    /// 控制程序集中单独托管类型或成员，或所有类型对 COM 的访问性，使其可见。
    /// [System.Runtime.InteropServices.ComVisible(true)]
    /// 为 ICapeThermoMaterialObject_IID 设置 GUID 值，详见 CAPE-OPEN 接口文档。
    /// [System.Runtime.InteropServices.Guid(" ")] // 必需项
    /// 该类在新版本中已弃用，请使用 System.ComponentModel.DescriptionAttribute 代替。
    /// 指定属性或事件的说明。
    /// [System.ComponentModel.Description("")]
    /// 对 CAPE-OPEN 组件的描述，将被用来设置 CapeDescription[Description] 注册键的值。
    /// [CapeOpen.CapeNameAttribute("MixerExample")]
    /// 同上。（译者注：我也不明白为什么要重复定义一个属性，应该是官方文档中的描述有问题）
    /// [CapeOpen.CapeDescriptionAttribute("An example mixer unit operation.")]
    /// 该组件所支持的 CAPE-OPEN 版本号，用来设置 CapeDescription[CapeVersion] 注册键的值。
    /// [CapeOpen.CapeVersionAttribute("1.0")]
    /// 该组件的供应商 URL，用来设置 CapeDescription[VendorURL] 注册键的值。
    /// [CapeOpen.CapeVendorURLAttribute("http:\\www.epa.gov")]
    /// 该组件的帮助 URL，用来设置 CapeDescription[HelpURL] 注册键的值。
    /// [CapeOpen.CapeHelpURLAttribute("http:\\www.epa.gov")]
    /// 该组件的关于信息，用来设置 CapeDescription[About] 注册键的值。
    /// [CapeOpen.CapeAboutAttribute("US Environmental Protection Agency\nCincinnati, Ohio")]
    /// public class CMixerExample : public CUnitBase {}
    /// </code>

    /// <summary>
    /// 为注册 CAPE-OPEN 对象提供文本名称。
    /// </summary>
    /// <remarks><para>
    /// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeName[Name] 注册键的值。 
    /// </para></remarks>
    [ComVisible(false)]
    [AttributeUsage(AttributeTargets.Class)]
    public class CapeNameAttribute : Attribute
    {
        private String m_Name;

        /// <summary>初始化 CapeDescriptionAttribute 类的新实例。</summary>
        /// <remarks>注册功能使用描述值设置 CapeDescription[Description] 注册密钥的值。</remarks>
        /// <param name = "name">CAPE-OPEN 组件说明。</param>
        public CapeNameAttribute(String name)
        {
            m_Name = name;
        }

        /// <summary>获取名称信息。</summary>
        /// <remarks>名称的值。</remarks>
        /// <value>CAPE-OPEN 组件名称。</value>
        public String Name
        {
            get
            {
                return m_Name;
            }
        }
    }

    /// <summary>为 CAPE-OPEN 对象的注册提供文字说明。</summary>
    /// <remarks><para>
    /// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeDescription[Description] 注册键的值。 
    /// </para></remarks>
    [ComVisible(false)]
    [AttributeUsage(AttributeTargets.Class)]
    public class CapeDescriptionAttribute : Attribute
    {
        private String m_Description;

        /// <summary>初始化 CapeDescriptionAttribute 类的新实例。</summary>
        /// <remarks>注册功能使用描述值设置 CapeDescription[Description] 注册密钥的值。</remarks>
        /// <param name = "description">CAPE-OPEN 组件说明。</param>
        public CapeDescriptionAttribute(String description)
        {
            m_Description = description;
        }

        /// <summary>获取描述。</summary>
        /// <remarks>描述值。</remarks>
        /// <value>CAPE-OPEN 组件描述。</value>
        public String Description
        {
            get
            {
                return m_Description;
            }
        }
    }

    /// <summary>为注册 CAPE-OPEN 对象提供 CAPE-OPEN 版本号。</summary>
    /// <remarks><para>
    /// 该属性用于在 COM 注册表注册 CAPE-OPEN 对象时设置 CapeDescription[CapeVersion] 注册键的值。
    /// </para></remarks>
    [System.Runtime.InteropServices.ComVisibleAttribute(false)]
    [System.AttributeUsageAttribute(System.AttributeTargets.Class)]
    public class CapeVersionAttribute : System.Attribute
    {
        private String m_Version;
        /// <summary>初始化 CapeVersionAttribute 类的新实例。</summary>
        /// <remarks>设置 CapeDescription[CapeVersion] 注册键值。</remarks>
        /// <param name = "version">The version of the CAPE-OPEN interfaces that this object supports.</param>
        public CapeVersionAttribute(String version)
        {
            m_Version = version;
        }

        /// <summary>Gets the the CAPE-OPEN version number.</summary>
        /// <remarks>The value of the CAPE-OPEN version number.</remarks>
        /// <value>The CAPE-OPEN component CAPE-OPEN version number.</value>
        public String Version
        {
            get
            {
                return m_Version;
            }
        }
    }
}
