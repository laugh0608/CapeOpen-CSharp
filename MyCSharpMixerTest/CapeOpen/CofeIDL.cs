// 大白萝卜重构于 2025.05.13，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CapeOpen;

/// <summary>
/// Stream type enumeration used by COFE.
/// </summary>
/// <remarks>
/// This enumeration provides the Stream type enumeration used by COFE.
/// </remarks>
[Serializable]
[ComVisible(true)]
[Guid(CapeOpenGuids.CofeStreamTypeIid)]  // "D1B15843-C0F5-4CB7-B462-E1B80456808E"
public enum CofeStreamType
{
	/// <summary>
	/// COFE Material Stream.
	/// </summary>
	STREAMTYPE_MATERIAL = 0,
	/// <summary>
	/// COFE Energy Stream.
	/// </summary>
	STREAMTYPE_ENERGY = 1,
	/// <summary>
	/// COFE Information Stream.
	/// </summary>
	STREAMTYPE_INFORMATION = 2
}

/// <summary>
/// COFE Stream Interface.
/// </summary>
/// <remarks>
/// Interface implemented by COFE Stream object.
/// </remarks>
[ComImport]
[ComVisible(true)]
[Guid(CapeOpenGuids.InCofeStreamIid)] // "B2A15C45-D878-4E56-A19A-DED6A6AD6F91"
[Description("ICOFEStream Interface")]
public interface ICOFEStream 
{
	/// <summary>
	/// Type of the Stream from COFE.
	/// </summary>
	/// <remarks>
	/// <para>Get the type of the COFE Stream. 
	/// It has three possible values:</para>
	/// <para>   (i)   MATERIAL</para>
	/// <para>   (ii)  ENERGY</para>
	/// <para>   (iii) INFORMATION</para>
	/// </remarks>
	/// <value>Type of the Stream from COFE.</value>
	[DispId(1)] 
	CofeStreamType StreamType
	{
		get;
	}

	/// <summary>
	/// Unit operation upstream of the COFE Stream.
	/// </summary>
	/// <remarks>
	/// <para>Gets the unit operation upstream of the current Stream.</para>para>
	/// </remarks>
	/// <value>Unit Operation upstream of the current stream.</value>
	[DispId(2)]
	object UpstreamUnit
	{
		[return: MarshalAs(UnmanagedType.IDispatch)]
		get;
	}

	/// <summary>
	/// Unit operation downstream of the COFE Stream.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Gets the unit operation downstream of the current Stream.
	/// </para>
	/// </remarks>
	/// <value>Unit Operation downstream of the current stream.</value>
	[DispId(3)]
	object DownstreamUnit
	{
		[return: MarshalAs(UnmanagedType.IDispatch)]
		get;
	}
}

/// <summary>
/// COFE Material Interface.
/// </summary>
/// <remarks>
/// Interface implemented by COFE Material object.
/// </remarks>
[ComImport]
[ComVisible(true)]
[Guid(CapeOpenGuids.InCofeMaterialIid)]  // "2BFFCBD3-7DAB-439D-9E25-FBECC8146BE8"
[Description("ICOFEMaterial Interface")]
public interface ICOFEMaterial
{
	/// <summary>
	/// Material type used by COFE.
	/// </summary>
	/// <remarks>
	/// This method provides the material type used by COFE.
	/// </remarks>
	[DispId(1)] 
	string MaterialType
	{
		get;
	}

	/// <summary>
	/// Phase list supported by this material in COFE.
	/// </summary>
	/// <remarks>
	/// Phase list supported by this material in COFE.
	/// </remarks>
	[DispId(2)] 
	object GetSupportedPhaseList();
}

[ComImport]
[ComVisible(true)]
[Guid(CapeOpenGuids.InCofeIconIid)]  // "5F6333E0-434F-4C03-85E2-6EB493EAE846"
[Description("ICOFEIcon Interface")]
internal interface ICOFEIcon
{
	[DispId(1)]
	void SetUnitOperationIcon(string iconFileName);
}