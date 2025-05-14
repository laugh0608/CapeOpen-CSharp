// 大白萝卜重构于 2025.05.14，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>
/// The unit selector class provides a graphical user interface (GUI) for the <see cref="UnitOperationWrapper"/> class.
/// </summary>
[ClassInterface(ClassInterfaceType.None)]
public partial class UnitSelector : Form
{
    private Type _mUnit;
    private List<string> _addedTypes = [];

    /// <summary>
    /// Creates a new instance of the <see cref="UnitSelector"/> class.
    /// </summary>
    public UnitSelector()
    {
        InitializeComponent();


        DialogResult = DialogResult.Cancel;
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var directory = assembly.Location;
        var types = assembly.GetTypes();
        var node = new TreeNode("Example Unit");
        treeView1.Nodes.Add(node);
        foreach (var type in types)
        {
            if (type.GUID != new Guid("B41DECE0-6C99-4CA4-B0EB-EFADBDCE23E9"))
                AddType(type, node);
        }
        if (System.Diagnostics.Debugger.IsAttached)
        {
            //System.Windows.Forms.MessageBox.Show("This option is currently not supported.");
            node = new TreeNode("Debugged Unit");
            treeView1.Nodes.Add(node);
            var obj = CapePInvoke.Com.GetActiveObject("VisualStudio.DTE.10.0");
            var dte = (CapePInvoke.EnvDte.Dte)obj;
            try
            {
                var myProject = (CapePInvoke.EnvDte.MyProject)dte.Solution.Projects.Item(1);
                var name = myProject.FullName;
                if (name != "")
                {
                    var path = string.Concat(Path.GetDirectoryName(name), "\\bin\\debug");
                    if (Directory.Exists(path))
                    {
                        var files = Directory.GetFiles(path);
                        foreach (var file in files)
                        {
                            var dll = file.Remove(0, file.Length - 3);
                            if (dll.ToLower() != "dll") continue;
                            var assembly2 = System.Reflection.Assembly.LoadFrom(file);
                            var types2 = assembly2.GetTypes();
                            foreach (var type in types2)
                            {
                                if (type.GUID != new Guid("883D46FE-5713-424C-BF10-7ED34263CD6D")
                                    && type.GUID != new Guid("56E8FDFD-2000-4264-9B47-745B26BE0EC9")
                                    && type.GUID != new Guid("B41DECE0-6C99-4CA4-B0EB-EFADBDCE23E9")
                                   )
                                    AddType(type, node);
                            }
                        }
                    }
                    path = string.Concat(Path.GetDirectoryName(name), "\\debug");
                    if (Directory.Exists(path))
                    {
                        var files = Directory.GetFiles(path);
                        foreach (var file in files)
                        {
                            var dll = file.Remove(0, file.Length - 3);
                            if (!dll.Equals("dll", StringComparison.CurrentCultureIgnoreCase)) continue;
                            var assembly2 = System.Reflection.Assembly.LoadFrom(file);
                            var types2 = assembly2.GetTypes();
                            foreach (var type in types2)
                            {
                                if (type.GUID != new Guid("883D46FE-5713-424C-BF10-7ED34263CD6D")
                                    && type.GUID != new Guid("56E8FDFD-2000-4264-9B47-745B26BE0EC9")
                                    && type.GUID != new Guid("B41DECE0-6C99-4CA4-B0EB-EFADBDCE23E9")
                                   )
                                    AddType(type, node);
                            }
                        }
                    }
                }
            }
            catch (Exception pEx)
            {
                MessageBox.Show(pEx.Message);
            }
        }
        node = null;
        var CapeOpenDirectory = string.Concat(Environment.GetEnvironmentVariable("CommonProgramFiles"), "\\CAPE-OPEN Objects");
        if (Directory.Exists(CapeOpenDirectory))
        {
            node = new TreeNode("CapeOpen Units");
            treeView1.Nodes.Add(node);
            AddPath(CapeOpenDirectory, node);
        }
        CapeOpenDirectory = "C:\\CAPE-OPEN Objects";
        if (!Directory.Exists(CapeOpenDirectory)) return;
        if (node == null)
        {
            node = new TreeNode("CapeOpen Units");
            treeView1.Nodes.Add(node);
        }
        AddPath(CapeOpenDirectory, node);
    }

    /// <summary>
    /// The type of the unit operation to be created.
    /// </summary>
    /// <value>The type of the unit operation to be created.</value>
    public Type Unit => _mUnit;

    private static void AddType(Type type, TreeNode node)
    {
        if (type.IsAbstract) return;
        if (!typeof(ICapeUnit).IsAssignableFrom(type)) return;
        if (type.FullName == "CapeOpen.UnitOperationManager") return;
        var assembly = type.Assembly;
        var versionNumber = (new System.Reflection.AssemblyName(assembly.FullName)).Version.ToString();
        var pUnit = Activator.CreateInstance(type);
        var pId = (ICapeIdentification)pUnit;
        var attributes = type.GetCustomAttributes(false);
        var nameInfoString = type.FullName;
        var descriptionInfoString = "";
        var versionInfoString = "";
        var companyUrlInfoString = "";
        var helpUrlInfoString = "";
        var aboutInfoString = "";
        var consumesThermo = false;
        var thermo10 = false;
        var thermo11 = false;
        foreach (var mt in attributes)
        {
            switch (mt)
            {
                case CapeConsumesThermoAttribute:
                    consumesThermo = true;
                    break;
                case CapeSupportsThermodynamics10Attribute:
                    thermo10 = true;
                    break;
                case CapeSupportsThermodynamics11Attribute:
                    thermo11 = true;
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
                    companyUrlInfoString = vendorUrlAttribute.VendorUrl;
                    break;
                case CapeHelpUrlAttribute helpUrlAttribute:
                    helpUrlInfoString = helpUrlAttribute.HelpUrl;
                    break;
                case CapeAboutAttribute aboutAttribute:
                    aboutInfoString = aboutAttribute.About;
                    break;
            }
        }

        var newNode = new TreeNode(nameInfoString);
        node.Nodes.Add(newNode);
        newNode.Tag = type;
        newNode.Nodes.Add(string.Concat("Description: ", descriptionInfoString));
        newNode.Nodes.Add(string.Concat("CapeVersion: ", versionInfoString));
        newNode.Nodes.Add(string.Concat("ComponentVersion: ", versionNumber));
        newNode.Nodes.Add(string.Concat("VendorURL: ", companyUrlInfoString));
        newNode.Nodes.Add(string.Concat("HelpURL: ", helpUrlInfoString));
        newNode.Nodes.Add(string.Concat("About: ", aboutInfoString));
        if (!consumesThermo) return;
        var thermoNode = new TreeNode("Supported Thermo Versions");
        newNode.Nodes.Add(thermoNode);
        if (thermo10) thermoNode.Nodes.Add("Supports Thermo 1.0");
        if (thermo11) thermoNode.Nodes.Add("Supports Thermo 1.1");
    }

    private static void AddPath(string path, TreeNode node)
    {
        if (!Directory.Exists(path)) return;
        var directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            var name = directory.Remove(0, path.Length + 1);
            var newNode = new TreeNode(name);
            node.Nodes.Add(newNode);
            AddPath(directory, newNode);
        }
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            var dll = file.Remove(0, file.Length - 3);
            if (!dll.Equals("dll", StringComparison.CurrentCultureIgnoreCase)) continue;
            var assembly = System.Reflection.Assembly.LoadFrom(file);
            Type[] types = null;
            types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.GUID != new Guid("883D46FE-5713-424C-BF10-7ED34263CD6D")
                    && type.GUID != new Guid("56E8FDFD-2000-4264-9B47-745B26BE0EC9")
                    && type.GUID != new Guid("B41DECE0-6C99-4CA4-B0EB-EFADBDCE23E9")
                   )
                    AddType(type, node);
            }
        }
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var fileDialog = new OpenFileDialog();
        fileDialog.Title = "Browse to Assebly Containing Desired Unit Operation";
        fileDialog.ShowDialog();
        var fileName = fileDialog.FileName;
        var file = Path.GetFileNameWithoutExtension(fileName);
        var node = new TreeNode(string.Concat("Units from File: ", file));
        treeView1.Nodes.Add(node);
        var dll = fileName.Remove(0, fileName.Length - 3);
        if (!dll.Equals("dll", StringComparison.CurrentCultureIgnoreCase)) return;
        var assembly = System.Reflection.Assembly.LoadFrom(fileName);
        Type[] types = null;
        types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (type.GUID != new Guid("883D46FE-5713-424C-BF10-7ED34263CD6D")
                && type.GUID != new Guid("56E8FDFD-2000-4264-9B47-745B26BE0EC9")
                && type.GUID != new Guid("B41DECE0-6C99-4CA4-B0EB-EFADBDCE23E9")
               )
                AddType(type, node);
        }
    }

    private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        throw new CapeUnknownException("Unit Creation Cancelled.");
        // this.Close();
    }

    private void OKbutton_Click(object sender, EventArgs e)
    {
        var node = treeView1.SelectedNode;
        _mUnit = (Type)node.Tag;
        if (node.Parent == null) return;
        _mUnit ??= (Type)node.Parent.Tag;
        if (_mUnit == null) return;
        DialogResult = DialogResult.OK;
        Close();
    }
}