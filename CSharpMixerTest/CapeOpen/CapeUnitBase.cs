// 大白萝卜重构于 2025.05.14，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CapeOpen
{
    internal class SelectedReportConverter : StringConverter
    {

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var unit = (CapeUnitBase)context.Instance;
            return new StandardValuesCollection(unit.Reports);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    /// <summary>
    /// Abstract base class to be used to develop unit operation models. 
    /// </summary>
    /// <remarks>
    /// This abstract class contains all required functionality for a unit operation
    /// PMC except the <c>Calculate()</c> method, which is a pure virtual function that 
    /// must be overridden. To use, add  parameters and ports to the appropriate collection 
    /// and implement the <c>Calculate()</c> method.
    /// </remarks>
    [Serializable]
    [ComVisible(true)]
    [ComSourceInterfaces(typeof(IUnitOperationValidatedEventArgs))]
    [ClassInterface(ClassInterfaceType.None)]
    public abstract class CapeUnitBase : CapeObjectBase,
        ICapeUnit, ICapeUnitCOM, ICapeUnitReport, ICapeUnitReportCOM
    //  IPersist, IPersistStream, IPersistStreamInit
    {
        private CapeValidationStatus _mValStatus;
        // private bool m_dirty;
        private string _mSelectReport;
        private List<string> _mReports;
        // Track whether Dispose has been called.
        private bool _disposed;
        
        /// <summary>
        /// Gets the collection of unit operation ports. 
        /// </summary>
        /// <remarks>
        /// Return type is System.Object and this method is simply here for classic 
        /// COM-based CAPE-OPEN interop.
        /// </remarks>
        /// <value>The port collection of the unit operation.</value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        [Browsable(false)]
        object ICapeUnitCOM.ports => Ports;

        /// <summary>
        ///	Gets the list of possible reports for the unit operation.
        /// </summary>
        /// <value>
        /// Return type is System.Object and this method is simply here for 
        /// classic COM-based CAPE-OPEN interop.
        /// </value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        [Browsable(false)]
        object ICapeUnitReportCOM.reports => _mReports.ToArray();


        /// <summary>
        ///	Produces the active report for the unit operation.
        /// </summary>
        /// <remarks>
        ///	Produce the designated report. If no value has been set, it produces the default report.
        /// </remarks>
        /// <param name = "message">String containing the text for the currently selected report.</param>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        void ICapeUnitReportCOM.ProduceReport(ref string message)
        {
            message = ProduceReport();
        }
    
    /// <summary>
        ///	Constructor for the unit operation.
        /// </summary>
        /// <remarks>
        /// This method is creates the port and parameter collections for the unit 
        /// operation. As a result, ports and parameters can be added in the constructor
        /// for the derived unt or during the <c>Initialize()</c> call. 
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
        /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        protected CapeUnitBase()
        {
            Ports = [];
            Ports.AddingNew += m_Ports_AddingNew;
            Ports.ListChanged += m_Ports_ListChanged;
            _mValStatus = CapeValidationStatus.CAPE_NOT_VALIDATED;
            _mReports = new List<string>();
            _mReports.Add("Default Report");
            _mSelectReport = "Default Report";
        }

        /// <summary>
        /// Finalizer for the <see cref = "CapeUnitBase"/> class.
        /// </summary>
        /// <remarks>
        /// This will finalize the current instance of the class.
        /// </remarks>
        ~CapeUnitBase()
        {
            Dispose(true);
        }
         
        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <remarks>
        /// <para>
        /// Clone can be implemented either as a deep copy or a shallow copy. In a deep copy, all objects are duplicated; 
        /// in a shallow copy, only the top-level objects are duplicated and the lower levels contain references.
        /// </para>
        /// <para>
        /// The resulting clone must be of the same type as, or compatible with, the original instance.
        /// </para>
        /// <para>
        /// See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples.
        /// </para>
        /// </remarks>
        /// <returns>A new object that is a copy of this instance.</returns>
        /// <param name = "objectToBeCopied">The unit operation being cloned.</param>
        protected CapeUnitBase(CapeUnitBase objectToBeCopied)
            : base(objectToBeCopied)
        {
            Ports = (PortCollection)objectToBeCopied.Ports.Clone();
            _mReports.AddRange(objectToBeCopied.Reports);
            _mValStatus = CapeValidationStatus.CAPE_NOT_VALIDATED;
            _mSelectReport = objectToBeCopied.selectedReport;
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        /// <summary>
        /// Releases the unmanaged resources used by the CapeIdentification object and optionally releases 
        /// the managed resources.
        /// </summary>
        /// <remarks><para>This method is called by the public <see href="http://msdn.microsoft.com/en-us/library/system.componentmodel.component.dispose.aspx">Dispose</see>see> 
        /// method and the <see href="http://msdn.microsoft.com/en-us/library/system.object.finalize.aspx">Finalize</see> method. 
        /// <bold>Dispose()</bold> invokes the protected <bold>Dispose(Boolean)</bold> method with the disposing
        /// parameter set to <bold>true</bold>. <see href="http://msdn.microsoft.com/en-us/library/system.object.finalize.aspx">Finalize</see> 
        /// invokes <bold>Dispose</bold> with disposing set to <bold>false</bold>.</para>
        /// <para>When the <italic>disposing</italic> parameter is <bold>true</bold>, this method releases all 
        /// resources held by any managed objects that this Component references. This method invokes the 
        /// <bold>Dispose()</bold> method of each referenced object.</para>
        /// <para><bold>Notes to Inheritors</bold></para>
        /// <para><bold>Dispose</bold> can be called multiple times by other objects. When overriding 
        /// <bold>Dispose(Boolean)</bold>, be careful not to reference objects that have been previously 
        /// disposed of in an earlier call to <bold>Dispose</bold>. For more information about how to 
        /// implement <bold>Dispose(Boolean)</bold>, see <see href="http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx">Implementing a Dispose Method</see>.</para>
        /// <para>For more information about <bold>Dispose</bold> and <see href="http://msdn.microsoft.com/en-us/library/system.object.finalize.aspx">Finalize</see>, 
        /// see <see href="http://msdn.microsoft.com/en-us/library/498928w2.aspx">Cleaning Up Unmanaged Resources</see> 
        /// and <see href="http://msdn.microsoft.com/en-us/library/ddae83kx.aspx">Overriding the Finalize Method</see>.</para>
        /// </remarks> 
        /// <param name = "disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (_disposed) return;
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                foreach (UnitPort port in Ports)
                    port.Disconnect();
                Ports.Clear();
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <remarks>
        /// <para>
        /// Clone can be implemented either as a deep copy or a shallow copy. In a deep copy, all objects are duplicated; 
        /// in a shallow copy, only the top-level objects are duplicated and the lower levels contain references.
        /// </para>
        /// <para>
        /// The resulting clone must be of the same type as, or compatible with, the original instance.
        /// </para>
        /// <para>
        /// See <see cref="Object.MemberwiseClone"/> for more information on cloning, deep versus shallow copies, and examples.
        /// </para>
        /// </remarks>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            var unitType = GetType();
            var retVal = (CapeUnitBase)Activator.CreateInstance(unitType);
            retVal.Parameters.Clear();
            foreach (CapeParameter param in Parameters)
            {
                retVal.Parameters.Add((CapeParameter)param.Clone());
            }
            retVal.Ports.Clear();
            foreach (UnitPort port in Ports)
            {
                retVal.Ports.Add((UnitPort)port.Clone());
            }
            retVal.Reports.Clear();
            retVal.Reports.AddRange(_mReports);
            retVal.selectedReport = _mSelectReport;
            retVal.SimulationContext = SimulationContext;
            return retVal;
        }

        /// <summary>
        /// Represents the method that will handle the changing the name of a component.
        /// </summary>
        /// <remarks>
        /// When you create a ComponentNameChangedHandler delegate, you identify the method that will handle the event. To associate the event with your event handler, add an 
        /// instance of the delegate to the event. The event handler is called whenever the event occurs, unless you remove the delegate. For more information about delegates, 
        /// see Events and Delegates.
        /// </remarks>
        /// <param name = "sender">The PMC that is the source .</param>
        /// <param name = "args">A <see cref = "System.ComponentModel.ListChangedEventArgs">System.ComponentModel.ListChangedEventArgs</see> that provides information about the name change.</param>
        private void m_Ports_ListChanged(object sender, ListChangedEventArgs args)
        {
            OnPortCollectionListChanged(args);
        }

        /// <summary>
        /// Represents the method that will handle the changing the name of a component.
        /// </summary>
        /// <remarks>
        /// When you create a ComponentNameChangedHandler delegate, you identify the method that will handle the event. To associate the event with your event handler, add an 
        /// instance of the delegate to the event. The event handler is called whenever the event occurs, unless you remove the delegate. For more information about delegates, 
        /// see Events and Delegates.
        /// </remarks>
        /// <param name = "sender">The PMC that is the source .</param>
        /// <param name = "args">A <see cref = "System.ComponentModel.AddingNewEventArgs">System.ComponentModel.AddingNewEventArgs</see> that provides information about the name change.</param>
        private void m_Ports_AddingNew(object sender, AddingNewEventArgs args)
        {
            OnPortCollectionAddingNew(args);
        }

        /// <summary>
        /// Occurs when the list or an item in the list changes.
        /// </summary>
        /// <remarks>ListChanged notifications for item value changes are only raised if the 
        /// list item type implements the INotifyPropertyChanged interface.</remarks> 
        public event ListChangedEventHandler PortCollectionListChanged;

        /// <summary>
        /// Occurs when the list or an item in the list changes.
        /// </summary>
        /// <remarks>ListChanged notifications for item value changes are only raised if the 
        /// list item type implements the INotifyPropertyChanged interface.</remarks> 
        /// <param name = "args">A <see cref = "System.ComponentModel.ListChangedEventArgs">System.ComponentModel.ListChangedEventArgs</see> that contains information about the event.</param>
        protected void OnPortCollectionListChanged(ListChangedEventArgs args)
        {
            PortCollectionListChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Occurs when the user Adds a new element to the port collection.
        /// </summary>
        /// <remarks>The event to be handles when the name of the PMC is changed.</remarks> 
        public event AddingNewEventHandler PortCollectionAddingNew;

        /// <summary>
        /// Occurs before an item is added to the list.
        /// </summary>
        /// <remarks>
        /// The AddingNew event occurs before a new object is added to the collection 
        /// represented by the Items property. This event is raised after the AddNew method is 
        /// called, but before the new item is created and added to the internal list. By 
        /// handling this event, the programmer can provide custom item creation and insertion 
        /// behavior without being forced to derive from the BindingList&gt;T&lt; class. 
        /// </remarks>
        /// <param name = "args">A <see cref = "System.ComponentModel.AddingNewEventArgs">System.ComponentModel.AddingNewEventArgs</see> that contains information about the event.</param>
        protected void OnPortCollectionAddingNew(AddingNewEventArgs args)
        {
            PortCollectionAddingNew?.Invoke(this, args);
        }


        /// <summary>
        /// Occurs when the user validates the unit operation.
        /// </summary>
        public event UnitOperationValidatedHandler UnitOperationValidated;
        /// <summary>
        /// Occurs when a unit operation is validated.
        /// </summary>
        /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
        /// <para>The <c>OnUnitOperationValidated</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
        /// technique for handling the event in a derived class.</para>
        /// <para>Notes to Inheritors: </para>
        /// <para>When overriding <c>OnUnitOperationValidated</c> in a derived class, be sure to call the base class's <c>OnUnitOperationValidated</c> method so that registered 
        /// delegates receive the event.</para>
        /// </remarks>
        /// <param name = "args">A <see cref = "UnitOperationValidatedEventArgs">UnitOperationValidatedEventArgs</see> that contains information about the event.</param>
        protected void OnUnitOperationValidated(UnitOperationValidatedEventArgs args)
        {
            UnitOperationValidated?.Invoke(this, args);
        }

        /// <summary>
        /// Occurs when the user begins the calculation of the unit operation.
        /// </summary>
        public event UnitOperationBeginCalculationHandler UnitOperationBeginCalculation;
        /// <summary>
        /// Occurs at the start of a unit operation calculation process.
        /// </summary>
        /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
        /// <para>The <c>OnUnitOperationBeginCalculation</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
        /// technique for handling the event in a derived class.</para>
        /// <para>Notes to Inheritors: </para>
        /// <para>When overriding <c>OnUnitOperationBeginCalculation</c> in a derived class, be sure to call the base class's <c>OnUnitOperationBeginCalculation</c> method so that registered 
        /// delegates receive the event.</para>
        /// </remarks>
        /// <param name = "message">A string that contains information about the the calculation.</param>
        protected void OnUnitOperationBeginCalculation(string message)
        {
            var args = new UnitOperationBeginCalculationEventArgs(ComponentName, message);
            UnitOperationBeginCalculation?.Invoke(this, args);
        }

        /// <summary>
        /// Occurs at the completion of a calculation of a unit operation.
        /// </summary>
        public event UnitOperationEndCalculationHandler UnitOperationEndCalculation;
        /// <summary>
        /// Occurs at the completion of a unit operation calculation process.
        /// </summary>
        /// <remarks><para>Raising an event invokes the event handler through a delegate.</para>
        /// <para>The <c>OnUnitOperationEndCalculation</c> method also allows derived classes to handle the event without attaching a delegate. This is the preferred 
        /// technique for handling the event in a derived class.</para>
        /// <para>Notes to Inheritors: </para>
        /// <para>When overriding <c>OnUnitOperationEndCalculation</c> in a derived class, be sure to call the base class's <c>OnUnitOperationBeginCalculation</c> method so that registered 
        /// delegates receive the event.</para>
        /// </remarks>
        /// <param name = "message">A string that contains information about the the calculation.</param>
        protected void OnUnitOperationEndCalculation(string message)
        {
            var args = new UnitOperationEndCalculationEventArgs(ComponentName, message);
            UnitOperationEndCalculation?.Invoke(this, args);
        }

        /// <summary>
        ///	The function that controls COM registration.  
        /// </summary>
        /// <remarks>
        ///	This function adds the registration keys specified in the CAPE-OPEN Method and
        /// Tools specifications. In particular, it indicates that this unit operation implements
        /// the CAPE-OPEN Unit Operation Category Identification. It also adds the CapeDescription
        /// registry keys using the <see cref ="CapeNameAttribute"/>, <see cref ="CapeDescriptionAttribute"/>, <see cref ="CapeVersionAttribute"/>
        /// <see cref ="CapeVendorUrlAttribute"/>, <see cref ="CapeHelpUrlAttribute"/>, 
        /// and <see cref ="CapeAboutAttribute"/> attributes.
        /// </remarks>
        /// <param name = "t">The type of the class being registered.</param> 
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        [ComRegisterFunction]
        public new static void RegisterFunction(Type t)
        {
            var assembly = t.Assembly;
            var versionNumber = (new AssemblyName(assembly.FullName)).Version.ToString();

            var keyName = string.Concat("CLSID\\{", t.GUID.ToString(), "}\\Implemented Categories");
            var catidKey = Registry.ClassesRoot.OpenSubKey(keyName, true);
            catidKey.CreateSubKey(CapeGuids.CapeOpenComponent_CATID);
            catidKey.CreateSubKey(CapeGuids.CapeUnitOperation_CATID);

            keyName = string.Concat("CLSID\\{", t.GUID.ToString(), "}\\InprocServer32");
            var key = Registry.ClassesRoot.OpenSubKey(keyName, true);
            var keys = key.GetSubKeyNames();
            foreach (var mt in keys)
            {
                if (mt == versionNumber)
                {
                    key.DeleteSubKey(mt);
                }
            }
            key.SetValue("CodeBase", assembly.CodeBase);
            key.Close();

            key = Registry.ClassesRoot.OpenSubKey(string.Concat("CLSID\\{", t.GUID.ToString(), "}"), true);
            keyName = string.Concat("CLSID\\{", t.GUID.ToString(), "}\\CapeDescription");

            var attributes = t.GetCustomAttributes(false);
            var nameInfoString = t.FullName;
            var descriptionInfoString = "";
            var versionInfoString = "";
            const string companyUrlInfoString = "";
            var helpUrlInfoString = "";
            var aboutInfoString = "";
            foreach (var mt in attributes)
            {
                switch (mt)
                {
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
                    case CapeDescriptionAttribute descriptionAttribute:
                        descriptionInfoString = descriptionAttribute.Description;
                        break;
                    case CapeVersionAttribute versionAttribute:
                        versionInfoString = versionAttribute.Version;
                        break;
                    case CapeVendorUrlAttribute vendorUrlAttribute:
                        versionInfoString = vendorUrlAttribute.VendorUrl;
                        break;
                    case CapeHelpUrlAttribute helpUrlAttribute:
                        helpUrlInfoString = helpUrlAttribute.HelpUrl;
                        break;
                    case CapeAboutAttribute aboutAttribute:
                        aboutInfoString = aboutAttribute.About;
                        break;
                }
            }
            catidKey.Close();
            key = Registry.ClassesRoot.CreateSubKey(keyName);
            key.SetValue("Name", nameInfoString);
            key.SetValue("Description", descriptionInfoString);
            key.SetValue("CapeVersion", versionInfoString);
            key.SetValue("ComponentVersion", versionNumber);
            key.SetValue("VendorURL", companyUrlInfoString);
            key.SetValue("HelpURL", helpUrlInfoString);
            key.SetValue("About", aboutInfoString);
            key.Close();
        }

        /// <summary>
        ///	This function controls the removal of the class from the COM registry when the class is unistalled.  
        /// </summary>
        /// <remarks>
        ///	The method will remove all subkeys added to the class' regristration, including the CAPE-OPEN
        /// specific keys added in the <see cref ="RegisterFunction"/> method.
        /// </remarks>
        /// <param name = "t">The type of the class being unregistered.</param> 
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        [ComUnregisterFunction]
        public new static void UnregisterFunction(Type t)
        {
            var keyName = string.Concat("CLSID\\{", t.GUID.ToString(), "}");
            var key = Registry.ClassesRoot.OpenSubKey(keyName, true);
            var keyNames = key.GetSubKeyNames();
            foreach (var t1 in keyNames)
            {
                key.DeleteSubKeyTree(t1);
            }
            var valueNames = key.GetValueNames();
            foreach (var t2 in valueNames)
            {
                key.DeleteValue(t2);
            }
        }

        /// <summary>
        ///	Displays the PMC graphic interface, if available.
        /// </summary>
        /// <remarks>
        /// <para>By default, this method throws a <see cref="CapeNoImplException">CapeNoImplException</see>
        /// that according to the CAPE-OPEN specification, is interpreted by the process
        /// modeling environment as indicating that the PMC does not have a editor 
        /// GUI, and the PME must perform editing steps.</para>
        /// <para>In order for a PMC to provide its own editor, the Edit method will
        /// need to be overridden to create a graphical editor. When the user requests the flowheet
        /// to show the editor, this method will be called to edit the unit. Overriden classes should
        /// not return a failure (throw and exception) as this will be interpreted by the flowsheeting 
        /// tool as the unit not providing its own editor.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        public override DialogResult Edit()
        {
            var editor = new BaseUnitEditor(this);
            editor.ShowDialog();
            return editor.DialogResult;
        }

        /// <summary>
        /// Gets the collection of unit operation ports.
        /// </summary>
        /// <remarks>
        /// <para>Return an interface to a collection containing the list of unit ports 
        /// (e.g. <see cref = "PortCollection"/>).</para>
        /// <para>Return the collection of unit ports (i.e. ICapeCollection). 
        /// These are delivered as a collection of elements exposing the interfaces 
        /// <see cref = "ICapeUnitPort"/>.</para>
        /// </remarks>
        /// <value>The port collection of the unit operation.</value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeFailedInitialisation">ECapeFailedInitialisation</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        //        [System.ComponentModel.EditorAttribute(typeof(capePortCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("ICapeUnit")]
        [Description("Unit Operation Port Collection. Click on the (...) button to edit collection.")]
        [TypeConverter(typeof(PortCollectionTypeConverter))]
        public PortCollection Ports { get; }

        /// <summary>
        /// Gets the flag to indicate the unit operation's validation status
        /// </summary>
        /// <remarks>
        /// <para> Get the flag that indicates whether the Flowsheet Unit is valid (e.g. some 
        /// parameter values have changed but they have not been validated by using 
        /// Validate). It has three possible values:</para>
        /// <list type="bullet"> 
        /// <item>notValidated(CAPE_NOT_VALIDATED)</item>
        /// <description>The unit’s validate() method has not 
        /// been called since the last operation that could have changed the validation 
        /// status of the unit, for example an update to a parameter value of a connection 
        /// to a port.</description>
        /// <item>invalid(CAPE_INVALID)</item>
        /// <description>The last time the unit’s validate() method was 
        /// called it returned false.</description>
        /// <item>valid(CAPE_VALID)</item>
        /// <description>The last time the unit’s validate() method was 
        /// called it returned true.</description>
        /// </list>
        /// </remarks>
        /// <value>A flag that indiciates the validation status of the unit operation.</value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
        /// <see cref= "CapeValidationStatus">CapeValidationStatus</see>.
        [Category("ICapeUnit")]
        [Description("Validation status of the unit. Either CAPE_VALID, CAPE_NOT_VALIDATED or CAPE_INVALID")]
        public virtual CapeValidationStatus ValStatus => _mValStatus;


        /// <summary>
        /// Gets the message returned during the last validation of the unit operation.
        /// </summary>
        /// <remarks>
        /// Gets the message that was retured fromt he last attempt to validate the Flowsheet Unit (e.g. some 
        /// parameter values have changed but they have not been validated by using 
        /// Validate). 
        /// </remarks>
        /// <value>The message returned during the last validation of the unit operation.</value>
        /// <see cref= "CapeValidationStatus">CapeValidationStatus</see>.
        [Category("ICapeUnit")]
        [Description("Validation message of the unit.")]
        public virtual string ValidationMessage => MValidationMessage;

        /// <summary>
        ///	Executes the necessary calculations involved in the unit operation model.
        /// </summary>
        /// <remarks>
        /// <para>This method is called by the PME to execute the calculation of the unit operation. The calculation process
        /// first fires the <see cref = "UnitOperationBeginCalculation" /> event. After the event is fired, the 
        /// <see cref = "OnCalculate"/> method is called. Derived classes must implement the 
        /// <see cref = "OnCalculate"/> method. After the unit has completed its calculation, this method fires the 
        /// <see cref = "UnitOperationEndCalculation"/> event.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
        /// <exception cref = "ECapeTimeOut">ECapeTimeOut</exception>
        /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
        /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
        void ICapeUnitCOM.Calculate()
        {
            OnUnitOperationBeginCalculation("Starting Calculation");
            OnCalculate();
            OnUnitOperationEndCalculation("Calculation completed normally.");
        }


        /// <summary>
        ///	Executes the necessary calculations involved in the unit operation model.
        /// </summary>
        /// <remarks>
        /// <para>This method is called by the PME to execute the calculation of the unit operation. The calculation process
        /// first fires the <see cref = "UnitOperationBeginCalculation" /> event. After the event is fired, the 
        /// <see cref = "OnCalculate"/> method is called. Derived classes must implement the 
        /// <see cref = "OnCalculate"/> method. After the unit has completed its calculation, this method fires the 
        /// <see cref = "UnitOperationEndCalculation"/> event.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
        /// <exception cref = "ECapeTimeOut">ECapeTimeOut</exception>
        /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
        /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
        void ICapeUnit.Calculate()
        {
            OnUnitOperationBeginCalculation("Starting Calculation");
            OnCalculate();
            OnUnitOperationEndCalculation("Calculation completed normally.");
        }

        /// <summary>
        ///	Executes the necessary calculations involved in the unit operation model.
        /// </summary>
        /// <remarks>
        /// <para>This method is called by the PME to execute the calculation of the unit operation. The calculation process
        /// first fires the <see cref = "UnitOperationBeginCalculation" /> event. After the event is fired, the 
        /// <see cref = "OnCalculate"/> method is called. Derived classes must implement the 
        /// <see cref = "OnCalculate"/> method. After the unit has completed its calculation, this method fires the 
        /// <see cref = "UnitOperationEndCalculation"/> event.</para>
        /// </remarks>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        /// <exception cref = "ECapeOutOfResources">ECapeOutOfResources</exception>
        /// <exception cref = "ECapeTimeOut">ECapeTimeOut</exception>
        /// <exception cref = "ECapeSolvingError">ECapeSolvingError</exception>
        /// <exception cref = "ECapeLicenceError">ECapeLicenceError</exception>
        private void OnCalculate()
        {
            OnUnitOperationBeginCalculation("Starting Calculation");
            Calculate();
            OnUnitOperationEndCalculation("Calculation completed normally.");
        }

        /// <summary>
        ///	This method is called by the Calculate method to perform necessary calculations involved in the 
        ///	unit operation model.
        /// </summary>
        /// <remarks>
        /// <para>The Flowsheet Unit performs its calculation, that is, computes the variables 
        /// that are missing at this stage in the complete description of the input and 
        /// output streams and computes any public parameter value that needs to be 
        /// displayed. OnCalculate will be able to do progress monitoring and checks for 
        /// interrupts as required using the simulation context. At present, there are no
        /// standards agreed for this.</para>
        /// <para>It is recommended that Flowsheet Units perform a suitable flash 
        /// calculation on all output streams. In some cases a Simulation Executive will 
        /// be able to perform a flash calculation but the writer of a Flowsheet Unit is 
        /// in the best position to decide the correct flash to use.</para>
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
        protected abstract void Calculate();

        /// <summary>
        /// Validates the unit operation. 
        /// </summary>
        /// <remarks>
        /// <para>Sets the flag that indicates whether the Flowsheet Unit is valid by 
        /// validating the ports and parameters of the Flowsheet Unit. For example, this 
        /// method could check that all mandatory ports have connections and that the 
        /// values of all parameters are within bounds.</para>
        /// <para>Note that the Simulation Executive can call the Validate routine at any 
        /// time, in particular it may be called before the executive is ready to call 
        /// the Calculate method. This means that Material Objects connected to unit ports 
        /// may not be correctly configured when Validate is called. The recommended approach 
        /// is for this method to validate parameters and ports but not Material Object 
        /// configuration. A second level of validation to check Material Objects can be
        /// implemented as part of Calculate, when it is reasonable to expect that the 
        /// Material Objects connected to ports will be correctly configured. </para>
        /// <para>The base-class implementation of this method traverses the port and 
        /// parameter collections and calls the  <see cref = "Validate"/> method of each 
        /// member. The unit is valid if all port and parameters are valid, which is 
        /// signified by the Validate method returning <c>true</c>.</para>
        /// </remarks>
        /// <returns>
        /// <para>true, if the unit is valid.</para>
        /// <para>false, if the unit is not valid.</para>
        /// </returns>
        /// <param name = "message">Reference to a string that will conain a message regarding the validation of the parameter.</param>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeBadCOParameter">ECapeBadCOParameter</exception>
        /// <exception cref = "ECapeBadInvOrder">ECapeBadInvOrder</exception>
        public override bool Validate(ref string message)
        {
            // m_dirty = true;
            UnitOperationValidatedEventArgs args;
            if (!base.Validate(ref message))
            {
                args = new UnitOperationValidatedEventArgs(ComponentName, message, _mValStatus, CapeValidationStatus.CAPE_INVALID);
                _mValStatus = CapeValidationStatus.CAPE_INVALID;
                OnUnitOperationValidated(args);
                MValidationMessage = message;
                return false;
            }
            foreach (var mt in Ports)
            {
                if (mt.connectedObject != null) continue;
                message = string.Concat("Port ", ((CapeIdentification)mt).ComponentName, " does not have a connected object.");
                MValidationMessage = message;
                args = new UnitOperationValidatedEventArgs(ComponentName, message, _mValStatus, CapeValidationStatus.CAPE_INVALID);
                _mValStatus = CapeValidationStatus.CAPE_INVALID;
                OnUnitOperationValidated(args);
                return false;
            }
            message = "Unit is valid.";
            MValidationMessage = message;
            args = new UnitOperationValidatedEventArgs(ComponentName, message, _mValStatus, CapeValidationStatus.CAPE_INVALID);
            _mValStatus = CapeValidationStatus.CAPE_VALID;
            OnUnitOperationValidated(args);
            return true;
        }

        /// <summary>
        ///	Gets the list of possible reports for the unit operation.
        /// </summary>
        /// <remarks>
        ///	Gets the list of possible reports for the unit operation.
        /// </remarks>
        /// <value>
        ///	The list of possible reports for the unit operation.
        /// </value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        [Category("ICapeUnitReport")]
        [Description("Reports available for the unit.")]
        public virtual List<string> Reports => _mReports;

        /// <summary>
        ///	Gets and sets the current active report for the unit operation.
        /// </summary>
        /// <remarks>
        ///	Gets and sets the current active report for the unit operation.
        /// </remarks>
        /// <value>
        ///	The current active report for the unit operation.
        /// </value>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        /// <exception cref = "ECapeInvalidArgument">To be used when an invalid argument value is passed, for example, an unrecognised Compound identifier or UNDEFINED for the props argument.</exception>
        [TypeConverter(typeof(SelectedReportConverter))]
        [Description("Name of the report generated by the unit.")]
        [Category("ICapeUnitReport")]
        public virtual string selectedReport
        {
            get => _mSelectReport;
            set => _mSelectReport = value;
        }

        /// <summary>
        ///	Produces the active report for the unit operation.
        /// </summary>
        /// <remarks>
        ///	Produce the designated report. If no value has been set, it produces the default report.
        /// </remarks>
        /// <returns>String containing the text for the currently selected report.</returns>
        /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
        /// <exception cref = "ECapeNoImpl">ECapeNoImpl</exception>
        public virtual string ProduceReport()
        {
            var retVal = string.Empty;
            var validMessage = string.Empty;
            var valid = Validate(ref validMessage);
            var status = ValStatus;
            retVal = string.Concat(retVal, "Unit Operation: ", ComponentName, Environment.NewLine);
            retVal = string.Concat(retVal, "   Description: ", ComponentDescription, Environment.NewLine, Environment.NewLine);
            retVal = string.Concat(retVal, "Validation Status: ", ValStatus.ToString(), Environment.NewLine);
            retVal = string.Concat(retVal, "                   ", validMessage, Environment.NewLine, Environment.NewLine);
            retVal = string.Concat(retVal, "Parameters: ", Environment.NewLine);

            for (var i = 0; i < Parameters.Count; i++)
            {
                var pParam = (ICapeParameter)Parameters[i];
                var pParamId = (CapeIdentification)pParam;
                retVal = string.Concat(retVal, "     Parameter ", i.ToString(), ": ", pParamId.ComponentName, Environment.NewLine);
                retVal = string.Concat(retVal, "     Desription: ", pParamId.ComponentDescription, Environment.NewLine);
                var pSpec = (ICapeParameterSpec)pParam.Specification;
                retVal = string.Concat(retVal, "     Type: ", pSpec.Type.ToString(), Environment.NewLine);
                valid = pParam.Validate(validMessage);
                retVal = string.Concat(retVal, "     Validation Status: ", pParam.ValStatus.ToString(), Environment.NewLine);
                retVal = string.Concat(retVal, "          ", validMessage, Environment.NewLine);
                switch (pSpec.Type)
                {
                    case CapeParamType.CAPE_ARRAY:
                    {
                        retVal = string.Concat(retVal, "     Values:", Environment.NewLine);
                        object[] values;
                        if (pParam.value is object[])
                        {
                            values = (object[])pParam.value;
                            foreach (var mt in values)
                            {
                                ICapeParameter p_ParamArrayElement;
                                if (mt is ICapeParameter)
                                {
                                    p_ParamArrayElement = (ICapeParameter)mt;
                                    retVal = string.Concat(retVal, string.Concat("          ", p_ParamArrayElement.value.ToString(), Environment.NewLine));
                                }
                                else
                                {
                                    retVal = string.Concat(retVal, string.Concat("          ", mt.ToString(), Environment.NewLine));
                                }
                            }
                        }

                        break;
                    }
                    case CapeParamType.CAPE_REAL:
                    {
                        var p_Real = (ICapeRealParameterSpecCOM)pSpec;
                        RealParameter p_RealParam;
                        if (pParam is RealParameter)
                        {
                            p_RealParam = (RealParameter)pParam;
                            retVal = string.Concat(retVal, "     Value: ", p_RealParam.DimensionedValue.ToString(), Environment.NewLine);
                            retVal = string.Concat(retVal, "     Dimensionality: ", p_RealParam.Unit, Environment.NewLine);
                        }
                        else
                        {
                            retVal = string.Concat(retVal, "     Value: ", pParam.value.ToString(), Environment.NewLine);
                            retVal = string.Concat(retVal, "     Dimensionality: ", pSpec.Dimensionality.ToString(), Environment.NewLine);
                        }
                        retVal = string.Concat(retVal, "     Default Value: ", p_Real.DefaultValue, Environment.NewLine);
                        retVal = string.Concat(retVal, "     Lower Bound: ", p_Real.LowerBound, Environment.NewLine);
                        retVal = string.Concat(retVal, "     Upper Bound: ", p_Real.UpperBound, Environment.NewLine);
                        break;
                    }
                    case CapeParamType.CAPE_INT:
                    {
                        retVal = string.Concat(retVal, "     Value: ", pParam.value.ToString(), Environment.NewLine);
                        var p_IntParam = (ICapeIntegerParameterSpec)pSpec;
                        retVal = string.Concat(retVal, "     Default Value: ", p_IntParam.DefaultValue, Environment.NewLine);
                        retVal = string.Concat(retVal, "     Lower Bound: ", p_IntParam.LowerBound, Environment.NewLine);
                        retVal = string.Concat(retVal, "     Upper Bound: ", p_IntParam.UpperBound, Environment.NewLine);
                        break;
                    }
                    case CapeParamType.CAPE_BOOLEAN:
                    {
                        retVal = string.Concat(retVal, "     Value: ", pParam.value.ToString(), Environment.NewLine);
                        var p_Bool = (ICapeBooleanParameterSpec)pSpec;
                        retVal = string.Concat(retVal, "     Default Value: ", p_Bool.DefaultValue, Environment.NewLine);
                        break;
                    }
                    case CapeParamType.CAPE_OPTION:
                    {
                        retVal = string.Concat(retVal, "     Value: ", pParam.value.ToString(), Environment.NewLine);
                        var p_Opt = (ICapeOptionParameterSpec)pSpec;
                        retVal = string.Concat(retVal, "     Default Value: ", p_Opt.DefaultValue, Environment.NewLine);
                        if (p_Opt.RestrictedToList)
                        {
                            retVal = string.Concat(retVal, "     Restricted to List: TRUE", Environment.NewLine);
                        }
                        else
                        {
                            retVal = string.Concat(retVal, "     Restricted to List: FALSE", Environment.NewLine);
                        }
                        retVal = string.Concat(retVal, "     Option List Values: ", Environment.NewLine);
                        var options = (string[])p_Opt.OptionList;
                        for (var j = 0; j < options.Length; j++)
                        {
                            retVal = string.Concat(retVal, "          Option[", j, "]: ", options[j], Environment.NewLine);
                        }

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                retVal = string.Concat(retVal, Environment.NewLine);
            }
            retVal = string.Concat(retVal, Environment.NewLine, "Ports: ", Environment.NewLine);
            for (var i = 0; i < Ports.Count; i++)
            {
                var pPort = (ICapeUnitPort)Ports[i];
                var pPortId = (CapeIdentification)pPort;
                retVal = string.Concat(retVal, "     Port ", i.ToString(), ": ", pPortId.ComponentName, Environment.NewLine);
                retVal = string.Concat(retVal, "     Description:  ", pPortId.ComponentDescription, Environment.NewLine);
                var pPortConnectedObjectId = (ICapeIdentification)pPort.connectedObject;
                retVal = string.Concat(retVal, "     Port Type: ", pPort.portType.ToString(), Environment.NewLine);
                retVal = string.Concat(retVal, "     Port Direction: ", pPort.direction.ToString(), Environment.NewLine);
                retVal = pPortConnectedObjectId != null ? string.Concat(retVal, "     Connected Object:  ", pPortConnectedObjectId.ComponentName, Environment.NewLine) : string.Concat(retVal, "     No Connected Object", Environment.NewLine);
                retVal = string.Concat(retVal, Environment.NewLine);
            }
            return retVal;
        }
    }
}
