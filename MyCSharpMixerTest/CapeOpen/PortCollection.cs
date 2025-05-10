// 大白萝卜重构于 2025.05.10，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CapeOpen;

[ComVisible(false)]
internal class PortCollectionTypeConverter : ExpandableObjectConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return typeof(PortCollection).IsAssignableFrom(destinationType) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context,
        CultureInfo culture, object value, Type destinationType)
    {
        if (typeof(string).IsAssignableFrom(destinationType) && value is PortCollection)
        {
            return "Port Collection";
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}

/// <summary> 对象 <see cref="UnitPort"/> 的类型安全集合。</summary>
/// <remarks>此集合使用BindingList泛型集合来创建仅实现 <seealse cref = "ICapeUnitPort"/> 接口的对象。
/// 该类还实现了 <seealse cref = "ICustomTypeDescriptor"/> 来提供有关集合的动态信息。
/// 由于此类利用泛型集合类，基于点网的对象可以获取通过使用对象的索引直接访问端口对象。
/// dotNet 集合的索引为 0，即第一个端口的索引为 0，第 n个端口的索引为 n-1。
/// 此外，COM 可以通过 < see cref = "ICapeCollection"/> 接口访问该集合。
/// ICapeCollection 成员是显式实现的，使它们只能通过 COM 使用 ICapeCollection 接口。</remarks>
[Serializable]
[ComSourceInterfaces( typeof(ICapeIdentificationEvents) )]    // 和 , typeof(ICapeCollectionEvents)
[ComVisible(true)]
[Guid(CapeOpenGuids.PortCollectionIid)] // ICapeThermoMaterialObject_IID 1C5F7CC3-31B4-4d81-829F-3EB5D692F7BD
[Description("")]
// [TypeConverter(typeof(PortCollectionTypeConverter))]
[ClassInterface(ClassInterfaceType.None)]
public class PortCollection : BindingList<ICapeUnitPort>,
    ICapeCollection, ICustomTypeDescriptor, ICloneable, ICapeIdentification
{
    private string _mComponentName;
    private string _mComponentDescription;

    // 这些是 ICapeCollection 成员实现

    /// <summary>获取当前存储在集合中的项数。</summary>
    /// <returns>Gets the number of items currently stored in the collection.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    int ICapeCollection.Count()
    {
        return Items.Count;
    }

    /// <summary>获取存储在集合中的特定项，由它的 ICapeIdentification 作为参数传递的 ComponentName 或从 1 开始的索引到方法。</summary>
    /// <remarks>从集合中返回一个元素。请求的元素可以通过其实际名称（例如类型 CapeString）或其在集合中的位置（例如类型CapeLong）来识别。
    /// 元素的名称是其 ICapeIdentification 接口的 ComponentName() 方法返回的值。通过名称而不是通过位置检索项的优势在于，
    /// 它更高效。这是因为从服务器部分检查所有名称比从客户端检查要快得多，在客户端需要大量的 COM/CORBA 调用。</remarks>
    /// <param name = "index"><para>Identifier for the requested item:</para>
    /// <para>name of item (the variant contains a string)</para>
    /// <para>position in collection (it contains a long)</para></param>
    /// <returns>System.object containing the requested collection item.</returns>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s), specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the prop's argument.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    /// <exception cref = "ECapeOutOfBounds">ECapeOutOfBounds</exception>
    object ICapeCollection.Item(object index)
    {
        var indexType = index.GetType();
        if (indexType == typeof(short) || indexType == typeof(int) || indexType == typeof(long))
        {
            var i = Convert.ToInt32(index);
            return Items[i - 1];
        }

        if (indexType != typeof(string))
            throw new CapeInvalidArgumentException("Item " + index + " not found.", 0);
        {
            var name = index.ToString();
            foreach (var t in Items)
            {
                var pId = (ICapeIdentification) t;
                if (pId.ComponentName == name)
                {
                    return t;
                }
            }
        }
        throw new CapeInvalidArgumentException("Item " + index + " not found.", 0);
    }

    /// <summary>初始化 <see cref = "PortCollection"/> 集合类的新的实例。</summary>
    /// <remarks>这将创建集合的新实例。</remarks>
    public PortCollection()
    {
    }

    /// <summary>集合类 < see cref = "PortCollection"/> 的终结器。</summary>
    /// <remarks>这将完成集合的当前实例。</remarks>
    ~PortCollection()
    {
        foreach (UnitPort item in Items)
        {
            item.Dispose();
        }
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <remarks>克隆可以实现为深层拷贝或浅层拷贝。在深层拷贝中，所有对象都被复制；
    /// 在浅表副本中，仅复制顶级对象，较低级别的对象包含引用。生成的克隆必须与原始实例的类型相同或兼容。
    /// 请参见< see cref="object.MemberwiseClone "/>，了解有关克隆、深层副本与浅层副本以及示例的更多信息。</remarks>
    /// <returns>作为此实例副本的新对象。</returns>
    public object Clone()
    {
        var clone = new PortCollection();
        foreach (ICloneable item in Items)
        {
            clone.Add((UnitPort)item.Clone());
        }
        return clone;
    }

    /// <summary>当用户更改组件名称时发生。</summary>
    /// <remarks>当 PMC 的名称更改时要处理的事件。</remarks> 
    public event ComponentNameChangedHandler ComponentNameChanged;

    /// <summary>当用户更改组件的说明时发生。</summary>
    /// <remarks>通过委托调用事件处理程序时，会引发事件。<c>OnComponentNameChanged</c> 方法还允许
    /// 子类在不附加委托的情况下处理事件。这是子类处理事件的优选技术。继承者的注意事项：
    /// 在子类中重写<c>OnComponentNameChanged </c>时，请务必调用基类的<c>OnComponentNameChanged（）</c>方法，以便注册的委托接收事件。</remarks>
    /// <param name = "args">A <see cref = "ComponentNameChangedEventArgs">NameChangedEventArgs</see> that contains information about the event.</param>
    protected void OnComponentNameChanged(ComponentNameChangedEventArgs args)
    {
        ComponentNameChanged?.Invoke(this, args);
    }

    /// <summary>当用户更改组件的说明时发生。</summary>
    /// <remarks>当 PMC 的描述更改时要处理的事件。</remarks> 
    public event ComponentDescriptionChangedHandler ComponentDescriptionChanged;

    /// <summary>当用户更改组件的说明时发生。</summary>
    /// <remarks>引发事件通过委托调用事件处理程序。
    /// 方法 <c>OnComponentDescriptionChanged</c> 还允许子类在不附加委托的情况下处理事件。这是子类处理事件的优选技术。
    /// 继承时的注意事项: 
    /// 当在派生类中重写 <c>OnComponentDescriptionChanged</c> 时，请务必调用基类的 <c>OnComponentDescriptionChanged </c> 方法，以便注册的委托接收该事件。</remarks>
    /// <param name = "args">A <see cref = "ComponentDescriptionChangedEventArgs">DescriptionChangedEventArgs</see> that contains information about the event.</param>
    protected void OnComponentDescriptionChanged(ComponentDescriptionChangedEventArgs args)
    {
        ComponentDescriptionChanged?.Invoke(this, args);
    }

    /// <summary>获取和设置组件的名称。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，
    /// 因此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的使用者会这样做。</remarks>
    /// <value>The unique name of the component.</value>
    /// <exception cref ="ECapeUnknown">为该操作指定的其他错误不适用时引发的错误。</exception>
    [Description("Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
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

    /// <summary>获取和设置组件的说明。</summary>
    /// <remarks>一个系统中的特定用例可能包含多个相同类的 CAPE-OPEN 组件。用户应该能够为每个实例分配不同的名称和描述，
    /// 以便以不模糊和用户友好的方式引用它们。由于并非总是能够设置这些标识的软件组件和需要此信息的软件组件由同一供应商开发，因
    /// 此需要设置和获取此信息的 CAPE-OPEN 标准。因此，组件通常不会设置自己的名称和描述：组件的使用者会这样做。</remarks>
    /// <value>The description of the component.</value>
    /// <exception cref ="ECapeUnknown">为该操作指定的其他错误不适用时引发的错误。</exception>
    [Description("Unit Operation Parameter Collection. Click on the (...) button to edit collection.")]
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

    // ICustomTypeDescriptor 的实现:
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

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
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

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
    {
        return ((ICustomTypeDescriptor)this).GetProperties();
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
        // 创建新的集合对象 PropertyDescriptorCollection
        var pds = new PropertyDescriptorCollection(null);
        // 迭代端口列表
        for (var i = 0; i < Items.Count; i++)
        {
            // 为每个端口创建一个属性描述符，并将其添加到 PropertyDescriptorCollection 实例中
            var pd = new PortCollectionPropertyDescriptor(this, i);
            pds.Add(pd);
        }
        return pds;
    }
}

/// <summary>CollectionPropertyDescriptor 的摘要说明。</summary>
[ComVisible(false)]
internal class PortCollectionPropertyDescriptor : PropertyDescriptor
{
    private PortCollection _collection;
    private int _index;

    public PortCollectionPropertyDescriptor(PortCollection coll, int idx) :
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

    public override Type ComponentType => this._collection.GetType();

    public override string DisplayName => ((UnitPort)_collection[_index]).ComponentName;

    public override string Description => ((UnitPort)_collection[_index]).ComponentDescription;

    public override object GetValue(object component)
    {
        return (UnitPort)_collection[_index];
    }

    public override bool IsReadOnly => false;

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
        // this.collection[index] = value;
    }
}
