using System.Data;
using System.Runtime.InteropServices;
using System.Xml;
using CapeOpen.Properties;

namespace CapeOpen;

/// <summary>
/// Summary for WAR
/// </summary>
[Serializable,ComVisible(true)]
[Guid(CapeOpenGuids.PpWarAddInIid)]  // "0BE9CCFD-29B4-4a42-B34E-76F5FE9B6BB4"
[CapeName("WAR Add-In")]
[CapeAbout("Waste Reduction Algorithm Add-in")]
[CapeDescription("Waste Reduction Algorithm Add-in")]
[CapeVersion("1.0")]
[CapeVendorUrl("https://www.epa.gov/nrmrl/std/sab/war/sim_war.htm")]
[ClassInterface(ClassInterfaceType.None)]
[CapeFlowsheetMonitoring(true)]
public class WarAddIn : CapeObjectBase
{

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
        return new WarAddIn();
    }

    private DataTable _warDataTable;
    private void AddData(string xmlData)
    {
        var document = new XmlDocument();
        document.LoadXml(xmlData);
        var list = document.SelectNodes("dataroot/data");
        var iEnum = list.GetEnumerator();
        using var piEnum = iEnum as IDisposable;
        while (iEnum.MoveNext())
        {
            var current = (XmlNode)iEnum.Current;
            var dataRow = _warDataTable.NewRow();
            _warDataTable.Rows.Add(dataRow);
            var childEnum = current.ChildNodes.GetEnumerator();
            using var pChildEnum = childEnum as IDisposable;
            while (childEnum.MoveNext())
            {
                var currentChild = (XmlNode)childEnum.Current;
                switch (currentChild.Name)
                {
                    case "Mol_ID":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Mol Id"] = value;
                        break;
                    }
                    case "DIPPR ID":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["DIPPR ID"] = value;
                        break;
                    }
                    case "ASPENID":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["ASPENID"] = value;
                        break;
                    }
                    case "ChemicalName":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["ChemicalName"] = value;
                        break;
                    }
                    case "CAS":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["CAS"] = value;
                        break;
                    }
                    case "Formula":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Formula"] = value;
                        break;
                    }
                    case "CLASS":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["class"] = value;
                        break;
                    }
                    case "MW":
                        dataRow["molecularWeight"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "Rat_LD50_Value":
                        dataRow["Rat LD50"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "Rat_LD50_Notes":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Rat LD50 Notes"] = value;
                        break;
                    }
                    case "Rat_LD50_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Rat LD50 Source"] = value;
                        break;
                    }
                    case "OSHA_TWA_Value":
                        dataRow["OSHA PEL"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "OSHA_TWA_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["OSHA Source"] = value;
                        break;
                    }
                    case "OSHA_TWA_Notes":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["OSHA Notes"] = value;
                        break;
                    }
                    case "FHM_LC50_Value":
                        dataRow["Fathead LC50"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "FHM_LC50_Notes":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Fathead LC50 Notes"] = value;
                        break;
                    }
                    case "FHM_LC50_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Fathead LC50 Source"] = value;
                        break;
                    }
                    case "PCO_Value":
                        dataRow["Photochemical Oxidation Potential"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "PCO_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Photochemical Oxidation Potential Source"] = value;
                        break;
                    }
                    case "GWP_Value":
                        dataRow["Global Warming Potential"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "GWP_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Global Warming Potential Source"] = value;
                        break;
                    }
                    case "OD_Value":
                        dataRow["Ozone Depletion Potential"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "OD_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Ozone Depletion Potential Source"] = value;
                        break;
                    }
                    case "AP_Value":
                        dataRow["Acidification Potential"] = Convert.ToDouble(currentChild.InnerText);
                        break;
                    case "AP_Source":
                    {
                        var value = currentChild.InnerText;
                        while (value.StartsWith(' ')) value = value[1..];
                        while (value.EndsWith(' ')) value = value.Remove(value.Length - 1);
                        dataRow["Acidification Potential Source"] = value;
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    ///	Displays the PMC graphic interface, if available.
    /// </summary>
    /// <remarks>
    /// The PMC displays its user interface and allows the Flowsheet User to 
    /// interact with it. If no user interface is available it returns an error.
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public WarAddIn()
    {
        _warDataTable = new DataTable();
        _warDataTable.Columns.Add("Mol Id", typeof(string));
        _warDataTable.Columns.Add("DIPPR ID", typeof(string));
        _warDataTable.Columns.Add("ASPENID", typeof(string));
        _warDataTable.Columns.Add("ChemicalName", typeof(string));
        _warDataTable.Columns.Add("CAS", typeof(string));
        _warDataTable.Columns.Add("Formula", typeof(string));
        _warDataTable.Columns.Add("class", typeof(string));
        _warDataTable.Columns.Add("molecularWeight", typeof(double));
        _warDataTable.Columns.Add("Rat LD50", typeof(double));
        _warDataTable.Columns.Add("Rat LD50 Notes", typeof(string));
        _warDataTable.Columns.Add("Rat LD50 Source", typeof(string));
        _warDataTable.Columns.Add("OSHA PEL", typeof(double));
        _warDataTable.Columns.Add("OSHA Notes", typeof(string));
        _warDataTable.Columns.Add("OSHA Source", typeof(string));
        _warDataTable.Columns.Add("Fathead LC50", typeof(double));
        _warDataTable.Columns.Add("Fathead LC50 Notes", typeof(string));
        _warDataTable.Columns.Add("Fathead LC50 Source", typeof(string));
        _warDataTable.Columns.Add("Global Warming Potential", typeof(double));
        _warDataTable.Columns.Add("Global Warming Potential Source", typeof(string));
        _warDataTable.Columns.Add("Ozone Depletion Potential", typeof(double));
        _warDataTable.Columns.Add("Ozone Depletion Potential Source", typeof(string));
        _warDataTable.Columns.Add("Photochemical Oxidation Potential", typeof(double));
        _warDataTable.Columns.Add("Photochemical Oxidation Potential Source", typeof(string));
        _warDataTable.Columns.Add("Acidification Potential", typeof(double));
        _warDataTable.Columns.Add("Acidification Potential Source", typeof(string));
        var domain = AppDomain.CurrentDomain;
        //System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        //String[] resources = myAssembly.GetManifestResourceNames();
        //System.IO.Stream resStream = myAssembly.GetManifestResourceStream("CapeOpen.Resources.WARdata.xml.resources");
        //System.Resources.ResourceReader resReader = new System.Resources.ResourceReader(resStream);
        //System.Collections.IDictionaryEnumerator en = resReader.GetEnumerator();
        //String temp = String.Empty;
        //while (en.MoveNext())
        //{
        //    if (en.Key.ToString() == "WARdata") temp = en.Value.ToString();
        //}
        AddData(Resources.WARdata);
    }

    /// <summary>
    ///	Displays the PMC graphic interface, if available.
    /// </summary>
    /// <remarks>
    /// The PMC displays its user interface and allows the Flowsheet User to 
    /// interact with it. If no user interface is available it returns an error.
    /// </remarks>
    /// <exception cref ="ECapeUnknown">The error to be raised when other error(s),  specified for this operation, are not suitable.</exception>
    public override DialogResult Edit()
    {
        try
        {
            var war = new WARalgorithm(_warDataTable, FlowsheetMonitoring);
            var result = war.ShowDialog();
            war.Dispose();
            return result;
        }
        catch (Exception pEx)
        {
            ThrowException(new CapeUnknownException(pEx.Message, pEx));
            return DialogResult.Cancel;
        }
    }
}