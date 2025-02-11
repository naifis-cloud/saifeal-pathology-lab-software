using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using CommonFunctions;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BusinessLogic
/// </summary>
public class BusinessLogic
{
    public BusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertUpdateCollCenter(int fiColCenterId, string fstrCenterCode, string fstrCenterName, string fstrCenterAdd, int fiStateId,
                                      int fiCityId, string fstrPinCode, string fstrEmail, string fstrMobileNo, string fstrPhoneNo, string fstrDoctorName,
                                        string fstrLabUnit, int fiRateListId, int fiDepositAmt, string fstrUserName, string fstrPassword, bool flgStatus)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateCollCenter", fiColCenterId,
            fstrCenterCode, fstrCenterName, fstrCenterAdd, fiStateId, fiCityId, fstrPinCode, fstrEmail, fstrMobileNo, fstrPhoneNo, fstrDoctorName,
            fstrLabUnit, fiRateListId, fiDepositAmt, fstrUserName, fstrPassword, flgStatus));
    }

    public DataTable GetAllCollCenters(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText, int fiColCenterId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllCollCenters", fiPageNum, fiPageSize, fstrSortColumn,
            fstrSearchText, fiColCenterId).Tables[0];
    }

    public DataTable GetCollCenterDetails(int fiCollCenterId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetCollCenterDetails", fiCollCenterId).Tables[0];
    }

    /// <summary>
    /// To fill all the collection centers ///
    /// </summary>
    public static void FillCollCenters(DropDownList foDropDownList, int fiColCenterId)
    {
        DataTable lobjdtCenters = new DataTable();

        lobjdtCenters = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllCollCenters", 0, 0, "stCenterName ASC", "", fiColCenterId).Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtCenters.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stCenterName"]), Convert.ToString(lodtRow["inColCenterId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtCenters = null;
        }
    }

    public int DeleteCollectionCenter(int fiCollCenterId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteCollCenter", fiCollCenterId));
    }

    public int InsertUpdateSubCollCenter(int fiSubColCenterId, int fiColCenterId, string fstrCenterCode, string fstrCenterName, string fstrCenterAdd, int fiStateId,
                                      int fiCityId, string fstrPinCode, string fstrEmail, string fstrMobileNo, string fstrPhoneNo, string fstrDoctorName,
                                        string fstrLabUnit, int fiRateListId, int fiDepositAmt, string fstrUserName, string fstrPassword, bool flgStatus)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateSubCollCenter", fiSubColCenterId, fiColCenterId,
            fstrCenterCode, fstrCenterName, fstrCenterAdd, fiStateId, fiCityId, fstrPinCode, fstrEmail, fstrMobileNo, fstrPhoneNo, fstrDoctorName,
            fstrLabUnit, fiRateListId, fiDepositAmt, fstrUserName, fstrPassword, flgStatus));
    }

    public DataTable GetAllSubCollCenters(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText, int fiColcenterId, int fiSubColcenterId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSubCollCenters", fiPageNum, fiPageSize, fstrSortColumn,
            fstrSearchText, fiColcenterId, fiSubColcenterId).Tables[0];
    }

    /// <summary>
    /// To fill all the collection centers ///
    /// </summary>
    public static void FillSubCollCenters(DropDownList foDropDownList, int fiColcenterId, int fiSubColcenterId)
    {
        DataTable lobjdtCenters = new DataTable();

        lobjdtCenters = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSubCollCenters", 0, 0, "CC.stCenterCode ASC", "",
            fiColcenterId, fiSubColcenterId).Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtCenters.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stCenterName"]), Convert.ToString(lodtRow["inSubColCenterId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtCenters = null;
        }
    }

    public DataTable GetSubCollCenterDetails(int fiSubCollCenterId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetSubCollCenterDetails", fiSubCollCenterId).Tables[0];
    }

    public int DeleteSubCollectionCenter(int fiSubCollCenterId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteSubCollCenter", fiSubCollCenterId));
    }

    public int InsertUpdateMainLabRateList(int fiRateListId, string fstrRateListCode, string fstrRateListName, string fstrRateListDesc)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateMainLabRateList", fiRateListId, fstrRateListCode, fstrRateListName, fstrRateListDesc));
    }

    public DataTable GetAllMainLabRateLists(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllMainLabRateLists", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetMainlabRateListDetails(int fiMainLabRateListId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetMainlabRateListDetails", fiMainLabRateListId).Tables[0];
    }

    public int DeleteMainLabRateList(int fiRateListId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteMainLabRateList", fiRateListId));
    }

    /// <summary>
    /// To fill all the cities of a particular state ///
    /// </summary>
    public static void FillRateLists(DropDownList foDropDownList)
    {
        DataTable lobjdtCenters = new DataTable();

        lobjdtCenters = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllMainLabRateLists", 0, 0, "inRateListId ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtCenters.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stRateListCode"]) + " - " + Convert.ToString(lodtRow["stRateListName"]), Convert.ToString(lodtRow["inRateListId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtCenters = null;
        }
    }

    public int InsertUpdateCollLabRateList(int fiRateListId, string fstrRateListCode, string fstrRateListName, string fstrRateListDesc)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateCollLabRateList", fiRateListId, fstrRateListCode, fstrRateListName, fstrRateListDesc));
    }

    public DataTable GetAllCollLabRateLists(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllCollLabRateLists", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetColllabRateListDetails(int fiMainLabRateListId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetColllabRateListDetails", fiMainLabRateListId).Tables[0];
    }

    public int DeleteCollLabRateList(int fiRateListId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteCollLabRateList", fiRateListId));
    }

    /// <summary>
    /// To fill all the cities of a particular state ///
    /// </summary>
    public static void FillCollectionRateLists(DropDownList foDropDownList)
    {
        DataTable lobjdtRateLists = new DataTable();

        lobjdtRateLists = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllCollLabRateLists", 0, 0, "inRateListId ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtRateLists.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stRateListCode"]) + " - " + Convert.ToString(lodtRow["stRateListName"]), Convert.ToString(lodtRow["inRateListId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtRateLists = null;
        }
    }

    public int InsertUpdateMachine(int fiMachineListId, string fstrCode, string fstrMachineName, string fstrMachineDesc)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateMachine", fiMachineListId, fstrCode, fstrMachineName, fstrMachineDesc));
    }

    public DataTable GetAllMachines(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllMachines", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public static void FillMachines(DropDownList foDropDownList)
    {
        DataTable lobjdtDepts = new DataTable();

        lobjdtDepts = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllMachines", 0, 0, "stMachineName ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtDepts.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stMachineName"]), Convert.ToString(lodtRow["inMachineId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtDepts = null;
        }
    }

    public DataTable GetMachineDetails(int fiMachineId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetMachineDetails", fiMachineId).Tables[0];
    }

    public int DeleteMachine(int fiMachineId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteMachine", fiMachineId));
    }

    public int InsertUpdateSample(int fiSampleId, string fstrSampleName)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateSample", fiSampleId, fstrSampleName));
    }

    public DataTable GetAllSamples(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSamples", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public static void FillSamples(DropDownList foDropDownList)
    {
        DataTable lobjdtDepts = new DataTable();

        lobjdtDepts = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSamples", 0, 0, "stSampleName ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtDepts.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stSampleName"]), Convert.ToString(lodtRow["inSampleId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtDepts = null;
        }
    }

    public DataTable GetSampleDetails(int fiSampleId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetSampleDetails", fiSampleId).Tables[0];
    }

    public int DeleteSample(int fiSampleId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteSample", fiSampleId));
    }

    /// <summary>
    /// To fill all the main departments ///
    /// </summary>
    public static void FillMainDepartments(DropDownList foDropDownList)
    {
        DataTable lobjdtDepts = new DataTable();

        lobjdtDepts = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllMainDepartments", 0, 0, "stMainDeptCode ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtDepts.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stMainDeptCode"]) + " - " + Convert.ToString(lodtRow["stMainDeptName"]),
                    Convert.ToString(lodtRow["inMainDeptId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtDepts = null;
        }
    }

    public int InsertUpdateSubDepartment(int fiSubDeptId, int fiMainDeptId, string fstrSubDeptCode, string fstrSubDeptName, int fiSortOrder, string fstrRemarks)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateSubDepartment", fiSubDeptId, fiMainDeptId,
            fstrSubDeptCode, fstrSubDeptName, fiSortOrder, fstrRemarks));
    }

    public DataTable GetAllSubDepartments(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSubDepartments", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetSubDepartmentDetails(int fiSubDeptId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetSubDepartmentDetails", fiSubDeptId).Tables[0];
    }

    public int DeleteSubDepartment(int fiSubDeptId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteSubDepartment", fiSubDeptId));
    }

    public int InsertUpdateReferalDoctor(int fiRefDoctorId, string fstrCode, string fstrName, string fstrAffiliateType, int fiColcenterId, string fstrAddress,
        int fiStateId, int fiCityId, string fstrPinCode, string fstrEmail, string fstrMobileNo, string fstrPhoneNo, string fstrSex,
        DateTime fdtDOB, string fstrQualification, int fiRefPercentage, string fstrClinicName, string fstrUserName, string fstrPassword,
        bool fbIsActive)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateReferalDoctor",
            fiRefDoctorId, fstrCode, fstrName, fstrAffiliateType, fiColcenterId, fstrAddress, fiStateId, fiCityId, fstrPinCode, fstrEmail, fstrMobileNo, fstrPhoneNo, fstrSex,
        fdtDOB, fstrQualification, fiRefPercentage, fstrClinicName, fstrUserName, fstrPassword, fbIsActive));
    }

    public DataTable GetAllReferalDoctors(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText,
        int fiColCenterId, int fiSubColCenterId, int fiRefDoctorId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllReferalDoctors", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText,
            fiColCenterId, fiSubColCenterId, fiRefDoctorId).Tables[0];
    }

    /// <summary>
    /// To fill all the Referral Doctors ///
    /// </summary>
    public static void FillRefDoctors(DropDownList foDropDownList)
    {
        DataTable lobjdtLabs = new DataTable();

        lobjdtLabs = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllReferalDoctors", 0, 0, "stCode ASC", "", 0, 0, 0).Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtLabs.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stCode"]) + " - " + Convert.ToString(lodtRow["stName"]),
                    Convert.ToString(lodtRow["inRefDoctorId"])));
            }

            foDropDownList.Items.Add(new ListItem("Other", "Other"));
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtLabs = null;
        }
    }

    public DataTable GetReferalDoctorDetails(int fiRefDoctorId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetRefDoctorDetails", fiRefDoctorId).Tables[0];
    }

    public int DeleteReferalDoctor(int fiRefDoctorId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteReferalDoctor", fiRefDoctorId));
    }

    public int InsertUpdateVendorLab(int fiVendorLabId, string fstrCode, string fstrName, string fstrAddress,
        int fiStateId, int fiCityId, string fstrPinCode, string fstrPhoneNo, string fstrMobileNo, string fstrEmail, string fstrContactPerson,
        string fstrWebsiteName)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateVendorLab",
            fiVendorLabId, fstrCode, fstrName, fstrAddress, fiStateId, fiCityId, fstrPinCode, fstrPhoneNo, fstrMobileNo, fstrEmail,
            fstrContactPerson, fstrWebsiteName));
    }

    public DataTable GetAllVendorLabs(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllVendorLabs", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    /// <summary>
    /// To fill all the Vendor Labs ///
    /// </summary>
    public static void FillVendorLabs(DropDownList foDropDownList)
    {
        DataTable lobjdtLabs = new DataTable();

        lobjdtLabs = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllVendorLabs", 0, 0, "stVendorLabCode ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtLabs.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stVendorLabCode"]) + " - " + Convert.ToString(lodtRow["stVendorLabName"]),
                    Convert.ToString(lodtRow["inVendorLabId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtLabs = null;
        }
    }

    public DataTable GetVendorLabDetails(int fiVendorLabId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetVendorLabDetails", fiVendorLabId).Tables[0];
    }

    public int DeleteVendorLab(int fiVendorLabId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteVendorLab", fiVendorLabId));
    }


    public int InsertUpdateTest(int fiTestId, string fstrTestCode, string fstrTestName, string fstrTestRoutine, int fiMainDeptId, string fstrTestDesc,
                                int fiOrder, string fstrTestMethod, string fstrICD10Code, string fstrHCPCSCode, string fstrCPTCode, string fstrTechnology,
                                string fstrMaterial, string fstrRemarks, string fstrConditions, int fiMachineId, int fiSampleType, string fstrFormula,
                                int fiDays, string fstrShortTerm, string fstrInstructions, string fstrSex, string fstrAttachGraph, string fstrActive,
                                string fstrParams, string fstrType, string fstrRange, int fiTestRate)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateTest", fiTestId, fstrTestCode, fstrTestName,
                                fstrTestRoutine, fiMainDeptId, fstrTestDesc, fiOrder, fstrTestMethod, fstrICD10Code, fstrHCPCSCode, fstrCPTCode, fstrTechnology,
                                fstrMaterial, fstrRemarks, fstrConditions, fiMachineId, fiSampleType, fstrFormula, fiDays, fstrShortTerm,
                                fstrInstructions, fstrSex, fstrAttachGraph, fstrActive, fstrParams, fstrType, fstrRange, fiTestRate));
    }

    public DataTable GetAllTests(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTests", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetAllTestsByMainDepartment(int fiMainDeptId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTestsByMainDepartment", fiMainDeptId).Tables[0];
    }

    public static void FillAllTestsByMainDepartment(DropDownList foDropDownList, int fiMainDeptId)
    {
        DataTable lobjdtTests = new DataTable();

        lobjdtTests = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTestsByMainDepartment", fiMainDeptId).Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtTests.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stTestCode"]) + " - " + Convert.ToString(lodtRow["stTestName"]),
                    Convert.ToString(lodtRow["inTestId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtTests = null;
        }
    }

    /// <summary>
    /// To fill all the Vendor Labs ///
    /// </summary>
    public static void FillAllTests(DropDownList foDropDownList)
    {
        DataTable lobjdtTests = new DataTable();

        lobjdtTests = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTests", 0, 0, "stTestCode ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtTests.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stTestCode"]) + " - " + Convert.ToString(lodtRow["stTestName"]),
                    Convert.ToString(lodtRow["inTestId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtTests = null;
        }
    }

    /// <summary>
    /// To fill all the Tests ///
    /// </summary>
    public static void FillAllTests(ListBox foListBox)
    {
        DataTable lobjdtTests = new DataTable();

        lobjdtTests = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTests", 0, 0, "stTestCode ASC", "").Tables[0];

        try
        {
            foreach (DataRow lodtRow in lobjdtTests.Rows)
            {
                foListBox.Items.Add(new ListItem(Convert.ToString(lodtRow["stTestCode"]) + " - " + Convert.ToString(lodtRow["stTestName"]),
                    Convert.ToString(lodtRow["inTestId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtTests = null;
        }
    }

    public DataTable GetTestDetails(int fiTestId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetTestDetails", fiTestId).Tables[0];
    }

    public int DeleteTest(int fiTestId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteTest", fiTestId));
    }

    public int InsertUpdateVendorTestRate(int fiVendorTestRateId, int fiVendorLabId, int fiTestId, int fiRate)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateVendorTestRates",
            fiVendorTestRateId, fiVendorLabId, fiTestId, fiRate));
    }

    public DataTable GetAllVendorTestRates(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllVendorTestRates", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetVendorTestRateDetails(int fiVendorTestRateId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetVendorTestRateDetails", fiVendorTestRateId).Tables[0];
    }

    public int DeleteVendorTestRate(int fiVendorTestRateId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteVendorTestRate", fiVendorTestRateId));
    }

    public int InsertUpdateTestAbbrev(int fiTestAbbrevId, int fiTestId, string fstrTestAbbrevCode, string fstrTestAbbrevDesc)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateTestAbbrev",
            fiTestAbbrevId, fiTestId, fstrTestAbbrevCode, fstrTestAbbrevDesc));
    }

    public DataTable GetAllTestAbbrevs(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTestAbbrevs", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetTestAbbrevDetails(int fiTestAbbrevId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetTestAbbrevDetails", fiTestAbbrevId).Tables[0];
    }

    public int DeleteTestAbbrev(int fiTestAbbrevId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteTestAbbrev", fiTestAbbrevId));
    }

    public int InsertUpdateTestInterpretation(int fiTestId, string fstrTestInterText)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateTestInterpretation", fiTestId, fstrTestInterText));
    }

    public DataTable GetAllTestInterpretations(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTestInterpretations", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetTestInterPretDetails(int fiTestId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetTestInterPretDetails", fiTestId).Tables[0];
    }

    public int DeleteTestInterPret(int fiTestInterPretId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteTestInterPret", fiTestInterPretId));
    }


    public int InsertUpdateNormalRange(int fiNormalRangeId, int inMainDeptId, string fstrCode, int fiTestId, string fstrGender, int liLowerRange,
                                        int liUpperRange, int fiDays, string fiUnit, string fstrRemarks)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateNormalRange",
        fiNormalRangeId, inMainDeptId, fstrCode, fiTestId, fstrGender, liLowerRange, liUpperRange, fiDays, fiUnit, fstrRemarks));
    }

    public DataTable GetAllNormalRange(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllNormalRange", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetNormalRangeDetails(int fiNormalRangeId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetNormalRangeDetails", fiNormalRangeId).Tables[0];
    }

    public int DeleteNormalRange(int fiNormalRangeId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteNormalRange", fiNormalRangeId));
    }

    public int InsertUpdateProfile(int fiProfileId, string fstrCode, string fstrName, string fstrMethod, int inMainDeptId, int liGroupRate)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateProfile",
        fiProfileId, fstrCode, fstrName, fstrMethod, inMainDeptId, liGroupRate));
    }

    public int InsertUpdateProfileTest(int fiProfileId, int inTestId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateProfileTest", fiProfileId, inTestId));
    }

    public DataTable GetAllProfiles(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllProfiles", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetAllProfileTests(int fiProfileId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllProfileTests", fiProfileId).Tables[0];
    }

    public DataTable GetProfileDetails(int fiNormalRangeId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetProfileDetails", fiNormalRangeId).Tables[0];
    }

    public void DeleteProfile(int fiNormalRangeId)
    {
        SqlHelper.ExecuteNonQuery(Commonfunction.getConnectionString(), "usp_DeleteProfile", fiNormalRangeId);
    }

    public int DeleteProfileTests(int fiProfileId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteProfileTests", fiProfileId));
    }

    public void InsertMainLabRates(int fiRateListId, int fiTestId, int fiRate)
    {
        SqlHelper.ExecuteNonQuery(Commonfunction.getConnectionString(), "usp_InsertMainLabRates", fiRateListId, fiTestId, fiRate);
    }

    public DataTable GetMainLabRates(int fiRateListId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetMainLabRates", fiRateListId).Tables[0];
    }

    public void DeleteMainLabRates(int fiRateListId)
    {
        SqlHelper.ExecuteNonQuery(Commonfunction.getConnectionString(), "usp_DeleteMainLabRates", fiRateListId);
    }

    public void InsertCollectionLabRates(int fiRateListId, int fiTestId, int fiRate)
    {
        SqlHelper.ExecuteNonQuery(Commonfunction.getConnectionString(), "usp_InsertCollectionLabRates", fiRateListId, fiTestId, fiRate);
    }

    public DataTable GetCollectionLabRates(int fiRateListId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetCollectionLabRates", fiRateListId).Tables[0];
    }

    public void DeleteCollectionLabRates(int fiRateListId)
    {
        SqlHelper.ExecuteNonQuery(Commonfunction.getConnectionString(), "usp_DeleteCollectionLabRates", fiRateListId);
    }

    public int InsertUpdateSupplier(int fiSupplierId, string fstrCode, string fstrName, string fstrAddress, int inStateId, int liCityId, string fstrPinCode,
        string fstrEmail, string fstrMobileNo, string fstrPhoneNo, string fstrCSTNo, string fstrTINNo, string fstrType, string fstrPaymentMode, bool fbIsActive)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateSupplier",
        fiSupplierId, fstrCode, fstrName, fstrAddress, inStateId, liCityId, fstrPinCode, fstrEmail, fstrMobileNo, fstrPhoneNo,
        fstrCSTNo, fstrTINNo, fstrType, fstrPaymentMode, fbIsActive));
    }

    public DataTable GetAllSuppliers(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSuppliers", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    /// <summary>
    /// To fill all the Suppliers ///
    /// </summary>
    public static void FillAllSuppliers(DropDownList foDropDownList)
    {
        DataTable lobjdtTests = new DataTable();

        lobjdtTests = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllSuppliers", 0, 0, "stCode ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtTests.Rows)
            {
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stCode"]) + " - " + Convert.ToString(lodtRow["stName"]),
                    Convert.ToString(lodtRow["inSupplierId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtTests = null;
        }
    }

    public DataTable GetSupplierDetails(int fiSupplierId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetSupplierDetails", fiSupplierId).Tables[0];
    }

    public int DeleteSupplier(int fiSupplierId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteSupplier", fiSupplierId));
    }

    public int InsertUpdateItem(int fiItemId, string fstrCode, string fstrName, string fstrAddress, int inSupplierId, string fstrCategory,
        string fstrBrand, int fiQuantity, bool fbIsActive)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateItem",
        fiItemId, fstrCode, fstrName, fstrAddress, inSupplierId, fstrCategory, fstrBrand, fiQuantity, fbIsActive));
    }

    public DataTable GetAllItems(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllItems", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetItemDetails(int fiItemId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetItemDetails", fiItemId).Tables[0];
    }

    public int DeleteItem(int fiItemId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteItem", fiItemId));
    }

    public DataTable GetItemsBySupplier(int fiSupplierId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetItemsBySupplier", fiSupplierId).Tables[0];
    }


    public int InsertUpdateRole(int fiRoleId, string fstrCode, string fstrName, int inSortOrderNo, string fstrRemarks)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateRole",
        fiRoleId, fstrCode, fstrName, inSortOrderNo, fstrRemarks));
    }

    public DataTable GetAllRoles(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllRoles", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetRoleDetails(int fiRoleId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetRoleDetails", fiRoleId).Tables[0];
    }

    public int DeleteRole(int fiRoleId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteRole", fiRoleId));
    }

    /// <summary>
    /// To fill all the Roles ///
    /// </summary>
    public static void FillAllRoles(DropDownList foDropDownList)
    {
        DataTable lobjdtTests = new DataTable();

        lobjdtTests = SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllRoles", 0, 0, "stCode ASC", "").Tables[0];

        foDropDownList.Items.Clear();
        foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
        try
        {
            foreach (DataRow lodtRow in lobjdtTests.Rows)
            {
                //foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stCode"]) + " - " + Convert.ToString(lodtRow["stRoleName"]),
                //    Convert.ToString(lodtRow["inRoleId"])));
                foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stRoleName"]), Convert.ToString(lodtRow["inRoleId"])));
            }
        }
        catch (Exception foexception)
        {
            throw new Exception(foexception.Message);
        }
        finally
        {
            lobjdtTests = null;
        }
    }

    public int InsertUpdateUser(int fiUserId, string fstrCode, string fstrName, int fiRoleId, string fstrAddress, int fiStateId,
        int fiCityId, string fstrPinCode, string fstrEmail, string fstrMobileNo, string fstrPhoneNo, string fstrSex, int fiAge,
        string fstrQualification, string fstrUsername, string fstrPassword, bool lbIsActive)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateUser",
        fiUserId, fstrCode, fstrName, fiRoleId, fstrAddress, fiStateId, fiCityId, fstrPinCode, fstrEmail, fstrMobileNo, fstrPhoneNo, fstrSex, fiAge,
        fstrQualification, fstrUsername, fstrPassword, lbIsActive));
    }

    public DataTable GetAllUsers(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllUsers", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetUserDetails(int fiUserId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetUserDetails", fiUserId).Tables[0];
    }

    public int DeleteUser(int fiRoleId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteUser", fiRoleId));
    }

    public DataTable GetAllModules(int fiRoleId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllModules", fiRoleId).Tables[0];
    }

    public int InsertRoleRights(int fiRoleId, int fiModuleId, bool fbView, bool fbAdd, bool fbEdit, bool fbDelete)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertRoleRights", fiRoleId, fiModuleId, fbView, fbAdd, fbEdit, fbDelete));
    }

    public int DeleteRoleRights(int fiRoleId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteRoleRights", fiRoleId));
    }

    public int InsertUpdateTestDescription(int fiTestId, string fstrTestDescription)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateTestDescription", fiTestId, fstrTestDescription));
    }

    public DataTable GetTestDescription(int fiTestId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetTestDescription", fiTestId).Tables[0];
    }

    public int InsertUpdateTestParameter(int fiParamId, int fiTestId, string fstrType, string fstrCode, string fstrName, int fiOrderNo, string fstrDesc,
                            string fstrTechnology, string fstrMaterial, string fstrMethod, string fstrShortForm, int fiMachineId, int fiSampleId,
                            string fstrDefaultValue, string fstrUpper, string fstrLower, string fstrUnit, bool lbBold, bool lbUnderLine, string fstrColor, int fiLines)
    {

        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateTestParameter",
            fiParamId, fiTestId, fstrType, fstrCode, fstrName, fiOrderNo, fstrDesc, fstrTechnology, fstrMaterial, fstrMethod, fstrShortForm, fiMachineId, fiSampleId,
            fstrDefaultValue, fstrUpper, fstrLower, fstrUnit, lbBold, lbUnderLine, fstrColor, fiLines));
    }

    public DataTable GetAllTestParameters(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText, int fiTestId, int fiTypeFlag)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllTestParameters", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText,
                                        fiTestId, fiTypeFlag).Tables[0];
    }

    public DataTable GetPatientTestParameters(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText, int fiTestId, int fiTypeFlag, int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTestParameters", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText,
                                        fiTestId, fiTypeFlag, fiPatientId).Tables[0];
    }

    public int DeleteTestParameter(int fiTestId, int fiParamId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteTestParameter", fiTestId, fiParamId));
    }

    public DataTable GetParameterDetails(int fiParamId, int fiTestId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetParameterDetails", fiParamId, fiTestId).Tables[0];
    }

    public int InsertUpdatePatient(int fiPatient, string fstrRegNo, string fstrType, string fstrEmail, string fstrTitle, string fstrPatientName, string fstrGender, int fiAge,
                                string fstrMobileNo, string fstrSampleTime, string fstrRefByPartner, string fstrAddress, string fstrClinicalHistory,
                                string fstrRemarks, int fiRefDoctorId, string fstrOtherDoctor, int fiDepartmentId, string fstrVialId, int fiVisitChgs,
                                int fiEmergencyChgs, int fiOtherChgs, int fiTotalTestChgs, string fstrPayType, int fiDiscountPerc, int fiDiscountAmt,
                                int fiNetAmt, int fiAdvanceAmt, int fiBalanceAmt, int inTestStatusId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdatePatient", fiPatient, fstrRegNo, fstrType,
                                fstrEmail, fstrTitle, fstrPatientName, fstrGender, fiAge, fstrMobileNo, fstrSampleTime, fstrRefByPartner,
                                fstrAddress, fstrClinicalHistory, fstrRemarks, fiRefDoctorId, fstrOtherDoctor, fiDepartmentId,
                                fstrVialId, fiVisitChgs, fiEmergencyChgs, fiOtherChgs, fiTotalTestChgs, fstrPayType,
                                fiDiscountPerc, fiDiscountAmt, fiNetAmt, fiAdvanceAmt, fiBalanceAmt, inTestStatusId));
    }

    public int DeletePatientTests(int fiPatientId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeletePatientTests", fiPatientId));
    }

    public int InsertPatientTests(int fiPatientId, int inTestId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertPatientTests", fiPatientId, inTestId));
    }

    public DataTable GetAllPatients(string fstrFrom, string fstrTo, int fiDepartmentId, int fiTestStatus, string fstrPatientName
                                    , string fstrRegNo, string fstrBillStatus, int fiRefDoctorId, int fiPrintCountFlag)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllPatients", fstrFrom, fstrTo, fiDepartmentId, fiTestStatus,
                                        fstrPatientName, fstrRegNo, fstrBillStatus, fiRefDoctorId, fiPrintCountFlag).Tables[0];
    }

    public static string GetPatientRegNo()
    {
        return Convert.ToString(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_GetPatientRegNo"));
    }

    public void InsertUpdatePayment(int fiPatientId, int fiPaymentAmt, string fstrPaymentFlag, int fiFlag)
    {
        SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertPayment", fiPatientId, fiPaymentAmt, fstrPaymentFlag, fiFlag);
    }

    public DataTable GetPatientDetails(int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientDetails", fiPatientId).Tables[0];
    }

    public DataTable GetPatientTests(int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTests", fiPatientId).Tables[0];
    }

    public DataTable GetPatientTestsForParamsEntry(int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTestsForParamsEntry", fiPatientId).Tables[0];
    }

    public DataTable GetPatientTestsForBarCode(int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTestforBarCode", fiPatientId).Tables[0];
    }

    public void InsertTestBarCodes(int fiPatientId, int fiTestId, string fstrBarCode)
    {
        SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertTestBarCode", fiPatientId, fiTestId, fstrBarCode);
    }

    public int ValidateTestBarcodes(int fiPatientId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_ValidateTestBarcodes", fiPatientId));
    }

    public int UpdateTestStatus(int fiPatientId, int fiTestStatusId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateTestStatus", fiPatientId, fiTestStatusId));
    }

    public void DeleteTestBarCodes(int fiPatientId)
    {
        SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteTestBarCode", fiPatientId);
    }

    public int InsertPatientTestParams(int fiPatientId, int fiTestId, int fiParamId, string fstrParamValue)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertPatientTestParams", fiPatientId, fiTestId, fiParamId, fstrParamValue));
    }

    public int ValidatePatientTestParams(int fiPatientId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_ValidatePatientTestParams", fiPatientId));
    }

    public int UpdateDispatchDetails(int fiPatientId, int fiTestStatusId, string fstrHandOverTo)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateDispatchDetails", fiPatientId, fiTestStatusId, fstrHandOverTo));
    }

    public DataTable GetCollectionSummaryReport(string fstrFrom, string fstrTo, int fiNoOfDays)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetCollectionSummaryReport", fstrFrom, fstrTo, fiNoOfDays).Tables[0];
    }

    public DataTable GetDeptWiseCollectionReport(string fstrFrom, string fstrTo, int fiDeptId, int fiNoOfDays)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetDeptWiseCollectionReport", fstrFrom, fstrTo, fiDeptId, fiNoOfDays).Tables[0];
    }

    public DataTable GetDoctorIncentiveReport(string fstrFrom, string fstrTo, int fiNoOfDays)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetDoctorIncentiveReport", fstrFrom, fstrTo, fiNoOfDays).Tables[0];
    }

    public int ValidateUserLogin(string lstrUserName, string lstrPassword, int fiRoleId, int fiRoleFlag)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_ValidateUserLogin", lstrUserName, lstrPassword, fiRoleId, fiRoleFlag));
    }

    /// <summary>
    /// To fill all the collection centers ///
    /// </summary>
    public static bool GetUserAccessRights(int fiRoleId, int fiModuleId, string fstrAction)
    {
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_GetUserAccessRights", fiRoleId, fiModuleId, fstrAction));
    }

    public int UpdateAuthoriseStatus(int fiPatientId, int fiTestId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateAuthoriseStatus", fiPatientId, fiTestId));
    }

    public bool GetAuthoriseStatus(int fiPatientId, int fiTestId)
    {
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_GetAuthoriseStatus", fiPatientId, fiTestId));
    }

    public DataTable GetTestAuthoriseCount(int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetTestAuthoriseCount", fiPatientId).Tables[0];
    }

    public DataTable GetPatientTestReportData(int fiPatientId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTestReportData", fiPatientId).Tables[0];
    }

    public DataTable GetPatientTestParamsForReport(int fiPatientId, int fiTestId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTestParamsForReport", fiPatientId, fiTestId).Tables[0];
    }

    public void UpdateTestPrintCount(int fiPatientId)
    {
        SqlHelper.ExecuteNonQuery(Commonfunction.getConnectionString(), "usp_UpdateTestPrintCount", fiPatientId);
    }

    public int ValidateUserEmail(string lstrEmailAddress, int fiRoleId, int fiRoleFlag)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_ValidateUserEmail", lstrEmailAddress, fiRoleId, fiRoleFlag));
    }

    public int UpdateRejectionDetails(int fiPatientId, int fiTestStatusId, string fstrRejectReason)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateRejectionDetails", fiPatientId, fiTestStatusId, fstrRejectReason));
    }

    public int UpdateDefaultPriceListForMainLab(int fiRateListId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_SetDefaultPriceForMainLab", fiRateListId));
    }

    public int GetTestRate(int fiTestId, int liRoleFlag, int fiUserId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_GetTestRate", fiTestId, liRoleFlag, fiUserId));
    }

    public int InsertUpdateReportSettings(int fiRptSettingsId, string fstrRptSettingsCode, string fstrRptSettingsName, string fstrRptSettingsDesc, int fiLeftMargin,
                                         int fiRightMargin, int fiTopMargin, int fiBottomMargin, string flgOuterBorder, string flgHeaderVisible, string flgLogoVisible,
                                         string flgFooterVisible, string fiSignVisible, int fiRptFontFamilyId, int fiRptFontSize, string fstrTextOnReport, string fstrLogo,
                                         string fstrSignature, string fstrLabName, string fstrAddress, string fstrPhone, string fstrFooterText)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateReportSettings", fiRptSettingsId, fstrRptSettingsCode,
                                fstrRptSettingsName, fstrRptSettingsDesc, fiLeftMargin, fiRightMargin, fiTopMargin, fiBottomMargin, flgOuterBorder,
                                flgHeaderVisible, flgLogoVisible, flgFooterVisible, fiSignVisible, fiRptFontFamilyId, fiRptFontSize, fstrTextOnReport,
                                fstrLogo, fstrSignature, fstrLabName, fstrAddress, fstrPhone, fstrFooterText));
    }

    public DataTable GetAllReportSettings(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllReportSettings", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetReportSettingsDetails(int fiRptSettingsId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetReportSettingsDetails", fiRptSettingsId).Tables[0];
    }

    public int DeleteReportSetting(int fiRptSettingsId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteReportSetting", fiRptSettingsId));
    }

    public int UpdateDefaultReportSetting(int fiRptSettingsId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateDefaultReportSetting", fiRptSettingsId));
    }



    public int InsertUpdateReceiptSettings(int fiRptSettingsId, string fstrRptSettingsCode, string fstrRptSettingsName, string fstrRptSettingsDesc, int fiLeftMargin,
                                         int fiRightMargin, int fiTopMargin, int fiBottomMargin, string flgOuterBorder, string flgHeaderVisible, string flgLogoVisible,
                                         string flgReceiptNoVisible, string fiSignVisible, int fiRptFontFamilyId, int fiRptFontSize, string fstrTextOnReport, string fstrLogo,
                                         string fstrSignature, string fstrLabName, string fstrAddress, string fstrPhone, string fstrFooterText)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertUpdateReceiptSettings", fiRptSettingsId, fstrRptSettingsCode,
                                fstrRptSettingsName, fstrRptSettingsDesc, fiLeftMargin, fiRightMargin, fiTopMargin, fiBottomMargin, flgOuterBorder,
                                flgHeaderVisible, flgLogoVisible, flgReceiptNoVisible, fiSignVisible, fiRptFontFamilyId, fiRptFontSize, fstrTextOnReport,
                                fstrLogo, fstrSignature, fstrLabName, fstrAddress, fstrPhone, fstrFooterText));
    }

    public DataTable GetAllReceiptSettings(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllReceiptSettings", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetReceiptSettingsDetails(int fiRCSettingsId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetReceiptSettingsDetails", fiRCSettingsId).Tables[0];
    }

    public int DeleteReceiptSetting(int fiRCSettingsId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_DeleteReceiptSetting", fiRCSettingsId));
    }

    public int UpdateDefaultReceiptSetting(int fiRCSettingsId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateDefaultReceiptSetting", fiRCSettingsId));
    }

    public DataTable GetDefaultReportSetting()
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetDefaultReportSetting").Tables[0];
    }

    public DataTable GetDefaultReceiptSetting()
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetDefaultReceiptSetting").Tables[0];
    }

    public int ValidateMainLabRateList(int fiRateListId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_ValidateMainLabRateList", fiRateListId));
    }

    public int UpdateModuleAbbreviation(int fiAbbrevId, string fstrShortCode, string fstrAbbrevCode)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_UpdateModuleAbbrev", fiAbbrevId, fstrShortCode, fstrAbbrevCode));
    }

    public DataTable GetAllAbbreviations(int fiPageNum, int fiPageSize, string fstrSortColumn, string fstrSearchText)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetAllModuleAbbrevs", fiPageNum, fiPageSize, fstrSortColumn, fstrSearchText).Tables[0];
    }

    public DataTable GetAbbreviationDetails(int fiAbbrevId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetModuleAbbrevDetails", fiAbbrevId).Tables[0];
    }

    public int InsertPatientTestDesc(int fiPatientId, int fiTestId, string fstrDescription)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_InsertPatientTestDesc", fiPatientId, fiTestId, fstrDescription));
    }

    public DataTable GetPatientTestDesc(int fiPatientId, int fiTestId)
    {
        return SqlHelper.ExecuteDataset(Commonfunction.getConnectionString(), "usp_GetPatientTestDesc", fiPatientId, fiTestId).Tables[0];
    }

    public int ValidatePatientTestDesc(int fiPatientId)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Commonfunction.getConnectionString(), "usp_ValidatePatientTestDesc", fiPatientId));
    }
}