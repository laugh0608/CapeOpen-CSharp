using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapeOpen
{
    /// <summary>
    /// BaseUnitEditor.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUnitEditor : Window
    {
        //public BaseUnitEditor()
        //{
        //    InitializeComponent();
        //}
        private CapeUnitBase m_Unit;

        /// <summary>
        /// Constructor for a standard unit operation editor using WPF.
        /// </summary>
        /// <param name="unit">The unit operation to be edited.</param>
        public BaseUnitEditor(CapeUnitBase unit)
        {
            InitializeComponent(); // Initializes components defined in XAML
            m_Unit = unit;

            // Bind the CapeUnitBase object to the PropertyGrid
            // The specific property to set might vary slightly depending on the PropertyGrid library
            // DataContext or SelectedObject are common. Let's use SelectedObject as it mirrors the original.
            this.propertyGrid1.SelectedObject = m_Unit;

            // Optional: Set DataContext if you want to bind other controls directly to the unit object
            // this.DataContext = m_Unit;
        }

        /// <summary>
        /// Handles the Click event of the OK button.
        /// </summary>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Set the DialogResult to true to indicate success (like DialogResult.OK)
            this.DialogResult = true;

            // Close the window
            this.Close();
        }

        // If you added a Cancel button with IsCancel="True" in XAML,
        // clicking it or pressing ESC will automatically set DialogResult to false
        // and close the window, without needing an explicit event handler.
        // If you implement a Cancel button manually, its click handler would do:
        // private void CancelButton_Click(object sender, RoutedEventArgs e)
        // {
        //     this.DialogResult = false;
        //     this.Close();
        // }
    }
}
