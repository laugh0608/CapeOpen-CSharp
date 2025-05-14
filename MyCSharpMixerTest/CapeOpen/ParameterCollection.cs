// 大白萝卜重构于 2025.05.12，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CapeOpen;

[ComVisible(false)]
internal class ParameterCollectionTypeConverter : ExpandableObjectConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return (typeof(ParameterCollection)).IsAssignableFrom(destinationType) 
               || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context,
        CultureInfo culture, object value, Type destinationType)
    {
        if (typeof(string).IsAssignableFrom(destinationType) 
            // && typeof(ICapeIdentification).IsAssignableFrom(value.GetType()))
            && value is ICapeIdentification)
        {
            return "Parameter Collection";
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}

/// <summary>CapeParameter 对象的类型安全集合。</summary>
/// <remarks>This collection uses the BindingList generic collection to create a collection that only
/// objects that implement the <seealse cref = "ICapeParameter"/> interface. This class also implements the 
/// ICustomTypeDescriptor to provide dynamic information about the collection.
/// Since this class utilizes a generic collection class, dotNet based objects can obtain 
/// the Parameter objects directly by using the index of the object. The .Net collection is 0-index,
/// that is, the index of the first parameter is 0, and the nth parameter has an index of n-1.
/// In addition, the collection can be accessed from COM through the <see cref="ICapeCollection"/> interface.
/// The ICapeCollection members are explicitly implemented, making them available only available to
/// COM through the ICapeCollection interface.</remarks>
[Serializable, ComVisible(true)]
[ComSourceInterfaces(
    typeof(ICapeIdentificationEvents), 
    /*typeof(ICapeCollectionEvents),*/ 
    typeof(IBindingList))]
[Guid(CapeGuids.PpParameterCollectionIid)] // ICapeThermoMaterialObject_IID "64A1B36C-106B-4d05-B585-D176CD4DD1DB"
[Description("")]
//[TypeConverter(typeof(ParameterCollectionTypeConverter))]
[ClassInterface(ClassInterfaceType.None)]
public class ParameterCollection :
    BindingList<ICapeParameter>, ICapeCollection, ICustomTypeDescriptor, ICloneable, ICapeIdentification
{
    private string _mComponentName;
    private string _mComponentDescription;

    // 这些是 ICapeCollection 成员的实现
    /// <summary>获取当前存储在集合中的项目数。</summary>
    /// <remarks>Gets the number of items currently stored in the collection.</remarks>
    /// <returns>Gets the number of items currently stored in the collection.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    int ICapeCollection.Count()
    {
        return Items.Count;
    }

    /// <summary>获取集合中存储的特定项，该项由其 ICapeIdentification.ComponentName 或以参数形式传递的
    /// 基于 1 的索引标识。</summary>
    /// <remarks>Return an element from the collection. The requested element can be 
    /// identified by its actual name (e.g. type CapeString) or by its position 
    /// in the collection (e.g. type CapeLong). The name of an element is the 
    /// value returned by the ComponentName() method of its ICapeIdentification 
    /// interface. The advantage of retrieving an item by name rather than by 
    /// position is that it is much more efficient. This is because it is faster 
    /// to check all names from the server part than checking then from the 
    /// client, where a lot of COM/CORBA calls would be required.</remarks>
    /// <param name = "index">
    /// <para>Identifier for the requested item:</para>
    /// <para>name of item (the variant contains a string)</para>
    /// <para>position in collection (it contains a long)</para></param>
    /// <returns>System.Object containing the requested collection item.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props' argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    object ICapeCollection.Item(object index)
    {
        var indexType = index.GetType();
        if (indexType == typeof(short) || (indexType == typeof(int)) || indexType == typeof(long))
        {
            var i = Convert.ToInt32(index);
            return Items[i - 1];
        }

        if (indexType != typeof(string))
            throw new CapeInvalidArgumentException("Item " + index + " not found.", 0);
        {
            var name = index.ToString();
            foreach (var mt in Items)
            {
                var pId = (ICapeIdentification)(mt);
                if (pId.ComponentName == name)
                {
                    return mt;
                }
            }
        }

        throw new CapeInvalidArgumentException("Item " + index + " not found.", 0);
    }

    /// <summary>初始化 <see cref="ParameterCollection"/> 集合类的新实例。</summary>
    /// <remarks>This will create a new instance of the collection.</remarks>
    public ParameterCollection() { }

    /// <summary> <see cref="ParameterCollection"/> 集合类的终结器。</summary>
    /// <remarks>This will finalize the current instance of the collection.</remarks>
    ~ParameterCollection()
    {
        foreach (CapeParameter item in Items)
        {
            item.Dispose();
        }
    }

    /// <summary>创建一个新对象，它是当前实例的副本。</summary>
    /// <remarks>Clone can be implemented either as a deep copy or a shallow copy. In a deep copy,
    /// all objects are duplicated; in a shallow copy, only the top-level objects are duplicated
    /// and the lower levels contain references. The resulting clone must be of the same type as,
    /// or compatible with, the original instance. See <see cref="Object.MemberwiseClone"/> for
    /// more information on cloning, deep versus shallow copies, and examples.</remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public object Clone()
    {
        ParameterCollection clone = new ParameterCollection();
        foreach (ICloneable item in Items)
        {
            clone.Add((CapeParameter)item.Clone());
        }

        return clone;
    }

    /// <summary>当用户更改组件名称时发生。</summary>
    /// <remarks>The event to be handles when the name of the PMC is changed.</remarks> 
    public event ComponentNameChangedHandler ComponentNameChanged;

    /// <summary>当用户更改组件的描述时发生。</summary>
    /// <remarks>Raising an event invokes the event handler through a delegate.
    /// The <c>OnComponentNameChanged</c> method also allows derived classes to handle the event
    /// without attaching a delegate. This is the preferred technique for handling the event in
    /// a derived class. otes to Inheritors:
    /// When overriding <c>OnComponentNameChanged</c> in a derived class, be sure to call the base
    /// class's <c>OnComponentNameChanged</c> method so that registered delegates receive the event.</remarks>
    /// <param name = "args">A <see cref = "ComponentNameChangedEventArgs">NameChangedEventArgs</see> that contains information about the event.</param>
    protected void OnComponentNameChanged(ComponentNameChangedEventArgs args)
    {
        ComponentNameChanged?.Invoke(this, args);
    }

    /// <summary>当用户更改组件的描述时发生。</summary>
    /// <remarks>The event to be handles when the description of the PMC is changed.</remarks> 
    public event ComponentDescriptionChangedHandler ComponentDescriptionChanged;

    /// <summary>当用户更改组件的描述时发生。</summary>
    /// <remarks>Raising an event invokes the event handler through a delegate.
    /// The <c>OnComponentDescriptionChanged</c> method also allows derived classes to handle
    /// the event without attaching a delegate. This is the preferred technique for handling the
    /// event in a derived class. Notes to Inheritors:
    /// When overriding <c>OnComponentDescriptionChanged</c> in a derived class, be sure to call
    /// the base class's <c>OnComponentDescriptionChanged</c> method so that registered delegates receive the event.</remarks>
    /// <param name = "args">A <see cref = "ComponentDescriptionChangedEventArgs">DescriptionChangedEventArgs</see> that contains information about the event.</param>
    protected void OnComponentDescriptionChanged(ComponentDescriptionChangedEventArgs args)
    {
        ComponentDescriptionChanged?.Invoke(this, args);
    }

    /// <summary>获取和设置组件的名称。</summary>
    /// <remarks>A particular Use Case in a system may contain several CAPE-OPEN components 
    /// of the same class. The user should be able to assign different names and 
    /// descriptions to each instance in order to refer to them unambiguously and in a 
    /// user-friendly way. Since not always the software components that are able to 
    /// set these identifications and the software components that require this information 
    /// have been developed by the same vendor, a CAPE-OPEN standard for setting and 
    /// getting this information is required. So, the component will not usually set its own
    /// name and description: the user of the component will do it.</remarks>
    /// <value>The unique name of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("CapeIdentification")]
    public string ComponentName
    {
        get => _mComponentName;
        set
        {
            var args = new ComponentNameChangedEventArgs(_mComponentName, value);
            _mComponentName = value;
            OnComponentNameChanged(args);
        }
    }

    /// <summary>获取和设置组件的描述。</summary>
    /// <remarks>A particular Use Case in a system may contain several CAPE-OPEN components 
    /// of the same class. The user should be able to assign different names and 
    /// descriptions to each instance in order to refer to them unambiguously and in a 
    /// user-friendly way. Since not always the software components that are able to 
    /// set these identifications and the software components that require this information 
    /// have been developed by the same vendor, a CAPE-OPEN standard for setting and 
    /// getting this information is required. So, the component will not usually set its own
    /// name and description: the user of the component will do it.</remarks>
    /// <value>The description of the component.</value>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    [Description(
        "Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
    [Category("CapeIdentification")]
    public string ComponentDescription
    {
        get => _mComponentDescription;
        set
        {
            var args = new ComponentDescriptionChangedEventArgs(_mComponentDescription, value);
            _mComponentDescription = value;
            OnComponentDescriptionChanged(args);
        }
    }

    // ICustomTypeDescriptor 的实现：

    string ICustomTypeDescriptor.GetClassName()
    {
        return TypeDescriptor.GetClassName(this, true);
    }

    AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
        return TypeDescriptor.GetAttributes(this, true);
    }

    string ICustomTypeDescriptor.GetComponentName()
    {
        return TypeDescriptor.GetComponentName(this, true);
    }

    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
        return TypeDescriptor.GetConverter(this, true);
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
        return TypeDescriptor.GetDefaultEvent(this, true);
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
        return TypeDescriptor.GetDefaultProperty(this, true);
    }

    object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
    {
        return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
        Attribute[] attributes)
    {
        return TypeDescriptor.GetEvents(this, attributes, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
        return TypeDescriptor.GetEvents(this, true);
    }

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
    {
        return this;
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
        Attribute[] attributes)
    {
        return ((ICustomTypeDescriptor)this).GetProperties();
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
        // 创建新的集合对象 PropertyDescriptorCollection
        var pds = new PropertyDescriptorCollection(null);
        // 迭代参数列表
        for (var i = 0; i < Items.Count; i++)
        {
            // 为每个参数创建一个属性描述符，并将其添加到 PropertyDescriptorCollection 实例中
            var pd = new ParameterCollectionPropertyDescriptor(this, i);
            pds.Add(pd);
        }

        return pds;
    }
}

//class ParameterCollectionEditor : CollectionEditor
//{
//    private Type[] m_types;

//    public ParameterCollectionEditor(Type t)
//        : base(t)
//    {
//        m_types = new Type[4];
//        m_types[0] = typeof(CapeOpen.BooleanParameter);
//        m_types[1] = typeof(CapeOpen.IntegerParameter);
//        m_types[2] = typeof(CapeOpen.OptionParameter);
//        m_types[3] = typeof(CapeOpen.RealParameter);
//    }

//    protected override Type[] CreateNewItemTypes()
//    {
//        return m_types;
//    }
//};

/// <summary>简要说明 CollectionPublicDescriptor。</summary>
[ComVisible(false)]
internal class ParameterCollectionPropertyDescriptor : PropertyDescriptor
{
    private ParameterCollection _collection;
    private int _index;

    public ParameterCollectionPropertyDescriptor(ParameterCollection coll, int idx) :
        base("#" + idx, null)
    {
        _collection = coll;
        _index = idx;
    }

    public override AttributeCollection Attributes => new(null);

    public override bool CanResetValue(object component)
    {
        return true;
    }

    public override Type ComponentType => _collection.GetType();

    public override string DisplayName => ((ICapeIdentification)_collection[_index]).ComponentName;

    public override string Description => ((ICapeIdentification)_collection[_index]).ComponentDescription;

    public override object GetValue(object component)
    {
        return _collection[_index];
    }

    public override bool IsReadOnly => true;

    public override string Name => string.Concat("#", _index.ToString());

    public override Type PropertyType => _collection[_index].GetType();

    public override void ResetValue(object component)
    {
    }

    public override bool ShouldSerializeValue(object component)
    {
        return true;
    }

    public override void SetValue(object component, object value)
    {
        //this.collection[index] = value;
    }
}
