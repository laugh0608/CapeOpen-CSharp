// 大白萝卜重构于 2025.05.11，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.Collections;
using System.Globalization;
using System.Xml;

namespace CapeOpen;

/// <summary>提供与参数相关联的测量单位的相关信息。</summary>
/// <remarks>计量单位可以是国际单位制（SI）或习惯单位。每个单位都被分配到一个具有与单位维度信息相关的 <see cref="UnitCategory"/>。</remarks>
internal struct UnitSet
{
    /// <summary>计量单位的名称。</summary>
    /// <remarks>计量单位的通用名称。通常，该字段表示单位的缩写。</remarks>
    public string Name;

    /// <summary>计量单位的描述。</summary>
    /// <remarks>计量单位的描述。</remarks>
    public string Description;

    /// <summary>计量单位的类别。</summary>
    /// <remarks>度量单位的类别定义了单位的维度。参数的维度表示该参数的物理维度轴。
    /// 预期维度必须覆盖至少6个基本轴 (length, mass, time, angle, temperature and charge)。
    /// 可能的实现方式可以是一个具有固定长度的数组向量，其中包含每个基本 SI 单位的指数，遵循 SI 手册的指示。(from http://www.bipm.fr/)
    /// So if we agree on order (m kg s A K) ... velocity would be (1,0,-1,0,0,0): 
    /// that is m1 * s-1 =m/s. We have suggested to the  CO Scientific Committee to use the SI base units plus the 
    /// SI derived units with special symbols (for a better usability and for allowing the definition of angles).</remarks>
    public string Category;

    /// <summary>转换因子，用于将测量值乘以该因子以将其转换为 SI 等效单位。</summary>
    /// <remarks>将单位转换为和从单位类别的 SI 等效值进行转换。通过首先将存储在 <see cref="ConversionPlus"/> 中的
    /// 任何偏移量添加到单位值，然后将该总和乘以单位 <see cref="ConversionTimes"/> 的值，即可获得以 SI 单位表示的测量值。</remarks>
    public double ConversionTimes;

    /// <summary>用于将测量值转换为 SI 等效值的偏移系数。</summary>
    /// <remarks>将单位转换为和从单位类别的 SI 等效值进行转换。通过首先将存储在 <see cref="ConversionPlus"/> 中的
    /// 任何偏移量添加到单位值，然后将该总和乘以单位 <see cref="ConversionTimes"/> 的值，即可获得以 SI 单位表示的测量值。</remarks>
    public double ConversionPlus;
}

internal struct UnitCategory
{
    /// <summary>获取单位类别的名称，例如压力、温度.</summary>
    /// <remarks>单位类别代表维度的唯一组合 (mass, length, time, temperature, 物质的量 (mole), 电流 (electrical current), luminosity)
    /// 与特定的度量单位相关联。</remarks>
    public string Name;

    /// <summary>获取参数的显示单位。由 AspenPlus(TM) 使用。</summary>
    /// <remarks>显示单位定义参数的测量符号单位。
    /// 注：符号必须是 AspenPlus 认可的大写字符串之一，以确保可以对参数值进行单位换算。
    /// 系统将参数值从 SI 单位转换为数据浏览器中的显示单位，并将更新后的值转换回 SI 单位。</remarks>
    public string AspenUnit;

    /// <summary>获取与单位类别关联的国际单位制单位的名称，例如帕斯卡表示压力。</summary>
    /// <remarks>国际单位制单位是同一类别（无论是国际单位制还是习惯单位制）的任何两个单位之间转换的基础。</remarks>
    public string SI_Unit;

    /// <summary>获取与单位类别关联的质量维度。</summary>
    /// <remarks>The mass dimensionality of the unit category.</remarks>
    public double Mass;

    /// <summary>获取与单位类别关联的时间维度。</summary>
    /// <remarks>The time dimensionality of the unit category.</remarks>
    public double Time;

    /// <summary>获取与单位类别关联的长度维度。</summary>
    /// <remarks>The length dimensionality of the unit category.</remarks>
    public double Length;

    /// <summary>获取与单位类别关联的电流(安培数)维度。</summary>
    /// <remarks>The electrical current (amperage) dimensionality of the unit category.</remarks>
    public double ElectricalCurrent;

    /// <summary>获取与单位类别关联的温度维度。</summary>
    /// <remarks>The temperature dimensionality of the unit category.</remarks>
    public double Temperature;

    /// <summary>获取与单位类别关联的物质(摩尔)维度数量。</summary>
    /// <remarks>The amount of substance (moles) dimensionality of the unit category.</remarks>
    public double AmountOfSubstance;

    /// <summary>获取与单位类别关联的亮度(流明)维度。</summary>
    /// <remarks>The luminosity dimensionality of the unit category.</remarks>
    public double Luminous;

    /// <summary>获取与单位类别关联的财务货币维度。</summary>
    /// <remarks>The financial currency dimensionality of the unit category.</remarks>
    public double Currency;
}

/// <summary>静态类，表示对 CAPE-OPEN 维数和实值参数度量单位的支撑。</summary>
/// <remarks>This class supports the use of CAPE-OPEN dimensionality and conversion between SI and customary units of
/// measure for real-valued parameters.</remarks>
internal static class CDimensions
{
    private static readonly ArrayList DimUnits;
    private static readonly ArrayList UnitCategories;

    /// <summary>初始化 <see cref = "CDimensions"/> 类的静态字段。</summary>
    /// <remarks>Loads units and unit category data from XML files.</remarks>
    static CDimensions()
    {
        DimUnits = new ArrayList();
        UnitCategories = new ArrayList();
        var domain = AppDomain.CurrentDomain;
        //System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        //System.IO.Stream resStream = myAssembly.GetManifestResourceStream("CapeOpen.Resources.units.xml.resources");
        //System.Resources.ResourceReader resReader = new System.Resources.ResourceReader(resStream);
        //System.Collections.IDictionaryEnumerator en = resReader.GetEnumerator();
        //String temp = String.Empty;
        //while (en.MoveNext())
        //{
        //    if (en.Key.ToString() == "units") temp = en.Value.ToString();
        //}
        var reader = new XmlDocument();
        reader.LoadXml(Properties.Resources.units);
        var list = reader.SelectNodes("Units/Unit_Specs");
        var ci = new CultureInfo(0x0409, false);
        for (var i = 0; i < list.Count; i++)
        {
            UnitSet newUnit;
            var unitName = list[i].SelectSingleNode("Unit").InnerText;
            newUnit.Name = unitName.Trim();
            newUnit.Description = "";
            newUnit.Category = list[i].SelectSingleNode("Category").InnerText;
            newUnit.ConversionTimes =
                Convert.ToDouble(list[i].SelectSingleNode("ConversionTimes").InnerText, ci.NumberFormat);
            newUnit.ConversionPlus =
                Convert.ToDouble(list[i].SelectSingleNode("ConversionPlus").InnerText, ci.NumberFormat);
            DimUnits.Add(newUnit);
        }

        var userUnitPath = string.Concat(domain.BaseDirectory, "//data//user_defined_UnitsResult.XML");
        if (File.Exists(userUnitPath))
        {
            reader.Load(userUnitPath);
            list = reader.SelectNodes("Units/Unit_Specs");
            for (var i = 0; i < list.Count; i++)
            {
                var newUnit = new UnitSet();
                var unitName = list[i].SelectSingleNode("Unit").InnerText;
                newUnit.Name = unitName.Trim();
                newUnit.Category = list[i].SelectSingleNode("Category").InnerText;
                newUnit.ConversionTimes =
                    Convert.ToDouble(list[i].SelectSingleNode("ConversionTimes").InnerText, ci.NumberFormat);
                newUnit.ConversionPlus =
                    Convert.ToDouble(list[i].SelectSingleNode("ConversionPlus").InnerText, ci.NumberFormat);
                DimUnits.Add(newUnit);
            }
        }

        //resStream = myAssembly.GetManifestResourceStream("CapeOpen.Resources.unitCategories.xml.resources");
        //resReader = new System.Resources.ResourceReader(resStream);
        //en = resReader.GetEnumerator();
        //while (en.MoveNext())
        //{
        //    if (en.Key.ToString() == "unitCategories") temp = en.Value.ToString();
        //}
        reader.LoadXml(Properties.Resources.unitCategories);
        list = reader.SelectNodes("CategorySpecifications/Category_Spec");
        for (var i = 0; i < list.Count; i++)
        {
            var unitName = list[i].SelectSingleNode("Category").InnerText;
            UnitCategory category;
            category.Name = unitName;
            category.AspenUnit = list[i].SelectSingleNode("Aspen").InnerText;
            category.SI_Unit = list[i].SelectSingleNode("SI_Unit").InnerText;
            category.Mass = Convert.ToDouble(list[i].SelectSingleNode("Mass").InnerText);
            category.Time = Convert.ToDouble(list[i].SelectSingleNode("Time").InnerText);
            category.Length = Convert.ToDouble(list[i].SelectSingleNode("Length").InnerText);
            category.ElectricalCurrent = Convert.ToDouble(list[i].SelectSingleNode("ElectricalCurrent").InnerText);
            category.Temperature = Convert.ToDouble(list[i].SelectSingleNode("Temperature").InnerText);
            category.AmountOfSubstance = Convert.ToDouble(list[i].SelectSingleNode("AmountOfSubstance").InnerText);
            category.Luminous = Convert.ToDouble(list[i].SelectSingleNode("Luminous").InnerText, ci.NumberFormat);
            category.Currency = Convert.ToDouble(list[i].SelectSingleNode("Currency").InnerText, ci.NumberFormat);
            UnitCategories.Add(category);
        }

        var userUnitCategoryPath = string.Concat(domain.BaseDirectory, "data//user_defined_units.XML");
        if (!File.Exists(userUnitCategoryPath)) return;
        {
            reader.Load(userUnitCategoryPath);
            list = reader.SelectNodes("CategorySpecifications/Category_Spec");
            for (var i = 0; i < list.Count; i++)
            {
                var unitName = list[i].SelectSingleNode("Category").InnerText;
                UnitCategory category;
                category.Name = unitName;
                category.AspenUnit = list[i].SelectSingleNode("Aspen").InnerText;
                category.SI_Unit = list[i].SelectSingleNode("SI_Unit").InnerText;
                category.Mass = Convert.ToDouble(list[i].SelectSingleNode("Mass").InnerText, ci.NumberFormat);
                category.Time = Convert.ToDouble(list[i].SelectSingleNode("Time").InnerText, ci.NumberFormat);
                category.Length = Convert.ToDouble(list[i].SelectSingleNode("Length").InnerText, ci.NumberFormat);
                category.ElectricalCurrent = Convert.ToDouble(list[i].SelectSingleNode("ElectricalCurrent").InnerText,
                    ci.NumberFormat);
                category.Temperature =
                    Convert.ToDouble(list[i].SelectSingleNode("Temperature").InnerText, ci.NumberFormat);
                category.AmountOfSubstance = Convert.ToDouble(list[i].SelectSingleNode("AmountOfSubstance").InnerText,
                    ci.NumberFormat);
                category.Luminous = Convert.ToDouble(list[i].SelectSingleNode("Luminous").InnerText, ci.NumberFormat);
                category.Currency = Convert.ToDouble(list[i].SelectSingleNode("Currency").InnerText, ci.NumberFormat);
                UnitCategories.Add(category);
            }
        }
    }


    /// <summary>与维度相关的国际单位制单位。</summary>
    /// <remarks>度量单位的类别定义了单位的维度。参数的维度表示该参数的物理维度轴。
    /// 预期维度必须覆盖至少6个基本轴 (length, mass, time, angle, temperature and charge)。
    /// 可能的实现方式可以是一个具有固定长度的数组向量，其中包含每个基本 SI 单位的指数，遵循 SI 手册的指示。(from http://www.bipm.fr/)
    /// So if we agree on order (m kg s A K) ... velocity would be (1,0,-1,0,0,0): 
    /// that is m1 * s-1 =m/s. We have suggested to the  CO Scientific Committee to use the SI base units plus the 
    /// SI derived units with special symbols (for a better usability and for allowing the definition of angles).</remarks>
    /// <param name="dimensions">The dimensionality of the unit.</param>
    /// <returns>The SI unit having the desired dimensionality</returns>
    public static string SIUnit(int[] dimensions)
    {
        foreach (UnitCategory category in UnitCategories)
        {
            if (dimensions[0] == category.Length &&
                dimensions[1] == category.Mass &&
                dimensions[2] == category.Time &&
                dimensions[3] == category.ElectricalCurrent &&
                dimensions[4] == category.Temperature &&
                dimensions[5] == category.AmountOfSubstance &&
                dimensions[6] == category.Luminous)
                return category.SI_Unit;
        }

        return string.Empty;
    }

    /// <summary>与维度相关的国际单位制单位。</summary>
    /// <remarks>度量单位的类别定义了单位的维度。参数的维度表示该参数的物理维度轴。
    /// 预期维度必须覆盖至少6个基本轴 (length, mass, time, angle, temperature and charge)。
    /// 可能的实现方式可以是一个具有固定长度的数组向量，其中包含每个基本 SI 单位的指数，遵循 SI 手册的指示。(from http://www.bipm.fr/)
    /// So if we agree on order (m kg s A K) ... velocity would be (1,0,-1,0,0,0): 
    /// that is m1 * s-1 =m/s. We have suggested to the  CO Scientific Committee to use the SI base units plus the 
    /// SI derived units with special symbols (for a better usability and for allowing the definition of angles).</remarks>
    /// <param name="dimensions">The dimensionality of the unit.</param>
    /// <returns>The SI unit having the desired dimensionality</returns>
    public static string SIUnit(double[] dimensions)
    {
        foreach (UnitCategory category in UnitCategories)
        {
            if (dimensions[0] == category.Length &&
                dimensions[1] == category.Mass &&
                dimensions[2] == category.Time &&
                dimensions[3] == category.ElectricalCurrent &&
                dimensions[4] == category.Temperature &&
                dimensions[5] == category.AmountOfSubstance &&
                dimensions[6] == category.Luminous)
                return category.SI_Unit;
        }

        return string.Empty;
    }

    /// <summary>提供维度包支持的所有单位。</summary>
    /// <remarks>Provides a list of all the units of measure available in this unit conversion package.</remarks>
    /// <value>The list of all units.</value>
    public static string[] Units
    {
        get {
            var retVal = new string[DimUnits.Count];
            for (var i = 0; i < DimUnits.Count; i++)
            {
                retVal[i] = ((UnitSet)DimUnits[i]).Name;
            }

            return retVal;
        }
    }

    /// <summary>转换因子，用于将测量值乘以该因子以将其转换为SI等效单位。</summary>
    /// <remarks>将单位转换为和从单位类别的 SI 等效值进行转换。通过首先将存储在 <see cref="ConversionsPlus"/> 中的
    /// 任何偏移量添加到单位值，然后将该总和乘以单位<see cref="ConversionsTimes"/>的值，即可获得以 SI 单位表示的测量值。</remarks>
    /// <param name="unitSet">The unit to get the conversion factor for.</param>
    /// <returns>The multiplicative part of the conversion factor.</returns>
    public static double ConversionsTimes(string unitSet)
    {
        double retVal = 0;
        var found = false;
        foreach (var mt in DimUnits)
        {
            var current = (UnitSet)mt;
            if (current.Name != unitSet) continue;
            retVal = current.ConversionTimes;
            found = true;
        }

        if (!found) throw new CapeBadArgumentException(string.Concat("Unit: ", unitSet, " was not found"), 1);
        return retVal;
    }

    /// <summary>转换因子，用于将测量值乘以该因子以将其转换为SI等效单位。</summary>
    /// <remarks>将单位转换为和从单位类别的 SI 等效值进行转换。通过首先将存储在 <see cref="ConversionsPlus"/> 中的
    /// 任何偏移量添加到单位值，然后将该总和乘以单位<see cref="ConversionsTimes"/>的值，即可获得以 SI 单位表示的测量值。</remarks>
    /// <param name="unitSet">The unit to get the conversion factor for.</param>
    /// <returns>The additive part of the conversion factor.</returns>
    public static double ConversionsPlus(string unitSet)
    {
        double retVal = 0;
        var found = false;
        foreach (var mt in DimUnits)
        {
            var current = (UnitSet)mt;
            if (current.Name != unitSet) continue;
            retVal = current.ConversionPlus;
            found = true;
        }

        if (!found) throw new CapeBadArgumentException(string.Concat("Unit: ", unitSet, " was not found"), 1);
        return retVal;
    }

    /// <summary>计量单位的类别。</summary>
    /// <remarks>The category for a unit of measure defines the dimensionality of the unit.</remarks>
    /// <param name="unitSet">The unit to get the category of.</param>
    /// <returns>The unit category.</returns>
    public static string UnitCategory(string unitSet)
    {
        var retVal = string.Empty;
        var found = false;
        foreach (var mt in DimUnits)
        {
            var current = (UnitSet)mt;
            if (current.Name != unitSet) continue;
            retVal = current.Category;
            found = true;
        }

        //for (int i = 0; i < unitCategories.Count; i++)
        //{
        //    CapeOpen.unitCategory current = (CapeOpen.unitCategory)unitCategories[i];
        //    if (current.Name == unit)
        //    {
        //        retVal = current.AspenUnit;
        //        found = true;
        //    }
        //}
        if (!found) throw new CapeBadArgumentException(string.Concat("Unit: ", unitSet, " was not found"), 1);
        return retVal;
    }

    /// <summary>获取与单位类别关联的国际单位制单位的名称，例如帕斯卡表示压力。</summary>
    /// <remarks>The SI unit is the basis for conversions between any two units of the same category, either SI or 
    /// customary.</remarks>
    /// <returns>The Aspen(TM) display unit that corresponds to the current unit.</returns>
    /// <param name="unitSet">The unit to get the Aspen(TM) unit for.</param>
    public static string AspenUnit(string unitSet)
    {
        var retVal = string.Empty;
        var category = string.Empty;
        var found = false;
        foreach (var mt in DimUnits)
        {
            var current = (UnitSet)mt;
            if (current.Name != unitSet) continue;
            category = current.Category;
            found = true;
        }

        foreach (var mt in UnitCategories)
        {
            var current = (UnitCategory)mt;
            if (current.Name != category) continue;
            retVal = current.AspenUnit;
            found = true;
        }

        if (!found) throw new CapeBadArgumentException(string.Concat("Unit: ", unitSet, " was not found"), 1);
        return retVal;
    }

    /// <summary>计量单位的描述。</summary>
    /// <remarks>The description of the unit of measure.</remarks>
    /// <param name="unitSet">The unit to get the conversion factor for.</param>
    /// <returns>The description of the unit of measure.</returns>
    public static string UnitDescription(string unitSet)
    {
        var retVal = string.Empty;
        var found = false;
        foreach (var mt in DimUnits)
        {
            var current = (UnitSet)mt;
            if (current.Name != unitSet) continue;
            retVal = current.Description;
            found = true;
        }

        if (!found) throw new CapeBadArgumentException(string.Concat("Unit: ", unitSet, " was not found"), 1);
        return retVal;
    }

    ///// <summary>更改计量单位的描述。</summary>
    ///// <remarks>Changes the description of the unit of measure.</remarks>
    //public static void ChangeUnitDescription(String unit, String newDescription)
    //{
    //    bool found = false;
    //    for (int i = 0; i < units.Count; i++)
    //    {
    //        CapeOpen.unit current = (CapeOpen.unit)units[i];
    //        if (current.Name == unit)
    //        {
    //            current.Description = newDescription;
    //            found = true;
    //        }
    //    }
    //    if (!found) throw new CapeOpen.CapeBadArgumentException(String.Concat("Unit:", " ",unit, " was not found"), 1);
    //}

    /// <summary>返回与单位类别匹配的所有单位。</summary>
    /// <remarks>A unit category represents a specific combination of dimensionality values. Examples would be 
    /// pressure or temperature. This method would return all units that are in the category, such as Celsius,
    /// Kelvin, Fahrenheit, and Rankine for temperature.</remarks>
    /// <param name="category">The category of the desired units.</param>
    /// <returns>All units that represent the category.</returns>
    public static string[] UnitsMatchingCategory(string category)
    {
        var unitNames = new ArrayList();
        foreach (var mt in DimUnits)
        {
            var current = (UnitSet)mt;
            if (current.Category == category)
            {
                unitNames.Add(current.Name);
            }
        }

        var retVal = new string[unitNames.Count];
        for (var i = 0; i < unitNames.Count; i++)
        {
            retVal[i] = unitNames[i].ToString();
        }

        return retVal;
    }

    /// <summary>返回与单位关联的国际单位制单位。</summary>
    /// <remarks>A unit category represents a specific combination of dimensionality values. Examples would be 
    /// pressure or temperature. This method would return the SI unit for the category, such as Kelvin (K) for 
    /// temperature or Pascal (N/m^2) for pressure.</remarks>
    /// <param name="unitSet">The unit to get the SI unit of.</param>
    /// <returns>The SI unit that corresponds to the unit.</returns>
    public static string FindSIUnit(string unitSet)
    {
        var retVal = string.Empty;
        var category = UnitCategory(unitSet);
        foreach (var mt in UnitCategories)
        {
            var current = (UnitCategory)mt;
            if (current.Name == category)
            {
                retVal = current.SI_Unit;
            }
        }

        return retVal;
    }

    /// <summary>计量单位的维度。</summary>
    /// <remarks>度量单位的类别定义了单位的维度。参数的维度表示该参数的物理维度轴。
    /// 预期维度必须覆盖至少6个基本轴 (length, mass, time, angle, temperature and charge)。
    /// 可能的实现方式可以是一个具有固定长度的数组向量，其中包含每个基本 SI 单位的指数，遵循 SI 手册的指示。(from http://www.bipm.fr/)
    /// So if we agree on order (m kg s A K) ... velocity would be (1,0,-1,0,0,0): 
    /// that is m1 * s-1 =m/s. We have suggested to the  CO Scientific Committee to use the SI base units plus the 
    /// SI derived units with special symbols (for a better usability and for allowing the definition of angles).</remarks>
    /// <param name="unitSet">The unit to get the dimensionality of.</param>
    /// <returns>The dimensionality of the unit.</returns>
    public static double[] Dimensionality(string unitSet)
    {
        var category = UnitCategory(unitSet);
        double[] retVal = [0, 0, 0, 0, 0, 0, 0, 0];
        foreach (var mt in UnitCategories)
        {
            var current = (UnitCategory)mt;
            if (current.Name != category) continue;
            retVal[0] = current.Length;
            retVal[1] = current.Mass;
            retVal[2] = current.Time;
            retVal[3] = current.ElectricalCurrent;
            retVal[4] = current.Temperature;
            retVal[5] = current.AmountOfSubstance;
            retVal[6] = current.Luminous;
            retVal[7] = current.Currency;
        }

        return retVal;
    }
}
