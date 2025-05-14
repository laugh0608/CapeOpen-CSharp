// 大白萝卜重构于 2025.05.09，使用 .NET8.O-windows、Microsoft Visual Studio 2022 Preview 和 Rider 2024.3。

/* IMPORTANT NOTICE
(c) The CAPE-OPEN Laboratory Network, 2002.
All rights are reserved unless specifically stated otherwise

Visit the web site at www.colan.org

This file has been edited using the editor from Microsoft Visual Studio 6.0
This file can view properly with any basic editors and browsers (validation done under MS Windows and Unix)
*/

// This file was developed/modified by JEAN-PIERRE-BELAUD for CO-LaN organisation - March 2003

namespace CapeOpen;

internal abstract class CapeOpenGuids
{
    // CAPE-OPEN Category
    public const string CapeOpenComponent_CATID = "{678c09a1-7d66-11d2-a67d-00105a42887f}";
    // External CAPE-OPEN Thermo Routines
    public const string CapeExternalThermoRoutine_CATID = "{678c09a2-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN Thermo System
    public const string CapeThermoSystem_CATID = "{678c09a3-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN Thermo Property Package
    public const string CapeThermoPropertyPackage_CATID = "{678c09a4-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN Thermo Equilibrium Server
    public const string CapeThermoEquilibriumServer_CATID = "{678c09a6-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN Unit Operation
    public const string CapeUnitOperation_CATID = "{678c09a5-7d66-11d2-a67d-00105a42887f}";
    // CAEP-OPEN Reactions Package manager components
    public const string CAPEOPENReactionsPackageManager_CATID = "{678c09aa-0100-11d2-a67d-00105a42887f}";
    // CAPE-OPEN Standalone Reactions Package
    public const string CAPEOPENReactionsPackage_CATID = "{678c09ab-0100-11d2-a67d-00105a42887f}";
    // CAPE-OPEN MINLP Solver Package
    public const string CapeMINLPSolverPackage_CATID = "{678c09ac-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN PPDB Service
    public const string CapePPDBService_CATID = "{678c09aa-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN SMST Package
    public const string CapeSMSTPackage_CATID = "{678c09ab-7d66-11d2-a67d-00105a42887f}";
    // CAPE-OPEN Solvers Package
    public const string CapeSolversPackage_CATID = "{79DD785E-27E5-4939-B040-B1E45B1F2C64}";
    // CAPE-OPEN PSP Package
    public const string CapePSPPackage_CATID = "{3EFFA2BD-D9E7-4e55-B515-AD3E3623AAD5}";
    // CAPE-OPEN Monitoring Object
    public const string CATID_MONITORING_OBJECT = "{7BA1AF89-B2E4-493d-BD80-2970BF4CBE99}";
    // CAPE-OPEN Thermodynamics Consuming Object
    public const string Consumes_Thermo_CATID = "{4150C28A-EE06-403f-A871-87AFEC38A249}";
    // CAPE-OPEN Object Supporting Thermo 1.0
    public const string SupportsThermodynamics10_CATID = "{0D562DC8-EA8E-4210-AB39-B66513C0CD09}";
    // CAPE-OPEN Object Supporting Thermo 1.1
    public const string SupportsThermodynamics11_CATID = "{4667023A-5A8E-4cca-AB6D-9D78C5112FED}";

    /**************************************
     Identification Interfaces
    **************************************/
    public const string CapeValidationStatus_IID = "678c0b04-7d66-11d2-a67d-00105a42887f";
    public const string CapeIdentification_IID = "678c0990-7d66-11d2-a67d-00105a42887f";

    /**************************************
     Collection Interfaces
    **************************************/
    public const string ICapeCollection_IID = "678c099a-0093-11d2-a67d-00105a42887f";

    /**************************************
     Utilities Interfaces
    **************************************/
    public const string ICapeUtilities_IID = "678c0a9b-0100-11d2-a67d-00105a42887f";

    /*****************************************
    CAPE-OPEN Flowsheet monitoring interface
    *****************************************/
    public const string ICapeFlowsheetMonitoring_IID = "2CC8CC79-F854-4d65-B296-F8CD3344A2CD";

    /**************************************
     Parameter Interfaces
    **************************************/
    public const string ICapeParameterSpec_IID = "678c099c-0093-11d2-a67d-00105a42887f";
    public const string ICapeRealParameterSpec_IID = "678c099d-0093-11d2-a67d-00105a42887f";
    public const string ICapeIntegerParameterSpec_IID = "678c099e-0093-11d2-a67d-00105a42887f";
    public const string ICapeOptionParameterSpec_IID = "678c099f-0093-11d2-a67d-00105a42887f";
    public const string ICapeParameter_IID = "678c09a0-0093-11d2-a67d-00105a42887f";
    public const string ICapeBooleanParameterSpec_IID = "678c09a8-0093-11d2-a67d-00105a42887f";
    public const string ICapeArrayParameterSpec_IID = "678c09a9-0093-11d2-a67d-00105a42887f";
    public const string CapeParamType_IID = "678c0b02-7d66-11d2-a67d-00105a42887f";
    public const string CapeParamMode_IID = "678c0b03-7d66-11d2-a67d-00105a42887f";

    /**************************************
     Error Interfaces
    **************************************/
    public const string CapeErrorInterfaceHRs_IID = "678c0b01-7d66-11d2-a67d-00105a42887f";
    public const string ECapeRoot_IID = "678c0b10-7d66-11d2-a67d-00105a42887f";
    public const string ECapeUser_IID = "678C0B11-7D66-11D2-A67D-00105A42887F";
    public const string ECapeUnknown_IID = "678c0b12-7d66-11d2-a67d-00105a42887f";
    public const string ECapeData_IID = "678c0b13-7d66-11d2-a67d-00105a42887f";
    public const string ECapeLicenceError_IID = "678c0b14-7d66-11d2-a67d-00105a42887f";
    public const string ECapeBadCOParameter_IID = "678c0b15-7d66-11d2-a67d-00105a42887f";
    public const string ECapeBadArgument_IID = "E29E42B3-E481-45c6-A737-78F4A7FC0391";
    public const string ECapeInvalidArgument_IID = "678c0b17-7d66-11d2-a67d-00105a42887f";
    public const string ECapeOutOfBounds_IID = "678c0b18-7d66-11d2-a67d-00105a42887f";
    public const string ECapeImplementation_IID = "678c0b19-7d66-11d2-a67d-00105a42887f";
    public const string ECapeNoImpl_IID = "678c0b1a-7d66-11d2-a67d-00105a42887f";
    public const string ECapeLimitedImpl_IID = "678c0b1b-7d66-11d2-a67d-00105a42887f";
    public const string ECapeComputation_IID = "678c0b1c-7d66-11d2-a67d-00105a42887f";
    public const string ECapeOutOfResources_IID = "678c0b1d-7d66-11d2-a67d-00105a42887f";
    public const string ECapeNoMemory_IID = "678c0b1e-7d66-11d2-a67d-00105a42887f";
    public const string ECapeTimeOut_IID = "678c0b1f-7d66-11d2-a67d-00105a42887f";
    public const string ECapeFailedInitialisation_IID = "678c0b20-7d66-11d2-a67d-00105a42887f";
    public const string ECapeSolvingError_IID = "678c0b21-7d66-11d2-a67d-00105a42887f";
    public const string ECapeBadInvOrder_IID = "678c0b22-7d66-11d2-a67d-00105a42887f";
    public const string ECapeInvalidOperation_IID = "678c0b23-7d66-11d2-a67d-00105a42887f";
    public const string ECapePersistence_IID = "678c0b24-7d66-11d2-a67d-00105a42887f";
    public const string ECapeIllegalAccess_IID = "678c0b25-7d66-11d2-a67d-00105a42887f";
    public const string ECapePersistenceNotFound_IID = "678c0b26-7d66-11d2-a67d-00105a42887f";
    public const string ECapePersistenceSystemError_IID = "678c0b27-7d66-11d2-a67d-00105a42887f";
    public const string ECapePersistenceOverflow_IID = "678c0b28-7d66-11d2-a67d-00105a42887f";
    public const string ECapeBoundaries_IID = "678c0b29-7d66-11d2-a67d-00105a42887f";
    public const string ECapeErrorDummy_IID = "678c0b07-7d66-11d2-a67d-00105a42887f";

    /**************************************
     COSE Interfaces
    **************************************/
    public const string ICapeSimulationContext_IID = "678c0a9c-0100-11d2-a67d-00105a42887f";
    public const string ICapeDiagnostic_IID = "678c0a9d-0100-11d2-a67d-00105a42887f";
    public const string ICapeMaterialTemplateSystem_IID = "678c0a9e-0100-11d2-a67d-00105a42887f";
    public const string ICapeCOSEUtilities_IID = "678c0a9f-0100-11d2-a67d-00105a42887f";

    /**************************************
     Thermo Interfaces
    **************************************/
    public const string ICapeThermoCalculationRoutine_IID = "678c0991-7d66-11d2-a67d-00105a42887f";
    public const string ICapeThermoReliability_IID = "678c0992-7d66-11d2-a67d-00105a42887f";
    public const string ICapeThermoMaterialTemplate_IID = "678c0993-7d66-11d2-a67d-00105a42887f";
    public const string ICapeThermoMaterialObject_IID = "678c0994-7d66-11d2-a67d-00105a42887f";
    public const string ICapeThermoSystem_IID = "678c0995-7d66-11d2-a67d-00105a42887f";
    public const string ICapeThermoPropertyPackage_IID = "678c0996-7d66-11d2-a67d-00105a42887f";
    public const string ICapeThermoEquilibriumServer_IID = "678c0997-7d66-11d2-a67d-00105a42887f";

    // Example CLSID - not for use 
    public const string AspenTechThermoSystem_CLSID = "678c09a7-7d66-11d2-a67d-00105a42887f";

    /**************************************
     Solvers Interfaces
    **************************************/
    public const string ICapeNumericMatrix_IID = "3AD3C8F6-E6EC-4e63-B51E-0E9403535463";
    public const string ICapeNumericUnstructuredMatrix_IID = "678c09af-7d66-11d2-a67d-00105a42887f";
    public const string ICapeNumericFullMatrix_IID = "678c0b71-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericBandedMatrix_IID = "678c0b72-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericESOManager_IID = "678c0b73-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericESO_IID = "9304E044-3311-41ed-8766-0123CB44038A";
    public const string ICapeNumericLAESO_IID = "678c0b74-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericNLAESO_IID = "678c0b75-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericDAESO_IID = "678c0b76-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericGlobalESO_IID = "678c0b77-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericGlobalLAESO_IID = "678c0b78-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericGlobalNLAESO_IID = "678c0b79-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericGlobalDAESO_IID = "678c0b7a-0100-11d2-a67d-00105a42887f";

    public const string ICapeNumericModel_IID = "678c0b7c-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericContinuousModel_IID = "678c0b7d-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericHierarchicalModel_IID = "678c0b7e-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericAggregateModel_IID = "678c0b7f-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericSTN_IID = "678c0b80-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericEvent_IID = "678c0b81-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericBasicEvent_IID = "678c0b82-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericCompositeEvent_IID = "678c0b83-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericBinaryEvent_IID = "678c0b84-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericUnaryEvent_IID = "678c0b85-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericEventInfo_IID = "678c0b86-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericExternalEventInfo_IID = "678c0b87-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericInternalEventInfo_IID = "678c0b88-0100-11d2-a67d-00105a42887f";

    public const string ICapeNumericSolver_IID = "678c0b8a-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericLASolver_IID = "678c0b8b-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericNLASolver_IID = "678c0b8c-0100-11d2-a67d-00105a42887f";
    public const string ICapeNumericDAESolver_IID = "678c0b8d-0100-11d2-a67d-00105a42887f";

    /**************************************
     Unit Operation Interfaces
    **************************************/
    public const string ICapeUnit_IID = "678c0998-0100-11d2-a67d-00105a42887f";
    public const string ICapeUnitPort_IID = "678c0999-0093-11d2-a67d-00105a42887f";
    public const string ICapeUnitReport_IID = "678c099b-0093-11d2-a67d-00105a42887f";

    //public const string  ICapeUnitEdit_IID			"678c0a9a-0093-11d2-a67d-00105a42887f";
    //public const string  ICapeUnitCollection_IID	"678c099a-7d66-11d2-a67d-00105a42887f";
    // ICapeUnitPortVariables : new interface for mapping EO ESO to port variables
    public const string ICapeUnitPortVariables_IID = "678c09b1-7d66-11d2-a67d-00105a42887f";
    public const string CapePortDirection_IID = "678c0b05-7d66-11d2-a67d-00105a42887f";
    public const string CapePortType_IID = "678c0b06-7d66-11d2-a67d-00105a42887f";

    // Example CLSID - not for use !
    public const string HyprotechMixerSplitter_CLSID = "678c0a99-7d66-11d2-a67d-00105a42887f";

    /**************************************
     Reactions interfaces
    **************************************/
    public const string ICapeElectrolyteReactionContext_IID = "678c0afd-0100-11d2-a67d-00105a42887f";
    public const string ICapeKineticReactionContext_IID = "678c0afe-0100-11d2-a67d-00105a42887f";
    public const string ICapeReactionsPackageManager_IID = "678c0afc-0100-11d2-a67d-00105a42887f";
    public const string ICapeReactionChemistry_IID = "678c0afb-0100-11d2-a67d-00105a42887f";
    public const string ICapeReactionProperties_IID = "678c0afa-0100-11d2-a67d-00105a42887f";
    public const string ICapeReactionsRoutine_IID = "678c0af9-0100-11d2-a67d-00105a42887f";

    // ICapeThermoContext - actually part of the 1.1 thermo specification but 
    // included here because it is required for the Reactions interfaces
    public const string ICapeThermoContext_IID = "678c0b5f-0100-11d2-a67d-00105a42887f";
    public const string CapeReactionType_IID = "678c0b00-0100-11d2-a67d-00105a42887f";
    public const string CapeReactionRateBasis_IID = "678c0aff-0100-11d2-a67d-00105a42887f";

    /**************************************
     Petroleum Fractions interfaces
    **************************************/
    public const string ICapeThermoPetroFractions_IID = "678c0aa0-0100-11d2-a67d-00105a42887f";
    public const string ICapeUnitTypeInfo_IID = "678c0aa1-0100-11d2-a67d-00105a42887f";
    public const string CapeUnitType_IID = "678c0aa2-0100-11d2-a67d-00105a42887f";

    /**************************************
     SMST interfaces
    **************************************/
    public const string ICapeSMSTFlowsheetManager_IID = "678c0b65-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTFlowsheet_IID = "678c0b66-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTProcessGraph_IID = "678c0b67-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTPartitionGraph_IID = "678c0b68-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTOpenPartitionGraph_IID = "678c0b69-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTAnalysisManager_IID = "678c0b6a-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTAnalysis_IID = "678c0b6b-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTSequencing_IID = "678c0b6c-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTTearing_IID = "678c0b6d-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTPartitioning_IID = "678c0b6e-0100-11d2-a67d-00105a42887f";
    public const string ICapeSMSTSMAnalysis_IID = "678c0b70-0100-11d2-a67d-00105a42887f";
    public const string CapeFlowsheetType_IID = "678c0b60-0100-11d2-a67d-00105a42887f";
    public const string CapeSMSTStream_IID = "678c0b61-0100-11d2-a67d-00105a42887f";
    public const string CapeAnalysisType_IID = "678c0b62-0100-11d2-a67d-00105a42887f";
    public const string CapeConsistencyCode_IID = "678c0b63-0100-11d2-a67d-00105a42887f";
    public const string CapeConvergenceCode_IID = "678c0b64-0100-11d2-a67d-00105a42887f";

    /**************************************
     MINLP Interfaces
    **************************************/
    public const string ICapeMINLP_IID = "678c09cc-7d66-11d2-a67d-00105a42887f";
    public const string ICapeMINLPSystem_IID = "678c09cd-7d66-11d2-a67d-00105a42887f";
    public const string ICapeMINLPSolverManager_IID = "678c09ce-7d66-11d2-a67d-00105a42887f";
    public const string ECapeOutsideSolverScope_IID = "678c0b0f-7d66-11d2-a67d-00105a42887f";
    public const string ECapeHessianInfoNotAvailable_IID = "3FF0B24B-4299-4dac-A46E-7843728AD205";

    /**************************************
     PPDB interfaces
    **************************************/
    public const string ICapePpdb_IID = "678c09b2-7d66-11d2-a67d-00105a42887f";
    public const string ICapePpdbRegister_IID = "678c09b3-7d66-11d2-a67d-00105a42887f";
    public const string ICapePpdbTables_IID = "678c09b4-7d66-11d2-a67d-00105a42887f";
    public const string ICapePpdbModels_IID = "678c09b5-7d66-11d2-a67d-00105a42887f";
    public const string CapeSpecCompound_IID = "678c0b0c-7d66-11d2-a67d-00105a42887f";
    public const string CapeSpecStructure_IID = "678c0b0d-7d66-11d2-a67d-00105a42887f";
    public const string CapeSpecDictionary_IID = "678c0b0e-7d66-11d2-a67d-00105a42887f";

    /**************************************
     PSP interfaces
    **************************************/
    public const string ICapePSPCollection_IID = "78DEFBBD-ED69-4f81-90A4-6B636CE13164";
    public const string ICapePSPResource_IID = "7A4D266A-E54D-4a7d-8877-632E89344FED";
    public const string ICapePSPRecipeEntity_IID = "85E4C4E2-57FC-43aa-A39A-78D392947080";
    public const string ICapePSPSchedule_IID = "F3E9CF96-DF8F-40f3-9543-E8D17CABBF96";
    public const string ICapePSPScheduleEntry_IID = "638D84DC-84E9-4aea-83A4-0BB8852832E9";
    public const string ICapePSPTransaction_IID = "45A1B544-4BE6-43f9-83A4-4A7CFEE802FE";
    public const string ICapePSPResourceRequirement_IID = "8F3C13F5-0C69-42a2-9438-4299C630A0A4";
    public const string ICapePSPReport_IID = "1DD05FA1-EB10-4cbe-A94A-24F6EA7E7815";
    public const string ICapePSPResourceCollection_IID = "E09E0B56-6A51-496f-B796-2C45C549B510";
    public const string ICapePSPRecipeEntityCollection_IID = "B3245794-9A3E-4af7-A8CA-2308290106F0";
    public const string ICapePSPScheduleCollection_IID = "033AC3EF-7449-4113-A10E-D70161B3FC22";
    public const string ICapePSPScheduleEntryCollection_IID = "25BC241A-8110-4806-AF7A-CB6CFA7B9A57";
    public const string ICapePSPTransactionCollection_IID = "052DAFEF-0F43-4f4d-88AB-50F6AD8FC0EB";
    public const string ICapePSPResourceRequirementCollection_IID = "DFF34851-E60E-40dd-BAA1-4FEDE69B3467";
    public const string ICapePSP_IID = "F840ECA2-941B-4af7-84DB-47E2187430A2";
    public const string ICapePetroFractions_IID = "72A94DE9-9A69-4369-B508-C033CDFD4F81";
    public const string CapeCompoundType_IID = "8091E285-3CFA-4a41-A5C4-141D0D709D87";
    
    /**************************************
     额外自定义补充的
    **************************************/
    // Error IDL
    public const string ECapeBadArg093Iid = "678c0b16-7d66-11d2-a67d-00105a42887f";
    public const string ECapeThProNotAvIid = "678C09B6-7D66-11D2-A67D-00105A42887F";
    public const string ECapeHeInfoNotAvIid = "3FF0B24B-4299-4DAC-A46E-7843728AD205";
    public const string ECapeOutSoScopeIid = "678c0b0f-7d66-11d2-a67d-00105a42887f";
    // ArrayParameter
    public const string ArrParaWrapperIid = "277E2E39-70E7-4FBA-89C9-2A19B9D5E576";
    // Port Collection
    public const string PortCollectionIid = "1C5F7CC3-31B4-4d81-829F-3EB5D692F7BD";
    // Unit IDL
    public const string InUnitOpValEvArgsIid = "50A759AF-5E38-4399-9050-93F823E5A6E6";
    public const string UnitOpValEvArgsIid = "9147E78B-29D6-4D91-956E-75D0FB90CEA7";
    public const string InUnitOpCalEvArgsIid = "DDCA3348-074C-4860-AD00-58386327D9AC";
    public const string UnitOpCalEvArgsIid = "7831C38B-A1C6-40C5-B9FC-DAC43426AAD4";
    public const string InUnitOpBeCalEvArgsIid = "3E827FD8-5BDB-41E4-81D9-AC438BC9B957";
    public const string UnitOpBeCalEvArgsIid = "763691E8-D792-4B97-A12A-D4AD7F66B5E4";
    public const string InUnitOpEndCalEvArgsIid = "951D755F-8831-4691-9B54-CC9935A5B7CC";
    public const string UnitOpEndCalEvArgsIid = "172F4D6E-65D1-4D9E-A275-7880FA3A40A5";
    public const string InPortConEvArgsIid = "DC735166-8008-4B39-BE1C-6E94A723AD65";
    public const string PortConEvArgsIid = "962B9FDE-842E-43F8-9280-41C5BF80DDEC";
    public const string InPortDisEvArgsIid = "5EFDEE16-7858-4119-B8BB-7394FFBCC02D";
    public const string PortDisEvArgsIid = "693F33AA-EE4A-4CDF-9BA1-8889086BC8AB";
    // Common IDL
    public const string InComNameChEvArgsIid = "F79EA405-4002-4fb2-AED0-C1E48793637D";
    public const string InComDescChEvArgsIid = "34C43BD3-86B2-46d4-8639-E0FA5721EC5C";
    public const string ComNameChEvArgsIid = "D78014E7-FB1D-43ab-B807-B219FAB97E8B";
    public const string ComDescChEvArgsIid = "0C51C4F1-20E8-413d-93E1-4704B888354A";
    public const string InCapeIdentEvIid = "5F5087A7-B27B-4b4f-902D-5F66E34A0CBE";
    public const string CapeIdentIid = "BF54DF05-924C-49a5-8EBB-733E37C38085";
    // Exception
    public const string CapeUserExIid = "28686562-77AD-448f-8A41-8CF9C3264A3E";
    public const string CapeUnKnExIid = "B550B2CA-6714-4e7f-813E-C93248142410";
    public const string CapeUnPeExIid = "16049506-E086-4baf-9905-9ED13D50D0E3";
    public const string CapeDataExPtIid = "53551E7C-ECB2-4894-B71A-CCD1E7D40995";
    public const string CapeBadCoParaIid = "667D34E9-7EF7-4ca8-8D17-C7577F2C5B62";
    public const string CapeBadArExPtiIid = "D168E99F-C1EF-454c-8574-A8E26B62ADB1";
    public const string CapeBoDaExPtIid = "62B1EE2F-E488-4679-AFA3-D490694D6B33";
    public const string CapeOutOfBoExPtIid = "4438458A-1659-48c2-9138-03AD8B4C38D8";
    public const string CapeComPuExPtIid = "9D416BF5-B9E3-429a-B13A-222EE85A92A7";
    public const string CapeFaInLiExPtIid = "E407595C-6D1C-4b8c-A29D-DB0BE73EFDDA";
    public const string CapeImTaExPtIid = "7828A87E-582D-4947-9E8F-4F56725B6D75";
    public const string CapeInValArExPtIid = "B30127DA-8E69-4d15-BAB0-89132126BAC9";
    public const string CapeInValOpTiExPtIid = "C0B943FE-FB8F-46b6-A622-54D30027D18B";
    public const string CapeBadInvOrExPtIid = "C0B943FE-FB8F-46b6-A622-54D30027D18B";
    public const string CapeLiCeErExPtIid = "CF4C55E9-6B0A-4248-9A33-B8134EA393F6";
    public const string CapeLimitedImplExIid = "CF4C55E9-6B0A-4248-9A33-B8134EA393F6";
    public const string CapeNoImplExPtIid = "1D2488A6-C428-4e38-AFA6-04F2107172DA";
    public const string CapeOutOfReCeExPtIid = "42B785A7-2EDD-4808-AC43-9E6E96373616";
    public const string CapeNoMeMoExPtIid = "1056A260-A996-4a1e-8BAE-9476D643282B";
    public const string CapePerSiTeExPtIid = "3237C6F8-3D46-47ee-B25F-52485A5253D8";
    public const string CapePeSiTeNotFoExPtIid = "271B9E29-637E-4eb0-9588-8A53203A3959";
    public const string CapePeSiOverFlExcPtIid = "A119DE0B-C11E-4a14-BA5E-9A2D20B15578";
    public const string CapePeSiSysErExIid = "85CB2D40-48F6-4c33-BF0C-79CB00684440";
    public const string CapeIllAccEssExIid = "45843244-ECC9-495d-ADC3-BF9980A083EB";
    // Monitoring Interfaces
    public const string CapeSoLvErrExIid = "F617AFEA-0EEE-4395-8C82-288BF8F2A136";
    public const string CapeHeInoNotAvBlExIid = "3044EA08-F054-4315-B67B-4E3CD2CF0B1E";
    public const string CapeTimeOutExPtIid = "0D5CA7D8-6574-4c7b-9B5F-320AA8375A3C";
    public const string ComCaOpExPtWrPerIid = "31CD55DE-AEFD-44ff-8BAB-F6252DD43F16";
    public const string CapeThProPeNotAvBlExIid = "5BA36B8F-6187-4e5e-B263-0924365C9A81";
    public const string CapeSolutionStatusIid = "D1B15843-C0F5-4CB7-B462-E1B80456808E";
    public const string InCapeFlShMoToIid = "834F65CC-29AE-41c7-AA32-EE8B2BAB7FC2";
    // Parameter Collection
    public const string PpParameterCollectionIid = "64A1B36C-106B-4d05-B585-D176CD4DD1DB";
    // Parameter IDL
    public const string InParameterEventsIid = "3C32AD8E-490D-4822-8A8E-073F5EDFF3F5";
    public const string IatCapeXRealPaTeSpecIid = "B777A1BD-0C88-11D3-822E-00C04F4F66C9";
    public const string InParTeVaChEvArgsIid = "41E1A3C4-F23C-4B39-BC54-39851A1D09C9";
    public const string ParTeVaChEvArgsIid = "C3592B59-92E8-4A24-A2EB-E23C38F88E7F";
    public const string InParDefValChaEveArgsIid = "E5D9CE6A-9B10-4A81-9E06-1B6C6C5257F3";
    public const string ParDefValChaEveArgsIid = "355A5BDD-F6B5-4EEE-97C7-F1533DD28889";
    public const string InParLowBoChaEveArgsIid = "FBCE7FC9-0F58-492B-88F9-8A23A23F93B1";
    public const string ParLowBoChaEveArgsIid = "A982AD29-10B5-4C86-AF74-3914DD902384";
    public const string InParUpBoChaEveArgsIid = "A2D0FAAB-F30E-48F5-82F1-4877F61950E9";
    public const string ParUpBoChaEveArgsIid = "92BF83FE-0855-4382-A15E-744890B5BBF2";
    public const string InParModeChaEveArgsIid = "5405E831-4B5F-4A57-A410-8E91BBF9FFD3";
    public const string ParModeChaEveArgsIid = "3C953F15-A1F3-47A9-829A-9F7590CEB5E9";
    public const string InParValEveArgsIid = "EFD819A4-E4EC-462E-90E6-5D994CA44F8E";
    public const string ParValEveArgsIid = "5727414A-838D-49F8-AFEF-1CC8C578D756";
    public const string InParResetEveArgsIid = "12067518-B797-4895-9B26-EA71C60A8803";
    public const string ParResetEveArgsIid = "01BF391B-415E-4F5E-905D-395A707DC125";
    public const string InParOpListChEvArgsIid = "78E06E7B-00AB-4295-9915-546DC1CD64A6";
    // public const string PpParOpListChEvArgsIid = "7B4DE7D2-1E39-4239-B8C5-4F876DDB15A4";
    public const string ParOpListChEvArgsIid = "2AEC279F-EBEC-4806-AA00-CC215432DB82";
    public const string InParRestToLiChEvArgsIid = "7F357261-095A-4FD4-99C1-ACDAEDA36141";
    public const string ParRestToLiChEvArgsIid = "82E0E6C2-3103-4B5A-A5BC-EBAB971B069A";
    public const string CapeParameterIid = "F027B4D1-A215-4107-AA75-34E929DD00A5";
    // Material Object Wrapper
    public const string MaterObjWrapIid = "5A65B4B2-2FDD-4208-813D-7CC527FB91BD";
    // Material Object Wrapper 10
    // public const string MaterObjWrap10Iid = "5A65B4B2-2FDD-4208-813D-7CC527FB91BD";
    // Material Object Wrapper 11
    // public const string MaterObjWrap11Iid = "5A65B4B2-2FDD-4208-813D-7CC527FB91BD";
    // Real Parameter
    public const string InRealPaSpecEveIid = "058B416C-FC61-4E64-802A-19070CB39703";
    public const string PpRealParTerIid = "77E39C43-046B-4b1f-9EE0-AA9EFC55D2EE";
    public const string PpRealParTerWapIid = "C7095FE4-E61D-4FFF-BA02-013FD38DBAE9";
    // Unit Port
    public const string InUnitPortEventsIid = "3530B780-5E59-42B1-801B-3C18F2AD08EE";
    public const string PpUnitPortIid = "51066F52-C0F9-48d7-939E-3A229010E77C";
    // Thermo IDL 02
    public const string InCapeTheMateComIid = "678C0A9B-7D66-11D2-A67D-00105A42887F";
    // Thermo IDL 03
    public const string InCapeTheMateConComIid = "678C0A9C-7D66-11D2-A67D-00105A42887F";
    public const string InCapeTheCompUndComIid = "678C0A9D-7D66-11D2-A67D-00105A42887F";
    public const string InCapeThePhasesComIid = "678C0A9E-7D66-11D2-A67D-00105A42887F";
    // Thermo IDL 04
    public const string InCapeTheProPerRouComIid = "678C0A9F-7D66-11D2-A67D-00105A42887F";
    public const string InCapeTheEqBrRouComIid = "678C0AA0-7D66-11D2-A67D-00105A42887F";
    public const string InCapeTheUnSalConComIid = "678C0AA1-7D66-11D2-A67D-00105A42887F";
    public const string InCapeTheProPackManComIid = "678C0AA2-7D66-11D2-A67D-00105A42887F";
    // Cape Thermo System
    public const string PpCapeThermoSystemIid = "B5483FD2-E8AB-4ba4-9EA6-53BBDB77CE81";
    // Boolean Parameter
    public const string InCaBoolParaSpecEveIid = "07D17ED3-B25A-48EA-8261-5ED2D076ABDD";
    public const string PpBooleanParameterIid = "8B8BC504-EEB5-4a13-B016-9614543E4536";
    public const string BooleanParameterWrapIid = "A6751A39-8A2C-4AFC-AD57-6395FFE0A7FE";
    // COFE IDL
    public const string CofeStreamTypeIid = "D1B15843-C0F5-4CB7-B462-E1B80456808E";
    public const string InCofeStreamIid = "B2A15C45-D878-4E56-A19A-DED6A6AD6F91";
    public const string InCofeMaterialIid = "2BFFCBD3-7DAB-439D-9E25-FBECC8146BE8";
    public const string InCofeIconIid = "5F6333E0-434F-4C03-85E2-6EB493EAE846";
    // COM Material Object Wrapper
    public const string ComMateObjWrapIid = "5A65B4B2-2FDD-4208-813D-7CC527FB91BD";
    // Integer Parameter
    public const string InCapeIntParaSpecEveIid = "2EA7C47A-A4E0-47A2-8AC1-658F96A0B79D";
    public const string PpIntegerParameterIid = "2C57DC9F-1368-42eb-888F-5BC6ED7DDFA7";
    public const string IntegerParaWrapperIid = "EFC01B53-9A6A-4AD9-97BE-3F0294B3BBFB";
    // Option Parameter
    public const string InOptParSpecEveIid = "991F95FB-2210-4E44-99B3-4AB793FF46C2";
    public const string PpOptionParameterIid = "8EB0F647-618C-4fcc-A16F-39A9D57EA72E";
    public const string OptParaWrapperIid = "70994E8C-179E-40E1-A51B-54A5C0F64A84";
    // Petro Fractions IDL
    public const string InCapePetFractionsIid = "72A94DE9-9A69-4369-B508-C033CDFD4F81";
    public const string CapeCompoundTypeIid = "8091E285-3CFA-4a41-A5C4-141D0D709D87";
    // Reactions IDL
    public const string PpCapeReactTypeIid = "678c0b00-0100-11d2-a67d-00105a42887f";
    public const string PpCapeReactRaBaIid = "678c0aff-0100-11d2-a67d-00105a42887f";
    // Unit Operation Manager
    public const string PpUnitOperaWrapIid = "B41DECE0-6C99-4CA4-B0EB-EFADBDCE23E9";
}
