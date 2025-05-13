// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CapeOpen;

[ComVisible(false)]
internal class PortConverter : ExpandableObjectConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return typeof(ICapeUnitPort).IsAssignableFrom(destinationType) 
               || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context,
        CultureInfo culture, object value, Type destinationType)
    {
        if (!typeof(string).IsAssignableFrom(destinationType) 
            // || !typeof(ICapeUnitPort).IsAssignableFrom(value.GetType()))
            || value is not ICapeUnitPort pPort)
            return base.ConvertTo(context, culture, value, destinationType);
        var connectedObject = pPort.connectedObject;
        return connectedObject != null 
            ? ((ICapeIdentification)connectedObject).ComponentName 
            : "No Connected Object.";
    }
}

/// <summary>Represents the method that will handle connecting an object to a unit port.</summary>
[ComVisible(false)]
public delegate void PortConnectedHandler(object sender, PortConnectedEventArgs args);

[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComVisible(true)]
[Guid(CapeOpenGuids.InUnitPortEventsIid)]  // "3530B780-5E59-42B1-801B-3C18F2AD08EE"
[Description("CapeRealParameterEvents Interface")]
internal interface IUnitPortEvents
{
    /// <summary>Occurs when the user connects a new object to a unit port.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnPortConnected</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnPortConnected</c> in a derived class, be sure to call the base class's <c>OnPortConnected</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "UnitPort">CapeUnitPort</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "PortConnectedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    void PortConnected(UnitPort sender, PortConnectedEventArgs args);

    /// <summary>Occurs when the user disconnects a object from a unit port.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnPortDisconnected</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnPortDisconnected</c> in a derived class, be sure to call the base class's <c>OnPortDisconnected</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "sender">The <see cref = "UnitPort">CapeUnitPort</see> that raised the event.</param>
    /// <param name = "args">A <see cref = "PortDisconnectedEventArgs">ParameterValueChangedEventArgs</see> that contains information about the event.</param>
    void PortDisconnected(UnitPort sender, PortDisconnectedEventArgs args);
}

/// <summary>This class represents the behaviour of a Unit 
///	Operation connection point (Unit Operation Port). It provides different 
///	attributes for configuring the port as well as to connect 
///	it to a material, energy or information object.</summary>
/// <remarks><para>The unit port provides the means by which a Flowsheet Unit is connected to its streams. 
/// Streams are implemented by means of material objects.</para>
/// <para>The three types of port: material, energy and 
///	information, have a lot of functionality in common. By combining the three into one we can simplify 
///	the interface to a useful degree. Each port type is to be distinguished by the value of an attribute.</para></remarks>
[Serializable,ComVisible(true)]
[ComSourceInterfaces(typeof(IUnitPortEvents))]
[Guid(CapeOpenGuids.PpUnitPortIid)] //ICapeThermoMaterialObject_IID  "51066F52-C0F9-48d7-939E-3A229010E77C"
[Description("")]
[ClassInterface(ClassInterfaceType.None)]
[TypeConverter(typeof(PortConverter))]
public class UnitPort : CapeIdentification, ICapeUnitPort, ICapeUnitPortCOM
{
    private CapePortDirection _mDirection;
    private CapePortType _mType;
    private object _mConnectedObject;
    private bool _isConnectObjectSerializable;
    [NonSerialized] private object _mConnectedNonSerializableObject;

    /// <summary>Initializes a new instance of the <see cref="UnitPort"/> class.</summary>
    /// <param name="name"><see cref="ICapeIdentification.ComponentName"/> of the <see cref="UnitPort"/>.</param>
    /// <param name="description"><see cref="ICapeIdentification.ComponentDescription"/> of the <see cref="UnitPort"/>.</param>
    /// <param name="direction"><see cref="CapePortDirection"/> of the <see cref="UnitPort"/></param>
    /// <param name="type"><see cref="CapePortType"/> of the <see cref="UnitPort"/></param>
    public UnitPort(string name, string description, CapePortDirection direction, CapePortType type)
        : base(name, description)
    {
        _mDirection = direction;
        _mType = type;
        _mConnectedObject = null;
        _mConnectedNonSerializableObject = null;
        _isConnectObjectSerializable = false;
    }

    /// <summary>Finalizer for the <see cref = "UnitPort"/> class.</summary>
    /// <remarks>This will finalize the current instance of the class.</remarks>
    ~UnitPort()
    {
        Disconnect(false);
    }

    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <remarks><para>Clone can be implemented either as a deep copy or a shallow copy. In a deep copy, all objects are duplicated; 
    /// in a shallow copy, only the top-level objects are duplicated and the lower levels contain references.</para>
    /// <para>The resulting clone must be of the same type as, or compatible with, the original instance.</para>
    /// <para>See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples.</para></remarks>
    /// <returns>A new object that is a copy of this instance.</returns>
    public override object Clone()
    {
        return new UnitPort(ComponentName, ComponentDescription, direction, portType);
    }

    /// <summary>Occurs when the user connects a new object to the port.</summary>
    public event PortConnectedHandler PortConnected;

    /// <summary>Occurs when the user connects a new object to the port.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnPortConnected</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnPortConnected</c> in a derived class, be sure to call the base class's <c>OnPortConnected</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "PortConnectedEventArgs">PortConnectedEventArgs</see> that contains information about the event.</param>
    protected void OnPortConnected(PortConnectedEventArgs args)
    {
        PortConnected?.Invoke(this, args);
    }

    /// <summary>Occurs when the user disconnects an object from the port.</summary>
    public event PortDisconnectedHandler PortDisconnected;

    /// <summary>Occurs when the user disconnects an object from the port.</summary>
    /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
    /// <para>The <c>OnPortDisconnected</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
    /// technique for handling the event in a derived class.</para>
    /// <para>Notes to Inheritors: </para>
    /// <para>When overriding <c>OnPortDisconnected</c> in a derived class, be sure to call the base class's <c>OnPortConnected</c> method so that registered 
    /// delegates receive the event.</para></remarks>
    /// <param name = "args">A <see cref = "PortDisconnectedEventArgs">PortDisconnectedEventArgs</see> that contains information about the event.</param>
    protected void OnPortDisconnected(PortDisconnectedEventArgs args)
    {
        PortDisconnected?.Invoke(this, args);
    }
    
    // ICapeUnitPort
    /// <summary>Returns the type of this port and allows the developer to change 
    /// the port type (allowed types are among the ones included in the CapePortType type.</summary>
    /// <see cref="CapePortType">CapePortType</see> 
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [Category("ICapeUnitPort")]
    public CapePortType portType
    {
        get => _mType;
        set
        {
            _mType = value;
            NotifyPropertyChanged(nameof(portType));
        }
    }

    /// <summary>Returns the direction of this port and allows the developer to change 
    /// the port direction. Allowed values are among those included 
    /// in the CapePortDirection type.</summary>
    /// <see cref="CapePortDirection">CapePortDirection</see>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    [Category("ICapeUnitPort")]
    public CapePortDirection direction
    {
        get => _mDirection;
        set
        {
            _mDirection = value;
            NotifyPropertyChanged(nameof(direction));
        }
    }

    /// <summary>Returns to the client the object that is connected to this port.</summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    object ICapeUnitPortCOM.connectedObject
    {
        get
        {
            if (_isConnectObjectSerializable) return _mConnectedObject;
            return _mConnectedNonSerializableObject switch
            {
                null => _mConnectedNonSerializableObject,
                // if (typeof(MaterialObjectWrapper).IsAssignableFrom(_mConnectedNonSerializableObject.GetType()))
                MaterialObjectWrapper pWrapper => pWrapper.MaterialObject10,
                _ => _mConnectedNonSerializableObject
            };
        }
    }

    /// <summary>Returns to the client the object that is connected to this port.</summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
    public virtual object connectedObject => _isConnectObjectSerializable 
        ? _mConnectedObject 
        : _mConnectedNonSerializableObject;

    /// <summary>Connects an object to the port. For a material port it must 
    /// be an object implementing the ICapeThermoMaterialObject interface, 
    /// for Energy and Information ports it must be an object implementing 
    /// the ICapeParameter interface. </summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
    public virtual void Connect(object objectToConnect)
    {
        Disconnect(true);
        var args = new PortConnectedEventArgs(ComponentName);
        if (objectToConnect.GetType().IsCOMObject)
        {
            //If the port is a material port
            if (_mType is CapePortType.CAPE_MATERIAL or CapePortType.CAPE_ANY)
            {
                switch (objectToConnect)
                {
                    // does the material object support both thermo 1.0 and 1.1?
                    case ICapeThermoMaterialObjectCOM and ICapeThermoMaterialCOM:
                        //use the material wrapper that exposes both interfaces.
                        _mConnectedNonSerializableObject = new MaterialObjectWrapper(objectToConnect);
                        _mConnectedObject = null;
                        _isConnectObjectSerializable = false;
                        OnPortConnected(args);
                        NotifyPropertyChanged(nameof(connectedObject));
                        return;
                    // Does the material only support thermo 1.1?
                    case ICapeThermoMaterialCOM:
                        //then only use the material wrapper that exposes thermo 1.1
                        _mConnectedNonSerializableObject = new MaterialObjectWrapper11(objectToConnect);
                        _mConnectedObject = null;
                        _isConnectObjectSerializable = false;
                        OnPortConnected(args);
                        NotifyPropertyChanged(nameof(connectedObject));
                        return;
                    //Does the thermo only support thermo 1.0?
                    case ICapeThermoMaterialObjectCOM:
                        // then use the wrapper that supports thermo 1.0.
                        _mConnectedNonSerializableObject = new MaterialObjectWrapper10(objectToConnect);
                        _mConnectedObject = null;
                        _isConnectObjectSerializable = false;
                        OnPortConnected(args);
                        NotifyPropertyChanged(nameof(connectedObject));
                        return;
                }

                //If we get here, the object to connect is not a material.
                //The object will be connected as-is. 
                _mConnectedNonSerializableObject = objectToConnect;
                _mConnectedObject = null;
                _isConnectObjectSerializable = false;
                OnPortConnected(args);
                NotifyPropertyChanged(nameof(connectedObject));
                return;
            }
        }

        if (objectToConnect.GetType().IsSerializable)
        {
            _mConnectedObject = objectToConnect;
            _isConnectObjectSerializable = true;
            _mConnectedNonSerializableObject = null;
            OnPortConnected(args);
            NotifyPropertyChanged(nameof(connectedObject));
            return;
        }

        _mConnectedNonSerializableObject = objectToConnect;
        _mConnectedObject = null;
        _isConnectObjectSerializable = false;
        OnPortConnected(args);
        NotifyPropertyChanged(nameof(connectedObject));
    }

    /// <summary>Disconnects whatever object is connected to this port.</summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public virtual void Disconnect()
    {
        Disconnect(false);
    }

    /// <summary>Disconnects whatever object is connected to this port.</summary>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    private void Disconnect(bool connecting)
    {
        if (_mConnectedNonSerializableObject != null)
        {
            if (_mConnectedNonSerializableObject.GetType().IsCOMObject)
            {
                Marshal.FinalReleaseComObject(_mConnectedNonSerializableObject);
            }

            if (_mConnectedNonSerializableObject is MaterialObjectWrapper pWrapper)
                pWrapper.Dispose();
            _mConnectedNonSerializableObject = null;
            return;
        }

        _mConnectedObject = null;
        var args = new PortDisconnectedEventArgs(ComponentName);
        OnPortDisconnected(args);
        if (!connecting) NotifyPropertyChanged(nameof(connectedObject));
    }
}