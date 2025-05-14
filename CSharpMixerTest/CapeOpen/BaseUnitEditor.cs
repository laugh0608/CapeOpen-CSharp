// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>
/// Base class for a unit operation editor.
/// </summary>
/// <remarks> This editor can be used by all unit operations. It creates a tabbed form 
/// that exposes the unit's properties (parameters and ports) on the first tab. This 
/// editor can be inherited and tabs added to the tab control to customize the form
/// for a unit operation.
/// </remarks>
[ClassInterface(ClassInterfaceType.None)]
public partial class BaseUnitEditor : Form
{
    private CapeUnitBase _mUnit;

    /// <remarks>
    /// Constructor for a standard unit operation editor.
    /// </remarks>
    /// <param name = "unit">The unit operation to be edited.</param>
    public BaseUnitEditor(CapeUnitBase unit)
    {
        InitializeComponent();
        _mUnit = unit;
        propertyGrid1.SelectedObject = unit;
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
